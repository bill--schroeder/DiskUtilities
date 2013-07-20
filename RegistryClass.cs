using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Security;
using System.Security.Permissions;

namespace Disk_Utilities
{
    class RegistryClass
    {
        #region Member Variables

        private string MenuName = "Folder\\shell\\NewMenuOption";
        private string Command = "Folder\\shell\\NewMenuOption\\command";

        #endregion Member Variables


        #region CheckSecurity
        private void CheckSecurity()
        {
            //check registry permissions
            RegistryPermission regPerm;
            regPerm = new RegistryPermission(RegistryPermissionAccess.Write, "HKEY_CLASSES_ROOT\\" + MenuName);
            regPerm.AddPathList(RegistryPermissionAccess.Write, "HKEY_CLASSES_ROOT\\" + Command);
            regPerm.Demand();
        }
        #endregion CheckSecurity


        #region AddContextMenu
        public void AddContextMenu(string MenuItem, string ExecutablePath, string ExecutableCommand)
        {
            RegistryKey regmenu = null;
            RegistryKey regcmd = null;

            try
            {
                string Path = ExecutablePath.Substring(0, ExecutablePath.LastIndexOf(@"\") + 1);
                string Name = ExecutablePath.Substring(Path.Length, ExecutablePath.Length - Path.Length - 4);

                Name += "_" + MenuItem;

                //
                RemoveContextMenu(MenuItem, ExecutablePath);

                //
                MenuName = MenuName.Replace("NewMenuOption", Name);
                Command = Command.Replace("NewMenuOption", Name);

                // add the registry entries
                regmenu = Registry.ClassesRoot.CreateSubKey(MenuName);
                if (regmenu != null)
                    regmenu.SetValue("", MenuItem);
                regcmd = Registry.ClassesRoot.CreateSubKey(Command);
                if (regcmd != null)
                    regcmd.SetValue("", ExecutablePath + " " + ExecutableCommand);

                //
                MenuName = MenuName.Replace(Name, "NewMenuOption");
                Command = Command.Replace(Name, "NewMenuOption");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {
                if (regmenu != null)
                    regmenu.Close();
                if (regcmd != null)
                    regcmd.Close();
            }
        }
        #endregion AddContextMenu


        #region AppendContextMenu
        public void AppendContextMenu()
        {

            RegistryKey regmenu = null;
            RegistryKey regcmd = null;
            try
            {
                this.CheckSecurity();
                //this.btnAddMenu.Enabled = false;
                //regmenu = Registry.ClassesRoot.OpenSubKey(MenuName, false);
                //if (regmenu != null)
                //    this.txtName.Text = (String)regmenu.GetValue("");
                //regcmd = Registry.ClassesRoot.OpenSubKey(Command, false);
                //if (regcmd != null)
                //    this.txtPath.Text = (String)regcmd.GetValue("");
                //btnRemoveMenu.Enabled = txtName.Text.Length > 0 || this.txtPath.Text.Length > 0;
                
            }
            catch (ArgumentException ex)
            {
                // RegistryPermissionAccess.AllAccess can not be used as a parameter for GetPathList.
                string error = ex.Message;
                //MessageBox.Show(this, "An ArgumentException occured as a result of using AllAccess.  "
                //    + "AllAccess cannot be used as a parameter in GetPathList because it represents more than one "
                //    + "type of registry variable access : \n" + ex);
            }
            catch (SecurityException ex)
            {
                // RegistryPermissionAccess.AllAccess can not be used as a parameter for GetPathList.
                string error = ex.Message;
                //MessageBox.Show(this, "An ArgumentException occured as a result of using AllAccess.  " + ex);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                //MessageBox.Show(this, ex.ToString());
            }
            finally
            {
                if (regmenu != null)
                    regmenu.Close();
                if (regcmd != null)
                    regcmd.Close();
            }
        }
        #endregion AppendContextMenu


        #region RemoveContextMenu
        public void RemoveContextMenu(string MenuItem, string ExecutablePath)
        {
            try
            {
                string Path = ExecutablePath.Substring(0, ExecutablePath.LastIndexOf(@"\") + 1);
                string Name = ExecutablePath.Substring(Path.Length, ExecutablePath.Length - Path.Length - 4);

                if(MenuItem.Trim().Length > 0)
                    Name += "_" + MenuItem;

                //
                MenuName = MenuName.Replace("NewMenuOption", Name);
                Command = Command.Replace("NewMenuOption", Name);

                // remove the registry entries
                RegistryKey reg = Registry.ClassesRoot.OpenSubKey(Command);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(Command);
                }
                reg = Registry.ClassesRoot.OpenSubKey(MenuName);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(MenuName);
                }

                //
                MenuName = MenuName.Replace(Name, "NewMenuOption");
                Command = Command.Replace(Name, "NewMenuOption");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                //MessageBox.Show(this, ex.ToString());
            }
            finally
            {
            }

        }
        #endregion RemoveContextMenu

    }
}
