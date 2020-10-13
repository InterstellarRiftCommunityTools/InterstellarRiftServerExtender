using Game.Server;
using NLog;
using System;
using System.Xml;

namespace IRSE.Managers.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        #region Fields

        protected static String m_directory;
        protected ControllerManager m_controllers;
        protected String m_version;
        protected String m_desc;
        protected String m_author;
        protected String m_name;
        protected String m_api;
        protected String[] m_aillias;
        protected Guid m_id;
        protected PluginHelper m_plugin_helper;
        protected Boolean isenabled = false;

        protected Logger m_log;
        protected PluginBaseConfig m_config;

        #endregion Fields


        #region Properties

        public virtual Boolean Enabled { get { return isenabled; } internal set { isenabled = value; } }
        public virtual Guid Id { get { return m_id; } internal set { m_id = value; } }
        public virtual String GetName { get { return m_name; } internal set { m_name = value; } }
        public virtual String Version { get { return m_version; } internal set { m_version = value; } }
        public virtual String Description { get { return m_desc; } internal set { m_desc = value; } }
        public virtual String Author { get { return m_author; } internal set { m_author = value; } }
        public virtual String Directory { get { return m_directory; } internal set { m_directory = value; } }
        public virtual String API { get { return m_api; } internal set { m_api = value; } }
        public virtual String[] Aillias { get { return m_aillias; } internal set { m_aillias = value; } }
        public virtual ControllerManager GetControllers => ServerInstance.Instance.Handlers.ControllerManager;

        public virtual PluginHelper GetPluginHelper { get { return m_plugin_helper; } }

        public virtual Logger PluginLog { get { return m_log; } }

        public virtual Object Settings { get; set; }


        public Logger GetLogger { get { return LogManager.GetCurrentClassLogger(); ; } }

        #endregion Properties

        #region Methods

        public PluginBase()
        {


        }

        public virtual void OnLoad(string directory)
        {
            m_log = NLog.LogManager.GetCurrentClassLogger();
            Directory = directory;
        }

        public virtual void Init()
        {
            m_plugin_helper = new PluginHelper(GetControllers);


            if (!ServerInstance.Instance.IsRunning)
            {
                //ERROR! No Server Running!
                Console.WriteLine("No Running Server found!");
                OnDisable();
            }

            PluginAttribute pluginAttribute = Attribute.GetCustomAttribute(GetType(), typeof(PluginAttribute), true) as PluginAttribute;
            if (pluginAttribute != null)
            {
                GetName = pluginAttribute.Name;
                Version = pluginAttribute.Version;
                Description = pluginAttribute.Description;
                Author = pluginAttribute.Author;
                Enabled = true;
                OnEnable();
            }
            else
            {
                Enabled = false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Plugin Invalid! No Plugin Attribute Found!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Plugin Loaded!");
            //#TODO add Logger
        }



        public virtual void Shutdown()
        {

        }



        public void DisablePlugin(bool remove = true)
        {
            Enabled = false;         
            OnDisable();
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }

        #endregion
    }

}
