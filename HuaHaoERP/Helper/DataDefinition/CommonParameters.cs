using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.DataDefinition
{
    class CommonParameters
    {
        private static string loginUserName;

        public static string LoginUserName
        {
            get { return CommonParameters.loginUserName; }
            set { CommonParameters.loginUserName = value; }
        }


        private static int permissions;

        public static int Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }
        private static string realName;

        public static string RealName
        {
            get { return CommonParameters.realName; }
            set { CommonParameters.realName = value; }
        }


        private static List<Guid> assemblyLineModuleShow;

        public static List<Guid> AssemblyLineModuleShow
        {
            get 
            {
                List<Guid> d = new List<Guid>();
                new SettingFile.AssemblyLineModule().Read(out d);
                return d; 
            }
            set { CommonParameters.assemblyLineModuleShow = value; }
        }

        private static string dbPassword = "";
        /// <summary>
        /// 数据库密码
        /// </summary>
        public static string DbPassword
        {
            get { return CommonParameters.dbPassword; }
            set { CommonParameters.dbPassword = value; }
        }

        private static StoneAnt.License.Model.LicenseModel licenseModel;
        /// <summary>
        /// License模块的Model
        /// </summary>
        public static StoneAnt.License.Model.LicenseModel LicenseModel
        {
            get { return CommonParameters.licenseModel; }
            set { CommonParameters.licenseModel = value; }
        }

        private static int periodOfValidity;
        /// <summary>
        /// 根据License算出来的剩余有效期
        /// </summary>
        public static int PeriodOfValidity
        {
            get { return CommonParameters.periodOfValidity; }
            set { CommonParameters.periodOfValidity = value; }
        }

        private static bool isLockAdminLogin = false;
        /// <summary>
        /// 锁定只能管理员登陆
        /// </summary>
        public static bool IsLockAdminLogin
        {
            get { return CommonParameters.isLockAdminLogin; }
            set { CommonParameters.isLockAdminLogin = value; }
        }
    }
}
