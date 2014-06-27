using HuaHaoERP.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuaHaoERP.ViewModel.MeansOfProduction
{
    class RawMaterialsConsole
    {
        private bool CheckRepeat(RawMaterialsModel d)
        {
            object oTemp;
            string sql_Repeat = "select 1 from T_ProductInfo_RawMaterials where (Number='" + d.Number + "' OR Name='" + d.Name + "') AND DeleteMark IS NULL AND Guid <> '" + d.Guid + "'";
            return new Helper.SQLite.DBHelper().QuerySingleResult(sql_Repeat, out oTemp);
        }
        internal bool Add(RawMaterialsModel d)
        {
            if (CheckRepeat(d))
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
            if (CheckRepeat(d))
            {
                return false;
            }
            string sql_Update = "Update T_ProductInfo_RawMaterials "
                                + " SET Number='" + d.Number
                                + "',Name='" + d.Name
                                + "',Weight='" + d.Weight
                                + "',Material='" + d.Material
                                + "',Supplier='" + d.Supplier
                                + "',Sp1='" + d.Sp1
                                + "',Sp2='" + d.Sp2
                                + "',Remark='" + d.Remark
                                + "' Where GUID='" + d.Guid + "'";
            return new Helper.SQLite.DBHelper().SingleExecution(sql_Update);
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
