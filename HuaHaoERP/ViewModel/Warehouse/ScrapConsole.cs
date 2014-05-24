using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class ScrapConsole
    {
        internal bool Add(Model.AssemblyLineModuleProcessModel d)
        {
            bool flag = true;
            string sql = "Insert into T_PM_ProductionSchedule(Guid,Date,StaffID,ProductID,Process,Number) "
                        + "values('" + d.Guid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + d.StaffID + "','" + d.ProductID + "','" + d.Process + "'," + d.Quantity + ")";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
    }
}
