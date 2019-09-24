using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.BLL;
namespace DataAccessLayer.DTO.CustomerServices

{
  public  class A2ZPENSIONDEFAULTERDTO
    {
        #region Propertise

        public DateTime TrnDate { set; get; }
        public DateTime Opendate { set; get; }
        public DateTime ProcDate { set; get; }
        public Int16 CuType { set; get; }
        public int CuNo { set; get; }
        public int MemNo { set; get; }
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public decimal TotalDepositAmt { set; get; }
        public decimal CalDepositAmt { set; get; }

        public decimal CalPaidDeposit { set; get; }
        public decimal CalPaidInterest { set; get; }
        public decimal CalPaidPenal { set; get; }
        
        public decimal UptoDueDepositAmt { set; get; }
        
        public decimal PayableDepositAmt { set; get; }
       
        public decimal PayablePenalAmt { set; get; }
        public decimal PaidDepositAmt { set; get; }
       
        public decimal PaidPenalAmt { set; get; }
        public decimal DueDepositAmt { set; get; }
        public decimal CurrDueDepositAmt { set; get; }
        public decimal UptoDepositAmt { set; get; }
        public decimal TargetDepositAmt { set; get; }
        public decimal NoMonth { set; get; }
        public int UMonth { set; get; }
        public int UptoYear { set; get; }
        public String UptoMonth { set; get; }

        public String UptoDate { set; get; }

        public int DepositFlag { set; get; }

        public int Period { set; get; }
        public decimal MthDeposit { set; get; }
        
        public String Remarks { set; get; }
        
        #endregion


        
        public static A2ZPENSIONDEFAULTERDTO GetPensionDepInformation(DateTime TrnDate, Int16 CuType, int CuNo, int MemNo, int AccType, Int64 AccNo)
        {

            var prm1 = new object[1];
            prm1[0] = AccNo;

            DataTable dt2 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGenerateTodaysPensionDeposit", prm1, "A2ZCSMCUS");

            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
                    AccNo + "' and CuType='" + CuType + "' and CuNo='" + CuNo + "' and MemNo='" + MemNo + "'", "A2ZCSMCUS");


            var p = new A2ZPENSIONDEFAULTERDTO();
            if (dt.Rows.Count > 0)
            {
                p.TotalDepositAmt = Converter.GetDecimal(dt.Rows[0]["AccTotalDep"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.MthDeposit = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.Period = Converter.GetInteger(dt.Rows[0]["AccPeriod"]);

                p.CalPaidDeposit = Converter.GetDecimal(dt.Rows[0]["CalPaidDeposit"]);
                p.CalPaidPenal = Converter.GetDecimal(dt.Rows[0]["CalPaidPenal"]);
            }
             
            
            var prm = new object[6];

            prm[0] = TrnDate;
            prm[1] = CuType;
            prm[2] = CuNo;
            prm[3] = MemNo;
            prm[4] = AccType;
            prm[5] = AccNo;

            DataTable dt1 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoPensionDefaulter", prm, "A2ZCSMCUS");
                   
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDESIGNATION WHERE DesigCode = " + Designcode, "A2ZCSMCUS");


            //var p = new A2ZPENSIONDEFAULTERDTO();
            if (dt1.Rows.Count > 0)
            {

                p.TrnDate = Converter.GetDateTime(dt1.Rows[0]["TrnDate"]);
                p.CuType = Converter.GetSmallInteger(dt1.Rows[0]["CuType"]);
                p.CuNo = Converter.GetInteger(dt1.Rows[0]["CuNo"]);
                p.MemNo = Converter.GetInteger(dt1.Rows[0]["MemNo"]);
                p.AccType = Converter.GetSmallInteger(dt1.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt1.Rows[0]["AccNo"]);
                p.CalDepositAmt = Converter.GetDecimal(dt1.Rows[0]["CalDepositAmt"]);  
                p.UptoDueDepositAmt = Converter.GetDecimal(dt1.Rows[0]["UptoDueDepositAmt"]);
                p.PayableDepositAmt = Converter.GetDecimal(dt1.Rows[0]["PayableDepositAmt"]);
                p.PayablePenalAmt = Converter.GetDecimal(dt1.Rows[0]["PayablePenalAmt"]);
                p.PaidDepositAmt = Converter.GetDecimal(dt1.Rows[0]["PaidDepositAmt"]);
                p.PaidPenalAmt = Converter.GetDecimal(dt1.Rows[0]["PaidPenalAmt"]);
                p.CurrDueDepositAmt = Converter.GetDecimal(dt1.Rows[0]["CurrDueDepositAmt"]);
                p.DepositFlag = Converter.GetSmallInteger(dt1.Rows[0]["DepositFlag"]);


                p.CurrDueDepositAmt = (p.CurrDueDepositAmt - p.CalPaidDeposit);


                p.TargetDepositAmt = (p.MthDeposit * p.Period);

                if (p.TotalDepositAmt == p.TargetDepositAmt)
                {
                    p.DepositFlag = 1;
                }

                p.UptoDepositAmt = Converter.GetDecimal(Math.Abs(p.CurrDueDepositAmt));

                if (p.UptoDepositAmt > 0)
                {
                    p.DueDepositAmt = (p.UptoDepositAmt - p.CalDepositAmt);
                }
                else 
                {
                    p.DueDepositAmt = 0;
                }

                
                p.NoMonth = Converter.GetDecimal(p.TotalDepositAmt / p.MthDeposit);

                if (p.NoMonth == 0)
                {
                    p.UptoDate = "";
                }
                else
                {
                    p.NoMonth = p.NoMonth - 1;
                    if (p.NoMonth < 1)
                    {
                        p.NoMonth = 0;
                    }
                    DateTime Depdate = new DateTime();
                    Depdate = p.Opendate.AddMonths(Converter.GetSmallInteger(p.NoMonth));
                    p.UptoDate = String.Format("{0:Y}", Depdate);
                }
               

               

                
                return p;
            }
            p.CuType = 0;

            return p;

        }
        
    }
}
