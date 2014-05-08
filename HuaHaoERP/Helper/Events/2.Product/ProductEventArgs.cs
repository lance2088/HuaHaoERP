using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Events
{
    class ProductEventArgs : EventArgs
    {
        private Model.ProductModel productData;

        internal Model.ProductModel ProductData
        {
            get { return productData; }
            set { productData = value; }
        }
    }
}
