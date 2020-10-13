using IRSE.Managers;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace IRSE.ReflectionWrappers.ServerWrappers
{
    public class IR : ReflectionClassWrapper
    {
        #region Fields

        private static NLog.Logger mainLog;

        private const String EntryClass = "Program";
        private const String InitMethod = "Init";
        private const String StopMethod = "Stop";

        private Assembly assembly;

        private ReflectionField m_startupArgsField;
        private ReflectionMethod m_startupMethod;
        private ReflectionMethod m_stopMethod;

        private ManualResetEvent m_waitEvent;
        private Thread serverThread;

        #endregion Fields



        #region Properties

        public override String ClassName { get { return "Program"; } }
        public override String AssemblyName { get { return "InterstellarRift"; } }

        #endregion Properties

        #region Methods

        public IR(Assembly Assembly, Assembly frameworkAssembly, String Namespace)
            : base(Assembly, Namespace, EntryClass)
        {
            assembly = frameworkAssembly;
            mainLog = NLog.LogManager.GetCurrentClassLogger();
            SetupReflection(Assembly);
            m_waitEvent = new ManualResetEvent(false);
        }

        private void SetupReflection(Assembly Assembly)
        {
            try
            {
                m_startupArgsField = new ReflectionField("CommandLineArgs", ClassName, m_classType);
                m_startupMethod = new ReflectionMethod(InitMethod, ClassName, m_classType);
                m_stopMethod = new ReflectionMethod(StopMethod, ClassName, m_classType);
            }
            catch (ArgumentException ex)
            {
                mainLog.Error(ex.ToString());
            }
        }

        public void StopServer()
        {
            try
            {
                m_stopMethod.Call(null, null);
            }
            catch (Exception)
            {
            }
        }

        public Thread StartServer(Object args)
        {

            if (serverThread == null) {

                mainLog.Info(string.Format(Program.Localization.Sentences["LaunchingServer"]));
                serverThread = new Thread(new ParameterizedThreadStart(this.ThreadStart));

                serverThread.IsBackground = false;
                serverThread.CurrentCulture = CultureInfo.InvariantCulture;
                serverThread.CurrentUICulture = CultureInfo.InvariantCulture;
                serverThread.Start(args);
            }

            return serverThread;
        }
        [STAThread]
        private void ThreadStart(Object args)
        {
            try
            {
                ServerWrapper.Program.Start(args as Object[]);
            }
            catch (Exception ex)
            {
                
                mainLog.Fatal(ex, string.Format(Program.Localization.Sentences["NoInitWrapper"]));
            }
        }

        private void Start(Object[] args)
        {
            try
            {
                m_startupArgsField.SetValue(null, args as String[]);
                m_startupMethod.Call(null, null);

                mainLog.Info(string.Format(Program.Localization.Sentences["WaitingForServer"]));

                ServerInstance.Instance.SetIsStarting();

                // just making sure 
                object gameServer = assembly.GetType("Game.GameStates.GameState").GetProperty("ActiveState").GetValue(null);

                while (gameServer == null)
                {
                    Thread.Sleep(1000);
                    if (gameServer != null)
                    {
                        break;
                    }
                }

                

                ServerInstance.Instance.Hook();
            }
            catch (Exception ex)
            {
                
                mainLog.Fatal(ex, string.Format(Program.Localization.Sentences["GameBaseCode"]));

            }
 
        }

        #endregion Methods
    }
}