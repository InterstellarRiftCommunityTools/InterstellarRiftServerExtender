
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;

namespace IRSE.Modules
{
    public class SteamCMD
    {
        private static NLog.Logger mainLog;

        private static string SteamCMDDir = Path.Combine(FolderStructure.IRSEFolderPath, "steamcmd");
        private static string SteamCMDExe = Path.Combine(SteamCMDDir, "steamcmd.exe");
        private static string SteamCMDZip = Path.Combine(SteamCMDDir, "steamcmd.zip");

        public static bool AutoUpdateIR = true;

        public SteamCMD()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();
            mainLog.Info("Running SteamCMD Checks..");

        }

        private int? GetLineNumber([CallerLineNumber] int? lineNumber = null) => lineNumber;


        bool errored = true;
        int count = 0;
        public bool GetSteamCMD()
        {

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
                


            //if(Console.K)


            Console.ReadLine();


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

            if(errored || count == 1) 
                mainLog.Warn("Interstellar Rift has NOT been installed!");
            else
                mainLog.Info("Interstellar Rift has been successfully installed or updated!");


            return true;
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            
        }
    }
}