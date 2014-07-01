using System;
using System.IO;

namespace HuaHaoERP.Helper
{
    public class AppStart
    {
        public void Init()
        {
            string DBPassword = "";
            if(new SettingFile.DatabaseEncryption().Read(out DBPassword))
            {
                Helper.DataDefinition.CommonParameters.DbPassword = DBPassword;
            }
            new Helper.SQLite.DBUpdate().Update();
            new Helper.License.FillLicense().CheckLicense(AppDomain.CurrentDomain.BaseDirectory + "License.key");
        }
    }
}
