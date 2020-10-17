using Game.GameStates;
using NLog;
using System;
using System.Reflection;

namespace IRSE.Managers.Handlers
{
    public class NetworkHandler
    {
        #region Fields

        private Logger mainLog;
        private Game.Server.NetworkController m_networkController;

        #endregion Fields

        public Game.Server.NetworkController Network
        {
            get
            {
                return m_networkController;
            }
        }

        public NetworkHandler(Game.Server.ControllerManager controllerManager)
        {
            try
            {
                mainLog = NLog.LogManager.GetCurrentClassLogger();
                mainLog.Info("IRSE: Loading NetworkHandler...");
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }

        public void SetupNetworkHandler(GameServer server)
        {
            try
            {
                m_networkController = server.Controllers.Network;

            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }
    }
}