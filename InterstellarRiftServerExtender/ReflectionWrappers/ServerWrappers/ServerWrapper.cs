using System.Reflection;

namespace IRSE.ReflectionWrappers.ServerWrappers
{
    public class ServerWrapper : ReflectionAssemblyWrapper
    {
        #region Fields

        private const string ServerNamespace = "Game";

        private static IR m_program;

        private static NLog.Logger mainLog; //mainLog.Error

        #endregion Fields

        #region Properties

        public static IR Program { get { return m_program; } }

        #endregion Properties

        #region Methods

        public ServerWrapper(Assembly serverAssembly, Assembly frameworkAssembly)
            : base(serverAssembly, frameworkAssembly)
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();
            m_program = new IR(serverAssembly, frameworkAssembly, ServerNamespace);
        }

        internal void Init()
        {
            m_program.Init();
        }

        #endregion Methods
    }
}