using System;
using System.Collections.Generic;
using System.IO;

namespace HuaHaoERP.Helper.SettingFile
{
    /// <summary>
    /// 记录流水线模块的默认显示产品GUID
    /// </summary>
    internal class AssemblyLineModule
    {
        private static string SettingFile = AppDomain.CurrentDomain.BaseDirectory + "Data\\AssemblyLineModule.data";

        internal bool Write(string Guid)
        {
            bool flag = true;
            FileStream fs = new FileStream(SettingFile, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            Guid = Guid + "\n";
            sw.Write(Guid);
            sw.Flush();//清空缓冲区  
            sw.Close();//关闭流  
            fs.Close();

            return flag;
        }

        internal void Clear()
        {
            if (File.Exists(SettingFile))
            {
                File.Delete(SettingFile);
            }
        }

        internal bool Read(out List<Guid> d)
        {
            bool flag = true;
            d = new List<Guid>();
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
                        d.Add(new Guid(line));
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
    }
}
