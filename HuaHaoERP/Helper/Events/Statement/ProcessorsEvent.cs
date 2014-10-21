using System;

namespace HuaHaoERP.Helper.Events.Statement
{
    static class ProcessorsEvent
    {
        internal static EventHandler<PopUpEventArgs> EReflash;
        internal static void OnReflash()
        {
            if (EReflash != null)
            {
                EReflash(null, null);
            }
        }
    }
}