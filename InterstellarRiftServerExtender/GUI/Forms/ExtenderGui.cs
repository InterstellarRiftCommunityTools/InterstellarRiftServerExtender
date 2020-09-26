

using IRSE.Managers;
using IRSE.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Server;
using IRSE.Modules.GameConfig;
using Telerik.WinControls.Data;

namespace IRSE.GUI.Forms
{
    public partial class ExtenderGui : Form
    {
        private Timer ObjectManipulationRefreshTimer = new Timer();
        private Timer PlayersRefreshTimer = new Timer();
      

        public ExtenderGui()
        {
            InitializeComponent();

            DisableControls();
            
            ServerInstance.Instance.OnServerStarted += Instance_OnServerStarted;
            ServerInstance.Instance.OnServerStopped += Instance_OnServerStopped;


            new ServerConfigProperties();
            serverconfig_properties.SelectedObject = ServerConfigProperties.Instance;
            extenderconfig_properties.SelectedObject = Config.Instance.Settings;


            //serverconfig_properties.PropertyGridElement.ToolbarElement.Filt //.FilterPropertyName = "Category";

            //FilterDescriptor filter = new FilterDescriptor("Category", FilterOperator.Contains, "server");
            //serverconfig_properties.FilterDescriptors.Add(filter);

            serverconfig_properties.Refresh();

            if (Config.Instance.Settings.EnableDevelopmentVersion)
            {
                var result = MessageBox.Show(
                    "Development Versions have been enabled.\r\n\r\n" +
                    "You have selected to use IRSE's Development Versions",
                    "Development Versions Enabled",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    
                }
            }
            
            //UpdateManager.Instance.OnUpdateChecked += new UpdateManager.UpdateEventHandler(Instance_OnUpdateChecked);
            //UpdateManager.Instance.OnUpdateDownloaded += new UpdateManager.UpdateEventHandler(Instance_OnUpdateDownloaded);
            //UpdateManager.Instance.OnUpdateApplied += new UpdateManager.UpdateEventHandler(Instance_OnUpdateApplied);

            server_hesNewsLabel.Text = 
                "Welcome to IRSE!\nIt's Almost Ready!!\n" +
                "Woot!";

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

            objectManipulation_grid.Enabled = !disable;
            objectManipulation_treeview.Enabled = !disable;

            cpc_chat_list.AppendText("Waiting for server to start..\r\n");
        }

        #region Events

        private void Instance_OnServerStopped()
        {
            this.Invoke(new MethodInvoker(delegate
            {
                DisableControls();

                ObjectManipulationRefreshTimer.Stop();
                ObjectManipulationRefreshTimer.Enabled = false;

                PlayersRefreshTimer.Stop();
                PlayersRefreshTimer.Enabled = false;

                this.Refresh();
            }));
        }

        public void UpdateChatPlayers()
        {
            try
            {


                if (!ServerInstance.Instance.IsRunning)
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

                    if (!pc_players_listview.Items.ContainsKey(item.Name))
                        pc_players_listview.Items.Add(item);
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

                // player control players
                foreach (ListViewItem item in pc_players_listview.Items)
                {
                    Player _player = item.Tag as Player;

                    if (!onlinePlayers.Contains(_player))
                    {
                        if (pc_players_listview.Items.ContainsKey(_player.ID.ToString()))
                            pc_players_listview.Items.RemoveByKey(_player.ID.ToString());
                    }
                }
            }
            catch (Exception)
            {
                // nope
            }
        }

        private void Instance_OnServerStarted()
        {
            Invoke(new MethodInvoker(delegate
            {
                DisableControls(false);

                UpdatePlayersTree();
                UpdateChatPlayers();

                this.Refresh();
            }));

            ObjectManipulationRefreshTimer.Interval = (1000); // 1 secs
            ObjectManipulationRefreshTimer.Tick += delegate (object sender, EventArgs e)
            {
                UpdatePlayersTree();
            };

            PlayersRefreshTimer.Interval = (10000); // 1 secs
            PlayersRefreshTimer.Tick += delegate (object sender, EventArgs e)
            {
                UpdateChatPlayers();
            };
        }

        #endregion Events

        #region Object Manipulation

        public List<Player> MyPlayers = new List<Player>();

        public void UpdatePlayersTree()
        {
            List<Player> onlinePlayers = ServerInstance.Instance.Handlers.PlayerHandler.GetPlayers.ToList();


            if (!ServerInstance.Instance.IsRunning)
                return;

            var treeNodeList = objectManipulation_treeview.Nodes[0].Nodes;

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

                    objectManipulation_treeview.Refresh();
                    objectManipulation_grid.Refresh();

                    continue;
                }

                TreeNode node = new TreeNode
                {
                    Name = _player.ID.ToString(),
                    Text = _player.Name + $" ({_player.ID})",

                    Tag = _player
                };

                if (!treeNodeList.ContainsKey(node.Name))
                    treeNodeList.Add(node);
            }

            foreach (TreeNode node in treeNodeList)
            {
                Player player = node.Tag as Player;

                if (player == null)
                {
                    treeNodeList.Remove(node);
                }
            }

            objectManipulation_treeview.Refresh();
            objectManipulation_grid.Refresh();
        }

        private void objectManipulation_treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            objectManipulation_grid.SelectedObject = node.Tag as Player;
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
                    StatusBar.Text = "HES Config Saved.";
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

                    StatusBar.Text = "Reloaded HES Config from Config.xml.";
                }
            }
        }

        private void server_config_startserver_Click(object sender, EventArgs e)
        {
            if (!ServerInstance.Instance.IsRunning)
            {
                Task.Run(() => ServerInstance.Instance.Start());
                StatusBar.Text = "Server Starting";
            }
            else
                StatusBar.Text = "The server is already running!";
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

        private void Default_SettingsSaving(object sender, CancelEventArgs e)
        {
            StatusBar.Text = "GUI Settings Changed";
        }

        private void objectManipulation_grid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
        }

        private void Tabs_Selected(object sender, TabControlEventArgs e)
        {
            ObjectManipulationRefreshTimer.Enabled = false;
            PlayersRefreshTimer.Enabled = false;

            switch (e.TabPageIndex)
            {
                case 0:
                    break;

                case 1:
                    PlayersRefreshTimer.Enabled = true;

                    break;

                case 2:
                    PlayersRefreshTimer.Enabled = true;
                    break;

                case 3:
                    ObjectManipulationRefreshTimer.Enabled = true;
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

            ServerInstance.Instance.Save(true);

            var result = MessageBox.Show(
                $"A new version has been detected: { UpdateManager.NewVersionNumber }\r\n" +
                $"\r\n Release Information:\r\n" +
                $"Release Name: {release.Name}\r\n" +
                $"Download Count: {release.Assets.FirstOrDefault().DownloadCount}\r\n" +
                $"Release Published {release.Assets.First().CreatedAt.ToLocalTime()}\r\n" +
                $"Release Description:\r\n\r\n" +
                $"{release.Body}\r\n\r\n" +
                $"Would you like to update now?", 
                "HES Updater", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
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




    }
}