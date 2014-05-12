using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.Helper.Events
{
    class RawMaterialsEvent
    {
        public static EventHandler<RawMaterialsEventArgs> EAdd;
        public static EventHandler<RawMaterialsEventArgs> EUpdate;
        public static EventHandler<RawMaterialsEventArgs> EMarkDelete;
        public static EventHandler EUpdateDataGrid;

        internal static void OnAdd(object sender, RawMaterialsModel RawMaterialsData)
        {
            if (EAdd != null)
            {
                RawMaterialsEventArgs ee = new RawMaterialsEventArgs();
                ee.RawMaterialsData = RawMaterialsData;
                EAdd(sender, ee);
            }
        }
        internal static void OnUpdate(object sender, RawMaterialsModel RawMaterialsData)
        {
            if (EUpdate != null)
            {
                RawMaterialsEventArgs ee = new RawMaterialsEventArgs();
                ee.RawMaterialsData = RawMaterialsData;
                EUpdate(sender, ee);
            }
        }
        internal static void OnMarkDelete(object sender, RawMaterialsModel RawMaterialsData)
        {
            if (EMarkDelete != null)
            {
                RawMaterialsEventArgs ee = new RawMaterialsEventArgs();
                ee.RawMaterialsData = RawMaterialsData;
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
