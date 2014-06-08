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
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "License.key"))
            {
                new Helper.License.FillLicense().Fill(AppDomain.CurrentDomain.BaseDirectory + "License.key");
            }
            else
            {
                Helper.DataDefinition.CommonParameters.PeriodOfValidity = -1;
                Helper.DataDefinition.CommonParameters.LicenseModel = new StoneAnt.License.Model.LicenseModel();
            }
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Background"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Background");
            }
        }
    }
}
