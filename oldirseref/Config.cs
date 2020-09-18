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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
[assembly: System.Reflection.AssemblyFileVersion("0.0.4.5")]

namespace IRSE
{
	[XmlRoot("IRSEConfig")]
	public class ConfigFile
	{
		public string MainEndpointName { get; set; }
		public string BindIP { get; set; }
		public int Port { get; set; }
		public string OnJoinMotd { get; set; }

		[NonSerialized]
		public List<ulong> m_admins;

		public List<ulong> GameAdminsSteamID
		{ 
			get 
			{
				if (m_admins == null)
					m_admins = new List<ulong>();

				return m_admins;
			}
			set { value = m_admins; }
		}
	}

	public static class Config
	{
		private static ConfigFile m_config;

		public static string ConfigXMLPath 
		{ 
			get 
			{ 
				return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\IRSEConfig.xml"; 
			}
		}

		public static ConfigFile Settings
		{
			get
			{
				if (m_config == null)
					m_config = Load();

				return m_config;
			}
			set { m_config = value; Save(); }
		}

		public static ConfigFile Load()
		{
			try
			{
				if (m_config == null)
					m_config = new ConfigFile();

				if (File.Exists(ConfigXMLPath))
				{
					Console.WriteLine("Config file exists");
					XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
					using (TextReader textReader = new StreamReader(ConfigXMLPath))
					{
						m_config = (ConfigFile)serializer.Deserialize(textReader);		
					}

					m_config.m_admins = m_config.GameAdminsSteamID;

					return m_config;
				}
				else
				{
					Console.WriteLine("Config file does not exist, Creating new");
					m_config.OnJoinMotd = "Welcome to Ettech.nets Interstellar Rift Server #name#!";
					m_config.MainEndpointName = "IRSE";
					m_config.BindIP = "localhost";
					m_config.Port = 9000;
					m_config.GameAdminsSteamID.Add(76561197997290742);
					m_config.GameAdminsSteamID.Add(76561198014435136);
				    m_config.GameAdminsSteamID.Add(76561198035278894);
					m_config.GameAdminsSteamID.Add(76561197977644095);
				    m_config.GameAdminsSteamID.Add(76561198141973062);

					Save();
				}
				return m_config;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return (ConfigFile)null;
			}
		}

		public static void Save()
		{
			try
			{
				Console.WriteLine("Saving config file...");
				XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
				using (TextWriter textWriter = new StreamWriter(ConfigXMLPath))
				{
					serializer.Serialize(textWriter, m_config);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Console.WriteLine(ex.StackTrace);
			}
		}
	}
}
