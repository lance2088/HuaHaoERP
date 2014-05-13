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
using System.Windows.Threading;

namespace HuaHaoERP.View.Windows
{
    public partial class Login : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private double ShowSeconds = 0;

        public Login()
        {
            InitializeComponent();
            Helper.AppInitialize.Initialize.Init();
            this.PasswordBox_LoginPassword.Focus();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);//设置刷新的间隔时间
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (ShowSeconds > 0)
            {
                ShowSeconds -= 1;
            }
            else if (ShowSeconds > -1)
            {
                this.Label_Message.Content = "";
                ShowSeconds = -1;
                timer.Stop();
            }
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
                this.Label_Message.Content = "用户名或密码错误";
                this.PasswordBox_LoginPassword.Clear();
                ShowSeconds = 5;
                timer.Start();
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
