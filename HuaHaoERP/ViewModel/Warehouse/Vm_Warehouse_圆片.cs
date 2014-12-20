using HuaHaoERP.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class Vm_Warehouse_圆片
    {
        public List<Model_圆片仓库> ReadList()
        {
            List<Model_圆片仓库> data = new List<Model_圆片仓库>();
            string sql = "Select sum(a.Quantity),b.* "
                + "from T_Warehouse_Wafer a "
                + "Left Join T_ProductInfo_Wafer b ON a.WaferGuid=b.Guid "
                + "Where b.Guid IS NOT NULL "
                + "Group By WaferGuid ";
            DataSet ds = new DataSet();
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int rowid = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model_圆片仓库 m = new Model_圆片仓库();
                    m.Guid = dr.Field<Guid>("Guid");
                    m.序号 = rowid++;
                    m.编号 = dr.Field<string>("Number");
                    m.直径 = dr.Field<string>("Diameter");
                    m.厚度 = dr.Field<string>("Thickness");
                    m.数量 = dr.Field<Int64>("sum(a.Quantity)");
                    data.Add(m);
                }
            }
            return data;
        }
    }
}
