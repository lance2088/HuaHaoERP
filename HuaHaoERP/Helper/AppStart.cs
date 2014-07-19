using System;
using System.IO;
using System.Windows;

namespace HuaHaoERP.Helper
{
    public class AppStart
    {
        public void Init()
        {
            if (CheckDB() == false)
            {
                new Helper.SQLite.DBCreate().Create();
            }
            string DBPassword = "";
            if (new SettingFile.DatabaseEncryption().Read(out DBPassword))
            {
                Helper.DataDefinition.CommonParameters.DbPassword = DBPassword;
            }
            new Helper.SQLite.DBUpdate().Update();
            new Helper.License.FillLicense().CheckLicense(AppDomain.CurrentDomain.BaseDirectory + "License.key");
        }

        /// <summary>
        /// 检查数据库文件是否存在
        /// </summary>
        /// <returns></returns>
        private bool CheckDB()
        {
            string dbfile = AppDomain.CurrentDomain.BaseDirectory + "Data\\Data.db";
            string dbpath = AppDomain.CurrentDomain.BaseDirectory + "Data";
            if (!File.Exists(dbfile))
            {
                if (!Directory.Exists(dbpath))
                {
                    Directory.CreateDirectory(dbpath);
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
