using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZSTAFFLOANDTO
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
        public Int16 LoanProcFlag { set; get; }
        public DateTime LoanStatusdate { set; get; }
        public String LoanStatusNote { set; get; }

        public Int16 AccTypeMode { set; get; }
        #endregion


        public static int InsertInformation(A2ZSTAFFLOANDTO dto)
        {
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZSTAFFLOAN(LoanApplicationNo,LoanApplicationDate,AccType,CuType,CuNo,MemNo,LoanApplicationAmt,LoanIntRate,LoanGrace,LoanInstlAmt,LoanLastInstlAmt,LoanNoInstl,LoanExpiryDate,LoanIntPeriod,LoanIntMethod,LoanPurpose,LoanProcFlag,LoanCategory,LoanSuretyMemNo,AccTypeMode)values('" + dto.LoanApplicationNo + "','" + dto.LoanApplicationDate + "','" + dto.LoanAccountType + "','" + dto.CuType + "','" + dto.CuNo + "','" + dto.LoanMemberNo + "','" + dto.LoanApplicationAmount + "','" + dto.LoanInterestRate + "','" + dto.LoanGracePeriod + "','" + dto.LoanInstallmentAmount + "','" + dto.LoanLastInstallmentAmount + "','" + dto.LoanNoInstallment + "','" + dto.LoanExpDate + "','" + dto.LoanInterestCalPeriod + "','" + dto.LoanInterestCalMethod + "','" + dto.LoanPurpose + "','" + dto.LoanProcFlag + "','" + dto.LoanCategory + "','" + dto.LoanSuretyMemberNo + "','" + dto.AccTypeMode + "')";
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

        public static A2ZSTAFFLOANDTO GetInformation(Int16 AppNo)
        {
            var prm = new object[1];

            prm[0] = AppNo;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoStaffLoan", prm, "A2ZCSMCUS");
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT  lTrim(str(CuType) +lTrim(str(CuNo))) As CuNo, LoanApplicationNo, LoanApplicationDate, AccType, MemNo, LoanApplicationAmt, LoanIntRate, LoanGrace, LoanInstlAmt, LoanLastInstlAmt, LoanNoInstl, LoanFirstInstlDt, LoanExpiryDate,LoanIntPeriod, LoanIntMethod, LoanPurpose, LoanCategory, LoanSuretyMemNo, LoanStatDate, LoanProcFlag FROM  dbo.A2ZSTAFFLOAN WHERE LoanApplicationNo = " + AppNo, "A2ZCSMCUS");


            var p = new A2ZSTAFFLOANDTO();
            if (dt.Rows.Count > 0)
            {

                p.LoanApplicationNo = Converter.GetInteger(dt.Rows[0]["LoanApplicationNo"]);
                p.LoanApplicationDate = Converter.GetDateTime(dt.Rows[0]["LoanApplicationDate"]);
                p.LoanAccountType = Converter.GetSmallInteger(dt.Rows[0]["AccType"]);
                p.CuNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.LoanMemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.LoanApplicationAmount = Converter.GetDecimal(dt.Rows[0]["LoanApplicationAmt"]);
                p.LoanInterestRate = Converter.GetDecimal(dt.Rows[0]["LoanIntRate"]);
                p.LoanGracePeriod = Converter.GetSmallInteger(dt.Rows[0]["LoanGrace"]);
                p.LoanInstallmentAmount = Converter.GetDecimal(dt.Rows[0]["LoanInstlAmt"]);
                p.LoanLastInstallmentAmount = Converter.GetDecimal(dt.Rows[0]["LoanLastInstlAmt"]);
                p.LoanNoInstallment = Converter.GetInteger(dt.Rows[0]["LoanNoInstl"]);
                //p.LoanFirstInstallmentdate = Converter.GetDateTime(dt.Rows[0]["LoanFirstInstlDt"]);
                p.LoanInterestCalPeriod = Converter.GetSmallInteger(dt.Rows[0]["LoanIntPeriod"]);
                p.LoanInterestCalMethod = Converter.GetSmallInteger(dt.Rows[0]["LoanIntMethod"]);
                p.LoanPurpose = Converter.GetSmallInteger(dt.Rows[0]["LoanPurpose"]);
                p.LoanCategory = Converter.GetSmallInteger(dt.Rows[0]["LoanCategory"]);
                p.LoanSuretyMemberNo = Converter.GetInteger(dt.Rows[0]["LoanSuretyMemNo"]);

                p.LoanProcFlag = Converter.GetSmallInteger(dt.Rows[0]["LoanProcFlag"]);
                // p.LoanStatus = Converter.GetSmallInteger(dt.Rows[0]["LoanStatus"]);
                p.LoanExpDate = Converter.GetDateTime(dt.Rows[0]["LoanExpiryDate"]);
                //p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);

                // p.LoanStatusNote = Converter.GetString(dt.Rows[0]["LoanStatNote"]);
                return p;
            }
            p.LoanApplicationNo = 0;

            return p;

        }

        public static int UpdateInformation(A2ZSTAFFLOANDTO dto)
        {

            int rowEffect = 0;
            string strQuery = "UPDATE A2ZSTAFFLOAN set LoanApplicationNo='" + dto.LoanApplicationNo + "',LoanApplicationDate='" + dto.LoanApplicationDate + "',MemNo='" + dto.LoanMemberNo + "',CuType='" + dto.CuType + "',CuNo='" + dto.CuNo + "', AccType='" + dto.LoanAccountType + "', AccTypeMode='" + dto.AccTypeMode + "',LoanApplicationAmt='" + dto.LoanApplicationAmount + "',LoanIntRate='" + dto.LoanInterestRate + "',LoanGrace='" + dto.LoanGracePeriod + "',LoanInstlAmt='" + dto.LoanInstallmentAmount + "',LoanLastInstlAmt='" + dto.LoanLastInstallmentAmount + "',LoanNoInstl='" + dto.LoanNoInstallment + "',LoanExpiryDate='" + dto.LoanExpDate + "',LoanIntPeriod='" + dto.LoanInterestCalPeriod + "',LoanIntMethod='" + dto.LoanInterestCalMethod + "',LoanPurpose='" + dto.LoanPurpose + "',LoanCategory='" + dto.LoanCategory + "',LoanSuretyMemNo='" + dto.LoanSuretyMemberNo + "' where LoanApplicationNo ='" + dto.LoanApplicationNo + "'";
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
