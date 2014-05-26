using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HuaHaoERP.Model;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class RawMaterialsConsole
    {
        internal bool AddByBatch(List<Model.RawMaterialsDetailModel> list,bool bol)
        {
            List<string> sqlList = new List<string>();
            string tag = bol?"":"-";
            foreach (RawMaterialsDetailModel d in list)
            {
                Console.WriteLine(d.Operator);
                string sql = "Insert into T_Warehouse_RawMaterials(Guid,RawMaterialsID,Date,Operator,Number,Remark) "
                        + "values('" + Guid.NewGuid() + "','" + d.RawMaterialsID + "','" + DateTime.Parse(d.Date).ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("T") + "','" + d.Operator + "','" + tag + d.Number + "','" + d.Remark + "')";
                sqlList.Add(sql);
               
            }
            return new Helper.SQLite.DBHelper().Transaction(sqlList);
        }

        internal bool IsRawMaterialsIDExist(string rawMateriasID)
        {
            string sql = "select count(1) from T_ProductInfo_RawMaterials where number='" + rawMateriasID + "'";
            object result = new object();
            new Helper.SQLite.DBHelper().QuerySingleResult(sql, out result);
            return result.ToString().Equals("0") ? false : true;
        }
        internal bool ReadList(out List<RawMaterialsDetailModel> data)
        {
            bool flag = true;
            data = new List<RawMaterialsDetailModel>();
            string sql = "select a.RawMaterialsID as RawMaterialsID,b.Name as Name,count(1) as Amount from T_Warehouse_RawMaterials a left join T_ProductInfo_RawMaterials b on a.RawMaterialsID = b.Number group by RawMaterialsID";
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
                    d.RawMaterialsID = dr["RawMaterialsID"].ToString();
                    d.Name = dr["Name"].ToString();
                    d.Amount = dr["Amount"].ToString();
                    data.Add(d);
                }
            }
            return flag;
        }

        internal bool ReadRecordList(out List<RawMaterialsDetailModel> data)
        {
            bool flag = true;
            data = new List<RawMaterialsDetailModel>();
            string sql = "select a.Guid as Guid,a.RawMaterialsID as RawMaterialsID,b.Name as Name,strftime(a.Date) as Date,a.Operator as Operator,a.Number as Number,a.Remark as Remark from T_Warehouse_RawMaterials a left join T_ProductInfo_RawMaterials b on a.RawMaterialsID = b.Number order by a.Date desc";
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
                    d.RawMaterialsID = dr["RawMaterialsID"].ToString();
                    d.Date = dr["Date"].ToString();
                    decimal dd = 0;
                    decimal.TryParse(dr["Number"].ToString(), out dd);
                    d.Number = dd;
                    d.Name = dr["Name"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    d.Operator = dr["Operator"].ToString();
                    data.Add(d);
                }
            }
            return flag;
        }

        internal string GetName(string number)
        {
            string sql = "select Name From T_ProductInfo_RawMaterials Where Number='" + number + "' and DeleteMark is null order by AddTime";
            object obj = new object();
            new Helper.SQLite.DBHelper().QuerySingleResult(sql, out obj);
            return obj.ToString();
        }
    }
}
