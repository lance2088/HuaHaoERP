using HuaHaoERP.Model.Warehouse;
using HuaHaoERP.ViewModel.Warehouse;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse_Product_PackingIn : Page
    {
        ObservableCollection<Model_WarehouseProductPackingIn> data = new ObservableCollection<Model_WarehouseProductPackingIn>();
        /// <summary>
        /// 0包装in 1散件in 2包装out 3散件out
        /// </summary>
        int TYPE = 0;

        public Page_Warehouse_Product_PackingIn(int Type)
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
        }

        private void InitializeDataGrid()
        {
            for (int i = 0; i < 20; i++)
            {
                data.Add(new Model_WarehouseProductPackingIn { Id = i + 1 });
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
            if (TYPE == 0)
            {
                if (new ViewModel.Warehouse.ProductPackingInConsole().InsertPacking(data, false))
                {
                    Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
                    Button_Cancel_Click(null, null);
                }
            }
            else if (TYPE == 1)
            {
                if (new ViewModel.Warehouse.ProductPackingInConsole().InsertSpareparts(data, false))
                {
                    Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
                    Button_Cancel_Click(null, null);
                }
            }
            else if (TYPE == 2)
            {
                if (new ViewModel.Warehouse.ProductPackingInConsole().InsertPacking(data, true))
                {
                    Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
                    Button_Cancel_Click(null, null);
                }
            }
            else if (TYPE == 3)
            {
                if (new ViewModel.Warehouse.ProductPackingInConsole().InsertSpareparts(data, true))
                {
                    Helper.Events.UpdateEvent.WarehouseProductEvent.OnUpdateDataGrid();
                    Button_Cancel_Click(null, null);
                }
            }
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Model_WarehouseProductPackingIn model = this.DataGrid.SelectedCells[0].Item as Model_WarehouseProductPackingIn;
            string newValue = (e.EditingElement as TextBox).Text.Trim();
            string Header = e.Column.Header.ToString();
            if (Header == "编号")
            {
                Model_WarehouseProductPackingIn m = new ProductPackingInConsole().ReadProductInfo(newValue);
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
