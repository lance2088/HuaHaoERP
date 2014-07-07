using HuaHaoERP.Model.Order;
using HuaHaoERP.ViewModel.Orders;
using System;
using System.Collections.Generic;
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
            InitializeComboBox();
        }

        /// <summary>
        /// 外加工锁定抛光户专用
        /// </summary>
        /// <param name="OrderType"></param>
        /// <param name="LockProcessors"></param>
        public Page_Warehouse_Product_BatchHistory(int OrderType, bool LockProcessors)
        {
            this.OrderType = OrderType;
            InitializeComponent();
            InitializeDataGrid();
            InitializeComboBox();
            this.CheckBox_LockProcessors.IsChecked = LockProcessors;
        }

        private void InitializeComboBox()
        {
            List<string> ComboBoxData = new List<string>();
            ComboBoxData.Add("全部类型");
            switch (OrderType)
            {
                case 1://流水线
                    this.Label_Type.Visibility = System.Windows.Visibility.Collapsed;
                    this.ComboBox_Type.Visibility = System.Windows.Visibility.Collapsed;
                    this.CheckBox_LockProcessors.Visibility = System.Windows.Visibility.Collapsed;
                    this.DataGridTextColumn_Processors.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case 2://外加工
                    ComboBoxData.Add("抛光领货");
                    ComboBoxData.Add("抛光交货");
                    break;
                case 3://仓库
                    ComboBoxData.Add("包装入库");
                    ComboBoxData.Add("散件入库");
                    ComboBoxData.Add("包装出库");
                    ComboBoxData.Add("散件出库");
                    this.CheckBox_LockProcessors.Visibility = System.Windows.Visibility.Collapsed;
                    this.DataGridTextColumn_Processors.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }
            this.ComboBox_Type.ItemsSource = ComboBoxData;
            this.ComboBox_Type.SelectedIndex = 0;
        }
        private void InitializeDataGrid()
        {
            int Type = this.ComboBox_Type.SelectedIndex;
            new BatchInputOrderConsole().ReadOrder(OrderType, out data, Type);
            this.DataGrid_BatchHistory.ItemsSource = data;
            if (OrderType == 2)
            {
                Helper.DataDefinition.CommonParameters.OrderNoList.Clear();
                foreach (Model_BatchInputOrder m in data)
                {
                    Helper.DataDefinition.CommonParameters.OrderNoList.Add(m);
                }
            }
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
                    if ((bool)this.CheckBox_LockProcessors.IsChecked)
                    {
                        Helper.Events.PopUpEvent.OnShowPopUp(new Content_ProductionManagement.Page_ProductionManagement_OutsideProcessBatch(m, true));
                    }
                    else
                    {
                        Helper.Events.PopUpEvent.OnShowPopUp(new Content_ProductionManagement.Page_ProductionManagement_OutsideProcessBatch(m, false));
                    }
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
                        if (OrderType == 1)
                        {
                            Helper.Events.UpdateEvent.AssemblyLineModuleEvent.OnUpdateDataGrid();
                        }
                        else if (OrderType == 2)
                        {
                            Helper.Events.ProductionManagement_AssemblyLineEvent.OnUpdateDataGrid();
                        }
                        else if (OrderType == 3)
                        {
                            Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
                        }
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

        private void ComboBox_Type_DropDownClosed(object sender, EventArgs e)
        {
            InitializeDataGrid();
        }

    }
}
