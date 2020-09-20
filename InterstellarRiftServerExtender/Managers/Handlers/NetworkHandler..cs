using System;
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
	public class NetworkHandler
	{
		#region Fields
		private Game.Server.NetworkController m_networkController;
        private static NLog.Logger mainLog; //mainLog.Error

        #endregion

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
                NLog.LogManager.GetCurrentClassLogger().Info("IRSE: Loading NetworkHandler...");
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
