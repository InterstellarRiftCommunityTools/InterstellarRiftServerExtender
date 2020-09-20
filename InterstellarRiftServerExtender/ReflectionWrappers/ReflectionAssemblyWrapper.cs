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
