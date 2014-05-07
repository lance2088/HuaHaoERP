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

namespace HuaHaoERP.View.Pages.Content_CustomerLibrary
{
    public partial class Page_CustomerLibrary : Page
    {
        public Page_CustomerLibrary()
        {
            InitializeComponent();
            SubscribeToEvent();
            InitializeCustomerDataGrid();
            InitializeSupplierDataGrid();
            InitializeStaffDataGrid();
            InitializeProcessorsDataGrid();
        }
        private void SubscribeToEvent()
        {
            CustomerEvent.EUpdateDataGrid += (sender, e) => 
            {
                InitializeCustomerDataGrid();
            };
            SupplierEvent.EUpdateDataGrid += (sender, e) =>
            {
                InitializeSupplierDataGrid();
            };
            StaffEvent.EUpdateDataGrid += (sender, e) =>
            {
                InitializeStaffDataGrid();
            };
            ProcessorsEvent.EUpdateDataGrid += (sender, e) =>
            {
                InitializeProcessorsDataGrid();
            };
        }

        #region Customer 客户

        private void InitializeCustomerDataGrid()
        {
            List<Model.CustomerModel> data;
            new ViewModel.Customer.CustomerConsole().ReadList(out data);
            this.DataGrid_Customer.ItemsSource = data;
        }
        private void Button_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_CustomerLibrary_Popup_AddCustomer());
        }
        private void DataGrid_Customer_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.MouseRightButtonDown += (s, a) =>
            {
                a.Handled = true;
                (sender as DataGrid).SelectedIndex = (s as DataGridRow).GetIndex();
                (s as DataGridRow).Focus();
                this.Popup.IsOpen = true;
                HuaHaoERP.Model.CustomerModel asd = this.DataGrid_Customer.SelectedCells[0].Item as HuaHaoERP.Model.CustomerModel;
                this.Frame_RightKeyMenu.Content = new Page_CustomerLibrary_Popup_RightKey();
                //this.L_A.Content = asd.Name;
            };
        }
        private void DataGrid_Customer_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Customer.SelectedCells.Count != 0)
            {
                HuaHaoERP.Model.CustomerModel data = this.DataGrid_Customer.SelectedCells[0].Item as HuaHaoERP.Model.CustomerModel;
                Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_CustomerLibrary_Popup_AddCustomer(data));
            }
        }

        #endregion

        #region Supplier 供应商

        private void InitializeSupplierDataGrid()
        {
            List<Model.SupplierModel> data;
            new ViewModel.Customer.SupplierConsole().ReadList(out data);
            this.DataGrid_Processors.ItemsSource = data;
        }
        private void Button_AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_CustomerLibrary_Popup_AddSupplier());
        }
        private void DataGrid_Supplier_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Staff 员工

        private void InitializeStaffDataGrid()
        {
            List<Model.StaffModel> data;
            new ViewModel.Customer.StaffConsole().ReadList(out data);
            this.DataGrid_Staff.ItemsSource = data;
        }
        private void Button_AddStaff_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_CustomerLibrary_Popup_AddStaff());
        }
        private void DataGrid_Staff_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Processors 外加工商
        private void InitializeProcessorsDataGrid()
        {
            List<Model.ProcessorsModel> data;
            new ViewModel.Customer.ProcessorsConsole().ReadList(out data);
            this.DataGrid_Processors.ItemsSource = data;
        }
        private void Button_Add_Processors_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_CustomerLibrary_Popup_AddProcessors());
        }
        private void DataGrid_Processors_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Processors.SelectedCells.Count != 0)
            {
                HuaHaoERP.Model.ProcessorsModel data = this.DataGrid_Processors.SelectedCells[0].Item as HuaHaoERP.Model.ProcessorsModel;
                Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_CustomerLibrary_Popup_AddProcessors(data));
            }
        }
        #endregion

    }
}
