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

namespace HuaHaoERP.View.Pages.Content_Settings
{
    public partial class Page_Settings : Page
    {
        public Page_Settings()
        {
            InitializeComponent();
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
                    this.Label_Message.Content = "修改密码成功，请妥善保管新密码。";
                }
            }
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Label).Visibility = System.Windows.Visibility.Hidden;
        }

        private void Button_EncryptedDB_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
