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
            string sql = "Insert Into T_ProductInfo_Product (GUID,Number,Name,Material,Type,Specification,P1,P2,P3,P4,P5,P6,PackageNumber,Remark,AddTime) "
                       + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Material + "','" + d.Type + "','" + d.Specification + "','" + d.P1 + "','" + d.P2 + "','" + d.P3 + "','" + d.P4 + "','" + d.P5 + "','" + d.P6 + "','" + d.PackageNumber + "','" + d.Remark + "','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
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
