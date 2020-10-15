namespace IRSE.GUI.Forms.Browser
{
    partial class BrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserForm));
            this.BrowserSplitter = new System.Windows.Forms.SplitContainer();
            this.BrowserForm_PluginTree = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pluginBrowser_Import = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.BrowserSplitter)).BeginInit();
            this.BrowserSplitter.Panel1.SuspendLayout();
            this.BrowserSplitter.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BrowserSplitter
            // 
            resources.ApplyResources(this.BrowserSplitter, "BrowserSplitter");
            this.BrowserSplitter.Name = "BrowserSplitter";
            // 
            // BrowserSplitter.Panel1
            // 
            this.BrowserSplitter.Panel1.Controls.Add(this.BrowserForm_PluginTree);
            // 
            // BrowserForm_PluginTree
            // 
            this.BrowserForm_PluginTree.CheckBoxes = true;
            resources.ApplyResources(this.BrowserForm_PluginTree, "BrowserForm_PluginTree");
            this.BrowserForm_PluginTree.Name = "BrowserForm_PluginTree";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.pluginBrowser_Import,
            this.toolStripSeparator2});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Name = "toolStripButton1";
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // pluginBrowser_Import
            // 
            this.pluginBrowser_Import.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.pluginBrowser_Import.Name = "pluginBrowser_Import";
            resources.ApplyResources(this.pluginBrowser_Import, "pluginBrowser_Import");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // BrowserForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BrowserSplitter);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BrowserForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BrowserForm_FormClosing);
            this.Load += new System.EventHandler(this.BrowserForm_Load);
            this.BrowserSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BrowserSplitter)).EndInit();
            this.BrowserSplitter.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer BrowserSplitter;
        private System.Windows.Forms.TreeView BrowserForm_PluginTree;
        private System.Windows.Forms.ToolStripButton pluginBrowser_Import;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}