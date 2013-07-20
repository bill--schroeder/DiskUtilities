using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; // DllImport

namespace Disk_Utilities
{
    public partial class MainForm : Form
    {
        #region Member Variables

        // Make sure you have the correct using clause to see DllImport:
        // using System.Runtime.InteropServices;
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
                
        private DiskUtilitiesClass _ds = null;
        private RegistryClass _reg = new RegistryClass();
        private string[] _Arguments;

        #endregion Member Variables


        #region Constructor/Destructor

        private void Main_Load(object sender, System.EventArgs e)
        {
            this.diskSpaceToolStripMenuItem.Checked = true;
            this.Width = this.Width / 2;
            this.Text = Application.ProductName + " - " + "Disk Space";

            string Path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf(@"\") + 1);

            toolStripStatusLabel1.Text = "";

            string CurrentFolder = @"C:\";
            if (_Arguments.Length > 0)
            {
                CurrentFolder = Environment.CurrentDirectory;
                if (_Arguments.Length > 1)
                {
                    if (_Arguments[1].IndexOf("~") < 0)
                        CurrentFolder = _Arguments[1];
                }
            }
            SourcePath.Text = CurrentFolder;

            ComparePath.Text = @"C:\";


            //// this is ONLY for testing
            //SourcePath.Text = @"C:\temp\test1\";
            //ComparePath.Text = @"C:\temp\test2\";
            //this.diskSpaceToolStripMenuItem.Checked = false;
            //this.compareToolStripMenuItem.Checked = true;
            //this.Width = this.Width * 2;
            //SyncRight.Visible = true;


            CreateTreeViewMenu();

            // Do we have any auto run parameters
            if (_Arguments.Length > 0)
            {
                if (_Arguments[0].Equals("/DiskSpace"))
                {
                    this.Show();
                    Application.DoEvents();

                    GetDiskSpaceUsage();
                }
                else if (_Arguments[0].Equals("/CompareSetSource"))
                {
                    StreamWriter sw = new StreamWriter(Path + "Compare.dat");
                    sw.WriteLine(CurrentFolder);
                    sw.Close();
                    sw = null;

                    _reg.AddContextMenu("Compare to Source", Application.ExecutablePath, " /CompareToSource %1");

                    Exit();
                }
                else if (_Arguments[0].Equals("/CompareToSource"))
                {
                    this.Show();
                    Application.DoEvents();

                    _reg.RemoveContextMenu("Compare to Source", Application.ExecutablePath);

                    ComparePath.Text = CurrentFolder;

                    StreamReader sr = new StreamReader(Path + "Compare.dat");
                    CurrentFolder = sr.ReadLine();
                    sr = null;

                    SourcePath.Text = CurrentFolder;

                    this.diskSpaceToolStripMenuItem.Checked = false;
                    this.compareToolStripMenuItem.Checked = true;
                    this.Width = this.Width * 2;
                    SyncRight.Visible = true;

                    Compare();
                }
            }
        }

        //public Main()
        //{
        //    //
        //    // Required for Windows Form Designer support
        //    //
        //    InitializeComponent();

        //    _ds = new DiskUtilitiesClass(this);
        //}

        public MainForm(string[] Arguments)
        {
            // set the arguments to a local variable
            _Arguments = Arguments;

            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _ds = new DiskUtilitiesClass(this);
        }

        #endregion Constructor/Destructor


        #region Form Buttons

        #region ChangeFolder_Click
        private void ChangeFolder_Click(object sender, EventArgs e)
        {
            string button = ((Button)sender).Name;
            if(button.EndsWith("2"))
            {
                folderBrowserDialog1.SelectedPath = this.ComparePath.Text;
                folderBrowserDialog1.ShowDialog();
                ComparePath.Text = folderBrowserDialog1.SelectedPath;
            }
            else
            {
                folderBrowserDialog1.SelectedPath = SourcePath.Text;
                folderBrowserDialog1.ShowDialog();
                SourcePath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        #endregion ChangeFolder_Click


        #region Go_Click
        private void Go_Click(object sender, EventArgs e)
        {
            if (this.Go.Text == "Go")
            {
                // which menu item is selected
                if (this.diskSpaceToolStripMenuItem.Checked == true)
                    GetDiskSpaceUsage();
                else if (this.compareToolStripMenuItem.Checked == true)
                {
                    Compare();
                }
                else
                {
                    // no menu item check
                }
            }
            else
            {
                this.Go.Text = "Stopping";
                _ds.ExitProcess = true;
            }
        }
        #endregion Go_Click


        #region SyncRight_Click
        private void SyncRight_Click(object sender, EventArgs e)
        {
            Sync(1);
        }
        #endregion SyncRight_Click


        #region SyncLeft_Click
        private void SyncLeft_Click(object sender, EventArgs e)
        {
            Sync(2);
        }
        #endregion SyncLeft_Click

        #endregion Form Buttons


        #region Menu Items

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();

            about.ShowDialog();

            about = null;
        }


        private void diskSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (compareToolStripMenuItem.Checked == true)
                this.Width = this.Width / 2;

            this.Text = Application.ProductName + " - " + "Disk Space";
            compareToolStripMenuItem.Checked = false;
            SyncRight.Visible = false;
            CreateTreeViewMenu();
        }


        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(diskSpaceToolStripMenuItem.Checked == true)
                this.Width = this.Width * 2;

            this.Text = Application.ProductName + " - " + "Compare";
            diskSpaceToolStripMenuItem.Checked = false;
            SyncRight.Visible = true;
            CreateTreeViewMenu();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(_ds);

            settings.ShowDialog();

            settings = null;
        }

