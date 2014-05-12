using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.Helper.Events
{
    class ProductEvent
    {
        public static EventHandler<ProductEventArgs> EAdd;
        public static EventHandler<ProductEventArgs> EDelete;
        public static EventHandler<ProductEventArgs> EMarkDelete;
        public static EventHandler EUpdateDataGrid;

        internal static void OnAdd(object sender, ProductModel ProductData)
        {
            if (EAdd != null)
            {
                ProductEventArgs ee = new ProductEventArgs();
                ee.ProductData = ProductData;
                EAdd(sender, ee);
            }
        }
        internal static void OnDelete(object sender, ProductModel ProductData)
        {
            if (EDelete != null)
            {
                ProductEventArgs ee = new ProductEventArgs();
                ee.ProductData = ProductData;
                EDelete(sender, ee);
            }
        }
        internal static void OnMarkDelete(object sender, ProductModel ProductData)
        {
            if (EMarkDelete != null)
            {
                ProductEventArgs ee = new ProductEventArgs();
                ee.ProductData = ProductData;
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
