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
using HuaHaoERP.ViewModel.Warehouse;
using HuaHaoERP.Model;
using System.Data;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse_RawMaterials : Page
    {
        private ViewModel.Warehouse.RawMaterialsConsole vmc = new RawMaterialsConsole();
        private List<RawMaterialsDetailModel> rawMaterials = new List<RawMaterialsDetailModel>();
        public Page_Warehouse_RawMaterials()
        {
            InitializeComponent();
            DataGrid_RawMaterials.LoadingRow += new EventHandler<DataGridRowEventArgs>(DataGrid_RawMaterials_LoadingRow);
            rawMaterials = new List<RawMaterialsDetailModel>()
            {
                new RawMaterialsDetailModel{
                    Id = 1
                }
            };
            DataGrid_RawMaterials.ItemsSource = rawMaterials;
        }

        private void DataGrid_RawMaterials_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        private bool Validate()
        {
            List<RawMaterialsDetailModel> list = new List<RawMaterialsDetailModel>();
            list = DataGrid_RawMaterials.ItemsSource as List<RawMaterialsDetailModel>;
            foreach (RawMaterialsDetailModel m in list)
            {
                if (m.Id != 0) 
                {
                    if (string.IsNullOrEmpty(m.RawMaterialsID))
                    {
                        MessageBox.Show("第" + m.Id + "行原材料编号为空！");
                        return false;
                    }
                    else if (!vmc.IsRawMaterialsIDExist(m.RawMaterialsID))
                    {
                        MessageBox.Show("第" + m.Id + "行原材料编号不存在系统中，请检查！");
                        return false;
                    }
                    
                    if (string.IsNullOrEmpty(m.Number))
                    {
                        MessageBox.Show("第" + m.Id + "行数量为空！");
                        return false;
                    }
                }
            }
            return true;
        }
        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                List<RawMaterialsDetailModel> list = new List<RawMaterialsDetailModel>();
                list = DataGrid_RawMaterials.ItemsSource as List<RawMaterialsDetailModel>;
                if (list.Count != 0)
                {
                    new ViewModel.Warehouse.RawMaterialsConsole().AddByBatch(list,true);
                }
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }
        string preValue = "";
        private void DataGrid_RawMaterials_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            preValue = (e.Column.GetCellContent(e.Row) as TextBlock).Text;
        }

        private void DataGrid_RawMaterials_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            string newValue = (e.EditingElement as TextBox).Text;
            string headerValue = e.Column.Header.ToString();
            if (!preValue.Equals(newValue))
            {
                if (headerValue.Equals("原材料编号"))
                {
                    (DataGrid_RawMaterials.Columns[2].GetCellContent(DataGrid_RawMaterials.Items[e.Row.GetIndex()]) as TextBlock).Text = "222";
                    (DataGrid_RawMaterials.Columns[4].GetCellContent(DataGrid_RawMaterials.Items[e.Row.GetIndex()]) as TextBlock).Text = DateTime.Now.ToString("yyyy.MM.dd");
                    (DataGrid_RawMaterials.Columns[5].GetCellContent(DataGrid_RawMaterials.Items[e.Row.GetIndex()]) as TextBlock).Text = Helper.DataDefinition.CommonParameters.LoginUserName;
                }
            }
            //if (headerValue.Equals("备注"))
            //{
            //    (DataGrid_RawMaterials.Columns[0].GetCellContent(DataGrid_RawMaterials.Items[e.Row.GetIndex() + 1]) as TextBlock).Text = "" + (e.Row.GetIndex() + 2);
            //}    
        }
    }
}
