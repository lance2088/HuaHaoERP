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
        private Model.CustomerModel d = new Model.CustomerModel();
        private Guid Guid;
        private Guid OldGuid;
        private bool isNew = true;

        public Page_CustomerLibrary_Popup_AddCustomer()
        {
            InitializeComponent();
            InitializeData();
        }
        public Page_CustomerLibrary_Popup_AddCustomer(object data)
        {
            InitializeComponent();
            isNew = false;
            InitializeData((Model.CustomerModel)data);
        }
        private void InitializeData()
        {
            
        }
        private void InitializeData(Model.CustomerModel d)
        {
            OldGuid = d.Guid;
            this.TextBox_Customer_Number.Text = d.Number;
            this.TextBox_Customer_Name.Text = d.Name;
            this.TextBox_Customer_Address.Text = d.Address;
            this.TextBox_Customer_Area.Text = d.Area;
            this.TextBox_Customer_Phone.Text = d.Phone;
            this.TextBox_Customer_MobilePhone.Text = d.MobilePhone;
            this.TextBox_Customer_Fax.Text = d.Fax;
            this.TextBox_Customer_Business.Text = d.Business;
            this.TextBox_Customer_Clerk.Text = d.Clerk;
            this.TextBox_Customer_DebtCeiling.Text = d.DebtCeiling.ToString();
            this.TextBox_Customer_Remark.Text = d.Remark;
        }

        private bool CheckAndGetData()
        {
            bool flag = true;
            if (this.TextBox_Customer_Number.Text.Trim() == "" || this.TextBox_Customer_Name.Text.Trim() == "")
            {
                return false;
            }
            Guid = Guid.NewGuid();
            d.Guid = Guid;
            d.Number = this.TextBox_Customer_Number.Text.Trim();
            d.Name = this.TextBox_Customer_Name.Text.Trim();
            d.Address = this.TextBox_Customer_Address.Text.Trim();
            d.Area = this.TextBox_Customer_Area.Text.Trim();
            d.Phone = this.TextBox_Customer_Phone.Text.Trim();
            d.MobilePhone = this.TextBox_Customer_MobilePhone.Text.Trim();
            d.Fax = this.TextBox_Customer_Fax.Text.Trim();
            d.Business = this.TextBox_Customer_Business.Text.Trim();
            d.Clerk = this.TextBox_Customer_Clerk.Text.Trim();
            decimal DebtCeiling = 0m;
            flag = decimal.TryParse(this.TextBox_Customer_DebtCeiling.Text, out DebtCeiling);
            d.DebtCeiling = DebtCeiling;
            d.Remark = this.TextBox_Customer_Remark.Text.Trim();
            return flag;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if(CheckAndGetData())
            {
                CustomerEvent.OnAdd(this, d);
                if (!isNew)
                {
                    Model.CustomerModel dOld = new Model.CustomerModel();
                    dOld.Guid = OldGuid;
                    CustomerEvent.OnDelete(this, dOld);
                    StatusBarMessageEvent.OnUpdateMessage(this, "修改用户：" + d.Name);
                }
                else
                {
                    StatusBarMessageEvent.OnUpdateMessage(this, "添加用户：" + d.Name);
                }
                CustomerEvent.OnUpdateDataGrid(this, new EventArgs());
                Button_Cancel_Click(null, null);
            }
            else
            {
                Console.WriteLine("Add False");
                StatusBarMessageEvent.OnUpdateMessage(this, "添加/修改用户操作失败");
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp(this);
        }
    }
}
