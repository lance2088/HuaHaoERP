﻿using HuaHaoERP.Helper.DataDefinition;
using HuaHaoERP.Model;
using System;
using System.Collections.Generic;
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
            string sql = "select *,case when permissions=0 then '仓库记录员' when permissions=1 then '流水线记录员' else '软件记录员'  end as displayPermissions from T_System_User Where DeleteMark is null and Permissions not in (9,8)";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserModel d = new UserModel();
                    d.Rowid = id;
                    id++;
                    d.Id = Int32.Parse(dr["ID"].ToString());
                    d.Username = dr["NAME"].ToString();
                    d.Password = dr["PASSWORD"].ToString();
                    d.Realname = dr["REALNAME"].ToString();
                    d.Permissions = Int32.Parse(dr["PERMISSIONS"].ToString());
                    d.Remark = dr["REMARK"].ToString();
                    d.DisplayPermissions = dr["DISPLAYPERMISSIONS"].ToString();
                    data.Add(d);
                }
            }
            return flag;
        }

        internal List<UserModel> GetComboBoxPermissions()
        {
            List<UserModel> list = new List<UserModel>();
            UserModel m = new UserModel();
            m.Permissions = -1;
            m.DisplayPermissions = "请选择";
            list.Add(m);
            m = new UserModel();
            m.Permissions = (int)ENUM.ENUM_PERMISSIONS.仓库记录员;
            m.DisplayPermissions = "仓库记录员";
            list.Add(m);
            m = new UserModel();
            m.Permissions = (int)ENUM.ENUM_PERMISSIONS.流水线记录员;
            m.DisplayPermissions = "流水线记录员";
            list.Add(m);
            m = new UserModel();
            m.Permissions = (int)ENUM.ENUM_PERMISSIONS.软件记录员;
            m.DisplayPermissions = "软件记录员";
            list.Add(m);
            return list;
        }

        internal bool ValidateUserName(string username)
        {
            string sql = "select 1 from T_System_User where Name='" + username + "' and DeleteMark is null";
            object result = new object();
            return new Helper.SQLite.DBHelper().QuerySingleResult(sql, out result);
        }
    }
}
