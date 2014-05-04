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

namespace HuaHaoERP.View.Pages.Content_CustomerLibrary
{
    public partial class Page_CustomerLibrary_Popup_AddCustomer : Page
    {
        Model.CustomerModel d = new Model.CustomerModel();
        private Guid Guid;

        public Page_CustomerLibrary_Popup_AddCustomer()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
            Guid = Guid.NewGuid();
            d.Guid = Guid;
        }

        private bool CheckAndGetData()
        {
            bool flag = true;
            d.Number = this.TextBox_Customer_Number.Text;
            d.Name = this.TextBox_Customer_Name.Text;
            d.Company = this.TextBox_Customer_Company.Text;
            d.Address = this.TextBox_Customer_Address.Text;
            d.Phone = this.TextBox_Customer_Phone.Text;
            d.MobilePhone = this.TextBox_Customer_MobilePhone.Text;
            d.Fax = this.TextBox_Customer_Fax.Text;
            d.Business = this.TextBox_Customer_Business.Text;
            d.Remark = this.TextBox_Customer_Remark.Text;
            int CustomerLevel;
            flag = int.TryParse(this.TextBox_Customer_CustomerLevel.Text, out CustomerLevel);
            d.CustomerLevel = CustomerLevel;
            int OrderQuantity;
            flag = int.TryParse(this.TextBox_Customer_OrderQuantity.Text, out OrderQuantity);
            d.OrderQuantity = OrderQuantity;
            return flag;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if(CheckAndGetData())
            {
                CustomerEvent.OnAdd(this, d);
                StatusBarMessageEvent.OnUpdateMessage(this, "添加用户：" + d.Name);
                CustomerEvent.OnUpdateDataGrid(this, new EventArgs());
                Button_Cancel_Click(null, null);
            }
            else
            {
                Console.WriteLine("Add False");
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp(this);
        }
    }
}
