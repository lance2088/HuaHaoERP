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

namespace HuaHaoERP.View.Pages.Content_Orders
{
    /// <summary>
    /// Interaction logic for Page_Orders.xaml
    /// </summary>
    public partial class Page_Orders : Page
    {
        public Page_Orders()
        {
            InitializeComponent();
        }

        private void Button_AddProductOrder_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_Orders_Product());
        }
    }
}
