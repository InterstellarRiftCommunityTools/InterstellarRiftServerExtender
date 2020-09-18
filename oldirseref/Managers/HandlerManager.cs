/* Copyright (C) Extra-Terrestrial Technologies - All Rights Reserved
 * Unauthorized copying of this file, via any medium is prohibited not including
 * the individuals and/or companies stated below;
 * 
 * -Split Polygon 
 * 
 * Proprietary and confidential
 * Written by General Wrex <generalwrex@gmail.com>, 2014
 */


using Game.ClientServer.Packets;
using Game.Framework.Networking;
using IRSE.Managers.Handlers;
using System;
using System.Collections.Generic;
using System.Reflection;

using IRSE.ReflectionWrappers.ServerWrappers;

using Game.Universe;

namespace IRSE.Managers
{
	public class HandlerManager
	{
		#region Fields
		private Game.Server.ControllerManager m_controllerManager;
		private ChatHandler m_chatHandler;
		private NetworkHandler m_networkHandler;
		private PlayerHandler m_playerHandler;
		private Assembly m_serverAssembly;
		private object m_server;
		#endregion

		#region Properties
		public Object Server { get { return m_server; } set { m_server = value; } }
		public ChatHandler ChatHandler { get { return m_chatHandler; } }
		public NetworkHandler NetworkHandler { get { return m_networkHandler; } }
		public PlayerHandler PlayerHandler { get { return m_playerHandler; } }
		#endregion

	
		public HandlerManager(Assembly Assembly)
		{
			try
			{
				m_serverAssembly = Assembly;
				//FieldInfo m_activeStates = Assembly.GetType("Game.GameStates.GameState").GetField("m_ActiveStates");



				//Object o = m_activeStates.GetValue(null);
				//List<object> collection = new List<object>((IEnumerable<object>)o);

				//foreach (object server in collection)
				//{
	
				//}
			}
			catch (Exception ex)
			{
				LogManager.ErrorLog.WriteLineAndConsole(ex.ToString());		
			}
		}

		public Object GetHandlers()
		{
			try
			{
				PropertyInfo m_activeState = m_serverAssembly.GetType("Game.GameStates.GameState").GetProperty("ActiveState");
				object server = m_activeState.GetValue(null);

				if (server == null)
					return null;

				m_server = server;

				FieldInfo m_controllerManagerField = server.GetType().GetField("m_controllers", BindingFlags.NonPublic | BindingFlags.Instance);

				m_controllerManager = m_controllerManagerField.GetValue(server) as Game.Server.ControllerManager;
				LogManager.MainLog.WriteLineAndConsole("IRSE: Loaded GameServer Instance!");
		
				LogManager.MainLog.WriteLineAndConsole("IRSE: Loading Handlers..");

				m_networkHandler = new NetworkHandler(m_controllerManager);
				m_networkHandler.SetupNetworkHandler(server);

				m_playerHandler = new PlayerHandler(m_controllerManager);
				m_playerHandler.SetupPlayerHandler(server);

				m_chatHandler = new ChatHandler(m_controllerManager);
				m_chatHandler.SetupChatMessageHandler(m_networkHandler);

				LogManager.MainLog.WriteLineAndConsole("IRSE: Handlers Loaded!");

				return server;
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine("Failed to get handlers [Argument Exception] \r\n Exception: " + ex.ToString());
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to get handlers \r\n Exception: " + ex.ToString());
				return null;
			}
		}

		/*
		public void ForceGalaxySave()
		{
			if (!ServerInstance.Instance.IsRunning)
				return;

			try
			{
				LogManager.MainLog.WriteLineAndConsole("Attempting to Force a save!!!!");

				//m_controllerManager.Universe.Galaxy.SaveGalaxy(m_controllerManager);

				LogManager.MainLog.WriteLineAndConsole("Saved Galaxy!");
			}
			catch (Exception ex)
			{
				LogManager.ErrorLog.WriteLineAndConsole("Invoking Save Failed!!!! Wrex probally fucked something up! Exception Info: " + ex.ToString());
			}
		

		}
		*/
	}
}
