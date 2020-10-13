using Game.Framework;
using IRSE.GUI.Forms;
using IRSE.Managers;
using IRSE.Modules;
using NLog;
using NLog.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Localization = IRSE.Modules.Localization;
using HarmonyLib;

namespace IRSE
{
    public class Program
    {
        public static string ThisGameVersion;

        #region Fields

        public const int VK_RETURN = 0x0D;
        public const int WM_KEYDOWN = 0x100;
        public static IntPtr HWnd;

        private static Config m_config;
        private static UpdateManager updateManager;
        private static Localization m_localization;
        private static ServerInstance m_serverInstance;
        private static EventHandler _handler;

        private static Thread uiThread;

        private static NLog.Logger mainLog;
        private static string[] CommandLineArgs = new string[10];

        public static Dictionary<string, Action<string[]>> IRSEConsoleCommands = new Dictionary<string, Action<string[]>>();

        private static bool debugMode = true;
        private static ExtenderGui m_form;

        public Version CurrentGameVerson { get; }

        public static bool GUIDisabled => Environment.UserInteractive;

        public static IEnumerator ConsoleCoroutine;

        public static bool PendingServerStart;
        private static bool _useGui;

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

        public static Harmony Harmony { get; private set; }

        public static Assembly EntryAssembly => Assembly.GetEntryAssembly();
        public static Version Version => EntryAssembly.GetName().Version;

        public static string ForGameVersion => EntryAssembly.GetCustomAttributes(typeof(SupportedGameAssemblyVersion), false).Cast<SupportedGameAssemblyVersion>().First().someText;

        public static String VersionString => Version.ToString(4) + $" Branch: {ThisAssembly.Git.Branch}";
        public static String WindowTitle => string.Format("IsR Server Extender V{0}", VersionString);

        public static Config Config => m_config;

        #endregion Properties

        public Program()
        {
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);

            if (!Dev)
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CrashDump.CurrentDomain_UnhandledException);
           
