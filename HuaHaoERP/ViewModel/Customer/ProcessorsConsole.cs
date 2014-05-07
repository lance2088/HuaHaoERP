using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.ViewModel.Customer
{
    class ProcessorsConsole
    {
        internal bool Add(ProcessorsModel d)
        {
            bool flag = true;
            string sql = "Insert Into T_Processors(GUID,Number,Name,Address,Area,Phone,MobilePhone,Fax,Business,Clerk,OpeningBank,BankCardNo,BankCardName,Remark,AddTime) "
                        + " values('" + d.Guid + "','"+d.Number+"','"+d.Name+"','"+d.Address+"','"+d.Area+"','"+d.Phone+"','"+d.MobilePhone+"','"+d.Fax+"','"+d.Business+"','"+d.Clerk+"','"+d.OpeningBank+"','"+d.BankCardNo+"','"+d.BankCardName+"','"+d.Remark+"','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool Delete(ProcessorsModel d)
        {


            return false;
        }
        internal bool MarkDelete(ProcessorsModel d)
        {


            return false;
        }
        internal bool ReadList(out List<ProcessorsModel> data)
        {
            data = new List<ProcessorsModel>();

            return false;
        }
    }
}
