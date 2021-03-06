﻿using System;
using System.Windows;
using System.Windows.Controls;

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
            this.TextBox_Number.Focus();
        }
        public Page_CustomerLibrary_Popup_AddStaff(object data)
        {
            InitializeComponent();
            this.DatePicker_DepartureTime.IsEnabled = false;
            this.DatePicker_DepartureTime.SelectedDate = DateTime.Now;
            isNew = false;
            InitializeData((Model.StaffModel)data);
            this.TextBox_Number.Focus();
        }
        private void InitializeData(Model.StaffModel d)
        {
            this.d = d;
            OldGuid = d.Guid;
            this.TextBox_Number.Text = d.Number;
            this.TextBox_Name.Text = d.Name;
            this.TextBox_Jobs.Text = d.Jobs;
            this.DatePicker_EntryTime.SelectedDate = Convert.ToDateTime(d.EntryTime);
            this.TextBox_Seniority.Text = d.Seniority;
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
            if (this.TextBox_Number.Text.Trim() == "")
            {
                return false;
            }
            if (this.TextBox_Name.Text.Trim() == "")
            {
                return false;
            }
            if (isNew)
            {
                this.Guid = Guid.NewGuid();
                d.Guid = this.Guid;
            }
            else
            {
                d.Guid = OldGuid;
            }
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
                if (!isNew)
                {
                    if(!new ViewModel.Customer.StaffConsole().Update(d))
                    {
                        MessageBox.Show("编号重复（请勿遗漏离职员工）", "错误");
                        return;
                    }
                    Helper.Events.StatusBarMessageEvent.OnUpdateMessage("修改员工：" + d.Name);
                }
                else
                {
                    if(!new ViewModel.Customer.StaffConsole().Add(d))
                    {
                        MessageBox.Show("编号重复（请勿遗漏离职员工）", "错误");
                        return;
                    }
                    Helper.Events.StatusBarMessageEvent.OnUpdateMessage("添加员工：" + d.Name);
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
            Helper.Events.StaffEvent.OnUpdateDataGrid();
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

        private void DatePicker_EntryTime_CalendarClosed(object sender, RoutedEventArgs e)
        {
            this.TextBox_Seniority.Text = Helper.Tools.Seniority.SeniorityForMonth((DateTime)(sender as DatePicker).SelectedDate);
        }
    }
}
