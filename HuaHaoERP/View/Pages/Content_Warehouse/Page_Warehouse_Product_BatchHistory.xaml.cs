using HuaHaoERP.Model.Order;
using HuaHaoERP.ViewModel.Orders;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    /// <summary>
    /// 流水线，外加工的批量记录也用这个页面
    /// </summary>
    public partial class Page_Warehouse_Product_BatchHistory : Page
    {
        ObservableCollection<Model_BatchInputOrder> data = new ObservableCollection<Model_BatchInputOrder>();
        int OrderType;
        /// <summary>
        /// 1流水线 2外加工 3仓库
        /// </summary>
        /// <param name="OrderType"></param>
        public Page_Warehouse_Product_BatchHistory(int OrderType)
        {
            this.OrderType = OrderType;
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            new BatchInputOrderConsole().ReadOrder(OrderType, out data);
            this.DataGrid_BatchHistory.ItemsSource = data;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
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
                Model_BatchInputOrder m = this.DataGrid_BatchHistory.SelectedCells[0].Item as Model_BatchInputOrder;
                if (OrderType == 1)//流水线
                {
                    Helper.Events.PopUpEvent.OnShowPopUp(new Content_ProductionManagement.Page_ProductionManagement_AssemblyLineModuleBatchInput(m));
                }
                else if (OrderType == 2)//外加工
                {
                    Helper.Events.PopUpEvent.OnShowPopUp(new Content_ProductionManagement.Page_ProductionManagement_OutsideProcessBatch(m));
                }
                else if (OrderType == 3)//仓库
                {
                    Helper.Events.PopUpEvent.OnShowPopUp(new Page_Warehouse_Product_BatchIn(m));
                }
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
                    if (new BatchInputOrderConsole().DeleteOrder(OrderType, OrderGuid))
                    {
                        InitializeDataGrid();
                        Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
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
