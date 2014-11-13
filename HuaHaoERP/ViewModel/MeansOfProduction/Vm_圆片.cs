using HuaHaoERP.Model.MeansOfProduction;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuaHaoERP.ViewModel.MeansOfProduction
{
    class Vm_圆片
    {
        public List<Model_圆片资料> ReadList()
        {
            List<Model_圆片资料> data = new List<Model_圆片资料>();
            string sql = "Select * from T_ProductInfo_Wafer";
            DataSet ds = new DataSet();
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int rowid = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model_圆片资料 m = new Model_圆片资料();
                    m.Guid = dr.Field<Guid>("Guid");
                    m.序号 = rowid++;
                    m.编号 = dr.Field<string>("Number");
                    m.直径 = dr.Field<string>("Diameter");
                    m.厚度 = dr.Field<string>("Thickness");
                    m.备注 = dr.Field<string>("Remark");
                    data.Add(m);
                }
            }
            return data;
        }

        public bool Delete(Guid guid)
        {
            string sql = "Delete From T_ProductInfo_Wafer Where Guid='" + guid + "'";
            Console.WriteLine(sql);
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }

        public bool Add(List<Model_圆片资料> data)
        {
            List<string> sqls = new List<string>();
            foreach (var m in data)
            {
                if (string.IsNullOrWhiteSpace(m.编号))
                {
                    continue;
                }
                sqls.Add("Insert Into T_ProductInfo_Wafer values('" + Guid.NewGuid() + "','" + m.编号 + "','" + m.直径 + "','" + m.厚度 + "','" + m.备注 + "')");
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }

        public bool Add(Model_圆片资料 m)
        {
            List<Model_圆片资料> data = new List<Model_圆片资料> { m };
            return Add(data);
        }
    }
}
