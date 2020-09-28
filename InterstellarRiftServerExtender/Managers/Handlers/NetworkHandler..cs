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

        public void SetupNetworkHandler(Object server)
        {
            try
            {
                FieldInfo m_packetHandlerField = server.GetType().GetField("m_serverPacketHandlers", BindingFlags.NonPublic | BindingFlags.Instance);
                Object m_packetHandler = m_packetHandlerField.GetValue(server);
                var m_networkControllerField = m_packetHandler.GetType().GetField("m_network", BindingFlags.NonPublic | BindingFlags.Instance);
                m_networkController = m_networkControllerField.GetValue(m_packetHandler) as Game.Server.NetworkController;
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }
    }
}