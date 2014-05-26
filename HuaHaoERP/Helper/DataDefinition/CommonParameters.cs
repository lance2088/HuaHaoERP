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


        private static string permissions;

        public static string Permissions
        {
            get { return permissions; }
            set { permissions = value; }
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

        public static string DbPassword
        {
            get { return CommonParameters.dbPassword; }
            set { CommonParameters.dbPassword = value; }
        }
    }
}
