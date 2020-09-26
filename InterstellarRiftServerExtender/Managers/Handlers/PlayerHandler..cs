using Game.Server;
using System;
using System.Collections.Generic;

using IRSE.ResultObjects;
using Game.ClientServer.Packets;

namespace IRSE.Managers.Handlers
{
    public class PlayerHandler
    {
        #region Fields

        private PlayerController m_playerController;
        private static NLog.Logger mainLog; //mainLog.Error

        #endregion Fields

        public PlayerController Players
        {
            get
            {
                return m_playerController;
            }
        }

        public IEnumerable<Player> GetPlayers
        {
            get { return m_playerController.AllPlayers(); }
        }

        public PlayerHandler(Game.Server.ControllerManager controllerManager)
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();

            try
            {
                mainLog.Info("IRSE: Loading PlayerHandler...");
                m_playerController = controllerManager.Players;
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }

        public void SetupPlayerHandler(Object server)
        {
            try
            {

            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }


    }
}