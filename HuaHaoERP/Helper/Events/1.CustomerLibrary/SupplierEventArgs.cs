using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class SupplierEventArgs : EventArgs
    {
        private Model.SupplierModel supplierData;

        internal Model.SupplierModel SupplierData
        {
            get { return supplierData; }
            set { supplierData = value; }
        }
    }
}
