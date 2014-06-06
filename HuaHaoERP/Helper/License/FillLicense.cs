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
                Helper.DataDefinition.CommonParameters.LicenseModel = m;
            }
        }
    }
}
