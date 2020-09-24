using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;

namespace IRSE.Modules
{
    public class SteamCMD
    {
        private static NLog.Logger mainLog;

        private static  string SteamCMDDir = Path.Combine(FolderStructure.IRSEFolderPath, "steamcmd");
        private static string SteamCMDExe = $"{SteamCMDDir}\\steamcmd.exe";
        private static string SteamCMDZip = $"{SteamCMDDir}\\steamcmd.zip";

        public static bool AutoUpdateIR = true;

        public SteamCMD()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();
            mainLog.Info("Running SteamCMD Checks..");
        }

        public bool GetSteamCMD()
        {
            if (!AutoUpdateIR)
            {
                if (!File.Exists(Path.Combine(FolderStructure.RootFolderPath, "IR.exe")))
                {
                    mainLog.Warn("Interstellar Rift EXE wasn't found.");
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
                mainLog.Info("Updating Interstellar Rift Dedicated Server...");

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
                    Console.WriteLine(steamCmd.StandardOutput.ReadLine());
                    Thread.Sleep(100);
                }
            }
            catch (Exception)
            {
                mainLog.Error("Could not start SteamCMD. Going into manual mode. Please run SteamCMD manually to install or update the dedicated server!");
                return false;
            }

            mainLog.Info("Interstellar Rift has been successfully installed or updated!");

            return true;
        }
    }
}