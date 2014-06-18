using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;

namespace HuaHaoERP.Helper.Tools
{
    class GenerateMD5
    {
        /// <summary>
        /// 计算字符串的32位md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string GetMD5_32(string str)
        {
            string result = "";
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(data);
            for (int i = 0; i < bytes.Length; i++)
            {
                result += bytes[i].ToString("x2");
            }
            return result;
        }
    }
}
