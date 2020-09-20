﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace IRSE.Plugins
{
	public abstract class PluginBase : IPlugin
	{
		#region Fields
		protected Guid m_PluginId;
		protected String m_name;
		protected String m_version;
		protected String m_directory;
		#endregion

		#region Properties
		public virtual Guid Id { get { return m_PluginId; } }
		public virtual String Name { get { return m_name; } }
		public virtual String Version { get { return m_version; } }
		public virtual String Directory { get { return m_directory; } }
		#endregion

		#region Methods
		public PluginBase()
		{
			Assembly assembly = Assembly.GetCallingAssembly();
			GuidAttribute guidAttr = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
			m_PluginId = new Guid(guidAttr.Value);

			AssemblyName asmName = assembly.GetName();
			m_name = asmName.Name;

			m_version = asmName.Version.ToString();
		}
		public virtual void Init(String modDirectory)
		{
			m_directory = modDirectory;
		}

		public abstract void Shutdown();
		#endregion
	}
}