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
                        AssemblyLineDetailsListModel d = new AssemblyLineDetailsListModel();
                        LastD = d;
                        LastD.Id = id++;
                        LastD.ProductID = (Guid)dr["ProductID"];
                        LastD.ProductNumber = dr["Number"].ToString();
                        LastD.ProductName = dr["Name"].ToString();

                        LastGuid = (Guid)dr["ProductID"];
                    }
                    else//旧的Product，累加
                    {

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
    }
}
