using System;

namespace HuaHaoERP.Helper.Events.UpdateEvent.Warehouse
{
    class SparePartsInventoryEvent
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
