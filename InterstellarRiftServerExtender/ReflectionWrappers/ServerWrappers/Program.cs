﻿using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using IRSE.Managers;

namespace IRSE.ReflectionWrappers.ServerWrappers
{
	public class IR : ReflectionClassWrapper
	{
        #region Fields
        private static NLog.Logger mainLog; //mainLog.Error

        private const String Class = "Program";
		private const String InitMethod = "Init";
		private const String StopMethod = "Stop";

		private ReflectionField m_startupArgsField;
		private ReflectionMethod m_startupMethod;
		private ReflectionMethod m_stopMethod;

		private Boolean m_isRunning;
		private ManualResetEvent m_waitEvent;
		#endregion

		#region Events
		public delegate void ServerRunningEvent();
		public event ServerRunningEvent OnServerStarted;
		public event ServerRunningEvent OnServerStopped;
		#endregion

		#region Properties
		public override String ClassName { get { return "Program"; } }
		public override String AssemblyName { get { return "IR"; } }
		private Boolean isRunning
		{
			get { return m_isRunning; }
			set
			{
				if (m_isRunning == value)
				{
					return;
				}
				m_isRunning = value;
				if (m_isRunning)
				{
					if (OnServerStarted != null)
					{
						OnServerStarted();
					}
				}
				else
				{
					if (OnServerStopped != null)
					{
						OnServerStopped();
					}
				}
			}
		}
		public Boolean IsRunning { get { return isRunning; } }
		#endregion

		#region Methods
		public IR(Assembly Assembly, String Namespace)
			: base(Assembly, Namespace, Class)
		{
            mainLog = NLog.LogManager.GetCurrentClassLogger();
            SetupReflection(Assembly);
			m_waitEvent = new ManualResetEvent(false);
		}

		private void SetupReflection(Assembly Assembly)
		{
			try
			{
				m_startupArgsField = new ReflectionField("CommandLineArgs", ClassName, m_classType);
				m_startupMethod = new ReflectionMethod(InitMethod, ClassName, m_classType);
				m_stopMethod = new ReflectionMethod(StopMethod, ClassName, m_classType);
				
			}
			catch (ArgumentException ex)
			{
				mainLog.Error(ex.ToString());
			}
		}

		public void StopServer()
		{
			m_stopMethod.Call(null, null);
			isRunning = false;
		}

		public Thread StartServer(Object args)
		{
			mainLog.Info("IRSE: Loading server.");

			Thread serverThread = new Thread(new ParameterizedThreadStart(this.ThreadStart));

			serverThread.IsBackground = true;
			serverThread.CurrentCulture = CultureInfo.InvariantCulture;
			serverThread.CurrentUICulture = CultureInfo.InvariantCulture;
			serverThread.Start(args);

			//Thread.Sleep(10000);

			mainLog.Info("IRSE: Waiting for server....");

			isRunning = true;

			return serverThread;
		}

		private void ThreadStart(Object args)
		{
			try
			{
				ServerWrapper.Program.Start(args as Object[]);
			}
			catch (Exception ex)
			{

				mainLog.Error("Unhandled Exception caused server to crash. Exception: " + ex.ToString());
			}
			isRunning = false;
		}

		private void Start(Object[] args)
		{
			m_startupArgsField.SetValue(null, args as String[]);
			m_startupMethod.Call(null, null);
		}
		#endregion
	}
}