
using System.Windows.Forms;

namespace IRSE.GUI.Forms.Browser
{
    public partial class BrowserForm : Form
    {


        public BrowserForm()
        {
            InitializeComponent();
        }

        private void BrowserForm_Load(object sender, System.EventArgs e)
        {

            
        }

        private void BrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            e.Cancel = true;
        }
    }
}