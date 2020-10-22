using IRSe.Modules;
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

        public static PasswordEncoder PasswordEncoder;

        public SteamCMD()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();
        }

        public bool AskToManage()
        {
            if (Config.Instance.Settings.DeclinedSteamCMDManagement)
                return false;

            if (!string.IsNullOrEmpty(Config.Instance.Settings.HashedSteamUserName))
                return true;

            if (!string.IsNullOrEmpty(Config.Instance.Settings.HashedSteamPassword))
                return true;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
            "|--------------------------------------------------------------------------|\n" +
            "|             INTERSTELLAR RIFT SERVER EXTENDER STEAM MANAGER.             |\n" +
            "|--------------------------------------------------------------------------|\n" +
            "| Interstellar Rift Dedicated server requires you own the game on steam.   |\n" +
            "| You can input your steam login by following the prompts.                 |\n" +
            "|                                                                          |\n" +
            "| IRSE will store your password encrypted in the config. Only this PC this |\n" +
            "| is ran on has the ability to decrypt, you must put your details in again |\n" +
            "| if you move the game install to another computer. And every computer its |\n" +
            "| ran on.                                                                  |\n" +
            "|                                                                          |\n" +
            "| This will allow IRSE to manage your IR installation to keep it updated.  |\n" +
            "| This feature is optional, without it you will need to manage updates     |\n" +
            "| yourself.                                                                |\n" +
            "|--------------------------------------------------------------------------|\n\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press Y if you would like IRSE to manage IR updates.");
            Console.WriteLine("Press Q if you would like to handle it yourself.");
            Console.WriteLine();
            Console.ResetColor();

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Q:
                    Config.Instance.Settings.ManageSteamCMD = false;
                    Config.Instance.Settings.DeclinedSteamCMDManagement = true;
                    Config.Instance.SaveConfiguration(true);
                    break;

                case ConsoleKey.Y:
                    Config.Instance.Settings.ManageSteamCMD = true;
                    Config.Instance.SaveConfiguration(true);

                    return true;
            }

            return false;
        }

        private string steamGuardCode = "";

        public bool GetLoginDetails(bool invalidPassword = false)
        {
            string username = "";
            string password = "";

            if (!string.IsNullOrEmpty(Config.Instance.Settings.HashedSteamUserName))
                username = SteamCMD.PasswordEncoder.DecryptWithByteArray(Config.Instance.Settings.HashedSteamUserName);

            if (!string.IsNullOrEmpty(Config.Instance.Settings.HashedSteamPassword))
                password = SteamCMD.PasswordEncoder.DecryptWithByteArray(Config.Instance.Settings.HashedSteamPassword);

            if (invalidPassword)
                password = "";

            ConsoleKeyInfo key;

            if (string.IsNullOrEmpty(username))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("What is your Steam Username? (Please type it correctly, its hidden from view.)");
                Console.WriteLine("Press Enter when done. or Escape to quit.");
                Console.WriteLine();
                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Enter)
                    {
                        if (key.Key == ConsoleKey.Backspace && username.Length > 0 && Console.CursorLeft > 0)
                        {
                            username.Remove(username.Length - 1);

                            Console.CursorLeft--;
                            Console.Write('\0');
                            Console.CursorLeft--;
                        }
                        else if (key.Key != ConsoleKey.Backspace)
                        {
                            username += key.KeyChar.ToString();
                            Console.Write("*");
                        }
                    }

                    if (key.Key == ConsoleKey.Escape)
                        return false;
                } while (key.Key != ConsoleKey.Enter);

                Console.WriteLine();

                if (username != Config.Instance.Settings.HashedSteamUserName)
                    Config.Instance.Settings.HashedSteamUserName = SteamCMD.PasswordEncoder.EncryptWithByteArray(username);

                Config.Instance.SaveConfiguration(true);
            }

            if (string.IsNullOrEmpty(password))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("What is your Steam Password? (Please type it correctly, its hidden from view.)");
                Console.WriteLine("Press Enter when done. or Escape to go back to the previous menu");
                Console.ResetColor();
                Console.WriteLine();

                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Enter)
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0 && Console.CursorLeft > 0)
                        {
                            password.Remove(password.Length - 1);

                            Console.CursorLeft--;
                            Console.Write('\0');
                            Console.CursorLeft--;
                        }
                        else if (key.Key != ConsoleKey.Backspace)
                        {
                            password += key.KeyChar.ToString();
                            Console.Write("*");
                        }
                    }

                    if (key.Key == ConsoleKey.Escape)
                        return false;
                } while (key.Key != ConsoleKey.Enter);

                Console.WriteLine();

                if (password != Config.Instance.Settings.HashedSteamPassword)
                    Config.Instance.Settings.HashedSteamPassword = SteamCMD.PasswordEncoder.EncryptWithByteArray(password);

                Config.Instance.SaveConfiguration(true);
            }

            if (Config.Instance.Settings.NeedsSteamGuard)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("Please enter a SteamGuard Code.");
                Console.WriteLine("If your account doesn't need steamguard, just press Enter.");
                Console.WriteLine("Press Enter when done. or Escape to quit.");
                Console.ResetColor();
                Console.WriteLine();

                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Enter)
                    {
                        if (key.Key == ConsoleKey.Backspace && steamGuardCode.Length > 0 && Console.CursorLeft > 0)
                        {
                            steamGuardCode.Remove(steamGuardCode.Length - 1);

                            Console.CursorLeft--;
                            Console.Write('\0');
                            Console.CursorLeft--;
                        }
                        else if (key.Key != ConsoleKey.Backspace)
                        {
                            string skey = key.KeyChar.ToString();
                            Console.Write(skey);
                            steamGuardCode += skey;
                        }
                    }

                    if (key.Key == ConsoleKey.Escape)
                        return false;
                } while (key.Key != ConsoleKey.Enter);

                Console.WriteLine();
            }

            return true;
        }

        public bool GetSteamCMD()
        {
            if (Config.Instance.Settings.DeclinedSteamCMDManagement)
                return false;

            mainLog.Info("Running SteamCMD Checks..");

            if (!AskToManage())
                return false;

            if (!File.Exists(SteamCMDExe))
            {
                try
                {
                    if (!Directory.Exists(SteamCMDDir))
                        Directory.CreateDirectory(SteamCMDDir);

                    mainLog.Info(string.Format(Program.Localization.Sentences["SteamCMDNoExist"]));

                    using (var client = new WebClient())
                        client.DownloadFile("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip", SteamCMDZip);

                    mainLog.Info(string.Format(Program.Localization.Sentences["Unpacking"]));

                    ZipFile.ExtractToDirectory(SteamCMDZip, SteamCMDDir);
                    File.Delete(SteamCMDZip);
                }
                catch (Exception ex)
                {
                    mainLog.Error(string.Format(Program.Localization.Sentences["ManualModeUnpack"] + $": Error {ex}"));
                    return false;
                }
            }

            if (Config.Instance.Settings.ManageSteamCMD)
            {
                GetLoginDetails();
            }

            if (!Directory.Exists(SteamCMDDir))
                Directory.CreateDirectory(SteamCMDDir);

            RunSteamCMD();

            return true;
        }

        private int timesAsked = 0;

        private bool RunSteamCMD()
        {
            string script = $"@NoPromptForPassword 1 " +
                $"+force_install_dir ../../ " +
                $"+login {PasswordEncoder.DecryptWithByteArray(Config.Instance.Settings.HashedSteamUserName)} " +
                $"{(Config.Instance.Settings.NeedsSteamGuard ? PasswordEncoder.DecryptWithByteArray(Config.Instance.Settings.HashedSteamPassword) : "")} {steamGuardCode} " +
                $"+app_update 363360 +quit";

            try
            {
                mainLog.Info(string.Format(Program.Localization.Sentences["CheckingIR"]));

                string path = Path.GetFullPath(SteamCMDDir);
                var steamCmdinfo = new ProcessStartInfo(SteamCMDExe, script)
                {
                    WorkingDirectory = path,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    StandardOutputEncoding = Encoding.ASCII
                };

                Process p = new Process();

                p.StartInfo = steamCmdinfo;

                p.EnableRaisingEvents = true;
                p.OutputDataReceived += new DataReceivedEventHandler(P_DataReceived);
                p.ErrorDataReceived += new DataReceivedEventHandler(P_DataReceived);

                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                mainLog.Error(ex, string.Format(Program.Localization.Sentences["ManualModeStart"]));
                return false;
            }

            if (errored || count == 1)
                mainLog.Warn(string.Format(Program.Localization.Sentences["NotInstalled"]));

            return true;
        }

        private bool errored = true;
        private int count = 0;

        private void P_DataReceived(object sender, DataReceivedEventArgs e)
        {
            string line = string.IsNullOrEmpty(e.Data) ? "" : e.Data;

            if (!line.Contains("ERROR!"))
                errored = false;

            if (line.Contains("Rate Limit Exceeded"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Steam Login Rate Limit Exceeded. Please try again in 10 minutes");
                Console.ResetColor();
                errored = true;
            }

            if (line.Contains("Logging in user '")) // hide the username from view.
                line = "Logging in user 'REDACTED' to Steam Public ...";

            if (line.Contains("Invalid Password") || line.Contains("ERROR! Failed to request AppInfo update, not online or not logged in to Steam."))
            {
                timesAsked++;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Your steam password was invalid. Please try again. ({timesAsked}/3)");
                Console.ResetColor();
                if (timesAsked < 3 && GetLoginDetails(true))
                {
                    RunSteamCMD();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Attempt Limit Reached, please make sure your details are right and try again by restarting IRSE.");
                    Console.ResetColor();
                    errored = true;
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(line);
            Console.ResetColor();

            if (line.Contains("Logged in OK"))
            {
                Config.Instance.Settings.NeedsSteamGuard = false;
                Config.Instance.SaveConfiguration(true);
            }

            if (line.Contains("Success!"))
            {
                mainLog.Info(string.Format(Program.Localization.Sentences["Success"]));
            }

            count++;
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
                        throw new Exception("Could not match version from URL");
                }
            }
            catch (Exception)
            {
                Console.WriteLine(string.Format(Program.Localization.Sentences["NoGameVersion"], GameNewsURL));
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