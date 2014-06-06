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
            object oTemp;
            string sql_Repeat = "select 1 from T_ProductInfo_RawMaterials where (Number='" + d.Number + "' OR Name='" + d.Name + "') AND DeleteMark IS NULL";
            if (new Helper.SQLite.DBHelper().QuerySingleResult(sql_Repeat, out oTemp))
            {
                return false;
            }
            bool flag = true;
            string sql = "Insert Into T_ProductInfo_RawMaterials (GUID,Number,Name,Weight,Material,Supplier,Sp1,Sp2,Remark,AddTime) "
                       + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Weight + "','" + d.Material + "','" + d.Supplier + "','" + d.Sp1 + "','" + d.Sp2 + "','" + d.Remark + "','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool Update(RawMaterialsModel d)
        {
            bool flag = true;
            List<string> sqls = new List<string>();
            string sql_Delete = "Delete From T_ProductInfo_RawMaterials Where GUID='" + d.Guid + "'";
            string sql_Update = "Insert Into T_ProductInfo_RawMaterials (GUID,Number,Name,Weight,Material,Supplier,Sp1,Sp2,Remark,AddTime) "
                                + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Weight + "','" + d.Material + "','" + d.Supplier + "','" + d.Sp1 + "','" + d.Sp2 + "','" + d.Remark + "','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            sqls.Add(sql_Delete);
            sqls.Add(sql_Update);
            flag = new Helper.SQLite.DBHelper().Transaction(sqls);
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
            string sql = "select a.*,b.Number SupplierNumber,b.Name SupplierName "
                +"from T_ProductInfo_RawMaterials a Left Join T_UserInfo_Supplier b On a.Supplier=b.Guid "
                +"Where a.DeleteMark is null order by a.AddTime";
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
                    d.SupplierName = dr["SupplierName"].ToString();
                    d.Sp1 = dr["Sp1"].ToString();
                    d.Sp2 = dr["Sp2"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    d.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    data.Add(d);
                }
            }
            return flag;
        }
        internal bool GetNameList(out DataSet ds)
        {
            bool flag = true;
            ds = new DataSet();
            string sql = "select Guid,Number,Name From T_ProductInfo_RawMaterials Where DeleteMark is null order by AddTime";
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            return flag;
        }
        internal bool GetNameList(string Parm, out DataSet ds)
        {
            bool flag = true;
            ds = new DataSet();
            string sql = "select Guid,Number,Name From T_ProductInfo_RawMaterials "
                       + " Where (Number LIKE '%" + Parm + "%' OR Name LIKE '%" + Parm + "%') AND DeleteMark is null order by AddTime";
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            return flag;
        }
    }
}
