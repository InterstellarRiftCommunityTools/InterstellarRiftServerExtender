using IRSE.ReflectionWrappers.ServerWrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Documents;

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
        private HandlerManager m_controllerManager;
        private DateTime m_launchedTime;
        private static NLog.Logger mainLog; //mainLog.Error
        private bool m_isRunning;

        private PluginManager m_pluginManager = null;


        #endregion Fields




        #region Properties

        public PluginManager PluginManager { get { return m_pluginManager; } }

        public HandlerManager Handlers { get { return m_controllerManager; } }

        public static ServerInstance Instance { get { return m_serverInstance; } }

        public static List<string> SystemNames = new List<string>(new string[]{"Vectron Syx","Alpha Ventura", "Sentinel Prime", "Scaverion"});

        public Assembly Assembly { get { return m_assembly; } }

        public Boolean IsRunning => isRunning;

        public TimeSpan Uptime { get { return DateTime.Now - m_launchedTime; } }


        private Boolean isRunning
        {
            get { return m_isRunning; }
            set
            {
                if (m_isRunning == value)
                {
                    return;
                }
                m_isRunning = value;
                if (m_isRunning)
                {
                    if (OnServerStarted != null)
                    {
                        OnServerStarted();
                    }
                }
                else
                {
                    if (OnServerStopped != null)
                    {
                        OnServerStopped();
                    }
                }
            }
        }


        #endregion Properties

        #region Events

        public delegate void ServerRunningEvent();

        public event ServerRunningEvent OnServerStarted;

        public event ServerRunningEvent OnServerStopped;

        #endregion Events


        public ServerInstance()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();

            m_launchedTime = DateTime.MinValue;
            m_serverThread = null;
            m_serverInstance = this;

            // Wrap IR.exe
            m_assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IR.exe"));

            // Wrap Aluna Framework as GameState was moved here.
            m_frameworkAssembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AlunaNetFramework.dll"));


            m_serverWrapper = new ServerWrapper(m_assembly, m_frameworkAssembly);
           
            ServerWrapper.Program.OnServerStarted += Program_OnServerStarted;
            ServerWrapper.Program.OnServerStopped += Program_OnServerStopped;
        }

        #region Methods

        public void Program_OnServerStarted()
        {
            m_launchedTime = DateTime.Now;
            try
            {
                m_controllerManager = new HandlerManager(m_assembly, m_frameworkAssembly);


                // Get the Handlers from the Controller
                while (m_controllerManager.GetHandlers() == null)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        if (m_controllerManager.GetHandlers() != null)
                        {
                            break;
                        }
                    }
                    catch (Exception)
                    {

                        break;
                    }

                }

                // Wait 5 seconds before activating ServerInstance.Instance.IsRunning
                Thread.Sleep(5000);

                m_isRunning = true;
                mainLog.Info("IRSE: Startup Procedure Complete!");
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

        private void Program_OnServerStopped()
        {
            m_isRunning = false;
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
            ServerWrapper.Program.StopServer();
            m_serverThread.Abort();
        }

        public void ForceGalaxySave()
        {
            if (!Instance.IsRunning)
                return;

            try
            {
                mainLog.Info("Attempting to Force a save!!!!");

                mainLog.Info("Saved Galaxy!");
            }
            catch (Exception ex)
            {
                mainLog.Error("Save Failed! Exception Info: " + ex.ToString());
            }
        }

        internal void Save(bool v)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}