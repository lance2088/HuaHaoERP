using System;
using System.IO;

namespace HuaHaoERP.Helper.SQLite
{
    internal class DBBackup
    {
        private string PATH = AppDomain.CurrentDomain.BaseDirectory + "DataBackup";

        internal DBBackup()
        {
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
        }

        internal bool BackupDB()
        {
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "Data\\Data.db", PATH + "\\DataBackup" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".db");
            return true;
        }
    }
}
