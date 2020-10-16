using CefSharp;
using CefSharp.Web;
using CefSharp.WinForms;
using System.Windows.Forms;

namespace IRSE.GUI.Forms.Browser
{
    public partial class BrowserForm : Form
    {
        private ChromiumWebBrowser chrome;

        public BrowserForm()
        {
            InitializeComponent();
        }

        private void BrowserForm_Load(object sender, System.EventArgs e)
        {
            //CefSettings settings = new CefSettings();
            //Cef.Initialize(settings);
            //var Text = "<a class=\"vglnk\" href=\"https://foxlearn.com\" rel=\"nofollow\"><span>https</span><span>://</span><span>foxlearn</span><span>.</span><span>com</span></a>";
            
            //chrome = new ChromiumWebBrowser("");
            //chrome.Dock = DockStyle.Fill;

            //chrome.LoadHtml(Text);


            //splitContainer1.Panel2.Controls.Add(chrome);
            
        }

        private void BrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            e.Cancel = true;
        }
    }
}