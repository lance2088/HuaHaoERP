using System;

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
