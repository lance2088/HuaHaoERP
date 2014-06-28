using HuaHaoERP.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class ProductPackingInConsole
    {
        internal Model_WarehouseProductPackingIn ReadProductInfo(string ProductNumber)
        {
            Model_WarehouseProductPackingIn m = new Model_WarehouseProductPackingIn();
            string sql = "SELECT GUID,Name,Material,PackageNumber FROM T_ProductInfo_Product WHERE NUMBER='" + ProductNumber + "' AND DELETEMARK ISNULL";
            DataSet ds = new DataSet();
            bool flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
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

        internal bool InsertPacking(ObservableCollection<Model_WarehouseProductPackingIn> data, bool isOut, DateTime date)
        {
            int Negative = 1;
            string Remark = "包装：手动录入";
            if (isOut)
            {
                Negative = -1;
                Remark = "出库";
            }
            List<string> sqls = new List<string>();
            foreach(Model_WarehouseProductPackingIn m in data)
            {
                if(m.Guid != new Guid())
                {
                    if(!isOut)
                    {
                        sqls.Add("Insert into T_Warehouse_Product(Guid,ProductID,Date,Operator,Quantity,Remark) "
                            + "values('" + Guid.NewGuid() + "','" + m.Guid + "','" + date.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Helper.DataDefinition.CommonParameters.RealName + "'," + -m.AllQuantity + ",'包装：手动包装自动扣除')");
                    }
                    sqls.Add("Insert into T_Warehouse_ProductPacking(Guid,ProductID,Date,Operator,Quantity,Remark) "
                        + "values('" + Guid.NewGuid() + "','" + m.Guid + "','" + date.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Helper.DataDefinition.CommonParameters.RealName + "'," + m.PackQuantity * Negative + ",'" + Remark + "') ");
                }
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }

        internal bool InsertSpareparts(ObservableCollection<Model_WarehouseProductPackingIn> data, bool isOut, DateTime date)
        {
            int Negative = 1;
            string Remark = "入库：手动录入";
            if(isOut)
            {
                Negative = -1;
                Remark = "出库：手动录入";
            }
            List<string> sqls = new List<string>();
            foreach (Model_WarehouseProductPackingIn m in data)
            {
                if (m.Guid != new Guid())
                {
                    sqls.Add("Insert into T_Warehouse_Product(Guid,ProductID,Date,Operator,Quantity,Remark) "
                        + "values('" + Guid.NewGuid() + "','" + m.Guid + "','" + date.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Helper.DataDefinition.CommonParameters.RealName + "'," + m.AllQuantity * Negative + ",'" + Remark + "')");
                }
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }
    }
}
