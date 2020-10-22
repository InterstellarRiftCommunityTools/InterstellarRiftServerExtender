using IRSE.Modules;
using System;
using System.IO;

namespace IRSE
{
    #region Server Name Enums

    public enum ServerFolderName
    {
        Base,
        AppData,
        Backups,
        Dumps,
        Logs,
        Server,
        UserDB,
        Workshop
    }

    /// <summary>
    /// Server File Name Enums
    /// </summary>
    public enum ServerFileName
    {
        IRConfig,
        GameDB,
    }

    #endregion Server Name Enums

    #region IRSE Name Enums

    public enum IRSEFolderName
    {
        IRSE,
        Bin,
        Config,
        Localization,
        Logs,
        Plugins,
        Updates
    }

    /// <summary>
    /// IRSE File Name Enums
    /// </summary>
    public enum IRSEFileName
    {
        NLogConfig,
        Config,
        Exe
    }

    #endregion IRSE Name Enums

    public static class ExtenderGlobals
    {
        #region Server Folder Name Fields

        public static readonly string ServerBaseFolder = "InterstellarRift";
        public static readonly string ServerDataFolderName = "InterstellarRift";
        public static readonly string ServerBackupsFolderName = "backups";
        public static readonly string ServerDumpsFolderName = "Dumps";
        public static readonly string ServerLogsFolderName = "Logs";
        public static readonly string ServerServerFolderName = "server";
        public static readonly string ServerUserDBFolderName = "userdb";
        public static readonly string ServerWorkshopFolderName = "workshop";

        #endregion Server Folder Name Fields

        #region Server File Name Fields

        public static readonly string IRConfigFileName = "server.json";
        public static readonly string IRGameDBFileName = "game.db";

        #endregion Server File Name Fields

        #region IRSE File Name Fields

        public static readonly string IRSEConfigFileName = "Config.cfg";
        public static readonly string NLogConfigFileName = "NLog.config";

        #endregion IRSE File Name Fields

        #region IRSE Folder Names Fields

        public static readonly string IRSERootFolderName = "IRSE";
        public static readonly string IRSEBinariesFolderName = "bin";
        public static readonly string IRSEConfigFolderName = "config";
        public static readonly string IRSELocalizationFolderName = "localization";
        public static readonly string IRSELogsFolderName = "logs";
        public static readonly string IRSEPluginsFolderName = "plugins";
        public static readonly string IRSEUpdatesFolderName = "updates";

        #endregion IRSE Folder Names Fields

        #region FilePath Methods

        public static string GetFilePath(ServerFileName serverFileName)
        {
            string file = "";
            switch (serverFileName)
            {
                case ServerFileName.GameDB:
                    file = Path.Combine(GetFolderPath(ServerFolderName.AppData), IRGameDBFileName);
                    break;

                case ServerFileName.IRConfig:
                    file = Path.Combine(GetFolderPath(ServerFolderName.AppData), IRConfigFileName);
                    break;
            }
            return file;
        }

        public static string GetFilePath(IRSEFileName IRSEFileName)
        {
            string file = "";
            switch (IRSEFileName)
            {
                case IRSEFileName.Config:
                    file = Path.Combine(GetFolderPath(IRSEFolderName.Config), IRSEConfigFileName);
                    break;

                case IRSEFileName.NLogConfig:
                    file = Path.Combine(GetFolderPath(IRSEFolderName.Config), NLogConfigFileName);
                    break;
            }
            return file;
        }

        #endregion FilePath Methods

        #region FolderPath Methods

        public static string GetFolderPath(ServerFolderName serverFolderName)
        {
            string path = "";
            switch (serverFolderName)
            {
                case ServerFolderName.Base:
                    path = Path.Combine(Path.GetDirectoryName(Program.EntryAssembly.Location));
                    break;

                case ServerFolderName.AppData:
                    path = Path.Combine(ServerBaseFolder, ServerDataFolderName);
                    break;

                case ServerFolderName.Backups:
                    path = Path.Combine(ServerBaseFolder, ServerBackupsFolderName);
                    break;

                case ServerFolderName.Dumps:
                    path = Path.Combine(ServerBaseFolder, ServerDumpsFolderName);
                    break;

                case ServerFolderName.Logs:
                    path = Path.Combine(ServerBaseFolder, ServerLogsFolderName);
                    break;

                case ServerFolderName.Server:
                    path = Path.Combine(ServerBaseFolder, ServerServerFolderName);
                    break;

                case ServerFolderName.UserDB:
                    path = Path.Combine(ServerBaseFolder, ServerUserDBFolderName);
                    break;

                case ServerFolderName.Workshop:
                    path = Path.Combine(ServerBaseFolder, ServerWorkshopFolderName);
                    break;

                default:
                    break;
            }
            return path;
        }

        public static string GetFolderPath(IRSEFolderName IRSEFolder)
        {
            string path = "";
            switch (IRSEFolder)
            {
                case IRSEFolderName.IRSE:
                    path = IRSERootFolderName;
                    break;

                case IRSEFolderName.Bin:
                    path = Path.Combine(IRSERootFolderName, IRSEBinariesFolderName);
                    break;

                case IRSEFolderName.Config:
                    path = Path.Combine(IRSERootFolderName, IRSEConfigFolderName);
                    break;

                case IRSEFolderName.Localization:
                    path = Path.Combine(IRSERootFolderName, IRSELocalizationFolderName);
                    break;

                case IRSEFolderName.Logs:
                    path = Path.Combine(IRSERootFolderName, IRSELogsFolderName);
                    break;

                case IRSEFolderName.Plugins:
                    path = Path.Combine(IRSERootFolderName, IRSEPluginsFolderName);
                    break;

                case IRSEFolderName.Updates:
                    path = Path.Combine(IRSERootFolderName, IRSEUpdatesFolderName);
                    break;
            }
            return Path.Combine(FolderStructure.RootFolderPath, path);
        }

        #endregion FolderPath Methods
    }
}