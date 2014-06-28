using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HuaHaoERP.Model.ProductionManagement;
using System.Collections.ObjectModel;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class AssemblyLineModuleBatchInputConsole
    {
        internal Model_AssemblyLineModuleBatchInput ReadProductInfo(string Number)
        {
            Model_AssemblyLineModuleBatchInput m = new Model_AssemblyLineModuleBatchInput();
            string sql = "SELECT GUID,Name,P1,P2,P3,P4,P5,P6 FROM T_ProductInfo_Product WHERE NUMBER='" + Number + "' AND DELETEMARK ISNULL";
            DataSet ds = new DataSet();
            bool flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m.ProductGuid = (Guid)dr["GUID"];
                    m.ProductNumber = Number;
                    m.ProductName = dr["Name"].ToString();
                    for (int i = 0; i < 6; i++)
                    {
                        if (dr["P" + (i + 1)].ToString() != "无")
                        {
                            m.ProcessList[i] = dr["P" + (i + 1)].ToString();
                            m.ProcessListStr += dr["P" + (i + 1)].ToString() + ",";
                        }
                    }
                }
                m.ProcessListStr = m.ProcessListStr.Substring(0, m.ProcessListStr.Length - 1);
            }
            return m;
        }

        internal Model_AssemblyLineModuleBatchInput ReadStaffInfo(string Number)
        {
            Model_AssemblyLineModuleBatchInput m = new Model_AssemblyLineModuleBatchInput();
            string sql = "SELECT GUID,Name FROM T_UserInfo_Staff WHERE NUMBER='" + Number + "' AND DELETEMARK ISNULL";
            DataSet ds = new DataSet();
            bool flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m.StaffGuid = (Guid)dr["GUID"];
                    m.StaffNumber = Number;
                    m.StaffName = dr["Name"].ToString();
                }
            }
            return m;
        }

        internal bool InsertData(ObservableCollection<Model_AssemblyLineModuleBatchInput> data)
        {
            List<string> sqls = new List<string>();
            foreach (Model_AssemblyLineModuleBatchInput m in data)
            {
                if (m.ProductGuid != new Guid() && m.StaffGuid != new Guid() && m.Process != "" && (m.Quantity > 0 || m.Injure > 0))
                {
                    sqls.Add("Insert into T_PM_ProductionSchedule(Guid,Date,StaffID,ProductID,Process,Number,Break,) "
                        + "values()");
                }
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }
    }
}
