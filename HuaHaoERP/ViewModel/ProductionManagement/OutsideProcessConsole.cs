using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class OutsideProcessConsole
    {
        internal bool Add(Model.ProductionManagement_OutsideProcessModel d)
        {
            bool flag = false;
            string sql = "Insert into T_PM_ProcessSchedule(Guid,Date,ProductID,ProcessorsID,Quantity,MinorInjuries,Injuries,Lose,OrderType,Remark) "
                       + " values('"+d.Guid+"','"+d.OrderDate+"','"+d.ProductGuid+"','"+d.ProcessorsGuid+"','"+d.Quantity+"','"+d.MinorInjuries+"','"+d.Injuries+"','"+d.Lose+"','"+d.OrderType+"','"+d.Remark+"')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
    }
}
