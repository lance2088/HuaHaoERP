using System;
using System.Runtime.InteropServices;
using System.Text;


namespace HuaHaoERP.Helper.SettingFile
{
    class INIHelper
    {
        private string _path = AppDomain.CurrentDomain.BaseDirectory + "Settings.ini";

        //声明写INI文件的API函数 
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        //声明读INI文件的API函数 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //类的构造函数，传递INI文件的路径和文件名
        internal INIHelper()
        {

        }
        internal INIHelper(string iniPath)
        {
            _path = iniPath;
        }

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _path);
        }

        /// <summary>
        /// 读取INI文件 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        internal string IniReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, _path);
            return temp.ToString();
        }
    }
}
