using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class AssemblyLineEventArgs : EventArgs
    {
        private string registerName;

        public string RegisterName
        {
            get { return registerName; }
            set { registerName = value; }
        }
    }
}
