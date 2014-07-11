using HuaHaoERP.Model.Order;
using HuaHaoERP.Model.ProductionManagement;
using HuaHaoERP.ViewModel.ProductionManagement;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    /// <summary>
    /// 批量导入导出外加工单Page
    /// </summary>
    public partial class Page_ProductionManagement_OutsideProcessBatch : Page
    {
        ObservableCollection<Model_ProductionManagement_OutsideProcessBatch> data = new ObservableCollection<Model_ProductionManagement_OutsideProcessBatch>();
        Model_BatchInputOrder OrderData;
        bool IsOUT = true;//是否出单
        bool IS_MODIFY = false;//是否是修改模式
        bool IS_CommitSuccess = false;//提交是否成功
        int OrderIndex;
        Guid ProcessorsGuid = new Guid();
        bool Is_LockProcessors = false;

        /// <summary>
        /// 正常录入模式
        /// </summary>
        /// <param name="Out"></param>
        public Page_ProductionManagement_OutsideProcessBatch(bool Out)
        {
            this.IsOUT = Out;
            InitializeComponent();
            InitializeDataGrid();
            if (IsOUT)
            {
                this.DataGridTextColumn_MinorInjuries.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_Injuries.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_Lose.Visibility = System.Windows.Visibility.Collapsed;
                this.Label_Title.Content = "外加工单：抛光领货";
            }
            else
            {
                this.Label_Title.Content = "外加工单：抛光交货";
            }
            this.DatePicker_InsertDate.SelectedDate = DateTime.Now;
            this.TextBox_Number.Text = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            this.TextBox_Remark.Text = this.Label_Title.Content.ToString();
            this.TextBox_Processors.Focus();
            this.CheckBox_LockProcessors.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// 修改模式
        /// </summary>
        /// <param name="data"></param>
        public Page_ProductionManagement_OutsideProcessBatch(Model_BatchInputOrder data, bool LockProcessors)
        {
            this.Is_LockProcessors = LockProcessors;
            this.OrderData = data;
            this.OrderIndex = Helper.DataDefinition.CommonParameters.OrderNoList.IndexOf(OrderData);
            this.IS_MODIFY = true;
            this.IsOUT = (data.OrderType == "0") ? true : false;
            InitializeComponent();
            InitializeDataGrid();
            InitializeButton();
            if (IsOUT)
            {
                this.DataGridTextColumn_MinorInjuries.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_Injuries.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_Lose.Visibility = System.Windows.Visibility.Collapsed;
                this.Label_Title.Content = "外加工单：抛光领货";
            }
            else
            {
                this.Label_Title.Content = "外加工单：抛光交货";
            }
            this.DatePicker_InsertDate.SelectedDate = Convert.ToDateTime(data.Date);
            this.TextBox_Number.Text = data.Number;
            this.TextBox_Remark.Text = data.Remark;
            this.Button_Commit.Content = "修改";
            this.TextBox_Processors.Focus();
            this.CheckBox_LockProcessors.IsChecked = this.Is_LockProcessors;
        }

        private void InitializeButton()
        {
            if (OrderIndex == 0)
            {
                this.Button_PreviousOrder.IsEnabled = false;
            }
            else if (OrderIndex == Helper.DataDefinition.CommonParameters.OrderNoList.Count - 1)
            {
                this.Button_NextOrder.IsEnabled = false;
            }
        }

        private void InitializeDataGrid()
        {
            if (IS_MODIFY)
            {
                data = new OutsideProcessBatchInputConsole().ReadDatas(OrderData);
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    data.Add(new Model_ProductionManagement_OutsideProcessBatch { Id = i + 1 });
                }
            }
            if (data[0].ProcessorsGuid != new Guid())
            {
                this.ProcessorsGuid = data[0].ProcessorsGuid;
                this.TextBox_Processors.Text = data[0].ProcessorsName;
            }
            this.DataGrid.ItemsSource = data;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)this.DatePicker_InsertDate.SelectedDate + DateTime.Now.TimeOfDay;
            string Number = this.TextBox_Number.Text;
            string Remark = this.TextBox_Remark.Text;
            if (ProcessorsGuid == new Guid())
            {
                MessageBox.Show("加工商不存在，请重新录入", "错误");
                this.TextBox_Processors.Focus();
                return;
            }
            foreach (Model_ProductionManagement_OutsideProcessBatch m in data)
            {
                m.ProcessorsGuid = ProcessorsGuid;
            }
            if (new OutsideProcessBatchInputConsole().InsertData(data, IsOUT, date, Number, Remark))
            {
                if (IS_MODIFY)
                {
                    new OutsideProcessBatchInputConsole().DeleteOld(OrderData.Guid);
                }
                IS_CommitSuccess = true;
                Helper.Events.ProductionManagement_AssemblyLineEvent.OnUpdateDataGrid();
                Button_Cancel_Click(null, null);
            }
            else
            {
                IS_CommitSuccess = false;
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (IS_MODIFY)
            {
                Helper.Events.PopUpEvent.OnShowPopUp(new Content_Warehouse.Page_Warehouse_Product_BatchHistory(2, Is_LockProcessors));
            }
            else
            {
                Helper.Events.PopUpEvent.OnHidePopUp();
            }
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Model_ProductionManagement_OutsideProcessBatch model = this.DataGrid.SelectedCells[0].Item as Model_ProductionManagement_OutsideProcessBatch;
            string newValue = (e.EditingElement as TextBox).Text.Trim();
            string Header = e.Column.Header.ToString();
            if (Header == "产品编号")
            {
                Model_ProductionManagement_OutsideProcessBatch m = new OutsideProcessBatchInputConsole().ReadProductInfo(newValue);
                if (m.ProductGuid == new Guid())
                {
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[0]);
                    data[data.IndexOf(model)].ProductGuid = new Guid();
                    data[data.IndexOf(model)].ProductName = "";
                    data[data.IndexOf(model)].Material = "";
                }
                else if (m.HasPolishing == false)
                {
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[0]);
                    data[data.IndexOf(model)].ProductGuid = new Guid();
                    data[data.IndexOf(model)].ProductName = "";
                    data[data.IndexOf(model)].Material = "";
                    data[data.IndexOf(model)].Remark = "该产品没有抛光工序，请重新录入。";
                }
                else
                {
                    data[data.IndexOf(model)].ProductGuid = m.ProductGuid;
                    data[data.IndexOf(model)].ProductName = m.ProductName;
                    data[data.IndexOf(model)].Material = m.Material;
                    if (data[data.IndexOf(model)].Remark == "该产品没有抛光工序，请重新录入。")
                    {
                        data[data.IndexOf(model)].Remark = "";
                    }
                }
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string Header = DataGrid.SelectedCells[0].Column.Header.ToString();
                if (Header == "产品编号")
                {
                    e.Handled = true;
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[5]);//跳数量
                }
                else if (Header == "备注")
                {
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[0]);
                }
                else
                {
                    if (!IsOUT)
                    {
                        if (Header == "数量")
                        {
                            e.Handled = true;
                            DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[6]);
                        }
                        else if (Header == "轻伤")
                        {
                            e.Handled = true;
                            DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[7]);
                        }
                        else if (Header == "重伤")
                        {
                            e.Handled = true;
                            DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[8]);
                        }
                        else if (Header == "丢失")
                        {
                            e.Handled = true;
                            DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[9]);
                        }
                    }
                    else
                    {
                        if (Header == "数量")
                        {
                            e.Handled = true;
                            DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[9]);
                        }
                    }
                }
                DataGrid.SelectedCells.Clear();
                DataGrid.SelectedCells.Add(DataGrid.CurrentCell);
            }
        }

        /// <summary>
        /// 上一单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_PreviousOrder_Click(object sender, RoutedEventArgs e)
        {
            int i = 1;
            Model_BatchInputOrder PreviousOrder = Helper.DataDefinition.CommonParameters.OrderNoList[OrderIndex - i];
            if (Is_LockProcessors)
            {
                while (PreviousOrder.ProcessorsID != this.ProcessorsGuid)
                {
                    i++;
                    if (OrderIndex - i < 0)
                    {
                        MessageBox.Show("已到第一单", "提示");
                        return;
                    }
                    PreviousOrder = Helper.DataDefinition.CommonParameters.OrderNoList[OrderIndex - i];
                }
            }
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_ProductionManagement_OutsideProcessBatch(PreviousOrder, Is_LockProcessors));
        }

        /// <summary>
        /// 下一单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_NextOrder_Click(object sender, RoutedEventArgs e)
        {
            int i = 1;
            Model_BatchInputOrder NextOrder = Helper.DataDefinition.CommonParameters.OrderNoList[OrderIndex + i];
            if (Is_LockProcessors)
            {
                while (NextOrder.ProcessorsID != this.ProcessorsGuid)
                {
                    i++;
                    if (OrderIndex + i >= Helper.DataDefinition.CommonParameters.OrderNoList.Count)
                    {
                        MessageBox.Show("已到最后一单", "提示");
                        return;
                    }
                    NextOrder = Helper.DataDefinition.CommonParameters.OrderNoList[OrderIndex + i];
                }
            }
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_ProductionManagement_OutsideProcessBatch(NextOrder, Is_LockProcessors));
        }

        private void Button_CommitNew_Click(object sender, RoutedEventArgs e)
        {
            Button_Commit_Click(null, null);
            if (IS_CommitSuccess)
            {
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_ProductionManagement_OutsideProcessBatch(IsOUT));
            }
        }

        private void TextBox_Processors_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.IsFocused)
            {
                if (e.Key == Key.Enter)
                {
                    string No = tb.Text;
                    Model_ProductionManagement_OutsideProcessBatch m = new OutsideProcessBatchInputConsole().ReadProcessorsInfo(No);
                    if (m.ProcessorsGuid != new Guid())
                    {
                        ProcessorsGuid = m.ProcessorsGuid;
                        tb.Text = m.ProcessorsName;
                    }
                    else
                    {
                        ProcessorsGuid = new Guid();
                        tb.Text = "";
                    }
                }
            }
        }

        private void CheckBox_LockProcessors_Click(object sender, RoutedEventArgs e)
        {
            this.Is_LockProcessors = (bool)this.CheckBox_LockProcessors.IsChecked;
        }

    }
}
