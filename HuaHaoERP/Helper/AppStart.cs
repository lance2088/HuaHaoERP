using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            new Helper.License.FillLicense().Fill(AppDomain.CurrentDomain.BaseDirectory + "License.key");
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Background"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Background");
            }
        }
    }
}
