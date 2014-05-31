using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model.Warehouse;
using System.Data;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class WarehouseProductConsole
    {
        internal bool Add(Guid ProductID, string StaffName, int Quantity)
        {
            string sql = " Insert into T_Warehouse_Product(Guid,ProductID,Date,Operator,Number) "
                       + " values('" + Guid.NewGuid() + "','" + ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + StaffName + "','" + Quantity + "')";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }

        internal bool ReadDetailsList(out List<WarehouseProductModel> data)
        {
            data = new List<WarehouseProductModel>();
            string sql = " SELECT                                                            "
                       + "	 a.*,                                                            "
                       + "   b.Name                                                          "
                       + " FROM                                                              "
                       + "	 T_Warehouse_Product a                                           "
                       + " LEFT JOIN T_ProductInfo_Product b ON a.ProductID=b.GUID           ";
            DataSet ds = new DataSet();
            if(new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WarehouseProductModel d = new WarehouseProductModel();
                    d.Guid = (Guid)dr["GUID"];
                    d.Id = id++;
                    d.ProductID = (Guid)dr["ProductID"];
                    d.ProductName = dr["Name"].ToString();
                    d.Date = dr["Date"].ToString();
                    d.Operator = dr["Operator"].ToString();
                    d.Number = int.Parse(dr["Number"].ToString());
                    d.Remark = dr["Remark"].ToString();
                    data.Add(d);
                }
                return true;
            }
            return false;
        }

        internal bool ReadNumList(out List<WarehouseProductNumModel> data)
        {
            data = new List<WarehouseProductNumModel>();

            return false;
        }
    }
}
