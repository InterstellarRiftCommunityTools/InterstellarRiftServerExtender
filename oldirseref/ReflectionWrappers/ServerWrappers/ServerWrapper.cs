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
		#endregion

		#region Properties
		public static IR Program { get { return m_program; } }
		#endregion

		#region Methods
		public ServerWrapper(Assembly serverAssembly)
			: base(serverAssembly)
		{
			m_program = new IR(serverAssembly, ServerNamespace);
		}

		internal void Init()
		{
			m_program.Init();
		}
		#endregion
	}

}
