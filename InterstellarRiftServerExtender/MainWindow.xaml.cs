using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IRSE
{
    partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.Title = Program.WindowTitle + " GUI";

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
        }
    }
}