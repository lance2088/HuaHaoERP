using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HuaHaoERP.Helper.License
{
    class FillLicense
    {
        public void Fill(string LicenseFile)
        {
            string KeyInDB = "";
            Helper.DataDefinition.CommonParameters.LicenseModel = new StoneAnt.License.Model.LicenseModel();
            
            if (new ViewModel.Security.LicenseConsole().ReadKeyFromDB(out KeyInDB))//数据库存在key
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "License.key"))//存在license文件
                {
                    StoneAnt.License.Model.LicenseModel m = new StoneAnt.License.Model.LicenseModel();
                    if (new StoneAnt.License.Verify.Term().VerfyLicense(LicenseFile, out m))//验证License文件
                    {
                        if (KeyInDB == m.Key)//验证License文件与数据库Key
                        {
                            if (new PCRegister().CheckRegistrationInformation())//验证系统信息
                            {
                                Helper.DataDefinition.CommonParameters.LicenseModel = m;
                                CalculatePeriodOfValidity();
                                return;
                            }
                            else//许可匹配，电脑不匹配
                            {
                                Helper.DataDefinition.CommonParameters.IsLockApp = true;
                                System.Windows.MessageBox.Show("感谢使用石蚁科技ERP产品，请支持正版。\n请使用管理员帐号登陆并导入许可文件以继续使用。", "代码：004");
                            }
                        }
                        else//许可与DB数据不匹配
                        {
                            System.Windows.MessageBox.Show("许可有误，请联系开发商，错误代码：003", "错误");
                        }
                    }
                    else//许可验证不通过
                    {
                        System.Windows.MessageBox.Show("许可损坏，请联系开发商，错误代码：002", "错误");
                    }
                }
                else//找不到许可文件
                {
                    System.Windows.MessageBox.Show("许可丢失，请联系开发商，错误代码：001", "错误");
                }
                Helper.DataDefinition.CommonParameters.IsLockAdminLogin = true;
                Helper.DataDefinition.CommonParameters.PeriodOfValidity = -1;
            }
        }

        /// <summary>
        /// 计算许可剩余时间
        /// </summary>
        public void CalculatePeriodOfValidity()
        {
            if(Helper.DataDefinition.CommonParameters.LicenseModel.PeriodOfValidity > 0)
            {
                Helper.DataDefinition.CommonParameters.PeriodOfValidity = Helper.DataDefinition.CommonParameters.LicenseModel.PeriodOfValidity 
                                                                        - DateTime.Now.Subtract(Helper.DataDefinition.CommonParameters.LicenseModel.CreationDate).Days;
            }
        }
    }
}
