using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HuaHaoERP.Model.Warehouse;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class ProductSparepartsInConsole
    {
        internal ProductSparepartsInModel ReadProductInfo(string ProductNumber)
        {
            ProductSparepartsInModel m = new ProductSparepartsInModel();
            string sql = "SELECT * FROM T_ProductInfo_Product WHERE NUMBER='" + ProductNumber + "' AND DELETEMARK ISNULL";
            DataSet ds = new DataSet();
            bool flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                if (ds.Tables[0].Rows.Count > 1)
                {
                    return m;
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m.Guid = (Guid)dr["GUID"];
                    m.Number = ProductNumber;
                    m.Name = dr["Name"].ToString();
                    m.Material = dr["Material"].ToString();
                    int PerQuantity = 0;
                    int.TryParse(dr["PackageNumber"].ToString(), out PerQuantity);
                    m.PerQuantity = PerQuantity;
                }
            }
            return m;
        }
    }
}
