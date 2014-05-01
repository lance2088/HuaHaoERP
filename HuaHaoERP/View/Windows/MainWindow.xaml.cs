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

namespace HuaHaoERP
{
    public partial class MainWindow : Window
    {
        private Rect WorkRect = SystemParameters.WorkArea;
        private bool isLockSaveRect = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            SubscribeToEvent();
        }

        private void InitializeData()
        {
            this.Frame_Head.Content = new View.Pages.Page_Head();
            this.Frame_Content.Content = new View.Pages.Page_Content();
            this.Frame_StatusBar.Content = new View.Pages.Page_StatusBar();
            if (Properties.Settings.Default.isMainWindowRectMax == true)
            {
                isLockSaveRect = true;
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                this.Top = 0;
                this.Left = 0;
                this.Width = WorkRect.Width;
                this.Height = WorkRect.Height;
                isLockSaveRect = false;
            }
            else if (Properties.Settings.Default.MainWindowRect != new Rect(0,0,0,0))
            {
                isLockSaveRect = true;
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                this.Width = Properties.Settings.Default.MainWindowRect.Width;
                this.Height = Properties.Settings.Default.MainWindowRect.Height;
                this.Top = Properties.Settings.Default.MainWindowRect.Top;
                this.Left = Properties.Settings.Default.MainWindowRect.Left;
                isLockSaveRect = false;
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
                if (isLockSaveRect == false)
                {
                    SaveRect();
                }
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Button_Max_Click(object sender, RoutedEventArgs e)
        {
            isLockSaveRect = true;
            if (Properties.Settings.Default.isMainWindowRectMax)
            {
                Properties.Settings.Default.isMainWindowRectMax = false;
                this.Width = Properties.Settings.Default.MainWindowRect.Width;
                this.Height = Properties.Settings.Default.MainWindowRect.Height;
                this.Top = Properties.Settings.Default.MainWindowRect.Top;
                this.Left = Properties.Settings.Default.MainWindowRect.Left;
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
            isLockSaveRect = false;
        }

        private void Window_MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (isLockSaveRect == false && this.IsLoaded == true)
            {
                SaveRect();
            }
        }
    }
}
