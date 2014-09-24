using HuaHaoERP.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class WarehouseHalfProductConsole
    {
        internal bool ReadDetailsList(string Search, out List<WarehouseHalpProductModel> data)
        {
            string TableName = "T_Warehouse_HalfProduct";
            data = new List<WarehouseHalpProductModel>();
            string sql = "SELECT\n" +
                        "	a.ProductID,\n" +
                        "	b.Number,\n" +
                        "	b.Name,\n" +
                        "	b.Material,\n" +
                        "	b.Specification,\n" +
                        "	b.Type,\n" +
                        "	total(a.Quantity) as Quantity " +
                        "FROM " + TableName
                        +
                        "  a LEFT JOIN T_ProductInfo_Product b ON a.ProductID = b.GUID\n" +
                        "GROUP BY\n" +
                        "	a.ProductID";
            DataSet ds = new DataSet();
            decimal dd = 0m;
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WarehouseHalpProductModel d = new WarehouseHalpProductModel();
                    d.Id = id++;
                    d.Number = dr["Number"].ToString();
                    d.ProductName = dr["Name"].ToString();
                    d.Specification = dr["Specification"].ToString();
                    d.Type = dr["Type"].ToString();
                    d.Material = dr["Material"].ToString();
                    decimal.TryParse(dr["Quantity"].ToString(), out dd);
                    d.Quantity = dd;
                    data.Add(d);
                }
                return true;
            }
            return false;
        }
    }
}
