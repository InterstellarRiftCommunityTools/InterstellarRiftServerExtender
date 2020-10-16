using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace IRSE.Modules
{


    [System.Runtime.Serialization.DataContract]
    public class Settings
    {
        private static NLog.Logger mainLog; //mainLog.Error

        internal bool RestartNeeded = false;

        public Settings()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();

            //Defaults
            AutoStartEnable = false;
            DebugMode = false;
            AutoRestartsEnable = false;
            AutoRestartTime = 0;
            AnnounceRestartTime = true;
            EnableAutomaticUpdates = true;
            EnableDevelopmentVersion = false;
            EnableAutomaticUpdates = true;
            CheckUpdatesTime = 60;

            manageSteamCmd = true;
            m_currentLanguage = "English (United States)";

            usePreReleaseVersions = EnableDevelopmentVersion;
        }


        private string m_currentLanguage;
        
        [System.Runtime.Serialization.DataMember]
        [Browsable(false)]
        [Description("The selected language for IRSE to use.")]

        [DisplayName("Selected Language - (Default: English )")]
        public string CurrentLanguage
        {
            get => m_currentLanguage;
            set
            {
                if (m_currentLanguage != value)
                    RestartNeeded = true;

                m_currentLanguage = value;
            }
        }

        [System.Runtime.Serialization.DataMember]
        [Category("Development")]
        [DisplayName("Debug Mode - (Default: False )")]
        [Description("Enable Verbose printing to the console.")]
        public bool DebugMode { get; set; }

        [System.Runtime.Serialization.DataMember]
        [Category("Main")]
        [DisplayName("Start Server On Load - (Default: False )")]
        [Description("Starts the Hellion server on IRSE load.\r\n" +
            "If enabled, Hellion Dedicated will automatically start after IRSE initializes")]
        public bool AutoStartEnable { get; set; }

        [System.Runtime.Serialization.DataMember]
        [Category("Updates")]
        [DisplayName("Enable Automatic Restarts - (Default: False )")]
        [Description("Allow IRSE to Restart itself after a set time elapses.\r\n" +
                    "Used for automatic restarts and releasing IRSE's resources after a set time.")]
        public bool AutoRestartsEnable { get; set; }

        [System.Runtime.Serialization.DataMember]
        [ReadOnly(true)]
        [Category("Updates")]
        [DisplayName("Automatic Restart Time - (Default: 0 )")]
        [Description("Auto-Restart IRSE After a set time in minutes.\r\n" +
            "Used for automatic updates and releasing IRSE's resources after a set time.")]
        public float AutoRestartTime { get; set; }

        [System.Runtime.Serialization.DataMember]
        [ReadOnly(true)]
        [Category("Updates")]
        [DisplayName("Announce Restart Time - (Default: True )")]
        [Description("Announce restart time to players on the server.\r\n" +
            "Used for automatic updates and releasing IRSE's resources after a set time.")]
        public bool AnnounceRestartTime { get; set; }

        [System.Runtime.Serialization.DataMember]
        [Category("Updates")]
        [DisplayName("Enable Automatic Updates - (Default: True )")]
        [Description("Allow IRSE to update itself.\r\n" +
            "Used for automatic updates and releasing IRSE's resources after a set time.")]
        public bool EnableExtenderAutomaticUpdates { get; set; }

        private bool usePreReleaseVersions = false;

        [System.Runtime.Serialization.DataMember]
        [Category("Development")]
        [DisplayName("Enable Development Version (Restart Required)- (Default: false )")]
        [Description("Change to true if you would like to use Development versions (I.E. PreReleases).\r\n" +
            "Used for automatic updates and releasing IRSE's resources after a set time.")]
        public bool EnableDevelopmentVersion
        {
            get => usePreReleaseVersions;
            set
            {
                if (usePreReleaseVersions != value)
                    RestartNeeded = true;

                usePreReleaseVersions = value;
            }
        }

        [System.Runtime.Serialization.DataMember]
        [Category("Updates")]
        [DisplayName("Enable IR Automatic Updates - (Default: True )")]
        [Description("Allows IRSE to update ISR Dedicated Server.\r\n" +
            "Used for automatic updates and releasing IRSE's resources after a set time.")]
        public bool EnableAutomaticUpdates { get; set; }

        [System.Runtime.Serialization.DataMember]
        [ReadOnly(true)]
        [Category("Updates")]
        [DisplayName("Check Updates Time - (Default: 60 )")]
        [Description("How often should IRSE check for updates, in minutes.\r\n" +
            "Used for automatic updates and releasing IRSE's resources after a set time.")]
        public int CheckUpdatesTime { get; set; }

        private bool manageSteamCmd;

        [System.Runtime.Serialization.DataMember]
        [Category("Steam CMD")]
        [DisplayName("Manage Steam CMD - (Restart Required)")]
        [Description("Let IRSE manage Interstellar Rift installations.")]
        public bool ManageSteamCMD
        {
            get
            {
                return manageSteamCmd;
            }
            set
            {
                //if (manageSteamCmd != value)
                    //RestartNeeded = true;

                manageSteamCmd = value;
            }
        }
    }

    /// <summary>
    /// Using XML configurations as its easy to add comments into the configuration file.
    /// </summary>
    public class Config
    {
        public static string FileName = Path.Combine(FolderStructure.IRSEFolderPath, "config/Config.xml");

        private Settings _settings;

        public static Config Instance;

        public Settings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                SaveConfiguration();
            }
        }

        public Config()
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

                /*
                if (_settings.RestartNeeded)
                {
                    DialogResult result = MessageBox.Show("The setting you have changed requires a restart of IRSE. Press 'Yes' to restart now. Or 'No' if you plan to restart later.",
                 "Restart Needed",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        Program.Restart();
                        return true;
                    }
                }
                */

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE:  Configuration Save Failed! (SaveConfiguration):" + ex.ToString());
            }
            return false;
        }

        public bool LoadConfiguration()
        {
            if (!File.Exists(FileName))
            {
                Console.WriteLine("IRSE:  IRSE Config does not exist, creating one from defaults.");
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
                        Console.WriteLine("IRSE:  Could not deserialize IRSE's Configuration File! Load Failed!");
                        return false;
                    }

                    _settings = (Settings)serializer.Deserialize(reader);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE:  Configuration Load Failed! (LoadConfiguration):" + ex.ToString());
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
                Console.WriteLine("IRSE:  Configuration Save Failed! (WriteComments)" + ex.ToString());
            }
        }
    }
}