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
        bool IsOUT = true;
        bool IS_MODIFY = false;//是否是修改模式

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
                this.Label_Title.Content = "外加工单：出单";
            }
            else
            {
                this.Label_Title.Content = "外加工单：入单";
            }
            this.DatePicker_InsertDate.SelectedDate = DateTime.Now;
            this.TextBox_Number.Text = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }

        public Page_ProductionManagement_OutsideProcessBatch(Model_BatchInputOrder data)
        {
            this.OrderData = data;
            this.IS_MODIFY = true;
            this.IsOUT = (data.OrderType == "0") ? true : false;
            InitializeComponent();
            InitializeDataGrid();
            if (IsOUT)
            {
                this.DataGridTextColumn_MinorInjuries.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_Injuries.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_Lose.Visibility = System.Windows.Visibility.Collapsed;
                this.Label_Title.Content = "外加工单：出单";
            }
            else
            {
                this.Label_Title.Content = "外加工单：入单";
            }
            this.DatePicker_InsertDate.SelectedDate = Convert.ToDateTime(data.Date);
            this.TextBox_Number.Text = data.Number;
            this.TextBox_Remark.Text = data.Remark;
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
            this.DataGrid.ItemsSource = data;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)this.DatePicker_InsertDate.SelectedDate;
            string Number = this.TextBox_Number.Text;
            string Remark = this.TextBox_Remark.Text;
            if (new OutsideProcessBatchInputConsole().InsertData(data, IsOUT, date, Number, Remark))
            {
                Helper.Events.ProductionManagement_AssemblyLineEvent.OnUpdateDataGrid();
                Button_Cancel_Click(null, null);
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if(IS_MODIFY)
            {
                Helper.Events.PopUpEvent.OnShowPopUp(new Content_Warehouse.Page_Warehouse_Product_BatchHistory(2));
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
                    return;
                }
                data[data.IndexOf(model)].ProductGuid = m.ProductGuid;
                data[data.IndexOf(model)].ProductName = m.ProductName;
                data[data.IndexOf(model)].Material = m.Material;
            }
            else if (Header == "加工商编号")
            {
                Model_ProductionManagement_OutsideProcessBatch m = new OutsideProcessBatchInputConsole().ReadProcessorsInfo(newValue);
                if (m.ProcessorsGuid == new Guid())
                {
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[3]);
                    return;
                }
                data[data.IndexOf(model)].ProcessorsGuid = m.ProcessorsGuid;
                data[data.IndexOf(model)].ProcessorsName = m.ProcessorsName;
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
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[3]);//跳加工商编号
                }
                else if (Header == "加工商编号")
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
    }
}
