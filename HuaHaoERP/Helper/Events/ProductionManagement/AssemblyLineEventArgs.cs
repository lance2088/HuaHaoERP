using System;
using System.Collections.Generic;

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

        private List<Model.ProductModel> productData;

        internal List<Model.ProductModel> ProductData
        {
            get { return productData; }
            set { productData = value; }
        }
    }

}
