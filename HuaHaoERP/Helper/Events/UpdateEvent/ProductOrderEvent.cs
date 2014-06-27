using System;

namespace HuaHaoERP.Helper.Events
{
    class ProductOrderEvent
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
