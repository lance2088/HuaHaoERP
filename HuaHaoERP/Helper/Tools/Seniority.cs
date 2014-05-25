using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.Helper.Tools
{
    /// <summary>
    /// 工龄
    /// </summary>
    static class Seniority
    {
        public static string SeniorityForMonth(DateTime Date)
        {
            TimeSpan TSDate = new TimeSpan(Date.Ticks);
            TimeSpan TSDateNow = new TimeSpan(DateTime.Now.Date.Ticks);
            TimeSpan Ts = TSDate.Subtract(TSDateNow).Duration();
            return (Ts.Days/30).ToString();
        }
        public static string SeniorityForMonth(DateTime Date, DateTime Date2)
        {
            TimeSpan TSDate = new TimeSpan(Date.Ticks);
            TimeSpan TSDateNow = new TimeSpan(Date2.Ticks);
            TimeSpan Ts = TSDate.Subtract(TSDateNow).Duration();
            return (Ts.Days / 30).ToString();
        }
    }
}
