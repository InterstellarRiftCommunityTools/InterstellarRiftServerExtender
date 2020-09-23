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
using System.Linq;

namespace IRSE.Managers.Handlers
{
	public class UniverseHandler
	{
		#region Fields
		private Game.Server.UniverseController m_universeController;
        private static NLog.Logger mainLog; //mainLog.Error

        #endregion

        public Game.Server.UniverseController Universe
		{
			get
			{
				return m_universeController;
			}
		}

		public UniverseHandler(Game.Server.ControllerManager controllerManager)
		{

			//var test = controllerManager.Universe as Game.Server.UniverseController;


			//Console.WriteLine(test.ActiveSystems.FirstOrDefault());

			try
			{
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
