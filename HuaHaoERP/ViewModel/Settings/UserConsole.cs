using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;
using System.Data;

namespace HuaHaoERP.ViewModel.Settings
{
    class UserConsole
    {
        internal bool Add(UserModel d)
        {
            bool flag = true;
            string sql = "Insert Into T_System_User (Name,Password,UserGroup,RealName,Permissions,Remark) "
                       + " values('" + d.Username + "','" + d.Password + "',3,'" + d.Realname + "','" + d.Permissions + "','" + d.Remark + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool Update(UserModel d)
        {
            bool flag = true;
            string sql = "update T_System_User set Password = '" + d.Password + "',RealName='" + d.Realname + "',Permissions='" + d.Permissions + "',Remark='" + d.Remark + "' where id = " + d.Id;
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool MarkDelete(UserModel d)
        {
            bool flag = true;
            string sql = "Update T_System_User Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where id=" + d.Id + "";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool ReadList(out List<UserModel> data)
        {
            bool flag = true;
            data = new List<UserModel>();
            string sql = "select * from T_UserInfo_Customer Where DeleteMark is null";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserModel d = new UserModel();
                    d.Id = Int32.Parse(dr["ID"].ToString());
                    d.Username = dr["NAME"].ToString();
                    d.Password = dr["PASSWORD"].ToString();
                    d.Realname = dr["REALNAME"].ToString();
                    d.Permissions = Int32.Parse(dr["PERMISSIONS"].ToString());
                    d.Remark = dr["REMARK"].ToString();
                    data.Add(d);
                }
            }
            return flag;
        }
    }
}
