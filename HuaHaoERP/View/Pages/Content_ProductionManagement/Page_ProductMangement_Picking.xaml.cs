using HuaHaoERP.Helper.Events;
using HuaHaoERP.Helper.Events.UpdateEvent.ProducttionManagement;
using HuaHaoERP.Helper.Events.UpdateEvent.Warehouse;
using HuaHaoERP.Model.ProductionManagement;
using HuaHaoERP.ViewModel.ProductionManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    /// <summary>
    /// Interaction logic for Page_ProductMangement_Picking.xaml
    /// </summary>
    public partial class Page_ProductMangement_Picking : Page
    {
        private string CountInOrder;

        private DateTime ProcessorsFirst;
        private DateTime ProcessorsEnd;
        private Guid ProductID;
        private Guid ProcessorsID;

        public Page_ProductMangement_Picking()
        {
            InitializeComponent();
            SubscribeToEvent();
            InitializeData();
            FunctionalLimitation();
        }


        private void InitializeData()
        {
            this.DatePicker_ProcessorsFirst.SelectedDate = DateTime.Now.Date;
            this.DatePicker_ProcessorsEnd.SelectedDate = DateTime.Now.Date;
            InitProductComboBox();
            InitProcessorsComboBox();
            InitializeDataGrid();
        }

        private void InitProductComboBox()
        {
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithAll.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
        }
        private void InitProcessorsComboBox()
        {
            this.ComboBox_Processors.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsListWithAll.DefaultView;
            this.ComboBox_Processors.DisplayMemberPath = "Name";
            this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Processors.SelectedIndex = 0;
        }

        private void SubscribeToEvent()
        {
            DeliveryProcessInEvent.EUpdateDataGrid += (s, e) =>
            {
                InitializeDataGrid();
            };
        }

        private void InitializeDataGrid()
        {
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                return;
            }
            this.ProcessorsID = (Guid)this.ComboBox_Processors.SelectedValue;
            if (this.ComboBox_Product.SelectedValue == null)
            {
                return;
            }
            this.ProductID = (Guid)this.ComboBox_Product.SelectedValue;
            this.ProcessorsFirst = (DateTime)this.DatePicker_ProcessorsFirst.SelectedDate;
            this.ProcessorsEnd = ((DateTime)this.DatePicker_ProcessorsEnd.SelectedDate).AddDays(1);

            List<ProductManagement_PickingModel> data;
            new ViewModel.ProductionManagement.DeliveryProcessInConsole().ReadList(ProcessorsFirst, ProcessorsEnd, ProductID, ProcessorsID, out data, out CountInOrder);
            //这里写查询30条数据
            if (data.Count == 0)
            {
                if (this.IsLoaded)
                {
                    if (MessageBox.Show("当前查询返回0条数据，是否为您显示最近30条数据。", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                DateTime dt = new DateTime();
                new DeliveryProcessInConsole().ReadList(dt, dt, ProductID, ProcessorsID, out data, out CountInOrder);
            }
            this.DataGrid_ProcessIn.ItemsSource = data;
            this.Label_CountInOrder.Content = this.CountInOrder;
        }

        /// <summary>
        /// 功能限制
        /// </summary>
        private void FunctionalLimitation()
        {
            if (Helper.DataDefinition.CommonParameters.PeriodOfValidity < 0)
            {
                this.Button_AddProcessIn.IsEnabled = false;
            }
        }


        private void ComboBox_Processors_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).IsDropDownOpen == false)
            {
                (sender as ComboBox).IsDropDownOpen = true;
            }
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                string Parm = this.ComboBox_Processors.Text;
                DataSet ds = new DataSet();
                if (new ViewModel.Customer.ProcessorsConsole().GetNameList(Parm, out ds))
                {
                    this.ComboBox_Processors.ItemsSource = ds.Tables[0].DefaultView;
                    this.ComboBox_Processors.DisplayMemberPath = "Name";
                    this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
                }
            }
        }

        private void ComboBox_Product_DropDownOpened(object sender, EventArgs e)
        {
            if (this.ComboBox_Product.SelectedValue == null)
            {
                InitProductComboBox();
            }
        }

        private void ComboBox_Processors_DropDownOpened(object sender, EventArgs e)
        {
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                InitProcessorsComboBox();
            }
        }


        private void ComboBox_Product_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ComboBox_Product.IsDropDownOpen = true;
        }

        private void ComboBox_Processors_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ComboBox_Processors.IsDropDownOpen = true;
        }


        private void ComboBox_Product_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).IsDropDownOpen == false)
            {
                (sender as ComboBox).IsDropDownOpen = true;
            }
            if (this.ComboBox_Product.SelectedValue == null)
            {
                string Parm = this.ComboBox_Product.Text;
                DataSet ds = new DataSet();
                if (new ViewModel.MeansOfProduction.ProductConsole().GetNameList(Parm, out ds))
                {
                    this.ComboBox_Product.ItemsSource = ds.Tables[0].DefaultView;
                    this.ComboBox_Product.DisplayMemberPath = "Name";
                    this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
                }
            }
        }


        private void MenuItem_dgmenu2_Del_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_ProcessIn.SelectedCells.Count > 0)
            {
                Model.ProductionManagement_OutsideProcessModel d = this.DataGrid_ProcessIn.SelectedCells[0].Item as Model.ProductionManagement_OutsideProcessModel;
                if (MessageBox.Show("确认删除入单：" + d.Id + "？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.ProductionManagement.OutsideProcessConsole().Delete(d.Guid);
                    InitializeDataGrid();
                }
            }
        }

        private void Button_AddProcessIn_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnShowPopUp(new Page_ProductMangement_PickingAdd());
        }



        private void ComboBox_Processors_DropDownClosed(object sender, EventArgs e)
        {
            InitializeDataGrid();
        }

        private void ComboBox_Product_DropDownClosed(object sender, EventArgs e)
        {
            InitializeDataGrid();
        }

        private void DatePicker_ProcessorsEnd_CalendarClosed(object sender, RoutedEventArgs e)
        {
            InitializeDataGrid();
        }

        private void DatePicker_ProcessorsFirst_CalendarClosed(object sender, RoutedEventArgs e)
        {
            InitializeDataGrid();
        }

        private void Button_Today_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_ProcessorsFirst.SelectedDate = DateTime.Now.Date;
            this.DatePicker_ProcessorsEnd.SelectedDate = DateTime.Now.Date;
            InitializeDataGrid();
        }

        private void Button_AllDate_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_ProcessorsFirst.SelectedDate = Convert.ToDateTime("2010-01-01 00:00:00");
            this.DatePicker_ProcessorsEnd.SelectedDate = Convert.ToDateTime("2024-01-01 00:00:00");
            InitializeDataGrid();
        }

        /// <summary>
        /// 外加工批量记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_BatchHistory1_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Content_Warehouse.Page_Warehouse_Product_BatchHistory(2));
        }

        private void Button_删除_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_ProcessIn.SelectedCells.Count > 0)
            {
                if (MessageBox.Show("确认删除订单？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
                ProductManagement_PickingModel m = this.DataGrid_ProcessIn.SelectedCells[0].Item as ProductManagement_PickingModel;
                if (new ViewModel.ProductionManagement.DeliveryProcessInConsole().DeleteOrder(m.Guid))
                {
                    InitializeDataGrid();
                    HalfProductEvent.OnUpdateDataGrid();
                    SparePartsInventoryEvent.OnUpdateDataGrid();
                    DeliveryProcessInEvent.OnUpdateDataGrid();
                }
            }
        }

    }
}
