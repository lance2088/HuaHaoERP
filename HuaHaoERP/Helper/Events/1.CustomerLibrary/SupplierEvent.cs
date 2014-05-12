using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.Helper.Events
{
    class SupplierEvent
    {
        public static EventHandler<SupplierEventArgs> EAdd;
        public static EventHandler<SupplierEventArgs> EUpdate;
        public static EventHandler<SupplierEventArgs> EMarkDelete;
        public static EventHandler EUpdateDataGrid;

        internal static void OnAdd(object sender, SupplierModel SupplierData)
        {
            if (EAdd != null)
            {
                SupplierEventArgs ee = new SupplierEventArgs();
                ee.SupplierData = SupplierData;
                EAdd(sender, ee);
            }
        }
        internal static void OnUpdate(object sender, SupplierModel SupplierData)
        {
            if (EUpdate != null)
            {
                SupplierEventArgs ee = new SupplierEventArgs();
                ee.SupplierData = SupplierData;
                EUpdate(sender, ee);
            }
        }
        internal static void OnMarkDelete(object sender, SupplierModel SupplierData)
        {
            if (EMarkDelete != null)
            {
                SupplierEventArgs ee = new SupplierEventArgs();
                ee.SupplierData = SupplierData;
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
