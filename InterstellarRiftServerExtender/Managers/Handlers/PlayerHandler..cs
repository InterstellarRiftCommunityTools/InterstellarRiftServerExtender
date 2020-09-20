﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

using Game.ClientServer.Packets;
using Game.Configuration;
using Game.Framework.Networking;
using Game.Server;

using System.Text.RegularExpressions;

using System.Xml.Serialization;

using System.Timers;

using Game.ClientServer;
using Game.ClientServer.Classes;

using IRSE;

namespace IRSE.Managers.Handlers
{

	public class PlayerHandler
	{
		#region Fields
		private PlayerController m_playerController;
        private static NLog.Logger mainLog; //mainLog.Error

        #endregion

        public PlayerHandler()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();
        }

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
				
				//m_playerController.OnAddPlayer += m_playerController_OnAddPlayer;
				//m_playerController.OnRemovePlayer += m_playerController_OnRemovePlayer;

			}
			catch (Exception ex)
			{
				mainLog.Error(ex.ToString());
			}
			
		}


		//void m_playerController_OnRemovePlayer(Player obj)
		//{
		//	ServerInstance.Instance.Controllers.ChatManager.ChatMessages.Add(new ChatMessage("", "/all " + obj.Name + " has left the server."));
		//}

		//void m_playerController_OnAddPlayer(Player obj)
		//{
		//	obj.SendRPC((object)new ServerChatMessage("~-C" + (object)Game.Configuration.Config.Singleton.NotificationChatColor + " " + Config.Settings.OnJoinMotd.Replace("#name#", obj.Name), ""));
		//
		//	ServerInstance.Instance.Controllers.ChatManager.ChatMessages.Add(new ChatMessage("", "/all " + obj.Name + " has joined the server."));
		//}
			
	}
}