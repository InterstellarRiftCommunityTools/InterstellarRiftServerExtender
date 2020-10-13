using Game.Framework;
using Game.Server;
using IRSE.Managers.Events;
using IRSE.Managers.Plugins;
using IRSE.Modules;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Logger = NLog.Logger;

namespace IRSE.Managers
{
    public class PluginManager
    {
        #region Fields

        private List<PluginInfo> m_discoveredPlugins;
        private List<PluginInfo> m_loadedPlugins;
        private readonly Object _lockObj = new Object();

        private static Logger mainLog = LogManager.GetCurrentClassLogger();

        #endregion Fields

        #region Properties

        public List<PluginInfo> LoadedPlugins { get { return m_loadedPlugins; } }

        #endregion Properties

        #region Methods

        public PluginManager()
        {
            m_discoveredPlugins = new List<PluginInfo>();
            m_loadedPlugins = new List<PluginInfo>();
        }

        public void InitializeAllPlugins()
        {
            if (m_discoveredPlugins.Count == 0)
                m_discoveredPlugins = FindAllPlugins();

            //mainLog.Warn(String.Format("Initializing {0} Plugins!", m_discoveredPlugins.Count));
            foreach (PluginInfo Plugin in m_discoveredPlugins)
            {
                InitializePlugin(Plugin);
            }
        }

        public void LoadAllPlugins()
        {
            m_discoveredPlugins = FindAllPlugins();

            //mainLog.Warn(String.Format("Loading {0} Plugins!", m_discoveredPlugins.Count));
            foreach (PluginInfo Plugin in m_discoveredPlugins)
            {                
                LoadPlugin(Plugin);
            }
        }

        public void LoadPlugin(PluginInfo Plugin)
        {
            mainLog.Warn(string.Format(Program.Localization.Sentences["LoadingPlugin"], Plugin.Assembly.GetName().Name));

            try
            {
                Plugin.MainClass = (PluginBase)Activator.CreateInstance(Plugin.MainClassType);
                Plugin.MainClass.OnLoad(Plugin.Directory);
            }
            catch (Exception ex)
            {
                mainLog.Error(string.Format(Program.Localization.Sentences["FailedLoadPlugin"], Plugin.Assembly.GetName().Name, ex.ToString()));
            }
            m_loadedPlugins.Add(Plugin);
        }

