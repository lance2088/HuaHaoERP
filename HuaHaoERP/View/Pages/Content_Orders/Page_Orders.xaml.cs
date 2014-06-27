using HuaHaoERP.Helper.Events;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace HuaHaoERP.View.Pages.Content_Orders
{
    public partial class Page_Orders : Page
    {
        public Page_Orders()
        {
            InitializeComponent();
            InitializeProductOrderData();

            SubscribeToEvent();
        }
        private void SubscribeToEvent()
        {
            ProductOrderEvent.EUpdateDataGrid += (s, e) =>
            {
                InitializeProductOrderData();
            };
        }

        private void InitializeProductOrderData()
        {
            List<Model.ProductOrderModelForDataGrid> data;
            new ViewModel.Orders.ProductOrderConsole().ReadList(out data);
            this.DataGrid_ProductOrder.ItemsSource = data;
        }
        private void Button_AddProductOrder_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Orders_Product());
        }

        private void Button_DeleteProductOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_ProductOrder.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.ProductOrderModelForDataGrid data = this.DataGrid_ProductOrder.SelectedCells[0].Item as HuaHaoERP.Model.ProductOrderModelForDataGrid;
                new ViewModel.Orders.ProductOrderConsole().MarkDelete(data);
                Helper.Events.ProductOrderEvent.OnUpdateDataGrid();
            }
        }

        private void DataGrid_ProductOrder_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_ProductOrder.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.ProductOrderModelForDataGrid data = this.DataGrid_ProductOrder.SelectedCells[0].Item as HuaHaoERP.Model.ProductOrderModelForDataGrid;
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_Orders_Product(data));
            }
        }

        private void Button_AddSupplierOrder_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Orders_Supplier());
        }

        private void Button_DelSupplierOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_AddProcessorsOrder_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Orders_Processors());
        }

        private void Button_DelProcessorsOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
