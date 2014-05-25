using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HuaHaoERP.ViewModel.Settings
{
    class ChangePasswordConsole
    {
        internal bool CheckPassword(string Name, string Password)
        {
            bool flag = false;
            string sql = "select Count(*) from T_System_User where Name='" + Name + "' AND Password='" + Password + "' AND DeleteMark is null";
            object o;
            flag = new Helper.SQLite.DBHelper().QuerySingleResult(sql, out o);
            return flag;
        }
    }
}
