using HuaHaoERP.Model.ProductionManagement;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class ProductionBookkeepingConsole
    {
        internal bool ReadData(string Parm, out ObservableCollection<Model_ProductionBookkeeping> data)
        {
            data = new ObservableCollection<Model_ProductionBookkeeping>();
            return ReadData("0000", Parm, out data);
        }

        internal bool ReadData(string DateStr, string Parm, out ObservableCollection<Model_ProductionBookkeeping> data)
        {
            string DateParm = "";
            if (!DateStr.StartsWith("0000"))
            {
                DateParm = " AND a.AddDate BETWEEN '" + DateStr + "' AND datetime('" + DateStr + "','+1 day') ";
            }
            data = new ObservableCollection<Model_ProductionBookkeeping>();
            Model_ProductionBookkeeping m;
            string sql = "Select a.*,b.Number,b.Name,b.P1,b.P2,b.P3,b.P4,b.P5,b.P6 "
                        + " from T_PM_ProductionBookkeeping a"
                        + " Left Join T_ProductInfo_Product b ON a.ProductID=b.Guid"
                        + " Where a.DeleteMark ISNULL"
                        + " AND b.Number LIKE '%" + Parm + "%' "
                        + DateParm
                        + " Order By a.AddDate"
                        ;
            DataSet ds = new DataSet();
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m = new Model_ProductionBookkeeping();
                    m.Guid = (Guid)dr["Guid"];
                    m.ProductGuid = (Guid)dr["ProductID"];
                    m.ProductNumber = dr["Number"].ToString();
                    m.ProductName = dr["Name"].ToString();

                    for (int i = 1; i < 7; i++)
                    {
                        m.ProductProcess[i - 1] = dr["P" + i].ToString();
                        if (dr["P" + i].ToString() != "无")
                        {
                            m.ProductProcessStr += dr["P" + i].ToString() + "-";
                        }
                    }
                    m.ProductProcessStr = m.ProductProcessStr.Substring(0, m.ProductProcessStr.Length - 1);

                    m.P1Num = int.Parse(dr["P1Num"].ToString());
                    m.P2Num = int.Parse(dr["P2Num"].ToString());
                    m.P3Num = int.Parse(dr["P3Num"].ToString());
                    m.P4Num = int.Parse(dr["P4Num"].ToString());
                    m.P5Num = int.Parse(dr["P5Num"].ToString());
                    m.P6Num = int.Parse(dr["P6Num"].ToString());

                    m.P1Diff = m.P2Num - m.P1Num;
                    m.P2Diff = m.P3Num - m.P2Num;
                    m.P3Diff = m.P4Num - m.P3Num;
                    m.P4Diff = m.P5Num - m.P4Num;
                    m.P5Diff = m.P6Num - m.P5Num;

                    m.IsTurn = int.Parse(dr["IsTurn"].ToString());
                    m.DisPlayIsTurn = m.IsTurn == 0 ? "否" : "是";
                    m.Remark = dr["Remark"].ToString();
                    m.AddDate = Convert.ToDateTime(dr["AddDate"].ToString()).ToString("yyyy-MM-dd");
                    data.Add(m);
                }
            }
            if (data.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool Update(Model_ProductionBookkeeping m)
        {
            string sql = "Update T_PM_ProductionBookkeeping Set "
                        + " ProductID='" + m.ProductGuid + "', "
                        + " P1Num=" + m.P1Num + ", "
                        + " P2Num=" + m.P2Num + ", "
                        + " P3Num=" + m.P3Num + ", "
                        + " P4Num=" + m.P4Num + ", "
                        + " P5Num=" + m.P5Num + ", "
                        + " P6Num=" + m.P6Num + ", "
                        + " Remark='" + m.Remark + "' "
                        + " Where Guid='" + m.Guid + "' AND DeleteMark ISNULL";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }

        internal bool Delete(Guid GUID)
        {
            string sql = "Update T_PM_ProductionBookkeeping SET DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Guid='" + GUID + "'";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }

        internal bool Add(DateTime dt, Guid ProductID)
        {
            string sql = "Insert into T_PM_ProductionBookkeeping(Guid,OrderNum,ProductID,AddDate) "
                        + " values('" + Guid.NewGuid() + "','" + dt.ToString("yyyyMMddHHmmss") + "','" + ProductID + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }

        internal Model_Product ReadProduct(string Number)
        {
            Model_Product m = new Model_Product();
            string sql = "Select Guid,Name From T_ProductInfo_Product Where Number='" + Number + "' AND DeleteMark ISNULL";
            DataSet ds = new DataSet();
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    m.Guid = (Guid)dr["Guid"];
                    m.Number = Number;
                    m.Name = dr["Name"].ToString();
                }
            }
            return m;
        }

        /// <summary>
        /// 更新状态，表示已经入了半成品库
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        internal bool UpdateTurn(Model_ProductionBookkeeping m)
        {
            string sql = "update T_PM_ProductionBookkeeping set isTurn = 1 Where Guid='" + m.Guid + "'";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }
    }
}
