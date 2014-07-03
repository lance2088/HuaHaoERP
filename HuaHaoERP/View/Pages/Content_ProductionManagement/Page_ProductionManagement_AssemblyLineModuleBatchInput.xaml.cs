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
    public partial class Page_ProductionManagement_AssemblyLineModuleBatchInput : Page
    {
        ObservableCollection<Model_AssemblyLineModuleBatchInput> data = new ObservableCollection<Model_AssemblyLineModuleBatchInput>();
        Model_BatchInputOrder OrderData;
        bool IS_MODIFY = false;//是否是修改模式

        public Page_ProductionManagement_AssemblyLineModuleBatchInput()
        {
            InitializeComponent();
            InitializeDataGrid();
            this.TextBox_Number.Text = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }

        public Page_ProductionManagement_AssemblyLineModuleBatchInput(Model_BatchInputOrder data)
        {
            this.OrderData = data;
            this.IS_MODIFY = true;
            InitializeComponent();
            InitializeDataGrid();
            this.TextBox_Number.Text = data.Number;
            this.TextBox_Remark.Text = data.Remark;
            this.Button_Commit.Content = "修改";
        }

        private void InitializeDataGrid()
        {
            if(IS_MODIFY)
            {
                data = new AssemblyLineModuleBatchInputConsole().ReadDatas(OrderData); 
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    data.Add(new Model_AssemblyLineModuleBatchInput { Id = i + 1 });
                }
            }
            this.DataGrid_BatchInput.ItemsSource = data;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            string Number = this.TextBox_Number.Text;
            string Remark = this.TextBox_Remark.Text;
            if (new AssemblyLineModuleBatchInputConsole().InsertData(data, (bool)this.CheckBox_AutoDeductionRawMaterials.IsChecked, Number, Remark))
            {
                if(IS_MODIFY)
                {
                    new AssemblyLineModuleBatchInputConsole().DeleteOld(OrderData.Guid);
                }
                Button_Cancel_Click(null, null);
                Helper.Events.UpdateEvent.AssemblyLineModuleEvent.OnUpdateDataGrid();
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if(IS_MODIFY)
            {
                Helper.Events.PopUpEvent.OnShowPopUp(new Content_Warehouse.Page_Warehouse_Product_BatchHistory(1));
            }
            else
            {
                Helper.Events.PopUpEvent.OnHidePopUp();
            }
        }

        private void DataGrid_BatchInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string Header = DataGrid_BatchInput.SelectedCells[0].Column.Header.ToString();
                if (Header == "产品编号")
                {
                    e.Handled = true;
                    DataGrid_BatchInput.CurrentCell = new DataGridCellInfo(DataGrid_BatchInput.SelectedCells[0].Item, DataGrid_BatchInput.Columns[2]);
                }
                else if (Header == "工序")
                {
                    e.Handled = true;
                    DataGrid_BatchInput.CurrentCell = new DataGridCellInfo(DataGrid_BatchInput.SelectedCells[0].Item, DataGrid_BatchInput.Columns[3]);
                }
                else if (Header == "员工编号")
                {
                    e.Handled = true;
                    DataGrid_BatchInput.CurrentCell = new DataGridCellInfo(DataGrid_BatchInput.SelectedCells[0].Item, DataGrid_BatchInput.Columns[5]);
                }
                else if (Header == "数量")
                {
                    e.Handled = true;
                    DataGrid_BatchInput.CurrentCell = new DataGridCellInfo(DataGrid_BatchInput.SelectedCells[0].Item, DataGrid_BatchInput.Columns[6]);
                }
                else if (Header == "损坏")
                {
                    DataGrid_BatchInput.CurrentCell = new DataGridCellInfo(DataGrid_BatchInput.SelectedCells[0].Item, DataGrid_BatchInput.Columns[0]);
                }
                else
                {
                    e.Handled = true;
                    DataGrid_BatchInput.CurrentCell = new DataGridCellInfo(DataGrid_BatchInput.SelectedCells[0].Item, DataGrid_BatchInput.Columns[0]);
                }
                DataGrid_BatchInput.SelectedCells.Clear();
                DataGrid_BatchInput.SelectedCells.Add(DataGrid_BatchInput.CurrentCell);
            }
        }

        private void DataGrid_BatchInput_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Model_AssemblyLineModuleBatchInput model = this.DataGrid_BatchInput.SelectedCells[0].Item as Model_AssemblyLineModuleBatchInput;
            string newValue = (e.EditingElement as TextBox).Text.Trim();
            string Header = e.Column.Header.ToString();
            if (Header == "产品编号")
            {
                Model_AssemblyLineModuleBatchInput m = new AssemblyLineModuleBatchInputConsole().ReadProductInfo(newValue);
                if (m.ProductGuid == new Guid())
                {
                    DataGrid_BatchInput.CurrentCell = new DataGridCellInfo(DataGrid_BatchInput.SelectedCells[0].Item, DataGrid_BatchInput.Columns[0]);
                    return;
                }
                data[data.IndexOf(model)].ProductGuid = m.ProductGuid;
                data[data.IndexOf(model)].ProductName = m.ProductName;
                data[data.IndexOf(model)].ProcessList = m.ProcessList;
                data[data.IndexOf(model)].ProcessListStr = m.ProcessListStr;
            }
            else if (Header == "工序")
            {
                int ProcessNum = 0;
                int.TryParse(newValue, out ProcessNum);
                if (ProcessNum <= 6 && ProcessNum > 0 && data[data.IndexOf(model)].ProcessList[ProcessNum - 1] != null)
                {
                    data[data.IndexOf(model)].Process = data[data.IndexOf(model)].ProcessList[ProcessNum - 1];
                }
                else
                {
                    DataGrid_BatchInput.CurrentCell = new DataGridCellInfo(DataGrid_BatchInput.SelectedCells[0].Item, DataGrid_BatchInput.Columns[2]);
                }
            }
            else if (Header == "员工编号")
            {
                Model_AssemblyLineModuleBatchInput m = new AssemblyLineModuleBatchInputConsole().ReadStaffInfo(newValue);
                if (m.StaffGuid == new Guid())
                {
                    DataGrid_BatchInput.CurrentCell = new DataGridCellInfo(DataGrid_BatchInput.SelectedCells[0].Item, DataGrid_BatchInput.Columns[3]);
                    return;
                }
                data[data.IndexOf(model)].StaffGuid = m.StaffGuid;
                data[data.IndexOf(model)].StaffName = m.StaffName;
            }
        }

        private void CheckBox_AutoDeductionRawMaterials_Click(object sender, RoutedEventArgs e)
        {
            if (this.CheckBox_AutoDeductionRawMaterials.IsChecked == false)
            {
                if (MessageBox.Show("取消自动扣半成品仅适用于“录入初始数据”\n是否继续？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    this.CheckBox_AutoDeductionRawMaterials.IsChecked = true;
                }
            }
        }
    }
}
