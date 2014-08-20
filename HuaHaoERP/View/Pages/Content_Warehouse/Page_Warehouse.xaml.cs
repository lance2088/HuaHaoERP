using HuaHaoERP.Helper.Events;
using HuaHaoERP.Helper.Events.UpdateEvent;
using HuaHaoERP.Model;
using HuaHaoERP.Model.Warehouse;
using HuaHaoERP.ViewModel.Warehouse;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    public partial class Page_Warehouse : Page
    {
        private WarehouseProductModel ProductModel = new WarehouseProductModel();
        private ScrapConsole sc = new ScrapConsole();
        private RawMaterialsConsole rmc = new RawMaterialsConsole();
        private ScrapModel m = new ScrapModel();
        int PackStock = 0;//包装后库存

        public Page_Warehouse()
        {
            InitializeComponent();
            this.Grid_OutGrid.Visibility = System.Windows.Visibility.Hidden;
            SubscribeToEvent();
            InitPage();
            FunctionalLimitation();
        }

        /// <summary>
        /// 功能限制
        /// </summary>
        private void FunctionalLimitation()
        {
            if (Helper.DataDefinition.CommonParameters.PeriodOfValidity < 0)
            {
                this.Button_SparepartsIn.IsEnabled = false;
                this.Button_Print.IsEnabled = false;
            }
        }

        private void SubscribeToEvent()
        {
            WarehouseRawMaterialsEvent.EUpdateDataGrid += (sender, e) => { InitializeRawMaterialsDataGrid(); };
            WarehouseProductEvent.EUpdateDataGrid += (s, e) => { InitializeProductDataGrid(); };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Button_Today_Click(null, null);
        }

        private void InitPage()
        {
            #region 余料管理
            ComboBox_DropDownOpened(this, null);
            RefreshData_Scrap();
            ComboBox_Name.ItemsSource = sc.GetName(false);
            ComboBox_Name.SelectedIndex = 0;
            DatePicker_Date.Text = DateTime.Now.ToShortDateString();
            #endregion
            InitializeRawMaterialsDataGrid();
        }

        #region 产品仓库管理
        private void Button_BatchHistory_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Warehouse_Product_BatchHistory(3));
        }

        private void TextBox_Search_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Focus();
        }

        private void TextBox_Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            InitializeProductDataGrid();
        }

        /// <summary>
        /// 散件录入按钮Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_SparepartsIn_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Warehouse_Product_BatchIn(1));
        }

        private void Button_PackingOut_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Warehouse_Product_BatchIn(2));
        }

        private void Button_SparepartsOut_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Warehouse_Product_BatchIn(3));
        }

        private void Button_Print_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_Num.SelectedCells.Clear();
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                var size = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                Label_PrintTitle.Visibility = Visibility.Visible;
                Grid_Print.Measure(size);
                printDialog.PrintVisual(Grid_Print, "金字塔出货单");
                Label_PrintTitle.Visibility = Visibility.Collapsed;
            }
        }

        private void InitializeProductDataGrid()
        {
            DateTime Start = ((DateTime)this.DatePicker_Start.SelectedDate).Date;
            DateTime End = ((DateTime)this.DatePicker_End.SelectedDate).Date.AddDays(1);
            string HistoryType = this.ComboBox_ShowHistory.Text.Substring(0, 2);
            string Search = this.TextBox_Search.Text;
            //明细
            List<WarehouseProductModel> d = new List<WarehouseProductModel>();
            if (new ViewModel.Warehouse.WarehouseProductConsole().ReadDetailsList(Start, End, HistoryType, out d, Search))
            {
                this.DataGrid_ProductDetails.ItemsSource = d;
            }
            //散件
            List<WarehouseProductNumModel> dn = new List<WarehouseProductNumModel>();
            if (new ViewModel.Warehouse.WarehouseProductConsole().ReadNumList(out dn, Search))
            {
                this.DataGrid_Num.ItemsSource = dn;
            }
            int TotalNum = 0;
            foreach (WarehouseProductNumModel m in dn)
            {
                TotalNum += m.Quantity;
            }
            this.TextBox_TotalNum.Text = TotalNum.ToString();
        }

        private void ComboBox_ShowHistory_DropDownClosed(object sender, EventArgs e)
        {
            InitializeProductDataGrid();
        }

        private void Button_Today_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_Start.SelectedDate = DateTime.Now;
            this.DatePicker_End.SelectedDate = DateTime.Now;
            InitializeProductDataGrid();
        }
        private void Button_AllDate_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_Start.SelectedDate = Convert.ToDateTime("2010-01-01 00:00:00");
            this.DatePicker_End.SelectedDate = Convert.ToDateTime("2024-01-01 00:00:00");
            InitializeProductDataGrid();
        }
        #endregion

        #region 原材料管理

        private void Button_CloseOutGrid_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_OutGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        string PCode = string.Empty;
        private void DataGrid_RawMaterialsQuantity_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.MouseRightButtonDown += (s, a) =>
            {
                a.Handled = true;
                (sender as DataGrid).SelectedIndex = (s as DataGridRow).GetIndex();
                (s as DataGridRow).Focus();
                RawMaterialsDetailModel d = this.DataGrid_RawMaterialsQuantity.SelectedCells[0].Item as RawMaterialsDetailModel;
                //show Packing Grid
                this.Label_ShowWarnMessage.Content = "";
                this.Grid_OutGrid.Visibility = System.Windows.Visibility.Visible;
                int count = 0;
                int.TryParse(d.Amount, out count);
                PackStock = count;
                PCode = d.Code;
                this.TextBox_Quantity_OutGrid.Focus();
                this.Label_RawMaterialsName.Content = d.Name;
            };
        }
        private void Button_OutGrid_Click(object sender, RoutedEventArgs e)
        {

            List<RawMaterialsDetailModel> list = new List<RawMaterialsDetailModel>();
            RawMaterialsDetailModel m = new RawMaterialsDetailModel();
            m.RawMaterialsID = rmc.GetGuid(PCode);
            m.Type = RadioButton_生产.IsChecked == true ? "生产" : "出库";
            m.Date = DateTime.Now.ToString();
            int Number = 0;
            int.TryParse(this.TextBox_Quantity_OutGrid.Text, out Number);
            m.Number = Number;
            m.Operator = Helper.DataDefinition.CommonParameters.RealName;
            list.Add(m);
            if (rmc.AddByBatch(list, false))
            {
                InitializeRawMaterialsDataGrid();
                this.Grid_OutGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void TextBox_Quantity_OutGrid_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                this.Label_ShowWarnMessage.Content = "";
                int Quantity = 0;
                int.TryParse(this.TextBox_Quantity_OutGrid.Text, out Quantity);
                if (Quantity > PackStock)
                {
                    this.Label_ShowWarnMessage.Content = "超出库存";
                }
            }
        }
        private void InitializeRawMaterialsDataGrid()
        {
            string Type = this.ComboBox_RawMaterialsRecord.Text;
            List<RawMaterialsDetailModel> rmm = new List<RawMaterialsDetailModel>();
            rmc.ReadList(out rmm);
            DataGrid_RawMaterialsQuantity.ItemsSource = rmm;
            rmc.ReadRecordList(Type, out rmm);
            DataGrid_RawMaterialsRecord.ItemsSource = rmm;
        }

        //private void Button_RawMaterials_Out_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DataGrid_RawMaterialsRecord.SelectedCells.Count != 0)
        //    {
        //        List<RawMaterialsDetailModel> list = new List<RawMaterialsDetailModel>();
        //        for (int i = 0; i < DataGrid_RawMaterialsRecord.SelectedItems.Count; i++) 
        //        {
        //            RawMaterialsDetailModel m = DataGrid_RawMaterialsRecord.SelectedItems[i] as RawMaterialsDetailModel;
        //            list.Add(m);
        //        }
        //        bool flag  = rmc.AddByBatch(list,false);
        //        if (flag)
        //        {
        //            InitializeRawMaterialsDataGrid();
        //        }
        //    }
        //}

        private void Button_RawMaterials_In_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Warehouse_RawMaterials());
        }

        private void ComboBox_RawMaterialsRecord_DropDownClosed(object sender, EventArgs e)
        {
            InitializeRawMaterialsDataGrid();
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
