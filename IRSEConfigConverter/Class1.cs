using Game.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace IRSEConfigConverter
{
    public class ServerConfigConverter
    {
        public class Result
        {
            public bool NeedsDialog;
            public bool Completed;
            public int Count; 
            public string Message = null;

            public Result(bool completed, string message = "")
            {
                NeedsDialog = false;
                Completed = completed;
                Message = message;
        }
        }


        #region Fields

        private static ServerConfigConverter m_instance;

        public static ServerConfigConverter Instance => m_instance == null ? m_instance = new ServerConfigConverter() : m_instance;

        #endregion Fields

        public ServerConfigConverter()
        {
        }

        #region Methods

        public Result BuildConfig(string outPath)
        {
           
            if (String.IsNullOrEmpty(outPath)) return new Result(false, "Path not set! Set a path first!");

            // get all the public properties from the class
            ServerConfig serverConfig = new ServerConfig();

            FieldInfo[] fieldInfos = serverConfig.GetType().GetFields();

            string scriptTop =
            $"//gv{IRSE.Program.ForGameVersion}\r\n" +
            $"//ev{IRSE.Program.VersionString}\r\n" +
            "// This file was generated with ServerConfigConverter class\r\n" +
            "// To allow a PropertyGrid to use IRs fields as properties.\r\n" +
            "using System;\r\n" +
            "using System.ComponentModel;\r\n" +
            "using System.Drawing;\r\n" +
            "using System.Drawing.Design;\r\n" +
            "using Game.Configuration;\r\n" +
            "namespace IRSE.Modules.GameConfig\r\n" +
            "{\r\n" +
            "    public class ServerConfigProperties\r\n" +
            "    {\r\n" +
            "        private static ServerConfigProperties m_instance; public static ServerConfigProperties Instance => m_instance == null ? m_instance = new ServerConfigProperties() : m_instance;\r\n" +
            "        public ServerConfigProperties(){}\r\n" +
            "        #region Manual Properties\r\n" +
            "        [Category(\"Welcome message\")]\r\n" +
            "        [Description(\"The color of the title of the welcome popup people will see when they connect to the server\")]\r\n" +
            "        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]\r\n" +
            "        public Color ServerWelcomePopupMessageTitleColor\r\n" +
            "        {\r\n" +
            "            get\r\n" +
            "            {\r\n" +
            "                uint[] color = ServerConfig.Singleton.ServerWelcomePopupMessageTitleColor;\r\n" +
            "                return Color.FromArgb(0, (int)color[0], (int)color[1], (int)color[2]);\r\n" +
            "            }\r\n" +
            "            set\r\n" +
            "            {\r\n" +
            "                uint[] color = new uint[] { value.R, value.G, value.B };\r\n" +
            "                ServerConfig.Singleton.ServerWelcomePopupMessageTitleColor = color;\r\n" +
            "            }\r\n" +
            "        }\r\n" +
            "        [Category(\"Miscellaneous\")]\r\n" +
            "        [Description(\"Sets the global trade stats of resources.\")]\r\n" +
            "        public static System.Collections.Generic.Dictionary<Game.ClientServer.Classes.Economics.ResourceTypes, Game.ClientServer.Classes.Economics.ResourcePriceStats> OverwriteResourcePriceStats\r\n" +
            "        {\r\n" +
            "            get { return ServerConfig.Singleton.OverwriteResourcePriceStats; }\r\n" +
            "            set { ServerConfig.Singleton.OverwriteResourcePriceStats = value; }\r\n" +
            "        }\r\n" +
            "        [Category(\"Miscellaneous\")]\r\n" +
            "        [Description(\"Sets the global trade stats of tools.\")]\r\n" +
            "        public static System.Collections.Generic.Dictionary<Game.ClientServer.Classes.Economics.ToolTypes, Game.ClientServer.Classes.Economics.ToolPriceStats> OverwriteToolPriceStats\r\n" +
            "        { get { return ServerConfig.Singleton.OverwriteToolPriceStats; } set { ServerConfig.Singleton.OverwriteToolPriceStats = value; } }\r\n" +
            "        [Category(\"Miscellaneous\")]\r\n" +
            "        [Description(\"Sets custom settings for specific systems.\")]\r\n" +
            "        public static System.Collections.Generic.Dictionary<System.String, Game.Universe.SystemSettings> SpecificSystemSettings\r\n" +
            "        {\r\n" +
            "            get { return ServerConfig.Singleton.SpecificSystemSettings; }\r\n" +
            "            set { ServerConfig.Singleton.SpecificSystemSettings = value; }\r\n" +
            "        }\r\n" +
            "        [Category(\"Starter Ships\")]\r\n" +
            "        [Description(\"Specifics of starter ships.\")]\r\n" +
            "        public static System.Collections.Generic.Dictionary<Game.ClientServer.Classes.FactionTypes, System.Collections.Generic.List<Game.ClientServer.Classes.FactionStarterShipDetails>> FactionStarterShips\r\n" +
            "        {\r\n" +
            "            get { return ServerConfig.Singleton.FactionStarterShips; }\r\n" +
            "            set { ServerConfig.Singleton.FactionStarterShips = value; }\r\n" +
            "        }\r\n" +
            "        #endregion\r\n" +
            "        //---<STARTGEN>---";

            string code = "";

            Result result = new Result(true);


            foreach (FieldInfo field in fieldInfos)
            {
                

                if (field == null)
                    continue;

                ConfigOptionAttribute attribute = field.GetCustomAttribute<ConfigOptionAttribute>();
                if (attribute == null)
                    continue;

                string name = field.Name;
                string category = attribute.GetCategoryName();
                string description = Game.Configuration.Localization.Singleton.GetString("ServerConfigOptions", attribute.Description);
                string type = field.FieldType.ToString();

                dynamic value = field.GetValue(serverConfig);

                string commentOut = "";
                if (type.Contains("Dictionary") || type.Contains("List") || type.Contains("UInt32[]"))
                {
                    result.Message += $"Manual Edit Item. Name: {name}, Commenting Out Lines\r\n";
                    commentOut = @"//";
                    result.NeedsDialog = true;
                }

                code +=
                    "\n" +
                    $"      {commentOut}[DisplayName(\"{AddSpacesToSentence(name, true)}\")]\n" +
                    $"      {commentOut}[Category(\"{category}\")]\n" +
                    $"      {commentOut}[Description(\"{description}\")]\n" +
                    $"      {commentOut}public {type} {name} \n" +
                    $"      {commentOut}{{get{{return ServerConfig.Singleton.{name};}} set {{ServerConfig.Singleton.{name} = value;}}}}\n" +
                    "\n";
            }

            string scriptBottom =
                "      //---<ENDGEN>---\n" +
                "  }\n" +
                "}\n" +
                "\n";



            Directory.CreateDirectory(outPath);
            File.WriteAllText(Path.Combine(outPath, "ServerConfigProperties.cs"), scriptTop + code + scriptBottom);

            result.Count += 1;


            return result;
        }

        private string AddSpacesToSentence(string text, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        #endregion Methods
    }
}
