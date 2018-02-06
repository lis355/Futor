namespace Futor
{
    partial class PluginLine
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LabelPanel = new System.Windows.Forms.Panel();
            this.PluginNameLabel = new System.Windows.Forms.Label();
            this.MoveUpButton = new System.Windows.Forms.Button();
            this.UIButton = new System.Windows.Forms.Button();
            this.BypassButton = new System.Windows.Forms.Button();
            this.MoveDownButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.LabelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LabelPanel);
            this.groupBox1.Controls.Add(this.MoveUpButton);
            this.groupBox1.Controls.Add(this.UIButton);
            this.groupBox1.Controls.Add(this.BypassButton);
            this.groupBox1.Controls.Add(this.MoveDownButton);
            this.groupBox1.Controls.Add(this.RemoveButton);
            this.groupBox1.Location = new System.Drawing.Point(0, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 39);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // LabelPanel
            // 
            this.LabelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelPanel.Controls.Add(this.PluginNameLabel);
            this.LabelPanel.Location = new System.Drawing.Point(1, 9);
            this.LabelPanel.Name = "LabelPanel";
            this.LabelPanel.Size = new System.Drawing.Size(309, 27);
            this.LabelPanel.TabIndex = 6;
            this.LabelPanel.Click += new System.EventHandler(this.LabelPanel_Click);
            this.LabelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelPanel_Paint);
            // 
            // PluginNameLabel
            // 
            this.PluginNameLabel.AutoSize = true;
            this.PluginNameLabel.Location = new System.Drawing.Point(2, 7);
            this.PluginNameLabel.Name = "PluginNameLabel";
            this.PluginNameLabel.Size = new System.Drawing.Size(84, 13);
            this.PluginNameLabel.TabIndex = 4;
            this.PluginNameLabel.Text = "PLUGIN_NAME";
            this.PluginNameLabel.Click += new System.EventHandler(this.PluginNameLabel_Click);
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveUpButton.BackColor = System.Drawing.SystemColors.Control;
            this.MoveUpButton.Enabled = false;
            this.MoveUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MoveUpButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.MoveUpButton.Image = global::Futor.Properties.Resources.sort_up_16;
            this.MoveUpButton.Location = new System.Drawing.Point(442, 11);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(23, 23);
            this.MoveUpButton.TabIndex = 5;
            this.MoveUpButton.TabStop = false;
            this.MoveUpButton.UseVisualStyleBackColor = false;
            this.MoveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // UIButton
            // 
            this.UIButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UIButton.BackColor = System.Drawing.SystemColors.Control;
            this.UIButton.Enabled = false;
            this.UIButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UIButton.Location = new System.Drawing.Point(316, 11);
            this.UIButton.Name = "UIButton";
            this.UIButton.Size = new System.Drawing.Size(32, 23);
            this.UIButton.TabIndex = 3;
            this.UIButton.TabStop = false;
            this.UIButton.Text = "UI";
            this.UIButton.UseVisualStyleBackColor = false;
            this.UIButton.Click += new System.EventHandler(this.UIButton_Click);
            // 
            // BypassButton
            // 
            this.BypassButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BypassButton.BackColor = System.Drawing.SystemColors.Control;
            this.BypassButton.Enabled = false;
            this.BypassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BypassButton.Location = new System.Drawing.Point(354, 11);
            this.BypassButton.Name = "BypassButton";
            this.BypassButton.Size = new System.Drawing.Size(53, 23);
            this.BypassButton.TabIndex = 2;
            this.BypassButton.TabStop = false;
            this.BypassButton.Text = "Bypass";
            this.BypassButton.UseVisualStyleBackColor = false;
            this.BypassButton.Click += new System.EventHandler(this.BypassButton_Click);
            // 
            // MoveDownButton
            // 
            this.MoveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveDownButton.BackColor = System.Drawing.SystemColors.Control;
            this.MoveDownButton.Enabled = false;
            this.MoveDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MoveDownButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.MoveDownButton.Image = global::Futor.Properties.Resources.sort_down_16;
            this.MoveDownButton.Location = new System.Drawing.Point(413, 11);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(23, 23);
            this.MoveDownButton.TabIndex = 1;
            this.MoveDownButton.TabStop = false;
            this.MoveDownButton.UseVisualStyleBackColor = false;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveButton.BackColor = System.Drawing.SystemColors.Control;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.Image = global::Futor.Properties.Resources.delete_16;
            this.RemoveButton.Location = new System.Drawing.Point(471, 11);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(23, 23);
            this.RemoveButton.TabIndex = 0;
            this.RemoveButton.TabStop = false;
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // PluginLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PluginLine";
            this.Size = new System.Drawing.Size(500, 33);
            this.groupBox1.ResumeLayout(false);
            this.LabelPanel.ResumeLayout(false);
            this.LabelPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label PluginNameLabel;
        private System.Windows.Forms.Button UIButton;
        private System.Windows.Forms.Button BypassButton;
        private System.Windows.Forms.Button MoveDownButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button MoveUpButton;
        private System.Windows.Forms.Panel LabelPanel;
    }
}
