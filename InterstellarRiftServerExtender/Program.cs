using Game.Server;
using IRSE.GUI.Forms;
using IRSE.Managers;
using IRSE.Modules;
using IRSE.Modules.GameConfig;
using NLog;
using NLog.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using Localization = IRSE.Modules.Localization;


namespace IRSE
{
    public class Program
    {
        public static string ForGameVersion = "1.0.0.10";
        public static string ThisGameVersion;

        #region Fields

        private static Config m_config;
        private static UpdateManager updateManager;
        private static Localization m_localization;
        private static ServerInstance m_serverInstance;
        private static EventHandler _handler;

        //private static Boolean m_useGui = true;
        private static Thread uiThread;

        private static Logger mainLog;
        private static string[] CommandLineArgs;
        private static bool debugMode = true;
        private static bool handleConsoleCommands = true;
        private static ExtenderGui m_form;
        private static IEnumerator ConsoleCoroutine;

        #endregion Fields

        #region Properties

        public static bool Dev
        {
            get
            {
                if (ThisAssembly.Git.Branch.ToLower() != "master") return true;
                return false;
            }
        }

        public static Localization Localization => m_localization;
        public static Program Instance { get; private set; }
        public static ExtenderGui GUI { get; private set; }
        //public static Window MainWindow => GUI.MainWindow;
        public static Version Version => Assembly.GetEntryAssembly().GetName().Version;
        public static String VersionString => Version.ToString(4) + $" Branch: {ThisAssembly.Git.Branch}";
        public static String WindowTitle => string.Format("IsR Server Extender V{0} - Game Version: v{1} - This Game Version: v{2}", VersionString, ForGameVersion, ThisGameVersion);

        public static Config Config => m_config;

        public static bool WPFGUI { get; private set; }

        #endregion Properties

        public Program()
        {
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);

            if(!Dev)
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CrashDump.CurrentDomain_UnhandledException);

            string configPath = ExtenderGlobals.GetFilePath(IRSEFileName.NLogConfig);

            LogManager.Configuration = new XmlLoggingConfiguration(configPath);

            mainLog = LogManager.GetCurrentClassLogger();

            mainLog.Info("Interstellar Rift Dedicated Server Initializing....");

            mainLog.Info($"Git Branch: {ThisAssembly.Git.Branch}");

            if (Dev)
            {

                mainLog.Info($"Git Commit: {ThisAssembly.Git.Commit}");
                mainLog.Info($"Git SHA: {ThisAssembly.Git.Sha}");
            }

            

            m_serverInstance = new ServerInstance();

