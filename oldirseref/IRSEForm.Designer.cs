namespace IRSE
{
	partial class IRSEForm
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusbar = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.Tab_Chat_Messages = new System.Windows.Forms.ListBox();
			this.Tab_Chat_Players = new System.Windows.Forms.ListBox();
			this.Tab_Chat_Message = new System.Windows.Forms.TextBox();
			this.Tab_Chat_SendMessage = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.statusStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusbar});
			this.statusStrip1.Location = new System.Drawing.Point(0, 365);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(802, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statusbar
			// 
			this.statusbar.Name = "statusbar";
			this.statusbar.Size = new System.Drawing.Size(118, 17);
			this.statusbar.Text = "toolStripStatusLabel1";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(802, 365);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(794, 339);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Chat";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.Tab_Chat_Message);
			this.splitContainer1.Panel2.Controls.Add(this.Tab_Chat_SendMessage);
			this.splitContainer1.Size = new System.Drawing.Size(788, 333);
			this.splitContainer1.SplitterDistance = 296;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.Tab_Chat_Messages);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.Tab_Chat_Players);
			this.splitContainer2.Size = new System.Drawing.Size(788, 296);
			this.splitContainer2.SplitterDistance = 570;
			this.splitContainer2.TabIndex = 0;
			// 
			// Tab_Chat_Messages
			// 
			this.Tab_Chat_Messages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tab_Chat_Messages.FormattingEnabled = true;
			this.Tab_Chat_Messages.Location = new System.Drawing.Point(0, 0);
			this.Tab_Chat_Messages.Name = "Tab_Chat_Messages";
			this.Tab_Chat_Messages.Size = new System.Drawing.Size(570, 296);
			this.Tab_Chat_Messages.TabIndex = 0;
			// 
			// Tab_Chat_Players
			// 
			this.Tab_Chat_Players.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tab_Chat_Players.FormattingEnabled = true;
			this.Tab_Chat_Players.Location = new System.Drawing.Point(0, 0);
			this.Tab_Chat_Players.Name = "Tab_Chat_Players";
			this.Tab_Chat_Players.Size = new System.Drawing.Size(214, 296);
			this.Tab_Chat_Players.TabIndex = 0;
			// 
			// Tab_Chat_Message
			// 
			this.Tab_Chat_Message.Location = new System.Drawing.Point(5, 7);
			this.Tab_Chat_Message.Name = "Tab_Chat_Message";
			this.Tab_Chat_Message.Size = new System.Drawing.Size(696, 20);
			this.Tab_Chat_Message.TabIndex = 1;
			this.Tab_Chat_Message.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tab_Chat_Message_KeyDown);
			// 
			// Tab_Chat_SendMessage
			// 
			this.Tab_Chat_SendMessage.Location = new System.Drawing.Point(707, 5);
			this.Tab_Chat_SendMessage.Name = "Tab_Chat_SendMessage";
			this.Tab_Chat_SendMessage.Size = new System.Drawing.Size(75, 23);
			this.Tab_Chat_SendMessage.TabIndex = 0;
			this.Tab_Chat_SendMessage.Text = "Send";
			this.Tab_Chat_SendMessage.UseVisualStyleBackColor = true;
			this.Tab_Chat_SendMessage.Click += new System.EventHandler(this.Tab_Chat_SendMessage_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(794, 339);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "ToCome";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// IRSEForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(802, 387);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "IRSEForm";
			this.Text = "Interstellar Rift Server Extender V0.2 ALPHA";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusbar;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ListBox Tab_Chat_Messages;
		private System.Windows.Forms.ListBox Tab_Chat_Players;
		private System.Windows.Forms.TextBox Tab_Chat_Message;
		private System.Windows.Forms.Button Tab_Chat_SendMessage;
		private System.Windows.Forms.TabPage tabPage2;
	}
}