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
            string PasswordOld = TranslatePassword.TranslateToString(this.PasswordBox_Old.SecurePassword);
            string PasswordNew = TranslatePassword.TranslateToString(this.PasswordBox_New.SecurePassword);
            string PasswordNewRepeat = TranslatePassword.TranslateToString(this.PasswordBox_NewRepeat.SecurePassword);
            if(new ViewModel.Settings.ChangePasswordConsole().CheckPassword(Helper.DataDefinition.CommonParameters.LoginUserName, PasswordOld))
            {

            }
            else if(PasswordNew != PasswordNewRepeat)
            {

            }
            else
            {

            }
        }
    }
}
