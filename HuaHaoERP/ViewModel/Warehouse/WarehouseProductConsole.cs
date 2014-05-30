
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class WarehouseProductConsole
    {
        internal bool Add()
        {
            string sql = "";


            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }
    }
}
