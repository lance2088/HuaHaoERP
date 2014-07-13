using System;
using System.Collections.Generic;
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
                + "Values('" + d.Guid + "','" + d.OrderNumber + "','" + d.CustomerID + "','" + d.DeliveryDate + "','" + d.OrderDate + "')";
            sqls.Add(sql_Order);
            foreach (Model.ProductOrderDetailsModel dd in d.Details)
            {
                string sql_Details = "Insert into T_Orders_ProductDetails(Guid,OrderID,ProductID,NumberOfItems,Quantity,Unit,Remark) "
                    + "Values('" + dd.Guid + "','" + dd.OrderID + "','" + dd.ProductID + "','" + dd.NumberOfItems + "','" + dd.Quantity + "','" + dd.Unit + "','" + dd.Remark + "')";
                sqls.Add(sql_Details);
            }
            flag = new Helper.SQLite.DBHelper().Transaction(sqls);
            return flag;
        }
        internal bool Update(Model.ProductOrderModel d)
        {
            bool flag = false;
            List<string> sqls = new List<string>();
            //删掉旧的
            string sql_DelOrder = "Delete From T_Orders_Product where Guid='" + d.Guid + "'";
            sqls.Add(sql_DelOrder);
            string sql_DelDetails = "Delete From T_Orders_ProductDetails where OrderID='" + d.Guid + "'";
            sqls.Add(sql_DelDetails);
            //插入修改的
            string sql_Order = "Insert into T_Orders_Product(Guid,OrderNumber,CustomerID,DeliveryDate,OrderDate) "
                + "Values('" + d.Guid + "','" + d.OrderNumber + "','" + d.CustomerID + "','" + d.DeliveryDate + "','" + d.OrderDate + "')";
            sqls.Add(sql_Order);
            foreach (Model.ProductOrderDetailsModel dd in d.Details)
            {
                string sql_Details = "Insert into T_Orders_ProductDetails(Guid,OrderID,ProductID,NumberOfItems,Quantity,Unit,Remark) "
                    + "Values('" + dd.Guid + "','" + dd.OrderID + "','" + dd.ProductID + "','" + dd.NumberOfItems + "','" + dd.Quantity + "','" + dd.Unit + "','" + dd.Remark + "')";
                sqls.Add(sql_Details);
            }
            flag = new Helper.SQLite.DBHelper().Transaction(sqls);
            return flag;
        }

        internal bool MarkDelete(Model.ProductOrderModelForDataGrid d)
        {
            bool flag = true;
            string sql = "Update T_Orders_Product Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
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
                       + " ORDER BY a.OrderNumber,b.rowid                                                           ";
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
                        if (LastOrderGuid != new Guid())
                        {
                            data.Add(d);
                        }
                        d = new Model.ProductOrderModelForDataGrid();
                        d.Guid = (Guid)dr["GUID"];
                        d.OrderNumber = dr["OrderNumber"].ToString();
                        d.CustomerName = dr["CustomerName"].ToString();
                        d.DeliveryDate = (Convert.ToDateTime(dr["DeliveryDate"]).Year < 10) ? "" : Convert.ToDateTime(dr["DeliveryDate"]).ToString("yyyy-MM-dd");
                        d.OrderDate = Convert.ToDateTime(dr["OrderDate"]).ToString("yyyy-MM-dd");
                        d.Id = id++;
                        d.ProductName = dr["ProductName"].ToString();
                        d.NumberOfItems = dr["NumberOfItems"].ToString();
                        d.Quantity = dr["Quantity"].ToString();
                        d.Unit = dr["Unit"].ToString();
                        d.Remark = dr["Remark"].ToString();
                    }
                    LastOrderGuid = (Guid)dr["GUID"];
                }
                if (LastOrderGuid != new Guid())
                {
                    data.Add(d);
                }
            }
            return flag;
        }

        internal bool GetOrderDetails(Guid OrderID, out List<Model.ProductOrderDetailsModel> dDetails)
        {
            bool flag = false;
            dDetails = new List<Model.ProductOrderDetailsModel>();
            string sql = " Select a.*,b.Number as ProductNumber,b.Name as ProductName"
                       + " from T_Orders_ProductDetails a "
                       + "   Left join T_ProductInfo_Product b ON a.ProductID=b.Guid"
                       + " where OrderID='" + OrderID + "'";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model.ProductOrderDetailsModel d = new Model.ProductOrderDetailsModel();
                    d.Guid = (Guid)dr["Guid"];
                    d.Id = id;
                    d.OrderID = OrderID;
                    d.ProductID = (Guid)dr["ProductID"];
                    d.ProductNumber = dr["ProductNumber"].ToString();
                    d.ProductName = dr["ProductName"].ToString();
                    d.NumberOfItems = int.Parse(dr["NumberOfItems"].ToString());
                    d.Quantity = int.Parse(dr["Quantity"].ToString());
                    d.Unit = dr["Unit"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    dDetails.Add(d);
                }
            }
            return flag;
        }
    }
}
