using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HuaHaoERP.Helper.License
{
    class PCRegister
    {
        private static string SettingFile = AppDomain.CurrentDomain.BaseDirectory + "Data\\";

        /// <summary>
        /// 计算机信息注册到文件
        /// </summary>
        /// <returns></returns>
        internal bool Register()
        {
            DeleteFile();
            //MAC
            List<string> Macs = new Tools.ComputerInfo().Macs;
            foreach (string str in Macs)
            {
                Write("M", Tools.GenerateMD5.GetMD5_32(str + "StoneAnt.HSH MM"));//MAC
            }
            //Disk
            List<string> Disk = new Tools.ComputerInfo().DiskSerialNumber;
            foreach (string str in Disk)
            {
                Write("D", Tools.GenerateMD5.GetMD5_32(str + "StoneAnt.HSH DOD"));//DISK
            }
            //Cpu
            Write("C", Tools.GenerateMD5.GetMD5_32(new Tools.ComputerInfo().CpuID + "StoneAnt.HSH C.C"));//CPU
            return true;
        }

        /// <summary>
        /// 检查PC是否为注册
        /// </summary>
        /// <returns></returns>
        internal bool CheckRegistrationInformation()
        {
            bool flag = false;
            List<string> ReadResult = new List<string>();
            //MAC
            if (File.Exists(SettingFile + "M.HSH"))
            {
                Read(SettingFile + "M.HSH", out ReadResult);
                foreach (string str in ReadResult)
                {
                    foreach(string s in new Tools.ComputerInfo().Macs)
                    {
                        if (str == Tools.GenerateMD5.GetMD5_32(s + "StoneAnt.HSH MM"))
                        {
                            flag = true;
                        }
                    }
                }
            }
            //Disk
            if (!flag)
            {
                if (File.Exists(SettingFile + "D.HSH"))
                {
                    Read(SettingFile + "D.HSH", out ReadResult);
                    foreach (string str in ReadResult)
                    {
                        foreach (string s in new Tools.ComputerInfo().DiskSerialNumber)
                        {
                            if (str == Tools.GenerateMD5.GetMD5_32(s + "StoneAnt.HSH DOD"))
                            {
                                flag = true;
                            }
                        }
                    }
                }
            }
            //Cpu
            if (!flag)
            {
                if (File.Exists(SettingFile + "C.HSH"))
                {
                    Read(SettingFile + "C.HSH", out ReadResult);
                    foreach (string str in ReadResult)
                    {
                        if (Tools.GenerateMD5.GetMD5_32(new Helper.Tools.ComputerInfo().CpuID + "StoneAnt.HSH C.C") == str)
                        {
                            flag = true;
                        }
                    }
                }
            }
            return flag;
        }

        private void Write(string Type, string str)
        {
            FileStream fs = new FileStream(SettingFile + Type + ".HSH", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            string asd = str + "\n";
            sw.Write(asd);
            sw.Flush();//清空缓冲区  
            sw.Close();//关闭流  
            fs.Close();
        }
        private bool Read(string HSHFile, out List<string> d)
        {
            bool flag = true;
            d = new List<string>();
            if (!File.Exists(HSHFile))
            {
                return false;
            }
            try
            {
                using (StreamReader sr = new StreamReader(HSHFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        d.Add(line);
                    }
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
        private void DeleteFile()
        {
            if (File.Exists(SettingFile + "C.HSH"))
            {
                File.Delete(SettingFile + "C.HSH");
            }
            if (File.Exists(SettingFile + "M.HSH"))
            {
                File.Delete(SettingFile + "M.HSH");
            }
            if (File.Exists(SettingFile + "D.HSH"))
            {
                File.Delete(SettingFile + "D.HSH");
            }
        }
    }
}
