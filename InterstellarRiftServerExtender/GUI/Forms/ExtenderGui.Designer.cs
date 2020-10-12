namespace IRSE.GUI.Forms
{
    partial class ExtenderGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtenderGui));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.ServerTab = new System.Windows.Forms.TabPage();
            this.ServerContainer = new System.Windows.Forms.SplitContainer();
            this.server_server_Tabs = new System.Windows.Forms.TabControl();
            this.ServerConfig = new System.Windows.Forms.TabPage();
            this.serverconfig_properties = new System.Windows.Forms.PropertyGrid();
            this.ExtenderConfig = new System.Windows.Forms.TabPage();
            this.extenderconfig_properties = new System.Windows.Forms.PropertyGrid();
            this.serverconfig_checkForUpdates = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.server_hesNewsLabel = new System.Windows.Forms.Label();
            this.server_config_reload = new System.Windows.Forms.Button();
            this.server_config_setdefaults = new System.Windows.Forms.Button();
            this.server_config_cancel = new System.Windows.Forms.Button();
            this.server_config_save = new System.Windows.Forms.Button();
            this.server_config_stopserver = new System.Windows.Forms.Button();
            this.server_config_startserver = new System.Windows.Forms.Button();
            this.PlayersAndChatTab = new System.Windows.Forms.TabPage();
            this.ChatPlayerContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.sc_onlineplayers_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.sc_playerslist_listview = new System.Windows.Forms.ListView();
            this.PlayerColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsAdminColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.sc_playerbans_listview = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pc_killplayer = new System.Windows.Forms.Button();
            this.pc_toggleadmin = new System.Windows.Forms.Button();
            this.pc_forgetplayer = new System.Windows.Forms.Button();
            this.pc_banplayer = new System.Windows.Forms.Button();
            this.pc_kickplayer = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.cpc_chat_tabs = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cpc_chat_list = new System.Windows.Forms.TextBox();
            this.cpc_chat_send = new System.Windows.Forms.Button();
            this.cpc_messagebox = new System.Windows.Forms.TextBox();
            this.CommandVisualizerTab = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.CV_PLAYERS_TAB = new System.Windows.Forms.TabPage();
            this.CV_SYSTEMS_TAB = new System.Windows.Forms.TabPage();
            this.CV_SHIPS_TAB = new System.Windows.Forms.TabPage();
            this.CV_CREW_TAB = new System.Windows.Forms.TabPage();
            this.CV_FLEET_TAB = new System.Windows.Forms.TabPage();
            this.CV_ASTEROIDS_TAB = new System.Windows.Forms.TabPage();
            this.CV_RIFTS_TAB = new System.Windows.Forms.TabPage();
            this.PluginsTab = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.BTN_Plugins_Enable = new System.Windows.Forms.Button();
            this.SelectedPluginStateStatus = new System.Windows.Forms.Label();
            this.SelectedPluginStateLabel = new System.Windows.Forms.Label();
            this.plugins_tab_pluginslist = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.plugins_tab_informationTab = new System.Windows.Forms.TabPage();
            this.plugins_tab_browser = new System.Windows.Forms.WebBrowser();
            this.plugins_tab_settingsTab = new System.Windows.Forms.TabPage();
            this.PluginSettingsFormPanel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.ExtenderWebsite = new System.Windows.Forms.TabPage();
            this.hesw_Website = new System.Windows.Forms.WebBrowser();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.development_label = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tabs.SuspendLayout();
            this.ServerTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerContainer)).BeginInit();
            this.ServerContainer.Panel1.SuspendLayout();
            this.ServerContainer.Panel2.SuspendLayout();
            this.ServerContainer.SuspendLayout();
            this.server_server_Tabs.SuspendLayout();
            this.ServerConfig.SuspendLayout();
            this.ExtenderConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.PlayersAndChatTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChatPlayerContainer)).BeginInit();
            this.ChatPlayerContainer.Panel1.SuspendLayout();
            this.ChatPlayerContainer.Panel2.SuspendLayout();
            this.ChatPlayerContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.cpc_chat_tabs.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.CommandVisualizerTab.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.PluginsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.plugins_tab_informationTab.SuspendLayout();
            this.plugins_tab_settingsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.ExtenderWebsite.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.ServerTab);
            this.Tabs.Controls.Add(this.PlayersAndChatTab);
            this.Tabs.Controls.Add(this.CommandVisualizerTab);
            this.Tabs.Controls.Add(this.PluginsTab);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(780, 495);
            this.Tabs.TabIndex = 0;
            this.Tabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.Tabs_Selected);
            // 
            // ServerTab
            // 
            this.ServerTab.Controls.Add(this.ServerContainer);
            this.ServerTab.Location = new System.Drawing.Point(4, 22);
            this.ServerTab.Name = "ServerTab";
            this.ServerTab.Padding = new System.Windows.Forms.Padding(3);
            this.ServerTab.Size = new System.Drawing.Size(772, 469);
            this.ServerTab.TabIndex = 0;
            this.ServerTab.Text = "Server Control";
            this.ServerTab.UseVisualStyleBackColor = true;
            // 
            // ServerContainer
            // 
            this.ServerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerContainer.Location = new System.Drawing.Point(3, 3);
            this.ServerContainer.Name = "ServerContainer";
            this.ServerContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ServerContainer.Panel1
            // 
            this.ServerContainer.Panel1.Controls.Add(this.server_server_Tabs);
            // 
            // ServerContainer.Panel2
            // 
            this.ServerContainer.Panel2.Controls.Add(this.serverconfig_checkForUpdates);
            this.ServerContainer.Panel2.Controls.Add(this.groupBox1);
            this.ServerContainer.Panel2.Controls.Add(this.server_config_reload);
            this.ServerContainer.Panel2.Controls.Add(this.server_config_setdefaults);
            this.ServerContainer.Panel2.Controls.Add(this.server_config_cancel);
            this.ServerContainer.Panel2.Controls.Add(this.server_config_save);
            this.ServerContainer.Panel2.Controls.Add(this.server_config_stopserver);
            this.ServerContainer.Panel2.Controls.Add(this.server_config_startserver);
            this.ServerContainer.Size = new System.Drawing.Size(766, 463);
            this.ServerContainer.SplitterDistance = 369;
            this.ServerContainer.TabIndex = 3;
            // 
            // server_server_Tabs
            // 
            this.server_server_Tabs.Controls.Add(this.ServerConfig);
            this.server_server_Tabs.Controls.Add(this.ExtenderConfig);
            this.server_server_Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.server_server_Tabs.Location = new System.Drawing.Point(0, 0);
            this.server_server_Tabs.Name = "server_server_Tabs";
            this.server_server_Tabs.SelectedIndex = 0;
            this.server_server_Tabs.Size = new System.Drawing.Size(766, 369);
            this.server_server_Tabs.TabIndex = 0;
            // 
            // ServerConfig
            // 
            this.ServerConfig.Controls.Add(this.serverconfig_properties);
            this.ServerConfig.Location = new System.Drawing.Point(4, 22);
            this.ServerConfig.Name = "ServerConfig";
            this.ServerConfig.Padding = new System.Windows.Forms.Padding(3);
            this.ServerConfig.Size = new System.Drawing.Size(758, 343);
            this.ServerConfig.TabIndex = 0;
            this.ServerConfig.Text = "Server Config";
            this.ServerConfig.UseVisualStyleBackColor = true;
            // 
            // serverconfig_properties
            // 
            this.serverconfig_properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverconfig_properties.Location = new System.Drawing.Point(3, 3);
            this.serverconfig_properties.Name = "serverconfig_properties";
            this.serverconfig_properties.Size = new System.Drawing.Size(752, 337);
            this.serverconfig_properties.TabIndex = 0;
            // 
            // ExtenderConfig
            // 
            this.ExtenderConfig.Controls.Add(this.extenderconfig_properties);
            this.ExtenderConfig.Location = new System.Drawing.Point(4, 22);
            this.ExtenderConfig.Name = "ExtenderConfig";
            this.ExtenderConfig.Padding = new System.Windows.Forms.Padding(3);
            this.ExtenderConfig.Size = new System.Drawing.Size(758, 343);
            this.ExtenderConfig.TabIndex = 1;
            this.ExtenderConfig.Text = "Extender Config";
            this.ExtenderConfig.UseVisualStyleBackColor = true;
            // 
            // extenderconfig_properties
            // 
            this.extenderconfig_properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extenderconfig_properties.Location = new System.Drawing.Point(3, 3);
            this.extenderconfig_properties.Name = "extenderconfig_properties";
            this.extenderconfig_properties.Size = new System.Drawing.Size(752, 337);
            this.extenderconfig_properties.TabIndex = 0;
            // 
            // serverconfig_checkForUpdates
            // 
            this.serverconfig_checkForUpdates.Location = new System.Drawing.Point(39, 9);
            this.serverconfig_checkForUpdates.Name = "serverconfig_checkForUpdates";
            this.serverconfig_checkForUpdates.Size = new System.Drawing.Size(111, 23);
            this.serverconfig_checkForUpdates.TabIndex = 8;
            this.serverconfig_checkForUpdates.Text = "Check For Update";
            this.serverconfig_checkForUpdates.UseVisualStyleBackColor = true;
            this.serverconfig_checkForUpdates.Click += new System.EventHandler(this.serverconfig_checkForUpdates_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.server_hesNewsLabel);
            this.groupBox1.Location = new System.Drawing.Point(183, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 72);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extender News";
            // 
            // server_hesNewsLabel
            // 
            this.server_hesNewsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.server_hesNewsLabel.Location = new System.Drawing.Point(3, 16);
            this.server_hesNewsLabel.Name = "server_hesNewsLabel";
            this.server_hesNewsLabel.Size = new System.Drawing.Size(367, 53);
            this.server_hesNewsLabel.TabIndex = 0;
            this.server_hesNewsLabel.Text = "Configuration options for IRSE have been moved to the Server tab\r\nunder the IRSE " +
    "Config subtab.\r\n";
            // 
            // server_config_reload
            // 
            this.server_config_reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.server_config_reload.Location = new System.Drawing.Point(562, 57);
            this.server_config_reload.Name = "server_config_reload";
            this.server_config_reload.Size = new System.Drawing.Size(106, 23);
            this.server_config_reload.TabIndex = 6;
            this.server_config_reload.Text = "Reload Config";
            this.server_config_reload.UseVisualStyleBackColor = true;
            this.server_config_reload.Click += new System.EventHandler(this.server_config_reload_Click);
            // 
            // server_config_setdefaults
            // 
            this.server_config_setdefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.server_config_setdefaults.Location = new System.Drawing.Point(562, 9);
            this.server_config_setdefaults.Name = "server_config_setdefaults";
            this.server_config_setdefaults.Size = new System.Drawing.Size(106, 23);
            this.server_config_setdefaults.TabIndex = 4;
            this.server_config_setdefaults.Text = "Set Config Defaults";
            this.server_config_setdefaults.UseVisualStyleBackColor = true;
            // 
            // server_config_cancel
            // 
            this.server_config_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.server_config_cancel.Location = new System.Drawing.Point(674, 57);
            this.server_config_cancel.Name = "server_config_cancel";
            this.server_config_cancel.Size = new System.Drawing.Size(85, 23);
            this.server_config_cancel.TabIndex = 3;
            this.server_config_cancel.Text = "Cancel";
            this.server_config_cancel.UseVisualStyleBackColor = true;
            this.server_config_cancel.Click += new System.EventHandler(this.server_config_cancel_Click);
            // 
            // server_config_save
            // 
            this.server_config_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.server_config_save.Location = new System.Drawing.Point(674, 9);
            this.server_config_save.Name = "server_config_save";
            this.server_config_save.Size = new System.Drawing.Size(85, 23);
            this.server_config_save.TabIndex = 1;
            this.server_config_save.Text = "Save Config";
            this.server_config_save.UseVisualStyleBackColor = true;
            this.server_config_save.Click += new System.EventHandler(this.server_config_save_Click);
            // 
            // server_config_stopserver
            // 
            this.server_config_stopserver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.server_config_stopserver.Location = new System.Drawing.Point(96, 55);
            this.server_config_stopserver.Name = "server_config_stopserver";
            this.server_config_stopserver.Size = new System.Drawing.Size(81, 23);
            this.server_config_stopserver.TabIndex = 1;
            this.server_config_stopserver.Text = "Stop Server";
            this.server_config_stopserver.UseVisualStyleBackColor = true;
            this.server_config_stopserver.Click += new System.EventHandler(this.server_config_stopserver_Click);
            // 
            // server_config_startserver
            // 
            this.server_config_startserver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.server_config_startserver.Location = new System.Drawing.Point(7, 55);
            this.server_config_startserver.Name = "server_config_startserver";
            this.server_config_startserver.Size = new System.Drawing.Size(83, 23);
            this.server_config_startserver.TabIndex = 0;
            this.server_config_startserver.Text = "Start Server";
            this.server_config_startserver.UseVisualStyleBackColor = true;
            this.server_config_startserver.Click += new System.EventHandler(this.server_config_startserver_Click);
            // 
            // PlayersAndChatTab
            // 
            this.PlayersAndChatTab.Controls.Add(this.ChatPlayerContainer);
            this.PlayersAndChatTab.Location = new System.Drawing.Point(4, 22);
            this.PlayersAndChatTab.Name = "PlayersAndChatTab";
            this.PlayersAndChatTab.Padding = new System.Windows.Forms.Padding(3);
            this.PlayersAndChatTab.Size = new System.Drawing.Size(772, 469);
            this.PlayersAndChatTab.TabIndex = 1;
            this.PlayersAndChatTab.Text = "Players & Chat";
            this.PlayersAndChatTab.UseVisualStyleBackColor = true;
            // 
            // ChatPlayerContainer
            // 
            this.ChatPlayerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatPlayerContainer.Location = new System.Drawing.Point(3, 3);
            this.ChatPlayerContainer.Name = "ChatPlayerContainer";
            // 
            // ChatPlayerContainer.Panel1
            // 
            this.ChatPlayerContainer.Panel1.Controls.Add(this.splitContainer2);
            // 
            // ChatPlayerContainer.Panel2
            // 
            this.ChatPlayerContainer.Panel2.Controls.Add(this.splitContainer3);
            this.ChatPlayerContainer.Size = new System.Drawing.Size(766, 463);
            this.ChatPlayerContainer.SplitterDistance = 196;
            this.ChatPlayerContainer.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.sc_onlineplayers_label);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(196, 463);
            this.splitContainer2.SplitterDistance = 30;
            this.splitContainer2.TabIndex = 0;
            // 
            // sc_onlineplayers_label
            // 
            this.sc_onlineplayers_label.AutoSize = true;
            this.sc_onlineplayers_label.Location = new System.Drawing.Point(119, 9);
            this.sc_onlineplayers_label.Name = "sc_onlineplayers_label";
            this.sc_onlineplayers_label.Size = new System.Drawing.Size(13, 13);
            this.sc_onlineplayers_label.TabIndex = 2;
            this.sc_onlineplayers_label.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Online Players: ";
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(196, 30);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.tabControl3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer4.Size = new System.Drawing.Size(196, 429);
            this.splitContainer4.SplitterDistance = 341;
            this.splitContainer4.TabIndex = 0;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage1);
            this.tabControl3.Controls.Add(this.tabPage2);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(196, 341);
            this.tabControl3.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.sc_playerslist_listview);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(188, 315);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Online";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // sc_playerslist_listview
            // 
            this.sc_playerslist_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlayerColumn,
            this.IsAdminColumn});
            this.sc_playerslist_listview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_playerslist_listview.HideSelection = false;
            this.sc_playerslist_listview.Location = new System.Drawing.Point(3, 3);
            this.sc_playerslist_listview.Name = "sc_playerslist_listview";
            this.sc_playerslist_listview.Size = new System.Drawing.Size(182, 309);
            this.sc_playerslist_listview.TabIndex = 1;
            this.sc_playerslist_listview.UseCompatibleStateImageBehavior = false;
            this.sc_playerslist_listview.View = System.Windows.Forms.View.Details;
            // 
            // PlayerColumn
            // 
            this.PlayerColumn.Text = "Player";
            this.PlayerColumn.Width = 125;
            // 
            // IsAdminColumn
            // 
            this.IsAdminColumn.Text = "Admin";
            this.IsAdminColumn.Width = 50;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.sc_playerbans_listview);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(188, 315);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bans";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // sc_playerbans_listview
            // 
            this.sc_playerbans_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.sc_playerbans_listview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_playerbans_listview.HideSelection = false;
            this.sc_playerbans_listview.Location = new System.Drawing.Point(3, 3);
            this.sc_playerbans_listview.Name = "sc_playerbans_listview";
            this.sc_playerbans_listview.Size = new System.Drawing.Size(182, 309);
            this.sc_playerbans_listview.TabIndex = 2;
            this.sc_playerbans_listview.UseCompatibleStateImageBehavior = false;
            this.sc_playerbans_listview.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Player";
            this.columnHeader1.Width = 176;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ban Date";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pc_killplayer);
            this.groupBox2.Controls.Add(this.pc_toggleadmin);
            this.groupBox2.Controls.Add(this.pc_forgetplayer);
            this.groupBox2.Controls.Add(this.pc_banplayer);
            this.groupBox2.Controls.Add(this.pc_kickplayer);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 84);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quick Player Control";
            // 
            // pc_killplayer
            // 
            this.pc_killplayer.Location = new System.Drawing.Point(57, 55);
            this.pc_killplayer.Name = "pc_killplayer";
            this.pc_killplayer.Size = new System.Drawing.Size(41, 23);
            this.pc_killplayer.TabIndex = 7;
            this.pc_killplayer.Text = "Kill";
            this.pc_killplayer.UseVisualStyleBackColor = true;
            this.pc_killplayer.Click += new System.EventHandler(this.pc_killplayer_Click);
            // 
            // pc_toggleadmin
            // 
            this.pc_toggleadmin.Location = new System.Drawing.Point(141, 19);
            this.pc_toggleadmin.Name = "pc_toggleadmin";
            this.pc_toggleadmin.Size = new System.Drawing.Size(48, 59);
            this.pc_toggleadmin.TabIndex = 6;
            this.pc_toggleadmin.Text = "Toggle Admin";
            this.pc_toggleadmin.UseVisualStyleBackColor = true;
            this.pc_toggleadmin.Click += new System.EventHandler(this.pc_toggleadmin_Click);
            // 
            // pc_forgetplayer
            // 
            this.pc_forgetplayer.Location = new System.Drawing.Point(4, 55);
            this.pc_forgetplayer.Name = "pc_forgetplayer";
            this.pc_forgetplayer.Size = new System.Drawing.Size(47, 23);
            this.pc_forgetplayer.TabIndex = 5;
            this.pc_forgetplayer.Text = "Forget";
            this.pc_forgetplayer.UseVisualStyleBackColor = true;
            this.pc_forgetplayer.Click += new System.EventHandler(this.pc_forgetplayer_Click);
            // 
            // pc_banplayer
            // 
            this.pc_banplayer.Location = new System.Drawing.Point(57, 19);
            this.pc_banplayer.Name = "pc_banplayer";
            this.pc_banplayer.Size = new System.Drawing.Size(41, 23);
            this.pc_banplayer.TabIndex = 4;
            this.pc_banplayer.Text = "Ban";
            this.pc_banplayer.UseVisualStyleBackColor = true;
            this.pc_banplayer.Click += new System.EventHandler(this.pc_banplayer_Click);
            // 
            // pc_kickplayer
            // 
            this.pc_kickplayer.Location = new System.Drawing.Point(4, 19);
            this.pc_kickplayer.Name = "pc_kickplayer";
            this.pc_kickplayer.Size = new System.Drawing.Size(47, 23);
            this.pc_kickplayer.TabIndex = 3;
            this.pc_kickplayer.Text = "Kick";
            this.pc_kickplayer.UseVisualStyleBackColor = true;
            this.pc_kickplayer.Click += new System.EventHandler(this.pc_kickplayer_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.cpc_chat_tabs);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.cpc_chat_send);
            this.splitContainer3.Panel2.Controls.Add(this.cpc_messagebox);
            this.splitContainer3.Size = new System.Drawing.Size(566, 463);
            this.splitContainer3.SplitterDistance = 412;
            this.splitContainer3.TabIndex = 0;
            // 
            // cpc_chat_tabs
            // 
            this.cpc_chat_tabs.Controls.Add(this.tabPage3);
            this.cpc_chat_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpc_chat_tabs.Enabled = false;
            this.cpc_chat_tabs.Location = new System.Drawing.Point(0, 0);
            this.cpc_chat_tabs.Name = "cpc_chat_tabs";
            this.cpc_chat_tabs.SelectedIndex = 0;
            this.cpc_chat_tabs.Size = new System.Drawing.Size(566, 412);
            this.cpc_chat_tabs.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cpc_chat_list);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(558, 386);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Chat";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cpc_chat_list
            // 
            this.cpc_chat_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpc_chat_list.Location = new System.Drawing.Point(3, 3);
            this.cpc_chat_list.Multiline = true;
            this.cpc_chat_list.Name = "cpc_chat_list";
            this.cpc_chat_list.ReadOnly = true;
            this.cpc_chat_list.Size = new System.Drawing.Size(552, 380);
            this.cpc_chat_list.TabIndex = 2;
            // 
            // cpc_chat_send
            // 
            this.cpc_chat_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cpc_chat_send.Location = new System.Drawing.Point(487, 3);
            this.cpc_chat_send.Name = "cpc_chat_send";
            this.cpc_chat_send.Size = new System.Drawing.Size(50, 23);
            this.cpc_chat_send.TabIndex = 1;
            this.cpc_chat_send.Text = "Send";
            this.cpc_chat_send.UseVisualStyleBackColor = true;
            this.cpc_chat_send.Click += new System.EventHandler(this.cpc_chat_send_Click);
            // 
            // cpc_messagebox
            // 
            this.cpc_messagebox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cpc_messagebox.Location = new System.Drawing.Point(3, 5);
            this.cpc_messagebox.Name = "cpc_messagebox";
            this.cpc_messagebox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.cpc_messagebox.Size = new System.Drawing.Size(478, 20);
            this.cpc_messagebox.TabIndex = 0;
            this.cpc_messagebox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cpc_messagebox_KeyDown);
            // 
            // CommandVisualizerTab
            // 
            this.CommandVisualizerTab.Controls.Add(this.tabControl2);
            this.CommandVisualizerTab.Location = new System.Drawing.Point(4, 22);
            this.CommandVisualizerTab.Name = "CommandVisualizerTab";
            this.CommandVisualizerTab.Padding = new System.Windows.Forms.Padding(3);
            this.CommandVisualizerTab.Size = new System.Drawing.Size(772, 469);
            this.CommandVisualizerTab.TabIndex = 6;
            this.CommandVisualizerTab.Text = "Command Visualizer";
            this.CommandVisualizerTab.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.CV_PLAYERS_TAB);
            this.tabControl2.Controls.Add(this.CV_SYSTEMS_TAB);
            this.tabControl2.Controls.Add(this.CV_SHIPS_TAB);
            this.tabControl2.Controls.Add(this.CV_CREW_TAB);
            this.tabControl2.Controls.Add(this.CV_FLEET_TAB);
            this.tabControl2.Controls.Add(this.CV_ASTEROIDS_TAB);
            this.tabControl2.Controls.Add(this.CV_RIFTS_TAB);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(766, 463);
            this.tabControl2.TabIndex = 0;
            // 
            // CV_PLAYERS_TAB
            // 
            this.CV_PLAYERS_TAB.Location = new System.Drawing.Point(4, 22);
            this.CV_PLAYERS_TAB.Name = "CV_PLAYERS_TAB";
            this.CV_PLAYERS_TAB.Size = new System.Drawing.Size(758, 437);
            this.CV_PLAYERS_TAB.TabIndex = 3;
            this.CV_PLAYERS_TAB.Text = "Players";
            this.CV_PLAYERS_TAB.UseVisualStyleBackColor = true;
            // 
            // CV_SYSTEMS_TAB
            // 
            this.CV_SYSTEMS_TAB.Location = new System.Drawing.Point(4, 22);
            this.CV_SYSTEMS_TAB.Name = "CV_SYSTEMS_TAB";
            this.CV_SYSTEMS_TAB.Size = new System.Drawing.Size(758, 437);
            this.CV_SYSTEMS_TAB.TabIndex = 6;
            this.CV_SYSTEMS_TAB.Text = "Systems";
            this.CV_SYSTEMS_TAB.UseVisualStyleBackColor = true;
            // 
            // CV_SHIPS_TAB
            // 
            this.CV_SHIPS_TAB.Location = new System.Drawing.Point(4, 22);
            this.CV_SHIPS_TAB.Name = "CV_SHIPS_TAB";
            this.CV_SHIPS_TAB.Size = new System.Drawing.Size(758, 437);
            this.CV_SHIPS_TAB.TabIndex = 0;
            this.CV_SHIPS_TAB.Text = "Ships";
            this.CV_SHIPS_TAB.UseVisualStyleBackColor = true;
            // 
            // CV_CREW_TAB
            // 
            this.CV_CREW_TAB.Location = new System.Drawing.Point(4, 22);
            this.CV_CREW_TAB.Name = "CV_CREW_TAB";
            this.CV_CREW_TAB.Size = new System.Drawing.Size(758, 437);
            this.CV_CREW_TAB.TabIndex = 4;
            this.CV_CREW_TAB.Text = "Crew";
            this.CV_CREW_TAB.UseVisualStyleBackColor = true;
            // 
            // CV_FLEET_TAB
            // 
            this.CV_FLEET_TAB.Location = new System.Drawing.Point(4, 22);
            this.CV_FLEET_TAB.Name = "CV_FLEET_TAB";
            this.CV_FLEET_TAB.Size = new System.Drawing.Size(758, 437);
            this.CV_FLEET_TAB.TabIndex = 5;
            this.CV_FLEET_TAB.Text = "Fleet";
            this.CV_FLEET_TAB.UseVisualStyleBackColor = true;
            // 
            // CV_ASTEROIDS_TAB
            // 
            this.CV_ASTEROIDS_TAB.Location = new System.Drawing.Point(4, 22);
            this.CV_ASTEROIDS_TAB.Name = "CV_ASTEROIDS_TAB";
            this.CV_ASTEROIDS_TAB.Size = new System.Drawing.Size(758, 437);
            this.CV_ASTEROIDS_TAB.TabIndex = 1;
            this.CV_ASTEROIDS_TAB.Text = "Asteroids";
            this.CV_ASTEROIDS_TAB.UseVisualStyleBackColor = true;
            // 
            // CV_RIFTS_TAB
            // 
            this.CV_RIFTS_TAB.Location = new System.Drawing.Point(4, 22);
            this.CV_RIFTS_TAB.Name = "CV_RIFTS_TAB";
            this.CV_RIFTS_TAB.Size = new System.Drawing.Size(758, 437);
            this.CV_RIFTS_TAB.TabIndex = 2;
            this.CV_RIFTS_TAB.Text = "Rifts";
            this.CV_RIFTS_TAB.UseVisualStyleBackColor = true;
            // 
            // PluginsTab
            // 
            this.PluginsTab.Controls.Add(this.splitContainer5);
            this.PluginsTab.Location = new System.Drawing.Point(4, 22);
            this.PluginsTab.Name = "PluginsTab";
            this.PluginsTab.Size = new System.Drawing.Size(772, 469);
            this.PluginsTab.TabIndex = 5;
            this.PluginsTab.Text = "Plugins";
            this.PluginsTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer6);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer5.Size = new System.Drawing.Size(772, 469);
            this.splitContainer5.SplitterDistance = 213;
            this.splitContainer5.TabIndex = 1;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.BTN_Plugins_Enable);
            this.splitContainer6.Panel1.Controls.Add(this.SelectedPluginStateStatus);
            this.splitContainer6.Panel1.Controls.Add(this.SelectedPluginStateLabel);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.plugins_tab_pluginslist);
            this.splitContainer6.Size = new System.Drawing.Size(213, 469);
            this.splitContainer6.SplitterDistance = 68;
            this.splitContainer6.TabIndex = 0;
            // 
            // BTN_Plugins_Enable
            // 
            this.BTN_Plugins_Enable.Location = new System.Drawing.Point(25, 35);
            this.BTN_Plugins_Enable.Name = "BTN_Plugins_Enable";
            this.BTN_Plugins_Enable.Size = new System.Drawing.Size(156, 23);
            this.BTN_Plugins_Enable.TabIndex = 3;
            this.BTN_Plugins_Enable.Text = "Disable";
            this.BTN_Plugins_Enable.UseVisualStyleBackColor = true;
            this.BTN_Plugins_Enable.Click += new System.EventHandler(this.BTN_Plugins_Enable_Click);
            // 
            // SelectedPluginStateStatus
            // 
            this.SelectedPluginStateStatus.AutoSize = true;
            this.SelectedPluginStateStatus.ForeColor = System.Drawing.Color.Lime;
            this.SelectedPluginStateStatus.Location = new System.Drawing.Point(135, 9);
            this.SelectedPluginStateStatus.Name = "SelectedPluginStateStatus";
            this.SelectedPluginStateStatus.Size = new System.Drawing.Size(46, 13);
            this.SelectedPluginStateStatus.TabIndex = 1;
            this.SelectedPluginStateStatus.Text = "Enabled";
            // 
            // SelectedPluginStateLabel
            // 
            this.SelectedPluginStateLabel.AutoSize = true;
            this.SelectedPluginStateLabel.Location = new System.Drawing.Point(22, 9);
            this.SelectedPluginStateLabel.Name = "SelectedPluginStateLabel";
            this.SelectedPluginStateLabel.Size = new System.Drawing.Size(112, 13);
            this.SelectedPluginStateLabel.TabIndex = 0;
            this.SelectedPluginStateLabel.Text = "Selected Plugin State:";
            // 
            // plugins_tab_pluginslist
            // 
            this.plugins_tab_pluginslist.AutoArrange = false;
            this.plugins_tab_pluginslist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.stateColumn});
            this.plugins_tab_pluginslist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plugins_tab_pluginslist.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.plugins_tab_pluginslist.HideSelection = false;
            this.plugins_tab_pluginslist.Location = new System.Drawing.Point(0, 0);
            this.plugins_tab_pluginslist.MultiSelect = false;
            this.plugins_tab_pluginslist.Name = "plugins_tab_pluginslist";
            this.plugins_tab_pluginslist.ShowGroups = false;
            this.plugins_tab_pluginslist.ShowItemToolTips = true;
            this.plugins_tab_pluginslist.Size = new System.Drawing.Size(213, 397);
            this.plugins_tab_pluginslist.TabIndex = 0;
            this.plugins_tab_pluginslist.UseCompatibleStateImageBehavior = false;
            this.plugins_tab_pluginslist.View = System.Windows.Forms.View.Details;
            this.plugins_tab_pluginslist.SelectedIndexChanged += new System.EventHandler(this.plugins_tab_pluginslist_SelectedIndexChanged);
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 135;
            // 
            // stateColumn
            // 
            this.stateColumn.Text = "State";
            this.stateColumn.Width = 70;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.plugins_tab_informationTab);
            this.tabControl1.Controls.Add(this.plugins_tab_settingsTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(555, 469);
            this.tabControl1.TabIndex = 0;
            // 
            // plugins_tab_informationTab
            // 
            this.plugins_tab_informationTab.Controls.Add(this.plugins_tab_browser);
            this.plugins_tab_informationTab.Location = new System.Drawing.Point(4, 22);
            this.plugins_tab_informationTab.Name = "plugins_tab_informationTab";
            this.plugins_tab_informationTab.Padding = new System.Windows.Forms.Padding(3);
            this.plugins_tab_informationTab.Size = new System.Drawing.Size(547, 443);
            this.plugins_tab_informationTab.TabIndex = 0;
            this.plugins_tab_informationTab.Text = "Information";
            this.plugins_tab_informationTab.UseVisualStyleBackColor = true;
            // 
            // plugins_tab_browser
            // 
            this.plugins_tab_browser.AllowNavigation = false;
            this.plugins_tab_browser.AllowWebBrowserDrop = false;
            this.plugins_tab_browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plugins_tab_browser.IsWebBrowserContextMenuEnabled = false;
            this.plugins_tab_browser.Location = new System.Drawing.Point(3, 3);
            this.plugins_tab_browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.plugins_tab_browser.Name = "plugins_tab_browser";
            this.plugins_tab_browser.ScriptErrorsSuppressed = true;
            this.plugins_tab_browser.Size = new System.Drawing.Size(541, 437);
            this.plugins_tab_browser.TabIndex = 0;
            // 
            // plugins_tab_settingsTab
            // 
            this.plugins_tab_settingsTab.Controls.Add(this.PluginSettingsFormPanel);
            this.plugins_tab_settingsTab.Location = new System.Drawing.Point(4, 22);
            this.plugins_tab_settingsTab.Name = "plugins_tab_settingsTab";
            this.plugins_tab_settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.plugins_tab_settingsTab.Size = new System.Drawing.Size(547, 443);
            this.plugins_tab_settingsTab.TabIndex = 1;
            this.plugins_tab_settingsTab.Text = "Settings";
            this.plugins_tab_settingsTab.UseVisualStyleBackColor = true;
            // 
            // PluginSettingsFormPanel
            // 
            this.PluginSettingsFormPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PluginSettingsFormPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginSettingsFormPanel.Location = new System.Drawing.Point(3, 3);
            this.PluginSettingsFormPanel.Name = "PluginSettingsFormPanel";
            this.PluginSettingsFormPanel.Size = new System.Drawing.Size(541, 437);
            this.PluginSettingsFormPanel.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(150, 100);
            this.splitContainer1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 0;
            // 
            // ExtenderWebsite
            // 
            this.ExtenderWebsite.Controls.Add(this.hesw_Website);
            this.ExtenderWebsite.Location = new System.Drawing.Point(4, 22);
            this.ExtenderWebsite.Name = "ExtenderWebsite";
            this.ExtenderWebsite.Padding = new System.Windows.Forms.Padding(3);
            this.ExtenderWebsite.Size = new System.Drawing.Size(772, 469);
            this.ExtenderWebsite.TabIndex = 4;
            this.ExtenderWebsite.Text = "IRSE GitHub";
            this.ExtenderWebsite.UseVisualStyleBackColor = true;
            // 
            // hesw_Website
            // 
            this.hesw_Website.AllowWebBrowserDrop = false;
            this.hesw_Website.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hesw_Website.IsWebBrowserContextMenuEnabled = false;
            this.hesw_Website.Location = new System.Drawing.Point(3, 3);
            this.hesw_Website.MinimumSize = new System.Drawing.Size(20, 20);
            this.hesw_Website.Name = "hesw_Website";
            this.hesw_Website.ScriptErrorsSuppressed = true;
            this.hesw_Website.Size = new System.Drawing.Size(766, 463);
            this.hesw_Website.TabIndex = 0;
            this.hesw_Website.Url = new System.Uri("https://github.com/TheServerExtenders/InterstellarRiftServerExtender", System.UriKind.Absolute);
            this.hesw_Website.WebBrowserShortcutsEnabled = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 495);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(780, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // StatusBar
            // 
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // development_label
            // 
            this.development_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.development_label.AutoSize = true;
            this.development_label.ForeColor = System.Drawing.Color.Red;
            this.development_label.Location = new System.Drawing.Point(466, 500);
            this.development_label.Name = "development_label";
            this.development_label.Size = new System.Drawing.Size(0, 13);
            this.development_label.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kickToolStripMenuItem,
            this.banToolStripMenuItem,
            this.forgetToolStripMenuItem,
            this.killToolStripMenuItem,
            this.toggleAdminToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 114);
            this.contextMenuStrip1.Text = "Control";
            // 
            // kickToolStripMenuItem
            // 
            this.kickToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            this.kickToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.kickToolStripMenuItem.Text = "Kick";
            // 
            // banToolStripMenuItem
            // 
            this.banToolStripMenuItem.Name = "banToolStripMenuItem";
            this.banToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.banToolStripMenuItem.Text = "Ban";
            // 
            // forgetToolStripMenuItem
            // 
            this.forgetToolStripMenuItem.Name = "forgetToolStripMenuItem";
            this.forgetToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.forgetToolStripMenuItem.Text = "Forget";
            // 
            // killToolStripMenuItem
            // 
            this.killToolStripMenuItem.Name = "killToolStripMenuItem";
            this.killToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.killToolStripMenuItem.Text = "Kill";
            // 
            // toggleAdminToolStripMenuItem
            // 
            this.toggleAdminToolStripMenuItem.Name = "toggleAdminToolStripMenuItem";
            this.toggleAdminToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.toggleAdminToolStripMenuItem.Text = "Toggle Admin";
            // 
            // ExtenderGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(780, 517);
            this.Controls.Add(this.development_label);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExtenderGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtenderGui_FormClosing);
            this.Tabs.ResumeLayout(false);
            this.ServerTab.ResumeLayout(false);
            this.ServerContainer.Panel1.ResumeLayout(false);
            this.ServerContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ServerContainer)).EndInit();
            this.ServerContainer.ResumeLayout(false);
            this.server_server_Tabs.ResumeLayout(false);
            this.ServerConfig.ResumeLayout(false);
            this.ExtenderConfig.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.PlayersAndChatTab.ResumeLayout(false);
            this.ChatPlayerContainer.Panel1.ResumeLayout(false);
            this.ChatPlayerContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChatPlayerContainer)).EndInit();
            this.ChatPlayerContainer.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.cpc_chat_tabs.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.CommandVisualizerTab.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.PluginsTab.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.plugins_tab_informationTab.ResumeLayout(false);
            this.plugins_tab_settingsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ExtenderWebsite.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage ServerTab;
        private System.Windows.Forms.SplitContainer ServerContainer;
        private System.Windows.Forms.Button server_config_stopserver;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Button server_config_setdefaults;
        private System.Windows.Forms.Button server_config_cancel;
        private System.Windows.Forms.Button server_config_save;
        private System.Windows.Forms.ToolStripStatusLabel StatusBar;
        private System.Windows.Forms.TabPage PlayersAndChatTab;
        private System.Windows.Forms.SplitContainer ChatPlayerContainer;
        public System.Windows.Forms.Button server_config_startserver;
        private System.Windows.Forms.Button server_config_reload;
        private System.Windows.Forms.TabPage ObjectManipulationTab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView objectManipulation_treeview;
        private System.Windows.Forms.PropertyGrid objectManipulation_grid;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label sc_onlineplayers_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TabPage ExtenderWebsite;
        private System.Windows.Forms.WebBrowser hesw_Website;
        private System.Windows.Forms.TabPage PluginsTab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label server_hesNewsLabel;
        private System.Windows.Forms.Button serverconfig_checkForUpdates;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.Label SelectedPluginStateStatus;
        private System.Windows.Forms.Label SelectedPluginStateLabel;
        private System.Windows.Forms.ListView plugins_tab_pluginslist;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader stateColumn;
        private System.Windows.Forms.Label development_label;
        private System.Windows.Forms.TabPage CommandVisualizerTab;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label objectmanipulator_grid_isEditing;
        private System.Windows.Forms.Button BTN_Plugins_Enable;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage plugins_tab_informationTab;
        private System.Windows.Forms.WebBrowser plugins_tab_browser;
        private System.Windows.Forms.TabPage plugins_tab_settingsTab;
        private System.Windows.Forms.Panel PluginSettingsFormPanel;
        private System.Windows.Forms.TabControl server_server_Tabs;
        private System.Windows.Forms.TabPage ServerConfig;
        private System.Windows.Forms.PropertyGrid serverconfig_properties;
        private System.Windows.Forms.TabPage ExtenderConfig;
        private System.Windows.Forms.PropertyGrid extenderconfig_properties;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView sc_playerslist_listview;
        private System.Windows.Forms.ColumnHeader PlayerColumn;
        private System.Windows.Forms.ColumnHeader IsAdminColumn;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button pc_killplayer;
        private System.Windows.Forms.Button pc_toggleadmin;
        private System.Windows.Forms.Button pc_forgetplayer;
        private System.Windows.Forms.Button pc_banplayer;
        private System.Windows.Forms.Button pc_kickplayer;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabControl cpc_chat_tabs;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox cpc_chat_list;
        private System.Windows.Forms.Button cpc_chat_send;
        private System.Windows.Forms.TextBox cpc_messagebox;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage CV_PLAYERS_TAB;
        private System.Windows.Forms.TabPage CV_SYSTEMS_TAB;
        private System.Windows.Forms.TabPage CV_SHIPS_TAB;
        private System.Windows.Forms.TabPage CV_CREW_TAB;
        private System.Windows.Forms.TabPage CV_FLEET_TAB;
        private System.Windows.Forms.TabPage CV_ASTEROIDS_TAB;
        private System.Windows.Forms.TabPage CV_RIFTS_TAB;
        private System.Windows.Forms.ListView sc_playerbans_listview;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleAdminToolStripMenuItem;
    }
}

