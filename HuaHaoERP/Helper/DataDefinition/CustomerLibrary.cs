using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HuaHaoERP.Helper.DataDefinition
{
    static class CustomerLibrary
    {
        static CustomerLibrary()
        {

        }

        private static DataTable supplierList;

        public static DataTable SupplierList
        {
            get 
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.SupplierConsole().GetNameList(out ds);
                return ds.Tables[0]; 
            }
            set { CustomerLibrary.supplierList = value; }
        }
    }
}
