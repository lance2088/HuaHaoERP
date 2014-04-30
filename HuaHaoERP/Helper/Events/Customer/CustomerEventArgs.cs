using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class CustomerEventArgs : EventArgs
    {
        private Model.CustomerModel customerData;

        internal Model.CustomerModel CustomerData
        {
            get { return customerData; }
            set { customerData = value; }
        }
    }
}
