using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DataAccessLayer.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.Helper;

namespace DataAccessLayer.DAO
{
    public class CommonDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySp(string spName, string dbName)
        {
            DataTable dtResults = new DataTable();
            try
            {
                dtResults = SqlHelper.ExecuteDataTable(DBConstants.GetConnectionString(dbName), CommandType.StoredProcedure, spName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DataTable GetDataTableByQuery(string strQuery, string dbName)
        {
            DataTable dtResults = new DataTable();

            try
            {
                dtResults = SqlHelper.ExecuteDataTable(DBConstants.GetConnectionString(dbName), CommandType.Text, strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="dropList"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DropDownList FillDropDownList(string strQuery, DropDownList dropList, string dbName)
        {

            DataTable dt = GetDataTableByQuery(strQuery, dbName);

            dropList.DataTextField = dt.Columns[1].ColumnName.ToString();
            dropList.DataValueField = dt.Columns[0].ColumnName.ToString();
            dropList.DataSource = dt;
            dropList.DataBind();

            if (dt.Rows.Count > 0)
            {
                dropList.Items.Insert(0, "-Select-");    
            }
            
            return dropList;
        }

        public DropDownList FillDropDownListWithSelect(string strQuery, DropDownList dropList, string dbName)
        {

            DataTable dt = GetDataTableByQuery(strQuery, dbName);

            dropList.DataTextField = dt.Columns[1].ColumnName.ToString();
            dropList.DataValueField = dt.Columns[0].ColumnName.ToString();
            dropList.DataSource = dt;
            dropList.DataBind();

            dropList.Items.Insert(0, "-Select-");
            
            return dropList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="gridList"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public GridView FillGridViewList(string strQuery, GridView gridList, string dbName)
        {
            DataTable dt = GetDataTableByQuery(strQuery, dbName);
            gridList.DataSource = dt;
            gridList.DataBind();

            return gridList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sparams"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySpWithParams(string spName, object[] sparams, string dbName)
        {
            DataTable dtResults = new DataTable();
            try
            {
                dtResults = SqlHelper.ExecuteDataTable(DBConstants.GetConnectionString(dbName), spName, sparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public object GetScalarValueByQuery(string strQuery, string dbName)
        {
            object objValue;
            try
            {
                objValue = SqlHelper.ExecuteScalar(DBConstants.GetConnectionString(dbName), CommandType.Text, strQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public object GetScalarValueBySp(string spName, string dbName)
        {
            object objValue;
            try
            {
                objValue = SqlHelper.ExecuteScalar(DBConstants.GetConnectionString(dbName), CommandType.StoredProcedure, spName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sparams"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public object GetScalarValueBySp(string spName, object[] sparams, string dbName)
        {
            object objValue;
            try
            {
                objValue = SqlHelper.ExecuteScalar(DBConstants.GetConnectionString(dbName), spName, sparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objValue;
        }

        /// <summary>
        /// Saaif:20130910. Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="dbName"></param>
        /// <returns>The number of rows affected.</returns>
        internal object ExecuteNonQuery(string sqlQuery, string dbName)
        {
            object objValue;
            try
            {
                objValue = SqlHelper.ExecuteNonQuery(DBConstants.GetConnectionString(dbName), CommandType.Text, sqlQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objValue;
        }

        /// <summary>
        /// Saaif: 20131003
        /// </summary>
        /// <param name="transactionList"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        internal bool ExecuteSqlTransaction(List<SQLTransaction> transactionList, string dbName)
        {
            bool result = true;
            try
            {
                String strConnectionString = DBConstants.GetConnectionString(dbName);
                {
                    using (SqlConnection con = new SqlConnection(strConnectionString))
                    {
                        con.Open();
                        using (SqlTransaction transaction = con.BeginTransaction())
                        {
                            try
                            {
                                int dbResult = -1;
                                foreach (SQLTransaction sqlTransaction in transactionList)
                                {
                                    dbResult = -1;
                                    dbResult = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, sqlTransaction.SqlQuery);
                                    if (dbResult >= 0)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        result = false;
                                        break;
                                    }
                                }

                                if (dbResult > 0 && result)
                                {
                                    transaction.Commit();
                                }
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                result = false;
                                throw ex;
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
