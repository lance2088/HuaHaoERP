using System;
using System.IO;
using System.Text;
using System.Windows;

namespace HuaHaoERP.Helper.SQLite
{
    class DBUpdate
    {
        private string UpdateFile = AppDomain.CurrentDomain.BaseDirectory + "Update.sql";

        internal void Update()
        {
            if(!File.Exists(UpdateFile))
            {
                return;
            }
            string sql = ReadFile();
            if(sql.Length > 0)
            {
                if(CheckSql(sql))
                {
                    if(new Helper.SQLite.DBHelper().SingleExecution(sql))
                    {
                        File.Delete(UpdateFile);
                        MessageBox.Show("数据库更新成功。", "石蚁科技");
                        return;
                    }
                }
            }
            MessageBox.Show("数据库更新失败，请联系管理员。","错误");
        }

        private bool CheckSql(string sql)
        {
            string UpperSql = sql.ToUpper();
            if(UpperSql.IndexOf("DELETE") > 0 || UpperSql.IndexOf("DROP") > 0)
            {
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
