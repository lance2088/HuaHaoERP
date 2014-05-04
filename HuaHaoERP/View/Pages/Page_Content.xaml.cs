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

namespace HuaHaoERP.View.Pages
{
    public partial class Page_Content : Page
    {
        public Page_Content()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            this.Frame_Content_CustomerLibrary.Content = new Content_CustomerLibrary.Page_CustomerLibrary();
            this.Frame_Content_MeansOfProduction.Content = new Content_MeansOfProduction.Page_MeansOfProduction();
            this.Frame_Content_ProductionManagement.Content = new Content_ProductionManagement.Page_ProductionManagement();
            this.Frame_Content_Warehouse.Content = new Content_Warehouse.Page_Warehouse();
            this.Frame_Content_Settings.Content = new Content_Settings.Page_Settings();
            this.Frame_Content_Orders.Content = new ContentOrders.Page_Orders();
        }
        
    }
}
