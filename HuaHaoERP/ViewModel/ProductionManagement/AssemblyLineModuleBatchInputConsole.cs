using HuaHaoERP.Model.Order;
using HuaHaoERP.Model.ProductionManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

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
                    m.ProcessListStr = m.ProcessListStr.Substring(0, m.ProcessListStr.Length - 1);
                }
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

        internal bool InsertData(ObservableCollection<Model_AssemblyLineModuleBatchInput> data, bool isAutoDeductionRawMaterials, string OrderNum, string OrderRemark)
        {
            Guid OrderGuid = Guid.NewGuid();
            string DateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            List<string> sqls = new List<string>();
            Guid Guid1;
            Guid Guid2;
            string LastProcess = "";
            foreach (Model_AssemblyLineModuleBatchInput m in data)
            {
                if (m.ProductGuid != new Guid() && m.StaffGuid != new Guid() && m.Process != "" && (m.Quantity > 0 || m.Injure > 0))
                {
                    Guid1 = Guid.NewGuid();
                    Guid2 = Guid.NewGuid();
                    for (int i = 0; i < m.ProcessList.Length; i++)
                    {
                        if (m.ProcessList[i] == m.Process && i != 0)
                        {
                            LastProcess = m.ProcessList[i - 1];
                        }
                    }
                    if (isAutoDeductionRawMaterials)
                    {
                        if (m.Process != m.ProcessList[0])
                        {
                            sqls.Add("Insert into T_PM_ProductionSchedule(Guid,Date,StaffID,ProductID,Process,Number,Break,Remark,ParentGuid,Obligate1) "
                                + "values('" + Guid1 + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + m.StaffGuid + "','" + m.ProductGuid + "','" + LastProcess + "'," + -(m.Quantity + m.Injure) + ",0,'自动扣半成品原料','" + Guid2 + "','" + OrderGuid + "')");
                        }
                    }
                    sqls.Add("Insert into T_PM_ProductionSchedule(Guid,Date,StaffID,ProductID,Process,Number,Break,Remark,ParentGuid,Obligate1) "
                        + "values('" + Guid2 + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + m.StaffGuid + "','" + m.ProductGuid + "','" + m.Process + "'," + m.Quantity + "," + m.Injure + ",'','" + Guid1 + "','" + OrderGuid + "')");
                }
            }
            if (sqls.Count > 0)
            {
                sqls.Add("Insert into T_PM_ProductionBatchInput(Guid,Number,Date,Remark) "
                    + "values('" + OrderGuid + "','" + OrderNum + "','" + DateStr + "','" + OrderRemark + "')");
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }

        /// <summary>
        /// 编辑时读取记录明细
        /// </summary>
        /// <returns></returns>
        internal ObservableCollection<Model_AssemblyLineModuleBatchInput> ReadDatas(Model_BatchInputOrder OrderData)
        {
            ObservableCollection<Model_AssemblyLineModuleBatchInput> data = new ObservableCollection<Model_AssemblyLineModuleBatchInput>();
            Model_AssemblyLineModuleBatchInput m;
            DataSet ds = new DataSet();
            string sql = "Select a.*,"
                            + " p.Guid as PGuid,p.Number as PNumber,p.Name as PName,p.P1,p.P2,p.P3,p.P4,p.P5,p.P6, "
                            + " s.Guid as SGuid,s.Number as SNumber,s.Name as SName "
                        + " from T_PM_ProductionSchedule a "
                            + " left join T_ProductInfo_Product p ON a.ProductID=p.Guid "
                            + " left join T_UserInfo_Staff s ON a.StaffID=s.Guid "
                        + " WHERE a.Obligate1='" + OrderData.Guid + "' "
                            + " AND a.DeleteMark ISNULL "
                            + " AND a.Remark<>'自动扣半成品原料' "
                        ;
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m = new Model_AssemblyLineModuleBatchInput();

                    //Product Info
                    m.ProductGuid = (Guid)dr["PGuid"];
                    m.ProductNumber = dr["PNumber"].ToString();
                    m.ProductName = dr["PName"].ToString();
                    for (int i = 0; i < 6; i++)
                    {
                        if (dr["P" + (i + 1)].ToString() != "无")
                        {
                            m.ProcessList[i] = dr["P" + (i + 1)].ToString();
                            m.ProcessListStr += dr["P" + (i + 1)].ToString() + ",";
                        }
                    }
                    m.ProcessListStr = m.ProcessListStr.Substring(0, m.ProcessListStr.Length - 1);

                    //Staff Info
                    m.StaffGuid = (Guid)dr["SGuid"];
                    m.StaffNumber = dr["SNumber"].ToString();
                    m.StaffName = dr["SName"].ToString();

                    //Input Info
                    m.Process = dr["Process"].ToString();
                    m.Quantity = int.Parse(dr["Number"].ToString());
                    m.Injure = int.Parse(dr["Break"].ToString());

                    data.Add(m);
                }
            }
            if (data.Count < 20)
            {
                int COUNT = data.Count;
                for (int i = 0; i < 20 - COUNT; i++)
                {
                    data.Add(new Model_AssemblyLineModuleBatchInput { Id = i + 1 });
                }
            }
            return data;
        }
    }
}
