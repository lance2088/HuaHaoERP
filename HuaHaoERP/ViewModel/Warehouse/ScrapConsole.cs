using HuaHaoERP.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class ScrapConsole
    {
        internal bool Add(Model.ScrapModel d)
        {
            bool flag = true;
            string sql = "Insert into T_Warehouse_Scrap(Guid,Name,Date,Operator,Number,Remark) "
                        + "values('" + d.Guid + "','" + d.Name + "','" + d.Date + "','" + d.Operator + "','" + d.Number + "','" + d.Remark + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool ReadList(string args,out List<ScrapModel> data)
        {
            args = args.Equals("全部") ? "" : "where Name='" + args + "'";
            bool flag = true;
            data = new List<ScrapModel>();
            string sql = "select GUID,Number,Name,Remark,Operator,strftime(Date) as Date from T_Warehouse_Scrap " + args + " order by Date";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScrapModel d = new ScrapModel();
                    d.Guid = (Guid)dr["GUID"];
                    d.Id = id++;
                    d.Date = dr["Date"].ToString().Split(' ')[0];
                    d.Number = dr["Number"].ToString();
                    d.Name = dr["Name"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    d.Operator = dr["Operator"].ToString();
                    data.Add(d);
                }
            }
            return flag;
        }

        internal decimal GetAmountByName(string args)
        {
            args = args.Equals("全部") ? "" : "where Name='" + args + "'";
            string sql = "select total(Number) from T_Warehouse_Scrap " + args;
            object d = 0;
            new Helper.SQLite.DBHelper().QuerySingleResult(sql, out d);
            return decimal.Parse(d.ToString());
        }
            
        internal List<string> GetName(bool bol)
        {
            List<string> list = new List<string>();
            DataSet ds = new DataSet();
            list.Add(bol ? "全部" : "请选择");
            string sql = "select distinct Name from T_Warehouse_Scrap";
            bool flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (ds != null)
            {
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    list.Add(d[0].ToString());
                }
            }
            return list;
        }
    }
}
