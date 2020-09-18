/* Copyright (C) Extra-Terrestrial Technologies - All Rights Reserved
 * Unauthorized copying of this file, via any medium is prohibited not including
 * the individuals and/or companies stated below;
 * 
 * -Split Polygon 
 * 
 * Proprietary and confidential
 * Written by General Wrex <generalwrex@gmail.com>, 2014
 */

using IRSE.ReflectionWrappers.ServerWrappers;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;

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

			m_assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IR.exe"));

			m_serverWrapper = new ServerWrapper(m_assembly);


			ServerWrapper.Program.OnServerStarted += Program_OnServerStarted;
			ServerWrapper.Program.OnServerStopped += Program_OnServerStopped;
		}

		#region Methods
		public void Program_OnServerStarted()
		{
			m_launchedTime = DateTime.Now;
			IsRunning = true;

			try
			{
				m_controllerManager = new HandlerManager(m_assembly);


				while (m_controllerManager.GetHandlers() == null) 
				{
					Thread.Sleep(1000);
					if (m_controllerManager.GetHandlers() != null)
					{
						break;
					}
				}

				LogManager.MainLog.WriteLineAndConsole("IRSE: Startup Procedure Complete!");
			}
			catch (Exception ex)
			{
				LogManager.MainLog.WriteLineAndConsole("IRSE: Startup Procedure FAILED!");
				LogManager.MainLog.WriteLineAndConsole("IRSE: Haulting Server.. TELL WREX!!!!!!!! - Exception:");
				LogManager.ErrorLog.WriteLineAndConsole(ex.ToString());

				Console.ReadLine();
				Stop();
			}


		}

		public void Program_OnServerStopped()
		{
			IsRunning = false;
		}

		public void Start()
		{
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
			m_serverThread.Join(60000);
			m_serverThread.Abort();
		}
		#endregion
	}
}