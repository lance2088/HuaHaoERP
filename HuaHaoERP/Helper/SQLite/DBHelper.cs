using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace HuaHaoERP.Helper.SQLite
{
    public class DBHelper
    {
        private string DataSource = "Data\\Data.db";
        private SQLiteConnection _dbConn = new SQLiteConnection();
        private SQLiteCommand _dbCmd = new SQLiteCommand();
        private SQLiteConnectionStringBuilder _dbConnBuilder = new SQLiteConnectionStringBuilder();
        private string _dbPassword = Helper.DataDefinition.CommonParameters.DbPassword;

        internal DBHelper()
        {
            InitializeDbConnect();
        }

        internal void ChangeDBPassword(string Password)
        {
            _dbConn.ChangePassword(Password);
        }
        internal void ClearDBPassword()
        {
            ChangeDBPassword("");
        }

        /// <summary>
        /// 初始化数据库连接 With Password
        /// </summary>
        /// <param name="Password"></param>
        private void InitializeDbConnect()
        {
            _dbConnBuilder.DataSource = DataSource;
            _dbConnBuilder.JournalMode = SQLiteJournalModeEnum.Wal;
            _dbConn.ConnectionString = _dbConnBuilder.ToString();
            _dbConn.SetPassword(_dbPassword);
            try
            {
                _dbConn.Open();
            }
            catch (Exception ee)
            {
                Helper.LogHelper.FileLog.ErrorLog(ee.ToString());
            }
            _dbCmd.Connection = _dbConn;
        }

        /// <summary>
        /// 回收conn
        /// </summary>
        private void ReleaseObject()
        {
            _dbConn.Close();
            _dbConn.Dispose();
            _dbCmd.Dispose();
        }

        internal void Backup(string backupDataSource)
        {
            _dbConnBuilder.DataSource = backupDataSource;
            SQLiteConnection connOut = new SQLiteConnection();
            connOut.ConnectionString = _dbConnBuilder.ToString();
            connOut.SetPassword(_dbPassword);
            connOut.Open();
            _dbConn.BackupDatabase(connOut, "main", "main", -1, null, -1);
            connOut.Close();
            connOut.Dispose();
            ReleaseObject();
        }

        /// <summary>
        /// 事务执行
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        internal bool Transaction(List<string> sqls)
        {
            bool flag = false;
            SQLiteTransaction strans = _dbConn.BeginTransaction();
            try
            {
                foreach (string sql in sqls)
                {
                    _dbCmd.CommandText = sql;
                    _dbCmd.ExecuteNonQuery();
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
            SQLiteTransaction strans = _dbConn.BeginTransaction();
            try
            {
                foreach (string sql in sqls)
                {
                    _dbCmd.CommandText = sql;
                    _dbCmd.ExecuteNonQuery();
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
                _dbCmd.CommandText = sql;
                _dbCmd.ExecuteNonQuery();
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
            SQLiteDataAdapter dAdapter = new SQLiteDataAdapter(sql, _dbConn);
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
                _dbCmd.CommandText = sql;
                SQLiteDataReader reader = _dbCmd.ExecuteReader();
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

        internal string ParametersLogin(string sql, string userName, string password)
        {
            string result = "";
            try
            {
                _dbCmd.CommandText = sql;
                _dbCmd.Parameters.Add(new SQLiteParameter("Name", userName));
                _dbCmd.Parameters.Add(new SQLiteParameter("Password", password));
                SQLiteDataReader reader = _dbCmd.ExecuteReader();
                if (reader.Read())
                {
                    result = reader.GetString(0);
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
            return result;
        }
    }
}
