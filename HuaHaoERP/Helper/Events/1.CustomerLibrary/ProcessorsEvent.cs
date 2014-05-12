using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class ProcessorsEvent
    {
        public static EventHandler<ProcessorsEventArgs> EAdd;
        public static EventHandler<ProcessorsEventArgs> EUpdate;
        public static EventHandler<ProcessorsEventArgs> EMarkDelete;
        public static EventHandler EUpdateDataGrid;

        internal static void OnAdd(object sender, Model.ProcessorsModel d)
        {
            if(EAdd != null)
            {
                ProcessorsEventArgs ee = new ProcessorsEventArgs();
                ee.ProcessorsData = d;
                EAdd(sender, ee);
            }
        }
        internal static void OnUpdate(object sender, Model.ProcessorsModel d)
        {
            if (EUpdate != null)
            {
                ProcessorsEventArgs ee = new ProcessorsEventArgs();
                ee.ProcessorsData = d;
                EUpdate(sender, ee);
            }
        }
        internal static void OnMarkDelete(object sender, Model.ProcessorsModel d)
        {
            if(EMarkDelete != null)
            {
                ProcessorsEventArgs ee = new ProcessorsEventArgs();
                ee.ProcessorsData = d;
                EMarkDelete(sender, ee);
            }
        }
        internal static void OnUpdateDataGrid()
        {
            if(EUpdateDataGrid != null)
            {
                EUpdateDataGrid(null, null);
            }
        }
    }
}
