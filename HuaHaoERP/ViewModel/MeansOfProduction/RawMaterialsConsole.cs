using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.ViewModel.MeansOfProduction
{
    class RawMaterialsConsole
    {
        internal bool Add(RawMaterialsModel d)
        {
            bool flag = true;

            return flag;
        }
        internal bool Delete(RawMaterialsModel d)
        {
            bool flag = true;
            string sql = "Delete From T_RawMaterials Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool MarkDelete(RawMaterialsModel d)
        {
            bool flag = true;
            string sql = "Update T_RawMaterials Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool ReadList(out List<RawMaterialsModel> data)
        {
            bool flag = true;
            data = new List<RawMaterialsModel>();

            return flag;
        }
    }
}
