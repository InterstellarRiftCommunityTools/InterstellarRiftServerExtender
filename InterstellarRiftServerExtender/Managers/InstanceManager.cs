using Game.Configuration;
using Game.Framework;
using Game.Framework.Threading;
using Game.Universe;
using IRSE.Managers.Events;
using IRSE.Modules;
using IRSE.ReflectionWrappers.ServerWrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRSE.Managers
{
    public class ServerInstance
    {
        #region Fields

        private static Thread m_serverThread;
        private Assembly m_assembly;
        private Assembly m_frameworkAssembly;
        private static ServerInstance m_serverInstance;
        private ServerWrapper m_serverWrapper;
        private HandlerManager m_handlerManager;
        private DateTime m_launchedTime;
        private static NLog.Logger mainLog; //mainLog.Error

        private bool m_isRunning;
        private bool m_isStarting;

        private PluginManager m_pluginManager = null;

        public static List<string> SystemNames = new List<string>(new string[] { "Vectron Syx", "Alpha Ventura", "Sentinel Prime", "Scaverion" });

        #endregion Fields

        #region Properties

        public static ServerInstance Instance { get { return m_serverInstance; } }

        public HandlerManager Handlers { get { return m_handlerManager; } }
        public PluginManager PluginManager { get { return m_pluginManager; } internal set { } }

        public Assembly Assembly { get { return m_assembly; } }

        public TimeSpan Uptime { get { return DateTime.Now - m_launchedTime; } }

        private void SetIsRunning(bool run = true)
        {
            m_isRunning = run;
            if (run)
                OnServerStarted?.Invoke();
            else
                OnServerStopped?.Invoke();
        }

        internal void SetIsStarting(bool starting = true)
        {
            m_isStarting = starting;
            if (starting)
                OnServerStarting?.Invoke();
        }

        public Boolean IsRunning => m_isRunning;

        public Boolean IsStarting => m_isStarting;

        #endregion Properties

        #region Events

        public delegate void ServerRunningEvent();

        public event ServerRunningEvent OnServerStarted;

        public event ServerRunningEvent OnServerStopped;

        public event ServerRunningEvent OnServerStarting;

        #endregion Events

        public ServerInstance()
        {
            m_serverInstance = this;

            mainLog = NLog.LogManager.GetCurrentClassLogger();

            m_launchedTime = DateTime.MinValue;
            m_serverThread = null;
            

            // Wrap IR.exe
            m_assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IR.exe"));

            // Wrap Aluna Framework as GameState was moved here.
            m_frameworkAssembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AlunaNetFramework.dll"));

            // Wrap both assemblies
            m_serverWrapper = new ServerWrapper(m_assembly, m_frameworkAssembly);

            m_pluginManager = new PluginManager();
        }

        #region Methods

        private void WaitForHandlers()
        {
            // Get the Handlers from the Controller
            while (m_handlerManager.GetHandlers() == null)
            {
                try
                {
                    Thread.Sleep(1000);
                    if (m_handlerManager.GetHandlers() != null)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        internal void Hook()
        {
            m_launchedTime = DateTime.Now;
            try
            {
                Program.SetTitle(true);

                m_handlerManager = new HandlerManager(m_assembly, m_frameworkAssembly);

                WaitForHandlers();

                Game.Server.ControllerManager controllerManager = m_handlerManager.ControllerManager;

                // Intercept events to copy and invoke
                EventManager.Instance.Intercept(controllerManager);

                // command loader
                mainLog.Info("IRSE: Loading Game Console Commands..");
                // soon

                ConsoleCommandManager.InitCommands(controllerManager);

               
                // start gamelogic coroutine
                Program.ConsoleCoroutine = CommandSystem.Singleton.Logic(controllerManager, Game.Configuration.Globals.NoConsoleAutoComplete);


                //SendKeys.SendWait("{ENTER}"); // activates the game console commands

                // plugin loader
                mainLog.Info("IRSE: Initializing Plugins...");
                m_pluginManager.InitializeAllPlugins();

                // Wait 5 seconds before activating ServerInstance.Instance.IsRunning
                Thread.Sleep(2000);
                mainLog.Info("IRSE: Startup Procedure Complete!");
                SetIsRunning(); // Server is running by now
                SetIsStarting(false);
            }
            catch (Exception ex)
            {
                mainLog.Info("IRSE: Startup Procedure FAILED!");
                mainLog.Info("IRSE: Haulting Server.. Major problem detected!!!!!!!! - Exception:");
                mainLog.Error(ex.ToString());

                Console.ReadLine();
                Stop();
            }
        }

        public void Start()
        {
            if (IsRunning)
                return;

            String[] serverArgs = new String[]
                {
                    "-server",
                };
         
            m_serverThread = ServerWrapper.Program.StartServer(serverArgs);
            m_serverWrapper.Init();
        }

        public void Stop()
        {

            if (ServerInstance.Instance.IsRunning)
            {

                Console.WriteLine("Shutting Down IR.");
                Thread.Sleep(2000);
                SetIsRunning(false);

                try
                {
                    Console.WriteLine("Saving Galaxy...");
                    Save();
                    Console.WriteLine("Shutting down plugins...");
                    PluginManager.ShutdownAllPlugins();
                    Console.WriteLine("Stopping server..");
                    ServerWrapper.Program.StopServer();

                    m_serverThread.Abort();
                }
                catch (Exception)
                {
                    // as long as the server saves, who cares
                }
            }

        }

        internal void Save()
        {
            (Handlers.ControllerManager.Universe.Galaxy as ServerGalaxy).SaveGalaxy(Handlers.ControllerManager, "user", Handlers.ControllerManager.Universe.Galaxy.Name.ToLower(), true);
        }

        // will be removed when they fix the ghost client spawner
        private ReaderWriterLockObject m_startingGhostClientsLock = new ReaderWriterLockObject(LockRecursionPolicy.NoRecursion);

        private ReaderWriterLockObject m_delayedAddUpdatablesLock = new ReaderWriterLockObject(LockRecursionPolicy.NoRecursion);
        private ReaderWriterLockObject m_ghostClientsLock = new ReaderWriterLockObject(LockRecursionPolicy.NoRecursion);

        private UpdatableCollection m_updatables = new UpdatableCollection();
        private List<IUpdatable> m_delayedAddUpdatables = new List<IUpdatable>();
        private HashSet<string> m_startingGhostClients = new HashSet<string>();
        private Dictionary<string, GhostClientState> m_ghostClients = new Dictionary<string, GhostClientState>();

        public void SpawnGhostClients(Game.Server.ControllerManager controllers)
        {
            SystemNames.ForEach((systemIdent) =>
            {
                Task.Run((Action)(() =>
                {
                    SolarSystem system = controllers.Universe.Galaxy.GetSystem(systemIdent);

                    using (WriteLock.CreateLock(m_startingGhostClientsLock))
                        m_startingGhostClients.Add(systemIdent);

                    GhostClientState ghostClientState;

                    //if(m_ghostClients.Count() != 5)

                    this.m_ghostClients.TryGetValue(systemIdent, out ghostClientState);
                    if (ghostClientState == null || !ghostClientState.PreventStart)
                    {
                        string str1 = ServerConfig.Singleton.GhostClientConsoleVisible ? "-console " : "";
                        string str2 = "-ip 127.0.0.1 ";
                        string str3 = "-port " + controllers.Network.NetGeneric.GetPort().ToString();
                        string str4 = " -GhostSystemName " + systemIdent;
                        try
                        {
                            if (ghostClientState == null)
                            {
                                ghostClientState = new GhostClientState(controllers);
                                using (WriteLock.CreateLock(m_delayedAddUpdatablesLock))
                                    m_delayedAddUpdatables.Add((IUpdatable)ghostClientState);
                            }
                            ghostClientState.Process = Process.Start(new ProcessStartInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "IRGhostClient.exe"), "-ghostclient " + str1 + str2 + str3 + str4)
                            {
                                UseShellExecute = false,
                                CreateNoWindow = !ServerConfig.Singleton.GhostClientConsoleVisible
                            });
                            ghostClientState.SystemName = systemIdent;
                            ghostClientState.ResetHeartbeat();
                            ++ghostClientState.StartCount;
                            using (WriteLock.CreateLock(m_ghostClientsLock))
                                this.m_ghostClients[systemIdent] = ghostClientState;
                            if (ServerConfig.Singleton.GhostClientStartCountThresholdEnabled && ghostClientState.StartCount >= ServerConfig.Singleton.GhostClientStartCountThreshold)
                            {
                                Console.WriteLine(string.Format("Ghost client for '{0}' started {1} in {2} seconds! Preventing start for {3}.", new object[4]
                                {
                (object) systemIdent,
                (object) ghostClientState.StartCount,
                (object) ServerConfig.Singleton.GhostClientStartCountResetDurationInSeconds,
                (object) ServerConfig.Singleton.GhostClientPreventStartDurationInSeconds
                                }), "Saving", (Exception)null);
                                ghostClientState.PreventStart = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(string.Format("Failed to start ghost client. Path: {0}, Arguments: {1}", (object)Assembly.GetEntryAssembly().Location, (object)("-ghostclient " + str1 + str2 + str3 + str4)), "Exceptions", ex);
                        }
                    }
                    using (WriteLock.CreateLock(this.m_startingGhostClientsLock))
                        this.m_startingGhostClients.Remove(systemIdent);
                }));
            });

            //Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {controllers.Network.NetGeneric.GetPort()} -GhostSystemName {system1.Identifier}");
            //Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {controllers.Network.NetGeneric.GetPort()} -GhostSystemName {system2.Identifier}");
            //Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {controllers.Network.NetGeneric.GetPort()} -GhostSystemName {system3.Identifier}");
            //Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {controllers.Network.NetGeneric.GetPort()} -GhostSystemName {system4.Identifier}");

            // dont remove this, its for after SP fixes a bug
            //MethodInfo clientMethod = server.GetType().GetMethod("StartGhostClient", BindingFlags.Instance | BindingFlags.Public);
            //clientMethod.Invoke(server, new object[] { system1 });
            //clientMethod.Invoke(server, new object[] { system2 });
            //clientMethod.Invoke(server, new object[] { system3 });
            //clientMethod.Invoke(server, new object[] { system4 });
        }

        #endregion Methods
    }
}