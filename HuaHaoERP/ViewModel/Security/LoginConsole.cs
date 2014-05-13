using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.ViewModel.Security
{
    class LoginConsole
    {
        internal bool LoginAuthentication(string UserName, string Password)
        {
            bool flag = false;
            object Permissions;//string
            string sql = "select b.Permissions "
                + " from T_System_User a LEFT JOIN T_System_UserGroup b ON  a.UserGroup=b.ID "
                + " where a.Name='"+UserName+"' And a.Password='"+Password+"' And a.DeleteMark is NULL";
            flag = new Helper.SQLite.DBHelper().QuerySingleResult(sql, out Permissions);
            return flag;
        }
    }
}
