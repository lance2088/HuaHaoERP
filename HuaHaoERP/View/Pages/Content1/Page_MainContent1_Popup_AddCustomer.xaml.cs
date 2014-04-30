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

namespace HuaHaoERP.View.Pages.Content1
{
    /// <summary>
    /// Interaction logic for Page_MainContent1_Popup_AddClient.xaml
    /// </summary>
    public partial class Page_MainContent1_Popup_AddCustomer : Page
    {
        public Page_MainContent1_Popup_AddCustomer()
        {
            InitializeComponent();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp(this, new Helper.Events.PopUpEventArgs());
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp(this, new Helper.Events.PopUpEventArgs());
        }
    }
}
