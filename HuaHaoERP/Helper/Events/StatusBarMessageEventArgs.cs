using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
