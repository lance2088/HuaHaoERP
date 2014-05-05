using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;
using System.Data;

namespace HuaHaoERP.ViewModel.Customer
{
    class CustomerConsole
    {
        internal bool Add(CustomerModel d)
        {
            bool flag = true;
            string sql = "Insert Into T_Customer (GUID,Number,Name,Company,Address,Phone,MobilePhone,Fax,Business,Remark,CustomerLevel,OrderQuantity) "
                       + " values('"+d.Guid+"','"+d.Number+"','"+d.Name+"','"+d.Company+"','"+d.Address+"','"+d.Phone+"','"+d.MobilePhone+"','"+d.Fax+"','"+d.Business+"','"+d.Remark+"','"+d.CustomerLevel+"','"+d.OrderQuantity+"')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool Delete(CustomerModel d)
        {
            bool flag = true;
            string sql = "Update T_Customer Set DeleteMark='" + DateTime.Now + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool Modify(CustomerModel d)
        {
            bool flag = true;
            string sql = "Insert Into T_Customer (GUID,Number,Name,Company,Address,Phone,MobilePhone,Fax,Business,Remark,CustomerLevel,OrderQuantity) "
                       + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Company + "','" + d.Address + "','" + d.Phone + "','" + d.MobilePhone + "','" + d.Fax + "','" + d.Business + "','" + d.Remark + "','" + d.CustomerLevel + "','" + d.OrderQuantity + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool ReadList(out List<CustomerModel> data)
        {
            bool flag = true;
            data = new List<CustomerModel>();
            string sql = "select * from T_Customer Where DeleteMark is null order by Number";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if(flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CustomerModel d = new CustomerModel();
                    d.Guid = (Guid)dr["GUID"];
                    d.Id = id++;
                    d.Number = dr[1].ToString();
                    d.Name = dr[2].ToString();
                    d.Company = dr[3].ToString();
                    d.Address = dr[4].ToString();
                    d.Phone = dr[5].ToString();
                    d.MobilePhone = dr[6].ToString();
                    d.Fax = dr[7].ToString();
                    d.Business = dr[8].ToString();
                    d.Remark = dr[9].ToString();
                    d.LastOrderTime = dr["LastOrderTime"].ToString();
                    d.CustomerLevel = int.Parse(dr["CustomerLevel"].ToString());
                    d.OrderQuantity = int.Parse(dr["OrderQuantity"].ToString());
                    data.Add(d);
                }
            }
            return flag;
        }
    }
}
