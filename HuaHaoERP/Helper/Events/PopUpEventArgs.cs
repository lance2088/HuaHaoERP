using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class PopUpEventArgs : EventArgs
    {
        private object classObject;

        internal object ClassObject
        {
            get { return classObject; }
            set { classObject = value; }
        }
    }
}
