using HuaHaoERP.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class ProductBatchInConsole
    {
        /// <summary>
        /// 读取产品详细信息
        /// </summary>
        /// <param name="ProductNumber">产品编号</param>
        /// <returns></returns>
        internal Model_WarehouseProductBatchIn ReadProductInfo(string ProductNumber)
        {
            Model_WarehouseProductBatchIn m = new Model_WarehouseProductBatchIn();
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

        /// <summary>
        /// 新插入：包装产品至仓库
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isOut">是否出库</param>
        /// <param name="date">日期</param>
        /// <param name="OrderNum">订单编号</param>
        /// <param name="OrderRemark">订单备注</param>
        /// <param name="OrderType">订单类型</param>
        /// <returns></returns>
        internal bool InsertPacking(ObservableCollection<Model_WarehouseProductBatchIn> data, bool isOut, DateTime date, string OrderNum, string OrderRemark, string OrderType, bool IsInitInput)
        {
            Guid OrderGuid = Guid.NewGuid();
            string DateStr = date.ToString("yyyy-MM-dd HH:mm:ss");
            int Negative = 1;
            string Remark = "包装：手动录入";
            if (isOut)
            {
                Negative = -1;
                Remark = "出库";
            }
            List<string> sqls = new List<string>();
            foreach (Model_WarehouseProductBatchIn m in data)
            {
                if (m.Guid != new Guid())
                {
                    if (!isOut && !IsInitInput)
                    {
                        //扣散件
                        sqls.Add("Insert into T_Warehouse_Product(Guid,ProductID,Date,Operator,Quantity,Remark,Obligate1) "
                            + "values('" + Guid.NewGuid() + "','" + m.Guid + "','" + DateStr + "','" + Helper.DataDefinition.CommonParameters.RealName + "'," + -m.AllQuantity + ",'包装：手动包装自动扣除','" + OrderGuid + "')");
                    }
                    sqls.Add("Insert into T_Warehouse_ProductPacking(Guid,ProductID,Date,Operator,Quantity,Remark,Obligate1) "
                        + "values('" + Guid.NewGuid() + "','" + m.Guid + "','" + DateStr + "','" + Helper.DataDefinition.CommonParameters.RealName + "'," + m.PackQuantity * Negative + ",'" + Remark + "','" + OrderGuid + "')");
                }
            }
            if (sqls.Count > 0)
            {
                sqls.Add("Insert into T_Warehouse_ProductBatchInput(Guid,Number,Date,Remark,OrderType) "
                    + "values('" + OrderGuid + "','" + OrderNum + "','" + DateStr + "','" + OrderRemark + "','" + OrderType + "')");
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }

        /// <summary>
        /// 新插入：散件产品到仓库
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isOut">是否出库</param>
        /// <param name="date"></param>
        /// <param name="OrderNum"></param>
        /// <param name="OrderRemark"></param>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        internal bool InsertSpareparts(ObservableCollection<Model_WarehouseProductBatchIn> data, bool isOut, DateTime date, string OrderNum, string OrderRemark, string OrderType)
        {
            Guid OrderGuid = Guid.NewGuid();
            string DateStr = date.ToString("yyyy-MM-dd HH:mm:ss");
            int Negative = 1;
            string Remark = "入库：手动录入";
            if (isOut)
            {
                Negative = -1;
                Remark = "出库：手动录入";
            }
            List<string> sqls = new List<string>();
            foreach (Model_WarehouseProductBatchIn m in data)
            {
                if (m.Guid != new Guid())
                {
                    sqls.Add("Insert into T_Warehouse_Product(Guid,ProductID,Date,Operator,Quantity,Remark,Obligate1) "
                        + "values('" + Guid.NewGuid() + "','" + m.Guid + "','" + DateStr + "','" + Helper.DataDefinition.CommonParameters.RealName + "'," + m.AllQuantity * Negative + ",'" + Remark + "','" + OrderGuid + "')");
                }
            }
            if (sqls.Count > 0)
            {
                sqls.Add("Insert into T_Warehouse_ProductBatchInput(Guid,Number,Date,Remark,OrderType) "
                    + "values('" + OrderGuid + "','" + OrderNum + "','" + DateStr + "','" + OrderRemark + "','" + OrderType + "')");
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }

        /// <summary>
        /// 读取要修改的单的详细内容
        /// </summary>
        /// <returns></returns>
        internal ObservableCollection<Model_WarehouseProductBatchIn> ReadDatas(Guid OrderGuid, string OrderType)
        {
            string TableName = "";
            int IsOut = 1;
            switch (OrderType)
            {
                case "0"://入库：包装产品
                    TableName = "T_Warehouse_ProductPacking";
                    break;
                case "1"://入库：散件产品
                    TableName = "T_Warehouse_Product";
                    break;
                case "2"://出库：包装产品
                    TableName = "T_Warehouse_ProductPacking";
                    IsOut = -1;
                    break;
                case "3"://出库：散件产品
                    TableName = "T_Warehouse_Product";
                    IsOut = -1;
                    break;
            }

            ObservableCollection<Model_WarehouseProductBatchIn> data = new ObservableCollection<Model_WarehouseProductBatchIn>();
            Model_WarehouseProductBatchIn m;
            DataSet ds = new DataSet();
            string sql = "Select a.*,p.Number,p.Name,p.Material,p.PackageNumber "
                + " from " + TableName + " a"
                + " left join T_ProductInfo_Product p ON a.ProductID=p.Guid"
                + " Where a.Obligate1='" + OrderGuid + "' "
                + " AND a.DeleteMark ISNULL";
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m = new Model_WarehouseProductBatchIn();
                    m.Guid = (Guid)dr["ProductID"];
                    m.Number = dr["Number"].ToString();
                    m.Name = dr["Name"].ToString();
                    m.Material = dr["Material"].ToString();

                    if (OrderType == "0" || OrderType == "2")//包装产品
                    {
                        m.PerQuantity = int.Parse(dr["PackageNumber"].ToString());//单件量
                        m.PackQuantity = IsOut * int.Parse(dr["Quantity"].ToString());//包装数
                        m.AllQuantity = m.PerQuantity * m.PackQuantity;//总数量
                    }
                    else//散件产品
                    {
                        m.AllQuantity = IsOut * int.Parse(dr["Quantity"].ToString());
                    }
                    data.Add(m);
                }
            }
            //补全20行
            if (data.Count < 20)
            {
                int COUNT = data.Count;
                for (int i = 0; i < 20 - COUNT; i++)
                {
                    data.Add(new Model_WarehouseProductBatchIn { Id = i + 1 });
                }
            }
            return data;
        }

        internal bool UpdateBatchIn(Guid OrderGuid)
        {
            List<string> sqls = new List<string>();
            string DateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sqls.Add("Update T_Warehouse_ProductBatchInput SET DeleteMark='" + DateStr + "' Where Guid='" + OrderGuid + "'");
            sqls.Add("Update T_Warehouse_Product SET DeleteMark='" + DateStr + "' Where Obligate1='" + OrderGuid + "'");
            sqls.Add("Update T_Warehouse_ProductPacking SET DeleteMark='" + DateStr + "' Where Obligate1='" + OrderGuid + "'");
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }
    }
}
