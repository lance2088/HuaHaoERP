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
            string sql = "select a.Permissions "
                + " from T_System_User a"
                + " where a.Name='" + UserName + "' And a.Password='" + Password + "' And a.DeleteMark is NULL";
            flag = new Helper.SQLite.DBHelper().QuerySingleResult(sql, out Permissions);
            if (flag)
            {
                Helper.DataDefinition.CommonParameters.LoginUserName = UserName;
                Helper.DataDefinition.CommonParameters.Permissions = Permissions.ToString();
            }
            return flag;
        }
    }
}
