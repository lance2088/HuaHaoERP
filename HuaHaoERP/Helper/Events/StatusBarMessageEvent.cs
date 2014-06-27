using System;

namespace HuaHaoERP.Helper.Events
{
    static class StatusBarMessageEvent
    {
        internal static EventHandler<StatusBarMessageEventArgs> EUpdateMessage;
        internal static void OnUpdateMessage(string Message)
        {
            if(EUpdateMessage != null)
            {
                StatusBarMessageEventArgs ee = new StatusBarMessageEventArgs();
                ee.Message = Message;
                EUpdateMessage(null, ee);
            }
        }
    }
}
