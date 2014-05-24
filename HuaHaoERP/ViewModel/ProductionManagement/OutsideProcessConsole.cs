using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HuaHaoERP.Model;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class OutsideProcessConsole
    {
        internal bool Add(ProductionManagement_OutsideProcessModel d)
        {
            bool flag = false;
            string sql = "Insert into T_PM_ProcessSchedule(Guid,Date,ProductID,ProcessorsID,Quantity,MinorInjuries,Injuries,Lose,OrderType,Remark) "
                       + " values('"+d.Guid+"','"+d.OrderDate+"','"+d.ProductGuid+"','"+d.ProcessorsGuid+"','"+d.Quantity+"','"+d.MinorInjuries+"','"+d.Injuries+"','"+d.Lose+"','"+d.OrderType+"','"+d.Remark+"')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool ReadList(string OrderType, DateTime Start, DateTime End, Guid ProductID, Guid ProcessorsID, out List<ProductionManagement_OutsideProcessModel> data, out int Count)
        {
            string sql_WhereParm = "";
            if (ProductID != new Guid())
            {
                sql_WhereParm += " AND a.ProductID='" + ProductID + "' ";
            }
            if (ProcessorsID != new Guid())
            {
                sql_WhereParm += " AND a.ProcessorsID='" + ProcessorsID + "' ";
            }
            sql_WhereParm += " AND a.Date between '" + Start.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + End.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            bool flag = false;
            data = new List<ProductionManagement_OutsideProcessModel>();
            Count = 0;
            string sql = " SELECT                                                                    "
                       + "	a.*,                                                                     "
                       + "   b.Name as ProductName,                                                  "
                       + "   c.Name as ProcessorsName                                                "
                       + " FROM                                                                      "
                       + "	T_PM_ProcessSchedule a                                                   "
                       + " LEFT JOIN T_ProductInfo_Product b ON a.ProductID=b.GUID                   "
                       + " LEFT JOIN T_UserInfo_Processors c ON a.ProcessorsID=c.GUID                "
                       + " WHERE                                                                     "
                       + "	OrderType = '" + OrderType + "'                                          "
                       + sql_WhereParm;
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProductionManagement_OutsideProcessModel d = new ProductionManagement_OutsideProcessModel();
                    d.Guid = (Guid)dr["GUID"];
                    d.Id = id++;
                    d.OrderDate = Convert.ToDateTime(dr["Date"].ToString()).ToString("yyyy-MM-dd");
                    d.ProductGuid = (Guid)dr["ProductID"];
                    d.ProductName = dr["ProductName"].ToString();
                    d.ProcessorsGuid = (Guid)dr["ProcessorsID"];
                    d.ProcessorsName = dr["ProcessorsName"].ToString();
                    d.Quantity = int.Parse(dr["Quantity"].ToString());
                    Count += d.Quantity;
                    d.MinorInjuries = int.Parse(dr["MinorInjuries"].ToString());
                    d.Injuries = int.Parse(dr["Injuries"].ToString());
                    d.Lose = int.Parse(dr["Lose"].ToString());
                    d.OrderType = OrderType;
                    d.Remark = dr["Remark"].ToString();
                    data.Add(d);
                }
            }
            return flag;
        }
    }
}
