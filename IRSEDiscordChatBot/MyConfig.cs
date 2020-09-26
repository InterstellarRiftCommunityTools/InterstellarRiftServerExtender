using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace IRSEDiscordChatBot
{
    [Serializable]
    public class Settings
    {

        [Description("The Discord Token that the bot will use.")]
        public string DiscordToken;

        [Description("The Client ID of your Discord Bot")]
        public ulong BotClientID;

        [Description("The Main Channel ID That the bot will be in.")]
        public ulong MainChannelID;

        [Description("The Main Channel ID ")]
        public List<string> AdditionalChannelIDList;


        public bool PrintDiscordLogToConsole { get; set; }

        public bool PrintDiscordChatToConsole { get; set; }

        public string PlayerSpawningMessage { get; set; }

        public string PlayerRespawningMessage { get; set; }

        public string PlayerLeavingMessage { get; set; }

        public string MessageSentToDiscord { get; set; }

        public string MessageSentToGameServer { get; set; }

        public string NewPlayerSpawningMessage { get; set; }

        public bool DebugMode { get; set; }


        public Settings()
        {

            DiscordToken = "discordtokenhere";
            BotClientID = 0;
            MainChannelID = 0;
            DebugMode = true;
            PrintDiscordLogToConsole = true;
            PrintDiscordChatToConsole = true;
            PlayerSpawningMessage = $"(%PlayerName%) connected to the game server at (%CurrentDateTime%)";
            PlayerRespawningMessage = $"(%PlayerName%) has respawned at (% CurrentDateTime %)";
            NewPlayerSpawningMessage = $"A new Player has connected! Welcome (%PlayerName%)!";
            PlayerLeavingMessage = $"(%PlayerName%) has left the game server at (%CurrentDateTime%)";
            MessageSentToDiscord = $"IRSE Server - (%PlayerName%): (%ChatMessage%) ";
            MessageSentToGameServer = $"Discord [(%DiscordChannelName%)]- (%DiscordUserName%): (%ChatMessage%)";

        }
    }

    /// <summary>
    /// Using XML configurations as its easy to add comments into the configuration file.
    /// </summary>
    public class MyConfig
    {
        public static string FileName;

        
        public static MyConfig Instance;

        private Settings _settings;
        public Settings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
            }
        }


        public MyConfig()
        {
            Instance = this;
            _settings = new Settings();
            LoadConfiguration();
        }

        public bool SaveConfiguration()
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    var writer = new StreamWriter(ms);
                    var serializer = new XmlSerializer(typeof(Settings));
                    serializer.Serialize(writer, _settings);
                    writer.Flush();

                    File.WriteAllBytes(FileName, ms.ToArray());                   
                }

                WriteComments();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSEDiscordChatBot:  Configuration Save Failed! (SaveConfiguration):" + ex.ToString());
            }
            return false;
        }

        public bool LoadConfiguration()
        {
            if (!File.Exists(FileName))
            {
                Console.WriteLine("IRSEDiscordChatBot:  Config.xml does not exist, creating one from defaults.");
                SaveConfiguration();              
                return true;
            }

            try
            {
                var serializer = new XmlSerializer(typeof(Settings));

                using (Stream fs = new FileStream(FileName, FileMode.Open))
                using (XmlReader reader = new XmlTextReader(fs))
                {
                    if (!serializer.CanDeserialize(reader))
                    {
                        Console.WriteLine("IRSEDiscordChatBot:  Could not deserialize IRSE's Configuration File! Load Failed!");
                        return false;
                    }

                    _settings = (Settings)serializer.Deserialize(reader);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSEDiscordChatBot:  Configuration Load Failed! (LoadConfiguration):" + ex.ToString());
            }
            return false;
        }

        private void WriteComments()
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(FileName);

                var parent = doc.SelectSingleNode(typeof(Settings).Name);
                if (parent == null) return;

                foreach (XmlNode child in parent.ChildNodes)
                {
                    PropertyInfo property = (typeof(Settings).GetProperty(child.Name));

                    if (property != null)
                    {
                        XmlNode nameNode = null;
                        if (Attribute.IsDefined(property, typeof(DisplayNameAttribute)))
                        {
                            DisplayNameAttribute name = (DisplayNameAttribute)property.GetCustomAttribute(typeof(DisplayNameAttribute));
                            nameNode = parent.InsertBefore(doc.CreateComment(name.DisplayName), child);
                        }

                        if (Attribute.IsDefined(property, typeof(DescriptionAttribute)))
                        {
                            DescriptionAttribute description = (DescriptionAttribute)property.GetCustomAttribute(typeof(DescriptionAttribute));

                            parent.InsertBefore(doc.CreateComment(description.Description), child);
                        }
                    }
                }
                doc.Save(FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSEDiscordChatBot:  Configuration Save Failed! (WriteComments)" + ex.ToString());
            }
        }
    }
}