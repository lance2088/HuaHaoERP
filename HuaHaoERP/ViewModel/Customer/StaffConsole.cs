using HuaHaoERP.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuaHaoERP.ViewModel.Customer
{
    class StaffConsole
    {
        private bool CheckRepeat(StaffModel d)
        {
            object oTemp;
            string sql_Repeat = "select 1 from T_UserInfo_Staff where Number='" + d.Number + "' AND DeleteMark IS NULL AND Guid <> '" + d.Guid + "'";
            return new Helper.SQLite.DBHelper().QuerySingleResult(sql_Repeat, out oTemp);
        }
        internal bool Add(StaffModel d)
        {
            if (CheckRepeat(d))
            {
                return false;
            }
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
            if (CheckRepeat(d))
            {
                return false;
            }
            if (d.DepartureTime == null)
            {
                d.DepartureTime = "0001-01-01 00:00:00";
            }
            string sql_Update = "update T_UserInfo_Staff "
                                + " SET Number='" + d.Number
                                + "',Name='" + d.Name
                                + "',Jobs='" + d.Jobs
                                + "',EntryTime='" + d.EntryTime
                                + "',Contact='" + d.Contact
                                + "',IDNumber='" + d.IDNumber
                                + "',Remark='" + d.Remark
                                + "',DepartureTime='" + d.DepartureTime
                                + "' Where GUID='" + d.Guid + "'";
            return new Helper.SQLite.DBHelper().SingleExecution(sql_Update);
        }
        internal bool MarkDelete(StaffModel d)
        {
            bool flag = true;
            string sql = "Update T_UserInfo_Staff Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool ReadList(bool ShowDeparture, out List<StaffModel> data)
        {
            string sql_Where = "";
            if (!ShowDeparture)
            {
                sql_Where += " AND DepartureTime IS '0001-01-01 00:00:00'";
            }
            bool flag = true;
            data = new List<StaffModel>();
            string sql = "select * from T_UserInfo_Staff Where DeleteMark is null " + sql_Where + " order by AddTime";
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
                    //d.Seniority = Helper.Tools.Seniority.SeniorityForMonth(Convert.ToDateTime(dr["EntryTime"]));
                    d.Contact = dr["Contact"].ToString();
                    d.IDNumber = dr["IDNumber"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    //d.DepartureTime = (Convert.ToDateTime(dr["DepartureTime"]).Year > 10) ? Convert.ToDateTime(dr["DepartureTime"]).ToString("yyyy-MM-dd") : "";
                    d.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    if (Convert.ToDateTime(dr["DepartureTime"]).Year > 10)
                    {
                        d.DepartureTime = Convert.ToDateTime(dr["DepartureTime"]).ToString("yyyy-MM-dd");
                        d.Seniority = Helper.Tools.Seniority.SeniorityForMonth(Convert.ToDateTime(dr["EntryTime"]), Convert.ToDateTime(dr["DepartureTime"]));
                    }
                    else
                    {
                        d.DepartureTime = "";
                        d.Seniority = Helper.Tools.Seniority.SeniorityForMonth(Convert.ToDateTime(dr["EntryTime"]));
                    }
                    data.Add(d);
                }
            }
            return flag;
        }
        internal bool GetNameList(out DataSet ds)
        {
            ds = new DataSet();
            string sql = "select Guid,Number,Name From T_UserInfo_Staff Where DeleteMark is null AND DepartureTime='0001-01-01 00:00:00' order by AddTime";
            return new Helper.SQLite.DBHelper().QueryData(sql, out ds);
        }
        internal bool GetNameList(string Parm, out DataSet ds)
        {
            ds = new DataSet();
            string sql = "select Guid,Number,Name From T_UserInfo_Staff "
                       + " Where (Name LIKE '%" + Parm + "%' OR Number LIKE '%" + Parm + "%') "
                       + " AND DeleteMark is null AND DepartureTime='0001-01-01 00:00:00' order by AddTime";
            return new Helper.SQLite.DBHelper().QueryData(sql, out ds);
        }
    }
}
