using System;
using System.Security;

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
