using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;

namespace HuaHaoERP.Helper.Tools
{
    class TranslatePassword
    {
        public static string TranslateToString(SecureString password)
        {
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(password);
            string passwordstr = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
            return passwordstr;
        }
    }
}
