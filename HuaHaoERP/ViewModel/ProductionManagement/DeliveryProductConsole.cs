using HuaHaoERP.Model.ProductionManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class DeliveryProductConsole
    {
        internal bool ReadProductInfo(Guid processorID,string Number,out ProductManagement_DevlieryDetailModel m,out int value)
        {
            m = new ProductManagement_DevlieryDetailModel();
            string sql0 = "select Guid,Name from T_ProductInfo_Product where Number='" + Number + "'";
            string sql1 = " SELECT " +
                        " a.ProcessorID, " +
                        " a.ProductID, " +
                        " total(a.Quantity) as QuantityB " +
                        " FROM " +
                        " T_Warehouse_SparePartsInventory a " +
                        " WHERE " +
                        " a.ProcessorID = '" + processorID + "' " +
                        " GROUP BY " +
                        " a.ProductID "
                        ;
            DataSet ds = new DataSet();
            value = 0;
            int temp = 0;
            if (new Helper.SQLite.DBHelper().QueryData(sql0, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m.ProductID = (Guid)dr["Guid"];
                    m.Name = dr["Name"].ToString();
                }
                object obj;
                new Helper.SQLite.DBHelper().QuerySingleResult(sql1, out obj);
                int.TryParse(obj.ToString(), out temp);
                value = temp;
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool DeleteDetail(ProductManagement_DevlieryDetailModel m)
        {
            string sql = "update T_PM_ProductOutProcessDetail set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Guid='" + m.Guid + "'";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }


        internal bool Insert(ProductManagement_DevlieryModel mm, System.Collections.ObjectModel.ObservableCollection<ProductManagement_DevlieryDetailModel> data)
        {
            throw new NotImplementedException();
        }

        internal bool Update(ProductManagement_DevlieryModel mm, System.Collections.ObjectModel.ObservableCollection<ProductManagement_DevlieryDetailModel> data)
        {
            throw new NotImplementedException();
        }
    }
}
