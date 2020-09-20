using Game.ClientServer.Packets;
using Game.Framework.Networking;
using IRSE.Managers.Handlers;
using System;
using System.Collections.Generic;
using System.Reflection;

using IRSE.ReflectionWrappers.ServerWrappers;

using Game.Universe;
using NLog;

namespace IRSE.Managers
{
	public class HandlerManager
	{
        #region Fields
        private static Logger mainLog; //mainLog.Error
        private Game.Server.ControllerManager m_controllerManager;
		private ChatHandler m_chatHandler;
		private NetworkHandler m_networkHandler;
		private PlayerHandler m_playerHandler;
		private Assembly m_serverAssembly;
		private object m_server;
		private Type m_gameStateType;
		#endregion

		#region Properties
		public Object Server { get { return m_server; } set { m_server = value; } }
		public ChatHandler ChatHandler { get { return m_chatHandler; } }
		public NetworkHandler NetworkHandler { get { return m_networkHandler; } }
		public PlayerHandler PlayerHandler { get { return m_playerHandler; } }
		#endregion

	
		public HandlerManager(Assembly Assembly)
		{
            mainLog = NLog.LogManager.GetCurrentClassLogger();


            try
			{
				m_serverAssembly = Assembly;
			}
			catch (Exception ex)
			{
				mainLog.Error(ex);		
			}
		}

		public Object GetHandlers()
		{
			try
			{
				m_gameStateType = m_serverAssembly.GetType("Game.GameStates.GameState");


				PropertyInfo m_activeState = m_gameStateType.GetProperty("ActiveState");
				object server = m_activeState.GetValue(null);

				if (server == null)
					return null;

				m_server = server;

				FieldInfo m_controllerManagerField = server.GetType().GetField("m_controllers", BindingFlags.NonPublic | BindingFlags.Instance);

				m_controllerManager = m_controllerManagerField.GetValue(server) as Game.Server.ControllerManager;
				mainLog.Info("IRSE: Loaded GameServer Instance!");

                mainLog.Info("IRSE: Loading Handlers..");

				m_networkHandler = new NetworkHandler(m_controllerManager);
				m_networkHandler.SetupNetworkHandler(server);

				m_playerHandler = new PlayerHandler(m_controllerManager);
				m_playerHandler.SetupPlayerHandler(server);

				m_chatHandler = new ChatHandler(m_controllerManager);
				m_chatHandler.SetupChatMessageHandler(m_networkHandler);

                mainLog.Info("IRSE: Handlers Loaded!");

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


		internal void ClearGameState()
		{



		}


		public void ForceGalaxySave()
		{
			if (!ServerInstance.Instance.IsRunning)
				return;

			try
			{
                mainLog.Info("Attempting to Force a save!!!!");

				(m_controllerManager.Universe.Galaxy as ServerGalaxy).SaveGalaxy(m_controllerManager, "user", m_controllerManager.Universe.Galaxy.Name.ToLower(), true);

                mainLog.Info("Saved Galaxy!");
			}
			catch (Exception ex)
			{
				mainLog.Error("Save Failed! Exception Info: " + ex.ToString());
			}
		

		}
		
	}
}
