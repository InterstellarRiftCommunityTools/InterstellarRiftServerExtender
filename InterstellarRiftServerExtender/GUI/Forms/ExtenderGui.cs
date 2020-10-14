using Game.Server;
using Game.Universe;
using IRSE.GUI.Forms.Browser;
using IRSE.Managers;
using IRSE.Managers.Plugins;
using IRSE.Modules;
using IRSE.Modules.GameConfig;
using MarkdownDeep;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRSE.GUI.Forms
{
    public partial class ExtenderGui : Form
    {
        private Timer PlayersRefreshTimer = new Timer();
        private Timer PluginsRefreshTimer = new Timer();

        public ExtenderGui()
        {
            InitializeComponent();


            
        }

        private bool AreYouSure(string sureOfWhat)
            => MessageBox.Show(sureOfWhat, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;

        private void DisableControls(bool disable = true)
        {
            cpc_chat_list.BackColor = disable ? SystemColors.Window : SystemColors.Control;

            cpc_messagebox.Enabled = !disable;
            cpc_chat_send.Enabled = !disable;

            pc_banplayer.Enabled = !disable;
            pc_kickplayer.Enabled = !disable;
            pc_forgetplayer.Enabled = !disable;
            pc_killplayer.Enabled = !disable;
            pc_toggleadmin.Enabled = !disable;

            sc_playerslist_listview.Enabled = !disable;

            server_config_stopserver.Enabled = !disable;
            server_config_startserver.Enabled = disable;
        }

        #region Events

        private void ExtenderGui_Load(object sender, EventArgs e)
        {
            AddChatLine("Waiting for server to start..");

            cpc_chat_list.ReadOnly = true;

            DisableControls();

            ServerInstance.Instance.OnServerStarted += Instance_OnServerStarted;
            ServerInstance.Instance.OnServerStopped += Instance_OnServerStopped;
            ServerInstance.Instance.OnServerStarting += Instance_OnServerStarting;

            UpdateManager.Instance.OnUpdateChecked += new UpdateManager.UpdateEventHandler(Instance_OnUpdateChecked);
            UpdateManager.Instance.OnUpdateDownloaded += new UpdateManager.UpdateEventHandler(Instance_OnUpdateDownloaded);
            UpdateManager.Instance.OnUpdateApplied += new UpdateManager.UpdateEventHandler(Instance_OnUpdateApplied);

            new ServerConfigProperties();
            serverconfig_properties.SelectedObject = ServerConfigProperties.Instance;
            extenderconfig_properties.SelectedObject = Config.Instance.Settings;

            if (Config.Instance.Settings.EnableDevelopmentVersion)
            {
                development_label.Visible = true;
            }

            server_hesNewsLabel.Text =
                "Welcome to IRSE!\nIt's Almost Ready!!\n" +
                "Woot!";

            PluginsRefreshTimer.Enabled = true;
            PluginsRefreshTimer.Interval = (1000); // 1 secs
            PluginsRefreshTimer.Tick += delegate (object sender, EventArgs e)
            {
                UpdatePluginTab();
            };

        }

        private void Instance_OnServerStarting()
        {
            Invoke(new MethodInvoker(delegate
            {
                server_config_startserver.Enabled = false;

                this.Refresh();
            }));
        }

        private void Instance_OnServerStarted()
        {
            
            Invoke(new MethodInvoker(delegate
            {
                AddChatLine("Server Online, Ready For Chat.");

                DisableControls(false);

                PlayersRefreshTimer.Enabled = true;
                
                PlayersRefreshTimer.Interval = (1000); // 1 secs
                PlayersRefreshTimer.Tick += delegate (object sender, EventArgs e)
                {
                    UpdateChatPlayers();
                    UpdateBannedPlayers();
                };

                this.Refresh();
            }));
        }

        private void Instance_OnServerStopped()
        {
            Invoke(new MethodInvoker(delegate
            {
                DisableControls();

                PlayersRefreshTimer.Stop();
                PlayersRefreshTimer.Enabled = false;

                this.Refresh();
            }));
        }

        private void Tabs_Selected(object sender, TabControlEventArgs e)
        {
            PlayersRefreshTimer.Enabled = e.TabPage.Name == "PlayersAndChatTab";
            PluginsRefreshTimer.Enabled = e.TabPage.Name == "PluginsTab";

            this.Refresh();
        }

        #endregion Events

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
                if (!AreYouSure("You wish to reload the Server Config?"))
                    return;

                Game.Configuration.ServerConfig.Load();
                serverconfig_properties.SelectedObject = ServerConfigProperties.Instance;

                serverconfig_properties.Refresh();
                StatusBar.Text = "Reloaded the config from appdata server.json";
            }
            else if (server_server_Tabs.SelectedTab.Name == "ExtenderConfig")
            {
                if (!AreYouSure("You wish to reload the Extender Config?"))
                    return;

                if (Config.Instance.LoadConfiguration())
                {
                    extenderconfig_properties.SelectedObject = Config.Instance.Settings;
                    extenderconfig_properties.Refresh();

                    StatusBar.Text = "Reloaded extender config.";
                }
            }
        }

        private void server_config_reload_Click(object sender, EventArgs e)
        {
            if (server_server_Tabs.SelectedTab.Name == "ServerConfig")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to reload the settings from the server.json?",
                 "Server Settings",
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

        public void UpdateBannedPlayers()
        {
            if (!ServerInstance.Instance.IsRunning)
                return;

            if (ServerInstance.Instance.Handlers.ControllerManager.Universe == null)
                return;

            ServerGalaxy galaxy = ServerInstance.Instance.Handlers.ControllerManager.Universe.Galaxy as ServerGalaxy;

            List<PlayerClientData> bannedPlayers =
                galaxy.GetPlayerClientData().Where(pcd => pcd.serverRights == ServerRights.SR_Banned).ToList();

            // add
            foreach (PlayerClientData player in bannedPlayers)
            {
                if (player == null)
                    continue;

                var item = new ListViewItem();
                item.Name = player.ID.ToString();
                item.Tag = player;
                item.Text = $"{player.Name} ({player.ID})";

                if (!sc_playerbans_listview.Items.ContainsKey(item.Name))
                    sc_playerbans_listview.Items.Add(item);
            }

            // remove
            foreach (ListViewItem item in sc_playerbans_listview.Items)
            {
                PlayerClientData _player = item.Tag as PlayerClientData;

                if (_player == null)
                    continue;

                if (!bannedPlayers.Contains(_player))
                {
                    if (sc_playerbans_listview.Items.ContainsKey(item.Name))
                        sc_playerbans_listview.Items.RemoveByKey(item.Name);
                }
            }
        }

        public void UpdateChatPlayers()
        {
            try
            {
                if (!ServerInstance.Instance.IsRunning)
                    return;

                if (ServerInstance.Instance.Handlers.PlayerHandler == null)
                    return;

                List<Player> onlinePlayers = ServerInstance.Instance.Handlers.ControllerManager.Players.AllPlayers().ToList();

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

        private void pc_kickplayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ServerInstance.Instance.IsRunning)
                    return;

                var players = sc_playerslist_listview.SelectedItems;

                if (players.Count == 0)
                    return;

                if (!AreYouSure("You wish to Kick the selected Player(s)?"))
                    return;

                foreach (ListViewItem player in players)
                {
                    SvCommands.c_kickPlayer(ServerInstance.Instance.Handlers.ControllerManager, new List<string>() { string.Empty, player.Name });
                }
            }
            catch (Exception)
            {
            }
        }

        private void pc_banplayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ServerInstance.Instance.IsRunning)
                    return;

                var players = sc_playerslist_listview.SelectedItems;

                if (players.Count == 0)
                    return;

                if (!AreYouSure("You wish to Ban the selected Player(s)?"))
                    return;

                foreach (ListViewItem player in players)
                {
                    SvCommands.c_banPlayer(ServerInstance.Instance.Handlers.ControllerManager, new List<string>() { string.Empty, player.Name });
                }
            }
            catch (Exception)
            {
            }
        }

        private void pc_forgetplayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ServerInstance.Instance.IsRunning)
                    return;

                var players = sc_playerslist_listview.SelectedItems;

                if (players.Count == 0)
                    return;

                if (!AreYouSure($"You wish to Forget the selected Player(s)?\n\n This will kick the selected players, then force them to pick a faction the next time they login."))
                    return;

                foreach (ListViewItem player in players)
                {
                    SvCommands.c_forgetPlayer(ServerInstance.Instance.Handlers.ControllerManager, new List<string>() { string.Empty, player.Name });
                }
            }
            catch (Exception)
            {
            }
        }

        private void pc_killplayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ServerInstance.Instance.IsRunning)
                    return;

                var players = sc_playerslist_listview.SelectedItems;

                if (players.Count == 0)
                    return;

                if (!AreYouSure("You wish to Kill the selected Player(s)?"))
                    return;

                foreach (ListViewItem player in players)
                {
                    SvCommands.c_killPlayer(ServerInstance.Instance.Handlers.ControllerManager, new List<string>() { string.Empty, player.Name });
                }
            }
            catch (Exception)
            {
            }
        }

        private void pc_toggleadmin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ServerInstance.Instance.IsRunning)
                    return;

                var players = sc_playerslist_listview.SelectedItems;

                if (players.Count == 0)
                    return;

                if (!AreYouSure("You wish to Toggle Admin on the selected Player(s)?"))
                    return;

                foreach (ListViewItem player in players)
                {
                    SvCommands.c_setSuperAdmin(ServerInstance.Instance.Handlers.ControllerManager, new List<string>() { string.Empty, player.Name });
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion Chat And Players

        #region Plugins

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
            catch (Exception)
            {
                
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
                //if (File.Exists(path))
                    //plugins_tab_browser.DocumentText = new Markdown().Transform(File.ReadAllText(path));
                //else
                    //plugins_tab_browser.DocumentText = new Markdown().Transform("#" + pluginInfo.Name.ToUpper());

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
            }
            catch (Exception)
            {
            }
        }

        #endregion Plugins

        #region Updates

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

        #endregion Updates

        private void ExtenderGui_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            e.Cancel = true;
        }

        private void ts_pc_startserver_Click(object sender, EventArgs e)
        {

        }

        private void ts_pc_stopserver_Click(object sender, EventArgs e)
        {

        }

        private void ts_pc_restartserver_Click(object sender, EventArgs e)
        {

        }

        private void ts_pc_closeirse_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("http://patreon.com/irse");
        }

        private BrowserForm BrowserForm = new BrowserForm();
        private void pm_pluginbrowserbtn_Click(object sender, EventArgs e)
        {
            BrowserForm.Visible = true;
        }
    }
}