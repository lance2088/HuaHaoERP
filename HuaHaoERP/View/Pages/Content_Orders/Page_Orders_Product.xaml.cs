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
using HuaHaoERP.Helper.Events;

namespace HuaHaoERP.View.Pages.Content_Orders
{
    public partial class Page_Orders_Product : Page
    {
        Model.ProductOrderModel d = new Model.ProductOrderModel();

        public Page_Orders_Product()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
            d.Details = new List<Model.ProductOrderDetailsModel>();
            this.ComboBox_Customer.ItemsSource = Helper.DataDefinition.CustomerLibrary.CustomerList.DefaultView;
            this.ComboBox_Customer.DisplayMemberPath = "Name";
            this.ComboBox_Customer.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Customer.SelectedIndex = 0;
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.CustomerLibrary.ProductList.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
            this.DatePicker_OrderDate.SelectedDate = DateTime.Now;
        }
        private bool CheckAndGetData()
        {
            bool flag = true;

            return flag;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndGetData())
            {

                ProductOrderEvent.OnAdd(this, d);
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
            Model.ProductOrderDetailsModel dd = new Model.ProductOrderDetailsModel();
            dd.Guid = Guid.NewGuid();
            dd.ProductID = (Guid)this.ComboBox_Product.SelectedValue;
            dd.ProductName = this.ComboBox_Product.Text.Trim();
            int NumberOfItems = 0;
            int.TryParse(this.TextBox_NumberOfItems.Text.Trim(), out NumberOfItems);
            dd.NumberOfItems = NumberOfItems;
            int Quantity = 0;
            int.TryParse(this.TextBox_Quantity.Text.Trim(), out Quantity);
            dd.Quantity = Quantity;
            dd.Unit = this.ComboBox_Unit.Text.Trim();
            dd.Remark = this.TextBox_Remark.Text.Trim();
            d.Details.Add(dd);
            this.DataGrid_ProductDetails.ItemsSource = null;
            this.DataGrid_ProductDetails.ItemsSource = d.Details;
        }
    }
}
