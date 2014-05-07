using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

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


            return false;
        }
        internal bool MarkDelete(SupplierModel d)
        {


            return false;
        }
        internal bool ReadList(out List<SupplierModel> data)
        {
            data = new List<SupplierModel>();

            return false;
        }
    }
}
