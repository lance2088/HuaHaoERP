using HuaHaoERP.Model.Order;
using HuaHaoERP.Model.Warehouse;
using HuaHaoERP.ViewModel.Warehouse;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse_Product_BatchIn : Page
    {
        ObservableCollection<Model_WarehouseProductBatchIn> data = new ObservableCollection<Model_WarehouseProductBatchIn>();
        Model_BatchInputOrder ORDER;
        /// <summary>
        /// 0包装in 1散件in 2包装out 3散件out
        /// </summary>
        int TYPE = -1;
        bool IS_MODIFY = false;//是否是修改模式

        public Page_Warehouse_Product_BatchIn(int Type)
        {
            this.TYPE = Type;
            InitializeComponent();
            InitializeDataGrid();
            switch (TYPE)
            {
                case 0:
                    this.Label_Title.Content = "入库：包装产品";
                    break;
                case 1:
                    this.Label_Title.Content = "入库：散件产品";
                    break;
                case 2:
                    this.Label_Title.Content = "出库：包装产品";
                    break;
                case 3:
                    this.Label_Title.Content = "出库：散件产品";
                    break;
            }
            this.DatePicker_InsertDate.SelectedDate = DateTime.Now;
            this.TextBox_Number.Text = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            this.TextBox_Remark.Text = this.Label_Title.Content.ToString();
        }

        public Page_Warehouse_Product_BatchIn(Model_BatchInputOrder Order)
        {
            this.ORDER = Order;
            IS_MODIFY = true;
            InitializeComponent();
            this.TYPE = int.Parse(Order.OrderType);
            this.DatePicker_InsertDate.SelectedDate = Convert.ToDateTime(Order.Date);
            this.TextBox_Number.Text = Order.Number;
            this.TextBox_Remark.Text = Order.Remark;
            this.Button_Commit.Content = "修改";
            InitializeDataGrid();
            switch (TYPE)
            {
                case 0:
                    this.Label_Title.Content = "入库：包装产品";
                    break;
                case 1:
                    this.Label_Title.Content = "入库：散件产品";
                    break;
                case 2:
                    this.Label_Title.Content = "出库：包装产品";
                    break;
                case 3:
                    this.Label_Title.Content = "出库：散件产品";
                    break;
            }
        }

        private void InitializeDataGrid()
        {
            if (IS_MODIFY)
            {
                data = new ProductBatchInConsole().ReadDatas(ORDER.Guid, ORDER.OrderType);
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    data.Add(new Model_WarehouseProductBatchIn { Id = i + 1 });
                }
            }
            this.DataGrid.ItemsSource = data;
            if (TYPE == 1 || TYPE == 3)
            {
                this.DataGridTextColumn_PackQuantity.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_PerQuantity.Visibility = System.Windows.Visibility.Collapsed;
                this.DataGridTextColumn_AllQuantity.IsReadOnly = false;
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)this.DatePicker_InsertDate.SelectedDate;
            string Number = this.TextBox_Number.Text;
            string Remark = this.TextBox_Remark.Text;
            if (TYPE == 0)
            {
                if (new ViewModel.Warehouse.ProductBatchInConsole().InsertPacking(data, false, date, Number, Remark, "0"))
                {
                    Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
                    Button_Cancel_Click(null, null);
                }
            }
            else if (TYPE == 1)
            {
                if (new ViewModel.Warehouse.ProductBatchInConsole().InsertSpareparts(data, false, date, Number, Remark, "1"))
                {
                    Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
                    Button_Cancel_Click(null, null);
                }
            }
            else if (TYPE == 2)
            {
                if (new ViewModel.Warehouse.ProductBatchInConsole().InsertPacking(data, true, date, Number, Remark, "2"))
                {
                    Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
                    Button_Cancel_Click(null, null);
                }
            }
            else if (TYPE == 3)
            {
                if (new ViewModel.Warehouse.ProductBatchInConsole().InsertSpareparts(data, true, date, Number, Remark, "3"))
                {
                    Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
                    Button_Cancel_Click(null, null);
                }
            }
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Model_WarehouseProductBatchIn model = this.DataGrid.SelectedCells[0].Item as Model_WarehouseProductBatchIn;
            string newValue = (e.EditingElement as TextBox).Text.Trim();
            string Header = e.Column.Header.ToString();
            if (Header == "编号")
            {
                Model_WarehouseProductBatchIn m = new ProductBatchInConsole().ReadProductInfo(newValue);
                if (m.Guid == new Guid())
                {
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[0]);
                    return;
                }
                data[data.IndexOf(model)].Guid = m.Guid;
                data[data.IndexOf(model)].Name = m.Name;
                data[data.IndexOf(model)].Material = m.Material;
                data[data.IndexOf(model)].PerQuantity = m.PerQuantity;
            }
            else if (Header == "件数")
            {
                int PackQuantity = 0;
                if (!int.TryParse(newValue, out PackQuantity))
                {
                    (e.EditingElement as TextBox).Text = "0";
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[3]);
                    return;
                }
                data[data.IndexOf(model)].PackQuantity = PackQuantity;
                data[data.IndexOf(model)].AllQuantity = data[data.IndexOf(model)].PackQuantity * data[data.IndexOf(model)].PerQuantity;
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataGrid.SelectedCells[0].Column.Header.ToString() != "件数" && DataGrid.SelectedCells[0].Column.Header.ToString() != "数量")
                {
                    e.Handled = true;
                    if (TYPE == 0 || TYPE == 2)
                    {
                        DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[3]);
                    }
                    else if (TYPE == 1 || TYPE == 3)
                    {
                        DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[5]);
                    }
                    DataGrid.SelectedCells.Clear();
                    DataGrid.SelectedCells.Add(DataGrid.CurrentCell);
                }
                else
                {
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[0]);
                }
            }
        }
    }
}
