namespace Futor
{
    partial class IconMenu
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
            this.ContextRightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BypassAllStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.InputDeviceStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OutputDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextRightMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextRightMenu
            // 
            this.ContextRightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PitchToolStripMenuItem,
            this.BypassAllStripMenuItem,
            this.toolStripSeparator2,
            this.InputDeviceStripMenuItem,
            this.OutputDeviceToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExitStripMenuItem});
            this.ContextRightMenu.Name = "ContextMenuStrip";
            this.ContextRightMenu.Size = new System.Drawing.Size(181, 148);
            // 
            // PitchToolStripMenuItem
            // 
            this.PitchToolStripMenuItem.Name = "PitchToolStripMenuItem";
            this.PitchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PitchToolStripMenuItem.Text = "Pitch";
            // 
            // BypassAllStripMenuItem
            // 
            this.BypassAllStripMenuItem.CheckOnClick = true;
            this.BypassAllStripMenuItem.Name = "BypassAllStripMenuItem";
            this.BypassAllStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BypassAllStripMenuItem.Text = "Bypass All";
            this.BypassAllStripMenuItem.Click += new System.EventHandler(this.BypassAllStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // InputDeviceStripMenuItem
            // 
            this.InputDeviceStripMenuItem.Name = "InputDeviceStripMenuItem";
            this.InputDeviceStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.InputDeviceStripMenuItem.Text = "Input Device";
            // 
            // OutputDeviceToolStripMenuItem
            // 
            this.OutputDeviceToolStripMenuItem.Name = "OutputDeviceToolStripMenuItem";
            this.OutputDeviceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.OutputDeviceToolStripMenuItem.Text = "Output Device";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // ExitStripMenuItem
            // 
            this.ExitStripMenuItem.Name = "ExitStripMenuItem";
            this.ExitStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ExitStripMenuItem.Text = "Exit";
            this.ExitStripMenuItem.Click += new System.EventHandler(this.ExitStripMenuItem_Click);
            // 
            // IconMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "IconMenu";
            this.Size = new System.Drawing.Size(600, 400);
            this.ContextRightMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip ContextRightMenu;
        private System.Windows.Forms.ToolStripMenuItem InputDeviceStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem BypassAllStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OutputDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PitchToolStripMenuItem;
    }
}
