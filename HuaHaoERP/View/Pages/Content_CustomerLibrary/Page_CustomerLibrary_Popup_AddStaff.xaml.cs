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
    public partial class Page_CustomerLibrary_Popup_AddStaff : Page
    {
        private Model.StaffModel d = new Model.StaffModel();
        private Guid Guid;
        private Guid OldGuid;
        private string OldAddTime = "";
        private bool isNew = true;

        public Page_CustomerLibrary_Popup_AddStaff()
        {
            InitializeComponent();
            this.DatePicker_DepartureTime.IsEnabled = false;
            this.DatePicker_EntryTime.SelectedDate = DateTime.Now;
            this.DatePicker_DepartureTime.SelectedDate = DateTime.Now;
        }
        public Page_CustomerLibrary_Popup_AddStaff(object data)
        {
            InitializeComponent();
            this.DatePicker_DepartureTime.IsEnabled = false;
            this.DatePicker_DepartureTime.SelectedDate = DateTime.Now;
            isNew = false;
            InitializeData((Model.StaffModel)data);
        }
        private void InitializeData(Model.StaffModel d)
        {
            this.d = d;
            OldGuid = d.Guid;
            this.TextBox_Number.Text = d.Number;
            this.TextBox_Name.Text = d.Name;
            this.TextBox_Jobs.Text = d.Jobs;
            this.DatePicker_EntryTime.SelectedDate = Convert.ToDateTime(d.EntryTime);
            this.TextBox_Contact.Text = d.Contact;
            this.TextBox_IDNumber.Text = d.IDNumber;
            this.TextBox_Remark.Text = d.Remark;
            if (!d.DepartureTime.Equals(""))
            {
                this.CheckBox_isDeparture.IsChecked = true;
                this.DatePicker_DepartureTime.IsEnabled = true;
                this.DatePicker_DepartureTime.SelectedDate = Convert.ToDateTime(d.DepartureTime);
            }
            OldAddTime = d.AddTime.ToString();
        }

        private bool CheckAndGetData()
        {
            bool flag = true;
            if (this.TextBox_Name.Text.Trim() == "")
            {
                return false;
            }
            Guid = Guid.NewGuid();
            d.Guid = Guid;
            d.Number = this.TextBox_Number.Text.Trim();
            d.Name = this.TextBox_Name.Text.Trim();
            d.Jobs = this.TextBox_Jobs.Text.Trim();
            d.EntryTime = ((DateTime)this.DatePicker_EntryTime.SelectedDate).ToString("yyyy-MM-dd HH:mm:ss");
            d.Contact = this.TextBox_Contact.Text.Trim();
            d.IDNumber = this.TextBox_IDNumber.Text.Trim();
            d.Remark = this.TextBox_Remark.Text.Trim();
            if ((bool)this.CheckBox_isDeparture.IsChecked)
            {
                d.DepartureTime = ((DateTime)this.DatePicker_DepartureTime.SelectedDate).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                d.DepartureTime = "0001-01-01 00:00:00";
            }
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
                Helper.Events.StaffEvent.OnAdd(this, d);
                if (!isNew)
                {
                    Model.StaffModel dOld = new Model.StaffModel();
                    dOld.Guid = OldGuid;
                    Helper.Events.StaffEvent.OnDelete(this, dOld);
                    Helper.Events.StatusBarMessageEvent.OnUpdateMessage(this, "修改员工：" + d.Name);
                }
                else
                {
                    Helper.Events.StatusBarMessageEvent.OnUpdateMessage(this, "添加员工：" + d.Name);
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

        private void CheckBox_isDeparture_Click(object sender, RoutedEventArgs e)
        {
            bool isDeparture = (bool)this.CheckBox_isDeparture.IsChecked;
            if (isDeparture)
            {
                this.DatePicker_DepartureTime.IsEnabled = true;
            }
            else
            {
                this.DatePicker_DepartureTime.IsEnabled = false;
            }
        }
    }
}
