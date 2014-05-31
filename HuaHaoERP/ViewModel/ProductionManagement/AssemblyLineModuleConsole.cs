using System;
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
            string sql = "Insert into T_PM_ProductionSchedule(Guid,Date,StaffID,ProductID,Process,Number,Break) "
                        + "values('" + d.Guid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + d.StaffID + "','" + d.ProductID + "','" + d.Process + "'," + d.Quantity + ","+d.BreakNum+")";
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
                       + "   total(b.Number) as Quantity, "
                       + "   total(b.Break) as BreakNum "
                       + " from T_ProductInfo_Product a "
                       + " LEFT JOIN T_PM_ProductionSchedule b ON b.ProductID = a.GUID "
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
                            ProcessList[i].BreakNum = Convert.ToInt32(dr["BreakNum"].ToString());
                        }
                    }
                }
                CalculateProcessList(ProductGuid, ref ProcessList);
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
        private void CalculateProcessList(Guid ProductGuid, ref List<Model.AssemblyLineModuleProcessModel> d)
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
            //补全入库扣掉的数量，使计算不出错。
            string sql2 = "Select total(Number) as Num,Process from T_PM_ProductionSchedule where ProductID='" + ProductGuid + "' AND Remark='入库' GROUP BY Process";
            DataSet ds2 = new DataSet();
            new Helper.SQLite.DBHelper().QueryData(sql2, out ds2);
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                for (int i = 0; i <= d.Count - 1; i++)
                {
                    if (d[i].Process == dr["Process"].ToString())
                    {
                        d[i].Quantity -= int.Parse(dr["Num"].ToString());
                    }
                }
            }
            //计算
            for (int i = 0; i <= d.Count - 1; i++)
            {
                if (i != d.Count - 1)
                {
                    d[i].Quantity -= d[i + 1].Quantity;
                }
                if(d[i].Process == "抛光")
                {
                    d[i].Quantity += In;
                    d[i].BreakNum += Break;
                    d[i-1].Quantity -= Out;
                }
            }
            //计算后去掉入库的，使最终结果是正确的
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                for (int i = 0; i <= d.Count - 1; i++)
                {
                    if (d[i].Process == dr["Process"].ToString())
                    {
                        d[i].Quantity += int.Parse(dr["Num"].ToString());
                    }
                }
            }
        }

        internal int ReadDetials(Guid ProductID, string Process, Guid StaffID, DateTime Start, DateTime End, out List<Model.ProductionManagement.AssemblyLineDetailsModel> data)
        {
            int Count = 0;
            string sql_Where = " Where a.Date Between '" + Start.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + End.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            if(ProductID != new Guid())
            {
                sql_Where += " AND a.ProductID='" + ProductID + "' ";
            }
            if(StaffID != new Guid())
            {
                sql_Where += " AND a.StaffID='" + StaffID + "' ";
            }
            if (Process != "全部工序")
            {
                sql_Where += " AND a.Process='" + Process + "' ";
            }
            data = new List<Model.ProductionManagement.AssemblyLineDetailsModel>();
            string sql = " Select a.*,b.Name as StaffName,c.Name as ProductName,a.Remark "
                       + " from T_PM_ProductionSchedule a "
                       + " Left join T_UserInfo_Staff b ON a.StaffID=b.GUID "
                       + " Left join T_ProductInfo_Product c ON a.ProductID=c.GUID "
                       + sql_Where;
            DataSet ds = new DataSet();
            new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            int id = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Model.ProductionManagement.AssemblyLineDetailsModel d = new Model.ProductionManagement.AssemblyLineDetailsModel();
                d.Guid = (Guid)dr["GUID"];
                d.Id = id++;
                d.StaffID = (Guid)dr["GUID"];
                d.Date = Convert.ToDateTime(dr["Date"].ToString()).ToString("yyyy-MM-dd");
                d.StaffName = dr["StaffName"].ToString();
                d.ProductID = (Guid)dr["GUID"];
                d.ProductName = dr["ProductName"].ToString();
                d.Process = dr["Process"].ToString();
                d.Quantity = int.Parse(dr["Number"].ToString());
                d.Remark = dr["Remark"].ToString();
                Count += d.Quantity;
                data.Add(d);
            }
            return Count;
        }
        /// <summary>
        /// 入库
        /// </summary>
        internal bool Storage(Guid StaffID, Guid ProductID, string Process, int Number)
        {
            string sql = " Insert into T_PM_ProductionSchedule(Guid,Date,StaffID,ProductID,Process,Number,Remark) "
                       + " values('" + Guid.NewGuid() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + StaffID + "','" + ProductID + "','" + Process + "','-" + Number + "','入库')";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }
    }
}
