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

namespace HuaHaoERP.View.Pages.Content_ProductionManagement
{
    public partial class Page_ProductionManagement_AssemblyLineModuleDetails : Page
    {
        public Page_ProductionManagement_AssemblyLineModuleDetails()
        {
            InitializeComponent();
            this.DatePicker_Start.SelectedDate = DateTime.Now;
            this.DatePicker_End.SelectedDate = DateTime.Now;
            this.ComboBox_Product.ItemsSource = Helper.DataDefinition.ComboBoxList.ProductListWithAll.DefaultView;
            this.ComboBox_Product.DisplayMemberPath = "Name";
            this.ComboBox_Product.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Product.SelectedIndex = 0;
            this.ComboBox_Process.ItemsSource = Helper.DataDefinition.Process.ProcessListWithAll;
            this.ComboBox_Process.SelectedIndex = 0;
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            Guid ProductID = (Guid)this.ComboBox_Product.SelectedValue;
            string Process = this.ComboBox_Process.Text;
            DateTime Start = ((DateTime)this.DatePicker_Start.SelectedDate).Date;
            DateTime End = ((DateTime)this.DatePicker_End.SelectedDate).Date.AddDays(1);

            List<Model.ProductionManagement.AssemblyLineDetailsModel> d = new List<Model.ProductionManagement.AssemblyLineDetailsModel>();
            new ViewModel.ProductionManagement.AssemblyLineModuleConsole().ReadDetials(ProductID, Process, Start, End, out d);
            this.DataGrid_Detials.ItemsSource = d;
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
            this.DatePicker_Start.SelectedDate = Convert.ToDateTime("2014-01-01 00:00:00");
            this.DatePicker_End.SelectedDate = Convert.ToDateTime("2024-01-01 00:00:00");
            InitializeDataGrid();
        }
    }
}
