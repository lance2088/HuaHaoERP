using HuaHaoERP.Helper.Events.UpdateEvent;
using HuaHaoERP.Model;
using HuaHaoERP.ViewModel.Warehouse;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse_RawMaterials : Page
    {
        private ViewModel.Warehouse.RawMaterialsConsole vmc = new RawMaterialsConsole();
        private List<RawMaterialsDetailModel> data = new List<RawMaterialsDetailModel>();
        public Page_Warehouse_RawMaterials()
        {
            InitializeComponent();
            data = new List<RawMaterialsDetailModel>();
            for (int i = 0; i < 18; i++)
            {
                RawMaterialsDetailModel m = new RawMaterialsDetailModel();
                m.Id = i + 1;
                m.Date = DateTime.Now.ToString("yyyy.MM.dd");
                m.Operator = Helper.DataDefinition.CommonParameters.RealName;
                m.RawMaterialsID = new Guid();
                m.Type = "入库";
                data.Add(m);
            }
            DataGrid_RawMaterials.ItemsSource = data;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            bool flag = new ViewModel.Warehouse.RawMaterialsConsole().AddByBatch(data, true);
            if (flag)
            {
                WarehouseRawMaterialsEvent.OnUpdateDataGrid();
                Helper.Events.PopUpEvent.OnHidePopUp();
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void DataGrid_RawMaterials_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            RawMaterialsDetailModel model = this.DataGrid_RawMaterials.SelectedCells[0].Item as RawMaterialsDetailModel;
            string newValue = (e.EditingElement as TextBox).Text;
            string HeaderStr = e.Column.Header.ToString();
            if (HeaderStr.Equals("原材料编号"))
            {
                RawMaterialsDetailModel m = vmc.GetRawMaterialsInfo(newValue);
                if (m.RawMaterialsID != new Guid())
                {
                    data[data.IndexOf(model)].RawMaterialsID = m.RawMaterialsID;
                    data[data.IndexOf(model)].Code = newValue;
                    data[data.IndexOf(model)].Name = m.Name;
                }
                else
                {
                    data[data.IndexOf(model)].RawMaterialsID = new Guid();
                    data[data.IndexOf(model)].Code = newValue;
                    data[data.IndexOf(model)].Name = "";
                    DataGrid_RawMaterials.CurrentCell = new DataGridCellInfo(DataGrid_RawMaterials.SelectedCells[0].Item, DataGrid_RawMaterials.Columns[1]);
                }
            }
        }

        private void DataGrid_RawMaterials_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string Header = DataGrid_RawMaterials.SelectedCells[0].Column.Header.ToString();
                if (Header == "原材料编号")
                {
                    e.Handled = true;
                    DataGrid_RawMaterials.CurrentCell = new DataGridCellInfo(DataGrid_RawMaterials.SelectedCells[0].Item, DataGrid_RawMaterials.Columns[3]);
                }
                else if (Header == "数量")
                {
                    e.Handled = true;
                    DataGrid_RawMaterials.CurrentCell = new DataGridCellInfo(DataGrid_RawMaterials.SelectedCells[0].Item, DataGrid_RawMaterials.Columns[4]);
                }
                else if (Header == "日期")
                {
                    e.Handled = true;
                    DataGrid_RawMaterials.CurrentCell = new DataGridCellInfo(DataGrid_RawMaterials.SelectedCells[0].Item, DataGrid_RawMaterials.Columns[5]);
                }
                else if (Header == "操作员")
                {
                    e.Handled = true;
                    DataGrid_RawMaterials.CurrentCell = new DataGridCellInfo(DataGrid_RawMaterials.SelectedCells[0].Item, DataGrid_RawMaterials.Columns[6]);
                }
                else
                {
                    DataGrid_RawMaterials.CurrentCell = new DataGridCellInfo(DataGrid_RawMaterials.SelectedCells[0].Item, DataGrid_RawMaterials.Columns[1]);
                }
                DataGrid_RawMaterials.SelectedCells.Clear();
                DataGrid_RawMaterials.SelectedCells.Add(DataGrid_RawMaterials.CurrentCell);
            }
        }
    }
}
