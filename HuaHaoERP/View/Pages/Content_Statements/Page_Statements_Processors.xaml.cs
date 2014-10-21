using HuaHaoERP.Model.Statement;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HuaHaoERP.View.Pages.Content_Statements
{
    public partial class Page_Statements_Processors : Page
    {
        List<Model_Processors> _data = new List<Model_Processors>();

        public Page_Statements_Processors()
        {
            InitializeComponent();
            Helper.Events.Statement.ProcessorsEvent.EReflash += (s, e) => { IntitializeDataGrid(); };
            this.ComboBox_Processors.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsListWithoutAll.DefaultView;
            this.ComboBox_Processors.DisplayMemberPath = "Name";
            this.ComboBox_Processors.SelectedValuePath = "GUID";
            this.ComboBox_Processors.SelectedIndex = 0;
            this.ComboBox_Year.Text = DateTime.Now.Year.ToString();
            this.ComboBox_Month.SelectedIndex = DateTime.Now.Month;
            IntitializeDataGrid();
        }

        private void IntitializeDataGrid()
        {
            int year = int.Parse(this.ComboBox_Year.Text);
            int month = this.ComboBox_Month.SelectedIndex;
            if (this.ComboBox_Processors.SelectedIndex != -1)
            {
                _data = new ViewModel.Statement.ProcessorsConsole().ReadList((Guid)this.ComboBox_Processors.SelectedValue, year, month);
            }
            this.DataGrid_List.ItemsSource = _data;
        }

        private void Button_Print_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_Month_DropDownClosed(object sender, EventArgs e)
        {
            IntitializeDataGrid();
        }

        private void ComboBox_Year_DropDownClosed(object sender, EventArgs e)
        {
            IntitializeDataGrid();
        }

        private void ComboBox_Processors_DropDownClosed(object sender, EventArgs e)
        {
            IntitializeDataGrid();
        }
    }
}