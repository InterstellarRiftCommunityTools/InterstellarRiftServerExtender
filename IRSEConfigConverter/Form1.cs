using Game.Configuration;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using IRSE;

namespace IRSEConfigConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            


            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            folderBrowserDialog1.SelectedPath = path;
            textBox1.Text = path;

        }

        private void browse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox1.Text;
                          
            folderBrowserDialog1.Description = "Select Path for the compiled script to go...";
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServerConfigConverter.Result result = ServerConfigConverter.Instance.BuildConfig(textBox1.Text);

            if (result.NeedsDialog) {



            }
            else
            {
                statusStrip1.Text = result.Message;
            }

            
        }
    }

}