using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.Helper.Events
{
    class StaffEvent
    {
        public static EventHandler<StaffEventArgs> EAdd;
        public static EventHandler<StaffEventArgs> EUpdate;
        public static EventHandler<StaffEventArgs> EMarkDelete;
        public static EventHandler EUpdateDataGrid;

        internal static void OnAdd(object sender, StaffModel StaffData)
        {
            if (EAdd != null)
            {
                StaffEventArgs ee = new StaffEventArgs();
                ee.StaffData = StaffData;
                EAdd(sender, ee);
            }
        }
        internal static void OnUpdate(object sender, StaffModel StaffData)
        {
            if (EUpdate != null)
            {
                StaffEventArgs ee = new StaffEventArgs();
                ee.StaffData = StaffData;
                EUpdate(sender, ee);
            }
        }
        internal static void OnMarkDelete(object sender, StaffModel StaffData)
        {
            if (EMarkDelete != null)
            {
                StaffEventArgs ee = new StaffEventArgs();
                ee.StaffData = StaffData;
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
