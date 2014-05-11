using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.Helper.Events
{
    class ProductOrderEvent
    {
        public static EventHandler<ProductOrderEventArgs> EAdd;
        public static EventHandler<ProductOrderEventArgs> EDelete;
        public static EventHandler<ProductOrderEventArgs> EMarkDelete;
        public static EventHandler EUpdateDataGrid;

        internal static void OnAdd(object sender, ProductOrderModel d)
        {
            if (EAdd != null)
            {
                ProductOrderEventArgs ee = new ProductOrderEventArgs();
                ee.Data = d;
                EAdd(sender, ee);
            }
        }
        internal static void OnDelete(object sender, ProductOrderModel d)
        {
            if (EDelete != null)
            {
                ProductOrderEventArgs ee = new ProductOrderEventArgs();
                ee.Data = d;
                EDelete(sender, ee);
            }
        }
        internal static void OnMarkDelete(object sender, ProductOrderModel d)
        {
            if (EMarkDelete != null)
            {
                ProductOrderEventArgs ee = new ProductOrderEventArgs();
                ee.Data = d;
                EMarkDelete(sender, ee);
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
