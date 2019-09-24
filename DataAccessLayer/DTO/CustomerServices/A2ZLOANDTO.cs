using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZLOANDTO
   {
       #region Propertise
       public int LoanApplicationNo { set; get; }
       public DateTime LoanApplicationDate { set; get; }
       public Int16 LoanAccountType { set; get; }
       public int LoanMemberNo { set; get; }
       public int CuType { set; get; }
       public int CuNo { set; get; }
       public Decimal LoanApplicationAmount { set; get; }
       public Decimal LoanInterestRate { set; get; }
       public Int16 LoanGracePeriod { set; get; }
       public Decimal LoanInstallmentAmount { set; get; }
       public Decimal LoanLastInstallmentAmount { set; get; }
       public int LoanNoInstallment { set; get; }
       public DateTime LoanFirstInstallmentdate { set; get; }
       public DateTime LoanExpDate { set; get; }
       public Int16 LoanInterestCalPeriod { set; get; }
       public Int16 LoanInterestCalMethod { set; get; }
       public Int16 LoanPurpose { set; get; }
       public Int16 LoanCategory { set; get; }
       public int LoanSuretyMemberNo { set; get; }
       public Int16 LoanStatus { set; get; }
       public DateTime LoanStatDate { set; get; }
       public Int16 LoanProcFlag { set; get; }
       public DateTime LoanStatusdate { set; get; }
       public String LoanStatusNote { set; get; }

       public Int16 AccTypeMode { set; get; }
       public int FromCashCode { set; get; }
       public Decimal LoanTotGuarantorAmt { set; get; }
       public Int16 InputBy { set; get; }
       public Int16 ApprovBy { set; get; }
       public DateTime InputByDate { set; get; }      
       public DateTime ApprovByDate { set; get; }
       public int AccPeriod { set; get; }
       #endregion


       public static int InsertInformation(A2ZLOANDTO dto)
       {
           int rowEffect = 0;
           string strQuery = @"INSERT into A2ZLOAN(LoanApplicationNo,LoanApplicationDate,AccType,CuType,CuNo,MemNo,LoanApplicationAmt,LoanIntRate,LoanInstlAmt,LoanLastInstlAmt,LoanNoInstl,LoanExpiryDate,LoanIntPeriod,LoanIntMethod,LoanPurpose,LoanProcFlag,LoanCategory,LoanSuretyMemNo,AccTypeMode,FromCashCode,LoanTotGuarantorAmt,InputBy,InputByDate,ApprovBy,ApprovByDate,AccPeriod)values('" + dto.LoanApplicationNo + "','" + dto.LoanApplicationDate + "','" + dto.LoanAccountType + "','" + dto.CuType + "','" + dto.CuNo + "','" + dto.LoanMemberNo + "','" + dto.LoanApplicationAmount + "','" + dto.LoanInterestRate + "','" + dto.LoanInstallmentAmount + "','" + dto.LoanLastInstallmentAmount + "','" + dto.LoanNoInstallment + "','" + dto.LoanExpDate + "','" + dto.LoanInterestCalPeriod + "','" + dto.LoanInterestCalMethod + "','" + dto.LoanPurpose + "','" + dto.LoanProcFlag + "','" + dto.LoanCategory + "','" + dto.LoanSuretyMemberNo + "','" + dto.AccTypeMode + "','" + dto.FromCashCode + "','" + dto.LoanTotGuarantorAmt + "','" + dto.InputBy + "','" + dto.InputByDate + "','" + dto.ApprovBy + "','" + dto.ApprovByDate + "','" + dto.AccPeriod + "')";
           rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));

           if (rowEffect == 0)
           {
               return 0;
           }
           else
           {
               return 1;
           }
       }

       public static A2ZLOANDTO GetInformation(Int16 AppNo)
       {

           var prm = new object[1];

           prm[0] = AppNo;


           DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoLoan", prm, "A2ZCSMCUS");
           
           
           //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT  lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo, LoanApplicationNo, LoanApplicationDate, AccType, MemNo, LoanApplicationAmt, LoanIntRate, LoanGrace, LoanInstlAmt, LoanLastInstlAmt, LoanNoInstl, LoanFirstInstlDt, LoanExpiryDate,LoanIntPeriod, LoanIntMethod, LoanPurpose, LoanCategory, LoanSuretyMemNo, LoanStatDate, LoanProcFlag,LoanExpiryDate,InputBy,ApprovBy,InputByDate,ApprovByDate FROM  dbo.A2ZLOAN WHERE LoanApplicationNo = " + AppNo, "A2ZCSMCUS");


           var p = new A2ZLOANDTO();
           if (dt.Rows.Count > 0)
           {

               p.LoanApplicationNo = Converter.GetInteger(dt.Rows[0]["LoanApplicationNo"]);
               p.LoanApplicationDate = Converter.GetDateTime(dt.Rows[0]["LoanApplicationDate"]);
               p.LoanAccountType = Converter.GetSmallInteger(dt.Rows[0]["AccType"]);
               p.CuNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
               p.LoanMemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
               p.LoanApplicationAmount = Converter.GetDecimal(dt.Rows[0]["LoanApplicationAmt"]);
               p.LoanInterestRate = Converter.GetDecimal(dt.Rows[0]["LoanIntRate"]);
               //p.LoanGracePeriod = Converter.GetSmallInteger(dt.Rows[0]["LoanGrace"]);
               p.LoanInstallmentAmount = Converter.GetDecimal(dt.Rows[0]["LoanInstlAmt"]);
               p.LoanLastInstallmentAmount = Converter.GetDecimal(dt.Rows[0]["LoanLastInstlAmt"]);
               p.LoanNoInstallment = Converter.GetInteger(dt.Rows[0]["LoanNoInstl"]);
               //p.LoanFirstInstallmentdate = Converter.GetDateTime(dt.Rows[0]["LoanFirstInstlDt"]);
               p.LoanInterestCalPeriod = Converter.GetSmallInteger(dt.Rows[0]["LoanIntPeriod"]);
               p.LoanInterestCalMethod = Converter.GetSmallInteger(dt.Rows[0]["LoanIntMethod"]);
               p.LoanPurpose = Converter.GetSmallInteger(dt.Rows[0]["LoanPurpose"]);
               p.LoanCategory = Converter.GetSmallInteger(dt.Rows[0]["LoanCategory"]);
               p.LoanSuretyMemberNo = Converter.GetInteger(dt.Rows[0]["LoanSuretyMemNo"]);

               p.LoanStatus = Converter.GetSmallInteger(dt.Rows[0]["LoanStatus"]);

               p.LoanStatDate = Converter.GetDateTime(dt.Rows[0]["LoanStatDate"]);

               
               p.LoanProcFlag = Converter.GetSmallInteger(dt.Rows[0]["LoanProcFlag"]);
             
               p.LoanExpDate = Converter.GetDateTime(dt.Rows[0]["LoanExpiryDate"]);

               p.FromCashCode = Converter.GetInteger(dt.Rows[0]["FromCashCode"]);

               p.LoanTotGuarantorAmt = Converter.GetDecimal(dt.Rows[0]["LoanTotGuarantorAmt"]);

               //p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);

              // p.LoanStatusNote = Converter.GetString(dt.Rows[0]["LoanStatNote"]);

               p.InputBy = Converter.GetSmallInteger(dt.Rows[0]["InputBy"]);
               p.ApprovBy = Converter.GetSmallInteger(dt.Rows[0]["ApprovBy"]);
               p.InputByDate = Converter.GetDateTime(dt.Rows[0]["InputByDate"]);
               p.ApprovByDate = Converter.GetDateTime(dt.Rows[0]["ApprovByDate"]);

               p.AccPeriod = Converter.GetInteger(dt.Rows[0]["AccPeriod"]);
               return p;
           }
           p.LoanApplicationNo = 0;

           return p;

       }

       public static int UpdateInformation(A2ZLOANDTO dto)
       {
          
           int rowEffect = 0;
           string strQuery = "UPDATE A2ZLOAN set LoanApplicationNo='" + dto.LoanApplicationNo + "',LoanApplicationDate='" + dto.LoanApplicationDate + "',MemNo='" + dto.LoanMemberNo + "',CuType='" + dto.CuType + "',CuNo='" + dto.CuNo + "', AccType='" + dto.LoanAccountType + "', AccTypeMode='" + dto.AccTypeMode + "',LoanApplicationAmt='" + dto.LoanApplicationAmount + "',LoanIntRate='" + dto.LoanInterestRate + "',LoanInstlAmt='" + dto.LoanInstallmentAmount + "',LoanLastInstlAmt='" + dto.LoanLastInstallmentAmount + "',LoanNoInstl='" + dto.LoanNoInstallment + "',LoanExpiryDate='" + dto.LoanExpDate + "',LoanIntPeriod='" + dto.LoanInterestCalPeriod + "',LoanIntMethod='" + dto.LoanInterestCalMethod + "',LoanPurpose='" + dto.LoanPurpose + "',LoanCategory='" + dto.LoanCategory + "',LoanSuretyMemNo='" + dto.LoanSuretyMemberNo + "',LoanTotGuarantorAmt='" + dto.LoanTotGuarantorAmt + "',InputBy='" + dto.InputBy + "',InputByDate='" + dto.InputByDate + "',ApprovBy='" + dto.ApprovBy + "',ApprovByDate='" + dto.ApprovByDate + "',AccPeriod='" + dto.AccPeriod + "' where LoanApplicationNo ='" + dto.LoanApplicationNo + "'";
           rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSMCUS"));
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
