using HuaHaoERP.Helper.Events;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace HuaHaoERP.View.Pages.Content_Orders
{
    public partial class Page_Orders_Product : Page
    {
        Model.ProductOrderModel d = new Model.ProductOrderModel();
        Model.ProductOrderModelForDataGrid dForDataGrid = new Model.ProductOrderModelForDataGrid();
        Guid Guid = Guid.NewGuid();
        bool isNew = true;

        public Page_Orders_Product()
        {
            InitializeComponent();
            InitializeData();
        }
        public Page_Orders_Product(Model.ProductOrderModelForDataGrid d)
        {
            InitializeComponent();
            this.dForDataGrid = d;
            this.Guid = dForDataGrid.Guid;
            isNew = false;
            FillData();
        }
        private void InitializeData()
        {
            d.Details = new List<Model.ProductOrderDetailsModel>();
            this.ComboBox_Customer.ItemsSource = Helper.DataDefinition.ComboBoxList.CustomerListWithoutAll.DefaultView;
            this.ComboBox_Customer.DisplayMemberPath = "Name";
            this.ComboBox_Customer.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Customer.SelectedIndex = 0;
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithoutAll.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
            this.DatePicker_OrderDate.SelectedDate = DateTime.Now;
            GenerateOrderNumber();
        }
        private void FillData()
        {
            this.ComboBox_Customer.ItemsSource = Helper.DataDefinition.ComboBoxList.CustomerListWithoutAll.DefaultView;
            this.ComboBox_Customer.DisplayMemberPath = "Name";
            this.ComboBox_Customer.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Customer.Text = dForDataGrid.CustomerName;
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithoutAll.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
            this.TextBox_OrderNumber.Text = dForDataGrid.OrderNumber;
            if (dForDataGrid.DeliveryDate != "")
            {
                this.DatePicker_DeliveryDate.SelectedDate = Convert.ToDateTime(dForDataGrid.DeliveryDate + " 00:00:00");
            }
            this.DatePicker_OrderDate.SelectedDate = Convert.ToDateTime(dForDataGrid.OrderDate + " 00:00:00");
            List<Model.ProductOrderDetailsModel> dDetails = new List<Model.ProductOrderDetailsModel>();
            new ViewModel.Orders.ProductOrderConsole().GetOrderDetails(dForDataGrid.Guid, out dDetails);
            d.Details = dDetails;
            this.DataGrid_ProductDetails.ItemsSource = d.Details;
        }
        private bool CheckAndGetData()
        {
            bool flag = true;
            if(d.Details.Count == 0)
            {
                return false;
            }
            d.Guid = Guid;
            d.OrderNumber = this.TextBox_OrderNumber.Text.Trim();
            d.CustomerID = (Guid)this.ComboBox_Customer.SelectedValue;
            try
            {
                d.DeliveryDate = ((DateTime)this.DatePicker_DeliveryDate.SelectedDate + DateTime.Now.TimeOfDay).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch(Exception)
            {
                Console.WriteLine("已处理Exception：DatePicker无日期");
                d.DeliveryDate = "0001-01-01 00:00:00";
            }
            d.OrderDate = ((DateTime)this.DatePicker_OrderDate.SelectedDate + DateTime.Now.TimeOfDay).ToString("yyyy-MM-dd HH:mm:ss");
            return flag;
        }
        private void GenerateOrderNumber()
        {
            if(isNew)
            {
                this.TextBox_OrderNumber.Text = ((DateTime)this.DatePicker_OrderDate.SelectedDate).ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss");
            }
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndGetData())
            {
                if(isNew)
                {
                    new ViewModel.Orders.ProductOrderConsole().Add(d);
                    StatusBarMessageEvent.OnUpdateMessage("新增订单：" + d.OrderNumber);
                }
                else
                {
                    new ViewModel.Orders.ProductOrderConsole().Update(d);
                    StatusBarMessageEvent.OnUpdateMessage("修改订单：" + d.OrderNumber);
                }
                ProductOrderEvent.OnUpdateDataGrid();
                Button_Cancel_Click(null, null);
            }
            else
            {

            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void Button_AddProductDetails_Click(object sender, RoutedEventArgs e)
        {
            if(this.ComboBox_Product.Text == "")
            {
                return;
            }
            Model.ProductOrderDetailsModel dd = new Model.ProductOrderDetailsModel();
            dd.Guid = Guid.NewGuid();
            dd.OrderID = Guid;
            dd.ProductID = (Guid)this.ComboBox_Product.SelectedValue;
            //dd.ProductNumber = this.ComboBox_Product.Text.Split('_')[0].Trim();
            dd.ProductName = this.ComboBox_Product.Text.Trim();
            dd.NumberOfItems = 0;
            dd.Quantity = 0;
            d.Details.Add(dd);
            this.DataGrid_ProductDetails.ItemsSource = null;
            this.DataGrid_ProductDetails.ItemsSource = d.Details;
        }

        private void ComboBox_Customer_DropDownClosed(object sender, EventArgs e)
        {
            GenerateOrderNumber();
        }

        private void DatePicker_OrderDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            GenerateOrderNumber();
        }
    }
}
