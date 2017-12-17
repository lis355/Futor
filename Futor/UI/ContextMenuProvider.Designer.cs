namespace Futor
{
    partial class ContextMenuProvider
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PluginStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveUpStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveDownStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.BypassStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.RemoveStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPluginStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.OptionsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextMenuStrip
            // 
            this.ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PluginStripMenuItem,
            this.AddPluginStripMenuItem,
            this.toolStripSeparator2,
            this.OptionsStripMenuItem,
            this.toolStripSeparator1,
            this.ExitStripMenuItem});
            this.ContextMenuStrip.Name = "ContextMenuStrip";
            this.ContextMenuStrip.Size = new System.Drawing.Size(153, 126);
            // 
            // PluginStripMenuItem
            // 
            this.PluginStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveUpStripMenuItem,
            this.MoveDownStripMenuItem,
            this.toolStripSeparator4,
            this.BypassStripMenuItem,
            this.toolStripSeparator5,
            this.RemoveStripMenuItem});
            this.PluginStripMenuItem.Name = "PluginStripMenuItem";
            this.PluginStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PluginStripMenuItem.Text = "PLUGIN";
            // 
            // MoveUpStripMenuItem
            // 
            this.MoveUpStripMenuItem.Name = "MoveUpStripMenuItem";
            this.MoveUpStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.MoveUpStripMenuItem.Text = "Move up";
            // 
            // MoveDownStripMenuItem
            // 
            this.MoveDownStripMenuItem.Name = "MoveDownStripMenuItem";
            this.MoveDownStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.MoveDownStripMenuItem.Text = "Move down";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(190, 6);
            // 
            // BypassStripMenuItem
            // 
            this.BypassStripMenuItem.CheckOnClick = true;
            this.BypassStripMenuItem.Name = "BypassStripMenuItem";
            this.BypassStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.NumPad1)));
            this.BypassStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.BypassStripMenuItem.Text = "Bypass";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(190, 6);
            // 
            // RemoveStripMenuItem
            // 
            this.RemoveStripMenuItem.Name = "RemoveStripMenuItem";
            this.RemoveStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.RemoveStripMenuItem.Text = "Remove";
            // 
            // AddPluginStripMenuItem
            // 
            this.AddPluginStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.AddPluginStripMenuItem.Name = "AddPluginStripMenuItem";
            this.AddPluginStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddPluginStripMenuItem.Text = "Add plugin...";
            this.AddPluginStripMenuItem.Click += new System.EventHandler(this.AddPluginStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // OptionsStripMenuItem
            // 
            this.OptionsStripMenuItem.Name = "OptionsStripMenuItem";
            this.OptionsStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.OptionsStripMenuItem.Text = "Options";
            this.OptionsStripMenuItem.Click += new System.EventHandler(this.OptionsStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // ExitStripMenuItem
            // 
            this.ExitStripMenuItem.Name = "ExitStripMenuItem";
            this.ExitStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExitStripMenuItem.Text = "Exit";
            this.ExitStripMenuItem.Click += new System.EventHandler(this.ExitStripMenuItem_Click);
            // 
            // ContextMenuProvider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ContextMenuProvider";
            this.Size = new System.Drawing.Size(600, 400);
            this.ContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip ContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OptionsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PluginStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddPluginStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MoveUpStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveDownStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem BypassStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem RemoveStripMenuItem;
    }
}
