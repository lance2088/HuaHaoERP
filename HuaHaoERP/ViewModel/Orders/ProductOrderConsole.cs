using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HuaHaoERP.ViewModel.Orders
{
    class ProductOrderConsole
    {
        internal bool Add(Model.ProductOrderModel d)
        {
            bool flag = false;
            List<string> sqls = new List<string>();
            string sql_Order = "Insert into T_Orders_Product(Guid,OrderNumber,CustomerID,DeliveryDate,OrderDate) "
                +"Values('"+d.Guid+"','"+d.OrderNumber+"','"+d.CustomerID+"','"+d.DeliveryDate+"','"+d.OrderDate+"')";
            sqls.Add(sql_Order);
            foreach(Model.ProductOrderDetailsModel dd in d.Details)
            {
                string sql_Details = "Insert into T_Orders_ProductDetails(Guid,OrderID,ProductID,NumberOfItems,Quantity,Unit,Remark) "
                    +"Values('"+dd.Guid+"','"+dd.OrderID+"','"+dd.ProductID+"','"+dd.NumberOfItems+"','"+dd.Quantity+"','"+dd.Unit+"','"+dd.Remark+"')";
                sqls.Add(sql_Details);
            }
            flag = new Helper.SQLite.DBHelper().Transaction(sqls);
            return flag;
        }
        internal bool Delete(Model.ProductOrderModel d)
        {
            bool flag = false;

            return flag;
        }
        internal bool MarkDelete(Model.ProductOrderModel d)
        {
            bool flag = false;

            return flag;
        }
        internal bool ReadList(out List<Model.ProductOrderModelForDataGrid> data)
        {
            bool flag = true;
            Guid LastOrderGuid = new Guid();
            data = new List<Model.ProductOrderModelForDataGrid>();
            string sql = " SELECT                                                                                   "
                       + "     a.Guid,a.OrderNumber,c.Name as CustomerName,a.DeliveryDate,a.OrderDate,              "
                       + "     d.Name as ProductName,b.NumberOfItems,b.Quantity,b.Unit,b.Remark                     "
                       + " FROM                                                                                     "
                       + "     T_Orders_Product a                                                                   "
                       + " LEFT JOIN T_Orders_ProductDetails b ON a.Guid = b.OrderID                                "
                       + " LEFT JOIN T_UserInfo_Customer c ON a.CustomerID=c.GUID                                   "
                       + " LEFT JOIN T_ProductInfo_Product d ON d.GUID=b.ProductID                                  "
                       + " WHERE a.DeleteMark IS NULL                                                               "
                       + " ORDER BY b.rowid                                                                         ";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                Model.ProductOrderModelForDataGrid d = new Model.ProductOrderModelForDataGrid();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (LastOrderGuid == (Guid)dr["GUID"])
                    {
                        d.ProductName += "\n" + dr["ProductName"].ToString();
                        d.NumberOfItems += "\n" + dr["NumberOfItems"].ToString();
                        d.Quantity += "\n" + dr["Quantity"].ToString();
                        d.Unit += "\n" + dr["Unit"].ToString();
                        d.Remark += "\n" + dr["Remark"].ToString();
                    }
                    else
                    {
                        if(LastOrderGuid != new Guid())
                        {
                            data.Add(d);
                        }
                        d = new Model.ProductOrderModelForDataGrid();
                        d.Guid = (Guid)dr["GUID"];
                        d.OrderNumber = dr["OrderNumber"].ToString();
                        d.CustomerName = dr["CustomerName"].ToString();
                        d.DeliveryDate = dr["DeliveryDate"].ToString();
                        d.OrderDate = dr["OrderDate"].ToString();
                        d.Id = id++;
                        d.ProductName = dr["ProductName"].ToString();
                        d.NumberOfItems = dr["NumberOfItems"].ToString();
                        d.Quantity = dr["Quantity"].ToString();
                        d.Unit = dr["Unit"].ToString();
                        d.Remark = dr["Remark"].ToString();
                    }
                    LastOrderGuid = (Guid)dr["GUID"];
                }
                data.Add(d);
            }
            return flag;
        }
    }
}
