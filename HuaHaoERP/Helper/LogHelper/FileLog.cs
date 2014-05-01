using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HuaHaoERP.Helper.LogHelper
{
    static class FileLog
    {
        private static string path = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";

        internal static void ErrorLog(string log)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileStream fs = new FileStream(path + "Error.log", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            log = DateTime.Now + " \t| " + log + "\n";
            sw.Write(log);
            sw.Flush();//清空缓冲区  
            sw.Close();//关闭流  
            fs.Close();
        }

        internal static void Log(string log)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileStream fs = new FileStream(path + "Error.log", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            log = DateTime.Now + " \t| " + log + "\n";
            sw.Write(log);
            sw.Flush();//清空缓冲区  
            sw.Close();//关闭流  
            fs.Close();
        }
    }
}
