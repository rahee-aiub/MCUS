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
  public  class A2ZLOANDEFAULTERDTO
    {
        #region Propertise

        public DateTime TrnDate { set; get; }
        public Int16 CuType { set; get; }
        public int CuNo { set; get; }
        public int MemNo { set; get; }
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public decimal CalPrincAmt { set; get; }
        public decimal CalIntAmt { set; get; }
        public decimal UptoDuePrincAmt { set; get; }
        public decimal UptoDueIntAmt { set; get; }
        public decimal PayablePrincAmt { set; get; }
        public decimal PayableIntAmt { set; get; }
        public decimal PayablePenalAmt { set; get; }
        public decimal PaidPrincAmt { set; get; }
        public decimal PaidIntAmt { set; get; }
        public decimal PaidPenalAmt { set; get; }
        public decimal CurrDuePrincAmt { set; get; }
        public decimal CurrDueIntAmt { set; get; }
        public String Remarks { set; get; }
        public decimal CalPaidDeposit { set; get; }
        public decimal CalPaidInterest { set; get; }
        public decimal CalPaidPenal { set; get; }
        
        #endregion


        
        public static A2ZLOANDEFAULTERDTO GetLoanInformation(DateTime TrnDate, Int16 CuType, int CuNo, int MemNo, int AccType, Int64 AccNo)
        {
            var prm1 = new object[1];
            prm1[0] = AccNo;

            DataTable dt0 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGenerateTodaysLoanDeposit", prm1, "A2ZCSMCUS");

            DataTable dt1 = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
                    AccNo + "' and CuType='" + CuType + "' and CuNo='" + CuNo + "' and MemNo='" + MemNo + "'", "A2ZCSMCUS");


            var p = new A2ZLOANDEFAULTERDTO();
            if (dt1.Rows.Count > 0)
            {
                p.CalPaidDeposit = Converter.GetDecimal(dt1.Rows[0]["CalPaidDeposit"]);
                p.CalPaidInterest = Converter.GetDecimal(dt1.Rows[0]["CalPaidInterest"]);
                p.CalPaidPenal = Converter.GetDecimal(dt1.Rows[0]["CalPaidPenal"]);
            }
   
            var prm = new object[6];

            prm[0] = TrnDate;
            prm[1] = CuType;
            prm[2] = CuNo;
            prm[3] = MemNo;
            prm[4] = AccType;
            prm[5] = AccNo;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoLoanDefaulter", prm, "A2ZCSMCUS");
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZDESIGNATION WHERE DesigCode = " + Designcode, "A2ZCSMCUS");


          
            if (dt.Rows.Count > 0)
            {

                p.TrnDate = Converter.GetDateTime(dt.Rows[0]["TrnDate"]);
                p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
                p.CuNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.MemNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.AccType = Converter.GetSmallInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.CalPrincAmt = Converter.GetDecimal(dt.Rows[0]["CalPrincAmt"]);
                p.CalIntAmt = Converter.GetDecimal(dt.Rows[0]["CalIntAmt"]);
                p.UptoDuePrincAmt = Converter.GetDecimal(dt.Rows[0]["UptoDuePrincAmt"]);
                p.UptoDueIntAmt = Converter.GetDecimal(dt.Rows[0]["UptoDueIntAmt"]);
                p.PayablePrincAmt = Converter.GetDecimal(dt.Rows[0]["PayablePrincAmt"]);
                p.PayableIntAmt = Converter.GetDecimal(dt.Rows[0]["PayableIntAmt"]);
                p.PayablePenalAmt = Converter.GetDecimal(dt.Rows[0]["PayablePenalAmt"]);
                p.PaidPrincAmt = Converter.GetDecimal(dt.Rows[0]["PaidPrincAmt"]);
                p.PaidIntAmt = Converter.GetDecimal(dt.Rows[0]["PaidIntAmt"]);
                p.PaidPenalAmt = Converter.GetDecimal(dt.Rows[0]["PaidPenalAmt"]);
                p.CurrDuePrincAmt = Converter.GetDecimal(dt.Rows[0]["CurrDuePrincAmt"]);
                p.CurrDueIntAmt = Converter.GetDecimal(dt.Rows[0]["CurrDueIntAmt"]);


                p.CurrDuePrincAmt = (p.CurrDuePrincAmt - p.CalPaidDeposit);
                p.CurrDueIntAmt = (p.CurrDueIntAmt - p.CalPaidInterest);



                return p;
            }
            p.CuType = 0;

            return p;

        }


        public static int UpdateInformation01(A2ZLOANDEFAULTERDTO dto)
        {

            var prm = new object[7];

            prm[0] = dto.TrnDate;
            prm[1] = dto.CuType;
            prm[2] = dto.CuNo;
            prm[3] = dto.MemNo;
            prm[4] = dto.AccType;
            prm[5] = dto.AccNo;
            prm[6] = dto.CurrDueIntAmt;
         
            BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSLoanAccountDefaulterDataUpdate", prm, "A2ZCSMCUS");

            return 0;

           
        }


        
    }
}
