
/* Copyright (C) Extra-Terrestrial Technologies - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by General Wrex <generalwrex@gmail.com>, 2014
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using IRSE.Managers;

namespace IRSE
{
	public partial class IRSEForm : Form
	{
		public IRSEForm()
		{
			InitializeComponent();
		}

		private void Tab_Chat_SendMessage_Click(object sender, EventArgs e)
		{
			if (Tab_Chat_Message.Text != "")
			{
				string message = String.Format("{0}: {1}", "Server", Tab_Chat_Message.Text);

				ServerInstance.Instance.Handlers.ChatHandler.SendMessageFromServer(message);

				AddToMessages(message);
			}
		}


		public void AddToMessages(string message)
		{
			Tab_Chat_Messages.Items.Add(message);

			Tab_Chat_Messages.SelectedIndex = Tab_Chat_Messages.Items.Count - 1;
		}

		private void Tab_Chat_Message_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter)
				return;

			if (Tab_Chat_Message.Text != "")
				return;
			
			string message = String.Format("{0}: {1}", "Server", Tab_Chat_Message.Text);

			ServerInstance.Instance.Handlers.ChatHandler.SendMessageFromServer(message);
			AddToMessages(message);	
		}
	}
}
