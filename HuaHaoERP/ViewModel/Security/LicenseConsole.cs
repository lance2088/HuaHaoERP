
namespace HuaHaoERP.ViewModel.Security
{
    class LicenseConsole
    {
        internal bool RegisterInDB(string Key)
        {
            string sql = "UPDATE T_System_Settings SET Value='" + Key + "' WHERE ID=1 AND Key='License'";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }

        internal bool ReadKeyFromDB(out string Key)
        {
            object oKey;
            string sql = "Select Value From T_System_Settings WHERE ID=1 AND Key='License'";
            bool flag = new Helper.SQLite.DBHelper().QuerySingleResult(sql, out oKey);
            Key = oKey.ToString();
            return flag;
        }
    }
}
