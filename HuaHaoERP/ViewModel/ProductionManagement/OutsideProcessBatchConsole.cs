using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using HuaHaoERP.Model.ProductionManagement;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class OutsideProcessBatchConsole
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
        internal bool InsertData(ObservableCollection<Model_ProductionManagement_OutsideProcessBatch> data, bool isOut)
        {
            string OrderType = "入单";
            if(isOut)
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
                                    + "OrderType,Remark) "
                            + "values('" + Guid.NewGuid() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                    + "','" + m.ProductGuid + "','" + m.ProcessorsGuid + "',"
                                    + m.Quantity + "," + m.MinorInjuries + "," + m.Injuries + "," + m.Lose + ",'" 
                                    + OrderType + "','" + m.Remark + "')");
                }
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }
    }
}
