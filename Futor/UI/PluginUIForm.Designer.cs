namespace Futor
{
    partial class PluginUIForm
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
            this.ViewPanel = new System.Windows.Forms.Panel();
            this.PresetsPanel = new System.Windows.Forms.Panel();
            this.AddButton = new System.Windows.Forms.Button();
            this.PluginsLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PresetsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewPanel
            // 
            this.ViewPanel.Location = new System.Drawing.Point(0, 0);
            this.ViewPanel.Name = "ViewPanel";
            this.ViewPanel.Size = new System.Drawing.Size(448, 145);
            this.ViewPanel.TabIndex = 0;
            // 
            // PresetsPanel
            // 
            this.PresetsPanel.AutoScroll = true;
            this.PresetsPanel.Controls.Add(this.AddButton);
            this.PresetsPanel.Controls.Add(this.PluginsLayoutPanel);
            this.PresetsPanel.Location = new System.Drawing.Point(25, 182);
            this.PresetsPanel.Name = "PresetsPanel";
            this.PresetsPanel.Size = new System.Drawing.Size(528, 351);
            this.PresetsPanel.TabIndex = 3;
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.Location = new System.Drawing.Point(3, 315);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(521, 33);
            this.AddButton.TabIndex = 9;
            this.AddButton.TabStop = false;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            // 
            // PluginsLayoutPanel
            // 
            this.PluginsLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginsLayoutPanel.AutoScroll = true;
            this.PluginsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.PluginsLayoutPanel.Margin = new System.Windows.Forms.Padding(6);
            this.PluginsLayoutPanel.Name = "PluginsLayoutPanel";
            this.PluginsLayoutPanel.Size = new System.Drawing.Size(525, 312);
            this.PluginsLayoutPanel.TabIndex = 2;
            // 
            // PluginUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 566);
            this.Controls.Add(this.PresetsPanel);
            this.Controls.Add(this.ViewPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "PluginUIForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLUGIN_NAME";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditorForm_FormClosed);
            this.PresetsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ViewPanel;
        private System.Windows.Forms.Panel PresetsPanel;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.FlowLayoutPanel PluginsLayoutPanel;
    }
}