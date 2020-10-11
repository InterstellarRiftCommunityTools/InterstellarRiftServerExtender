using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRSEDiscordChatBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            propertyGrid1.SelectedObject = MyConfig.Instance.Settings;

        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = MyConfig.Instance.LoadConfiguration() ? "Configuration Loaded!" : "Could not load configuration";
            propertyGrid1.SelectedObject = MyConfig.Instance.Settings;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = MyConfig.Instance.SaveConfiguration() ? "Configuration Saved!" : "Could not save configuration";
            propertyGrid1.SelectedObject = MyConfig.Instance.Settings;
        }
    }
}
