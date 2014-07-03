using HuaHaoERP.Model.Order;
using HuaHaoERP.ViewModel.Orders;
using System;
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
            new BatchInputOrderConsole().ReadOrder(3, out data);
            this.DataGrid_BatchHistory.ItemsSource = data;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
            Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
        }

        /// <summary>
        /// DataGrid RightKey Modify Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Modify_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_BatchHistory.SelectedCells.Count > 0)
            {
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_Warehouse_Product_BatchIn(this.DataGrid_BatchHistory.SelectedCells[0].Item as Model_BatchInputOrder));
            }
        }

        /// <summary>
        /// DataGrid RightKey Del Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Del_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_BatchHistory.SelectedCells.Count > 0)
            {
                Guid OrderGuid = (this.DataGrid_BatchHistory.SelectedCells[0].Item as Model_BatchInputOrder).Guid;
                if (MessageBox.Show("确认删除这条记录？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (new BatchInputOrderConsole().DeleteOrder(3, OrderGuid))
                    {
                        InitializeDataGrid();
                        MessageBox.Show("删除成功。", "石蚁科技");
                    }
                }
            }
        }

        /// <summary>
        /// DataGrid行双击事件=》右键编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_BatchHistory_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            MenuItem_Modify_Click(null, null);
        }
    }
}
