using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;

namespace IRSE.Modules
{
    public class Localization
    {
        public static Dictionary<string, string> Languages = new Dictionary<string, string>()
        {
            ["English (United States)"] = "IRSE.resx",
            ["Russian (Russia)"] = "IRSE.ru-RU.resx",
        };

        public static string PathFolder = Path.Combine(FolderStructure.IRSEFolderPath, "localization");
        public static Version Version = new Version("0.0.0.10");
        private Dictionary<string, string> m_sentences = new Dictionary<string, string>();
        private static NLog.Logger mainLog;

        public Localization()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();
        }

        public Dictionary<string, string> Sentences
        {
            get
            {
                try
                {
                    return this.m_sentences;
                }
                catch (Exception)
                {
                    Console.WriteLine("Missing Localization Key");
                }
                return null;
            }
        }

        public void Load(string languageName)
        {
            Localization.CreateDefault(); // just create the file each time to avoid needing to update the file as well

            string fileName;

            try
            {
                fileName = Languages[languageName];
            }
            catch (Exception)
            {
                fileName = Languages.First().Value;
            }

            string path = Path.Combine(FolderStructure.IRSEFolderPath, "localization", fileName);

            if (File.Exists(path))
            {
                using (ResXResourceReader resXresourceReader = new ResXResourceReader(path))
                {
                    foreach (DictionaryEntry dictionaryEntry in resXresourceReader)
                        this.m_sentences.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
                }
            }
            else
            {
                mainLog.Info("No localization file detected ! English language loading...");
                Localization.CreateDefault();
                Load("English (United States)");
            }
        }

