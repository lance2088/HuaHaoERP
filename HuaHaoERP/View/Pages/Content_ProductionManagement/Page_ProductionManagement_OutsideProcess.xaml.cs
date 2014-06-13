using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
        //private Guid OldGuid;
        //private bool isNew = true;

        public Page_ProductionManagement_OutsideProcess(bool isOut)
        {
            InitializeComponent();
            this.isOut = isOut;
            InitializeData();
        }
        /// <summary>
        /// 目标入单
        /// </summary>
        /// <param name="ProductName"></param>
        public Page_ProductionManagement_OutsideProcess(bool isOut, string ProductName)
        {
            InitializeComponent();
            this.isOut = isOut;
            InitializeData();
            this.ComboBox_Product.Text = ProductName;
            this.TextBox_Quantity.Focus();
        }
        public Page_ProductionManagement_OutsideProcess(bool isOut, string ProductName, string ProcessorsName)
        {
            InitializeComponent();
            this.isOut = isOut;
            InitializeData();
            this.ComboBox_Product.Text = ProductName;
            this.ComboBox_Processors.Text = ProcessorsName;
            this.TextBox_Quantity.Focus();
        }
        private void InitializeData()
        {
            this.DatePicker_OrderDate.SelectedDate = DateTime.Now;
            InitComboBox_Product();
            InitComboBox_Processors();

            if(isOut)
            {
                this.Label_Title.Content += "出单";
                this.Label_MinorInjuries.Visibility = System.Windows.Visibility.Collapsed;
                this.TextBox_MinorInjuries.Visibility = System.Windows.Visibility.Collapsed;
                this.Label_Injuries.Visibility = System.Windows.Visibility.Collapsed;
                this.TextBox_Injuries.Visibility = System.Windows.Visibility.Collapsed;
                this.Label_Lose.Visibility = System.Windows.Visibility.Collapsed;
                this.TextBox_Lose.Visibility = System.Windows.Visibility.Collapsed;
                this.Label_Remark.Margin = new Thickness(30, 220, 0, 0);
                this.TextBox_Remark.Margin = new Thickness(120, 220, 0, 0);
            }
            else
            {
                this.Label_Title.Content += "入单";
            }
        }

        private void InitComboBox_Product()
        {
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithoutAll.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
        }
        private void InitComboBox_Processors()
        {
            this.ComboBox_Processors.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsListWithoutAll.DefaultView;
            this.ComboBox_Processors.DisplayMemberPath = "Name";
            this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Processors.SelectedIndex = 0;
        }

        private bool GetAndCheckData()
        {
            int FalseCount = 0;
            this.Guid = Guid.NewGuid();
            d.Guid = this.Guid;
            d.OrderDate = ((DateTime)this.DatePicker_OrderDate.SelectedDate + DateTime.Now.TimeOfDay).ToString("yyyy-MM-dd HH:mm:ss");
            if (this.ComboBox_Product.SelectedValue == null)
            {
                MessageBox.Show("请录入至少一个产品","错误");
                return false;
            }
            d.ProductGuid = (Guid)this.ComboBox_Product.SelectedValue;
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                MessageBox.Show("请录入至少一个加工商", "错误");
                return false;
            }
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
                Helper.Events.ProductionManagement_AssemblyLineEvent.OnUpdateDataGrid();
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
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal || e.Key == Key.Tab)
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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.SelectAll();
        }

        private void ComboBox_Product_DropDownOpened(object sender, EventArgs e)
        {
            if(this.ComboBox_Product.SelectedValue == null)
            {
                InitComboBox_Product();
            }
        }

        private void ComboBox_Product_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).IsDropDownOpen == false)
            {
                (sender as ComboBox).IsDropDownOpen = true;
            }
            if (this.ComboBox_Product.SelectedValue == null)
            {
                string Parm = this.ComboBox_Product.Text;
                DataSet ds = new DataSet();
                if (new ViewModel.MeansOfProduction.ProductConsole().GetNameList(Parm, out ds))
                {
                    this.ComboBox_Product.ItemsSource = ds.Tables[0].DefaultView;
                    this.ComboBox_Product.DisplayMemberPath = "Name";
                    this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
                }
            }
        }

        private void ComboBox_Processors_DropDownOpened(object sender, EventArgs e)
        {
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                InitComboBox_Processors();
            }
        }

        private void ComboBox_Processors_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).IsDropDownOpen == false)
            {
                (sender as ComboBox).IsDropDownOpen = true;
            }
            if (this.ComboBox_Product.SelectedValue == null)
            {
                string Parm = this.ComboBox_Processors.Text;
                DataSet ds = new DataSet();
                if (new ViewModel.Customer.ProcessorsConsole().GetNameList(Parm, out ds))
                {
                    this.ComboBox_Processors.ItemsSource = ds.Tables[0].DefaultView;
                    this.ComboBox_Processors.DisplayMemberPath = "Name";
                    this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
                }
            }
        }

        private void EditableComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).IsDropDownOpen = true;
        }
    }
}
