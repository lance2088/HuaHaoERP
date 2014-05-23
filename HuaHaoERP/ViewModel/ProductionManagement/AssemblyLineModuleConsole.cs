﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class AssemblyLineModuleConsole
    {
        internal bool Add(Model.AssemblyLineModuleProcessModel d)
        {
            bool flag = true;
            string sql = "Insert into T_PM_ProductionSchedule(Guid,Date,StaffID,ProductID,Process,Number) "
                        + "values('" + d.Guid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + d.StaffID + "','" + d.ProductID + "','" + d.Process + "'," + d.Quantity + ")";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool ReadList(Guid ProductGuid, out Model.AssemblyLineModuleModel d)
        {
            bool flag = true;
            bool isFirstLine = true;
            d = new Model.AssemblyLineModuleModel();
            List<Model.AssemblyLineModuleProcessModel> ProcessList = new List<Model.AssemblyLineModuleProcessModel>();
            d.Guid = ProductGuid;
            string sql = " select "
                       + "   a.Name,"
                       + "   a.P1,"
                       + "   a.P2,"
                       + "   a.P3,"
                       + "   a.P4,"
                       + "   a.P5,"
                       + "   a.P6,"
                       + "   b.Process,"
                       + "   total(b.Number) as Quantity "
                       + " from T_ProductInfo_Product a "
                       + " LEFT JOIN T_PM_ProductionSchedule b "
                       + "   ON b.ProductID = a.GUID "
                       + " where a.GUID='" + ProductGuid + "' "
                       + " GROUP BY b.Process";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if(flag)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    d.Name = dr["Name"].ToString();
                    Model.AssemblyLineModuleProcessModel dp = new Model.AssemblyLineModuleProcessModel();
                    if(isFirstLine)
                    {
                        InitProcessList(dr, ref ProcessList);
                        isFirstLine = false;
                    }
                    for (int i = 0; i < ProcessList.Count; i++)
                    {
                        if (ProcessList[i].Process == dr["Process"].ToString())
                        {
                            ProcessList[i].Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                        }
                    }
                }
                CalculateProcessList(ref ProcessList);
                d.ProcessList = ProcessList;
            }
            return flag;
        }
        private void InitProcessList(DataRow dr, ref List<Model.AssemblyLineModuleProcessModel> d)
        {
            for (int i = 1; i < 7; i++)
            {
                if (dr["P" + i].ToString() != "无")
                {
                    Model.AssemblyLineModuleProcessModel dp = new Model.AssemblyLineModuleProcessModel();
                    dp.Process = dr["P" + i].ToString();
                    dp.Quantity = 0;
                    d.Add(dp);
                }
            }
        }
        /// <summary>
        /// 处理工序数量相减
        /// </summary>
        /// <param name="d"></param>
        private void CalculateProcessList(ref List<Model.AssemblyLineModuleProcessModel> d)
        {
            for (int i = 0; i < d.Count - 1; i++)
            {
                d[i].Quantity -= d[i + 1].Quantity;
            }
        }
    }
}