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
        private Game.Server.ControllerManager m_controllers;

        #endregion Fields

        public Game.Server.UniverseController Universe
        {
            get; private set;
        }

        public UniverseHandler(Game.Server.ControllerManager controllerManager)
        {
            m_controllers = controllerManager;
           Universe = m_controllers.Universe as Game.Server.UniverseController;

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
    
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }

        public void ForceGalaxySave()
        {
            if (!ServerInstance.Instance.IsRunning)
                return;

            try
            {
                (m_controllers.Universe.Galaxy as ServerGalaxy).SaveGalaxy(m_controllers, "user", m_controllers.Universe.Galaxy.Name.ToLower(), true);
            }
            catch (Exception ex)
            {
                mainLog.Error("Save Failed! Exception Info: " + ex.ToString());
            }
        }
    }
}