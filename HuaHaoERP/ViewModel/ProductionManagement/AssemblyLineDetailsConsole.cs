﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HuaHaoERP.Model.ProductionManagement;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class AssemblyLineDetailsConsole
    {
        internal bool ReadList(out List<AssemblyLineDetailsListModel> data)
        {
            Guid LastGuid = new Guid();
            AssemblyLineDetailsListModel LastD = new AssemblyLineDetailsListModel();
            List<string> ProcessList = new List<string>();

            data = new List<AssemblyLineDetailsListModel>();
            string sql = " SELECT                                                             "
                       + "    a.ProductID,                                                    "
                       + "    b.Number,                                                         "
                       + "    b.Name,                                                          "
                       + "    b.P1,                                                            "
                       + " 	  b.P2,                                                           "
                       + " 	  b.P3,                                                           "
                       + " 	  b.P4,                                                           "
                       + " 	  b.P5,                                                           "
                       + " 	  b.P6,                                                           "
                       + " 	  a.Process,                                                      "
                       + " 	  total(a.Number),                                                "
                       + "    total(a.Break)                                                   "
                       + " FROM                                                               "
                       + " 	  T_ProductInfo_Product b                                       "
                       + " left join T_PM_ProductionSchedule a ON a.ProductID=b.Guid            "
                       + " WHERE b.DeleteMark IS NULL "
                       + " GROUP BY                                                           "
                       + " 	  a.ProductID,                                                    "
                       + " 	  a.Process                                                       "
                       ;
            DataSet ds = new DataSet();
            if(new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (LastGuid != (Guid)dr["ProductID"])//新的Product，新建
                    {
                        if (LastGuid != new Guid())
                        {
                            CalculateProcessList(ProcessList, LastGuid, ref LastD);
                            data.Add(LastD);
                        }
                        ProcessList.Clear();
                        for (int i = 1; i < 7; i++)
                        {
                            ProcessList.Add(dr["P"+i].ToString());
                        }
                        AssemblyLineDetailsListModel d = new AssemblyLineDetailsListModel();
                        LastGuid = (Guid)dr["ProductID"];
                        LastD = d;
                        LastD.Id = id++;
                        LastD.ProductID = (Guid)dr["ProductID"];
                        LastD.ProductNumber = dr["Number"].ToString();
                        LastD.ProductName = dr["Name"].ToString();
                        for (int i = 1; i < 7; i++)
                        {
                            if (dr["Process"].ToString() == dr["P" + i].ToString())
                            {
                                SetPNum(i, int.Parse(dr["total(a.Number)"].ToString()), int.Parse(dr["total(a.Break)"].ToString()), ref LastD);
                            }
                        }
                    }
                    else//旧的Product，累加
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            if (dr["Process"].ToString() == dr["P" + i].ToString())
                            {
                                SetPNum(i, int.Parse(dr["total(a.Number)"].ToString()), int.Parse(dr["total(a.Break)"].ToString()), ref LastD);
                            }
                        }
                    }
                }
                if (ds.Tables[0].Rows.Count != 0)
                {
                    CalculateProcessList(ProcessList, LastGuid, ref LastD);
                    data.Add(LastD);
                }
                return true;
            }

            return false;
        }

        private void SetPNum(int p,int Quantity,int BreakNum, ref AssemblyLineDetailsListModel d)
        {
            switch (p)
            {
                case 1:
                    d.P1Num = Quantity;
                    break;
                case 2:
                    d.P2Num = Quantity;
                    break;
                case 3:
                    d.P3Num = Quantity;
                    break;
                case 4:
                    d.P4Num = Quantity;
                    break;
                case 5:
                    d.P5Num = Quantity;
                    break;
                case 6:
                    d.P6Num = Quantity;
                    break;
            }
        }

        /// <summary>
        /// 处理外加工数量相减
        /// </summary>
        /// <param name="d"></param>
        private void CalculateProcessList(List<string> ProcessList, Guid ProductGuid, ref AssemblyLineDetailsListModel d)
        {
            int Out = 0, In = 0;
            int Break = 0;
            string sql = "select total(Quantity),total(MinorInjuries+Injuries+Lose) as Break,OrderType from T_PM_ProcessSchedule where ProductID='" + ProductGuid + "' GROUP BY OrderType ";
            DataSet ds = new DataSet();
            new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["OrderType"].ToString() == "入单")
                {
                    In = int.Parse(dr["total(Quantity)"].ToString());
                    Break = int.Parse(dr["Break"].ToString());
                }
                else if (dr["OrderType"].ToString() == "出单")
                {
                    Out = int.Parse(dr["total(Quantity)"].ToString());
                }
            }
            for (int i = 0; i < 6; i++ )
            {
                if(ProcessList[i] == "抛光")
                {
                    switch (i + 1)
                    {
                        case 1:
                            d.P1Num += In;
                            break;
                        case 2:
                            d.P1Num -= Out;
                            d.P2Num += In;
                            break;
                        case 3:
                            d.P2Num -= Out;
                            d.P3Num += In;
                            break;
                        case 4:
                            d.P3Num -= Out;
                            d.P4Num += In;
                            break;
                        case 5:
                            d.P4Num -= Out;
                            d.P5Num += In;
                            break;
                        case 6:
                            d.P5Num -= Out;
                            d.P6Num += In;
                            break;
                    }
                }
            }
        }
    }
}
