using Game.GameStates;
using Game.Universe;
using IRSE.Managers.Handlers;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Logger = NLog.Logger;

namespace IRSE.Managers
{
    public class HandlerManager
    {
        #region Fields

        private static Logger mainLog; //mainLog.Error
        private Game.Server.ControllerManager m_controllerManager;
        private readonly Assembly m_serverAssembly;
        private Assembly m_frameworkAssembly;
        private Type m_gameStateType;

        #endregion Fields

        #region Properties

        public ChatHandler ChatHandler { get; private set; }
        public NetworkHandler NetworkHandler { get; private set; }
        public PlayerHandler PlayerHandler { get; private set; }
        public UniverseHandler UniverseHandler { get; private set; }

        public Game.Server.ControllerManager ControllerManager => m_controllerManager;

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
                GameServer server = (GameServer)m_activeState.GetValue(null);
               
                if (server == null)
                    return null;

                mainLog.Info("IRSE: Loaded GameServer Instance!");

                //FieldInfo m_controllerManagerField = server.GetType().GetField("m_controllers", BindingFlags.NonPublic | BindingFlags.Instance);

                m_controllerManager = server.Controllers;//m_controllerManagerField.GetValue(server) as Game.Server.ControllerManager;

                var universe = m_controllerManager.Universe as Game.Server.UniverseController;

                mainLog.Info("IRSE: Waiting for Universe To Populate..");
             
                while (!universe.IsInitialized)
                {
                    Thread.Sleep(1000);
                    if (universe.IsInitialized)
                    {
                        break;
                    }
                }

                mainLog.Info("IRSE: Loading Handlers..");
                NetworkHandler = new NetworkHandler(m_controllerManager);
                NetworkHandler.SetupNetworkHandler(server);

                PlayerHandler = new PlayerHandler(m_controllerManager);
                PlayerHandler.SetupPlayerHandler(server);

                ChatHandler = new ChatHandler(m_controllerManager);
                ChatHandler.SetupChatMessageHandler(NetworkHandler);

                UniverseHandler = new UniverseHandler(m_controllerManager);
                UniverseHandler.SetupHandler(server);

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
    
    }
}