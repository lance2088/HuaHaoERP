﻿using HuaHaoERP.Model.Order;
using HuaHaoERP.Model.ProductionManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class OutsideProcessBatchInputConsole
    {
        internal Model_ProductionManagement_OutsideProcessBatch ReadProductInfo(string ProductNumber)
        {
            Model_ProductionManagement_OutsideProcessBatch m = new Model_ProductionManagement_OutsideProcessBatch();
            string sql = "SELECT GUID,Name,Material FROM T_ProductInfo_Product WHERE NUMBER='" + ProductNumber + "' AND DELETEMARK ISNULL";
            DataSet ds = new DataSet();
            bool flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m.ProductGuid = (Guid)dr["GUID"];
                    m.ProductNumber = ProductNumber;
                    m.ProductName = dr["Name"].ToString();
                    m.Material = dr["Material"].ToString();
                }
            }
            return m;
        }

        internal Model_ProductionManagement_OutsideProcessBatch ReadProcessorsInfo(string ProcessorsNumber)
        {
            Model_ProductionManagement_OutsideProcessBatch m = new Model_ProductionManagement_OutsideProcessBatch();
            string sql = "SELECT GUID,Name FROM T_UserInfo_Processors WHERE NUMBER='" + ProcessorsNumber + "' AND DELETEMARK ISNULL";
            DataSet ds = new DataSet();
            bool flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m.ProcessorsGuid = (Guid)dr["GUID"];
                    m.ProcessorsNumber = ProcessorsNumber;
                    m.ProcessorsName = dr["Name"].ToString();
                }
            }
            return m;
        }

        internal bool InsertData(ObservableCollection<Model_ProductionManagement_OutsideProcessBatch> data, bool isOut, DateTime date, string OrderNum, string OrderRemark)
        {
            Guid OrderGuid = Guid.NewGuid();
            string DateStr = date.ToString("yyyy-MM-dd HH:mm:ss");
            string OrderType = "入单";
            if (isOut)
            {
                OrderType = "出单";
            }
            List<string> sqls = new List<string>();
            foreach (Model_ProductionManagement_OutsideProcessBatch m in data)
            {
                if (m.ProductGuid != new Guid() && m.ProcessorsGuid != new Guid())
                {
                    sqls.Add("Insert Into T_PM_ProcessSchedule(GUID,DATE,"
                                    + "ProductID,ProcessorsID,"
                                    + "Quantity,MinorInjuries,Injuries,Lose,"
                                    + "OrderType,Remark,Obligate1) "
                            + "values('" + Guid.NewGuid() + "','" + DateStr
                                    + "','" + m.ProductGuid + "','" + m.ProcessorsGuid + "',"
                                    + m.Quantity + "," + m.MinorInjuries + "," + m.Injuries + "," + m.Lose + ",'"
                                    + OrderType + "','" + m.Remark + "','" + OrderGuid + "')");
                }
            }
            if (sqls.Count > 0)
            {
                sqls.Add("Insert into T_PM_ProcessBatchInput(Guid,Number,Date,Remark,OrderType) "
                    + "values('" + OrderGuid + "','" + OrderNum + "','" + DateStr + "','" + OrderRemark + "','" + ((isOut) ? "0" : "1") + "')");
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }

        /// <summary>
        /// 编辑时读取记录明细
        /// </summary>
        /// <returns></returns>
        internal ObservableCollection<Model_ProductionManagement_OutsideProcessBatch> ReadDatas(Model_BatchInputOrder OrderData)
        {
            ObservableCollection<Model_ProductionManagement_OutsideProcessBatch> data = new ObservableCollection<Model_ProductionManagement_OutsideProcessBatch>();
            Model_ProductionManagement_OutsideProcessBatch m;
            DataSet ds = new DataSet();
            string sql = "Select * from T_PM_ProcessSchedule";
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m = new Model_ProductionManagement_OutsideProcessBatch();


                    data.Add(m);
                }
            }
            return data;
        }
    }
}