using HuaHaoERP.Helper.SQLite;
using HuaHaoERP.Model.Order;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace HuaHaoERP.ViewModel.Orders
{
    class BatchInputOrderConsole
    {
        internal void ReadOrder(out ObservableCollection<Model_BatchInputOrder> data)
        {
            data = new ObservableCollection<Model_BatchInputOrder>();
            Model_BatchInputOrder m;
            DataSet ds = new DataSet();
            string TableName = "T_Warehouse_ProductBatchInput";
            string sql = "select * from " + TableName + " WHERE DeleteMark ISNULL";
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
    }
}
