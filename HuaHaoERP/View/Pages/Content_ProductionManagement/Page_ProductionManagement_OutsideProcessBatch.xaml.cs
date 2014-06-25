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
using HuaHaoERP.Model.ProductionManagement;
using HuaHaoERP.ViewModel.ProductionManagement;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    /// <summary>
    /// 批量导入导出外加工单Page
    /// </summary>
    public partial class Page_ProductionManagement_OutsideProcessBatch : Page
    {
        ObservableCollection<Model_ProductionManagement_OutsideProcessBatch> data = new ObservableCollection<Model_ProductionManagement_OutsideProcessBatch>();
        bool IsOUT = true;

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
        }

        private void InitializeDataGrid()
        {
            for (int i = 0; i < 20; i++)
            {
                data.Add(new Model_ProductionManagement_OutsideProcessBatch { Id = i + 1 });
            }
            this.DataGrid.ItemsSource = data;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (new OutsideProcessBatchConsole().InsertData(data, IsOUT))
            {
                Helper.Events.ProductionManagement_AssemblyLineEvent.OnUpdateDataGrid();
                Button_Cancel_Click(null, null);
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Model_ProductionManagement_OutsideProcessBatch model = this.DataGrid.SelectedCells[0].Item as Model_ProductionManagement_OutsideProcessBatch;
            string newValue = (e.EditingElement as TextBox).Text.Trim();
            string Header = e.Column.Header.ToString();
            if(Header == "产品编号")
            {
                Model_ProductionManagement_OutsideProcessBatch m = new OutsideProcessBatchConsole().ReadProductInfo(newValue);
                if (m.ProductGuid == new Guid())
                {
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[0]);
                    return;
                }
                data[data.IndexOf(model)].ProductGuid = m.ProductGuid;
                data[data.IndexOf(model)].ProductName = m.ProductName;
                data[data.IndexOf(model)].Material = m.Material;
            }
            else if(Header == "加工商编号")
            {
                Model_ProductionManagement_OutsideProcessBatch m = new OutsideProcessBatchConsole().ReadProcessorsInfo(newValue);
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
                else if(Header == "备注")
                {
                    DataGrid.CurrentCell = new DataGridCellInfo(DataGrid.SelectedCells[0].Item, DataGrid.Columns[0]);
                }
                else
                {
                    if(!IsOUT)
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
