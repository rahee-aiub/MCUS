using System;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.GeneralLedger
{
    public class A2ZGLPARAMETERDTO
    {
        #region Properties

        public Int16 ID { set; get; }
        public Int16 FinancialMonth { set; get; }
        public Int16 FinancialBegYear { set; get; }
        public Int16 FinancialEndYear { set; get; }
        public Int16 CurrentMonth { set; get; }
        public Int16 CurrentYear { set; get; }
        public String SystemBatchNo { set; get; }
        public String TransactionBatchNo { set; get; }
        public DateTime LastVoucherDate { set; get; }
        public String LastVoucherNo { set; get; }
        public DateTime LastUpdateDate { set; get; }
        public Int64 IncomeCode { set; get; }
        public Int64 UDPLCode { set; get; }
        public int PLCode { set; get; }
        public String DataDir { set; get; }
        public String BackupDir { set; get; }
        public Int16 NumberOfUser { set; get; }
        public Int16 SingleUserFlag { set; get; }
        public DateTime ProcessDate { set; get; }
        public Int16 ProcessStatus { set; get; }
        public Int16 BackupStatus { set; get; }
        public DateTime InstallDate { set; get; }
        public int CashCode { set; get; }
        public Int16 ApprovBy { set; get; }
        public DateTime ApprovByDate { set; get; }
      
        #endregion

        public static A2ZGLPARAMETERDTO GetParameterValue()
        {
            A2ZGLPARAMETERDTO p = new A2ZGLPARAMETERDTO();

            DataTable dt = CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZGLPARAMETER", "A2ZGLMCUS");

            p.FinancialMonth = Utility.Converter.GetSmallInteger(dt.Rows[0]["FinancialMonth"]);
            p.FinancialBegYear = Utility.Converter.GetSmallInteger(dt.Rows[0]["FinancialBegYear"]);
            p.FinancialEndYear = Utility.Converter.GetSmallInteger(dt.Rows[0]["FinancialEndYear"]);
            p.CurrentMonth = Utility.Converter.GetSmallInteger(dt.Rows[0]["CurrentMonth"]);
            p.CurrentYear = Utility.Converter.GetSmallInteger(dt.Rows[0]["CurrentYear"]);                                    
            p.LastVoucherNo = Utility.Converter.GetString(dt.Rows[0]["LastVoucherNo"]);
            p.LastUpdateDate = Utility.Converter.GetDateTime(dt.Rows[0]["LastUpdateDate"]);            
            p.NumberOfUser = Utility.Converter.GetSmallInteger(dt.Rows[0]["NumberOfUser"]);
            p.SingleUserFlag = Utility.Converter.GetSmallInteger(dt.Rows[0]["SingleUserFlag"]);            
            p.ProcessDate = Utility.Converter.GetDateTime(dt.Rows[0]["ProcessDate"]);
            p.ProcessStatus = Utility.Converter.GetSmallInteger(dt.Rows[0]["ProcessStatus"]);
            p.BackupStatus = Utility.Converter.GetSmallInteger(dt.Rows[0]["BackupStatus"]);
            p.InstallDate = Utility.Converter.GetDateTime(dt.Rows[0]["InstallDate"]);
            p.UDPLCode = Utility.Converter.GetInteger(dt.Rows[0]["UnDisPLGLCode"]);
            p.PLCode = Utility.Converter.GetInteger(dt.Rows[0]["PLGLCode"]);
            p.CashCode = Utility.Converter.GetInteger(dt.Rows[0]["CashCode"]);
            p.ApprovBy = Utility.Converter.GetSmallInteger(dt.Rows[0]["ApprovBy"]);
            p.ApprovByDate = Utility.Converter.GetDateTime(dt.Rows[0]["ApprovByDate"]);
            
            return p;
        }

        public static int UpdateSingleUserFlag(Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZGLPARAMETER SET SingleUserFlag = " + nFlag;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZGLMCUS"));
        }


        public static int UpdateInformation(A2ZGLPARAMETERDTO dto)
        {
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZGLPARAMETER SET FinancialMonth='" + dto.FinancialMonth + "',PLGLCode='" + dto.PLCode + "',UnDisPLGLCode='" + dto.UDPLCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLMCUS"));
            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
