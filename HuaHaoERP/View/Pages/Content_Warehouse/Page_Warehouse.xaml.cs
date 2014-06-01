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
using HuaHaoERP.Model.Warehouse;
using HuaHaoERP.Helper.Events;
using HuaHaoERP.Helper.Events.UpdateEvent;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse : Page
    {
        private WarehouseProductModel ProductModel = new WarehouseProductModel();
        private ScrapConsole sc = new ScrapConsole();
        private RawMaterialsConsole rmc = new RawMaterialsConsole();
        private ScrapModel m = new ScrapModel();
        /// <summary>
        /// 库存
        /// </summary>
        int Stock = 0;

        public Page_Warehouse()
        {
            InitializeComponent();
            this.Grid_Outbound.Visibility = System.Windows.Visibility.Hidden;
            this.Grid_Packing.Visibility = System.Windows.Visibility.Hidden;
            SubscribeToEvent();
            InitializeRawMaterialsDataGrid();
            InitPage();
        }
        private void SubscribeToEvent()
        {
            WarehouseRawMaterialsEvent.EUpdateDataGrid += (sender, e) => { InitializeRawMaterialsDataGrid(); };
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeProductDataGrid();
        }
        private void InitPage()
        {
            this.ComboBox_ProductList_Outbound.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithoutAll.DefaultView;
            this.ComboBox_ProductList_Outbound.DisplayMemberPath = "Name";
            this.ComboBox_ProductList_Outbound.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_ProductList_Outbound.SelectedIndex = 0;
            this.ComboBox_ProductList.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithoutAll.DefaultView;
            this.ComboBox_ProductList.DisplayMemberPath = "Name";
            this.ComboBox_ProductList.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_ProductList.SelectedIndex = 0;
            //ComboBox_ProductList_DropDownClosed(null, null);
            InitializeProductDataGrid();
            #region 余料管理
            ComboBox_DropDownOpened(this, null);
            RefreshData_Scrap();
            ComboBox_Name.ItemsSource = sc.GetName(false);
            ComboBox_Name.SelectedIndex = 0;
            DatePicker_Date.Text = DateTime.Now.ToShortDateString();
            #endregion 
        }

        #region 产品仓库
        private void Button_ClosePacking_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Packing.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void InitializeProductDataGrid()
        {
            List<WarehouseProductModel> d = new List<WarehouseProductModel>();
            if(new ViewModel.Warehouse.WarehouseProductConsole().ReadDetailsList(out d))
            {
                this.DataGrid_ProductDetails.ItemsSource = d;
            }
            List<WarehouseProductNumModel> dn = new List<WarehouseProductNumModel>();
            if(new ViewModel.Warehouse.WarehouseProductConsole().ReadNumList(out dn))
            {
                this.DataGrid_Num.ItemsSource = dn;
            }
            List<WarehouseProductNumModel> dnPack = new List<WarehouseProductNumModel>();
            if(new ViewModel.Warehouse.WarehouseProductConsole().ReadPackingNumList(out dnPack))
            {
                this.DataGrid_PackingNum.ItemsSource = dnPack;
            }
        }

        private void Button_Packed_Click(object sender, RoutedEventArgs e)
        {
            Guid ProductID = (Guid)this.ComboBox_ProductList.SelectedValue;
            int PackedQuantity = 0;
            int.TryParse(this.TextBox_PackQuantity.Text, out PackedQuantity);
            int Quantity = 0;
            int.TryParse(this.TextBox_Quantity.Text, out Quantity);
            if (PackedQuantity == 0 || Quantity == 0)
            {
                this.Label_ShowPackWarnMessage.Content = "请输入包装件数";
                this.TextBox_PackQuantity.Focus();
                return;
            }
            if (new ViewModel.Warehouse.WarehouseProductConsole().Packing(ProductID, Quantity, PackedQuantity))
            {
                InitializeProductDataGrid();
                this.TextBox_PackQuantity.Clear();
                this.TextBox_Quantity.Clear();
            }
        }

        private void TextBox_PackQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Label_ShowPackWarnMessage.Content = "";
            CountQuantity();
        }
        int PerPackQuantity = 1;
        private void ComboBox_ProductList_DropDownClosed(object sender, EventArgs e)
        {
            CountQuantity();
        }
        private void CountQuantity()
        {
            PerPackQuantity = new ViewModel.Warehouse.WarehouseProductConsole().ReadProductPackingNum((Guid)this.ComboBox_ProductList.SelectedValue);
            this.Label_ShowPackMessage.Content = PerPackQuantity + "个/包";
            int PackQuantity = 0;
            int.TryParse(this.TextBox_PackQuantity.Text, out PackQuantity);
            this.TextBox_Quantity.Text = (PackQuantity * PerPackQuantity).ToString();
            if (Stock < PackQuantity * PerPackQuantity)
            {
                this.Label_ShowPackWarnMessage.Content = "库存不够包装";
            }
        }

        /// <summary>
        /// DataGrid右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_Num_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.MouseRightButtonDown += (s, a) =>
            {
                a.Handled = true;
                (sender as DataGrid).SelectedIndex = (s as DataGridRow).GetIndex();
                (s as DataGridRow).Focus();
                WarehouseProductNumModel d = this.DataGrid_Num.SelectedCells[0].Item as WarehouseProductNumModel;
                //show Packing Grid
                this.Label_ShowPackWarnMessage.Content = "";
                this.Grid_Packing.Visibility = System.Windows.Visibility.Visible;
                this.Grid_Outbound.Visibility = System.Windows.Visibility.Hidden;
                this.ComboBox_ProductList.Text = d.ProductName;
                Stock = d.Quantity;
                this.TextBox_PackQuantity.Focus();
                CountQuantity();
                if (Stock < PerPackQuantity)
                {
                    this.Label_ShowPackWarnMessage.Content = "库存不够包装";
                }
            };
        }

        private void ComboBox_ShowHistory_DropDownClosed(object sender, EventArgs e)
        {

        }
        private void DataGrid_PackingNum_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.MouseRightButtonDown += (s, a) =>
            {
                a.Handled = true;
                (sender as DataGrid).SelectedIndex = (s as DataGridRow).GetIndex();
                (s as DataGridRow).Focus();
                WarehouseProductNumModel d = this.DataGrid_PackingNum.SelectedCells[0].Item as WarehouseProductNumModel;
                //show Packing Grid
                this.Grid_Outbound.Visibility = System.Windows.Visibility.Visible;
                this.Grid_Packing.Visibility = System.Windows.Visibility.Hidden;
                this.ComboBox_ProductList_Outbound.Text = d.ProductName;
            };
        }
        private void Button_Outbound_Click(object sender, RoutedEventArgs e)
        {
            Guid ProductID = (Guid)this.ComboBox_ProductList_Outbound.SelectedValue;
            int Quantity = 0;
            int.TryParse(this.TextBox_Quantity_Outbound.Text, out Quantity);
            if(new ViewModel.Warehouse.WarehouseProductConsole().Outbound(ProductID, Quantity))
            {
                InitializeProductDataGrid();
            }
        }
        private void Button_CloseOutbound_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Outbound.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion

        #region 原材料管理
        private void InitializeRawMaterialsDataGrid()
        {
            List<RawMaterialsDetailModel> rmm = new List<RawMaterialsDetailModel>();
            rmc.ReadList(out rmm);
            DataGrid_RawMaterialsQuantity.ItemsSource = rmm;
            rmc.ReadRecordList(out rmm);
            DataGrid_RawMaterialsRecord.ItemsSource = rmm;
        }
        private void Button_RawMaterials_Out_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_RawMaterialsRecord.SelectedCells.Count != 0)
            {
                List<RawMaterialsDetailModel> list = new List<RawMaterialsDetailModel>();
                for (int i = 0; i < DataGrid_RawMaterialsRecord.SelectedItems.Count; i++) 
                {
                    RawMaterialsDetailModel m = DataGrid_RawMaterialsRecord.SelectedItems[i] as RawMaterialsDetailModel;
                    list.Add(m);
                }
                bool flag  = rmc.AddByBatch(list,false);
                if (flag)
                {
                    InitializeRawMaterialsDataGrid();
                }
            }
        }

        private void Button_RawMaterials_In_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Warehouse_RawMaterials());
        }
        #endregion

        #region 余料管理

        private void RefreshData_Scrap()
        {
            List<ScrapModel> data = new List<ScrapModel>();
            sc.ReadList(ComboBox_Scrap_Name.Text, out data);
            DataGrid_Scrap.ItemsSource = data;
            Label_Scrap_Amount.Content = "数量：" + sc.GetAmountByName(ComboBox_Scrap_Name.Text);
        }
        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox_Scrap_Name.ItemsSource = sc.GetName(true);
            ComboBox_Scrap_Name.SelectedIndex = 0;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            RefreshData_Scrap();
        }

        private void ComboBox_Name_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox_Name.ItemsSource = sc.GetName(false);
            ComboBox_Name.SelectedIndex = 0;
        }
        private bool CheckAndGetData()
        {
            DateTime dt = new DateTime();
            DateTime.TryParse(DatePicker_Date.Text, out dt);
            TimeSpan ts = DateTime.Now - dt;
            int day = ts.Days;
            if (day > 1000) 
            {
                StatusBarMessageEvent.OnUpdateMessage("日期不能为空！");
                DatePicker_Date.Text = DateTime.Now.ToShortDateString();
                return false;
            }
            if (string.IsNullOrEmpty(TextBox_Operator.Text))
            {
                StatusBarMessageEvent.OnUpdateMessage("操作人不能空！");
                return false;
            }
            if (ComboBox_Name.SelectedIndex == 0)
            {
                StatusBarMessageEvent.OnUpdateMessage("请选择余料类型！");
                ComboBox_Name.Focus();
                return false;
            }
            if (RadioButton_添加.IsChecked == false)
            {
                decimal dTmp = 0;
                decimal.TryParse(TextBox_Number.Text, out dTmp);
                if (dTmp > sc.GetAmountByName(ComboBox_Name.Text))
                {
                    StatusBarMessageEvent.OnUpdateMessage("卖出数量/重量大于库存值！");
                    return false;
                }
            }
            m.Guid = Guid.NewGuid();
            m.Name = ComboBox_Name.Text;
            m.Number = RadioButton_添加.IsChecked == true ? TextBox_Number.Text : "-" + TextBox_Number.Text;
            m.Remark = TextBox_Remark.Text;
            m.Date = DateTime.Parse(DatePicker_Date.Text).ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("T");
            m.Operator = TextBox_Operator.Text;
            return true;
        }
        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            string typeMsg = RadioButton_添加.IsChecked == true ? "添加" : "卖出";
            if (CheckAndGetData())
            {
                sc.Add(m);
                RefreshData_Scrap();
                StatusBarMessageEvent.OnUpdateMessage(typeMsg + "余料名称：" + m.Name);
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab)
            {
                if (txt.Text.Contains(".") && e.Key == Key.Decimal)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                if (txt.Text.Contains(".") && e.Key == Key.OemPeriod)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion




    }
}
