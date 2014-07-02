using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Collections.Generic;

namespace HuaHaoERP.Helper.SQLite
{
    class DBUpdate
    {
        private string UpdateFile = AppDomain.CurrentDomain.BaseDirectory + "Update.sql";

        internal void Update()
        {
            if (!File.Exists(UpdateFile))
            {
                return;
            }
            string sql = ReadFile();
            if (sql.Length > 0)
            {
                if (CheckSql(sql))
                {
                    new DBBackup().BackupDB();
                    string[] sqls = sql.Split(';');
                    if (new Helper.SQLite.DBHelper().Transaction(sqls))
                    {
                        File.Delete(UpdateFile);
                        MessageBox.Show("数据库更新成功。", "石蚁科技");
                        Helper.LogHelper.FileLog.Log("Update Update.sql Success.\n" + sql);
                        return;
                    }
                }
            }
            MessageBox.Show("数据库更新失败，请联系管理员。", "错误");
            Helper.LogHelper.FileLog.ErrorLog("Update Update.sql false.\n" + sql);
        }

        private bool CheckSql(string sql)
        {
            string UpperSql = sql.ToUpper();
            if (UpperSql.IndexOf("DELETE ") > 0 || UpperSql.IndexOf("DROP ") > 0)
            {
                Helper.LogHelper.FileLog.WarnLog("Check Update.sql false.\n" + sql);
                return false;
            }
            return true;
        }

        private string ReadFile()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                using (StreamReader sr = new StreamReader(UpdateFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.LogHelper.FileLog.ErrorLog("Read Update.sql false.\n" + e.ToString());
            }
            return sb.ToString();
        }
    }
}
