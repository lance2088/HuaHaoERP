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

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement_OutsideProcess : Page
    {
        private bool isOut = true;
        private Model.ProductionManagement_OutsideProcessModel d = new Model.ProductionManagement_OutsideProcessModel();
        private Guid Guid;
        private Guid OldGuid;
        private bool isNew = true;

        public Page_ProductionManagement_OutsideProcess(bool isOut)
        {
            InitializeComponent();
            this.isOut = isOut;
            InitializeData();
        }

        private void InitializeData()
        {
            this.DatePicker_OrderDate.SelectedDate = DateTime.Now;
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductList.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
            this.ComboBox_Processors.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsList.DefaultView;
            this.ComboBox_Processors.DisplayMemberPath = "Name";
            this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Processors.SelectedIndex = 0;

            if(isOut)
            {
                this.Label_Title.Content += "出单";
            }
            else
            {
                this.Label_Title.Content += "入单";
            }
        }

        private bool GetAndCheckData()
        {
            int FalseCount = 0;
            this.Guid = Guid.NewGuid();
            d.Guid = this.Guid;
            d.OrderDate = ((DateTime)this.DatePicker_OrderDate.SelectedDate).ToString("yyyy-MM-dd HH:mm:ss");
            d.ProductGuid = (Guid)this.ComboBox_Product.SelectedValue;
            d.ProcessorsGuid = (Guid)this.ComboBox_Processors.SelectedValue;
            int Quantity = 0;
            if(!int.TryParse(this.TextBox_Quantity.Text.Trim(), out Quantity))
            {
                FalseCount++;
            }
            d.Quantity = Quantity;
            int MinorInjuries = 0;
            int.TryParse(this.TextBox_MinorInjuries.Text.Trim(), out MinorInjuries);
            d.MinorInjuries = MinorInjuries;
            int Injuries = 0;
            int.TryParse(this.TextBox_Injuries.Text.Trim(), out Injuries);
            d.Injuries = Injuries;
            int Lose = 0;
            int.TryParse(this.TextBox_Lose.Text.Trim(), out Lose);
            d.Lose = Lose;
            if(isOut)
            {
                d.OrderType = "出单";
            }
            else
            {
                d.OrderType = "入单";
            }
            d.Remark = this.TextBox_Remark.Text.Trim();
            if (FalseCount > 0)
            {
                return false;
            }
            return true;
        }

        private void Button_Commit_Click(object sender, RoutedEventArgs e)
        {
            if (GetAndCheckData())
            {
                new ViewModel.ProductionManagement.OutsideProcessConsole().Add(d);
                Button_Cancel_Click(null,null);
            }
            else
            {

            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            PopUpEvent.OnHidePopUp();
        }

        #region 限制输入数字
        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;

            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal)
            {
                if (txt.Text.Contains(".") && e.Key == Key.Decimal)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                if (txt.Text.Contains(".") && e.Key == Key.OemPeriod)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }
        #endregion
    }
}
