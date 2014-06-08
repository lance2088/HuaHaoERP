using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HuaHaoERP.Helper.Tools;
using HuaHaoERP.ViewModel.Settings;
using HuaHaoERP.Helper.Events.UpdateEvent;
using System.IO;

namespace HuaHaoERP.View.Pages.Content_Settings
{
    public partial class Page_Settings : Page
    {
        public Page_Settings()
        {
            InitializeComponent();
            PermissionsSettings();
            SubscribeToEvent();
            if(Helper.DataDefinition.CommonParameters.DbPassword != "")
            {
                this.Button_EncryptedDB.Content = "解密数据库";
            }
            RefreshDataGrid_UserControl();
            this.Frame_About.Content = new View.Pages.Content_Others.Page_About();
            this.TabControl_Settings.SelectedIndex = 2;
        }

        private void PermissionsSettings()
        {
            int Permissions = Helper.DataDefinition.CommonParameters.Permissions;
            if (Permissions < 9)
            {
                this.Button_EncryptedDB.Visibility = System.Windows.Visibility.Collapsed;
                this.GroupBox_EncryptedDB.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (Permissions < 8)
            {
                this.GroupBox_UserInfo.Visibility = System.Windows.Visibility.Collapsed;
                this.Expander_SecuritySettings_DB.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void RefreshDataGrid_UserControl()
        {
            List<Model.UserModel> data = new List<Model.UserModel>();
            new ViewModel.Settings.UserConsole().ReadList(out data);
            DataGrid_UserControl.ItemsSource = data;
        }
        private void SubscribeToEvent()
        {
            UserEvent.EUpdateDataGrid += (sender, e) =>
            {
                RefreshDataGrid_UserControl();
            };
        }
        private void Button_ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            bool Check = true;
            string PasswordOld = TranslatePassword.TranslateToString(this.PasswordBox_Old.SecurePassword);
            string PasswordNew = TranslatePassword.TranslateToString(this.PasswordBox_New.SecurePassword);
            string PasswordNewRepeat = TranslatePassword.TranslateToString(this.PasswordBox_NewRepeat.SecurePassword);
            if(!new ChangePasswordConsole().CheckPassword(Helper.DataDefinition.CommonParameters.LoginUserName, PasswordOld))
            {
                this.Label_Message.Content = "错误：原始密码错误";
                this.PasswordBox_Old.Clear();
                this.PasswordBox_Old.Focus();
                Check = false;
            }
            if (PasswordNew != PasswordNewRepeat || PasswordNew.Length == 0)
            {
                this.Label_Message.Content = "错误：新密码不一致";
                Check = false;
            }
            if(Check)
            {
                if(new ChangePasswordConsole().ChangePassword(Helper.DataDefinition.CommonParameters.LoginUserName, PasswordNew))
                {
                    this.PasswordBox_Old.Clear();
                    this.PasswordBox_New.Clear();
                    this.PasswordBox_NewRepeat.Clear();
                    //this.Label_Message.Content = "修改密码成功，请妥善保管新密码。";
                    MessageBox.Show("修改密码成功，请妥善保管新密码。", "提示");
                }
            }
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Label).Visibility = System.Windows.Visibility.Hidden;
        }

        private void Button_EncryptedDB_Click(object sender, RoutedEventArgs e)
        {
            if(Helper.DataDefinition.CommonParameters.DbPassword == "")
            {
                new ViewModel.Settings.EncryptedDBConsole().Encrypted(Guid.NewGuid().ToString());
                this.Button_EncryptedDB.Content = "解密数据库";
            }
            else
            {
                new ViewModel.Settings.EncryptedDBConsole().Decryption();
                this.Button_EncryptedDB.Content = "加密数据库";
            }
        }

        private void ExpanderSecuritySettings_Expanded(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                Expander ep = sender as Expander;
                foreach(Expander epd in this.StackPanel_SecuritySettings.Children)
                {
                    if(epd != ep)
                    {
                        epd.IsExpanded = false;
                    }
                }
                
            }
        }
        private void DataGrid_UserControl_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_UserControl.SelectedCells.Count != 0)
            {
                HuaHaoERP.Model.UserModel data = this.DataGrid_UserControl.SelectedCells[0].Item as HuaHaoERP.Model.UserModel;
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_Settings_Popup_AddUser(data));
            }
        }
        private void Button_AddUser_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Settings_Popup_AddUser());
        }

        private void Button_DelUser_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_UserControl.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.UserModel data = this.DataGrid_UserControl.SelectedCells[0].Item as HuaHaoERP.Model.UserModel;
                if (MessageBox.Show("确认删除用户：" + data.Username + "？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.Settings.UserConsole().MarkDelete(data);
                    RefreshDataGrid_UserControl();
                }
            }
        }

        private void Button_ChangeLoginBackground_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.Title = "请选择要导入的图片文件";
            open.Filter = "图片文件|*.jpg";
            open.RestoreDirectory = true;
            if ((bool)open.ShowDialog().GetValueOrDefault())
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Background"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Background");
                }
                try
                {
                    File.Copy(open.FileName, AppDomain.CurrentDomain.BaseDirectory + "Background\\LoginBackground.jpg", true);
                    Helper.Events.UpdateEvent.BackgroundEvent.OnUpdateLoginBackground();
                }
                catch(Exception)
                {

                }
            }
        }

        private void Button_ChangeMainBackground_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.Title = "请选择要导入的图片文件";
            open.Filter = "图片文件|*.jpg";
            open.RestoreDirectory = true;
            if ((bool)open.ShowDialog().GetValueOrDefault())
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Background"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Background");
                }
                try
                {
                    File.Copy(open.FileName, AppDomain.CurrentDomain.BaseDirectory + "Background\\Background.jpg", true);
                    Helper.Events.UpdateEvent.BackgroundEvent.OnUpdateBackground();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
