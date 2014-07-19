namespace HuaHaoERP.Helper.SQLite
{
    class DBCreate
    {
        internal bool Create()
        {
            string sql = Properties.Resources.DataBase;
            string[] sqls = sql.Split(';');
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }
    }
}
