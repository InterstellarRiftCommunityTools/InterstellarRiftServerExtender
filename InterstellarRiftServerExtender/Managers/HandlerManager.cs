using Game.ClientServer.Packets;
using Game.Framework.Networking;
using IRSE.Managers.Handlers;
using System;
using System.Collections.Generic;
using System.Reflection;

using IRSE.ReflectionWrappers.ServerWrappers;

using Game.Universe;
using NLog;
using System.Threading;
using System.Linq;
using System.Diagnostics;

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
		private UniverseHandler m_universeHandler;
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


		public void SpawnGhostClients(Game.Server.ControllerManager controllers, object server) {

			  

			SolarSystem system1 = controllers.Universe.Galaxy.GetSystem("Vectron Syx");
			SolarSystem system2 = controllers.Universe.Galaxy.GetSystem("Sentinel Prime");
			SolarSystem system3 = controllers.Universe.Galaxy.GetSystem("Scaverion");
			SolarSystem system4 = controllers.Universe.Galaxy.GetSystem("Alpha Ventura");


			Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {controllers.Network.NetGeneric.GetPort()} -GhostSystemName {system1.Identifier}");
			Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {controllers.Network.NetGeneric.GetPort()} -GhostSystemName {system2.Identifier}");
			Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {controllers.Network.NetGeneric.GetPort()} -GhostSystemName {system3.Identifier}");
			Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {controllers.Network.NetGeneric.GetPort()} -GhostSystemName {system4.Identifier}");

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

				

				mainLog.Info("IRSE: Loaded GameServer Instance!");

				m_server = server;

				FieldInfo m_controllerManagerField = server.GetType().GetField("m_controllers", BindingFlags.NonPublic | BindingFlags.Instance);

				m_controllerManager = m_controllerManagerField.GetValue(server) as Game.Server.ControllerManager;

				mainLog.Info("Spawning Ghost Client?...");
				SpawnGhostClients(m_controllerManager, server);


				var universe = m_controllerManager.Universe as Game.Server.UniverseController;

				var systems = universe.Galaxy.GetActiveSystemCount();
			
				mainLog.Info("IRSE: Waiting for Universe...");

				//universe.Galaxy.

				while (systems != 4)
				{
					Thread.Sleep(1000);
					if (systems == 4)
					{
						break;
					}
				}

				Thread.Sleep(30000);

				mainLog.Info("IRSE: Loaded Universe!");

				Console.WriteLine(systems);

				mainLog.Info("IRSE: Loading Handlers..");

				m_networkHandler = new NetworkHandler(m_controllerManager);
				m_networkHandler.SetupNetworkHandler(server);

				m_playerHandler = new PlayerHandler(m_controllerManager);
				m_playerHandler.SetupPlayerHandler(server);

				m_chatHandler = new ChatHandler(m_controllerManager);
				m_chatHandler.SetupChatMessageHandler(m_networkHandler);

				m_universeHandler = new UniverseHandler(m_controllerManager);
				m_universeHandler.SetupHandler(server);

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
