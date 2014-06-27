
namespace HuaHaoERP.ViewModel.Settings
{
    class ChangePasswordConsole
    {
        internal bool CheckPassword(string Name, string Password)
        {
            bool flag = false;
            string sql = "select * from T_System_User where Name='" + Name + "' AND Password='" + Password + "' AND DeleteMark is null";
            object o;
            flag = new Helper.SQLite.DBHelper().QuerySingleResult(sql, out o);
            return flag;
        }

        internal bool ChangePassword(string Name, string Password)
        {
            bool flag = false;
            string sql = "UPDATE T_System_User SET Password='" + Password + "' WHERE name='" + Name + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
    }
}
