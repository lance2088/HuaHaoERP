using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.License
{
    class FillLicense
    {
        public void Fill(string LicenseFile)
        {
            StoneAnt.License.Model.LicenseModel m = new StoneAnt.License.Model.LicenseModel();
            if (new StoneAnt.License.Verify.Term().VerfyLicense(LicenseFile, out m))
            {
                string KeyInDB = "";
                if(new ViewModel.Security.LicenseConsole().ReadKeyFromDB(out KeyInDB))
                {
                    if (KeyInDB == m.Key)
                    {
                        Helper.DataDefinition.CommonParameters.LicenseModel = m;
                        CalculatePeriodOfValidity();
                    }
                    else
                    {
                        Helper.DataDefinition.CommonParameters.PeriodOfValidity = -1;
                        Helper.DataDefinition.CommonParameters.LicenseModel = new StoneAnt.License.Model.LicenseModel();
                        System.Windows.MessageBox.Show("许可有误，请联系开发商", "错误");
                    }
                }
                else
                {
                    Helper.DataDefinition.CommonParameters.PeriodOfValidity = -1;
                    Helper.DataDefinition.CommonParameters.LicenseModel = new StoneAnt.License.Model.LicenseModel();
                    System.Windows.MessageBox.Show("许可有误，请联系开发商", "错误");
                }
            }
            else
            {
                Helper.DataDefinition.CommonParameters.PeriodOfValidity = -1;
                Helper.DataDefinition.CommonParameters.LicenseModel = new StoneAnt.License.Model.LicenseModel();
                System.Windows.MessageBox.Show("许可有误，请联系开发商", "错误");
            }
        }

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
