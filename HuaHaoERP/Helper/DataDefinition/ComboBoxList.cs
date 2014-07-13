using System;
using System.Collections.Generic;
using System.Data;

namespace HuaHaoERP.Helper.DataDefinition
{
    static class ComboBoxList
    {
        private static DataTable AddAll(DataSet ds,string Name)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Clone();
            DataRow dr = dt.NewRow();
            dr["GUID"] = new Guid();
            dr["Name"] = "全部" + Name;
            dt.Rows.Add(dr);
            foreach (DataRow drTemp in ds.Tables[0].Rows)
            {
                dt.Rows.Add(drTemp.ItemArray);
            }
            return dt;
        }
        public static DataTable SupplierListWithoutAll
        {
            get 
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.SupplierConsole().GetNameList(out ds);
                return ds.Tables[0]; 
            }
        }
        public static DataTable SupplierListWithAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.SupplierConsole().GetNameList(out ds);
                return AddAll(ds,"供应商");
            }
        }
        public static DataTable CustomerListWithoutAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.CustomerConsole().GetNameList(out ds);
                return ds.Tables[0]; 
            }
        }
        public static DataTable CustomerListWithAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.CustomerConsole().GetNameList(out ds);
                return AddAll(ds, "客户");
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
                return AddAll(ds, "产品");
            }
        }
        public static List<string> ProductTypeListWithAll
        {
            get
            {
                List<string> ls = new List<string>();
                ls.Add("全部类型");
                DataSet ds = new DataSet();
                new ViewModel.MeansOfProduction.ProductConsole().GetTypeList(out ds);
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    ls.Add(dr[0].ToString());
                }
                return ls;
            }
        }
        public static DataTable StaffListWithoutAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.StaffConsole().GetNameList(out ds);
                return ds.Tables[0];
            }
        }
        public static DataTable StaffListWithAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.StaffConsole().GetNameList(out ds);
                return AddAll(ds, "员工");
            }
        }
        public static DataTable ProcessorsListWithoutAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.ProcessorsConsole().GetNameList(out ds);
                return ds.Tables[0];
            }
        }
        public static DataTable ProcessorsListWithAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.Customer.ProcessorsConsole().GetNameList(out ds);
                return AddAll(ds, "抛光户");
            }
        }
        public static DataTable RawMaterialsListWithoutAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.MeansOfProduction.RawMaterialsConsole().GetNameList(out ds);
                return ds.Tables[0];
            }
        }
        public static DataTable RawMaterialsListWithAll
        {
            get
            {
                DataSet ds = new DataSet();
                new ViewModel.MeansOfProduction.RawMaterialsConsole().GetNameList(out ds);
                return AddAll(ds, "原材料");
            }
        }
    }
}
