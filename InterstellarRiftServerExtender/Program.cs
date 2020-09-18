using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace InterstellarRiftServerExtender
{
    public class Program
    {
        #region Fields
        private static Thread uiThread;
        public static string ForGameVersion = "0.2.15.40";
        #endregion

        #region Properties
        public static Program IRSE { get; private set; }
        public static Application WinApp { get; private set; }
        public static Window MainWindow => WinApp.MainWindow; 
        public static Version Version => Assembly.GetEntryAssembly().GetName().Version;
        public static String VersionString => Version.ToString(4) + $" Branch: {ThisAssembly.Git.Branch}";
        public static String WindowTitle => String.Format("INTERSTELLAR RIFT SERVER EXTENDER V{0} - Game Version: {1}", VersionString, ForGameVersion);
        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = WindowTitle;
            IRSE = new Program();
            IRSE.Run(args);
        }

        // this is where stuff goes!
        private void Run(string[] args)
        {
            SetupGUI();


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
            Console.WriteLine("Loading GUI");

            if (WinApp == null)
            {
                Console.WriteLine("holy shit");
                WinApp = new Application();
                WinApp.StartupUri = new Uri("MainWindow.xaml", System.UriKind.Relative);
                WinApp.Run();    
            }
        }

        /// <summary>
        /// This contains the console commands
        /// </summary>
        public void ReadConsoleCommands(string[] commandLineArgs)
        {
            while (true)
            {
                string line = Console.ReadLine();

                if (line.Length > 1)
                {
                    if (!line.StartsWith("/"))
                    {
                        // if (Server.IsRunning)
                        //if (NetworkManager.Instance != null)
                        //    NetworkManager.Instance.MessageAllClients(cmd);
                        //else
                        // Console.WriteLine("The Server must be running to message connected clients!");

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
                    /*
                    if (Server.IsRunning && ServerInstance.Instance.CommandManager != null)
                    {
                        ServerInstance.Instance.CommandManager.HandleConsoleCommand(cmd, args);
                        flag = true;
                    }

                    

                    if (stringList[1] == "help")
                    {
                        HES.PrintHelp();
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
                        if (!Server.IsRunning)
                            ServerInstance.Instance.Start();
                        else
                            Console.WriteLine("The server is already running.");
                        flag = true;
                    }

                    if (stringList[1] == "stop")
                    {
                        if (Server.IsRunning)
                            ServerInstance.Instance.Stop();
                        else
                            Console.WriteLine("The server is not running");
                        flag = true;
                    }
                    */
                    if (stringList[1] == "opengui")
                    {
                        WinApp.Dispatcher.BeginInvoke((Action)(()=>
                        {

                            if (!MainWindow.IsVisible)
                            {
                                Console.WriteLine("Reopening GUI...");
                                MainWindow.Show();
                            }

                            if (MainWindow.WindowState == WindowState.Minimized) {
                                MainWindow.WindowState = WindowState.Normal;
                            }
                            
                            MainWindow.Activate();
                            MainWindow.Topmost = true; //required, resets topwindow
                            MainWindow.Topmost = false; //required, resets topwindow
                            MainWindow.Focus();                          
                        }));
                        flag = true;
                    }

                    if (!flag)
                        Console.WriteLine("bad syntax");
                }
            }
        }
    }
}
