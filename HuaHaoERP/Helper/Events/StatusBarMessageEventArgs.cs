using System;

namespace HuaHaoERP.Helper.Events
{
    class StatusBarMessageEventArgs : EventArgs
    {
        private string message;

        internal string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
