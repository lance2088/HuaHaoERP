using System;

namespace HuaHaoERP.Helper.Tools
{
    /// <summary>
    /// 工龄
    /// </summary>
    static class Seniority
    {
        /// <summary>
        /// 未离职
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static string SeniorityForMonth(DateTime Date)
        {
            TimeSpan TSDate = new TimeSpan(Date.Ticks);
            TimeSpan TSDateNow = new TimeSpan(DateTime.Now.Date.Ticks);
            TimeSpan Ts = TSDate.Subtract(TSDateNow).Duration();
            return (Ts.Days / 30).ToString();
        }
        /// <summary>
        /// 已离职
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="Date2"></param>
        /// <returns></returns>
        public static string SeniorityForMonth(DateTime Date, DateTime Date2)
        {
            TimeSpan TSDate = new TimeSpan(Date.Ticks);
            TimeSpan TSDateNow = new TimeSpan(Date2.Ticks);
            TimeSpan Ts = TSDate.Subtract(TSDateNow).Duration();
            return (Ts.Days / 30).ToString();
        }
    }
}
