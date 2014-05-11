using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.ViewModel.MeansOfProduction
{
    class ProductConsole
    {
        internal bool Add(ProductModel d)
        {
            bool flag = true;

            return flag;
        }
        internal bool Delete(ProductModel d)
        {
            bool flag = true;
            string sql = "Delete From T_ProductInfo_Product Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool MarkDelete(ProductModel d)
        {
            bool flag = true;
            string sql = "Update T_ProductInfo_Product Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool ReadList(out List<ProductModel> data)
        {
            bool flag = true;
            data = new List<ProductModel>();

            return flag;
        }
    }
}
