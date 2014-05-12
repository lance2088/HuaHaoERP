﻿using System;
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

namespace HuaHaoERP.View.Pages.Content_Orders
{
    public partial class Page_Orders : Page
    {
        public Page_Orders()
        {
            InitializeComponent();
            InitializeProductOrderData();

            SubscribeToEvent();
        }
        private void SubscribeToEvent()
        {
            ProductOrderEvent.EUpdateDataGrid += (s, e) =>
            {
                InitializeProductOrderData();
            };
        }

        private void InitializeProductOrderData()
        {
            //this.DataGrid_ProductOrder.ItemsSource
        }
        private void Button_AddProductOrder_Click(object sender, RoutedEventArgs e)
        {
            Helper.Events.PopUpEvent.OnShowPopUp(new Page_Orders_Product());
        }
    }
}
