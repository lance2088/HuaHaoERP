using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HuaHaoERP.Helper.SettingFile
{
    class AssemblyLineModule
    {
        private static string path = AppDomain.CurrentDomain.BaseDirectory + "Data\\";

        internal bool Write(string Guid)
        {
            bool flag = true;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileStream fs = new FileStream(path + "AssemblyLineModule.data", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            Guid = Guid + "\n";
            sw.Write(Guid);
            sw.Flush();//清空缓冲区  
            sw.Close();//关闭流  
            fs.Close();

            return flag;
        }

        internal bool Read()
        {
            bool flag = true;


            return flag;
        }
    }
}
