using HuaHaoERP.Model.Warehouse;
using HuaHaoERP.ViewModel.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace HuaHaoERP.View.Pages.Content_Warehouse
{
    /// <summary>
    /// Interaction logic for Page_Warehouse_SparePartsInventory.xaml
    /// </summary>
    public partial class Page_Warehouse_SparePartsInventory : Page
    {
        private Guid ProcessorsID;

        public Page_Warehouse_SparePartsInventory()
        {
            InitializeComponent();
            InitProcessorsComboBox();
            InitializeDataGrid();
        }

        private void InitProcessorsComboBox()
        {
            this.ComboBox_Processors.ItemsSource = Helper.DataDefinition.ComboBoxList.ProcessorsListWithAll.DefaultView;
            this.ComboBox_Processors.DisplayMemberPath = "Name";
            this.ComboBox_Processors.SelectedValuePath = "GUID";//GUID四个字母要大写
            this.ComboBox_Processors.SelectedIndex = 0;
        }

        private void InitializeDataGrid()
        {
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                return;
            }
            this.ProcessorsID = (Guid)this.ComboBox_Processors.SelectedValue;
            List<WarehouseSparePartsInventoryModel> dd = new List<WarehouseSparePartsInventoryModel>();
            new WarehouseSparePartsInventoryConsole().ReadDetailsList(this.TextBox_Search.Text.Trim().Replace("'", ""),ProcessorsID, out dd);
            DataGrid_Num.ItemsSource = dd;
        }


        private void TextBox_Search_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Focus();
        }

        private void TextBox_Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            InitializeDataGrid();
        }

        private void ComboBox_Processors_DropDownClosed(object sender, EventArgs e)
        {
            InitializeDataGrid();
        }

        private void ComboBox_Processors_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).IsDropDownOpen == false)
            {
                (sender as ComboBox).IsDropDownOpen = true;
            }
            if (this.ComboBox_Processors.SelectedValue == null)
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

        private void ComboBox_Processors_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ComboBox_Processors.IsDropDownOpen = true;
        }

        private void ComboBox_Processors_DropDownOpened(object sender, EventArgs e)
        {
            if (this.ComboBox_Processors.SelectedValue == null)
            {
                InitProcessorsComboBox();
            }
        }
    }
}
