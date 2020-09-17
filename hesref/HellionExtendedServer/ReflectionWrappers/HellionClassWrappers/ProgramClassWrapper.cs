using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HellionExtendedServer.ReflectionWrappers.HellionClassWrappers
{

    public class ProgramClassWrapper : ReflectionAssemblyWrapper
    {

        private const string DedicatedServerNamespace = "";

        private static HellionProgram m_program;


        public static HellionProgram HellionProgram => m_program;


        public ProgramClassWrapper(Assembly hellionDedicatedAssembly) 
            : base(hellionDedicatedAssembly)
        {

           //m_program = new HellionProgram(hellionDedicatedAssembly, DedicatedServerNamespace);
        }

        internal void Init()
        {
            m_program.Init();
        }
    }
}
