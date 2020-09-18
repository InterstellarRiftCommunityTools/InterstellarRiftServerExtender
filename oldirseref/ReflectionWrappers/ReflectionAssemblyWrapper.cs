/* Copyright (C) Extra-Terrestrial Technologies - All Rights Reserved
 * Unauthorized copying of this file, via any medium is prohibited not including
 * the individuals and/or companies stated below;
 * 
 * -Split Polygon 
 * 
 * Proprietary and confidential
 * Written by General Wrex <generalwrex@gmail.com>, 2014
 */


using System.Reflection;

namespace IRSE.ReflectionWrappers
{
	public class ReflectionAssemblyWrapper
	{
		#region Fields
		protected static Assembly m_assembly;
		#endregion

		#region Properties
		public static Assembly Assembly { get { return m_assembly; } }
		#endregion

		#region Methods

		public ReflectionAssemblyWrapper(Assembly assembly)
		{
			m_assembly = assembly;
		}
		#endregion
	}
}
