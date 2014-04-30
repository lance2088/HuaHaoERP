using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace HuaHaoERP.Helper.Excel
{
    class Reader
    {
        /// <summary>
        /// 读取Excel表的内容到DataSet
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="sheetname"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public bool ReadExcelDataSource(string filepath, string sheetname, out DataSet ds)
        {
            ds = new DataSet();
            if (!File.Exists(filepath))
            {
                LogHelper.FileLog.Log("ReadExcelDataSource:" + filepath + " Not Exists");
                return false;
            }
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + sheetname + "$]", strConn);
            try
            {
                oada.Fill(ds);
            }
            catch (Exception ee)
            {
                LogHelper.FileLog.Log(ee.ToString());
                return false;
            }
            finally
            {
                oada.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return true;
        }
    }
}
