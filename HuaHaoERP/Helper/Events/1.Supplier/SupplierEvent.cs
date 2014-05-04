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
        public static EventHandler<SupplierEventArgs> EDelete;
        public static EventHandler<SupplierEventArgs> EModify;
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
        internal static void OnDelete(object sender, SupplierModel SupplierData)
        {
            if (EDelete != null)
            {
                SupplierEventArgs ee = new SupplierEventArgs();
                ee.SupplierData = SupplierData;
                EDelete(sender, ee);
            }
        }
        internal static void OnModify(object sender, SupplierModel SupplierData)
        {
            if (EModify != null)
            {
                SupplierEventArgs ee = new SupplierEventArgs();
                ee.SupplierData = SupplierData;
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
