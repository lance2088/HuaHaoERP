using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HuaHaoERP.Helper.Tools
{
    class Date
    {
        public static string FormatDate(string datestring)
        {
            DateTime dt = Convert.ToDateTime(datestring);
            string format = "yyyy-MM-dd";
            return dt.ToString(format);
        }
        public static string Format(string time)
        {
            time = time.Split(' ')[0];//如果有时分秒，只取年月日
            string[] date = time.Split(new char[2] { '-', '/' });
            if (date[1].Equals("13"))//月==13，则加一年
            {
                date[0] = (int.Parse(date[0]) + 1).ToString();
                date[1] = "01";
                date[2] = "01";
            }
            else
            {
                date[1] = FormatMonth(time);
                date[2] = FormatDay(time);
            }
            return date[0] + "-" + date[1] + "-" + date[2];
        }
        /// <summary>
        /// 转换成 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FormatToF(string time)
        {
            DateTime now = new DateTime();
            DateTime.TryParse(time, out now);
            string format = "yyyy-MM-dd HH:mm:ss";
            return now.ToString(format);
        }
        /// <summary>
        /// 日期时间 转换成 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FormatToD(string time)
        {
            DateTime now = new DateTime();
            DateTime.TryParse(time, out now);
            return now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");
        }
        public static string FormatMonth(string time)
        {
            time = time.Split(' ')[0];
            string month = time.Split(new char[2] { '-', '/' })[1];
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            return month;
        }

        /// <summary>
        /// 返回日
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FormatDay(string time)
        {
            time = time.Split(' ')[0];
            string day = time.Split(new char[2] { '-', '/' })[2];
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            return day;
        }

        internal static string FormatNow()
        {
            DateTime now = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";
            return now.ToString(format);
        }
        public static bool IsStringOfDay(string input)
        {
            if (Regex.Match(input, @"^[0-3]{0,1}[0-9]{1,1}$").Success)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 验证字符串是否为浮点数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsStringOfDouble(string input)
        {
            if (Regex.Match(input, @"^[0-9]+\.?[0-9]*$").Success)
            {
                return true;
            }
            return false;
        }

        public static List<string> GetDateRank(int index)
        {
            List<string> list = new List<string>();
            string a1 = string.Empty;
            string a2 = string.Empty;
            DateTime dt = DateTime.Now;
            int w = (int)dt.DayOfWeek;
            string rex = "yyyy/MM/1";
            switch (index)
            {
                case 0:
                    a1 = dt.ToShortDateString();
                    a2 = a1;
                    break;
                case 1:
                    a1 = dt.AddDays(-1).ToShortDateString();
                    a2 = a1;
                    break;
                case 2:
                    a1 = dt.AddDays(w == 0 ? w - 7 : -(w - 1)).ToShortDateString();
                    a2 = dt.AddDays(w == 0 ? 0 : 7 - w).ToShortDateString();
                    break;
                case 3:
                    a1 = dt.AddDays(w == 0 ? w : -(w + 6)).ToShortDateString();
                    a2 = dt.AddDays(w == 0 ? 7 : 0 - w).ToShortDateString();
                    break;
                case 4:
                    a1 = dt.ToString(rex);
                    a2 = dt.ToShortDateString();
                    break;
                case 5:
                    a1 = dt.AddMonths(-1).ToString(rex);
                    a2 = dt.AddDays(-dt.Day).AddMinutes(-1).ToShortDateString();
                    break;
                case 6:
                    a1 = dt.AddMonths(-dt.Month + 1).ToString(rex);
                    a2 = dt.ToShortDateString();
                    break;
                case 7:
                    a1 = dt.AddYears(-1).AddMonths(-dt.Month + 1).ToString(rex);
                    a2 = dt.AddDays(-dt.DayOfYear).ToShortDateString();
                    break;
            }
            list.Add(a1);
            list.Add(a2);
            return list;
        }
    }
}
