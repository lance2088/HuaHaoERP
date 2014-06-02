using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HuaHaoERP.Model.ProductionManagement;

namespace HuaHaoERP.ViewModel.ProductionManagement
{
    class AssemblyLineDetailsConsole
    {
        internal bool ReadList(out List<AssemblyLineDetailsListModel> data)
        {
            Guid LastGuid = new Guid();
            AssemblyLineDetailsListModel LastD = new AssemblyLineDetailsListModel();
            List<string> ProcessList = new List<string>();

            data = new List<AssemblyLineDetailsListModel>();
            string sql = " SELECT                                                             "
                       + "    a.ProductID,                                                    "
                       + "    b.Number,                                                         "
                       + "    b.Name,                                                          "
                       + "    b.P1,                                                            "
                       + " 	  b.P2,                                                           "
                       + " 	  b.P3,                                                           "
                       + " 	  b.P4,                                                           "
                       + " 	  b.P5,                                                           "
                       + " 	  b.P6,                                                           "
                       + " 	  a.Process,                                                      "
                       + " 	  total(a.Number),                                                "
                       + "    total(a.Break)                                                   "
                       + " FROM                                                               "
                       + " 	  T_PM_ProductionSchedule a                                       "
                       + " left join T_ProductInfo_Product b ON a.ProductID=b.Guid            "
                       + " GROUP BY                                                           "
                       + " 	  a.ProductID,                                                    "
                       + " 	  a.Process                                                       ";
            DataSet ds = new DataSet();
            if(new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (LastGuid != (Guid)dr["ProductID"])//新的Product，新建
                    {
                        if (LastGuid != new Guid())
                        {
                            
                            data.Add(LastD);
                        }
                        ProcessList.Clear();
                        for (int i = 1; i < 7; i++)
                        {
                            ProcessList.Add(dr["P"+i].ToString());
                        }
                        AssemblyLineDetailsListModel d = new AssemblyLineDetailsListModel();
                        LastGuid = (Guid)dr["ProductID"];
                        LastD = d;
                        LastD.Id = id++;
                        LastD.ProductID = (Guid)dr["ProductID"];
                        LastD.ProductNumber = dr["Number"].ToString();
                        LastD.ProductName = dr["Name"].ToString();
                        for (int i = 1; i < 7; i++)
                        {
                            if (dr["Process"].ToString() == dr["P" + i].ToString())
                            {
                                SetPNum(i, int.Parse(dr["total(a.Number)"].ToString()), int.Parse(dr["total(a.Break)"].ToString()), ref LastD);
                            }
                        }
                    }
                    else//旧的Product，累加
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            if (dr["Process"].ToString() == dr["P" + i].ToString())
                            {
                                SetPNum(i, int.Parse(dr["total(a.Number)"].ToString()), int.Parse(dr["total(a.Break)"].ToString()), ref LastD);
                            }
                        }
                    }
                }
                if (ds.Tables[0].Rows.Count != 0)
                {
                    data.Add(LastD);
                }
                return true;
            }

            return false;
        }

        private void SetPNum(int p,int Quantity,int BreakNum, ref AssemblyLineDetailsListModel d)
        {
            switch (p)
            {
                case 1:
                    d.P1Num = Quantity+"/"+BreakNum;
                    break;
                case 2:
                    d.P2Num = Quantity + "/" + BreakNum;
                    break;
                case 3:
                    d.P3Num = Quantity + "/" + BreakNum;
                    break;
                case 4:
                    d.P4Num = Quantity + "/" + BreakNum;
                    break;
                case 5:
                    d.P5Num = Quantity + "/" + BreakNum;
                    break;
                case 6:
                    d.P6Num = Quantity + "/" + BreakNum;
                    break;
            }
        }
    }
}
