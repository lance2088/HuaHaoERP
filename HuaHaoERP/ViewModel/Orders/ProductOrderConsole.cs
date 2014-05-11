using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HuaHaoERP.ViewModel.Orders
{
    class ProductOrderConsole
    {
        internal bool Add(Model.ProductOrderModel d)
        {
            bool flag = false;

            return flag;
        }
        internal bool Delete(Model.ProductOrderModel d)
        {
            bool flag = false;

            return flag;
        }
        internal bool MarkDelete(Model.ProductOrderModel d)
        {
            bool flag = false;

            return flag;
        }
        internal bool ReadList(out List<Model.ProductOrderModel> data)
        {
            bool flag = true;
            data = new List<Model.ProductOrderModel>();
            string sql = "select * from T_ProductInfo_Product Where DeleteMark is null order by AddTime";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model.ProductOrderModel d = new Model.ProductOrderModel();
                    //d.Guid = (Guid)dr["GUID"];
                    //d.Id = id++;
                    //d.Number = dr["Number"].ToString();
                    //d.Name = dr["Name"].ToString();
                    //d.Material = dr["Material"].ToString();
                    //d.Type = dr["Type"].ToString();
                    //d.Specification = dr["Specification"].ToString();
                    //d.P1 = dr["P1"].ToString();
                    //d.P2 = dr["P2"].ToString();
                    //d.P3 = dr["P3"].ToString();
                    //d.P4 = dr["P4"].ToString();
                    //d.P5 = dr["P5"].ToString();
                    //d.P6 = dr["P6"].ToString();
                    //GenerateProcess(ref d);
                    //d.PackageNumber = int.Parse(dr["PackageNumber"].ToString());
                    //d.Remark = dr["Remark"].ToString();
                    //d.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    data.Add(d);
                }
            }
            return flag;
        }
    }
}
