using System;

namespace HuaHaoERP.Helper.Events.UpdateEvent
{
    class AssemblyLineModuleEvent
    {
        public static EventHandler EUpdateDataGrid;

        internal static void OnUpdateDataGrid()
        {
            if (EUpdateDataGrid != null)
            {
                EUpdateDataGrid(null, null);
            }
        }
    }
}
