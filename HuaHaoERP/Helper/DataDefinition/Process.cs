using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.DataDefinition
{
    static class Process
    {
        public static List<string> ProcessList
        {
            get 
            {
                List<string> ProcessList = new List<string>();
                ProcessList.Add("无");
                ProcessList.Add("冲版");
                ProcessList.Add("拉伸");
                ProcessList.Add("冲孔");
                ProcessList.Add("卷边");
                ProcessList.Add("抛光");
                return ProcessList; 
            }
        }
        public static List<string> ProcessListWithAll
        {
            get
            {
                List<string> ProcessList = new List<string>();
                ProcessList.Add("全部工序");
                ProcessList.Add("冲版");
                ProcessList.Add("拉伸");
                ProcessList.Add("冲孔");
                ProcessList.Add("卷边");
                ProcessList.Add("抛光");
                return ProcessList;
            }
        }

        public static List<string> FiveProcessList
        {
            get
            {
                List<string> ProcessList = new List<string>();
                ProcessList.Add("冲版");
                ProcessList.Add("拉伸");
                ProcessList.Add("冲孔");
                ProcessList.Add("卷边");
                ProcessList.Add("抛光");
                return ProcessList;
            }
        }
    }
}
