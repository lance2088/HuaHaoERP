using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    static class StatusBarMessageEvent
    {
        internal static EventHandler<StatusBarMessageEventArgs> EUpdateMessage;
        internal static void OnUpdateMessage(object sender, StatusBarMessageEventArgs e)
        {
            if(EUpdateMessage != null)
            {
                EUpdateMessage(sender, e);
            }
        }
    }
}
