using Game.ClientServer.Packets;
using Game.Configuration;
using Game.Framework;
using Game.Framework.Networking;
using Game.Framework.Threading;
using Game.Universe;
using IRSE.Managers.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Logger = NLog.Logger;

namespace IRSE.Managers
{
    public class HandlerManager
    {
        #region Fields

        private static Logger mainLog; //mainLog.Error
        private Game.Server.ControllerManager m_controllerManager;
        private UniverseHandler m_universeHandler;
        private readonly Assembly m_serverAssembly;
        private Assembly m_frameworkAssembly;
        private Type m_gameStateType;

        #endregion Fields

        #region Properties

        public Object Server { get; set; }
        public ChatHandler ChatHandler { get; private set; }
        public NetworkHandler NetworkHandler { get; private set; }
        public PlayerHandler PlayerHandler { get; private set; }
        public UniverseHandler UniverseHandler { get; private set; }

        #endregion Properties

        public HandlerManager(Assembly assembly, Assembly frameworkAssembly)
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();

            m_serverAssembly = assembly;
            m_frameworkAssembly = frameworkAssembly;
        }

        public Object GetHandlers()
        {
            try
            {
                m_gameStateType = m_frameworkAssembly.GetType("Game.GameStates.GameState");
                PropertyInfo m_activeState = m_gameStateType.GetProperty("ActiveState");
                object server = m_activeState.GetValue(null);

                if (server == null)
                    return null;

                Server = server;

                mainLog.Info("IRSE: Loaded GameServer Instance!");

                FieldInfo m_controllerManagerField = server.GetType().GetField("m_controllers", BindingFlags.NonPublic | BindingFlags.Instance);

                m_controllerManager = m_controllerManagerField.GetValue(server) as Game.Server.ControllerManager;


                var universe = m_controllerManager.Universe as Game.Server.UniverseController;

                mainLog.Info("IRSE: Waiting for Universe To Populate..");

                //SpawnGhostClients(m_controllerManager);

               
                while (universe.Galaxy.GetActiveSystemCount() != 4)
                {
                    Thread.Sleep(1000);
                    if (universe.Galaxy.GetActiveSystemCount() == 4)
                    {
                        break;
                    }
                }
                

                mainLog.Info("IRSE: Loading Handlers..");

                NetworkHandler = new NetworkHandler(m_controllerManager);
                NetworkHandler.SetupNetworkHandler(Server);


                PlayerHandler = new PlayerHandler(m_controllerManager);
                PlayerHandler.SetupPlayerHandler(Server);

                ChatHandler = new ChatHandler(m_controllerManager);
                ChatHandler.SetupChatMessageHandler(NetworkHandler);

                m_universeHandler = new UniverseHandler(m_controllerManager);
                m_universeHandler.SetupHandler(Server);


                mainLog.Info("IRSE: Loaded Universe!");

                return true;

            }
            catch (ArgumentException ex)
            {
                mainLog.Error("Failed to get handlers [Argument Exception] \r\n Exception:" + ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                mainLog.Error("Failed to get handlers \r\n Exception: " + ex.ToString());
                return null;
            }
        }



        // todo move to class

        private ReaderWriterLockObject m_startingGhostClientsLock = new ReaderWriterLockObject(LockRecursionPolicy.NoRecursion);
        private ReaderWriterLockObject m_delayedAddUpdatablesLock = new ReaderWriterLockObject(LockRecursionPolicy.NoRecursion);
        private ReaderWriterLockObject m_ghostClientsLock = new ReaderWriterLockObject(LockRecursionPolicy.NoRecursion);

        private UpdatableCollection m_updatables = new UpdatableCollection();
        private List<IUpdatable> m_delayedAddUpdatables = new List<IUpdatable>();

        private HashSet<string> m_startingGhostClients = new HashSet<string>();

        private Dictionary<string, GhostClientState> m_ghostClients = new Dictionary<string, GhostClientState>();

        public void SpawnGhostClients(Game.Server.ControllerManager controllers)
        {


            ServerInstance.SystemNames.ForEach((systemIdent) =>
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

        public void ForceGalaxySave()
        {
            if (!ServerInstance.Instance.IsRunning)
                return;

            try
            {
                mainLog.Info("Attempting to Force a save!!!!");

                (m_controllerManager.Universe.Galaxy as ServerGalaxy).SaveGalaxy(m_controllerManager, "user", m_controllerManager.Universe.Galaxy.Name.ToLower(), true);

                mainLog.Info("Saved Galaxy!");
            }
            catch (Exception ex)
            {
                mainLog.Error("Save Failed! Exception Info: " + ex.ToString());
            }
        }
    }
}