            CurrentGameVerson = SteamCMD.GetGameVersion();
            Console.Title = WindowTitle;
        }

        public static void SetTitle(bool after = false)
        {
            Console.Title = Console.Title + " : " + (after ? WindowTitle.Replace("IsR", "") : WindowTitle);
        }

        [MTAThread]
        private static void Main(string[] args)
        {
            CommandLineArgs = args;

            SetTitle();

            new FolderStructure().Build();

            m_config = new Config();
            debugMode = m_config.Settings.DebugMode;

            string configPath = ExtenderGlobals.GetFilePath(IRSEFileName.NLogConfig);
            LogManager.Configuration = new XmlLoggingConfiguration(configPath);

            m_localization = new Localization();
            m_localization.Load(m_config.Settings.CurrentLanguage.ToString().Substring(0, 2));

            AppDomain.CurrentDomain.AssemblyResolve += (sender, rArgs) =>
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyName = new AssemblyName(rArgs.Name);

                var pathh = assemblyName.Name + ".dll";
                if (assemblyName.CultureInfo != null && assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture) == false)
                    pathh = String.Format(@"{0}\{1}", assemblyName.CultureInfo, pathh);

                // get binaries in plugins
                String modPath = Path.Combine(FolderStructure.IRSEFolderPath, "plugins");
                String[] subDirectories = Directory.GetDirectories(modPath);
                foreach (String subDirectory in subDirectories)
                {
                    string path = Path.Combine(Path.GetFullPath(FolderStructure.IRSEFolderPath), "plugins", subDirectory, pathh);
                    if (File.Exists(path))
                        return Assembly.LoadFrom(path);

                    // maybe a subfolder?
                    foreach (String subDirectory2 in Directory.GetDirectories(subDirectory))
                    {
                        string path2 = Path.Combine(Path.GetFullPath(FolderStructure.IRSEFolderPath), "plugins", subDirectory2, "bin", pathh);
                        if (File.Exists(path2))
                            return Assembly.LoadFrom(path2);
                    }
                }

                using (Stream stream = executingAssembly.GetManifestResourceStream(pathh))
                {
                    if (stream == null) return null;

                    var assemblyRawBytes = new byte[stream.Length];
                    stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                    //Console.WriteLine("found missing dll: " + pathh);
                    return Assembly.Load(assemblyRawBytes);
                }
            };

            mainLog = LogManager.GetCurrentClassLogger();

            Console.WriteLine($"Interstellar Rift Extended Server v{Version} Initializing....");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Repo URL: {ThisAssembly.Git.RepositoryUrl}");
            Console.WriteLine($"Git Branch: {ThisAssembly.Git.Branch}");
            if (Dev)
            {
                Console.WriteLine($"Git Commit Date: {ThisAssembly.Git.CommitDate}");
                Console.WriteLine($"Git Commit: {ThisAssembly.Git.Commit}");
                Console.WriteLine($"Git SHA: {ThisAssembly.Git.Sha}");
            }
            Console.WriteLine();

            if (!File.Exists(Path.Combine(FolderStructure.RootFolderPath, "IR.exe")))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("IRSE: IR.EXE wasn't found.");
                Console.WriteLine("Make sure IRSE.exe is in the same folder as IR.exe.");
                Console.WriteLine("Press enter to close.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            Instance = new Program();
            Instance.Run(args);
        }

        // this is where stuff goes!
        private void Run(string[] args)
        {
            //Harmony
            Harmony.DEBUG = Dev;
            Harmony = new Harmony("com.tse.irse");

            // This is for args that should be used before IRSE loads
            bool noUpdateIRSE = false;
            bool noUpdateIR = false;
            bool usePrereleaseVersions = false;
            bool autoStart = false;
            _useGui = true;
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (string arg in args)
            {
                if (arg.Equals("-noupdateirse"))
                    noUpdateIRSE = true;

                if (arg.Equals("-noupdateir"))
                    noUpdateIR = true;

                if (arg.Equals("-usedevversion"))
                    usePrereleaseVersions = true;

                if (arg.Equals("-nogui"))
                    _useGui = false;

                if (arg.Equals("-autostart"))
                    autoStart = true;
            }

            if (usePrereleaseVersions || Config.Settings.EnableDevelopmentVersion)
            {
                Console.WriteLine("IRSE: (Arg: -usedevversion is set) IRSE Will use Pre-releases versions");
            }

            if (noUpdateIRSE || !Config.Settings.EnableExtenderAutomaticUpdates)
            {
                UpdateManager.EnableAutoUpdates = false;
                Console.WriteLine("IRSE: (Arg: -noupdate is set or option in IRSE config is enabled) IRSE will not be auto-updated.");
            }

            if (noUpdateIR || !Config.Settings.EnableAutomaticUpdates)
            {
                SteamCMD.AutoUpdateIR = false;
                Console.WriteLine("IRSE: (Arg: -noupdateir is set) IsR Dedicated Serevr will not be auto-updated.");
            }

            Console.WriteLine();
            Console.ResetColor();

            //new SteamCMD().GetSteamCMD();

            // Run anything that doesn't require the loading of IR references above here

            m_serverInstance = new ServerInstance();

            ThisGameVersion = m_serverInstance.Assembly.GetName().Version.ToString();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("For Game Version: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(ForGameVersion + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("This Game Version: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(ThisGameVersion + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Online Game Version: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(SteamCMD.GetGameVersion() + "\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (new Version(ThisGameVersion) < SteamCMD.GetGameVersion())
            {
                Console.WriteLine("There is a new version of Interstellar Rift! Update your IR Installation!");
            }

            if (new Version(ForGameVersion) < new Version(ThisGameVersion))
            {
                Console.WriteLine("Interstellar Rifts Version is newer than what this version of IRSE Supports, Check for IRSE updates!");
            }

            Console.WriteLine();
            Console.ResetColor();

            ServerInstance.Instance.OnServerStarted += Instance_OnServerStarted;

            updateManager = new UpdateManager(); // REPO NEEDS TO BE PUBLIC

            //Build IR's paths so we can use the Localization system
            Game.Program.InitFileSystems();
            Game.Program.InitGameDirectory("InterstellarRift");
            Game.Program.CommandLineArgs = new string[1];

            m_serverInstance.PluginManager.LoadAllPlugins();
          
            SetupGUI();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("IRSE: Ready for Input, try /help !");
            Console.ResetColor();

            if (autoStart || Config.Settings.AutoStartEnable)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("IRSE: Arg: -autostart or Gui's Autostart Checkbox was Checked)");
                Console.ResetColor();
                PendingServerStart = true;
            }

            //console logic for commands
            ReadConsoleCommands(args);
        }

        #region GUI

        /// <summary>
        /// The UI Thread
        /// </summary>
        internal static void SetupGUI()
        {
            if (!Environment.UserInteractive)
            {
                Console.WriteLine("Non interactive environment detected, GUI disabled");
                return;
            }

            if (!_useGui)
            {
                Console.WriteLine("GUI Disabled");
                return;
            }

            if (uiThread != null)
            {
                if (m_form.InvokeRequired)
                    m_form.Invoke(new SafeCall(OpenGUI));
                else
                    OpenGUI();

                return;
            }

            uiThread = new Thread(LoadGUI);
            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = true;
            uiThread.Start();
        }

        /// <summary>
        /// Allows Thread Safe calls to the Form
        /// </summary>
        private delegate void SafeCall();

        /// <summary>
        /// do NOT call this by itself, call SetupGui()
        /// </summary>
        private static void OpenGUI()
        {
            if (m_form.IsDisposed)
                return;

            m_form.WindowState = FormWindowState.Normal;
            m_form.Visible = true;
            m_form.BringToFront();
            m_form.Show();
        }

        /// <summary>
        /// Loads the gui into its own thread !!DO NOT CALL!! call SetupGUI()
        /// </summary>
        [STAThread]
        private static void LoadGUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Console.WriteLine("Loading GUI..");

            if (m_form == null || m_form.IsDisposed)
            {
                m_form = new ExtenderGui();
            }

            m_form.Text = WindowTitle + " GUI";

            GUI = m_form;

            Application.Run(m_form);
        }

        #endregion GUI

        internal static void Restart()
        {
            if (ServerInstance.Instance != null)
            {
                if (ServerInstance.Instance.IsRunning)
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
            startInfo.FileName = thisProcess.MainModule.FileName;
            startInfo.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            startInfo.Arguments = string.Join(" ", CommandLineArgs);
            startInfo.WindowStyle = thisProcess.StartInfo.WindowStyle;

            var proc = Process.Start(startInfo);
            
            thisProcess.Kill();
        }

        public void BuildConsoleCommands()
        {
            
            IRSEConsoleCommands["help"] = (args) => 
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("/help - This page!");
                Console.WriteLine("/opengui - If closed, will open and/or focus the GUI to the front.");
                Console.WriteLine("/start - Starts the server if its not running!");
                Console.WriteLine("/stop - Stops the server if its running!");
                Console.WriteLine("/restart - Restarts IRSE, if autostart is set the server will start automatically.");
                Console.WriteLine("/checkupdate - Checks for IRSE updates. Prompts user with new update details.");
                Console.WriteLine("/forceupdate - Forces an Update of IRSE with no prompts.");
                Console.ResetColor();
            };

            IRSEConsoleCommands["start"] = (args) => StartServer();

            IRSEConsoleCommands["stop"] = (args) =>
            {
                if (ServerInstance.Instance.IsRunning)
                    ServerInstance.Instance.Stop();
                else
                    Console.WriteLine("The server is not running");
            };


            IRSEConsoleCommands["restart"] = (args) =>
                Restart();

            IRSEConsoleCommands["opengui"] = (args) =>
                SetupGUI();

            IRSEConsoleCommands["checkupdate"] = (args) => 
                updateManager.CheckForUpdates().GetAwaiter().GetResult();


            IRSEConsoleCommands["forceupdate"] = (args) => 
                updateManager.CheckForUpdates(true).GetAwaiter().GetResult();
           
        }

        /// <summary>
        /// This contains the console commands
        /// </summary>
        public void ReadConsoleCommands(string[] args)
        {
            BuildConsoleCommands();
         
            HWnd = Process.GetCurrentProcess().MainWindowHandle;
            string line = null;

            while (true)
            {
                line = Console.ReadLine();

                if (PendingServerStart)
                {
                    PendingServerStart = false;
                    StartServer();
                    
                }

                if (ServerInstance.Instance.IsRunning)
                {                  
                    while (ServerInstance.Instance.IsRunning)
                    {
                        ConsoleCoroutine.MoveNext();
                        Thread.Sleep(50);
                    }                   
                }

                if (ServerInstance.Instance.IsRunning)
                    continue;

                if (!string.IsNullOrEmpty(line) && line.Length > 1)
                {
                    if (!line.StartsWith("/"))
                    {
                        continue;
                    }

                    string cmd = line.Split(" ".ToCharArray())[0].Replace("/", "");
                    string[] lineArgs = line.Split(" ".ToCharArray()).Skip(1).ToArray();

                    try
                    {
                        IRSEConsoleCommands[cmd](lineArgs);
                    }
                    catch (Exception ex)
                    {
                        mainLog.Error("IRSE Command run exception", ex);
                    }

                }

            }
        }
        private void Instance_OnServerStarted()
        {
            mainLog.Warn("IRSE: Game Console Commands Enabled.");
            Program.PostMessage(Program.HWnd, Program.WM_KEYDOWN, Program.VK_RETURN, 0);
        }

        private static void StartServer()
        {
            if (!ServerInstance.Instance.IsRunning)
            {
                CommandSystem.Singleton = new CommandSystem();
                Program.ConsoleCoroutine =
                    CommandSystem.Singleton.Logic((object)null, Game.Configuration.Globals.NoConsoleAutoComplete);

                ServerInstance.Instance.Start();
            }
            else
                Console.WriteLine("The server is already running.");
        }

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

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
                Console.WriteLine("Attempting to stop any running servers.");
                if (ServerInstance.Instance != null)
                    ServerInstance.Instance.Stop();
                Console.WriteLine("Closing. Press any key to quit");

                Console.ReadKey(true);
            }
            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    public class SupportedGameAssemblyVersion : Attribute
    {
        public string someText;

        public SupportedGameAssemblyVersion() : this(string.Empty)
        {
        }

        public SupportedGameAssemblyVersion(string txt)
        {
            someText = txt;
        }
    }
}