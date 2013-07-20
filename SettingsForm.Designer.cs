namespace Disk_Utilities
{
    partial class SettingsForm
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
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.FolderWarningSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AddRegistrySettings = new System.Windows.Forms.LinkLabel();
            this.RemoveRegistrySettings = new System.Windows.Forms.LinkLabel();
            this.DisplayEmptyFolders = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIgnoreFolders = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.FolderWarningSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Ok.Location = new System.Drawing.Point(66, 232);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancel.Location = new System.Drawing.Point(178, 232);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // FolderWarningSize
            // 
            this.FolderWarningSize.Location = new System.Drawing.Point(9, 38);
            this.FolderWarningSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.FolderWarningSize.Name = "FolderWarningSize";
            this.FolderWarningSize.Size = new System.Drawing.Size(120, 20);
            this.FolderWarningSize.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Folder Warning Size";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtIgnoreFolders);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.AddRegistrySettings);
            this.groupBox1.Controls.Add(this.RemoveRegistrySettings);
            this.groupBox1.Controls.Add(this.DisplayEmptyFolders);
            this.groupBox1.Controls.Add(this.FolderWarningSize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 214);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // AddRegistrySettings
            // 
            this.AddRegistrySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddRegistrySettings.AutoSize = true;
            this.AddRegistrySettings.Location = new System.Drawing.Point(6, 161);
            this.AddRegistrySettings.Name = "AddRegistrySettings";
            this.AddRegistrySettings.Size = new System.Drawing.Size(108, 13);
            this.AddRegistrySettings.TabIndex = 7;
            this.AddRegistrySettings.TabStop = true;
            this.AddRegistrySettings.Text = "Add Registry Settings";
            this.AddRegistrySettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddRegistrySettings_LinkClicked);
            // 
            // RemoveRegistrySettings
            // 
            this.RemoveRegistrySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemoveRegistrySettings.AutoSize = true;
            this.RemoveRegistrySettings.Location = new System.Drawing.Point(6, 188);
            this.RemoveRegistrySettings.Name = "RemoveRegistrySettings";
            this.RemoveRegistrySettings.Size = new System.Drawing.Size(129, 13);
            this.RemoveRegistrySettings.TabIndex = 6;
            this.RemoveRegistrySettings.TabStop = true;
            this.RemoveRegistrySettings.Text = "Remove Registry Settings";
            this.RemoveRegistrySettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RemoveRegistrySettings_LinkClicked);
            // 
            // DisplayEmptyFolders
            // 
            this.DisplayEmptyFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplayEmptyFolders.AutoSize = true;
            this.DisplayEmptyFolders.Location = new System.Drawing.Point(9, 127);
            this.DisplayEmptyFolders.Name = "DisplayEmptyFolders";
            this.DisplayEmptyFolders.Size = new System.Drawing.Size(129, 17);
            this.DisplayEmptyFolders.TabIndex = 4;
            this.DisplayEmptyFolders.Text = "Display Empty Folders";
            this.DisplayEmptyFolders.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ignore Folders";
            // 
            // txtIgnoreFolders
            // 
            this.txtIgnoreFolders.Location = new System.Drawing.Point(9, 88);
            this.txtIgnoreFolders.Name = "txtIgnoreFolders";
            this.txtIgnoreFolders.Size = new System.Drawing.Size(253, 20);
            this.txtIgnoreFolders.TabIndex = 9;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 265);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.FolderWarningSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.NumericUpDown FolderWarningSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox DisplayEmptyFolders;
        private System.Windows.Forms.LinkLabel RemoveRegistrySettings;
        private System.Windows.Forms.LinkLabel AddRegistrySettings;
        private System.Windows.Forms.TextBox txtIgnoreFolders;
        private System.Windows.Forms.Label label2;
    }
}