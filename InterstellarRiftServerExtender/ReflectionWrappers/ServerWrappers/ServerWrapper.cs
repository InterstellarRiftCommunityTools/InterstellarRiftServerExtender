using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace IRSE.ReflectionWrappers.ServerWrappers
{

	public class ServerWrapper : ReflectionAssemblyWrapper
	{
		#region Fields
		private const string ServerNamespace = "Game";

		private static IR m_program;

        private static NLog.Logger mainLog; //mainLog.Error

        #endregion

        #region Properties
        public static IR Program { get { return m_program; } }
		#endregion

		#region Methods
		public ServerWrapper(Assembly serverAssembly)
			: base(serverAssembly)
		{
            mainLog = NLog.LogManager.GetCurrentClassLogger();
            m_program = new IR(serverAssembly, ServerNamespace);
		}

		internal void Init()
		{
			m_program.Init();
		}
		#endregion
	}

}
