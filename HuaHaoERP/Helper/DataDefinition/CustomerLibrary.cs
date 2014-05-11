using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.DataDefinition
{
    static class CustomerLibrary
    {
        static CustomerLibrary()
        {

        }

        private static List<string> supplierList;

        public static List<string> SupplierList
        {
            get 
            { 

                return CustomerLibrary.supplierList; 
            }
            set { CustomerLibrary.supplierList = value; }
        }
    }
}
