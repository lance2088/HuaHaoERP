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

namespace HuaHaoERP.View.Pages.Content_Orders
{
    public partial class Page_Orders_Product : Page
    {
        public Page_Orders_Product()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
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

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {

            Button_Cancel_Click(null, null);
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }
    }
}
