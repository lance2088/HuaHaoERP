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

namespace HuaHaoERP.View.Pages.Content1
{
    public partial class Page_MainContent1 : Page
    {
        public Page_MainContent1()
        {
            InitializeComponent();
            InitializeCustomerDataGrid();
        }

    #region Client

        private void InitializeCustomerDataGrid()
        {
            List<Model.CustomerModel> data;
            new ViewModel.Customer.CustomerConsole().ReadList(out data);
            this.DataGrid_Customer.ItemsSource = data;
            
        }
        private void Button_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_MainContent1_Popup_AddCustomer());
        }

    #endregion

    #region Supplier

        private void Button_AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_MainContent1_Popup_AddSupplier());
        }

    #endregion

    #region Staff

        private void Button_AddStaff_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(this, new Page_MainContent1_Popup_AddStaff());
        }

    #endregion
    }
}
