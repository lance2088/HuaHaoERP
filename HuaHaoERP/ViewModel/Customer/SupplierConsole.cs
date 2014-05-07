﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;
using System.Data;

namespace HuaHaoERP.ViewModel.Customer
{
    class SupplierConsole
    {
        internal bool Add(SupplierModel d)
        {
            bool flag = true;
            string sql = "Insert Into T_Supplier(GUID,Number,Name,Address,Area,Phone,MobilePhone,Fax,Business,Clerk,OpeningBank,BankCardNo,BankCardName,Remark,AddTime) "
                        + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Address + "','" + d.Area + "','" + d.Phone + "','" + d.MobilePhone + "','" + d.Fax + "','" + d.Business + "','" + d.Clerk + "','" + d.OpeningBank + "','" + d.BankCardNo + "','" + d.BankCardName + "','" + d.Remark + "','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool Delete(SupplierModel d)
        {
            bool flag = true;
            string sql = "Delete From T_Supplier Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool MarkDelete(SupplierModel d)
        {
            bool flag = true;
            string sql = "Update T_Supplier Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool ReadList(out List<SupplierModel> data)
        {
            bool flag = true;
            data = new List<SupplierModel>();
            string sql = "select * from T_Supplier Where DeleteMark is null order by AddTime";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SupplierModel d = new SupplierModel();
                    d.Guid = (Guid)dr["GUID"];
                    d.Id = id++;
                    d.Number = dr["Number"].ToString();
                    d.Name = dr["Name"].ToString();
                    d.Address = dr["Address"].ToString();
                    d.Area = dr["Area"].ToString();
                    d.Phone = dr["Phone"].ToString();
                    d.MobilePhone = dr["MobilePhone"].ToString();
                    d.Fax = dr["Fax"].ToString();
                    d.Business = dr["Business"].ToString();
                    d.Clerk = dr["Clerk"].ToString();
                    d.OpeningBank = dr["OpeningBank"].ToString();
                    d.BankCardNo = int.Parse(dr["BankCardNo"].ToString());
                    d.BankCardName = dr["BankCardName"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    d.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    data.Add(d);
                }
            }
            return flag;
        }
    }
}