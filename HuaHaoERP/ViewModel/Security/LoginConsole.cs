using System;

namespace HuaHaoERP.ViewModel.Security
{
    class LoginConsole
    {
        internal bool LoginAuthentication(string UserName, string Password)
        {
            bool flag = false;
            object OutResult;//string
            string sql = "select a.Permissions||'-'||a.RealName "
                + " from T_System_User a"
                + " where a.Name='" + UserName + "' And a.Password='" + Password + "' And a.DeleteMark is NULL";
            flag = new Helper.SQLite.DBHelper().QuerySingleResult(sql, out OutResult);
            if (flag)
            {
                Helper.DataDefinition.CommonParameters.LoginUserName = UserName;
                Helper.DataDefinition.CommonParameters.Permissions = Int32.Parse(OutResult.ToString().Split('-')[0]);
                Helper.DataDefinition.CommonParameters.RealName = OutResult.ToString().Split('-')[1];
            }
            return flag;
        }
    }
}
