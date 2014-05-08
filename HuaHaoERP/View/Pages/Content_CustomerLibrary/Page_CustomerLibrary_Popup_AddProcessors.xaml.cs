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

namespace HuaHaoERP.View.Pages.Content_CustomerLibrary
{
    public partial class Page_CustomerLibrary_Popup_AddProcessors : Page
    {
        private Model.ProcessorsModel d = new Model.ProcessorsModel();
        private Guid Guid;
        private Guid OldGuid;
        private string OldAddTime = "";
        private bool isNew = true;

        public Page_CustomerLibrary_Popup_AddProcessors()
        {
            InitializeComponent();
        }
        public Page_CustomerLibrary_Popup_AddProcessors(object data)
        {
            InitializeComponent();
            isNew = false;
            InitializeData((Model.ProcessorsModel)data);
        }
        private void InitializeData(Model.ProcessorsModel d)
        {
            this.d = d;
            OldGuid = d.Guid;
            this.TextBox_Number.Text = d.Number;
            this.TextBox_Name.Text = d.Name;
            this.TextBox_Address.Text = d.Address;
            this.TextBox_Area.Text = d.Area;
            this.TextBox_Phone.Text = d.Phone;
            this.TextBox_MobilePhone.Text = d.MobilePhone;
            this.TextBox_Fax.Text = d.Fax;
            this.TextBox_Business.Text = d.Business;
            this.TextBox_Clerk.Text = d.Clerk;
            this.TextBox_OpeningBank.Text = d.OpeningBank;
            this.TextBox_BankCardNo.Text = d.BankCardNo.ToString();
            this.TextBox_BankCardName.Text = d.BankCardName;
            this.TextBox_Remark.Text = d.Remark;
            OldAddTime = d.AddTime.ToString();
        }

        private bool CheckAndGetData()
        {
            bool flag = true;
            if(this.TextBox_Number.Text.Trim() == "" || this.TextBox_Name.Text.Trim() == "")
            {
                return false;
            }
            this.Guid = Guid.NewGuid();
            d.Guid = this.Guid;
            d.Number = this.TextBox_Number.Text.Trim();
            d.Name = this.TextBox_Name.Text.Trim();
            d.Address = this.TextBox_Address.Text.Trim();
            d.Area = this.TextBox_Area.Text.Trim();
            d.Phone = this.TextBox_Phone.Text.Trim();
            d.MobilePhone = this.TextBox_MobilePhone.Text.Trim();
            d.Fax = this.TextBox_Fax.Text.Trim();
            d.Business = this.TextBox_Business.Text.Trim();
            d.Clerk = this.TextBox_Clerk.Text.Trim();
            d.OpeningBank = this.TextBox_OpeningBank.Text.Trim();
            d.BankCardNo = this.TextBox_BankCardNo.Text.Trim();
            d.BankCardName = this.TextBox_BankCardName.Text.Trim();
            d.Remark = this.TextBox_Remark.Text.Trim();
            if (OldAddTime == "")
            {
                d.AddTime = DateTime.Now;
            }
            return flag;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAndGetData())
            {
                Helper.Events.ProcessorsEvent.OnAdd(this, d);
                if (!isNew)
                {
                    Model.ProcessorsModel dOld = new Model.ProcessorsModel();
                    dOld.Guid = OldGuid;
                    Helper.Events.ProcessorsEvent.OnDelete(this, dOld);
                    Helper.Events.StatusBarMessageEvent.OnUpdateMessage(this, "修改外加工商：" + d.Name);
                }
                else
                {
                    Helper.Events.StatusBarMessageEvent.OnUpdateMessage(this, "添加外加工商：" + d.Name);
                }
                Button_Cancel_Click(null, null);
            }
            else
            {

            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp(this);
        }
    }
}
