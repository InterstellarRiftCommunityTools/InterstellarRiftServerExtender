using System;
using System.IO;

namespace IRSE
{
    #region Server Name Enums

    public enum ServerFolderName
    {
        Base,
        Data
    }

    /// <summary>
    /// Server File Name Enums
    /// </summary>
    public enum ServerFileName
    {
        IRConfig
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
        Config
    }

    #endregion IRSE Name Enums

    public static class Globals
    {
        #region Server Folder Name Fields

        public static readonly string ServerBaseFolderName = "";
        public static readonly string ServerDataFolderName = "%APPDATA%/InterstellarRift";

        #endregion Server Folder Name Fields

        #region Server File Name Fields

        public static readonly string IRConfigFileName = "server.json";

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

        public static string GetFilePath(ServerFileName serverFileName, bool fullPath = true)
        {
            string file = "";
            switch (serverFileName)
            {
                case ServerFileName.IRConfig:
                    file = Path.Combine(GetFolderPath(ServerFolderName.Base, fullPath), IRConfigFileName);
                    break;
            }
            return file;
        }

        public static string GetFilePath(IRSEFileName IRSEFileName, bool fullPath = true)
        {
            string file = "";
            switch (IRSEFileName)
            {
                case IRSEFileName.Config:
                    file = Path.Combine(GetFolderPath(IRSEFolderName.Config, fullPath), IRSEConfigFileName);
                    break;

                case IRSEFileName.NLogConfig:
                    file = Path.Combine(GetFolderPath(IRSEFolderName.Config, fullPath), NLogConfigFileName);
                    break;
            }
            return file;
        }

        #endregion FilePath Methods

        #region FolderPath Methods

        public static string GetFolderPath(ServerFolderName serverFolderName, bool fullPath = false)
        {

            string path = "";
            switch (serverFolderName)
            {
                case ServerFolderName.Base:
                    path = ServerBaseFolderName;
                    break;

                case ServerFolderName.Data:
                    path = Path.Combine(IRSERootFolderName, ServerDataFolderName);
                    break;
            }

            return fullPath ? Path.Combine(ServerBaseFolderName, path) : path;
        }

        public static string GetFolderPath(IRSEFolderName IRSEFolder, bool fullPath = false)
        {
            string fullBasePath = Environment.CurrentDirectory;

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
            return fullPath ? Path.Combine(fullBasePath, path) : path;
        }

        #endregion FolderPath Methods
    }
}