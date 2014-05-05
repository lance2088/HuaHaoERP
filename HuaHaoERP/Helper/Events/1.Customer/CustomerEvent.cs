using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.Helper.Events
{
    static class CustomerEvent
    {
        public static EventHandler<CustomerEventArgs> EAdd;
        public static EventHandler<CustomerEventArgs> EDelete;
        public static EventHandler<CustomerEventArgs> EMarkDelete;
        public static EventHandler<CustomerEventArgs> EModify;
        public static EventHandler EUpdateDataGrid;

        internal static void OnAdd(object sender, CustomerModel CustomerData)
        {
            if(EAdd != null)
            {
                CustomerEventArgs ee = new CustomerEventArgs();
                ee.CustomerData = CustomerData;
                EAdd(sender, ee);
            }
        }
        internal static void OnDelete(object sender, CustomerModel CustomerData)
        {
            if (EDelete != null)
            {
                CustomerEventArgs ee = new CustomerEventArgs();
                ee.CustomerData = CustomerData;
                EDelete(sender, ee);
            }
        }
        internal static void OnMarkDelete(object sender, CustomerModel CustomerData)
        {
            if (EMarkDelete != null)
            {
                CustomerEventArgs ee = new CustomerEventArgs();
                ee.CustomerData = CustomerData;
                EMarkDelete(sender, ee);
            }
        }
        internal static void OnModify(object sender, CustomerModel CustomerData)
        {
            if (EModify != null)
            {
                CustomerEventArgs ee = new CustomerEventArgs();
                ee.CustomerData = CustomerData;
                EModify(sender, ee);
            }
        }
        internal static void OnUpdateDataGrid(object sender, EventArgs e)
        {
            if (EUpdateDataGrid != null)
            {
                EUpdateDataGrid(sender, e);
            }
        }
    }
}
