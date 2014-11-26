using HuaHaoERP.Model.Order;
using HuaHaoERP.Model.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuaHaoERP.ViewModel.Orders
{
    class Vm_Order_圆片
    {
        public List<Model_圆片订单> ReadList(int inOut, DateTime start, DateTime end)
        {
            List<Model_圆片订单> data = new List<Model_圆片订单>();
            string sql = "SELECT "
                       + "	a.*,"
                       + "  b.Quantity,b.LossQuantity,b.HalfProductGuid,b.HalfProductQuantity,"
                       + "  c.Number,c.Diameter,c.Thickness, "
                       + "  d.Number as PNumber,d.Name as PName "
                       + "FROM "
                       + "	T_Order_Wafer a "
                       + "Left join T_Warehouse_Wafer b on a.Guid=b.OrderGuid "
                       + "Left join T_ProductInfo_Wafer c on b.WaferGuid=c.Guid "
                       + "Left Join T_ProductInfo_Product d ON d.Guid=b.HalfProductGuid "
                       + "where a.OrderType=" + inOut + " "
                       + "  AND a.Date Between '" + start.ToString("yyyy-MM-dd 00:00:00") + "' And '" + end.ToString("yyyy-MM-dd 23:59:59") + "' "
                       + "Order By a.Date,c.Number "
                       ;
            DataSet ds = new DataSet();
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int rowid = 1;
                Guid lastOrderGuid = new Guid();
                Model_圆片订单 m = new Model_圆片订单 { 序号 = 0 };
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (lastOrderGuid != dr.Field<Guid>("Guid"))
                    {
                        if (lastOrderGuid != new Guid())
                        {
                            data.Add(m);
                        }
                        m = new Model_圆片订单 { 序号 = rowid++ };
                        lastOrderGuid = dr.Field<Guid>("Guid");
                        m.Guid = lastOrderGuid;
                        m.OrderType = inOut;
                        m.单号 = dr.Field<string>("OrderNo");
                        m.下单日期 = dr.Field<DateTime>("Date").ToString("yyyy-MM-dd");
                        m.备注 = dr.Field<string>("Remark");
                        m.编号s += dr.Field<string>("Number");
                        m.直径s += dr.Field<string>("Diameter");
                        m.厚度s += dr.Field<string>("Thickness");
                        m.数量s += inOut == 1 ? dr.Field<Int64?>("Quantity") : -dr.Field<Int64?>("Quantity");
                        m.损耗数量s += dr.Field<Int64?>("LossQuantity");
                        m.半成品数量s += dr.Field<Int64?>("HalfProductQuantity");
                        m.入库半成品编号s += dr.Field<string>("PNumber");
                        m.半成品品名s += dr.Field<string>("PName");
                    }
                    else
                    {
                        m.编号s += "\n" + dr.Field<string>("Number");
                        m.直径s += "\n" + dr.Field<string>("Diameter");
                        m.厚度s += "\n" + dr.Field<string>("Thickness");
                        m.数量s += "\n" + (inOut == 1 ? dr.Field<Int64?>("Quantity") : -dr.Field<Int64?>("Quantity"));
                        m.损耗数量s += "\n" + dr.Field<Int64?>("LossQuantity");
                        m.半成品数量s += "\n" + dr.Field<Int64?>("HalfProductQuantity");
                        m.入库半成品编号s += "\n" + dr.Field<string>("PNumber");
                        m.半成品品名s += "\n" + dr.Field<string>("PName");
                    }
                }
                if (m.序号 != 0)
                {
                    data.Add(m);
                }
            }
            return data;
        }

        public bool Add(Model_圆片订单 data)
        {
            List<string> sqls = new List<string>();
            sqls.Add("Insert into T_Order_Wafer values('" + data.Guid + "'," + data.OrderType + ",'" + data.单号 + "','" + data.下单日期 + "','" + data.备注 + "')");
            foreach (Model_圆片仓库 m in data.明细)
            {
                if (m.Guid != new Guid() && m.数量 != 0)
                {
                    if (data.OrderType == 0)
                    {
                        m.数量 = -m.数量;
                        sqls.Add("Insert into T_Warehouse_HalfProduct(Guid,ProductID,Date,Operator,Quantity,OrderGuid) values('" + Guid.NewGuid() + "','" + m.半成品Guid + "','" + data.下单日期 + "',''," + m.半成品数量 + ",'" + data.Guid + "')");
                    }
                    sqls.Add("Insert into T_Warehouse_Wafer values('" + Guid.NewGuid() + "','" + data.Guid + "','" + m.Guid + "'," + m.数量 + "," + m.损耗数量 + ",'" + m.半成品Guid + "'," + m.半成品数量 + ")");
                }
            }
            if (sqls.Count == 1)//无明细记录返回false
            {
                return false;
            }
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }

        public bool Delete(Guid orderGuid)
        {
            if (orderGuid == new Guid())
            {
                return false;
            }
            List<string> sqls = new List<string>();
            sqls.Add("Delete From T_Order_Wafer Where Guid='" + orderGuid + "'");
            sqls.Add("Delete From T_Warehouse_Wafer Where OrderGuid='" + orderGuid + "'");
            sqls.Add("Delete From T_Warehouse_HalfProduct Where OrderGuid='" + orderGuid + "'");
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }
    }
}
