namespace Disk_Utilities
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diskSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Go = new System.Windows.Forms.Button();
            this.SourcePath = new System.Windows.Forms.TextBox();
            this.ChangeFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ChangePath2 = new System.Windows.Forms.Button();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.ComparePath = new System.Windows.Forms.TextBox();
            this.SyncRight = new System.Windows.Forms.Button();
            this.SyncLeft = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diskSpaceToolStripMenuItem,
            this.compareToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // diskSpaceToolStripMenuItem
            // 
            this.diskSpaceToolStripMenuItem.Checked = true;
            this.diskSpaceToolStripMenuItem.CheckOnClick = true;
            this.diskSpaceToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.diskSpaceToolStripMenuItem.Name = "diskSpaceToolStripMenuItem";
            this.diskSpaceToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.diskSpaceToolStripMenuItem.Text = "Disk Space";
            this.diskSpaceToolStripMenuItem.Click += new System.EventHandler(this.diskSpaceToolStripMenuItem_Click);
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.CheckOnClick = true;
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.compareToolStripMenuItem.Text = "Compare";
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(12, 53);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(390, 354);
            this.treeView1.TabIndex = 18;
            this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView1.MouseCaptureChanged += new System.EventHandler(this.treeView1_MouseCaptureChanged);
            this.treeView1.Click += new System.EventHandler(this.treeView1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(0, 443);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(823, 12);
            this.progressBar1.TabIndex = 20;
            this.progressBar1.Visible = false;
            // 
            // Go
            // 
            this.Go.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Go.Location = new System.Drawing.Point(175, 413);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(77, 24);
            this.Go.TabIndex = 17;
            this.Go.Text = "Go";
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // SourcePath
            // 
            this.SourcePath.Location = new System.Drawing.Point(12, 27);
            this.SourcePath.Name = "SourcePath";
            this.SourcePath.Size = new System.Drawing.Size(366, 20);
            this.SourcePath.TabIndex = 15;
            this.SourcePath.Text = "SourcePath";
            // 
            // ChangeFolder
            // 
            this.ChangeFolder.Location = new System.Drawing.Point(375, 27);
            this.ChangeFolder.Name = "ChangeFolder";
            this.ChangeFolder.Size = new System.Drawing.Size(27, 23);
            this.ChangeFolder.TabIndex = 16;
            this.ChangeFolder.Text = "...";
            this.ChangeFolder.Click += new System.EventHandler(this.ChangeFolder_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(823, 22);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // ChangePath2
            // 
            this.ChangePath2.Location = new System.Drawing.Point(784, 27);
            this.ChangePath2.Name = "ChangePath2";
            this.ChangePath2.Size = new System.Drawing.Size(27, 23);
            this.ChangePath2.TabIndex = 23;
            this.ChangePath2.Text = "...";
            this.ChangePath2.Click += new System.EventHandler(this.ChangeFolder_Click);
            // 
            // treeView2
            // 
            this.treeView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView2.Location = new System.Drawing.Point(421, 53);
            this.treeView2.Name = "treeView2";
            this.treeView2.ShowNodeToolTips = true;
            this.treeView2.Size = new System.Drawing.Size(390, 354);
            this.treeView2.TabIndex = 24;
            this.treeView2.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterCollapse);
            this.treeView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView2_MouseClick);
            this.treeView2.DoubleClick += new System.EventHandler(this.treeView2_DoubleClick);
            this.treeView2.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterExpand);
            this.treeView2.MouseCaptureChanged += new System.EventHandler(this.treeView2_MouseCaptureChanged);
            this.treeView2.Click += new System.EventHandler(this.treeView2_Click);
            // 
            // ComparePath
            // 
            this.ComparePath.Location = new System.Drawing.Point(421, 27);
            this.ComparePath.Name = "ComparePath";
            this.ComparePath.Size = new System.Drawing.Size(367, 20);
            this.ComparePath.TabIndex = 22;
            this.ComparePath.Text = "ComparePath";
            // 
            // SyncRight
            // 
            this.SyncRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SyncRight.Location = new System.Drawing.Point(314, 413);
            this.SyncRight.Name = "SyncRight";
            this.SyncRight.Size = new System.Drawing.Size(88, 24);
            this.SyncRight.TabIndex = 25;
            this.SyncRight.Text = "Sync Right ->";
            this.SyncRight.Visible = false;
            this.SyncRight.Click += new System.EventHandler(this.SyncRight_Click);
            // 
            // SyncLeft
            // 
            this.SyncLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SyncLeft.Location = new System.Drawing.Point(421, 413);
            this.SyncLeft.Name = "SyncLeft";
            this.SyncLeft.Size = new System.Drawing.Size(88, 24);
            this.SyncLeft.TabIndex = 26;
            this.SyncLeft.Text = "<- Sync Left";
            this.SyncLeft.Click += new System.EventHandler(this.SyncLeft_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 480);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.SyncLeft);
            this.Controls.Add(this.SyncRight);
            this.Controls.Add(this.ChangePath2);
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.ComparePath);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ChangeFolder);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.SourcePath);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Disk Utilities";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diskSpaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button Go;
        private System.Windows.Forms.TextBox SourcePath;
        private System.Windows.Forms.Button ChangeFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button ChangePath2;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.TextBox ComparePath;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button SyncRight;
        private System.Windows.Forms.Button SyncLeft;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}

