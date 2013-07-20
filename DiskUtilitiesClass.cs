using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Disk_Utilities
{
    public class DiskUtilitiesClass
    {
        #region Member Variables

        public Disk_Utilities.MainForm _form = null;

        public bool ExitProcess = false;

        public bool DisplayEmptyFolders = true;
        public int FolderWarningSize = 200;
        public Color FolderWarningColor = Color.Red;
        public Color DefaultFolderColor = Color.Green;
        public string IgnoreFolders = @"C:\$Recycle.Bin;C:\temp";

        #endregion Member Variables


        #region Constructors

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Arguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //// FOR DEBUG TESTING ONLY
            //string arguments = @"/DiskSpace D:\MYDEVE~1\c#\DISKUT~1";
            //string arguments = @"/CompareSetSource C:\";
            //Arguments = arguments.Split(' ');

            Disk_Utilities.MainForm _form = new Disk_Utilities.MainForm(Arguments);
            Application.Run(_form);
        }

        public DiskUtilitiesClass(MainForm MainForm)
		{
            MainForm _form = MainForm;
        }

        #endregion Constructors


        #region AddRegistrySettings
        public void AddRegistrySettings()
        {
            RegistryClass reg = new RegistryClass();

            // Create Windows Explorer Content Menus
            reg.RemoveContextMenu("", Application.ExecutablePath);
            reg.AddContextMenu("Disk Space", Application.ExecutablePath, " /DiskSpace %1");
            reg.AddContextMenu("Compare - Set Source", Application.ExecutablePath, " /CompareSetSource %1");
            //reg.RemoveContextMenu("Compare to Source", Application.ExecutablePath);

            reg = null;
        }
        #endregion AddRegistrySettings


        #region ChangeTreeColor
        public void ChangeTreeColor(TreeNodeCollection nodes, Color color)
        {
            try
            {
                foreach (TreeNode checkNode in nodes)
                {
                    checkNode.ForeColor = color;

                    ChangeTreeColor(checkNode.Nodes, color);
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
                //Exception innerEx = ex.InnerException;
            }
        }
        #endregion ChangeTreeColor


        #region CompareFiles
        /// Returns the size of the directory in bytes
        public void CompareFiles(TreeNode node, TreeNode node2)
        {
            // update the message label
            //_ds.UpdateControl("", "Message", dirInfo.FullName);
            //toolStripStatusLabel1.Text = dirInfo.FullName;

            if (ExitProcess == false)
            {

                try
                {
                    #region  loop thru the files in the first tree
                    for (int i = 0; i < node.Nodes.Count && i < node2.Nodes.Count; i++)
                    {
                        try
                        {
                            // does the file match
                            if (node.Nodes[i].Text.ToUpper() != node2.Nodes[i].Text.ToUpper() && !node.Nodes[i].Name.StartsWith("folder") && !node.Nodes[i].Name.Equals(string.Empty))
                            {
                                // does the file name match
                                if (node.Nodes[i].Name.ToUpper() == node2.Nodes[i].Name.ToUpper())
                                {
                                    // file name does match, but the size or date do not
                                    node.ForeColor = Color.Red;
                                    node.Nodes[i].ForeColor = Color.Red;
                                    node2.ForeColor = Color.Red;
                                    node2.Nodes[i].ForeColor = Color.Red;
                                }
                                else
                                {
                                    // does this node exist elsewhere in the compare to tree
                                    TreeNode[] fNodes = node2.Nodes.Find(node.Nodes[i].Name, false);
                                    if (fNodes.Length < 1)
                                    {
                                        // it doesn't, so let's add a missing tag
                                        node.ForeColor = Color.Maroon;
                                        node.Nodes[i].ForeColor = Color.Maroon;
                                        node2.ForeColor = Color.Maroon;
                                        //if (node.Level == 1)
                                            node2.Nodes.Insert(i, FormatMissingFileNode(node2.Nodes[i], node.Nodes[i].Name));
                                        //else
                                        //    node2.Nodes.Insert(i + 1, FormatMissingFileNode(node2.Nodes[i], node.Nodes[i].Name));
                                    }

                                }       // does the file name match

                            }       // does the file match

                        }
                        catch (Exception ex)
                        {
                            string temp = ex.Message;
                            //string source = ex.Source;
                            ////Exception innerEx = ex.InnerException;

                            //TreeNode[] fNodes = node2.Nodes.Find(node.Nodes[i].Name, false);
                            //if (fNodes.Length < 1)
                            //{
                            //    // it doesn't, so let's add a missing tag
                            //    node.ForeColor = Color.Brown;
                            //    node.Nodes[i].ForeColor = Color.Brown;
                            //    node2.ForeColor = Color.Brown;
                            //    if (node.Nodes[i].Name.Equals(string.Empty))
                            //    {
                            //        //TreeNode newRoot = new TreeNode();
                            //        //newRoot.Text = node2.Parent.ToolTipText + @"\" + node.Nodes[i].Parent.Text;
                            //        //newRoot.ToolTipText = "missing folder: " + node.Nodes[i].Text;
                            //        //newRoot.ForeColor = Color.Brown;
                            //        //node2.Nodes.Add(newRoot);
                            //        //node2.Nodes.Insert(i, newRoot);
                            //        node2.Nodes.Insert(i, FormatMissingFileNode(node2, node.Nodes[i].Text));
                            //    }
                            //    else
                            //    {
                            //        //node2.Nodes.Insert(i, FormatMissingFileNode(node2.Nodes[i], node.Nodes[i].Name));
                            //    }
                            //}
                        }

                    }
                    #endregion  loop thru the files in the first tree


                    #region  loop thru the files in the second tree
                    for (int i = 0; i < node.Nodes.Count && i < node2.Nodes.Count; i++)
                    {
                        try
                        {
                            // does the file match
                            if (node.Nodes[i].Text.ToUpper() != node2.Nodes[i].Text.ToUpper() && !node2.Nodes[i].Name.StartsWith("folder") && !node2.Nodes[i].Name.Equals(string.Empty))
                            {
                                // does the file name match
                                if (node.Nodes[i].Name.ToUpper() == node2.Nodes[i].Name.ToUpper())
                                {
                                    // file name does match, but the size or date do not
                                    node.ForeColor = Color.Red;
                                    node.Nodes[i].ForeColor = Color.Red;
                                    node2.ForeColor = Color.Red;
                                    node2.Nodes[i].ForeColor = Color.Red;
                                }
                                else
                                {
                                    // does this node exist elsewhere in the compare to tree
                                    //int Found = node2.Nodes.IndexOf(node.Nodes[i]);
                                    //if (Found < 0)
                                    TreeNode[] fNodes = node.Nodes.Find(node2.Nodes[i].Name, false);
                                    if (fNodes.Length < 1)
                                    {
                                        // it doesn't, so let's add a missing tag
                                        node2.ForeColor = Color.Maroon;
                                        node2.Nodes[i].ForeColor = Color.Maroon;
                                        node.ForeColor = Color.Maroon;
                                        //if (node.Level == 1)
                                            node.Nodes.Insert(i, FormatMissingFileNode(node.Nodes[i], node2.Nodes[i].Name));
                                        //else
                                        //    node.Nodes.Insert(i + 1, FormatMissingFileNode(node.Nodes[i], node2.Nodes[i].Name));
                                    }

                                }       // does the file name match

                            }       // does the file match

                        }
                        catch (Exception ex)
                        {
                            string temp = ex.Message;
                            //string source = ex.Source;
                            ////Exception innerEx = ex.InnerException;

                            //TreeNode[] fNodes = node.Nodes.Find(node2.Nodes[i].Name, false);
                            //if (fNodes.Length < 1)
                            //{
                            //    // it doesn't, so let's add a missing tag
                            //    node2.ForeColor = Color.Maroon;
                            //    node2.Nodes[i].ForeColor = Color.Maroon;
                            //    node.Nodes.Insert(i + 1, FormatMissingFileNode(node.Nodes[i], node2.Nodes[i].Name));
                            //}
                        }

                    }
                    #endregion  loop thru the files in the second tree

                }
                catch (Exception ex)
                {
                    string temp = ex.Message;
                    string source = ex.Source;
                    //Exception innerEx = ex.InnerException;
                }

            }

        }
        #endregion CompareFiles


        #region ConvertBytes
        public string ConvertBytes(long Bytes)
		{
			string Converted	= "";
			float FileSize		= Bytes;

			if (FileSize > 1024)
			{
				FileSize = (FileSize / 1024);
				if (FileSize > 1024)
				{
					FileSize = (FileSize / 1024);
					if (FileSize > 1024)
					{
						FileSize	= (FileSize / 1024);
						//Converted	= FileSize.ToString().Substring(0, FileSize.ToString().IndexOf(".") + 3);
						Converted	= FileSize.ToString("###,###,##0.00");
						Converted	= Converted + " GB";
					}
					else
					{
						//Converted	= FileSize.ToString().Substring(0, FileSize.ToString().IndexOf(".") + 3);
						Converted	= FileSize.ToString("###,###,##0.00");
						Converted	= Converted + " MB";
					}
				}
				else
				{
					//Converted	= FileSize.ToString().Substring(0, FileSize.ToString().IndexOf(".") + 3);
					Converted	= FileSize.ToString("###,###,##0.00");
					Converted	= Converted + " KB";
				}
			}
			else
			{
				Converted	= FileSize.ToString("###,###,##0.00");
				Converted	= Converted + " Bytes";
			}

			return Converted;
        }
        #endregion ConvertBytes


        #region CopyDirectory
        public static void CopyDirectory(string Source, string Destination)
        {
            String[] Files;

            try
            {
                if (Destination[Destination.Length - 1] != Path.DirectorySeparatorChar)
                    Destination += Path.DirectorySeparatorChar;

                if (!Directory.Exists(Destination)) Directory.CreateDirectory(Destination);

                Files = Directory.GetFileSystemEntries(Source);

                foreach (string Element in Files)
                {
                    // Sub directories
                    if (Directory.Exists(Element))
                        CopyDirectory(Element, Destination + Path.GetFileName(Element));
                    // Files in directory
                    else
                        File.Copy(Element, Destination + Path.GetFileName(Element), true);

                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }

        }
        #endregion CopyDirectory


        #region GetFiles
        public TreeNode GetFiles(System.IO.DirectoryInfo dirInfo, TreeNode node)
        {
            // get a list of all the files from the source
            foreach (System.IO.FileInfo file in dirInfo.GetFiles())
            {
                //toolStripStatusLabel1.Text = file.Name;

                try
                {
                    TreeNode rootNode = FormatFileNode(file);
                    node.Nodes.Add(rootNode);
                }
                catch (Exception ex)
                {
                    string temp = ex.Message;
                    string source = ex.Source;
                    //Exception innerEx = ex.InnerException;
                }
            }

            return node;
        }
        #endregion GetFiles


        #region FormatFileNode
        private TreeNode FormatFileNode(System.IO.FileInfo file)
        {
            TreeNode node = new TreeNode();
            node.ForeColor = DefaultFolderColor;
            node.Text = file.Name;
            node.Text += "   " + file.LastWriteTime.ToShortDateString() + " " + file.LastWriteTime.ToShortTimeString();
            node.Text += "   " + ConvertBytes(file.Length);
            node.Name = file.Name.ToUpper();
            node.ToolTipText = file.FullName;
            node.Tag = "file";

            return node;
        }
        #endregion FormatFileNode


        #region FormatMissingFileNode
        private TreeNode FormatMissingFileNode(TreeNode rootNode, string FileName)
        {
            string Path = rootNode.Parent.ToolTipText;
            if(!Path.EndsWith(rootNode.Parent.Text))
                Path += rootNode.Parent.Text;
            if (!Path.EndsWith(@"\"))
                Path += @"\";

            TreeNode node = new TreeNode();
            node.ForeColor = Color.Maroon;
            node.Text = "missing";
            node.ToolTipText = "missing file: " + Path + FileName;

            return node;
        }
        #endregion FormatMissingFileNode


        #region OpenWindowsExplorer
        public void OpenWindowsExplorer(string Path)
        {
            try
            {
                Process.Start("explorer.exe", Path);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {
            }
        }
        #endregion OpenWindowsExplorer


        #region RemoveRegistrySettings
        public void RemoveRegistrySettings()
        {
            RegistryClass reg = new RegistryClass();

            reg.RemoveContextMenu("", Application.ExecutablePath);
            reg.RemoveContextMenu("Disk Space", Application.ExecutablePath);
            reg.RemoveContextMenu("Compare - Set Source", Application.ExecutablePath);
            reg.RemoveContextMenu("Compare to Source", Application.ExecutablePath);

            reg = null;
        }
        #endregion RemoveRegistrySettings


        #region SyncFiles
        public string SyncFiles(TreeNodeCollection nodes, TreeNodeCollection nodes2)
        {
            string Return = string.Empty;
            string file = string.Empty;
            string file2 = string.Empty;

            try
            {
                //if (nodes2[0].Text == "missing folder")
                //{
                //    file = nodes[0].ToolTipText;
                //}

                //if (nodes2.Count == 1 && nodes.Count == 1)
                //{
                //    file = nodes[0].ToolTipText;
                //    file2 = nodes2[0].Text;

                //    if (file2.Length > 0)
                //        Directory.CreateDirectory(file2);
                //}

                // loop thru the nodes
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].ForeColor == DefaultFolderColor)
                    {
                        // do nothing as these files are good
                    }
                    else
                    {
                        if (nodes[i].Tag == null)
                        {
                            // this is a folder, so check it's folders/files
                            Return = SyncFiles(nodes[i].Nodes, nodes2[i].Nodes);

                            if (Return.Equals("found missing folder"))
                            {
                                file = nodes[0].ToolTipText;
                                file2 = nodes2[0].ToolTipText;
                                file2 = file2.Replace("missing folder: ", "");

                                if (file2.Length > 0)
                                {
                                    CopyDirectory(file, file2);
                                    //Directory.CreateDirectory(file2);
                                    //File.Copy(file + @"\*.*", file2 + @"\*.*", true);
                                }
                            }
                        }
                        else
                        {
                            // we found a file to sync
                            file = nodes[i].Name;
                            if(i < nodes2.Count)
                                file2 = nodes2[i].Name;

                            if (file != file2)
                            {
                                if (i < nodes2.Count && !nodes2[i].ToolTipText.Equals(string.Empty))
                                {
                                    file2 = nodes2[i].ToolTipText;
                                    int Index = file2.LastIndexOf(@"\") + 1;
                                    file2 = file2.Substring(0, Index) + file;
                                    file = nodes[i].ToolTipText;
                                }
                                else
                                    return "found missing folder";
                            }
                            else
                            {
                                file = nodes[i].ToolTipText;
                                file2 = nodes2[i].ToolTipText;
                            }

                            file2 = file2.Replace("missing file: ", "");

                            try
                            {
                                if(file.Length > 0 && file2.Length > 0)
                                    File.Copy(file, file2, true);
                            }
                            catch (Exception ex)
                            {
                                string temp = ex.Message;
                            }

                            Application.DoEvents();
                        }
                    }
                }       // loop thru the nodes

            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
                //Exception innerEx = ex.InnerException;
            }

            return Return;
        }
        #endregion SyncFiles


        #region TreeViewSearch
        public TreeNode TreeViewSearch(TreeNodeCollection nodes, string Search)
        {
            TreeNode rootNode = new TreeNode();

            try
            {
                foreach (TreeNode checkNode in nodes)
                {
                    if (rootNode.Text.Length > 0)
                    {
                        // Yes, we have have our record
                        return rootNode;
                    }
                    if (checkNode.Text.EndsWith(Search))
                    {
                        rootNode = (TreeNode)checkNode.Clone();
                        return rootNode;
                    }
                    else
                        rootNode = TreeViewSearch(checkNode.Nodes, Search);
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                string source = ex.Source;
                //Exception innerEx = ex.InnerException;
            }

            return rootNode;
        }
        #endregion TreeViewSearch


        #region UpdateControl
        public void UpdateControl(string ParentControlName, string ControlName, string NewValue)
        {
            // search thru the forms control and find the parent control
            for (int i = 0; i < _form.Controls.Count; i++)
            {
                //string test1 = _mainForm.Controls[i].Name;
                if (_form.Controls[i].Name.ToUpper() == ParentControlName.ToUpper())
                {
                    // search thru the controls in the parent control
                    for (int ii = 0; ii < _form.Controls[i].Controls.Count; ii++)
                    {
                        //string test2 = _mainForm.Controls[i].Controls[ii].Name;
                        if (_form.Controls[i].Controls[ii].Name.ToUpper() == ControlName.ToUpper())
                        {
                            // update the value of the control
                            _form.Controls[i].Controls[ii].Text = NewValue;
                        }
                    }
                }
                else if (ParentControlName == "")
                {
                    //string test3 = _mainForm.Controls[i].Name;
                    if (_form.Controls[i].Name.ToUpper() == ControlName.ToUpper())
                    {
                        // update the value of the control
                        _form.Controls[i].Text = NewValue;
                    }
                }
            }

            Application.DoEvents();
        }	// UpdateControl
        #endregion UpdateControl

    }
}