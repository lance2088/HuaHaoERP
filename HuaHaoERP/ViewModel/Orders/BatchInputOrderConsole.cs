using HuaHaoERP.Helper.SQLite;
using HuaHaoERP.Model.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace HuaHaoERP.ViewModel.Orders
{
    class BatchInputOrderConsole
    {



        internal void ReadOrder(int Type, out ObservableCollection<Model_BatchInputOrder> data)
        {
            data = new ObservableCollection<Model_BatchInputOrder>();
            Model_BatchInputOrder m;
            DataSet ds = new DataSet();
            string TableName = GetTableName(Type);
            string sql = "select * from " + TableName + " WHERE DeleteMark ISNULL Order By Date Desc";
            if (new DBHelper().QueryData(sql, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m = new Model_BatchInputOrder();
                    m.Guid = (Guid)dr["Guid"];
                    m.Number = dr["Number"].ToString();
                    m.Name = dr["Name"].ToString();
                    m.Date = Convert.ToDateTime(dr["Date"].ToString()).ToString("yyyy-MM-dd");
                    m.Remark = dr["Remark"].ToString();
                    data.Add(m);
                }
            }
        }

        internal bool DeleteOrder(int Type, Guid OrderGuid)
        {
            string TableName = GetTableName(Type);
            string DetailTableName = GetDetailTableName(Type);
            List<string> sqls = new List<string>();
            sqls.Add("Update " + TableName + " SET DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Guid='" + OrderGuid + "'");
            if (Type == 1 || Type == 2)
            {
                sqls.Add("Update " + DetailTableName + " Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Obligate1='" + OrderGuid + "'");
            }
            else if (Type == 3)
            {
                sqls.Add("Update T_Warehouse_ProductPacking Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Obligate1='" + OrderGuid + "'");
                sqls.Add("Update T_Warehouse_Product Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Obligate1='" + OrderGuid + "'");
            }
            return new DBHelper().Transaction(sqls);
        }

        private string GetTableName(int Type)
        {
            switch (Type)
            {
                case 1://流水线批量录入
                    return "T_PM_ProductionBatchInput";
                case 2://外加工批量录入
                    return "T_PM_ProcessBatchInput";
                case 3://仓库产品批量录入
                    return "T_Warehouse_ProductBatchInput";
            }
            return "";
        }

        private string GetDetailTableName(int Type)
        {
            switch (Type)
            {
                case 1://流水线批量录入
                    return "T_PM_ProductionSchedule";
                case 2://外加工批量录入
                    return "T_PM_ProcessSchedule";
            }
            return "";
        }
    }
}
