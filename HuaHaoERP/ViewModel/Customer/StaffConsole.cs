using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;
using System.Data;

namespace HuaHaoERP.ViewModel.Customer
{
    class StaffConsole
    {
        internal bool Add(StaffModel d)
        {
            if(d.DepartureTime == null)
            {
                d.DepartureTime = "0001-01-01 00:00:00";
            }
            bool flag = true;
            string sql = "Insert Into T_UserInfo_Staff (GUID,Number,Name,Jobs,EntryTime,Contact,IDNumber,Remark,DepartureTime,AddTime) "
                       + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Jobs + "','" + d.EntryTime + "','" + d.Contact + "','" + d.IDNumber + "','" + d.Remark + "','" + d.DepartureTime + "','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool Update(StaffModel d)
        {
            bool flag = true;
            List<string> sqls = new List<string>();
            string sql_Delete = "Delete From T_UserInfo_Staff Where GUID='" + d.Guid + "'";
            if (d.DepartureTime == null)
            {
                d.DepartureTime = "0001-01-01 00:00:00";
            }
            string sql_Update = "Insert Into T_UserInfo_Staff (GUID,Number,Name,Jobs,EntryTime,Contact,IDNumber,Remark,DepartureTime,AddTime) "
                                + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Jobs + "','" + d.EntryTime + "','" + d.Contact + "','" + d.IDNumber + "','" + d.Remark + "','" + d.DepartureTime + "','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            sqls.Add(sql_Delete);
            sqls.Add(sql_Update);
            flag = new Helper.SQLite.DBHelper().Transaction(sqls);
            return flag;
        }
        internal bool MarkDelete(StaffModel d)
        {
            bool flag = true;
            string sql = "Update T_UserInfo_Staff Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool ReadList(out List<StaffModel> data)
        {
            bool flag = true;
            data = new List<StaffModel>();
            string sql = "select * from T_UserInfo_Staff Where DeleteMark is null order by AddTime";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    StaffModel d = new StaffModel();
                    d.Guid = (Guid)dr["GUID"];
                    d.Id = id++;
                    d.Number = dr["Number"].ToString();
                    d.Name = dr["Name"].ToString();
                    d.Jobs = dr["Jobs"].ToString();
                    d.EntryTime = Convert.ToDateTime(dr["EntryTime"]).ToString("yyyy-MM-dd");
                    d.Contact = dr["Contact"].ToString();
                    d.IDNumber = dr["IDNumber"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    d.DepartureTime = (Convert.ToDateTime(dr["DepartureTime"]).Year > 10) ? Convert.ToDateTime(dr["DepartureTime"]).ToString("yyyy-MM-dd") : "";
                    d.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    data.Add(d);
                }
            }
            return flag;
        }
        internal bool GetNameList(out DataSet ds)
        {
            bool flag = true;
            ds = new DataSet();
            string sql = "select Guid,Number,Name From T_UserInfo_Staff Where DeleteMark is null order by AddTime";
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            return flag;
        }
    }
}
