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
        public static EventHandler<ProductOrderEventArgs> EUpdate;
        public static EventHandler<ProductOrderEventArgsForDateGrid> EMarkDelete;
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
        internal static void OnUpdate(object sender, ProductOrderModel d)
        {
            if (EUpdate != null)
            {
                ProductOrderEventArgs ee = new ProductOrderEventArgs();
                ee.Data = d;
                EUpdate(sender, ee);
            }
        }
        internal static void OnMarkDelete(object sender, ProductOrderModelForDataGrid d)
        {
            if (EMarkDelete != null)
            {
                ProductOrderEventArgsForDateGrid ee = new ProductOrderEventArgsForDateGrid();
                ee.Data = d;
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
