using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellionExtendedServer.ServerWrappers.Rewrite.ReflectionWrappers
{
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.ExceptionServices;
    using System.Security;
    using System.Threading;
    using ZeroGravity;
    using ZeroGravity.Objects;

    public class HellionServer :  ReflectionClassWrapper
    {
        #region Fields
        private const String Class = "Server";

        private static HellionServer m_instance;
        private static Server m_internalServer;

        private Boolean m_isRunning;
        private ManualResetEvent m_waitEvent;
        #endregion Fields

        #region Events
        public delegate void ServerRunningEvent();
        public event ServerRunningEvent OnServerStarted;
        public event ServerRunningEvent OnServerStopped;
        #endregion


        #region Properties
        public override String ClassName => "Server";

        #endregion Properties

        public Boolean IsRunning { get { return isRunning; } }
        private Boolean isRunning
        {
            get => m_isRunning;
            set
            {
                if (m_isRunning == value)
                    return;

                m_isRunning = value;
                if (m_isRunning)
                    OnServerStarted?.Invoke();
                else
                    OnServerStopped?.Invoke();
            }
        }

        public static HellionServer Instance
        {
            get
            {
                return m_instance;
            }
        }

        #region Internal Properties

        public static Type InternalType
        {
            get
            {
                return typeof(Server);
            }
        }    
        #endregion Internal Properties




       

        public HellionServer(Assembly Assembly, String Namespace )
			: base(Assembly, Namespace)
		{
            SetupReflection();
            m_waitEvent = new ManualResetEvent(false);
        }

        public bool ReflectionTest()
        {
            try
            {
                if (InternalType == null)
                {
                    throw new Exception($"Could not find internal type for '{ClassName}");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetupReflection()
        {
            try
            {
                if (InternalType == null)
                {
                    throw new Exception($"Could not find internal type for '{ClassName}");
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Internal Methods

        public Server GetInternalServer()
        {
            if (m_internalServer == null)
            {
                m_internalServer = new Server();
            }

            return m_internalServer;
        }

        private void SetIsRunning()
        {
            isRunning = Server.IsRunning;
        }

        #endregion Internal Methods


        public Thread StartServer(string[] args)
        {

            Thread serverThread = new Thread(new ParameterizedThreadStart(this.ThreadStart))
            {
                IsBackground = true,
                CurrentCulture = CultureInfo.InvariantCulture,
                CurrentUICulture = CultureInfo.InvariantCulture
            };
            serverThread.Start(args);

            return serverThread;
        }

        private void ThreadStart(Object args)
        {
            isRunning = false;

            try
            {

            }
            catch (Exception ex)
            {

            }
            isRunning = false;
        }

    }
}