        private void zealocity2006ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string target = "www.zealocity.com";
            System.Diagnostics.Process.Start(target);
        }

        #endregion Menu Items


        #region TreeView Events

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.diskSpaceToolStripMenuItem.Checked == true)
                {
                    //string Path = treeView1.SelectedNode.Text;
                    //Path = Path.Substring(0, Path.IndexOf("-")).Trim();

                    string Path = treeView1.SelectedNode.ToolTipText;

                    SourcePath.Text = Path;
                }
                else if (this.compareToolStripMenuItem.Checked == true)
                {
                    ChangeTree(1);
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.compareToolStripMenuItem.Checked == true)
                {
                    ChangeTree(1);
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
        }


        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.compareToolStripMenuItem.Checked == true)
                {
                    ChangeTree(1);
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
        }


        private void treeView2_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.compareToolStripMenuItem.Checked == true)
                {
                    ChangeTree(2);
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
        }

        private void treeView2_AfterExpand(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.compareToolStripMenuItem.Checked == true)
                {
                    ChangeTree(2);
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
        }


        #region Not Needed TreeView Events

        private void treeView2_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.diskSpaceToolStripMenuItem.Checked == true)
            //    {
            //    }
            //    else if (this.compareToolStripMenuItem.Checked == true)
            //    {
            //        ChangeTree(2);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string temp = ex.Message;
            //    string source = ex.Source;
            //}
        }


        private void treeView1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.compareToolStripMenuItem.Checked == true)
            //    {
            //        ChangeTree(1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string temp = ex.Message;
            //    string source = ex.Source;
            //}
        }

        private void treeView2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.compareToolStripMenuItem.Checked == true)
            //    {
            //        ChangeTree(2);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string temp = ex.Message;
            //    string source = ex.Source;
            //}
        }


        private void treeView2_MouseClick(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    if (this.compareToolStripMenuItem.Checked == true)
            //    {
            //        ChangeTree(2);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string temp = ex.Message;
            //    string source = ex.Source;
            //}
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    if (this.compareToolStripMenuItem.Checked == true)
            //    {
            //        ChangeTree(1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string temp = ex.Message;
            //    string source = ex.Source;
            //}
        }


        private void treeView1_MouseCaptureChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.compareToolStripMenuItem.Checked == true)
            //    {
            //        ChangeTree(1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string temp = ex.Message;
            //    string source = ex.Source;
            //}
        }

        private void treeView2_MouseCaptureChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.compareToolStripMenuItem.Checked == true)
            //    {
            //        ChangeTree(2);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string temp = ex.Message;
            //    string source = ex.Source;
            //}
        }

        #endregion Not Needed TreeView Events

        #endregion TreeView Events


        #region Create TreeView Menu
        private void CreateTreeViewMenu()
        {
            ContextMenu contextMenu = new ContextMenu();

            if (this.diskSpaceToolStripMenuItem.Checked == true)
            {
                contextMenu.MenuItems.Add("Open", new EventHandler(this.Open_clicked));
                this.treeView1.ContextMenu = contextMenu;
            }
            else if (this.compareToolStripMenuItem.Checked == true)
            {
                contextMenu.MenuItems.Add("Sync Right", new EventHandler(this.SyncRight_clicked));
                this.treeView1.ContextMenu = contextMenu;

                contextMenu = new ContextMenu();
                contextMenu.MenuItems.Add("Sync Left", new EventHandler(this.SyncLeft_clicked));
                this.treeView2.ContextMenu = contextMenu;
            }
        }

        private void RemoveTreeViewMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            this.treeView1.ContextMenu = contextMenu;

            this.treeView2.ContextMenu = contextMenu;
        }

        #region Open_clicked
        private void Open_clicked(object sender, EventArgs e)
        {
            //string MouseButton = e.Button.ToString();
            string Path = string.Empty;

            try
            {
                if (this.diskSpaceToolStripMenuItem.Checked == true)
                {
                    SetTreeViewSelectedNode(1, true);
                    Path = treeView1.SelectedNode.ToolTipText;

                    SourcePath.Text = Path;

                    _ds.OpenWindowsExplorer(Path);
                }
                //else if (this.compareToolStripMenuItem.Checked == true)
                //{
                //    ChangeTree();
                //}
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
        }
        #endregion Open_clicked

        #region SyncRight_clicked
        private void SyncRight_clicked(object sender, EventArgs e)
        {
            string Path = string.Empty;

            try
            {
                SetTreeViewSelectedNode(1, true);
                Sync(1);
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
        }
        #endregion SyncRight_clicked

        #region SyncLeft_clicked
        private void SyncLeft_clicked(object sender, EventArgs e)
        {
            string Path = string.Empty;

            try
            {
                SetTreeViewSelectedNode(2, true);
                Sync(2);
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
        }
        #endregion SyncLeft_clicked

        #endregion Create TreeView Menu


        #region SetTreeViewSelectedNode
        private void SetTreeViewSelectedNode(int Tree, bool OffsetValues)
        {
            Point pt;

            try
            {
                if (Tree == 1)
                {
                    pt = treeView1.PointToClient(Cursor.Position);
                    treeView1.SelectedNode = treeView1.GetNodeAt(pt);
                    
                    if (OffsetValues)
                    {
                        pt.X = pt.X - 5;
                        pt.Y = pt.Y - 5;
                        treeView1.SelectedNode = treeView1.GetNodeAt(pt);

                        if (treeView1.SelectedNode == null)
                        {
                            pt.X = pt.X - 5;
                            pt.Y = pt.Y - 5;
                            treeView1.SelectedNode = treeView1.GetNodeAt(pt);
                        }
                    }
                }
                else
                {
                    pt = treeView2.PointToClient(Cursor.Position);
                    treeView2.SelectedNode = treeView1.GetNodeAt(pt);
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
        }
        #endregion SetTreeViewSelectedNode


        #region GetDiskSpaceUsage
        private void GetDiskSpaceUsage()
        {
            long TotalSize = 0;
            long lSize = 0;

            this.Go.Text = "Stop";

            // setup the progressbar
            progressBar1.Value = 0;
            progressBar1.Visible = true;

            // empty the treeviews
            for (int i = this.treeView1.Nodes.Count - 1; i > -1; i--)
            {
                this.treeView1.Nodes.RemoveAt(i);
            }
            for (int i = this.treeView2.Nodes.Count - 1; i > -1; i--)
            {
                this.treeView2.Nodes.RemoveAt(i);
            }

            // Erase the existing trees.
            this.treeView1.Nodes.Clear();
            this.treeView2.Nodes.Clear();


            // get the images in the images path
            string[] directories = Directory.GetDirectories(SourcePath.Text);
            progressBar1.Maximum = directories.Length;

            // Set the first node.
            TreeNode rootNode = new TreeNode(SourcePath.Text);
            rootNode.ForeColor = _ds.DefaultFolderColor;
            this.treeView1.Nodes.Add(rootNode);

            string[] IgnoreFolders = _ds.IgnoreFolders.Split(';');

            // loop thru all of the files and sub folders
            for (int d = 0; d < directories.Length; d++)
            {
                string directory = directories[d].ToString();

                bool SkipFolder = false;
                for (int s = 0; s < IgnoreFolders.Length; s++)
                {
                    if (directory.ToLower().Contains(IgnoreFolders[s].ToLower()))
                    {
                        SkipFolder = true;
                        break;
                    }
                }

                if (!SkipFolder)
                {
                    toolStripStatusLabel1.Text = "Processing " + directory;
                    if (toolStripStatusLabel1.Text.Length > 70)
                        toolStripStatusLabel1.Text = toolStripStatusLabel1.Text.Substring(toolStripStatusLabel1.Text.Length - 70, 70);
                    progressBar1.Value = d;
                    this.treeView1.Nodes[0].Expand();

                    if (_ds.ExitProcess == false)
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(directory);
                        lSize = FindSize(this, di, rootNode);
                        TotalSize += lSize;
                    }
                }

                Application.DoEvents();
            }

            this.treeView1.Nodes[0].Expand();

            _ds.ExitProcess = false;

            this.Go.Text = "Go";

            // finish the progress bar
            progressBar1.Visible = false;

            // update the message label
            toolStripStatusLabel1.Text = "Processing Completed - Total Size: " + _ds.ConvertBytes(TotalSize);
        }
        #endregion GetDiskSpaceUsage


        #region FindSize
        /// Returns the size of the directory in bytes
        public long FindSize(MainForm mainForm, System.IO.DirectoryInfo dirInfo, TreeNode node)
        {
            // update the message label
            //this._form = mainForm;
            //UpdateControl("", "statusStrip1", dirInfo.FullName);
            toolStripStatusLabel1.Text = "Processing " + dirInfo.FullName;
            if (toolStripStatusLabel1.Text.Length > 70)
                toolStripStatusLabel1.Text = toolStripStatusLabel1.Text.Substring(toolStripStatusLabel1.Text.Length-70, 70);

            Application.DoEvents();

            string ConvertedSize;

            long total = 0;

            if (_ds.ExitProcess == false)
            {
                try
                {
                    foreach (System.IO.FileInfo file in dirInfo.GetFiles())
                    {
                        total += file.Length;

                        //Application.DoEvents();
                    }
                }
                catch (Exception ex)
                {
                    string temp = ex.Message;
                    string source = ex.Source;
                    //Exception innerEx = ex.InnerException;
                }

                TreeNode rootNode = new TreeNode();
                rootNode.ForeColor = _ds.DefaultFolderColor;
                
                try
                {
                    foreach (System.IO.DirectoryInfo dir in dirInfo.GetDirectories())
                    {
                        total += FindSize(mainForm, dir, rootNode);

                        //Application.DoEvents();
                    }
                }
                catch (Exception ex)
                {
                    string temp = ex.Message;
                    string source = ex.Source;
                    //Exception innerEx = ex.InnerException;
                }

                ConvertedSize = _ds.ConvertBytes(total);
                if (_ds.DisplayEmptyFolders == false && ConvertedSize.StartsWith("0.00"))
                {
                    // don't display empty folders
                    string temp = string.Empty;
                }
                else
                {
                    if (ConvertedSize.ToUpper().EndsWith("GB"))
                        rootNode.ForeColor = _ds.FolderWarningColor;
                    else if (ConvertedSize.ToUpper().EndsWith("MB"))
                    {
                        string cSize = ConvertedSize.Substring(0, ConvertedSize.IndexOf(" "));
                        if(ConvertedSize.IndexOf(".") > 0)
                            cSize = ConvertedSize.Substring(0, ConvertedSize.IndexOf("."));
                        cSize.Replace(",", "");
                        int size = int.Parse(cSize);
                        if(size >= _ds.FolderWarningSize)
                            rootNode.ForeColor = _ds.FolderWarningColor;
                    }
                    rootNode.Text = dirInfo.Name + " - " + ConvertedSize;
                    rootNode.ToolTipText = dirInfo.FullName;
                    node.Nodes.Add(rootNode);
                }
            }

            return total;
        }
        #endregion FindSize


        #region Compare
        private void Compare()
        {
            System.IO.DirectoryInfo di = null;
            System.IO.DirectoryInfo di2 = null;

            this.Go.Text = "Stop";

            // setup the progressbar
            progressBar1.Value = 0;
            progressBar1.Visible = true;

            // empty the treeviews
            for (int i = this.treeView1.Nodes.Count - 1; i > -1; i--)
            {
                this.treeView1.Nodes.RemoveAt(i);
            }
            for (int i = this.treeView2.Nodes.Count - 1; i > -1; i--)
            {
                this.treeView2.Nodes.RemoveAt(i);
            }

            // Erase the existing trees.
            this.treeView1.Nodes.Clear();
            this.treeView2.Nodes.Clear();


            // Set the first node.
            TreeNode rootNode = new TreeNode(SourcePath.Text);
            rootNode.ForeColor = _ds.DefaultFolderColor;
            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.Add(rootNode);

            // Set the second node.
            TreeNode rootNode2 = new TreeNode(ComparePath.Text);
            rootNode2.ForeColor = _ds.DefaultFolderColor;
            this.treeView2.Nodes.Clear();
            this.treeView2.Nodes.Add(rootNode2);

            // get the folders/files for the source path
            di = new DirectoryInfo(SourcePath.Text);
            GetFolders(di, rootNode);

            // get the folders/files for the compare path
            di2 = new DirectoryInfo(this.ComparePath.Text);
            GetFolders(di2, rootNode2);

            Application.DoEvents();

            // compare the two paths
            CompareFolders(rootNode, rootNode2);

            this.treeView1.Nodes[0].Expand();
            this.treeView2.Nodes[0].Expand();

            di = null;
            di2 = null;

            progressBar1.Visible = false;
            this.Go.Text = "Go";

            _ds.ExitProcess = false;

            // update the message label
            toolStripStatusLabel1.Text = "Processing Completed";
        }
        #endregion Compare


        #region CompareFolders
        private void CompareFolders(TreeNode rootNode, TreeNode rootNode2)
        {
            int i = 0;

            try
            {
                _ds.CompareFiles(rootNode, rootNode2);

                for (i = 0; i < rootNode.Nodes.Count; i++)
                {
                    progressBar1.Maximum = rootNode.Nodes.Count + 1;
                    progressBar1.Value = i + 1;

                    if (rootNode.Nodes[i].Name.StartsWith("folder"))
                    {
                        TreeNode[] fNodes = rootNode2.Nodes.Find(rootNode.Nodes[i].Name, false);
                        if (fNodes.Length < 1)
                        {
                            TreeNode newRoot = new TreeNode();
                            newRoot.Text = "missing folder: " + rootNode.Nodes[i].Text;
                            //newRoot.Text = rootNode.Nodes[i].Text;
                            newRoot.Name = "folder: " + rootNode2.ToolTipText + rootNode.Nodes[i].Text; 
                            newRoot.ToolTipText = "missing folder: " + rootNode2.ToolTipText + rootNode.Nodes[i].Text;
                            newRoot.ForeColor = Color.Brown;
                            TreeNode child = new TreeNode();
                            child.Text = "missing file(s)";
                            child.ForeColor = Color.Brown;
                            newRoot.Nodes.Add(child);
                            rootNode2.Nodes.Insert(i, newRoot);
                            rootNode2.ForeColor = Color.Brown;
                            rootNode.ForeColor = Color.Brown;
                            rootNode.Nodes[i].ForeColor = Color.Brown;
                            _ds.ChangeTreeColor(rootNode.Nodes[i].Nodes, Color.Brown);
                        }
                        else
                        {
                            CompareFolders(rootNode.Nodes[i], rootNode2.Nodes[i]);
                        }
                    }

                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
                //Exception innerEx = ex.InnerException;
            }
        }
        #endregion CompareFolders


        #region ChangeTree
        private void ChangeTree(int Tree)
        {
            TreeNode rootNode = null;
            int Index = -1;

            try
            {
                //SetTreeViewSelectedNode(Tree, false);

                //TreeNode[] fNodes = treeView2.Nodes[0].Nodes.Find(treeView1.SelectedNode.Text, true);
                //fNodes.Expand();
                //fNodes = null;

                //int Index = treeView2.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode);
                //Index = treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode);
                //Index = treeView1.SelectedNode.Index;

                if (Tree == 1)
                {
                    if (treeView1.SelectedNode != null)
                        rootNode = treeView1.SelectedNode;
                    //else
                    //    treeView1.GetChildAtPoint(Cursor.Position).Select();
                }
                else
                {
                    if (treeView2.SelectedNode != null)
                        rootNode = treeView2.SelectedNode;
                }

                // is the selected node null
                if (rootNode != null)
                {
                    Index = rootNode.Index;
                    if (Index > -1)
                    {
                        int Level = rootNode.Level;

                        //for (int i = 0; i < Level; i++)
                        //{
                        //    //rootNode.Nodes.Add(rootNode.Nodes[0].Nodes[0]);
                        //    rootNode.m
                        //}

                        // is the selected node expanded
                        if (rootNode.IsExpanded)
                        {
                            if (Tree == 1)
                                rootNode = treeView2.Nodes[0];
                            else
                                rootNode = treeView1.Nodes[0];

                            // yes it is
                            if (Level < 2)
                            {
                                rootNode.Nodes[Index].Expand();
                            }
                            else if (Level == 2)
                            {
                                rootNode.Nodes[0].Nodes[Index].Expand();
                            }
                        }
                        else
                        {
                            if (Tree == 1)
                                rootNode = treeView2.Nodes[0];
                            else
                                rootNode = treeView1.Nodes[0];

                            if (Level < 2)
                            {
                                rootNode.Nodes[Index].Collapse();
                            }
                            else if (Level == 2)
                            {
                                rootNode.Nodes[0].Nodes[Index].Collapse();
                            }
                        }       // is the selected node expanded
                    }
                }       // is the selected node null


                TreeScroll(Tree);
                //if (Tree == 1)
                //{
                //    pt = treeView1.PointToClient(Cursor.Position);
                //    //pt.X = treeView2.AutoScrollOffset.X;
                //    treeView2.AutoScrollOffset = pt;
                //    //treeView2.GetChildAtPoint(pt).Select();
                //}
                //else
                //{
                //    pt = treeView2.PointToClient(Cursor.Position);
                //    //pt.X = 135;
                //    treeView1.AutoScrollOffset = pt;
                //}

            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
            finally
            {
                rootNode = null;
            }
        }
        #endregion ChangeTree


        #region TreeScroll
        // Implement an "autoscroll" routine for drag
        //  and drop. If the drag cursor moves to the bottom
        //  or top of the treeview, call the Windows API
        //  SendMessage function to scroll up or down automatically.
        //private void DragScroll(object sender, DragEventArgs e)
        private void TreeScroll(int Tree)
        {
            // Set a constant to define the autoscroll region
            //const Single scrollRegion = 20;

            Point pt;
            Point pt2;

            try
            {
                if (Tree == 1)
                {
                    // See where the cursor is
                    pt = treeView1.PointToClient(Cursor.Position);
                    pt2 = treeView2.AutoScrollOffset;


                    //SetTreeViewSelectedNode(Tree, false);
                    if (treeView1.SelectedNode != null)
                    {
                        string Path = treeView1.SelectedNode.Text;
                        string Parent = treeView1.SelectedNode.Parent.Text;
                        TreeNode rootNode = _ds.TreeViewSearch(treeView2.Nodes, Parent);
                        if (rootNode != null)
                        {
                            rootNode.Expand();

                            rootNode = _ds.TreeViewSearch(rootNode.Nodes, Path);
                            if (rootNode != null)
                                rootNode.Expand();
                        }
                        else
                        {
                            rootNode = _ds.TreeViewSearch(treeView2.Nodes, Path);
                            if (rootNode != null)
                                rootNode.Expand();
                        }
                    }


                    //if (pt.Y == pt2.Y || pt.Y == 0 || pt2.Y == 0)
                    if (pt.Y == pt2.Y)
                    {
                        // don't move
                        treeView2.AutoScrollOffset = pt;
                    }
                    else if (pt.Y < pt2.Y)
                    {
                        treeView2.AutoScrollOffset = pt;
                        // Call thje API to scroll up
                        SendMessage(treeView2.Handle, (int)277, (int)0, 0);
                    }
                    else
                    {
                        treeView2.AutoScrollOffset = pt;
                        // Call the API to scroll down
                        SendMessage(treeView2.Handle, (int)277, (int)1, 10);
                    }

                    //// See if we need to scroll up or down
                    //if ((pt.Y + scrollRegion) > treeView1.Height)
                    //{
                    //    // Call the API to scroll down
                    //    SendMessage(treeView2.Handle, (int)277, (int)1, 10);
                    //}
                    //else if (pt.Y < (treeView1.Top + scrollRegion))
                    //{
                    //    // Call thje API to scroll up
                    //    SendMessage(treeView2.Handle, (int)277, (int)0, 0);
                    //}
                }
                else
                {
                    // See where the cursor is
                    pt = treeView2.PointToClient(Cursor.Position);
                    pt2 = treeView1.AutoScrollOffset;


                    if (treeView2.SelectedNode != null)
                    {
                        string Path = treeView2.SelectedNode.Text;
                        string Parent = treeView2.SelectedNode.Parent.Text;
                        TreeNode rootNode = _ds.TreeViewSearch(treeView1.Nodes, Parent);
                        if (rootNode != null)
                        {
                            rootNode.Expand();

                            rootNode = _ds.TreeViewSearch(rootNode.Nodes, Path);
                            if (rootNode != null)
                                rootNode.Expand();
                        }
                        else
                        {
                            rootNode = _ds.TreeViewSearch(treeView1.Nodes, Path);
                            if (rootNode != null)
                                rootNode.Expand();
                        }
                    }


                    //if (pt.Y == pt2.Y || pt.Y == 0 || pt2.Y == 0)
                    if (pt.Y == pt2.Y)
                    {
                        // don't move
                        treeView1.AutoScrollOffset = pt;
                    }
                    else if (pt.Y < pt2.Y)
                    {
                        treeView1.AutoScrollOffset = pt;
                        // Call thje API to scroll up
                        SendMessage(treeView1.Handle, (int)277, (int)0, 0);
                    }
                    else
                    {
                        treeView1.AutoScrollOffset = pt;
                        // Call the API to scroll down
                        SendMessage(treeView1.Handle, (int)277, (int)1, 10);
                    }
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
            }
            finally
            {
            }
    
        }
        #endregion TreeScroll


        #region GetFolders
        public TreeNode GetFolders(System.IO.DirectoryInfo dirInfo, TreeNode node)
        {
            // get a list of all the folders from the source
            string[] directories = Directory.GetDirectories(dirInfo.FullName);

            for (int d = 0; d < directories.Length && _ds.ExitProcess == false; d++)
            {
                progressBar1.Maximum = directories.Length + 1;
                progressBar1.Value = d + 1;
                string directory = directories[d].ToString();
                toolStripStatusLabel1.Text = "Processing " + directory;
                if (toolStripStatusLabel1.Text.Length > 70)
                    toolStripStatusLabel1.Text = toolStripStatusLabel1.Text.Substring(toolStripStatusLabel1.Text.Length - 70, 70);

                try
                {
                    string subdirectory = directory.Substring(dirInfo.FullName.Length, directory.Length - dirInfo.FullName.Length);
                    TreeNode rootNode = new TreeNode();
                    rootNode.ForeColor = _ds.DefaultFolderColor;
                    if (!subdirectory.StartsWith(@"\"))
                        subdirectory = @"\" + subdirectory;
                    rootNode.ToolTipText = directory;
                    rootNode.Text = subdirectory;
                    rootNode.Name = "folder: " + subdirectory;

                    if (dirInfo.GetDirectories().Length > 0)
                    {
                        GetFolders(new DirectoryInfo(directory), rootNode);
                    }

                    node.Nodes.Add(rootNode);
                }
                catch (Exception ex)
                {
                    string temp = ex.Message;
                    string source = ex.Source;
                    //Exception innerEx = ex.InnerException;
                }

                Application.DoEvents();
            }

            _ds.GetFiles(dirInfo, node);

            return node;
        }
        #endregion GetFolders

        
        #region Sync
        private void Sync(int Tree)
        {
            //MessageBox.Show("Sorry, this feature is not finished.", Application.ProductName);

            // update the message label
            toolStripStatusLabel1.Text = "Sync Started";

            TreeNode selectedNode = null;
            TreeNode rootNode = null;
            TreeNode rootNode2 = null;
            string Path = string.Empty;

            //SetTreeViewSelectedNode(Tree, false);

            if (Tree == 1)
                selectedNode = treeView1.SelectedNode;
            else
                selectedNode = treeView2.SelectedNode;

            if (selectedNode != null)
            {
                // Set the first node.
                Path = selectedNode.ToolTipText;
                rootNode = new TreeNode(Path);
                rootNode.ForeColor = _ds.DefaultFolderColor;
                Path = selectedNode.Text;
                if (Tree == 1)
                    rootNode.Nodes.Add(_ds.TreeViewSearch(treeView1.Nodes, Path));
                else
                    rootNode.Nodes.Add(_ds.TreeViewSearch(treeView2.Nodes, Path));

                // Set the second node.
                Path = selectedNode.ToolTipText;
                rootNode2 = new TreeNode(Path);
                rootNode2.ForeColor = _ds.DefaultFolderColor;
                Path = selectedNode.Text;
                if (Tree == 1)
                    rootNode2.Nodes.Add(_ds.TreeViewSearch(treeView2.Nodes, Path));
                else
                    rootNode2.Nodes.Add(_ds.TreeViewSearch(treeView1.Nodes, Path));

                _ds.SyncFiles(rootNode.Nodes, rootNode2.Nodes);

                //this.treeView1.Nodes.Clear();
                //this.treeView1.Nodes.Add(rootNode);
                //this.treeView1.Nodes[0].Expand();
                
                //this.treeView2.Nodes.Clear();
                //this.treeView2.Nodes.Add(rootNode2);
                //this.treeView2.Nodes[0].Expand();

                Compare();
            }

            rootNode = null;
            rootNode2 = null;
            selectedNode = null;

            // update the message label
            toolStripStatusLabel1.Text = "Processing Completed";
        }
        #endregion Sync


        #region Exit
        private void Exit()
        {
            _ds.ExitProcess = true;

            Application.DoEvents();
            Application.ExitThread();
            Application.Exit();
        }
        #endregion Exit


    }
}