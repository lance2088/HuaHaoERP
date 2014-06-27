using System.Windows;
using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_Orders
{
    public partial class Page_Orders_Processors : Page
    {
        public Page_Orders_Processors()
        {
            InitializeComponent();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }
    }
}
