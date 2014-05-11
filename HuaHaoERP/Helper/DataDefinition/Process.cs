using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.DataDefinition
{
    static class Process
    {
        static Process()
        {
            ProcessList = new List<string>();
            ProcessList.Add("无");
            ProcessList.Add("冲版");
            ProcessList.Add("拉伸");
            ProcessList.Add("冲孔");
            ProcessList.Add("卷边");
            ProcessList.Add("抛光");
        }

        private static List<string> processList;

        public static List<string> ProcessList
        {
            get { return Process.processList; }
            set { Process.processList = value; }
        }
    }
}
