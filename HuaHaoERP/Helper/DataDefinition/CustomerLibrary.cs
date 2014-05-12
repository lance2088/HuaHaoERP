using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HuaHaoERP.Helper.DataDefinition
{
    static class CustomerLibrary
    {
        public static DataTable SupplierList
        {
            get 
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.SupplierConsole().GetNameList(out ds);
                return ds.Tables[0]; 
            }
        }
        public static DataTable CustomerList
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.CustomerConsole().GetNameList(out ds);
                return ds.Tables[0]; 
            }
        }
        public static DataTable ProductList
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.MeansOfProduction.ProductConsole().GetNameList(out ds);
                return ds.Tables[0];
            }
        }
    }
}
