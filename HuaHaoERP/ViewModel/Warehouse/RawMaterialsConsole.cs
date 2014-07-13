using HuaHaoERP.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class RawMaterialsConsole
    {
        /// <summary>
        /// 批量入库,true为入库，false为出库或生产
        /// </summary>
        /// <param name="list"></param>
        /// <param name="bol"></param>
        /// <returns></returns>
        internal bool AddByBatch(List<Model.RawMaterialsDetailModel> list, bool bol)
        {
            List<string> sqlList = new List<string>();
            string tag = bol ? "" : "-";
            foreach (RawMaterialsDetailModel d in list)
            {
                if (d.RawMaterialsID != new Guid())
                {
                    string sql = "Insert into T_Warehouse_RawMaterials(Guid,RawMaterialsID,Date,Operator,Number,Remark,Type) "
                        + "values('" + Guid.NewGuid() + "','" + d.RawMaterialsID + "','" + DateTime.Parse(d.Date).ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("T") + "','" + d.Operator + "','" + tag + d.Number + "','" + d.Remark + "','" + d.Type + "')";
                    sqlList.Add(sql);
                }
            }
            return new Helper.SQLite.DBHelper().Transaction(sqlList);
        }

        internal bool ReadList(out List<RawMaterialsDetailModel> data)
        {
            bool flag = true;
            data = new List<RawMaterialsDetailModel>();
            string sql = "select b.Number as Code,b.Name as Name,total(a.Number) as Amount from T_Warehouse_RawMaterials a left join T_ProductInfo_RawMaterials b on a.RawMaterialsID = b.GUID group by a.RawMaterialsID";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RawMaterialsDetailModel d = new RawMaterialsDetailModel();
                    d.Id = id;
                    id++;
                    d.Code = dr["Code"].ToString();
                    d.Name = dr["Name"].ToString();
                    d.Amount = dr["Amount"].ToString();
                    data.Add(d);
                }
            }
            return flag;
        }

        internal bool ReadRecordList(string Type, out List<RawMaterialsDetailModel> data)
        {
            string sql_Where = "";
            if (!Type.StartsWith("全部"))
            {
                sql_Where += " Where a.Type='" + Type + "' ";
            }
            bool flag = true;
            data = new List<RawMaterialsDetailModel>();
            string sql = " select a.Guid as Guid,a.RawMaterialsID as RawMaterialsID,b.Name as Name,strftime(a.Date) as Date,a.Operator as Operator,a.Number as Number,a.Remark as Remark,a.Type as Type "
                       + " from T_Warehouse_RawMaterials a "
                       + " left join T_ProductInfo_RawMaterials b on a.RawMaterialsID = b.Guid "
                       + sql_Where
                       + " order by a.Date desc ";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RawMaterialsDetailModel d = new RawMaterialsDetailModel();
                    d.Id = id;
                    id++;
                    d.Guid = Guid.Parse(dr["Guid"].ToString());
                    d.RawMaterialsID = Guid.Parse(dr["RawMaterialsID"].ToString());
                    d.Date = dr["Date"].ToString();
                    decimal dd = 0;
                    decimal.TryParse(dr["Number"].ToString(), out dd);
                    d.Number = dd;
                    d.Name = dr["Name"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    d.Operator = dr["Operator"].ToString();
                    d.Type = dr["Type"].ToString();
                    data.Add(d);
                }
            }
            return flag;
        }

        internal Guid GetGuid(string number)
        {
            string sql = "select Guid From T_ProductInfo_RawMaterials Where Number='" + number + "' and DeleteMark is null order by AddTime";
            object obj = new object();
            new Helper.SQLite.DBHelper().QuerySingleResult(sql, out obj);
            return Guid.Parse(obj.ToString());
        }

        /// <summary>
        /// Lugia
        /// 获取原料信息
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        internal RawMaterialsDetailModel GetRawMaterialsInfo(string Number)
        {
            RawMaterialsDetailModel m = new RawMaterialsDetailModel();
            string sql = "Select Guid,Name From T_ProductInfo_RawMaterials Where Number='" + Number + "' AND DeleteMark ISNULL";
            DataSet ds = new DataSet();
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m.RawMaterialsID = (Guid)dr["Guid"];
                    m.Name = dr["Name"].ToString();
                }
            }
            return m;
        }
    }
}
