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
    class DeliveryProcessInConsole
    {
        internal bool DeleteDetail(ProductManagement_PickingDetailModel m)
        {
            string sql = "update T_PM_ProductInProcessDetail set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where Guid='" + m.Guid + "'";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }


        internal bool Insert(ProductManagement_PickingModel m, ObservableCollection<ProductManagement_PickingDetailModel> list)
        {
            List<string> sqlList = new List<string>();
            string date = Date.FormatToD(m.Date);
            string sql = "insert into T_PM_ProductInProcess(Guid,Number,ProcessorID,Date,Operator,Remark) VALUES ('" + m.Guid + "','" + m.OrderNO + "','" + m.ProcessorID + "','" + date + "','" + CommonParameters.LoginUserName + "','" + m.Remark + "')";
            sqlList.Add(sql);
            foreach (ProductManagement_PickingDetailModel mm in list)
            {
                if (mm.ProductID.Equals(Guid.Empty))
                {
                    continue;
                }
                else
                {
                    sql = "insert into T_PM_ProductInProcessDetail(Guid,ParentId,ProductID,Date,Operator,QuantityA,QuantityB,QuantityC) VALUES ('" + Guid.NewGuid() + "','" + m.Guid + "','" + mm.ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CommonParameters.LoginUserName + "','" + mm.QuantityA + "','" + mm.QuantityB + "','" + mm.QuantityD + "')";
                    sqlList.Add(sql);
                    sql = "Insert into T_Warehouse_SparePartsInventory(Guid,ProcessorID,ProductID,Date,Operator,Quantity,Remark) "
                        + " values('" + Guid.NewGuid() + "','" + m.ProcessorID + "','" + mm.ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CommonParameters.LoginUserName + "','-" + (mm.QuantityA + mm.QuantityB) + "','从抛光收货单录入')";
                    sqlList.Add(sql);
                    sql = "Insert into T_Warehouse_Product(Guid,ProductID,Date,Operator,Quantity,Remark) "
                        + " values('" + Guid.NewGuid() + "','" + mm.ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CommonParameters.LoginUserName + "','" + mm.QuantityA + "','从抛光收货单录入,记录损坏数量：" + mm.QuantityB + "')";
                    sqlList.Add(sql);
                }
            }
            //更新排名
            return new Helper.SQLite.DBHelper().Transaction(sqlList);
        }

        internal bool Update(ProductManagement_PickingModel mm, ObservableCollection<ProductManagement_PickingDetailModel> data)
        {
            throw new NotImplementedException();
        }

        internal bool ReadProductInfo(Guid processorID, string Number, out ProductManagement_PickingDetailModel m, out int value)
        {
            m = new ProductManagement_PickingDetailModel();
            string sql0 = "select Guid,Name from T_ProductInfo_Product where Number='" + Number + "'";
            DataSet ds = new DataSet();
            value = 0;
            decimal temp = 0;
            if (new Helper.SQLite.DBHelper().QueryData(sql0, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m.ProductID = (Guid)dr["Guid"];
                    m.Name = dr["Name"].ToString();
                }
                string sql1 = " SELECT " +
                        " total(a.Quantity) as QuantityB " +
                        " FROM " +
                        " T_Warehouse_SparePartsInventory a " +
                        " WHERE " +
                        " a.ProcessorID = '" + processorID + "' AND A.ProductID = '" + m.ProductID +
                        "' GROUP BY " +
                        " a.ProductID "
                        ;
                object obj;
                new Helper.SQLite.DBHelper().QuerySingleResult(sql1, out obj);
                decimal.TryParse(obj.ToString(), out temp);
                value = decimal.ToInt32(temp);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool ReadList(DateTime Start, DateTime End, Guid ProductID, Guid ProcessorID, out List<ProductManagement_PickingModel> data, out string strCount)
        {
            strCount = "";
            string sql_WhereParm = "";
            string LastID = string.Empty;
            if (ProductID != new Guid())
            {
                sql_WhereParm += " AND b.ProductID='" + ProductID + "' ";
            }
            if (ProcessorID != new Guid())
            {
                sql_WhereParm += " AND a.ProcessorID='" + ProcessorID + "' ";
            }
            sql_WhereParm += " AND a.Date between '" + Start.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + End.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            bool flag = false;
            data = new List<ProductManagement_PickingModel>();
            int Count = 0;
            int temp = 0;
            ProductManagement_PickingModel LastData = new ProductManagement_PickingModel();
            string sql = "SELECT " +
                        "	a.Guid,a.Number, " +
                        "	strftime(a.Date) as Date, " +
                        "	a.ProcessorID, " +
                        "	c.Name as ProcessorName, " +
                        "	b.ProductID, " +
                        "	d.Name as ProductName, " +
                        "	b.QuantityA,b.QuantityB," +
                        "	a.Remark " +
                        "FROM " +
                        "	T_PM_ProductInProcess a " +
                        "LEFT JOIN T_PM_ProductInProcessDetail b ON a.Guid = b.ParentId " +
                        "LEFT JOIN T_UserInfo_Processors c ON a.ProcessorID = c.GUID " +
                        "LEFT JOIN T_ProductInfo_Product d ON b.ProductID = d.GUID  where a.deleteMark is null " + sql_WhereParm +
                        " order by a.Date desc";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (LastID != dr["Guid"].ToString())
                    {
                        if (LastID != string.Empty)
                        {
                            data.Add(LastData);
                        }
                        LastID = dr[0].ToString();
                        ProductManagement_PickingModel d = new ProductManagement_PickingModel();
                        LastData = d;
                        LastData.Guid = (Guid)dr["Guid"];
                        LastData.Id = id;
                        id++;
                        LastData.Date = dr["Date"].ToString();
                        LastData.OrderNO = dr["Number"].ToString();
                        LastData.ProcessorName = dr["ProcessorName"].ToString();
                        LastData.ProductName = dr["ProductName"].ToString();
                        LastData.ProcessorID = (Guid)dr["ProcessorID"];
                        int.TryParse(dr["QuantityA"].ToString(), out temp);
                        LastData.QuantityA = temp.ToString();
                        Count += temp;
                        int.TryParse(dr["QuantityB"].ToString(), out temp);
                        LastData.QuantityB = temp.ToString();
                        LastData.Remark = dr["Remark"].ToString();
                    }
                    else //旧凭证
                    {
                        LastData.ProductName += "\n" + dr["ProductName"].ToString();
                        int.TryParse(dr["QuantityA"].ToString(), out temp);
                        LastData.QuantityA += "\n" + dr["QuantityA"].ToString();
                        Count += temp;
                        int.TryParse(dr["QuantityB"].ToString(), out temp);
                        LastData.QuantityB += "\n" + dr["QuantityB"].ToString();
                    }
                }
                if (ds.Tables[0].Rows.Count != 0)
                {
                    data.Add(LastData);
                }
            }
            strCount = (Count).ToString();
            return flag;
        }
    }
}
