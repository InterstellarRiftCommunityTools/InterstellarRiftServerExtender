using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HellionExtendedServer.ReflectionWrappers
{
    using ZeroGravity;
    public class HellionProgram : ReflectionClassWrapper
    {
        #region Fields

        private const String Class = "Program";
        
        private ReflectionMethod m_startupMethod;

        private Boolean m_isRunning;
        private ManualResetEvent m_waitEvent;
        #endregion Fields

        #region Events
        public delegate void ServerRunningEvent();
        public event ServerRunningEvent OnServerStarted;
        public event ServerRunningEvent OnServerStopped;
        #endregion

        public override String ClassName => "Program";
        public override String AssemblyName => "Hellion_Dedicated";


        public static Type InternalType
        {
            get
            {
                return typeof(Program);
            }
        }


        public HellionProgram(Assembly Assembly, String Namespace, String Class)
			: base(Assembly, Namespace, Class)
		{
            SetupReflection();


        }


        public void SetupReflection()
        {
            try
            {
                if (InternalType == null)
                {
                    throw new Exception($"Could not find internal type for '{ClassName}");
                }
            }
            catch (Exception)
            {

            }
        }

        public void LoadAssembly()
        {

        }
    }
}
