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
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_CustomerLibrary_Popup_AddCustomer());
        }
        private void DataGrid_Customer_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Customer.SelectedCells.Count != 0)
            {
                HuaHaoERP.Model.CustomerModel data = this.DataGrid_Customer.SelectedCells[0].Item as HuaHaoERP.Model.CustomerModel;
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_CustomerLibrary_Popup_AddCustomer(data));
            }
        }
        private void Button_DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Customer.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.CustomerModel data = this.DataGrid_Customer.SelectedCells[0].Item as HuaHaoERP.Model.CustomerModel;
                if(MessageBox.Show("确认删除用户："+data.Name+"？","警告",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.Customer.CustomerConsole().MarkDelete(data);
                    CustomerEvent.OnUpdateDataGrid();
                }
            }
        }
        #endregion

        #region Supplier 供应商

        private void InitializeSupplierDataGrid()
        {
            List<Model.SupplierModel> data;
            new ViewModel.Customer.SupplierConsole().ReadList(out data);
            this.DataGrid_Supplier.ItemsSource = data;
        }
        private void Button_AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_CustomerLibrary_Popup_AddSupplier());
        }
        private void DataGrid_Supplier_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Supplier.SelectedCells.Count != 0)
            {
                HuaHaoERP.Model.SupplierModel data = this.DataGrid_Supplier.SelectedCells[0].Item as HuaHaoERP.Model.SupplierModel;
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_CustomerLibrary_Popup_AddSupplier(data));
            }
        }
        private void Button_DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Supplier.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.SupplierModel data = this.DataGrid_Supplier.SelectedCells[0].Item as HuaHaoERP.Model.SupplierModel;
                if (MessageBox.Show("确认删除供应商：" + data.Name + "？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.Customer.SupplierConsole().MarkDelete(data);
                    Helper.Events.SupplierEvent.OnUpdateDataGrid();
                }
            }
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
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_CustomerLibrary_Popup_AddStaff());
        }
        private void DataGrid_Staff_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Staff.SelectedCells.Count != 0)
            {
                HuaHaoERP.Model.StaffModel data = this.DataGrid_Staff.SelectedCells[0].Item as HuaHaoERP.Model.StaffModel;
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_CustomerLibrary_Popup_AddStaff(data));
            }
        }
        private void Button_DeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Staff.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.StaffModel data = this.DataGrid_Staff.SelectedCells[0].Item as HuaHaoERP.Model.StaffModel;
                if (MessageBox.Show("确认删除员工：" + data.Name + "？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.Customer.StaffConsole().MarkDelete(data);
                    Helper.Events.StaffEvent.OnUpdateDataGrid();
                }
            }
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
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_CustomerLibrary_Popup_AddProcessors());
        }
        private void DataGrid_Processors_Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Processors.SelectedCells.Count != 0)
            {
                HuaHaoERP.Model.ProcessorsModel data = this.DataGrid_Processors.SelectedCells[0].Item as HuaHaoERP.Model.ProcessorsModel;
                Helper.Events.PopUpEvent.OnShowPopUp(new Page_CustomerLibrary_Popup_AddProcessors(data));
            }
        }
        private void Button_DeleteProcessors_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid_Processors.SelectedCells.Count > 0)
            {
                HuaHaoERP.Model.ProcessorsModel data = this.DataGrid_Processors.SelectedCells[0].Item as HuaHaoERP.Model.ProcessorsModel;
                if (MessageBox.Show("确认删除加工商：" + data.Name + "？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ViewModel.Customer.ProcessorsConsole().MarkDelete(data);
                    ProcessorsEvent.OnUpdateDataGrid();
                }
            }
        }
        #endregion

        
    }
}
