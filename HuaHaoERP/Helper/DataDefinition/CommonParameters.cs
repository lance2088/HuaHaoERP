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
    }
}
