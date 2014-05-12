using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class ProductOrderEventArgs : EventArgs
    {
        private Model.ProductOrderModel data;

        internal Model.ProductOrderModel Data
        {
            get { return data; }
            set { data = value; }
        }
    }
    class ProductOrderEventArgsForDateGrid : EventArgs
    {
        private Model.ProductOrderModelForDataGrid data;

        internal Model.ProductOrderModelForDataGrid Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
