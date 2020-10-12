using Game.Client;
using Game.Server;
using Game.Universe;
using Game.Universe.Net;
using IRSE.GUI.ObjectManipulator.Wrappers;
using IRSE.Managers;
using IRSE.Managers.Plugins;
using IRSE.Modules;
using IRSE.Modules.GameConfig;
using MarkdownDeep;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace IRSE.GUI.Forms
{
    public partial class ExtenderGui : Form
    {
        private Timer ObjectManipulationRefreshTimer = new Timer();
        private Timer PlayersRefreshTimer = new Timer();
        private Timer PluginsRefreshTimer = new Timer();
        private Timer GlobalKeyPressTimer = new Timer();

        private bool _isHoldingAlt = false;
        public ExtenderGui()
        {


            InitializeComponent();

            AddChatLine("Waiting for server to start..");

            DisableControls();

            ServerInstance.Instance.OnServerStarted += Instance_OnServerStarted;
            ServerInstance.Instance.OnServerStopped += Instance_OnServerStopped;
            ServerInstance.Instance.OnServerStarting += Instance_OnServerStarting;

            new ServerConfigProperties();
            serverconfig_properties.SelectedObject = ServerConfigProperties.Instance;
            extenderconfig_properties.SelectedObject = Config.Instance.Settings;


            extenderconfig_properties.Refresh();
            serverconfig_properties.Refresh();

            if (Config.Instance.Settings.EnableDevelopmentVersion)
            {
                development_label.Text = "WARNING: Development Versions have been enabled. Possibility of server corruption.";
            }

            UpdateManager.Instance.OnUpdateChecked += new UpdateManager.UpdateEventHandler(Instance_OnUpdateChecked);
            UpdateManager.Instance.OnUpdateDownloaded += new UpdateManager.UpdateEventHandler(Instance_OnUpdateDownloaded);
            UpdateManager.Instance.OnUpdateApplied += new UpdateManager.UpdateEventHandler(Instance_OnUpdateApplied);

            server_hesNewsLabel.Text =
                "Welcome to IRSE!\nIt's Almost Ready!!\n" +
                "Woot!";

            GlobalKeyPressTimer.Tick += GlobalKeyPressTimer_Tick;
            GlobalKeyPressTimer.Enabled = true;
            GlobalKeyPressTimer.Start();

            UpdatePluginTab();
        }

        private void Instance_OnServerStarting()
        {
            server_config_startserver.Enabled = false;
        }

        private void DisableControls(bool disable = true)
        {
            cpc_chat_list.ReadOnly = true;
            cpc_chat_list.BackColor = System.Drawing.SystemColors.Window;

            cpc_messagebox.Enabled = !disable;
            cpc_chat_send.Enabled = !disable;

            pc_banplayer.Enabled = !disable;
            pc_kickplayer.Enabled = !disable;

            sc_playerslist_listview.Enabled = !disable;

            server_config_stopserver.Enabled = !disable;
            server_config_startserver.Enabled = disable;

            //objectManipulation_grid.Enabled = !disable;
            objectManipulation_treeview.Enabled = !disable;

        }

        #region Events

        private void Instance_OnServerStarted()
        {

            Invoke(new MethodInvoker(delegate
            {
                AddChatLine("Server Online, Ready For Chat.");

                DisableControls(false);

                UpdatePlayersTree();
                UpdateChatPlayers();
                

                PlayersRefreshTimer.Enabled = true;
                PluginsRefreshTimer.Enabled = true;
                ObjectManipulationRefreshTimer.Enabled = true;

                //var test = new CommandVisualizer();
                //test.BuildLayout();

                //foreach (var name in test.Map)
                    //cv_tab_list.Items.Add(name.Key);


                ObjectManipulationRefreshTimer.Interval = (1000); // 1 secs
                ObjectManipulationRefreshTimer.Tick += delegate (object sender, EventArgs e)
                {
                    UpdatePlayersTree();
                    //UpdateGalaxyTree();

                    if (!_isHoldingAlt) 
                        objectManipulation_grid.Refresh();

                    objectManipulation_treeview.Refresh();
                };

                PlayersRefreshTimer.Interval = (1000); // 1 secs
                PlayersRefreshTimer.Tick += delegate (object sender, EventArgs e)
                {
                    UpdateChatPlayers();                    
                };

                PluginsRefreshTimer.Interval = (1000); // 1 secs
                PluginsRefreshTimer.Tick += delegate (object sender, EventArgs e)
                {
                    UpdatePluginTab();
                };

                this.Refresh();

            }));


        }

        private void Instance_OnServerStopped()
        {
            Invoke(new MethodInvoker(delegate
            {
                DisableControls();

                ObjectManipulationRefreshTimer.Stop();
                ObjectManipulationRefreshTimer.Enabled = false;

                PlayersRefreshTimer.Stop();
                PlayersRefreshTimer.Enabled = false;

                PluginsRefreshTimer.Stop();
                PluginsRefreshTimer.Enabled = false;

                this.Refresh();
            }));
        }

        public void UpdateChatPlayers()
        {
            try
            {
                if (!ServerInstance.Instance.IsRunning)
                    return;

                if (ServerInstance.Instance.Handlers.PlayerHandler == null)
                    return;

                List<Player> onlinePlayers = ServerInstance.Instance.Handlers.PlayerHandler.GetPlayers.ToList();

                sc_onlineplayers_label.Text = onlinePlayers.Count.ToString();

                foreach (Player player in onlinePlayers)
                {
                    if (player == null)
                        continue;

                    var item = new ListViewItem();
                    item.Name = player.ID.ToString();
                    item.Tag = player;
                    item.Text = $"{player.Name} ({player.ID})";

                    if (!sc_playerslist_listview.Items.ContainsKey(item.Name))
                        sc_playerslist_listview.Items.Add(item);

                }

                // chat players
                foreach (ListViewItem item in sc_playerslist_listview.Items)
                {
                    Player _player = item.Tag as Player;

                    if (!onlinePlayers.Contains(_player))
                    {
                        if (sc_playerslist_listview.Items.ContainsKey(item.Name))
                            sc_playerslist_listview.Items.RemoveByKey(item.Name);
                    }
                }         
            }
            catch (Exception)
            {
                // nope
            }
        }

        private void UpdatePluginTab()
        {

            try
            {
                List<PluginInfo> pluginsArray = ServerInstance.Instance.PluginManager.LoadedPlugins;

                if (pluginsArray.Count == 0)
                    return;

                foreach (PluginInfo plugin in pluginsArray)
                {
                    string name = plugin.Name;

                    ListViewItem item = new ListViewItem(new[] { name, "Enabled" })
                    {
                        Name = name,
                        Text = name,
                        Tag = plugin
                    };

                    if (!plugins_tab_pluginslist.Items.ContainsKey(item.Name))
                        plugins_tab_pluginslist.Items.Add(item);

                    if (plugin.Loaded)
                    {
                        BTN_Plugins_Reload.Enabled = true;
                        BTN_Plugins_Enable.Text = "Disable";
                        SelectedPluginStateLabel.ForeColor = Color.Green;
                        SelectedPluginStateStatus.Text = "Enabled";
                    }
                    else
                    {
                        BTN_Plugins_Reload.Enabled = false;
                        BTN_Plugins_Enable.Text = "Enable";
                        SelectedPluginStateLabel.ForeColor = Color.Red;
                        SelectedPluginStateStatus.Text = "Disabled";
                    }

                }

                foreach (ListViewItem item in plugins_tab_pluginslist.Items)
                {
                    PluginInfo _plugin = item.Tag as PluginInfo;

                    if (!pluginsArray.Contains(_plugin))
                    {
                        if (plugins_tab_pluginslist.Items.ContainsKey(item.Name))
                            plugins_tab_pluginslist.Items.RemoveByKey(item.Name);
                    }
                }


            }
            catch (Exception)
            {

                // nup
            }

        }





        #endregion Events

        #region Object Manipulation

        public List<Player> MyPlayers = new List<Player>();
        public List<SolarSystem> MySystems = new List<SolarSystem>();



        public void UpdateGalaxyTree()
        {
            try
            {
                if (!ServerInstance.Instance.IsRunning || ServerInstance.Instance.Handlers.UniverseHandler == null)
                    return;

                Game.Server.UniverseController Universe = ServerInstance.Instance.Handlers.UniverseHandler.Universe;

                TreeNodeCollection treeNodeList = objectManipulation_treeview.Nodes[1].Nodes; // Universe

                Galaxy galaxyObj = Universe.Galaxy;

                TreeNode galaxy = new TreeNode()
                {
                    Text = galaxyObj.Name,
                    Name = galaxyObj.Name,
                    Tag = galaxyObj
                };

                if (!treeNodeList.ContainsKey(galaxy.Name))
                    treeNodeList.Add(galaxy);


                foreach (string systemName in ServerInstance.SystemNames)
                {
                    if (string.IsNullOrEmpty(systemName)) continue;

                    SolarSystem systemObj = Universe.Galaxy.GetSystem(systemName);

                    if (systemObj == null) continue;

                    string name = systemObj.Identifier;


                    TreeNode system = new TreeNode()
                    {
                        Text = name,
                        Name = name,
                        Tag = systemObj
                    };


                    //foreach (NetEntityShip shipObj in systemObj.State.GetEntitiesByType<NetEntityShip>())
                    //{

                       // TreeNode ship = new TreeNode()
                        //{
                        //    Text = shipObj.ShipName,
                         //   Name = shipObj.ShipName,
                        //    Tag = shipObj
                       // };



                       // if (!system.Nodes.ContainsKey(shipObj.ShipName))
                            //system.Nodes.Add(ship);
                    //}



                    if (!galaxy.Nodes.ContainsKey(name))
                    {


                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(name);
                        Console.ResetColor();

                        treeNodeList[treeNodeList.IndexOf(galaxy)].Nodes.Add(system);

                       // galaxy.Nodes.Add(system);
                    }

                }

            }
            catch (Exception)
            {

               // no way, would be spammy
            }
 
        }


        public void UpdatePlayersTree()


        {
            if (!ServerInstance.Instance.IsRunning || ServerInstance.Instance.Handlers.PlayerHandler == null)
                return;

            List<Player> onlinePlayers = ServerInstance.Instance.Handlers.PlayerHandler.GetPlayers.ToList();


            TreeNodeCollection treeNodeList = objectManipulation_treeview.Nodes[0].Nodes; // Players Node

            // Convert the Games Players into something the GUI can use

            foreach (Player player in onlinePlayers)
            {
                if (!MyPlayers.Exists(x => x.ID == player.ID))
                    MyPlayers.Add(player);
            }

            // Remove the player
            foreach (Player _player in MyPlayers)
            {
                if (_player == null)
                {
                    if (MyPlayers.Exists(x => x.ID == _player.ID))
                        MyPlayers.Remove(_player);

                    if (treeNodeList.ContainsKey(_player.ID.ToString()))
                        treeNodeList.RemoveByKey(_player.ID.ToString());

                    continue;
                }

                TreeNode node = new TreeNode
                {
                    Name = _player.ID.ToString(),
                    Text = _player.Name + $" ({_player.ID})",

                    Tag = new PlayerWrapper(_player)
                };

                if (!treeNodeList.ContainsKey(node.Name))
                    treeNodeList.Add(node);
            }

            foreach (TreeNode node in treeNodeList)
            {
                PlayerWrapper player = node.Tag as PlayerWrapper;

                if (player == null)
                {
                    treeNodeList.Remove(node);
                }
            }

        }

        private void objectManipulation_treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            objectManipulation_grid.SelectedObject = node.Tag;
            objectManipulation_grid.Refresh();
        }

        private void objectManipulation_treeview_Click(object sender, EventArgs e)
        {
            UpdatePlayersTree();
        }

        #endregion Object Manipulation

        #region Server Control

        private void server_config_save_Click(object sender, EventArgs e)
        {
            if (server_server_Tabs.SelectedTab.Name == "ServerConfig")
            {
                Game.Configuration.ServerConfig.Singleton.Save();
                StatusBar.Text = "Server Config Saved.";
            }
            else if (server_server_Tabs.SelectedTab.Name == "ExtenderConfig")
            {
                if (Config.Instance.SaveConfiguration())
                {
                    StatusBar.Text = "IRSE Config Saved.";
                }
            }
        }

        private void server_config_cancel_Click(object sender, EventArgs e)
        {
            if (server_server_Tabs.SelectedTab.Name == "ServerConfig")
            {
                Game.Configuration.ServerConfig.Load();
                serverconfig_properties.SelectedObject = ServerConfigProperties.Instance;

                serverconfig_properties.Refresh();
                StatusBar.Text = "Reloaded the config from appdata server.json";
            }
            else if (server_server_Tabs.SelectedTab.Name == "ExtenderConfig")
            {
                if (Config.Instance.LoadConfiguration())
                {
                    extenderconfig_properties.SelectedObject = Config.Instance.Settings;
                    extenderconfig_properties.Refresh();

                    StatusBar.Text = "Reloaded extender config.";
                }
            }
        }

        /*
        private void server_config_setdefaults_Click(object sender, EventArgs e)
        {
            if (server_server_Tabs.SelectedTab.Name == "ServerConfig")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to load the default settings?",
                        "Server Config Settings",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    //ServerInstance.Instance.GameServerConfig.LoadDefaults();
                    serverconfig_properties.SelectedObject = ServerInstance.Instance.GameServerConfig;
                    serverconfig_properties.Refresh();
                    StatusBar.Text = "Config defaults loaded. Don't forget to save!";
                }
            }
            else if (server_server_Tabs.SelectedTab.Name == "ExtenderConfig")
            {
                Config.Instance.Settings = new Settings();
                extenderconfig_properties.SelectedObject = Config.Instance.Settings;
                extenderconfig_properties.Refresh();
                StatusBar.Text = "Extender Config Defaults loaded. Don't forget to save!";
            }
        }
        */

        private void server_config_reload_Click(object sender, EventArgs e)
        {
            if (server_server_Tabs.SelectedTab.Name == "ServerConfig")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to reload the settings from the server.json?",
                 "2. Server Settings Error",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    Game.Configuration.ServerConfig.Load();
                    serverconfig_properties.SelectedObject = ServerConfigProperties.Instance;
                    serverconfig_properties.Refresh();
                    StatusBar.Text = "Config reloaded from server.json";
                }
            }
            else if (server_server_Tabs.SelectedTab.Name == "ExtenderConfig")
            {
                if (Config.Instance.LoadConfiguration())
                {
                    extenderconfig_properties.SelectedObject = Config.Instance.Settings;
                    extenderconfig_properties.Refresh();

                    StatusBar.Text = "Reloaded IRSE Config from Config.xml.";
                }
            }
        }

        private void server_config_startserver_Click(object sender, EventArgs e)
        {
            Program.PendingServerStart = true;

            server_config_startserver.Enabled = false;

            // REMOVE ME WHEN GHOST CLIENTS ARE FIXED ON SP SIDE
            // OVerride IR starting ghost clients until they repair the name replacer.
            Game.Configuration.ServerConfig.Singleton.CreateGhostClients = false;

            //Sends an "Enter" into the Console to get the MainThread out of Console.ReadLine()
            Program.PostMessage(Program.HWnd, Program.WM_KEYDOWN, Program.VK_RETURN, 0);

        }

        private void server_config_stopserver_Click(object sender, EventArgs e)
        {
            if (ServerInstance.Instance.IsRunning)
            {
                Task.Run(() => ServerInstance.Instance.Stop());
                StatusBar.Text = "Server Stopping";
            }
            else
                StatusBar.Text = "The server is already stopped!";
        }

        #endregion Server Control

        #region Chat And Players

        public void AddChatLine(string line)
        {
            if (cpc_chat_list.InvokeRequired)
            {
                cpc_chat_list.Invoke(new MethodInvoker(delegate
                {
                    cpc_chat_list.AppendText(line + Environment.NewLine);
                }));

                return;
            }
            else
                cpc_chat_list.AppendText(line + Environment.NewLine);
        }

        private void cpc_players_kick_Click(object sender, EventArgs e)
        {
        }

        private void cpc_players_ban_Click(object sender, EventArgs e)
        {
        }

        private void cpc_messagebox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter & !String.IsNullOrEmpty(cpc_messagebox.Text))
            {
                ServerInstance.Instance.Handlers.ChatHandler.SendMessageFromServer(cpc_messagebox.Text);
                cpc_messagebox.Text = "";

                
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void cpc_chat_send_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cpc_messagebox.Text))
            {
                ServerInstance.Instance.Handlers.ChatHandler.SendMessageFromServer(cpc_messagebox.Text);
                cpc_messagebox.Text = "";
            }
        }

        #endregion Chat And Players

        #region Plugins

        private void BTN_Plugins_Reload_Click(object sender, EventArgs e)
        {
            try
            {
                if (plugins_tab_pluginslist.SelectedItems.Count == 0)
                    return;

                if (plugins_tab_pluginslist.SelectedItems.Count != 1 && plugins_tab_pluginslist.SelectedItems == null)
                    return;

                var pluginInfo = plugins_tab_pluginslist.SelectedItems[0].Tag as PluginInfo;

                ServerInstance.Instance.PluginManager.ShutdownPlugin(pluginInfo);
                ServerInstance.Instance.PluginManager.LoadPlugin(pluginInfo);

                if(ServerInstance.Instance.IsRunning)
                    ServerInstance.Instance.PluginManager.InitializePlugin(pluginInfo);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void BTN_Plugins_Enable_Click(object sender, EventArgs e)
        {
            try
            {
                if (plugins_tab_pluginslist.SelectedItems.Count == 0)
                    return;

                if (plugins_tab_pluginslist.SelectedItems.Count != 1 && plugins_tab_pluginslist.SelectedItems == null)
                    return;

                var pluginInfo = plugins_tab_pluginslist.SelectedItems[0].Tag as PluginInfo;

                ServerInstance.Instance.PluginManager.LoadedPlugins.Find(p => p == pluginInfo).MainClass.Enabled = BTN_Plugins_Enable.Text == "Disable";
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        private void plugins_tab_pluginslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (plugins_tab_pluginslist.SelectedItems.Count == 0)
                    return;

                if (plugins_tab_pluginslist.SelectedItems.Count != 1 && plugins_tab_pluginslist.SelectedItems == null)
                    return;

                var pluginInfo = plugins_tab_pluginslist.SelectedItems[0].Tag as PluginInfo;

                Type pluginType = pluginInfo.MainClassType;

                if (pluginType == null)
                    return;

                string path = Path.Combine(pluginInfo.Directory, "information.md");
                if (File.Exists(path))              
                    plugins_tab_browser.DocumentText = new Markdown().Transform(File.ReadAllText(path));               
                else             
                    plugins_tab_browser.DocumentText = new Markdown().Transform( "#" + pluginInfo.Name.ToUpper());
                
                PropertyInfo info = pluginType.GetProperty("PluginControlForm");
                if (info != null)// Form view
                {

                    Form value = (Form)info.GetValue(pluginInfo.MainClass, null);

                    foreach (Control control in PluginSettingsFormPanel.Controls)
                    {
                        if (control.Visible)
                            control.Visible = false;
                    }

                    if (!PluginSettingsFormPanel.Controls.Contains(value))
                    {
                        value.TopLevel = false;
                        PluginSettingsFormPanel.Controls.Add(value);
                    }

                    value.Dock = DockStyle.Fill;
                    value.FormBorderStyle = FormBorderStyle.None;
                    value.Visible = true;
                }

                // Set state

            }
            catch (Exception)
            {

                
            }


            
        }
        #endregion Plugins
        private void Default_SettingsSaving(object sender, CancelEventArgs e)
        {
            StatusBar.Text = "GUI Settings Changed";
        }

        private void Tabs_Selected(object sender, TabControlEventArgs e)
        {
            //ObjectManipulationRefreshTimer.Enabled = false;
            //PlayersRefreshTimer.Enabled = false;


            switch (e.TabPageIndex)
            {
                case 0: // Server
                    break;

                case 1:  // Server Chat
                    //PlayersRefreshTimer.Enabled = true;
                    break;

                case 2: // Object Manipulation
                    //ObjectManipulationRefreshTimer.Enabled = true;
                    objectManipulation_grid.Focus();
                    break;

                case 3: // Plugins
                    //PluginsRefreshTimer.Enabled = true;
                    break;

                default:
                    break;
            }
        }

        private async void serverconfig_checkForUpdates_Click(object sender, EventArgs e)
        {
            StatusBar.Text = "Checking for updates...";

            UpdateManager.GUIMode = true;

            await UpdateManager.Instance.GetLatestReleaseInfo();

            UpdateManager.Instance.CheckVersion(false);
        }

        private void Instance_OnUpdateChecked(Octokit.Release release)
        {
            if (!UpdateManager.HasUpdate)
            {
                StatusBar.Text = $"You are running the latest version!";
                return;
            }
          
            var result = MessageBox.Show(
                $"A new version has been detected: { UpdateManager.NewVersionNumber }\r\n" +
                $"\r\n Release Information:\r\n" +
                $"Release Name: {release.Name}\r\n" +
                $"Download Count: {release.Assets.FirstOrDefault().DownloadCount}\r\n" +
                $"Release Published {release.Assets.First().CreatedAt.ToLocalTime()}\r\n" +
                $"Release Description:\r\n\r\n" +
                $"{release.Body}\r\n\r\n" +
                $"Would you like to update now?",
                "IRSE Updater",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                ServerInstance.Instance.Handlers.UniverseHandler.ForceGalaxySave();
                UpdateManager.Instance.DownloadLatestRelease(Config.Instance.Settings.EnableDevelopmentVersion);
            }
            else
            {
                StatusBar.Text = "The Update has been canceled.";
            }
        }

        private void Instance_OnUpdateDownloaded(Octokit.Release release)
        {
            if (UpdateManager.GUIMode)
            {
                StatusBar.Text = "The Update is being applied..";
                UpdateManager.Instance.ApplyUpdate();
            }
        }

        private void Instance_OnUpdateApplied(Octokit.Release release)
        {
            var result = MessageBox.Show(
                $"You must restart before the update can be completed!\r\n\r\n" +
                $"Would you like to restart now?\r\n" +
                $"Note: The server was saved after downloading this release.",
                "Extender Updater",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                Program.Restart();
            }
            else
            {
                StatusBar.Text = "IRSE needs to be restarted before you can use the new features!";
            }
        }

        private void ExtenderGui_Load(object sender, EventArgs e)
        {

        }

        private void ExtenderGui_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            e.Cancel = true;
        }

        #region Global Keys

        private void GlobalKeyPressTimer_Tick(object sender, EventArgs e)
        {
            if (objectManipulation_grid.Focused)
            {
                Console.WriteLine("grid focus!");
                GlobalKeyPressTimer.Enabled = false;

                if (!wasPressed && globalKeyThread(Keys.Alt))
                {
                    Console.WriteLine("ALT hold");
                    wasPressed = true;
                }
                else if (wasPressed)
                {
                    wasPressed = false;
                    Console.WriteLine("ALT hold");
                }

                GlobalKeyPressTimer.Enabled = true;
            }
           
        }

        private bool wasPressed = false;
        private bool globalKeyThread(Keys key)
        {
            int state;
            state = Convert.ToInt32(GetAsyncKeyState(key));
            if (state == -32767)
            {
                return true;
            }
            return false;
        }

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        #endregion

        private void objectManipulation_grid_Click(object sender, EventArgs e)
        {
            objectManipulation_grid.Focus();
        }

    }
}