            ThisGameVersion = m_serverInstance.Assembly.GetName().Version.ToString();
            Console.Title = WindowTitle;
        }

        [STAThread]
        private static void Main(string[] args)
        {           
            Console.Title = WindowTitle;

            new FolderStructure().Build();

            m_config = new Config();
            debugMode = m_config.Settings.DebugMode;

            AppDomain.CurrentDomain.AssemblyResolve += (sender, rArgs) =>
            {
                string assemblyName = new AssemblyName(rArgs.Name).Name;
                if (assemblyName.EndsWith(".resources"))
                    return null;

                string dllName = assemblyName + ".dll";
                string dllFullPath = Path.Combine(Path.GetFullPath(FolderStructure.IRSEFolderPath), dllName);

                if (debugMode)
                    Console.WriteLine($"The assembly '{dllName}' is missing or has been updated. Adding/Updating missing assembly.");

                // get binaries in plugin

                using (Stream s = Assembly.GetCallingAssembly().GetManifestResourceStream("IRSE.Resources." + dllName))
                {
                    if (s != null) 
                    {
                        byte[] data = new byte[s.Length];
                        s.Read(data, 0, data.Length);

                        File.WriteAllBytes(dllFullPath, data);
                    }
                        
                }

                return Assembly.LoadFrom(dllFullPath);
            };

            // This is for args that should be used before IRSE loads
            bool noUpdateIRSE = false;
            bool noUpdateIR = false;
            bool usePrereleaseVersions = false;
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (string arg in args)
            {
                if (arg.Equals("-noupdateirse"))
                    noUpdateIRSE = true;

                if (arg.Equals("-noupdateir"))
                    noUpdateIR = true;

                if (arg.Equals("-usedevversion"))
                    usePrereleaseVersions = true;
            }

            if (usePrereleaseVersions || Config.Settings.EnableDevelopmentVersion)
            {
                Console.WriteLine("IRSE: (Arg: -usedevversion is set) HES Will use Pre-releases versions");
            }

            if (noUpdateIRSE || !Config.Settings.EnableAutomaticUpdates)
            {
                UpdateManager.EnableAutoUpdates = false;
                Console.WriteLine("IRSE: (Arg: -noupdate is set or option in IRSE config is enabled) HES will not be auto-updated.\r\n");
            }

            if (noUpdateIR || !Config.Settings.EnableHellionAutomaticUpdates)
            {
                SteamCMD.AutoUpdateIR = false;
                Console.WriteLine("IRSE: (Arg: -noupdateir is set) Hellion Dedicated will not be auto-updated.");
            }

            Console.ResetColor();

            //updateManager = new UpdateManager(); // REPO NEEDS TO BE PUBLIC

            Instance = new Program();
            Instance.Run(args);
        }

        // this is where stuff goes!
        private void Run(string[] args)
        {
            //Build IR's paths so we can use the Localization system
            Game.Program.InitFileSystems();
            Game.Program.InitGameDirectory("InterstellarRift");

            //new ServerConfigConverter().BuildAndUpdateConfigProperties();

            m_localization = new Localization();

            SetupGUI();

            // They initialize it as (string[])null, not good for us trying to use their static classes, this fixes it
            Game.Program.CommandLineArgs = new string[1];

            SvCommands.InitCommands(null);
            Program.ConsoleCoroutine = Game.Framework.CommandSystem.Singleton.Logic((object)null, Game.Configuration.Globals.NoConsoleAutoComplete);
            SvCommands.InitCommandHooks();



            //while (true) { }
            ReadConsoleCommands(args);          
        }

        /// <summary>
        /// The UI Thread
        /// </summary>
        private static void SetupGUI()
        {

            if (uiThread != null)
                return;

            uiThread = new Thread(LoadGUI);
            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.Start();


        }

        /// <summary>
        /// Loads the gui into its own thread
        /// </summary>
        [STAThread]
        private static void LoadGUI()
        {


            if (m_form == null || m_form.IsDisposed)
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                m_form = new ExtenderGui();
            }
            else if (m_form.Visible)
                return;



            m_form.Text = WindowTitle + " GUI";

            GUI = m_form;

            System.Windows.Forms.Application.Run(m_form);

            Console.WriteLine("Loading GUI");

        }

        internal static void Restart(bool stopServer = true)
        {
            if (ServerInstance.Instance != null)

            {
                if (ServerInstance.Instance.IsRunning && stopServer == true)
                {
                    if (ServerInstance.Instance != null)
                    {
                        ServerInstance.Instance.Stop();
                        SpinWait.SpinUntil(() => !ServerInstance.Instance.IsRunning, 2000);
                    }
                }
            }

            var thisProcess = Process.GetCurrentProcess();
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = thisProcess.ProcessName;
            startInfo.Arguments = string.Join(" ", CommandLineArgs);
            startInfo.WindowStyle = thisProcess.StartInfo.WindowStyle;

            var proc = Process.Start(startInfo);

            thisProcess.Kill();
        }

        public static void PrintHelp()
        {
            //mainLog.Warn("------------------------------------------------------------");
            //mainLog.Warn(m_localization.Sentences["DescHelp"]);
            //mainLog.Warn(m_localization.Sentences["HelpCommand"]);
            //mainLog.Warn(m_localization.Sentences["SaveCommand"]);
            //mainLog.Warn(m_localization.Sentences["StartCommand"]);
            //mainLog.Warn(m_localization.Sentences["StopCommand"]);
            //mainLog.Warn(m_localization.Sentences["OpenGUICommand"]);
            //mainLog.Warn(m_localization.Sentences["PlayersCommand"]);

            //mainLog.Warn("-------------------------------------------------------------");
        }

        /// <summary>
        /// This contains the console commands
        /// </summary>
        public void ReadConsoleCommands(string[] commandLineArgs)
        {
            while (true)
            {
                string line = Console.ReadLine();
           
                if (!string.IsNullOrEmpty(line) && line.Length > 1)
                {
                    if (!line.StartsWith("/"))
                    {
                        if (ServerInstance.Instance.IsRunning)
                            if (Instance != null)
                                ServerInstance.Instance.Handlers.ChatHandler.SendMessageFromServer(line);
                            else
                                Console.WriteLine("The Server must be running to message connected clients!");

                        continue;
                    }

                    string cmd = line.Split(" ".ToCharArray())[0].Replace("/", "");
                    string[] args = line.Split(" ".ToCharArray()).Skip(1).ToArray();

                    //if (ServerInstance.Instance.CommandManager.HandleConsoleCommand(cmmd, args)) continue;

                    string[] strArray = Regex.Split(line, "^/([a-z]+) (\\([a-zA-Z\\(\\)\\[\\]. ]+\\))|([a-zA-Z\\-]+)");
                    List<string> stringList = new List<string>();
                    int num = 1;

                    foreach (string str2 in strArray)
                    {
                        if (str2 != "" && str2 != " ")
                            stringList.Add(str2);
                        ++num;
                    }
                    bool flag = false;

                    if (stringList[1] == "help")
                    {
                        Program.PrintHelp();
                        flag = true;
                    }

                    if (stringList[1] == "checkupdate")
                    {
                        updateManager.CheckForUpdates().GetAwaiter().GetResult();
                        flag = true;
                    }

                    if (stringList[1] == "restart")
                    {
                        Restart();
                        flag = true;
                    }

                    if (stringList[1] == "forceupdate")
                    {
                        updateManager.CheckForUpdates(true).GetAwaiter().GetResult();
                        flag = true;
                    }

                    if (stringList[1] == "start")
                    {
                        if (!ServerInstance.Instance.IsRunning)
                            ServerInstance.Instance.Start();
                      
                        else
                            Console.WriteLine("The server is already running.");
                        flag = true;
                    }

                    if (stringList[1] == "stop")
                    {
                        if (ServerInstance.Instance.IsRunning)
                            ServerInstance.Instance.Stop();
                        else
                            Console.WriteLine("The server is not running");
                        flag = true;
                    }

                    if (stringList[1] == "opengui")
                    {
                        SetupGUI();
                        flag = true;
                    }

                    if (!flag)
                        Console.WriteLine("bad syntax");
                }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);

        private enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6,
        }

        private static bool Handler(CtrlType sig)
        {
            if (sig == CtrlType.CTRL_C_EVENT || sig == CtrlType.CTRL_BREAK_EVENT || (sig == CtrlType.CTRL_LOGOFF_EVENT || sig == CtrlType.CTRL_SHUTDOWN_EVENT) || sig == CtrlType.CTRL_CLOSE_EVENT)
            {
                if (ServerInstance.Instance.IsRunning)
                {
                    ServerInstance.Instance.Stop();
                    Console.WriteLine("Closing IRSE Safely...");
                }
            }
            return false;
        }
    }
}