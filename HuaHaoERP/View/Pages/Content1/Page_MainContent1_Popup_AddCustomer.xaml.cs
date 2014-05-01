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

namespace HuaHaoERP.View.Pages.Content1
{
    public partial class Page_MainContent1_Popup_AddCustomer : Page
    {
        Model.CustomerModel d = new Model.CustomerModel();
        private Guid Guid;

        public Page_MainContent1_Popup_AddCustomer()
        {
            InitializeComponent();
            InitializeData();
        }
        private void InitializeData()
        {
            Guid = Guid.NewGuid();
        }

        private bool CheckAndGetData()
        {
            bool flag = true;
            d.Guid = Guid;
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
                CustomerEventArgs MyE = new CustomerEventArgs();
                MyE.CustomerData = d;
                CustomerEvent.OnAdd(this, MyE);
                StatusBarMessageEventArgs MessE = new StatusBarMessageEventArgs();
                MessE.Message = "添加用户：" + d.Name;
                StatusBarMessageEvent.OnUpdateMessage(this, MessE);
                Button_Cancel_Click(null, null);
            }
            else
            {
                Console.WriteLine("Add False");
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp(this, new Helper.Events.PopUpEventArgs());
        }
    }
}
