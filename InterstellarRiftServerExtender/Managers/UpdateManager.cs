﻿using IRSE.Modules;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IRSE.Managers
{
    public class UpdateManager
    {
        private readonly string Organization = "InterstellarRiftCommunityTools";
        private readonly string Repository = "InterstellarRiftServerExtender";

        private static NLog.Logger mainLog; //mainLog.Error

        private GitHubClient _git = new GitHubClient(new ProductHeaderValue("InterstellarRiftServerExtender"));
        private const string UpdateFileName = "update.zip";
        private bool m_useDevRelease = Config.Instance.Settings.EnableDevelopmentVersion;

        public static UpdateManager Instance { get; private set; }

        public Release CurrentRelease { get; private set; }
        public Release DevelopmentRelease { get; private set; }

        public List<FileInfo> FileList = new List<FileInfo>();
        public List<FileInfo> CurrentFileList = new List<FileInfo>();

        public static bool EnableAutoUpdates = true;
        public static bool EnableAutoRestarts = false;
        public static bool GUIMode = false;
        public static bool HasUpdate = false;
        public static Version NewVersionNumber = new Version();

        public static bool UpdaterDone = false;
        public static bool WaitingForRestart = false;

        public UpdateManager()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();

            Instance = this;

            ServicePointManager.DefaultConnectionLimit = 10;

            foreach (string file in Directory.GetFiles(ExtenderGlobals.GetFolderPath(IRSEFolderName.Updates), "*", SearchOption.AllDirectories))
                FileList.Add(new FileInfo(file));

            foreach (string file in Directory.GetFiles(FolderStructure.RootFolderPath, "*", SearchOption.AllDirectories))
            {
                var currentFile = new FileInfo(file);

                if (currentFile.Extension == ".old")
                    currentFile.Delete();

                if (file.Contains("updates") || file.Contains("temp"))
                    continue;

                CurrentFileList.Add(currentFile);
            }

            CheckForUpdates().GetAwaiter().GetResult();
        }

        public async Task CheckForUpdates(bool forceUpdate = false)
        {
            try
            {
                await GetLatestReleaseInfo();
                CheckVersion(forceUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE:  Update Failed (CheckForUpdates)" + ex.ToString());
            }
        }

        public bool DownloadLatestRelease(bool getDevelopmentVersion = false)
        {
            try
            {
                Console.WriteLine("IRSE:  Downloading latest release...");

                WebClient client = new WebClient();
                client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(ReleaseDownloaded);

                if (getDevelopmentVersion)
                    client.DownloadDataAsync(new Uri(DevelopmentRelease.Assets.FirstOrDefault().BrowserDownloadUrl));
                else
                    client.DownloadDataAsync(new Uri(CurrentRelease.Assets.FirstOrDefault().BrowserDownloadUrl));

                while (!UpdaterDone) ;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE:  Update Failed (DownloadLatestRelease)" + ex.ToString());
            }
            return false;
        }

        private void ReleaseDownloaded(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                FileList.ForEach((file) => file.Delete());
                FileList.Clear();

                string updatePath = ExtenderGlobals.GetFolderPath(IRSEFolderName.Updates);

                File.WriteAllBytes(Path.Combine(updatePath, UpdateFileName), e.Result);
                ZipFile.ExtractToDirectory(Path.Combine(updatePath, UpdateFileName), updatePath);
                File.Delete(Path.Combine(updatePath, UpdateFileName));
                Console.WriteLine("IRSE:  Update has been downloaded!");

                foreach (string file in Directory.GetFiles(updatePath, "*", SearchOption.AllDirectories))
                    FileList.Add(new FileInfo(file));

                OnUpdateDownloaded?.Invoke(m_useDevRelease ? DevelopmentRelease : CurrentRelease);

                if (!GUIMode)
                {
                    ApplyUpdate();
                    Console.WriteLine("IRSE:  Update has been applied.");

                    if (Config.Instance.Settings.AutoRestartsEnable)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("IRSE:  Auto-Restarts enabled. Restarting...");
                        Program.Restart();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("IRSE:  Press Y to restart IRSE. or N if you want to restart later.");
                        Console.ResetColor();

                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.Y:
                                Program.Restart();
                                break;

                            case ConsoleKey.N:
                                break;

                            default:
                                break;
                        }
                    }
                }

                UpdaterDone = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE:  Update Failed (ReleaseDownloadedEvent)" + ex.ToString());
            }
        }

        public bool ApplyUpdate()
        {
            try
            {
                Console.WriteLine("IRSE:  Applying Update...");

                string updatePath = ExtenderGlobals.GetFolderPath(IRSEFolderName.Updates);
                string IRSEPath = ExtenderGlobals.GetFolderPath(IRSEFolderName.IRSE);

                // for all of the files already in the server folder
                foreach (var file in CurrentFileList)
                {
                    // if the old file has an updated version
                    if (FileList.Exists(x => x.Name == file.Name))
                    {
                        var newFile = FileList.Find(x => x.Name == file.Name);
                        var fullName = Path.GetFullPath(file.FullName);

                        // rename old file if the file exists
                        if (File.Exists(fullName))
                            File.Move(fullName, fullName + ".old");

                        // move new file if it doesn't already exist
                        if (!File.Exists(fullName) && File.Exists(Path.GetFullPath(newFile.FullName)))
                            File.Move(Path.GetFullPath(newFile.FullName), fullName);
                    }
                }

                if (EnableAutoRestarts)
                    Program.Restart();

                OnUpdateApplied?.Invoke(m_useDevRelease ? DevelopmentRelease : CurrentRelease);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE:  Update Failed (ApplyUpdate)" + ex.ToString());
            }
            return false;
        }

        public bool CheckVersion(bool forceUpdate = false)
        {
            try
            {
                if (!GUIMode) Console.WriteLine("Checking for IRSE updates...");

                if (m_useDevRelease && DevelopmentRelease == null)
                {
                    Console.WriteLine("No Development Updates Exist");
                    return false;
                }

                if (!m_useDevRelease && CurrentRelease == null)
                {
                    Console.WriteLine("No Updates Exist");
                    return false;
                }

                string devText = (m_useDevRelease ? "Development Version" : "");

                var checkedVersion = new Version(CurrentRelease?.TagName);

                if (m_useDevRelease)
                    checkedVersion = new Version(DevelopmentRelease?.TagName);

                NewVersionNumber = checkedVersion;

                Release localRelease = CurrentRelease;

                if (m_useDevRelease)
                    localRelease = DevelopmentRelease;

                if (WaitingForRestart)
                {
                    OnUpdateChecked?.Invoke(null);
                    Console.WriteLine("IRSE: An update is already in-progress. Please restart IRSE with 'irserestart' to use the new features.");
                    return true;
                }

                HasUpdate = (checkedVersion > Program.Version);

                if (GUIMode)
                {
                    OnUpdateChecked?.Invoke(localRelease);
                    return true;
                }

                if (HasUpdate)
                {
                    Console.WriteLine($"IRSE:  A new {devText} version of IRSE has been detected.\r\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Name: { localRelease.Name }");
                    Console.WriteLine($"Version: { localRelease.TagName }");
                    if (localRelease.Assets.Count > 0) Console.WriteLine($"Total Downloads: { localRelease.Assets.First().DownloadCount }");
                    if (localRelease.Assets.Count > 0) Console.WriteLine($"Published Date: { localRelease.Assets.First().CreatedAt.ToLocalTime() }\r\n");
                    Console.ResetColor();

                    if (EnableAutoUpdates || forceUpdate)
                    {
                        Console.WriteLine("IRSE: Auto updating");
                        DownloadLatestRelease(m_useDevRelease);
                        return true;
                    }

                    Console.WriteLine("Would you like to see the changes? (y/n)");

                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\r\nChanges:\r\n" + localRelease.Body);
                        Console.ResetColor();
                    }

                    if (m_useDevRelease)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("WARNING: Be absolutely sure that your server is backed up!");
                        Console.WriteLine("WARNING: Development Versions CAN break your server!\r\n");
                        Console.WriteLine($"Press Y to continue, or N to quit. (y/n)");
                        Console.ResetColor();

                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\r\nWould you like to update with the development version now? (y/n)");

                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.Y:
                                    Console.WriteLine("\r\n");
                                    DownloadLatestRelease(true);
                                    return true;

                                case ConsoleKey.N:
                                    Console.WriteLine("IRSE:  Skipping update.. We'll ask next time you restart IRSE!");
                                    return false;
                            }
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.N)
                        {
                            Console.WriteLine("IRSE:  Skipping update.. We'll ask next time you restart IRSE!");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Would you like to update now? (y/n)");

                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.Y:
                                Console.WriteLine("\r\n");
                                DownloadLatestRelease();
                                return true;

                            case ConsoleKey.N:
                                Console.WriteLine("IRSE:  Skipping update.. We'll ask next time you restart IRSE!");
                                return false;
                        }
                    }

                    return true;
                }
                else
                {
                    Console.WriteLine("IRSE:  IRSE is running the latest version!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE:  Update Failed (CheckVersion)" + ex.ToString());
            }
            return false;
        }

        public async Task GetLatestReleaseInfo()
        {
            try
            {
                CurrentRelease = await _git.Repository.Release.GetLatest(Organization, Repository).ConfigureAwait(false);

                var releases = await _git.Repository.Release.GetAll(Organization, Repository).ConfigureAwait(false);
                DevelopmentRelease = releases.FirstOrDefault(x => x.Prerelease == true);
            }
            catch (NotFoundException)
            {
                Console.WriteLine("Repository or Releases Not found error, check the organization and repository settings and that releases exist.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Failed (GetLatestReleaseInfo)" + ex.ToString());
            }
        }

        public delegate void UpdateEventHandler(Release release);

        public event UpdateEventHandler OnUpdateChecked;

        public event UpdateEventHandler OnUpdateDownloaded;

        public event UpdateEventHandler OnUpdateApplied;
    }
}