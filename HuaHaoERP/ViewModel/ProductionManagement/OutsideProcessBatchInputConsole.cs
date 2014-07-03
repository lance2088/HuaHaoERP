using HuaHaoERP.Model.Order;
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
            string sql = "Select a.*, "
                + " b.Guid as PDGuid,b.Number as PDNumber,b.Name as PDName,b.Material, "
                + " c.Guid as PCGuid,c.Number as PCNumber,c.Name as PCName "
                + " from T_PM_ProcessSchedule a "
                + " Left join T_ProductInfo_Product b ON b.Guid=a.ProductID "
                + " Left Join T_UserInfo_Processors c ON c.Guid=a.ProcessorsID "
                + " WHERE a.Obligate1='" + OrderData.Guid + "' "
                //+ " AND a.DeleteMark ISNULL "
                //+ " AND a.OrderType='" + ((OrderData.OrderType == "0") ? "出单" : "入单") + "'"
                ;
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m = new Model_ProductionManagement_OutsideProcessBatch();

                    //Product Info
                    m.ProductGuid = (Guid)dr["PDGuid"];
                    m.ProductNumber = dr["PDNumber"].ToString();
                    m.ProductName = dr["PDName"].ToString();
                    m.Material = dr["Material"].ToString();
                    //Processors Info
                    m.ProcessorsGuid = (Guid)dr["PCGuid"];
                    m.ProcessorsNumber = dr["PCNumber"].ToString();
                    m.ProcessorsName = dr["PCName"].ToString();
                    //Input Info
                    m.Quantity = int.Parse(dr["Quantity"].ToString());
                    m.MinorInjuries = int.Parse(dr["MinorInjuries"].ToString());
                    m.Injuries = int.Parse(dr["Injuries"].ToString());
                    m.Lose = int.Parse(dr["Lose"].ToString());
                    m.Remark = dr["Remark"].ToString();

                    data.Add(m);
                }
            }
            if (data.Count < 20)
            {
                int COUNT = data.Count;
                for (int i = 0; i < 20 - COUNT; i++)
                {
                    data.Add(new Model_ProductionManagement_OutsideProcessBatch { Id = i + 1 });
                }
            }
            return data;
        }

        internal bool DeleteOld(Guid OrderGuid)
        {
            string DateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            List<string> sqls = new List<string>();
            sqls.Add("Update T_PM_ProcessBatchInput SET DeleteMark='" + DateStr + "' Where Guid='" + OrderGuid + "'");
            sqls.Add("Update T_PM_ProcessSchedule Set DeleteMark='" + DateStr + "' Where Obligate1='" + OrderGuid + "'");
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }
    }
}
