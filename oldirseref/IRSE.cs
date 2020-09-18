/* Copyright (C) Extra-Terrestrial Technologies - All Rights Reserved
 * Unauthorized copying of this file, via any medium is prohibited not including
 * the individuals and/or companies stated below;
 * 
 * -Split Polygon 
 * 
 * Proprietary and confidential
 * Written by General Wrex <generalwrex@gmail.com>, 2014
 */

using IRSE.Managers;
using IRSE.ReflectionWrappers.ServerWrappers;
using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

using System.Security.Principal;

namespace IRSE
{
	public class IRSE
	{

		private static LogManager m_logManager;

		private static ServerInstance m_serverInstance;

		private static IRSE m_instance;

		private static IRSEForm m_irseForm;

		public static IRSE Instance { get { return m_instance; } }

		public IRSEForm Form { get { return m_irseForm; } }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		public static void Main(string[] args)
		{

			Thread uiThread = new Thread(LoadGUI);
			uiThread.SetApartmentState(ApartmentState.STA);
			uiThread.Start();

			Console.Title = "IRSE ( Interstellar Rift Server Extender ) ";

			IRSE program = new IRSE(args);

			program.Run(args);

			var test = Console.Out;

			

		}

		public IRSE(string[] args)
		{
			

			m_instance = this;
			m_logManager = new LogManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));
			LogManager.MainLog.WriteLineAndConsole("Initiating IRSE Startup Procedure");

			

			LogManager.MainLog.WriteLineAndConsole("LogManager loaded successfully");


			// Init an instance of IRSE
			m_serverInstance = new ServerInstance();
			
		}

		

		private void Run(string[] args)
		{

			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identity);


			if (args.Length < 1)
			{
				Console.WriteLine("You have to set the ip and port on the command line!");
				Console.WriteLine("Press any key to quit.");
				Console.ReadLine();
				return;
			}

			// Start IRSE
			ServerInstance.Instance.Start();

			ServerHost.StartHttpServer(args[0], int.Parse(args[1]));

			ReadConsoleCommands();
		
		}

		public void ReadConsoleCommands()
		{
			string cmd = Console.ReadLine();

			if (!cmd.StartsWith("/"))
			{
				ServerInstance.Instance.Handlers.ChatHandler.SendMessageFromServer("Server: " + cmd);
			}
			else
			{
				Match say = Regex.Match(cmd, @"^(/help)");
				if (say.Success)
				{
					try
					{
						LogManager.ChatLog.WriteLineAndConsole("Current commands are;");
						LogManager.ChatLog.WriteLineAndConsole("/help - this page ;)");
					}
					catch (ArgumentException)
					{
						Console.WriteLine("Missing message to send!");
					}
				}
			}

			ReadConsoleCommands();
		}


		[STAThread]
		static void LoadGUI()
		{
			//Application.EnableVisualStyles();
			//Application.SetCompatibleTextRenderingDefault(false);
			//if (m_irseForm == null || m_irseForm.IsDisposed)
			//	m_irseForm = new IRSEForm();
			//else if (m_irseForm.Visible)
			//	return;

			//Application.Run(m_irseForm);
		
		}

	}
}
