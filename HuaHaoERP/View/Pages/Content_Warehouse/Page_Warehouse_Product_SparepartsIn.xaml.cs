using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using HuaHaoERP.Model.Warehouse;
using HuaHaoERP.ViewModel.Warehouse;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse_Product_SparepartsIn : Page
    {
        ObservableCollection<ProductSparepartsInModel> data = new ObservableCollection<ProductSparepartsInModel>();

        public Page_Warehouse_Product_SparepartsIn()
        {
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            for (int i = 0; i < 20; i++)
            {
                data.Add(new ProductSparepartsInModel { Id = i+1 });
            }
            this.DataGrid.ItemsSource = data;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            Button_Cancel_Click(null,null);
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ProductSparepartsInModel model = this.DataGrid.SelectedCells[0].Item as ProductSparepartsInModel;
            string newValue = (e.EditingElement as TextBox).Text.Trim();
            string Header = e.Column.Header.ToString();
            if (Header == "编号")
            {
                ProductSparepartsInModel m = new ProductSparepartsInConsole().ReadProductInfo(newValue);
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
            else if(Header == "件数")
            {
                int PackQuantity = 0;
                if(!int.TryParse(newValue, out PackQuantity))
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
            if(e.Key == Key.Enter)
            {
                if(DataGrid.SelectedCells[0].Column.Header.ToString() != "件数")
                {
                    e.Handled = true;
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[3]);
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
