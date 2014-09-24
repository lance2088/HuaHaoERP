using HuaHaoERP.Helper.DataDefinition;
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
        internal bool Insert(WarehouseHalpProductModel m)
        {
            string sql = "Insert into T_Warehouse_HalfProduct(Guid,ProductID,Date,Operator,Quantity,Remark) "
                        + " values('" + Guid.NewGuid() + "','" + m.ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CommonParameters.LoginUserName + "','" + m.Quantity + "','" + m.Remark + "')";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }

        internal bool ReadDetailsList(string Search, out List<WarehouseHalpProductModel> data)
        {
            string TableName = "T_Warehouse_HalfProduct";
            data = new List<WarehouseHalpProductModel>();
            string sql = "SELECT" +
                        "	a.ProductID," +
                        "	b.Number," +
                        "	b.Name," +
                        "	b.Material," +
                        "	b.Specification," +
                        "	b.Type," +
                        "	total(a.Quantity) as Quantity " +
                        "FROM " 
                        + TableName +
                        "  a LEFT JOIN T_ProductInfo_Product b ON a.ProductID = b.GUID " +
                        "GROUP BY" +
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
