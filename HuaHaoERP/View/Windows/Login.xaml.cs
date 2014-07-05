using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            new Helper.AppStart().Init();
            this.TextBox_LoginUserName.Focus();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);//设置刷新的间隔时间
            UpdateBackground();
            Helper.Events.UpdateEvent.BackgroundEvent.EUpdateLoginBackground += (s, e) =>
            {
                UpdateBackground();
            };
        }

        private void UpdateBackground()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Background\\LoginBackground.jpg";
            if (File.Exists(filePath))
            {
                BitmapImage bitmapImage;
                Image image;
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    FileInfo fi = new FileInfo(filePath);
                    byte[] bytes = reader.ReadBytes((int)fi.Length);
                    reader.Close();

                    image = new Image();
                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(bytes);
                    bitmapImage.EndInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    image.Source = bitmapImage;
                    ImageBrush b3 = new ImageBrush();
                    b3.ImageSource = bitmapImage;
                    this.Grid_Main.Background = b3;
                }
            }
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
            if(Helper.DataDefinition.CommonParameters.IsLockAdminLogin)
            {
                if (UserName != "root" && UserName !="admin")
                {
                    this.Label_Message.Content = "请使用管理员帐号登陆";
                    this.PasswordBox_LoginPassword.Clear();
                    this.TextBox_LoginUserName.Clear();
                    this.TextBox_LoginUserName.Focus();
                    ShowSeconds = 5;
                    timer.Start();
                    return;
                }
            }
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
                this.PasswordBox_LoginPassword.Focus();
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

        private void TextBox_LoginUserName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.TextBox_LoginUserName.IsFocused == true)
                {
                    this.PasswordBox_LoginPassword.Focus();
                }
            }
        }
    }
}
