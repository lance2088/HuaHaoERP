using HuaHaoERP.Helper.DataDefinition;
using HuaHaoERP.Helper.Tools;
using HuaHaoERP.Model.ProductionManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        internal bool Insert(ProductManagement_DevlieryModel m, ObservableCollection<ProductManagement_DevlieryDetailModel> list)
        {
            List<string> sqlList = new List<string>();
            string date = Date.FormatToD(m.Date);
            string sql = "insert into T_PM_ProductOutProcess(Guid,Number,ProcessorID,Date,Operator,Remark) VALUES ('" + m.Guid + "','" + m.OrderNO + "','" + m.ProcessorID + "','" + date + "','" + CommonParameters.LoginUserName + "','" + m.Remark + "')";
            sqlList.Add(sql);
            foreach (ProductManagement_DevlieryDetailModel mm in list)
            {
                if (mm.ProductID.Equals(Guid.Empty))
                {
                    continue;
                }
                else
                {
                    sql = "insert into T_PM_ProductOutProcessDetail(Guid,ParentId,ProductID,Date,Operator,QuantityA,QuantityB) VALUES ('" + Guid.NewGuid() + "','" + m.Guid + "','" + mm.ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CommonParameters.LoginUserName + "','" + mm.QuantityA + "','" + mm.QuantityB + "')";
                    sqlList.Add(sql);
                    sql = "Insert into T_Warehouse_HalfProduct(Guid,ProductID,Date,Operator,Quantity,Remark) "
                        + " values('" + Guid.NewGuid() + "','" + mm.ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CommonParameters.LoginUserName + "','-" + mm.QuantityA + "','从抛光发货单录入')";
                    sqlList.Add(sql);
                    sql = "Insert into T_Warehouse_SparePartsInventory(Guid,ProcessorID,ProductID,Date,Operator,Quantity,Remark) "
                        + " values('" + Guid.NewGuid() + "','" + m.ProcessorID + "','" + mm.ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CommonParameters.LoginUserName + "','" + mm.QuantityA + "','从抛光发货单录入')";
                    sqlList.Add(sql);
                }
            }
            //更新排名
            return new Helper.SQLite.DBHelper().Transaction(sqlList);
        }

        internal bool Update(ProductManagement_DevlieryModel mm, ObservableCollection<ProductManagement_DevlieryDetailModel> data)
        {
            throw new NotImplementedException();
        }
    }
}
