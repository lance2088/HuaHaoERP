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
            this.Frame_Content1.Content = new Content_CustomerLibrary.Page_MainContent_CustomerLibrary();
            this.Frame_Content2.Content = new Content2.Page_MainContent2();
            this.Frame_Content3.Content = new Content3.Page_MainContent3();
            this.Frame_Content4.Content = new Content4.Page_MainContent4();
            this.Frame_ContentSettings.Content = new ContentSettings.Page_MainContent_Settings();
            this.Frame_Content_Orders.Content = new ContentOrders.Page_MainContent_Orders();
        }
        
    }
}
