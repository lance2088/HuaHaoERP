using HuaHaoERP.Helper.Events;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_CustomerLibrary
{
    public partial class Page_CustomerLibrary_Popup_AddCustomer : Page
    {
        private Model.CustomerModel d = new Model.CustomerModel();
        private Guid Guid;
        private Guid OldGuid;
        private string OldAddTime = "";
        private bool isNew = true;

        public Page_CustomerLibrary_Popup_AddCustomer()
        {
            InitializeComponent();
            this.TextBox_Customer_Number.Focus();
        }
        public Page_CustomerLibrary_Popup_AddCustomer(object data)
        {
            InitializeComponent();
            isNew = false;
            InitializeData((Model.CustomerModel)data);
            this.TextBox_Customer_Number.Focus();
        }

        private void InitializeData(Model.CustomerModel d)
        {
            this.d = d;
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
            OldAddTime = d.AddTime.ToString();
        }

        private bool CheckAndGetData()
        {
            bool flag = true;
            if (this.TextBox_Customer_Number.Text.Trim() == "" || this.TextBox_Customer_Name.Text.Trim() == "")
            {
                return false;
            }
            if(isNew)
            {
                Guid = Guid.NewGuid();
                d.Guid = Guid;
            }
            else
            {
                d.Guid = OldGuid;
            }
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
            if(OldAddTime == "")
            {
                d.AddTime = DateTime.Now;
            }
            return flag;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if(CheckAndGetData())
            {
                if (!isNew)
                {
                    if(!new ViewModel.Customer.CustomerConsole().Update(d))
                    {
                        MessageBox.Show("编号或名称重复", "错误");
                        return;
                    }
                    StatusBarMessageEvent.OnUpdateMessage("修改用户：" + d.Name);
                }
                else
                {
                    if(!new ViewModel.Customer.CustomerConsole().Add(d))
                    {
                        MessageBox.Show("编号或名称重复", "错误");
                        return;
                    }
                    StatusBarMessageEvent.OnUpdateMessage("添加用户：" + d.Name);
                }
                Button_Cancel_Click(null, null);
            }
            else
            {
                MessageBox.Show("请检查输入是否有误。", "错误");
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
            CustomerEvent.OnUpdateDataGrid();
        }
    }
}
