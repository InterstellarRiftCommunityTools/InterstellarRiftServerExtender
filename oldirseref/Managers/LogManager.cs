/* Copyright (C) Extra-Terrestrial Technologies - All Rights Reserved
 * Unauthorized copying of this file, via any medium is prohibited not including
 * the individuals and/or companies stated below;
 * 
 * -Split Polygon 
 * 
 * Proprietary and confidential
 * Written by General Wrex <generalwrex@gmail.com>, 2014
 */


using System;
using System.IO;
using System.Text;
using System.Threading;

namespace IRSE.Managers
{
	internal class LogManager
	{
		#region Fields
		private const String _ERROR_LOG_NAME = "IRSE_Error.log";
		private const String _MAIN_LOG_NAME = "IRSE.log";
		private const String _CHAT_LOG_NAME = "IRSE_Chat.log";

		private String m_logDirectory;
		private TextWriter m_consoleRedirect;
		private TextWriter m_originalConsole;
		#endregion

		#region Properties
		public String LogDirectory { get { return m_logDirectory; } set { m_logDirectory = value; } }

		static public LogInstance ErrorLog { get; set; }
		static public LogInstance MainLog { get; set; }
		static public LogInstance ChatLog { get; set; }

		#endregion

		#region Methods
		public LogManager(String logDirectory)
		{
			m_logDirectory = logDirectory;
			ErrorLog = new LogInstance(m_logDirectory, _ERROR_LOG_NAME);
			MainLog = new LogInstance(m_logDirectory, _MAIN_LOG_NAME);
			ChatLog = new LogInstance(m_logDirectory, _CHAT_LOG_NAME);
		}

		public LogManager(String logDirectory, bool redirectConsole)			
		{
			m_logDirectory = logDirectory;
			ErrorLog = new LogInstance(m_logDirectory, _ERROR_LOG_NAME);
			MainLog = new LogInstance(m_logDirectory, _MAIN_LOG_NAME);
			ChatLog = new LogInstance(m_logDirectory, _CHAT_LOG_NAME);
		}

		#endregion
	}

	public class LogInstance
	{
		#region Fields
		private String m_logFile;
		private StringBuilder m_stringBuilder;
		private String m_logDirectory;
		private String m_logName;
		private Boolean m_initialized;

		private static readonly object _logLock = new object();
		#endregion

		#region Properties
		#endregion

		#region Methods
		public LogInstance(String logDirectory, String logName)
		{
			m_logDirectory = logDirectory;
			m_logName = logName;
			m_initialized = false;
		}

		private void Init()
		{
			if (!Directory.Exists(m_logDirectory))
			{
				try
				{
					Directory.CreateDirectory(m_logDirectory);
				}
				catch (Exception ex)
				{
					LogManager.ErrorLog.WriteLineAndConsole("Failed to create log directory " + m_logDirectory + " - " + ex.Message);
					throw;
				}
			}
			m_stringBuilder = new StringBuilder();
			m_logFile = Path.Combine(m_logDirectory, m_logName);
			if (File.Exists(m_logFile))
			{
				FileInfo oldLog = new FileInfo(m_logFile);
				String oldLogName = Path.Combine(m_logDirectory, Path.GetFileNameWithoutExtension(oldLog.FullName));

				DateTime logCreated = oldLog.LastWriteTime;

				oldLogName += logCreated.ToString("_yyyy_MMM_dd_HHmm_ss");
				oldLogName += ".log";

				File.Move(oldLog.FullName, oldLogName);
			}
			m_initialized = true;
			WriteLine(Timestamp() + " - Thread: " + ThreadInfo() + " -> " + "Log File Opened.");
		}

		public void WriteLine(String message)
		{
			if (!m_initialized)
			{
				Init();
			}

			if (m_logFile != null)
			{
				try
				{
					TextWriter m_Writer = new StreamWriter(m_logFile, true);
					TextWriter.Synchronized(m_Writer).WriteLine(message);
					m_Writer.Close();

				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed to write to log: " + ex.Message);
				}
			}
		}

		public void WriteLineAndConsole(String message)
		{

			String timestamp = Timestamp();
			String thread = ThreadInfo();
			Console.WriteLine(timestamp + ": " + message);
			WriteLine(timestamp + " - Thread: " + thread + " -> " + message);
		}

		private String Timestamp()
		{
			DateTimeOffset now = DateTimeOffset.Now;
			return now.ToString("yyyy-MM-dd HH.mm:ss.fff");
		}

		private String ThreadInfo()
		{
			return Thread.CurrentThread.ManagedThreadId.ToString();
		}
		#endregion
	}
}
