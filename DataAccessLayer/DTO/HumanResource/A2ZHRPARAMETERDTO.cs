using System;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;

namespace DataAccessLayer.DTO.HumanResource
{
    public class A2ZHRPARAMETERDTO
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


        #endregion

        public static A2ZHRPARAMETERDTO GetParameterValue()
        {
            A2ZHRPARAMETERDTO p = new A2ZHRPARAMETERDTO();

            DataTable dt = CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZHRPARAMETER", "A2ZHRMCUS");

            p.CurrentMonth = Converter.GetSmallInteger(dt.Rows[0]["CurrentMonth"]);
            p.FinancialMonth = Converter.GetSmallInteger(dt.Rows[0]["FinancialMonth"]);
            p.CurrentYear = Converter.GetSmallInteger(dt.Rows[0]["CurrentYear"]);
            p.SystemBatchNo = Converter.GetString(dt.Rows[0]["SystemBatchNo"]);
            p.IncomeCode = Converter.GetLong(dt.Rows[0]["IncomeCode"]);
            p.PLCode = Converter.GetLong(dt.Rows[0]["PLCode"]);
            p.BackupDir = Converter.GetString(dt.Rows[0]["BackupDir"]);
            p.FinancialMonth = Converter.GetSmallInteger(dt.Rows[0]["FinancialMonth"]);
            p.FinancialBegYear = Converter.GetSmallInteger(dt.Rows[0]["FinancialBegYear"]);
            p.FinancialEndYear = Converter.GetSmallInteger(dt.Rows[0]["FinancialEndYear"]);
            p.ProcessDate = Converter.GetDateTime(dt.Rows[0]["ProcessDate"]);

            return p;
        }

        public static int UpdateParameterValue(A2ZHRPARAMETERDTO dto)
        {
            string strQuery = "UPDATE A2ZHRPARAMETER   SET BackupDir ='" + dto.BackupDir + "'  WHERE SystemBatchNo='" + dto.SystemBatchNo + "'";
            return Converter.GetInteger(CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRMCUS"));

        }

        //public static A2ZHRPARAMETERDTO GetParameterValueAAA()
        //{

        //    var p = new A2ZHRPARAMETERDTO();
        //    var s = new A2ZMONTHDTO();

        //    string strQuery = "SELECT DBHKMONTH.MonthName,DBHRPARAM.*FROM A2ZERPHK..A2ZMONTH DBHKMONTH INNER JOIN  A2ZERPHR..A2ZHRPARAMETER DBHRPARAM ON DBHRPARAM.CURRENTMONTH = DBHKMONTH.MONTHNO";
        //    DataTable dt = CommonManager.Instance.GetDataTableByQuery(strQuery,"A2ZERPHR");
        //    p.CurrentMonth = Converter.GetString(s.MonthName);
        //    p.FinancialMonth = Converter.GetSmallInteger(dt.Rows[0]["FinancialMonth"]);
        //    p.CurrentYear = Converter.GetSmallInteger(dt.Rows[0]["CurrentYear"]);
        //    p.SystemBatchNo = Converter.GetString(dt.Rows[0]["SystemBatchNo"]);
        //    p.IncomeCode = Converter.GetLong(dt.Rows[0]["IncomeCode"]);
        //    p.PLCode = Converter.GetLong(dt.Rows[0]["PLCode"]);
        //    p.BackupDir = Converter.GetString(dt.Rows[0]["BackupDir"]);
        //    p.FinancialMonth = Converter.GetSmallInteger(dt.Rows[0]["FinancialMonth"]);
        //    p.FinancialBegYear = Converter.GetSmallInteger(dt.Rows[0]["FinancialBegYear"]);
        //    p.FinancialEndYear = Converter.GetSmallInteger(dt.Rows[0]["FinancialEndYear"]);

        //    return p;
           
        //}

        public static int UpdateSingleUserFlag(Int16 nFlag)
        {
            string sqlQuery = "UPDATE A2ZHRPARAMETER SET SingleUserFlag = " + nFlag;
            return Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZHRMCUS"));
        }
    }
}
