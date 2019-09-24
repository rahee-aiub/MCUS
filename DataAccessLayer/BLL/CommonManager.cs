using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DataAccessLayer.DAO;
using DataAccessLayer.DTO;

namespace DataAccessLayer.BLL
{
    public class CommonManager
    {
        private static CommonManager _CommonManager;
        private static object _sync = new object();
        public static CommonManager Instance
        {
            get
            {
                if (_CommonManager == null)
                {
                    {
                        lock (_sync)
                        {
                            if (_CommonManager == null)
                            {
                                _CommonManager = new CommonManager();
                            }
                        }
                    }
                }
                return _CommonManager;
            }
        }

        CommonDAO _objDAO;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySp(string spName, string dbName)
        {
            _objDAO = new CommonDAO();
            return _objDAO.GetDataTableBySp(spName, dbName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DataTable GetDataTableByQuery(string strQuery, string dbName)
        {
            _objDAO = new CommonDAO();
            return _objDAO.GetDataTableByQuery(strQuery, dbName);
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
            _objDAO = new CommonDAO();
            return _objDAO.FillDropDownList(strQuery, dropList, dbName);
        }

        public DropDownList FillDropDownListWithSelect(string strQuery, DropDownList dropList, string dbName)
        {
            _objDAO = new CommonDAO();
            return _objDAO.FillDropDownListWithSelect(strQuery, dropList, dbName);
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
            _objDAO = new CommonDAO();
            return _objDAO.FillGridViewList(strQuery, gridList, dbName);
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
            _objDAO = new CommonDAO();
            return _objDAO.GetDataTableBySpWithParams(spName, sparams, dbName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public object GetScalarValueByQuery(string strQuery, string dbName)
        {
            _objDAO = new CommonDAO();
            return _objDAO.GetScalarValueByQuery(strQuery, dbName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public object GetScalarValueBySp(string spName, string dbName)
        {
            _objDAO = new CommonDAO();
            return _objDAO.GetScalarValueBySp(spName, dbName);
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
            _objDAO = new CommonDAO();
            return _objDAO.GetScalarValueBySp(spName, sparams, dbName);
        }

        /// <summary>
        /// Saaif:20130910. Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="dbName"></param>
        /// <returns>The number of rows affected.</returns>
        public object ExecuteNonQuery(string sqlQuery, string dbName)
        {
            _objDAO = new CommonDAO();
            return _objDAO.ExecuteNonQuery(sqlQuery, dbName);
        }

        /// <summary>
        /// Saaif: 20131003
        /// </summary>
        /// <param name="transactionList"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public bool ExecuteSqlTransaction(List<SQLTransaction> transactionList, string dbName)
        {
            _objDAO = new CommonDAO();
            return _objDAO.ExecuteSqlTransaction(transactionList, dbName);
        }

        internal DataTable GetDataTableBySpWithParams(string p1, string p2)
        {
            throw new System.NotImplementedException();
        }
    }
}
