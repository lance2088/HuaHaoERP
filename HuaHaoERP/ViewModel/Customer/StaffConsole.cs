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
            string sql = "Insert Into T_Staff (GUID,Number,Name,Jobs,EntryTime,Contact,IDNumber,Remark,DepartureTime,AddTime) "
                       + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Jobs + "','" + d.EntryTime + "','" + d.Contact + "','" + d.IDNumber + "','" + d.Remark + "','" + d.DepartureTime + "','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool Delete(StaffModel d)
        {


            return false;
        }
        internal bool MarkDelete(StaffModel d)
        {


            return false;
        }
        internal bool ReadList(out List<StaffModel> data)
        {
            bool flag = true;
            data = new List<StaffModel>();
            string sql = "select * from T_Staff Where DeleteMark is null order by AddTime";
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
    }
}
