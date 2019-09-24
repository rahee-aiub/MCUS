using System;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZCSPARAMETERDTO
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
        public Int64 PLCode { set; get; }
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
        public Int32 LastDOSNo { set; get; }
        public Int32 LastPOSNo { set; get; }
        public Int32 LastGRNNoRaw { set; get; }
        public Int32 LastGRNNoPacking { set; get; }
        public Int32 LastChallanNo { set; get; }
        public Int32 LastPPICNo { set; get; }
        public Int32 LastRequireNo { set; get; }
        public Int32 LastOrderNo { set; get; }
        public DateTime DummyProcessDate { set; get; }

        #endregion

        public static A2ZCSPARAMETERDTO GetParameterValue()
        {
            A2ZCSPARAMETERDTO p = new A2ZCSPARAMETERDTO();

            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoParameter", "A2ZCSMCUS");

            DataTable dt = CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAMETER", "A2ZCSMCUS");

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
            p.CashCode = Utility.Converter.GetInteger(dt.Rows[0]["CashCode"]);
            p.ApprovBy = Utility.Converter.GetSmallInteger(dt.Rows[0]["ApprovBy"]);
            p.ApprovByDate = Utility.Converter.GetDateTime(dt.Rows[0]["ApprovByDate"]);
            p.DummyProcessDate = Utility.Converter.GetDateTime(dt.Rows[0]["DummyProcessDate"]);
            
            return p;
        }

        public static int UpdateSingleUserFlag(Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZCSPARAMETER SET SingleUserFlag = " + nFlag;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
        }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
