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

namespace HuaHaoERP.View.Pages.Content_Settings
{
    /// <summary>
    /// Interaction logic for Page_Settings_Popup_AddUser.xaml
    /// </summary>
    public partial class Page_Settings_Popup_AddUser : Page
    {
        private ViewModel.Settings.UserConsole uc = new ViewModel.Settings.UserConsole();
        public Page_Settings_Popup_AddUser()
        {
            InitializeComponent();
            ComboBox_用户权限.ItemsSource = uc.GetComboBoxPermissions();
            ComboBox_用户权限.DisplayMemberPath = "DisplayPermissions";
            ComboBox_用户权限.SelectedValuePath = "Permissions";
            ComboBox_用户权限.SelectedIndex = 0;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_用户名_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void TextBox_用户密码_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }
    }
}
