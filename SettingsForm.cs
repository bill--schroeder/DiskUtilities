using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Disk_Utilities
{
    public partial class SettingsForm : Form
    {
        #region Member Variables

        public DiskUtilitiesClass _Settings = null;

        #endregion Member Variables


        #region Constructor
        public SettingsForm(DiskUtilitiesClass diskUtilities)
        {
            InitializeComponent();

            _Settings = diskUtilities;

            this.FolderWarningSize.Value = _Settings.FolderWarningSize;
            this.DisplayEmptyFolders.Checked = _Settings.DisplayEmptyFolders;
            this.txtIgnoreFolders.Text = _Settings.IgnoreFolders;
        }
        #endregion Constructor


        #region Ok_Click
        private void Ok_Click(object sender, EventArgs e)
        {
            _Settings.FolderWarningSize = int.Parse(this.FolderWarningSize.Value.ToString());
            _Settings.DisplayEmptyFolders = this.DisplayEmptyFolders.Checked;
            _Settings.IgnoreFolders = this.txtIgnoreFolders.Text.Trim();

            this.Close();
        }
        #endregion Ok_Click


        #region Cancel_Click
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Cancel_Click


        private void RemoveRegistrySettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _Settings.RemoveRegistrySettings();

            MessageBox.Show("Settings have been removed.", Application.ProductName);
        }

        private void AddRegistrySettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _Settings.AddRegistrySettings();

            MessageBox.Show("Settings have been added.", Application.ProductName);
        }

    }
}