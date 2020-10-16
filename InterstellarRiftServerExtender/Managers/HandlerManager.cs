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
        private readonly Assembly m_serverAssembly;
        private Assembly m_frameworkAssembly;
        private Type m_gameStateType;

        #endregion Fields

        #region Properties

        public ChatHandler ChatHandler { get; private set; }
        public NetworkHandler NetworkHandler { get; private set; }
        public PlayerHandler PlayerHandler { get; private set; }
        public UniverseHandler UniverseHandler { get; private set; }

        public Game.Server.ControllerManager ControllerManager { get; private set; }

        #endregion Properties

        public HandlerManager(Assembly assembly, Assembly frameworkAssembly)
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();

            m_serverAssembly = assembly;
            m_frameworkAssembly = frameworkAssembly;
        }

        public GameServer GetHandlers()
        {
            try
            {
                GameServer server = (GameServer)GameState.ActiveState; 
               
                if (server == null)
                    return null;

                mainLog.Info("IRSE: Loaded GameServer Instance!");

                ControllerManager = server.Controllers;

                var universe = ControllerManager.Universe as Game.Server.UniverseController;

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
                NetworkHandler = new NetworkHandler(ControllerManager);
                NetworkHandler.SetupNetworkHandler(server);

                PlayerHandler = new PlayerHandler(ControllerManager);
                PlayerHandler.SetupPlayerHandler(server);

                ChatHandler = new ChatHandler(ControllerManager);
                ChatHandler.SetupChatMessageHandler(NetworkHandler);

                UniverseHandler = new UniverseHandler(ControllerManager);
                UniverseHandler.SetupHandler(server);

                mainLog.Info("IRSE: Loaded Universe!");

                return server;
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