        public static void CreateDefault()
        {
            string path = Path.Combine(FolderStructure.IRSEFolderPath, "localization", "IRSE.Resx");

            if (File.Exists(path))
                File.Delete(path);

            File.Create(path).Close();
            using (ResXResourceWriter resXresourceWriter = new ResXResourceWriter(path))
            {
                // Program.cs
                resXresourceWriter.AddResource("version", Localization.Version.ToString());

                resXresourceWriter.AddResource("Initialization", "Interstellar Rift Extended Server v{0} Initializing....");
                resXresourceWriter.AddResource("IRNotFound", "IRSE: IR.EXE wasn't found.\nMake sure IRSE.exe is in the same folder as IR.exe.\nPress enter to close.");
                resXresourceWriter.AddResource("GameConsoleEnabled", "IRSE: Game Console Commands Enabled.");
                resXresourceWriter.AddResource("ServerIsAlreadyRunning", "The server is already running!");
                resXresourceWriter.AddResource("StopRunningServers", "Attempting to stop any running servers.");
                // Program.cs - Command Line Args
                resXresourceWriter.AddResource("UseDevVersion", "IRSE: (Arg: -usedevversion is set) IRSE Will use Pre-releases versions");
                resXresourceWriter.AddResource("NoUpdate", "IRSE: (Arg: -noupdate is set or option in IRSE config is enabled) IRSE will not be auto-updated.");
                resXresourceWriter.AddResource("NoUpdateIR", "IRSE: (Arg: -noupdateir is set) IsR Dedicated Server will not be auto-updated.");
                resXresourceWriter.AddResource("AutoStart", "IRSE: Arg: -autostart or Gui's Autostart Checkbox was Checked)");
                resXresourceWriter.AddResource("NonInteractive", "Non interactive environment detected, GUI disabled");
                resXresourceWriter.AddResource("GUIDisabled", "IRSE: GUI Disabled");
                resXresourceWriter.AddResource("CommandNoExist", "IRSE: command Doesn't Exist.");
                resXresourceWriter.AddResource("CommandException", "IRSE: Command exception!");
                // Program.cs - Versioning
                resXresourceWriter.AddResource("ForGameVersion", "For Game Version: ");
                resXresourceWriter.AddResource("ThisGameVersion", "This Game Version: ");
                resXresourceWriter.AddResource("OnlineGameVersion", "Online Game Version: ");
                resXresourceWriter.AddResource("NewIRVersion", "There is a new version of Interstellar Rift! Update your IR Installation for new IR features!");
                resXresourceWriter.AddResource("IRNewer", "Interstellar Rift version was updated past what this IRSE was built on. Note: Updating IRSE isn't always needed. It's only required when the minimum required version is raised!");
                // Program.cs - Console Commands
                resXresourceWriter.AddResource("LoadingGUI", "Loading GUI...");
                resXresourceWriter.AddResource("HelpCommand", "help - this page ;)");
                resXresourceWriter.AddResource("OpenGUICommand", "opengui - If closed, will open and/or focus the GUI to the front.");
                resXresourceWriter.AddResource("StartCommand", "start - Starts the server if its not running!");
                resXresourceWriter.AddResource("StopCommand", "stop - stops the server if it's running!");
                resXresourceWriter.AddResource("RestartCommand", "restart - Restarts IRSE, if autostart is set the server will start automatically.");
                resXresourceWriter.AddResource("CheckUpdateCommand", "checkupdate - Checks for IRSE updates. Prompts user with new update details.");
                resXresourceWriter.AddResource("ForceUpdateCommand", "forceupdate - Forces an Update of IRSE with no prompts.");

                // PluginManager.cs
                resXresourceWriter.AddResource("FailedInitPlugin", "Failed initialization of Plugin {0}. Exception: {1}");
                resXresourceWriter.AddResource("FailedLoadPlugin", "Failed load of Plugin {0}. Exception: {1}");
                resXresourceWriter.AddResource("FailedLoadAssembly", "Failed to load assembly : {0} : {1}");
                resXresourceWriter.AddResource("FailedShutdownPlugin", "Uncaught Exception in Plugin {0}. Exception: {1}");
                resXresourceWriter.AddResource("InitializationPlugin", "Initialization of Plugin {0} failed. Could not find a public, parameterless constructor for {0}");
                resXresourceWriter.AddResource("InitializingPlugin", "Initializing Plugin : {0}");
                resXresourceWriter.AddResource("LoadingPlugin", "Loading Plugin : {0}");
                resXresourceWriter.AddResource("ShutdownPlugin", "Shutting down Plugin {0}");

                // ServerWrappers/Program.cs
                resXresourceWriter.AddResource("LaunchingServer", "IRSE: Launching Server...");
                resXresourceWriter.AddResource("NoInitWrapper", "IRSE: Could not initialize the wrapper. This is a fatal error, please report the exception to the github issues. Shutting Down...");
                resXresourceWriter.AddResource("WaitingForServer", "IRSE: Waiting for server");
                resXresourceWriter.AddResource("GameBaseCode", "IR.exe base game code was probably changed, this is a fatal error, please report the error below to the github issues.");

                // SteamCMD.cs
                resXresourceWriter.AddResource("SteamCMDNoExist", "SteamCMD does not exist, downloading!");
                resXresourceWriter.AddResource("Unpacking", "Done! Unpacking and starting SteamCMD to install Interstellar Rift Dedicated Server");
                resXresourceWriter.AddResource("ManualModeUnpack", "Could not download or unpack SteamCMD. Going into manual mode. Please install Interstellar Rift and copy IRSE there!");
                resXresourceWriter.AddResource("CheckingIR", "Checking/Updating IR");
                resXresourceWriter.AddResource("ManualModeStart", "Could not start SteamCMD. Going into manual mode. Please run SteamCMD manually to install or update the dedicated server!");
                resXresourceWriter.AddResource("NotInstalled", "Interstellar Rift has NOT been installed!");
                resXresourceWriter.AddResource("Success", "Interstellar Rift has been successfully installed or updated!");
                resXresourceWriter.AddResource("NoGameVersion", "Could not get game version from {0}");

                // GUI ExtenderConfig.cs
                resXresourceWriter.AddResource("AreYouSure", "Are you sure?");
                resXresourceWriter.AddResource("ServerOnlineChat", "Server Online, Ready For Chat.");
                resXresourceWriter.AddResource("ServerConfigSaved", "Server Config Saved.");
                resXresourceWriter.AddResource("IRSEConfigSaved", "IRSE Config Saved.");
                resXresourceWriter.AddResource("LooseServerChanges", "You want to loose the Server Config changes?");
                resXresourceWriter.AddResource("ReloadedServerConfig", "Reloaded the config from appdata server.json");
                resXresourceWriter.AddResource("LooseExtenderConfig", "You want to loose all changes to the Extender Config?");
                resXresourceWriter.AddResource("ReloadExtenderConfig", "You wish to reload the Extender Config?");
                resXresourceWriter.AddResource("ReloadedExtenderConfig", "Reloaded extender config.");
                resXresourceWriter.AddResource("ReloadServerConfig", "Are you sure you want to reload the settings from the server.json?");
                resXresourceWriter.AddResource("ServerSettings", "Server Settings");
                resXresourceWriter.AddResource("ServerStopped", "The Server Is Already Stopped");

                resXresourceWriter.AddResource("KickPlayers", "You wish to Kick the selected Player(s)?");
                resXresourceWriter.AddResource("BanPlayers", "You wish to Ban the selected Player(s)?");
                resXresourceWriter.AddResource("ForgetPlayers", "You wish to Forget the selected Player(s)?\n\n This will kick the selected players, then force them to pick a faction the next time they login.");
                resXresourceWriter.AddResource("KillPlayers", "You wish to Kill the selected Player(s)?");
                resXresourceWriter.AddResource("ToggleAdmin", "You wish to Toggle Admin on the selected Player(s)?");
                resXresourceWriter.AddResource("GuiCheckingUpdates", "Checking for updates...");
                resXresourceWriter.AddResource("GuiRunningLatest", "You are running the latest version!");
                resXresourceWriter.AddResource("GuiNewVersion", "A new version has been detected: {0}\r\n\r\n Release Information:\r\nRelease Name: {1}\r\nDownload Count: {2}\r\nRelease Published {3}\r\nRelease Description:\r\n\r\n{4}\r\n\r\nWould you like to update now?");
                resXresourceWriter.AddResource("GuiIRSEUpdater", "IRSE Updater");
                resXresourceWriter.AddResource("GuiCanceledUpdate", "The Update has been canceled.");
                resXresourceWriter.AddResource("GuiUpdateApply", "The Update is being applied..");
                resXresourceWriter.AddResource("GuiDialogMustRestart", "You must restart before the update can be completed!\r\n\r\nWould you like to restart now?\r\nNote: The server was saved after downloading this release.");
                resXresourceWriter.AddResource("GuiExtenderUpdater", "Extender Updater");
                resXresourceWriter.AddResource("GuiNeedsRestart", "IRSE needs to be restarted before you can use the new features!");
                resXresourceWriter.AddResource("RestartIRSE", "Restart IRSE?");
                resXresourceWriter.AddResource("Close IRSE", "Close IRSE?");
            }
        }
    }
}