using HuaHaoERP.Helper.DataDefinition;
using HuaHaoERP.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class WarehouseSparePartsInventoryConsole
    {
        //internal bool Insert(WarehouseSparePartsInventoryModel m)
        //{
        //    string sql = "Insert into T_Warehouse_SparePartsInventory(Guid,ProcessorID,ProductID,Date,Operator,Quantity,Remark) "
        //                + " values('" + Guid.NewGuid() + "','" + m.ProcessorID + "','" + m.ProductID +"','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CommonParameters.LoginUserName + "','" + m.Quantity + "','" + m.Remark + "')";
        //    return new Helper.SQLite.DBHelper().SingleExecution(sql);
        //}

        internal bool ReadDetailsList(string Search,Guid ProcessorsID, out List<WarehouseSparePartsInventoryModel> data)
        {
            string TableName = "T_Warehouse_SparePartsInventory";
            string sql_WhereParm = string.Empty;
            if (!string.IsNullOrEmpty(Search))
            {
                sql_WhereParm += " and  (b.Number like '%" +
                        Search + "%' or b.Name like '%" + Search + "%')";
            }
            if (ProcessorsID != new Guid())
            {
                sql_WhereParm += " AND a.ProcessorsID='" + ProcessorsID + "' ";
            }
            data = new List<WarehouseSparePartsInventoryModel>();
            string sql = "SELECT" +
                        "	a.ProductID," +
                        "	c.Name as ProcessorName,b.Number," +
                        "	b.Name," +
                        "	b.Material," +
                        "	b.Specification," +
                        "	b.Type," +
                        "	total(a.Quantity) as Quantity " +
                        "FROM "
                        + TableName +
                        "  a LEFT JOIN T_ProductInfo_Product b ON a.ProductID = b.GUID left join T_UserInfo_Processors c on a.ProcessorID = c.Guid where a.deleteMark is null " + sql_WhereParm +
                        " GROUP BY" +
                        "	a.ProcessorID,a.ProductID";
            DataSet ds = new DataSet();
            decimal dd = 0m;
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WarehouseSparePartsInventoryModel d = new WarehouseSparePartsInventoryModel();
                    d.Id = id++;
                    d.ProcessorName = dr["ProcessorName"].ToString();
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
