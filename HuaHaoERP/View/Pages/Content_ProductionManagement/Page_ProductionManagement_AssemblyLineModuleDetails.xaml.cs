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
using System.Data;

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement_AssemblyLineModuleDetails : Page
    {
        public Page_ProductionManagement_AssemblyLineModuleDetails()
        {
            InitializeComponent();
            this.DatePicker_Start.SelectedDate = DateTime.Now;
            this.DatePicker_End.SelectedDate = DateTime.Now;
            InitProductComboBox();
            InitStaffComboBox();
            this.ComboBox_Process.ItemsSource = Helper.DataDefinition.Process.ProcessListWithAll;
            this.ComboBox_Process.SelectedIndex = 0;
            InitializeDataGrid();
        }
        private void InitProductComboBox()
        {
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithAll.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
        }
        private void InitStaffComboBox()
        {
            this.ComboBox_Staff.ItemsSource = Helper.DataDefinition.ComboBoxList.StaffListWithAll.DefaultView;
            this.ComboBox_Staff.DisplayMemberPath = "Name";
            this.ComboBox_Staff.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Staff.SelectedIndex = 0;
        }
        private void InitializeDataGrid()
        {
            //if(this.IsLoaded)
            {
                bool IsShowAutoDeduction = (bool)this.CHeckBox_IsShowAutoDeduction.IsChecked;
                if (this.ComboBox_Product.SelectedValue == null)
                {

                    return;
                }
                Guid ProductID = (Guid)this.ComboBox_Product.SelectedValue;
                string Process = this.ComboBox_Process.Text;
                Guid StaffID = (Guid)this.ComboBox_Staff.SelectedValue;
                DateTime Start = ((DateTime)this.DatePicker_Start.SelectedDate).Date;
                DateTime End = ((DateTime)this.DatePicker_End.SelectedDate).Date.AddDays(1);

                List<Model.ProductionManagement.AssemblyLineDetailsModel> d = new List<Model.ProductionManagement.AssemblyLineDetailsModel>();
                int Count = new ViewModel.ProductionManagement.AssemblyLineModuleConsole().ReadDetials(IsShowAutoDeduction, ProductID, Process, StaffID, Start, End, out d);
                this.DataGrid_Detials.ItemsSource = d;

                this.Label_Count.Content = "统计数量：" + Count;
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnHidePopUp();
        }

        private void ComboBox_Product_DropDownClosed(object sender, EventArgs e)
        {
            InitializeDataGrid();
        }

        private void ComboBox_Process_DropDownClosed(object sender, EventArgs e)
        {
            InitializeDataGrid();
        }

        private void DatePicker_Start_CalendarClosed(object sender, RoutedEventArgs e)
        {
            InitializeDataGrid();
        }

        private void DatePicker_End_CalendarClosed(object sender, RoutedEventArgs e)
        {
            InitializeDataGrid();
        }

        private void Button_Today_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_Start.SelectedDate = DateTime.Now;
            this.DatePicker_End.SelectedDate = DateTime.Now;
            InitializeDataGrid();
        }

        private void Button_AllDate_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_Start.SelectedDate = Convert.ToDateTime("2010-01-01 00:00:00");
            this.DatePicker_End.SelectedDate = Convert.ToDateTime("2024-01-01 00:00:00");
            InitializeDataGrid();
        }

        private void ComboBox_Staff_DropDownClosed(object sender, EventArgs e)
        {
            InitializeDataGrid();
        }

        private void CHeckBox_IsShowAutoDeduction_Click(object sender, RoutedEventArgs e)
        {
            InitializeDataGrid();
        }

        private void MenuItem__Del_Click(object sender, RoutedEventArgs e)
        {
            if(this.DataGrid_Detials.SelectedCells.Count > 0)
            {
                Model.ProductionManagement.AssemblyLineDetailsModel d = this.DataGrid_Detials.SelectedCells[0].Item as Model.ProductionManagement.AssemblyLineDetailsModel;
                if (MessageBox.Show("确认删除生产记录？这是危险操作，可能导致数据混乱\n序号\t"+d.Id+"\n日期\t"+d.Date+"\n产品\t"+d.ProductName+"\n工序\t"+d.Process+"\n数量\t"+d.Quantity, "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (new ViewModel.ProductionManagement.AssemblyLineDetailsConsole().DeleteDetails(d.Guid))
                    {
                        InitializeDataGrid();
                        Helper.Events.ProductionManagement_AssemblyLineEvent.OnUpdateDataGrid();
                    }
                }
            }
        }

        private void ComboBox_Product_PreviewKeyUp(object sender, KeyEventArgs e)
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

        private void ComboBox_Product_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).IsDropDownOpen = true;
        }

        private void ComboBox_Product_DropDownOpened(object sender, EventArgs e)
        {
            if (this.ComboBox_Product.SelectedValue == null)
            {
                InitProductComboBox();
            }
        }
    }
}
