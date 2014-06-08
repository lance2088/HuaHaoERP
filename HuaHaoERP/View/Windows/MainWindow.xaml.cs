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
using HuaHaoERP.Helper.Events;
using System.IO;

namespace HuaHaoERP
{
    public partial class MainWindow : Window
    {
        private Rect WorkRect = SystemParameters.WorkArea;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            SubscribeToEvent();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Background\\Background.jpg";
            UpdateBackground();
            Helper.Events.UpdateEvent.BackgroundEvent.EUpdateBackground += (s, e) =>
            {
                UpdateBackground();
            };
        }
        private void UpdateBackground()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Background\\Background.jpg";
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
        private void InitializeData()
        {
            this.Frame_Head.Content = new View.Pages.Page_Head();
            this.Frame_Content.Content = new View.Pages.Page_Content();
            this.Frame_StatusBar.Content = new View.Pages.Page_StatusBar();
            if (Properties.Settings.Default.isMainWindowRectMax == true)
            {
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                this.Top = 0;
                this.Left = 0;
                this.Width = WorkRect.Width;
                this.Height = WorkRect.Height;
            }
            else if (Properties.Settings.Default.MainWindowRect != new Rect(0,0,0,0))
            {
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                this.Width = Properties.Settings.Default.MainWindowRect.Width;
                this.Height = Properties.Settings.Default.MainWindowRect.Height;
                this.Top = Properties.Settings.Default.MainWindowRect.Top;
                this.Left = Properties.Settings.Default.MainWindowRect.Left;
            }
        }

        private void SubscribeToEvent()
        {
            PopUpEvent.EShowPopUp += Grid_Popup_Show;
            PopUpEvent.EHidePopUp += Grid_Popup_Hide;
        }
        private void Grid_Popup_Show(object sender, PopUpEventArgs e)
        {
            this.Frame_Popup.Content = e.ClassObject;
            this.Grid_Popup.Visibility = System.Windows.Visibility.Visible;
        }
        private void Grid_Popup_Hide(object sender, PopUpEventArgs e)
        {
            this.Frame_Popup.Content = null;
            this.Grid_Popup.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void SaveRect()
        {
            Properties.Settings.Default.MainWindowRect = new Rect(this.Left, this.Top, this.Width, this.Height);
        }

        private void Window_MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && Properties.Settings.Default.isMainWindowRectMax == false)
            {
                this.DragMove();
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.isMainWindowRectMax == false)
            {
                SaveRect();
            }
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Button_Max_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.isMainWindowRectMax)
            {
                Properties.Settings.Default.isMainWindowRectMax = false;
                this.Top = Properties.Settings.Default.MainWindowRect.Top;
                this.Height = Properties.Settings.Default.MainWindowRect.Height;
                this.Left = Properties.Settings.Default.MainWindowRect.Left;
                this.Width = Properties.Settings.Default.MainWindowRect.Width;
            }
            else
            {
                Properties.Settings.Default.isMainWindowRectMax = true;
                SaveRect();//保存非最大化状态的Rect
                this.Width = WorkRect.Width;
                this.Left = 0;
                this.Height = WorkRect.Height;
                this.Top = 0;
            }
        }

        private void Window_MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualHeight > WorkRect.Height || this.ActualWidth > WorkRect.Width)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                Button_Max_Click(null, null);
            }
        }
    }
}
