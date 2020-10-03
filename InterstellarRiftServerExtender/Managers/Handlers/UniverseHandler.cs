using Game.Framework.Nodes.Visual;
using Game.Universe;
using NLog;
using System;
using System.Web.UI;
using System.Windows.Controls;
using System.Windows.Forms;

namespace IRSE.Managers.Handlers
{
    public class UniverseHandler
    {
        #region Fields

        private Logger mainLog = NLog.LogManager.GetCurrentClassLogger();

        #endregion Fields

        public Game.Server.UniverseController Universe
        {
            get; private set;
        }

        public UniverseHandler(Game.Server.ControllerManager controllerManager)
        {
            Universe = controllerManager.Universe as Game.Server.UniverseController;



            //Console.WriteLine(test.ActiveSystems.FirstOrDefault());
            try
            {
                mainLog = NLog.LogManager.GetCurrentClassLogger();
                NLog.LogManager.GetCurrentClassLogger().Info("IRSE: Loading UniverseHandler...");
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }

        public void SetupHandler(Object server)
        {
            try
            {
                //FieldInfo m_packetHandlerField = server.GetType().GetField("m_serverPacketHandlers", BindingFlags.NonPublic | BindingFlags.Instance);
                //Object m_packetHandler = m_packetHandlerField.GetValue(server);
                //var m_networkControllerField = m_packetHandler.GetType().GetField("m_network", BindingFlags.NonPublic | BindingFlags.Instance);
                //m_networkController = m_networkControllerField.GetValue(m_packetHandler) as Game.Server.NetworkController;
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }
    }
}