using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace IRSE.Modules
{
    public class SteamCMD
    {
        private static NLog.Logger mainLog;

        private static string SteamCMDDir = Path.Combine(FolderStructure.IRSEFolderPath, "steamcmd");
        private static string SteamCMDExe = Path.Combine(SteamCMDDir, "steamcmd.exe");
        private static string SteamCMDZip = Path.Combine(SteamCMDDir, "steamcmd.zip");
        private static string GameNewsURL = "https://api.steampowered.com/ISteamNews/GetNewsForApp/v2?appid=363360&count=1";

        public static bool AutoUpdateIR = true;

        public SteamCMD()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();
            mainLog.Info("Running SteamCMD Checks..");
        }

        private int? GetLineNumber([CallerLineNumber] int? lineNumber = null) => lineNumber;

        private bool errored = true;
        private int count = 0;

        public bool HandleLogin() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|-------------------------------------------------------------------------|");
            Console.WriteLine("INTERSTELLAR RIFT SERVER EXTENDER STEAM MANAGER.");
            Console.WriteLine("|-------------------------------------------------------------------------|");
            Console.WriteLine("Interstellar Rift Dedicated server requires you own the game on steam.");
            Console.WriteLine();
            Console.WriteLine("You can input your steam login by following the prompts.");
            Console.WriteLine();
            Console.WriteLine("SteamCMD itself will ask for your SteamGuard code. This is needed only once.");
            Console.WriteLine();
            Console.WriteLine("IRSE will store your password securely on your system if you agree to it. ");
            Console.WriteLine("");
            Console.WriteLine("This will allow IRSE to manage your IR installation to keep it updated.");
            Console.WriteLine("|-------------------------------------------------------------------------|");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("If you don't agree to having IRSE manage installations. There's a batch file");
            Console.WriteLine("Press Q now to close IRSE so you can move irse.exe to an existing installation of Interstellar Rift.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press S if you would like IRSE to manage IR updates.");

            // quit if they dont agree
            if (Console.ReadKey().Key == ConsoleKey.Q)
            {
                Config.Instance.Settings.ManageSteamCMD = false;
                Config.Instance.SaveConfiguration();
                Environment.Exit(0);
            }


            Console.ReadLine();



            return false;
        }


        public bool GetSteamCMD()
        {
         

            if (!AutoUpdateIR)
            {
                if (!File.Exists(Path.Combine(FolderStructure.RootFolderPath, "IR.exe")))
                {
                    mainLog.Warn("IRSE: IR.EXE wasn't found.");
                    mainLog.Info("Make sure IRSE.exe is in the same folder as IR.exe.");
                    mainLog.Info("Press enter to close.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                return false;
            }

            if (!Directory.Exists(SteamCMDDir))
                Directory.CreateDirectory(SteamCMDDir);

            if (!File.Exists(SteamCMDExe))
            {
                try
                {
                    mainLog.Info("SteamCMD does not exist, downloading!");

                    using (var client = new WebClient())
                        client.DownloadFile("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip", SteamCMDZip);

                    mainLog.Info("Done! Unpacking and starting SteamCMD to install Interstellar Rift Dedicated Server");

                    ZipFile.ExtractToDirectory(SteamCMDZip, SteamCMDDir);
                    File.Delete(SteamCMDZip);
                }
                catch (Exception)
                {
                    mainLog.Error("Could not download or unpack SteamCMD. Going into manual mode. Please install Interstellar Rift and copy IRSE there!");
                    return false;
                }
            }

            string script = @"+force_install_dir ../../ +app_update 363360 validate +quit";

            try
            {
                mainLog.Info("Checking/Updating IR");

                var steamCmdinfo = new ProcessStartInfo(SteamCMDExe, script)
                {
                    WorkingDirectory = Path.GetFullPath(SteamCMDDir),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    StandardOutputEncoding = Encoding.ASCII
                };
                Process steamCmd = Process.Start(steamCmdinfo);

                while (!steamCmd.HasExited)
                {
                    string line = steamCmd.StandardOutput.ReadLine();
                    errored = !line.Contains("ERROR!");

                    Console.WriteLine(line);
                    Thread.Sleep(100);

                    count++;
                }
            }
            catch (Exception)
            {
                mainLog.Error("Could not start SteamCMD. Going into manual mode. Please run SteamCMD manually to install or update the dedicated server!");
                return false;
            }

            if (errored || count == 1)
                mainLog.Warn("Interstellar Rift has NOT been installed!");
            else
                mainLog.Info("Interstellar Rift has been successfully installed or updated!");

            return true;
        }

        public static Root SteamIRData { get; set; }

        public static Version GetGameVersion()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string info = wc.DownloadString(GameNewsURL);

                    SteamIRData = JsonConvert.DeserializeObject<Root>(info);

                    string verString = SteamIRData.appnews.newsitems.FirstOrDefault().title;

                    Match match = Regex.Match(verString, @"(\d+\.)?(\d+\.)?(\d+\.)?(\*|\d+)");

                    if (match.Success)
                        return new Version(match.Value);
                    else
                        throw new Exception("Could not match version from URL, Please use github issues at our repository.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not get game version from " + GameNewsURL + "\n" + ex.ToString());
            }
            return null;
        }

        public class Newsitem
        {
            public string gid { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public bool is_external_url { get; set; }
            public string author { get; set; }
            public string contents { get; set; }
            public string feedlabel { get; set; }
            public int date { get; set; }
            public string feedname { get; set; }
            public int feed_type { get; set; }
            public int appid { get; set; }
            public List<string> tags { get; set; }
        }

        public class Appnews
        {
            public int appid { get; set; }
            public List<Newsitem> newsitems { get; set; }
            public int count { get; set; }
        }

        public class Root
        {
            public Appnews appnews { get; set; }
        }
    }
}