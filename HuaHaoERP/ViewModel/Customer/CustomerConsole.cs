using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

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
        internal void Delete(CustomerModel CustomerData)
        {

        }
        internal void Modify(CustomerModel CustomerData)
        {

        }
    }
}
