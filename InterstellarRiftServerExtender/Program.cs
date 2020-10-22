using HarmonyLib;
using IRSe.Modules;
using IRSE.GUI.Forms;
using IRSE.Managers;
using IRSE.Modules;
using NLog;
using NLog.Config;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Localization = IRSE.Modules.Localization;

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

        public static string[] CommandLineArgs = new string[50];

        private static bool debugMode = true;
        private static ExtenderGui m_form;

        public Version CurrentGameVerson { get; }

        public static bool GUIDisabled => !Environment.UserInteractive;

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

        public static bool Wait { get; set; }
        public static Localization Localization { get { return m_localization; } internal set { m_localization = value; } }
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

            //if (!Dev)
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CrashDump.CurrentDomain_UnhandledException);

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
            new FolderStructure().Build();

            CommandLineArgs = args;

            SetTitle();

            SteamCMD.PasswordEncoder = new PasswordEncoder();

            m_config = new Config();
            debugMode = m_config.Settings.DebugMode;

            m_localization = new Localization();

            m_localization.Load(m_config.Settings.CurrentLanguage);

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

                pathh = "IRSE.Resources." + pathh;
                using (Stream stream = executingAssembly.GetManifestResourceStream(pathh))
                {
                    if (stream == null) return null;

                    var assemblyRawBytes = new byte[stream.Length];
                    stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                    //Console.WriteLine("found missing dll: " + pathh);
                    return Assembly.Load(assemblyRawBytes);
                }
            };

            if (Program.Localization.Sentences.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Missing Language Resources! Please make sure all files are installed!");
                Console.WriteLine("Press any key to Quit.");
                Console.ReadKey(true);
                Environment.Exit(0);
            }

            Console.WriteLine(string.Format(Program.Localization.Sentences["Initialization"], Version));

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

            if (args.Contains("-noupdateir") || !Config.Settings.EnableAutomaticUpdates)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format(Program.Localization.Sentences["NoUpdateIR"]));
            }
            //else
            //new SteamCMD().GetSteamCMD();

            if (!File.Exists(Path.Combine(FolderStructure.RootFolderPath, "IR.exe")))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format(Program.Localization.Sentences["IRNotFound"]));
                Console.ReadLine();
                Environment.Exit(0);
            }

            Instance = new Program();
            Instance.Run(args);
        }

        // this is where stuff goes!
        private void Run(string[] args)
        {
            // This is for args that should be used before IRSE loads

            string configPath = ExtenderGlobals.GetFilePath(IRSEFileName.NLogConfig);
            LogManager.Configuration = new XmlLoggingConfiguration(configPath);

            _useGui = true;

            if (args.Contains("-nogui"))
            {
                _useGui = false;
            }

            if (args.Contains("-usedevversion") || Config.Settings.EnableDevelopmentVersion)
            {
                Console.WriteLine(string.Format(Program.Localization.Sentences["UseDevVersion"]));
            }

            if (args.Contains("-noupdateirse") || !Config.Settings.EnableExtenderAutomaticUpdates)
            {
                UpdateManager.EnableAutoUpdates = false;
                Console.WriteLine(string.Format(Program.Localization.Sentences["NoUpdate"]));
                Console.WriteLine();
            }

            if (args.Contains("-autorestart") || Config.Settings.AutoRestartsEnable)
            {
                UpdateManager.EnableAutoRestarts = true;
                Console.WriteLine(string.Format("IRSE: (Arg: -autorestart is set) IRSE Will auto restart when it needs to."));
            }

            Console.WriteLine();
            Console.ResetColor();

            // Run anything that doesn't require the loading of IR references above here

            //Harmony
            if (Harmony.DEBUG)
                Console.WriteLine("IRSE: Harmony Debug enabled, logging will be in the file 'harmony.log.txt' on your desktop.");

            Console.WriteLine("Initializing Harmony Patches...");
            Harmony = new Harmony("com.tse.irse");
            Harmony.PatchAll();
            Console.WriteLine();

            m_serverInstance = new ServerInstance();

            ThisGameVersion = m_serverInstance.Assembly.GetName().Version.ToString();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(string.Format(Program.Localization.Sentences["ForGameVersion"]));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(ForGameVersion + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(string.Format(Program.Localization.Sentences["ThisGameVersion"]));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(ThisGameVersion + "\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(string.Format(Program.Localization.Sentences["OnlineGameVersion"]));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(SteamCMD.GetGameVersion() + "\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (new Version(ThisGameVersion) < SteamCMD.GetGameVersion())
            {
                Console.WriteLine(string.Format(Program.Localization.Sentences["NewIRVersion"]));
            }

            if (new Version(ForGameVersion) < new Version(ThisGameVersion))
            {
                Console.WriteLine(string.Format(Program.Localization.Sentences["IRNewer"]));
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("IRSE: Ready for Input, try using ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("help\n\n");
            Console.ResetColor();

            if (args.Contains("-autostart") || Config.Settings.AutoStartEnable)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format(Program.Localization.Sentences["AutoStart"]));
                Console.ResetColor();
                StartServer();
            }

            // run only when they change properties to update the non manual section of ServerConfigProperties
            //new Modules.GameConfig.ServerConfigConverter().BuildAndUpdateConfigProperties();

            //console logic for commands
            ReadConsoleCommands(args);
        }

        private void Instance_OnServerStarted()
        {
            Console.WriteLine(string.Format(Program.Localization.Sentences["GameConsoleEnabled"]));
        }

        private static void StartServer()
        {
            if (!ServerInstance.Instance.IsRunning)
            {
                Wait = true;
                ServerInstance.Instance.Start();
            }
            else
                Console.WriteLine(string.Format(Program.Localization.Sentences["ServerIsAlreadyRunning"]));
        }

        public static void CloseIRSE()
        {
            Console.WriteLine(string.Format(Program.Localization.Sentences["StopRunningServers"]));
            if (ServerInstance.Instance != null)
                ServerInstance.Instance.Stop(true);
        }

        internal static void Restart(bool prompt = true, bool consoleOnly = false)
        {
            if (prompt)
            {
                if (_useGui && !consoleOnly)
                {
                    DialogResult result = MessageBox.Show(
                        "IRSE has requested a Restart.\nPress 'Yes' to restart now. Or 'No' if you would like to restart later.",
                        "IRSE Restart Requested.",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.No)
                        return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("IRSE Restart Requested.");
                    Console.WriteLine("Press Y to proceed. Press N if you would like to restart later.");
                    Console.WriteLine();
                    Console.ResetColor();

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Y:
                            break;

                        case ConsoleKey.N:
                            Config.Instance.Settings.ManageSteamCMD = true;
                            Config.Instance.Settings.DeclinedSteamCMDManagement = false;
                            Config.Instance.SaveConfiguration(true);
                            return;
                    }
                }
            }
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

        #region GUI

        /// <summary>
        /// The UI Thread
        /// </summary>
        internal static void SetupGUI()
        {
            if (!Environment.UserInteractive)
            {
                Console.WriteLine(string.Format(Program.Localization.Sentences["NonInteractive"]));
                return;
            }

            if (!_useGui)
            {
                Console.WriteLine(string.Format(Program.Localization.Sentences["GUIDisabled"]));
                return;
            }

            Console.WriteLine(string.Format(Program.Localization.Sentences["LoadingGUI"]));

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

            if (m_form == null || m_form.IsDisposed)
            {
                m_form = new ExtenderGui();
            }

            m_form.Text = WindowTitle + " GUI";

            GUI = m_form;

            Application.Run(m_form);
        }

        #endregion GUI

        /// <summary>
        /// This contains the console commands
        /// </summary>
        public void ReadConsoleCommands(string[] args)
        {
            Wait = false;

            ConsoleCommandManager.InitHandlers();

            Program.ConsoleCoroutine = ConsoleCommandManager.IRSECommandSystem
                .Logic((object)null, Game.Configuration.Globals.NoConsoleAutoComplete || args.Contains("-noConsoleAutoComplete"));

            while (true)
            {
                if (PendingServerStart)
                {
                    PendingServerStart = false;
                    StartServer();
                }

                if (!Wait)
                    ConsoleCoroutine.MoveNext();

                Thread.Sleep(50);
            }
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
                CloseIRSE();
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