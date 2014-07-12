using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace HuaHaoERP.Helper.SQLite
{
    public class DBHelper
    {
        private string DataSource = "Data\\Data.db";
        private SQLiteConnection conn = new SQLiteConnection();
        private SQLiteCommand cmd = new SQLiteCommand();

        internal DBHelper()
        {
            InitializeDbConnect();
        }

        internal void ChangeDBPassword(string Password)
        {
            conn.ChangePassword(Password);
        }
        internal void ClearDBPassword()
        {
            ChangeDBPassword("");
        }

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        private void InitializeDbConnect()
        {
            if (Helper.DataDefinition.CommonParameters.DbPassword == "")
            {
                InitializeDbConnect("");
            }
            else
            {
                InitializeDbConnect(Helper.DataDefinition.CommonParameters.DbPassword);
            }
        }
        /// <summary>
        /// 初始化数据库连接 With Password
        /// </summary>
        /// <param name="Password"></param>
        private void InitializeDbConnect(string Password)
        {
            SQLiteConnectionStringBuilder connBuilder = new SQLiteConnectionStringBuilder();
            connBuilder.DataSource = DataSource;
            conn.ConnectionString = connBuilder.ToString();
            conn.SetPassword(Password);
            try
            {
                conn.Open();
            }
            catch (Exception ee)
            {
                Helper.LogHelper.FileLog.ErrorLog(ee.ToString());
            }
            cmd.Connection = conn;
        }
        /// <summary>
        /// 回收conn
        /// </summary>
        private void ReleaseObject()
        {
            conn.Close();
            conn.Dispose();
        }

        /// <summary>
        /// 事务执行
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        internal bool Transaction(List<string> sqls)
        {
            bool flag = false;
            SQLiteTransaction strans = conn.BeginTransaction();
            try
            {
                foreach (string sql in sqls)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                strans.Commit();
                flag = true;
            }
            catch (Exception ee)
            {
                strans.Rollback();
                LogHelper.FileLog.ErrorLog(ee.ToString());
            }
            finally
            {
                ReleaseObject();
            }
            return flag;
        }

        internal bool Transaction(string[] sqls)
        {
            bool flag = false;
            SQLiteTransaction strans = conn.BeginTransaction();
            try
            {
                foreach (string sql in sqls)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                strans.Commit();
                flag = true;
            }
            catch (Exception ee)
            {
                strans.Rollback();
                LogHelper.FileLog.ErrorLog(ee.ToString());
            }
            finally
            {
                ReleaseObject();
            }
            return flag;
        }

        /// <summary>
        /// 单次执行语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal bool SingleExecution(string sql)
        {
            bool flag = false;
            try
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ee)
            {
                LogHelper.FileLog.ErrorLog(ee.ToString());
            }
            finally
            {
                ReleaseObject();
            }
            return flag;
        }

        /// <summary>
        /// 查询,out DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal bool QueryData(string sql, out DataSet ds)
        {
            bool flag = false;
            ds = new DataSet();
            SQLiteDataAdapter dAdapter = new SQLiteDataAdapter(sql, conn);
            try
            {
                dAdapter.Fill(ds);
                flag = true;
            }
            catch (Exception ee)
            {
                LogHelper.FileLog.ErrorLog(ee.ToString());
            }
            finally
            {
                ReleaseObject();
            }
            return flag;
        }

        /// <summary>
        /// 查询单个结果
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        internal bool QuerySingleResult(string sql, out object result)
        {
            bool flag = false;
            result = new object();
            try
            {
                cmd.CommandText = sql;
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = reader.GetValue(0);
                    flag = true;
                }
            }
            catch (Exception ee)
            {
                LogHelper.FileLog.ErrorLog(ee.ToString());
            }
            finally
            {
                ReleaseObject();
            }
            return flag;
        }
    }
}
