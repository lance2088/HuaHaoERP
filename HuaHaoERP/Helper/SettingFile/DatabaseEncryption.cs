using System;
using System.IO;

namespace HuaHaoERP.Helper.SettingFile
{
    /// <summary>
    /// 数据库加密密码
    /// </summary>
    class DatabaseEncryption
    {
        private static string SettingFile = AppDomain.CurrentDomain.BaseDirectory + "Data\\Encryption.data";

        internal bool Read(out string Password)
        {
            bool flag = true;
            Password = "";
            if (!File.Exists(SettingFile))
            {
                return false;
            }
            try
            {
                using (StreamReader sr = new StreamReader(SettingFile))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Password = line;
                    }
                }
            }
            catch (Exception e)
            {
                flag = false;
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return flag;
        }

        internal void Write(string str)
        {
            FileStream fs = new FileStream(SettingFile, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string asd = str + "\n";
            sw.Write(asd);
            sw.Flush();//清空缓冲区  
            sw.Close();//关闭流  
            fs.Close();
        }

        internal void Clear()
        {
            if (File.Exists(SettingFile))
            {
                File.Delete(SettingFile);
            }
        }
    }
}
