using HuaHaoERP.Model.Order;
using HuaHaoERP.ViewModel.Orders;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse_Product_BatchHistory : Page
    {
        ObservableCollection<Model_BatchInputOrder> data = new ObservableCollection<Model_BatchInputOrder>();

        public Page_Warehouse_Product_BatchHistory()
        {
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            new BatchInputOrderConsole().ReadOrder(out data);
            this.DataGrid_BatchHistory.ItemsSource = data;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }
    }
}
