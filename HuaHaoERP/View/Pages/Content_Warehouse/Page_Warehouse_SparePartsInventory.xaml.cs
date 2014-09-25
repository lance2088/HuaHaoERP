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

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    /// <summary>
    /// Interaction logic for Page_Warehouse_SparePartsInventory.xaml
    /// </summary>
    public partial class Page_Warehouse_SparePartsInventory : Page
    {
        public Page_Warehouse_SparePartsInventory()
        {
            InitializeComponent();
        }

        private void TextBox_Search_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Focus();
        }

        private void TextBox_Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
