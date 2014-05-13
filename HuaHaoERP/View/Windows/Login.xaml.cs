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
using System.Windows.Shapes;

namespace HuaHaoERP.View.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Helper.AppInitialize.Initialize.Init();
            this.PasswordBox_LoginPassword.Focus();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            string UserName = this.TextBox_LoginUserName.Text.Trim();
            string Password = Helper.Tools.TranslatePassword.TranslateToString(this.PasswordBox_LoginPassword.SecurePassword);
            if(new ViewModel.Security.LoginConsole().LoginAuthentication(UserName, Password))
            {
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                //
            }
            
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
