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
using HuaHaoERP.Model.ProductionManagement;
using HuaHaoERP.ViewModel.ProductionManagement;
using System.Collections.ObjectModel;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement_AssemblyLineModuleBatchInput : Page
    {
        ObservableCollection<Model_AssemblyLineModuleBatchInput> data = new ObservableCollection<Model_AssemblyLineModuleBatchInput>();

        public Page_ProductionManagement_AssemblyLineModuleBatchInput()
        {
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            for (int i = 0; i < 20; i++)
            {
                data.Add(new Model_AssemblyLineModuleBatchInput { Id = i + 1 });
            }
            this.DataGrid_BatchInput.ItemsSource = data;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
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

        }
    }
}
