using System;

namespace HuaHaoERP.Helper.Events.UpdateEvent
{
    class WarehouseRawMaterialsEvent
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
