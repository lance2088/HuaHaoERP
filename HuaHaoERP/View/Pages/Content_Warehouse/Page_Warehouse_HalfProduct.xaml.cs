using HuaHaoERP.Helper.Events.UpdateEvent.Warehouse;
using HuaHaoERP.Model.Warehouse;
using HuaHaoERP.ViewModel.Warehouse;
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
    /// Interaction logic for Page_Warehouse_HalfProduct.xaml
    /// </summary>
    public partial class Page_Warehouse_HalfProduct : Page
    {
        public Page_Warehouse_HalfProduct()
        {
            InitializeComponent();
            HalfProductEvent.EUpdateDataGrid += (s, e) => { InitializeDataGrid(); };
        }

        private void InitializeDataGrid()
        {
            List<WarehouseHalpProductModel> dd = new List<WarehouseHalpProductModel>();
            new WarehouseHalfProductConsole().ReadDetailsList(this.TextBox_Search.Text.Trim().Replace("'", ""), out dd);
            DataGrid_Num.ItemsSource = dd;
        }

        private void TextBox_Search_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Focus();
        }

        private void TextBox_Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            InitializeDataGrid();
        }
    }
}
