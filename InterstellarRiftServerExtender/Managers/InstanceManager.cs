using IRSE.ReflectionWrappers.ServerWrappers;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;

using IRSE.API;

namespace IRSE.Managers
{
	public class ServerInstance
	{
		#region Fields
		private static Thread m_serverThread;
		private Assembly m_assembly;
		private static ServerInstance m_serverInstance;
		private ServerWrapper m_serverWrapper;
		private HandlerManager m_controllerManager;
		private DateTime m_launchedTime;
        private static NLog.Logger mainLog; //mainLog.Error

        #endregion

        #region Properties
        public HandlerManager Handlers { get { return m_controllerManager; } }
		
		public static ServerInstance Instance { get { return m_serverInstance; } }

		public Assembly Assembly { get { return m_assembly; } }

		public Boolean IsRunning { get; set; }
		
		public TimeSpan Uptime { get { return DateTime.Now - m_launchedTime; } }

		

		#endregion

		public ServerInstance()
		{
			m_launchedTime = DateTime.MinValue;
			m_serverThread = null;
			m_serverInstance = this;


			//AssemblyName an = AssemblyName.GetAssemblyName(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IR.exe"));

			m_assembly = Assembly.UnsafeLoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IR.exe")); 

			
			m_serverWrapper = new ServerWrapper(m_assembly);

            mainLog = NLog.LogManager.GetCurrentClassLogger();


            ServerWrapper.Program.OnServerStarted += Program_OnServerStarted;
			ServerWrapper.Program.OnServerStopped += Program_OnServerStopped;
		}

		#region Methods
		public void Program_OnServerStarted()
		{

            m_launchedTime = DateTime.Now;
			
			try
			{
				//m_controllerManager = new HandlerManager(m_assembly);


				//while (m_controllerManager.GetHandlers() == null) 
				//{
					//Thread.Sleep(1000);
					//if (m_controllerManager.GetHandlers() != null)
					//{
					//	break;
					//}
				//}

				IsRunning = true;


                mainLog.Info("IRSE: Startup Procedure Complete!");
			}
			catch (Exception ex)
			{
				mainLog.Info("IRSE: Startup Procedure FAILED!");
				mainLog.Info("IRSE: Haulting Server.. Major problem detected!!!!!!!! - Exception:");
				mainLog.Error(ex.ToString());

				Console.ReadLine();
				Stop();
			}


		}

		void Program_OnServerStopped()
		{
			IsRunning = false;
		}

		public void Start()
		{
			if (IsRunning)
				return;

			String[] serverArgs = new String[]
				{
					"-server",	
				};

			m_serverThread = ServerWrapper.Program.StartServer(serverArgs);

			m_serverWrapper.Init();
		}

        public void Stop()
		{
			ServerWrapper.Program.StopServer();
			//m_serverThread.Join(60000);
			m_serverThread.Abort();
		}

		public void ForceGalaxySave()
		{
			if (!Instance.IsRunning)
				return;

			try
			{
				mainLog.Info("Attempting to Force a save!!!!");

				mainLog.Info("Saved Galaxy!");
			}
			catch (Exception ex)
			{
				mainLog.Error("Save Failed! Exception Info: " + ex.ToString());
			}


		}

		#endregion
	}
}