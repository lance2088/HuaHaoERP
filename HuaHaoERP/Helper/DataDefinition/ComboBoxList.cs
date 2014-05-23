using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HuaHaoERP.Helper.DataDefinition
{
    static class ComboBoxList
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
        public static DataTable ProductListWithoutAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.MeansOfProduction.ProductConsole().GetNameList(out ds);
                return ds.Tables[0];
            }
        }
        public static DataTable ProductListWithAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.MeansOfProduction.ProductConsole().GetNameList(out ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Clone();
                DataRow dr = dt.NewRow();
                dr["GUID"] = "00000000-0000-0000-0000-000000000000";
                dr["Number"] = 0;
                dr["Name"] = "全部产品";
                dt.Rows.Add(dr);
                foreach (DataRow drTemp in ds.Tables[0].Rows)
                {
                    dt.Rows.Add(drTemp.ItemArray);
                }
                return dt;
            }
        }
        public static DataTable StaffList
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.StaffConsole().GetNameList(out ds);
                return ds.Tables[0];
            }
        }
        public static DataTable ProcessorsList
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.ProcessorsConsole().GetNameList(out ds);
                return ds.Tables[0];
            }
        }
        public static DataTable RawMaterialsList
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.MeansOfProduction.RawMaterialsConsole().GetNameList(out ds);
                return ds.Tables[0];
            }
        }
    }
}
