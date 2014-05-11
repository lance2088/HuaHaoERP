using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;
using System.Data;

namespace HuaHaoERP.ViewModel.MeansOfProduction
{
    class RawMaterialsConsole
    {
        internal bool Add(RawMaterialsModel d)
        {
            bool flag = true;
            string sql = "Insert Into T_ProductInfo_RawMaterials (GUID,Number,Name,Weight,Material,SupplierNumber,Sp1,Sp2,Remark,AddTime) "
                       + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Weight + "','" + d.Material + "','" + d.SupplierNumber + "','" + d.Sp1 + "','" + d.Sp2 + "','" + d.Remark + "','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool Delete(RawMaterialsModel d)
        {
            bool flag = true;
            string sql = "Delete From T_ProductInfo_RawMaterials Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool MarkDelete(RawMaterialsModel d)
        {
            bool flag = true;
            string sql = "Update T_ProductInfo_RawMaterials Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool ReadList(out List<RawMaterialsModel> data)
        {
            bool flag = true;
            data = new List<RawMaterialsModel>();
            string sql = "select * from T_ProductInfo_RawMaterials Where DeleteMark is null order by AddTime";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RawMaterialsModel d = new RawMaterialsModel();
                    d.Guid = (Guid)dr["GUID"];
                    d.Id = id++;
                    d.Number = dr["Number"].ToString();
                    d.Name = dr["Name"].ToString();
                    d.Weight = dr["Weight"].ToString();
                    d.Material = dr["Material"].ToString();
                    d.SupplierNumber = dr["SupplierNumber"].ToString();
                    d.Sp1 = dr["Sp1"].ToString();
                    d.Sp2 = dr["Sp2"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    d.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    data.Add(d);
                }
            }
            return flag;
        }
    }
}
