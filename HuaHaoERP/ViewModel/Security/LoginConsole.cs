using System;

namespace HuaHaoERP.ViewModel.Security
{
    class LoginConsole
    {
        internal bool LoginAuthentication(string userName, string password)
        {
            bool flag = false;
            string OutResult;
            string sql = "select a.Permissions||'-'||a.RealName "
                + " from T_System_User a"
                + " where a.Name=@Name And a.Password=@Password And a.DeleteMark is NULL";
            OutResult = new Helper.SQLite.DBHelper().ParametersLogin(sql, userName, password);
            if (OutResult.Length > 0)
            {
                flag = true;
                Helper.DataDefinition.CommonParameters.LoginUserName = userName;
                Helper.DataDefinition.CommonParameters.Permissions = Int32.Parse(OutResult.ToString().Split('-')[0]);
                Helper.DataDefinition.CommonParameters.RealName = OutResult.ToString().Split('-')[1];
            }
            return flag;
        }
    }
}
