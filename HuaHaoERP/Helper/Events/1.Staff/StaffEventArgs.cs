using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class StaffEventArgs : EventArgs
    {
        private Model.StaffModel staffData;

        internal Model.StaffModel StaffData
        {
            get { return staffData; }
            set { staffData = value; }
        }
    }
}
