using HuaHaoERP.Model.Statement;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuaHaoERP.ViewModel.Statement
{
    class ProcessorsConsole
    {
        internal List<Model_Processors> ReadList(Guid processorsID, int year, int month)
        {
            List<Model_Processors> data = new List<Model_Processors>();
            DateTime start, end;
            if (month == 0)
            {
                start = Convert.ToDateTime(year + "-01-01");
                end = Convert.ToDateTime((year + 1) + "-01-01").AddSeconds(-1);
            }
            else
            {
                start = Convert.ToDateTime(year + "-" + month + "-01");
                if (month == 12)
                {
                    end = Convert.ToDateTime((year + 1) + "-01-01").AddSeconds(-1);
                }
                else
                {
                    end = Convert.ToDateTime(year + "-" + (month + 1) + "-01").AddSeconds(-1);
                }
            }
            string sql = "select a.OrderType,total(a.Quantity),total(a.MinorInjuries),total(a.Injuries),total(a.Lose),b.Name "
                + "from T_PM_ProcessSchedule a "
                + "Left Join T_ProductInfo_Product b ON a.ProductID=b.Guid "
                + "Where a.DeleteMark ISNULL "
                + "AND a.ProcessorsID='" + processorsID + "' "
                + "AND a.Date between '" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + end.ToString("yyyy-MM-dd HH:mm:ss") + "' "
                + "Group By a.ProductID,a.OrderType "
                ;
            DataSet ds = new DataSet();
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                string lastproductName = "";
                Model_Processors m = new Model_Processors();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (lastproductName != dr["Name"].ToString())
                    {
                        if (lastproductName != "")
                        {
                            m.Row = data.Count + 1;
                            data.Add(m);
                        }
                        m = new Model_Processors();
                    }
                    m.ProductName = dr["Name"].ToString();
                    if (dr["OrderType"].ToString() == "入单")
                    {
                        m.In = int.Parse(dr["total(a.Quantity)"].ToString());
                        m.InMinorInjuries = int.Parse(dr["total(a.MinorInjuries)"].ToString());
                        m.InInjuries = int.Parse(dr["total(a.Injuries)"].ToString());
                        m.InLose = int.Parse(dr["total(a.Lose)"].ToString());
                    }
                    else
                    {
                        m.Out = int.Parse(dr["total(a.Quantity)"].ToString());
                    }
                    lastproductName = m.ProductName;
                }
                m.Row = data.Count + 1;
                data.Add(m);
            }
            return data;
        }
    }
}