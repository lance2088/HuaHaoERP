﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.Helper.Events
{
    static class CustomerEvent
    {
        public static EventHandler<CustomerEventArgs> EAdd;
        public static EventHandler<CustomerEventArgs> EUpdate;
        public static EventHandler<CustomerEventArgs> EMarkDelete;
        public static EventHandler EUpdateDataGrid;

        internal static void OnAdd(object sender, CustomerModel d)
        {
            if(EAdd != null)
            {
                CustomerEventArgs ee = new CustomerEventArgs();
                ee.CustomerData = d;
                EAdd(sender, ee);
            }
        }
        internal static void OnUpdate(object sender, CustomerModel d)
        {
            if (EUpdate != null)
            {
                CustomerEventArgs ee = new CustomerEventArgs();
                ee.CustomerData = d;
                EUpdate(sender, ee);
            }
        }
        internal static void OnMarkDelete(object sender, CustomerModel d)
        {
            if (EMarkDelete != null)
            {
                CustomerEventArgs ee = new CustomerEventArgs();
                ee.CustomerData = d;
                EMarkDelete(sender, ee);
            }
        }
        internal static void OnUpdateDataGrid()
        {
            if (EUpdateDataGrid != null)
            {
                EUpdateDataGrid(null, null);
            }
        }
    }
}