        public void InitializePlugin(PluginInfo Plugin)
        {
            if (Plugin == null) return;
            mainLog.Warn(string.Format(Program.Localization.Sentences["InitializingPlugin"], Plugin.Assembly.GetName().Name));
            bool PluginInitialized = false;

            try
            {
                if (Plugin.MainClass == null)
                    Plugin.MainClass = (PluginBase)Activator.CreateInstance(Plugin.MainClassType);

                if (Plugin.MainClass != null)
                {
                    try
                    {
                        SetupTypes(Plugin);
                        InitPluginCommands(Plugin);

                        Plugin.MainClass.Init();

                        PluginInitialized = true;
                    }
                    catch (MissingMethodException)
                    {
                        mainLog.Error(string.Format(Program.Localization.Sentences["InitializationPlugin"], Plugin.Assembly.GetName().Name, Plugin.MainClassType.ToString()));
                    }
                    catch (Exception ex)
                    {
                        mainLog.Error(string.Format(Program.Localization.Sentences["FailedInitPlugin"], Plugin.Assembly.GetName().Name, ex.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                mainLog.Warn(string.Format(Program.Localization.Sentences["FailedInitPlugin"], Plugin.Assembly.GetName().Name, ex.ToString()));
            }
            m_loadedPlugins.Find(p => p.MainClass.Enabled == PluginInitialized);
            
        }

        public void ShutdownAllPlugins()
        {
            List<PluginInfo> loadedPlugins;
            lock (_lockObj)
            {
                loadedPlugins = new List<PluginInfo>(m_loadedPlugins);
            }
            foreach (PluginInfo Plugin in loadedPlugins)
            {
                ShutdownPlugin(Plugin);
            }
        }

        public void ShutdownPlugin(PluginInfo Plugin)
        {
            mainLog.Warn(string.Format(Program.Localization.Sentences["ShutdownPlugin"], Plugin.Name));
            lock (_lockObj)
            {
                try
                {
                    //BUG HUGE!!!!!
                    if (Plugin.MainClass != null)
                    {
                        Plugin.MainClass.DisablePlugin();
                    }
                    Plugin.MainClass = null;
                }
                catch (Exception ex)
                {

                    mainLog.Error(string.Format(Program.Localization.Sentences["ShutdownPlugin"], Plugin.Assembly.GetName().Name, ex.ToString()));
                }
                m_loadedPlugins.Remove(Plugin);
                m_discoveredPlugins.Remove(Plugin);
            }
        }

        public void ShutdownPlugin(String Plugin)
        {
            lock (_lockObj)
            {
                foreach (PluginInfo Plugininfo in m_discoveredPlugins)
                {
                    PluginBase pb = Plugininfo.MainClass;
                    if (pb == null)
                    {
                        mainLog.Warn("Error 131!");
                        return;
                    }
                    if (pb.GetName.ToLower() == Plugin)
                    {                                                     
                        mainLog.Warn(String.Format(Program.Localization.Sentences["ShutdownPlugin"], Plugininfo.Assembly.GetName().Name));

                        pb.DisablePlugin(false);
                        m_loadedPlugins.Remove(Plugininfo);
                        m_discoveredPlugins.Remove(Plugininfo);
                        return;
                    }
                }
            }
        }

        public List<Assembly> LoadPluginReferences(string pluginFolder)
        {
            List<Assembly> pluginReferences = new List<Assembly>();

            try
            {
                string[] subDirectories = Directory.GetDirectories(pluginFolder);
                foreach (string path in subDirectories)
                {
                    string[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        try
                        {
                            if (IsValidAssembly(file))
                            {
                                Assembly pluginreference = Assembly.LoadFrom(file);
                                pluginReferences.Add(pluginreference);
                            }
                            else
                            {
                                mainLog.Warn($"WARNING: '{Path.GetFileName(file)}' is not valid for plugin '{Path.GetDirectoryName(pluginFolder)}' Reference will not be loaded.");
                            }
                        }
                        catch (Exception ex)
                        {
                            mainLog.Warn($"Failed to load plugin reference assembly '{Path.GetFileName(file)}' for plugin '{Path.GetDirectoryName(pluginFolder)}' Error: " + ex.ToString());
                        }
                    }
                }
                return pluginReferences;
            }
            catch (Exception ex)
            {
                mainLog.Warn($"Failed to load plugin references for {Path.GetDirectoryName(pluginFolder)} Error: " + ex.ToString());
            }

            return null;
        }

        public List<PluginInfo> FindAllPlugins()
        {
            List<PluginInfo> foundPlugins = new List<PluginInfo>();
            //TODO create Plugin Folder if it does not exist
            String modPath = Path.Combine(FolderStructure.IRSEFolderPath, "plugins");
            String[] subDirectories = Directory.GetDirectories(modPath);

            foreach (String subDirectory in subDirectories)
            {
                LoadPluginReferences(subDirectory);
                PluginInfo[] Plugins = FindPlugin(subDirectory);

                if (Plugins.Length > 0) foundPlugins.AddRange(Plugins);
            }

            m_discoveredPlugins = foundPlugins;

            return foundPlugins;
        }

        public PluginInfo[] FindPlugin(String directory)
        {
            List<PluginInfo> found = new List<PluginInfo>();
            String[] libraries = Directory.GetFiles(directory, "*.Plugin.dll");

            foreach (String library in libraries)
            {
                PluginInfo Plugin = ValidatePlugin(library);
                if (Plugin != null)
                {
                    Plugin.Directory = directory;
                    found.Add(Plugin);
                }
            }
            return found.ToArray();
        }

        private void SetupTypes(PluginInfo plugin)
        {
            //Loads Events
            foreach (MethodInfo method in plugin.MainClassType.GetMethods())
            {
                foreach (Attribute attribute in method.GetCustomAttributes(true))
                {
                    if (attribute is IRSEEventAttribute)
                    {
                        IRSEEventAttribute hea = attribute as IRSEEventAttribute;

                        HandleEvent(method, plugin, hea.EventType);
                    }
                }
            }
        }

        internal void SetPluginDirectory(PluginInfo pluginInfo, string directory)
        {
            pluginInfo.MainClass.Directory = directory;
        }

        private PluginInfo ValidatePlugin(String library)
        {
            byte[] bytes;
            Assembly libraryAssembly;
            try
            {
                bytes = File.ReadAllBytes(library);
                libraryAssembly = Assembly.Load(bytes);

                PluginInfo plugin = new PluginInfo();
                plugin.Assembly = libraryAssembly;
                plugin.Directory = Path.GetDirectoryName(library);


                Type[] PluginTypes = libraryAssembly.GetExportedTypes();

                foreach (Type PluginType in PluginTypes)
                {
                    if (PluginType.GetInterface(typeof(IPlugin).FullName) != null)
                    {
                        //mainLog.Warn("Loading Plugin Located at " + library);

                        PluginAttribute attr = PluginType.GetCustomAttribute<PluginAttribute>();
                    
                        plugin.MainClassType = PluginType;
                        plugin.Name = attr.Name;
                        continue;
                    }
                }

                return plugin;
            }
            catch (Exception ex)
            {
                mainLog.Error("Failed to load assembly: " + library + " Error: " + ex.ToString());
            }
            return null;
        }

        public PluginInfo HandleEvent(MethodInfo method, PluginInfo plugin, Type type)
        {
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length <= 0)
            {
                mainLog.Error("Parameter had no length! Method Name: " + method.Name);
                return plugin;
            }
            if (parameters[0].ParameterType.BaseType != typeof(Event))
            {
                mainLog.Error("INVALID Function Format! Event Expected but got " + parameters[0].Name);
                return plugin;
            }

            var listener = new EventListener(method, plugin.MainClassType, type);

            EventManager.Instance.Events[type].Add(listener);

           // mainLog.Info("Found Event Function : " + parameters[0].ParameterType.Name + " For EventType : " + type.Name);
            return plugin;
        }

        // Utility method for loading plugin references
        public bool IsValidAssembly(string path)
        {
            try
            {
                var assembly = AssemblyName.GetAssemblyName(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void InitPluginCommands(PluginInfo Plugin)
        {
            ///Permission.Admin

            foreach (MethodInfo method in Plugin.MainClassType.GetMethods(BindingFlags.Static | BindingFlags.Public))
            {
                object[] customAttributes1 = method.GetCustomAttributes(typeof(SvCommandMethod), false);
                if (((IEnumerable<object>)customAttributes1).Count<object>() != 0)
                {
                    object[] customAttributes2 = null;
                    SvCommandMethod svCommandMethod = customAttributes1[0] as SvCommandMethod;
                    EventHandler<List<string>> handler = (EventHandler<List<string>>)Delegate.CreateDelegate(typeof(EventHandler<List<string>>), method);
                    CommandSystem.Singleton.AddCommand(new Command(svCommandMethod.Names, svCommandMethod.Description, svCommandMethod.Arguments, handler, svCommandMethod.RequiredRight, customAttributes2 != null && ((IEnumerable<object>)customAttributes2).Any<object>(), method.GetCustomAttribute<TalkCommandAttribute>() != null), true);
                }
            }
            //CommandSystem.Singleton.SortCommands();
        }

        #endregion Methods
    }
}