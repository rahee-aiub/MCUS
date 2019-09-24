using System;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.Invertory
{
    public class A2ZINVPARAMETERDTO
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

        #endregion

        public static A2ZINVPARAMETERDTO GetParameterValue()
        {
            A2ZINVPARAMETERDTO p = new A2ZINVPARAMETERDTO();

            DataTable dt = CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZINVPARAMETER", "A2ZCSMCUS");

            p.FinancialMonth = Utility.Converter.GetSmallInteger(dt.Rows[0]["FinancialMonth"]);
            p.FinancialBegYear = Utility.Converter.GetSmallInteger(dt.Rows[0]["FinancialBegYear"]);
            p.FinancialEndYear = Utility.Converter.GetSmallInteger(dt.Rows[0]["FinancialEndYear"]);
            p.CurrentMonth = Utility.Converter.GetSmallInteger(dt.Rows[0]["CurrentMonth"]);
            p.CurrentYear = Utility.Converter.GetSmallInteger(dt.Rows[0]["CurrentYear"]);
            p.SystemBatchNo = Utility.Converter.GetString(dt.Rows[0]["SystemBatchNo"]);
            p.TransactionBatchNo = Utility.Converter.GetString(dt.Rows[0]["TransactionBatchNo"]);
            p.LastVoucherDate = Utility.Converter.GetDateTime(dt.Rows[0]["LastVoucherDate"]);
            p.LastVoucherNo = Utility.Converter.GetString(dt.Rows[0]["LastVoucherNo"]);
            p.LastUpdateDate = Utility.Converter.GetDateTime(dt.Rows[0]["LastUpdateDate"]);            
            p.IncomeCode = Utility.Converter.GetLong(dt.Rows[0]["IncomeCode"]);
            p.PLCode = Utility.Converter.GetLong(dt.Rows[0]["PLCode"]);
            p.DataDir = Utility.Converter.GetString(dt.Rows[0]["DataDir"]);
            p.BackupDir = Utility.Converter.GetString(dt.Rows[0]["BackupDir"]);
            p.NumberOfUser = Utility.Converter.GetSmallInteger(dt.Rows[0]["NumberOfUser"]);
            p.SingleUserFlag = Utility.Converter.GetSmallInteger(dt.Rows[0]["SingleUserFlag"]);            
            p.ProcessDate = Utility.Converter.GetDateTime(dt.Rows[0]["ProcessDate"]);
            p.ProcessStatus = Utility.Converter.GetSmallInteger(dt.Rows[0]["ProcessStatus"]);
            p.BackupStatus = Utility.Converter.GetSmallInteger(dt.Rows[0]["BackupStatus"]);
            p.InstallDate = Utility.Converter.GetDateTime(dt.Rows[0]["InstallDate"]);
            p.ApprovBy = Utility.Converter.GetSmallInteger(dt.Rows[0]["ApprovBy"]);
            p.ApprovByDate = Utility.Converter.GetDateTime(dt.Rows[0]["ApprovByDate"]);
            p.LastDOSNo = Utility.Converter.GetInteger(dt.Rows[0]["LastDOSNo"]);
            p.LastPOSNo = Utility.Converter.GetInteger(dt.Rows[0]["LastPOSNo"]);
            p.LastGRNNoRaw = Utility.Converter.GetInteger(dt.Rows[0]["LastGRNNoRaw"]);
            p.LastGRNNoPacking = Utility.Converter.GetInteger(dt.Rows[0]["LastGRNNoPacking"]);
            p.LastChallanNo = Utility.Converter.GetInteger(dt.Rows[0]["LastChallanNo"]);
            p.LastPPICNo = Utility.Converter.GetInteger(dt.Rows[0]["LastPPICNo"]);
            p.LastRequireNo = Utility.Converter.GetInteger(dt.Rows[0]["LastRequireNo"]);
            p.LastOrderNo = Utility.Converter.GetInteger(dt.Rows[0]["LastOrderNo"]);

            return p;
        }

        public static int UpdateSingleUserFlag(Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZINVPARAMETER SET SingleUserFlag = " + nFlag;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZCSMCUS"));
        }
    }
}
