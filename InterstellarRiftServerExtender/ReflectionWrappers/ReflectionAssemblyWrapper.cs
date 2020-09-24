using System.Reflection;

namespace IRSE.ReflectionWrappers
{
    public class ReflectionAssemblyWrapper
    {
        #region Fields

        protected static Assembly m_assembly;
        protected static Assembly m_frameworkAssembly;

        #endregion Fields

        #region Properties

        public static Assembly Assembly { get { return m_assembly; } }
        public static Assembly FrameworkAssembly { get { return m_frameworkAssembly; } }

        #endregion Properties

        #region Methods

        public ReflectionAssemblyWrapper(Assembly assembly, Assembly frameworkAssembly)
        {
            m_assembly = assembly;
            m_frameworkAssembly = frameworkAssembly;
        }

        #endregion Methods
    }
}