using System;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer.Constants;

namespace DataAccessLayer.Conn
{
    public class Sqlconn
    {
        private static Sqlconn _sqlconn;
        private static object _sync = new object();
        public static Sqlconn Instance
        {
            get
            {
                if (_sqlconn == null)
                {
                    {
                        lock (_sync)
                        {
                            if (_sqlconn == null)
                            {
                                _sqlconn = new Sqlconn();
                            }
                        }
                    }
                }
                return _sqlconn;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConn()
        {
            return new SqlConnection(GetConnString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public SqlConnection GetConn(DBConstants.DBName dbName)
        {
            return new SqlConnection(GetConnString(dbName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetConnString()
        {
            return DBConstants.GetConnectionString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public string GetConnString(DBConstants.DBName dbName)
        {
            return DBConstants.GetConnectionString(dbName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sqlQuery)
        {
            object success = null;
            using (SqlConnection cn = new SqlConnection(GetConnString()))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, cn);
                    cmd.CommandTimeout = 0;                    
                    success = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public object ExecuteNonQuery(string sqlQuery)
        {
            object success = null;
            using (SqlConnection cn = new SqlConnection(GetConnString()))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, cn);
                    cmd.CommandTimeout = 0;      
                    success = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public int InsertDataIntoTable(string sqlQuery)
        {
            int effectedRow = -1;
            using (SqlConnection cn = new SqlConnection(GetConnString()))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, cn);
                    cmd.CommandTimeout = 0;      
                    effectedRow = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
            return effectedRow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public bool DeleteDataFromTable(string sqlQuery)
        {
            bool success = false;
            using (SqlConnection cn = new SqlConnection(GetConnString()))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, cn);
                    cmd.CommandTimeout = 0;      
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                        success = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public bool UpdateDataFromTable(string sqlQuery)
        {
            bool success = false;
            using (SqlConnection cn = new SqlConnection(GetConnString()))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, cn);
                    cmd.CommandTimeout = 0;      
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                        success = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public DataTable GetDataThroughDataTable(string sqlQuery)
        {
            DataTable dt = null;
            using (SqlConnection cn = new SqlConnection(GetConnString()))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    SqlDataAdapter dataAdapterObj = new SqlDataAdapter(sqlQuery, cn);
                    dt = new DataTable();
                    dataAdapterObj.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();
                }
            }
            return dt;
        }

    }
}
