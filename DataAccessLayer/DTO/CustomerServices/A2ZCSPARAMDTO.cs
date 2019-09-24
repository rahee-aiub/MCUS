using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
  public  class A2ZCSPARAMDTO
  {
      #region Propertise

        public Int16 AccType { set; get; }
        public Int16 CalculationPeriod { set; get; }
        public Int16 CalculationMethod { set; get; }
        public Int16 LoanCalculationMethod { set; get; }
        public decimal InterestRate { set; get; }
        public decimal FundRate { set; get; }
        public Int16 ProductCondition { set; get; }
        public Int16 ProductInterestType { set; get; }
        public decimal MinDepositAmt { set; get; }
        public Int16 IntWithdrDays { set; get; }
        public Int16 RoundFlag { set; get; }
        public decimal AccProcFees { set; get; }
        public decimal AccClosingFees { set; get; }
        public int Period { set; get; }
        public Int16 PeriodSlab { set; get; }
      #endregion

        public static int InsertInformation(A2ZCSPARAMDTO dto)
        {
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZCSPARAM(AccType,PrmCalPeriod,PrmCalMethod, PrmLoanCalMethod,PrmIntRate,PrmFundRate,PrmProdCon,PrmProdIntType,PrmMinDeposit,PrmIntWithdrDays,PrmRoundFlag,PrmAccProcFees,PrmAccClosingFees,PrmPeriodSlab,PrmPeriod) values('" + dto.AccType + "','" + dto.CalculationPeriod + "','" + dto.CalculationMethod + "','" + dto.LoanCalculationMethod + "','" + dto.InterestRate + "','" + dto.FundRate + "','" + dto.ProductCondition + "','" + dto.ProductInterestType + "','" + dto.IntWithdrDays + "','" + dto.MinDepositAmt + "','" + dto.RoundFlag + "','" + dto.AccProcFees + "','" + dto.AccClosingFees + "','" + dto.PeriodSlab + "','" + dto.Period + "')";
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
        public static A2ZCSPARAMDTO GetInformation(Int16 AccType)
        {
            var prm = new object[1];

            prm[0] = AccType;
            
            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoParam", prm, "A2ZCSMCUS");
            
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + AccType + "'", "A2ZCSMCUS");


            var p = new A2ZCSPARAMDTO();
            if (dt.Rows.Count > 0)
            {
                p.AccType = Converter.GetSmallInteger(dt.Rows[0]["AccType"]);
                p.CalculationPeriod = Converter.GetSmallInteger(dt.Rows[0]["PrmCalPeriod"]);
                p.CalculationMethod = Converter.GetSmallInteger(dt.Rows[0]["PrmCalMethod"]);
                p.LoanCalculationMethod = Converter.GetSmallInteger(dt.Rows[0]["PrmLoanCalMethod"]);
                p.InterestRate= Converter.GetDecimal(dt.Rows[0]["PrmIntRate"]);
                p.FundRate = Converter.GetDecimal(dt.Rows[0]["PrmFundRate"]);
                p.ProductCondition = Converter.GetSmallInteger(dt.Rows[0]["PrmProdCon"]);
                p.ProductInterestType = Converter.GetSmallInteger(dt.Rows[0]["PrmProdIntType"]);
                p.MinDepositAmt = Converter.GetDecimal(dt.Rows[0]["PrmMinDeposit"]);
                p.RoundFlag = Converter.GetSmallInteger(dt.Rows[0]["PrmRoundFlag"]);
                p.IntWithdrDays = Converter.GetSmallInteger(dt.Rows[0]["PrmIntWithdrDays"]);
                p.AccProcFees = Converter.GetDecimal(dt.Rows[0]["PrmAccProcFees"]);
                p.AccClosingFees = Converter.GetDecimal(dt.Rows[0]["PrmAccClosingFees"]);
                p.Period = Converter.GetInteger(dt.Rows[0]["PrmPeriod"]);
               
                return p;

            }
            else
            {
                p.AccType = 0;
            
            }


            return p;



        }

        public static int UpdateInformation(A2ZCSPARAMDTO dto)
        {

            int rowEffect = 0;
            string strQuery = "UPDATE A2ZCSPARAM set PrmCalPeriod='" + dto.CalculationPeriod + "',PrmCalMethod='" + dto.CalculationMethod + "',PrmLoanCalMethod='" + dto.LoanCalculationMethod + "',PrmIntRate='" + dto.InterestRate + "',PrmFundRate='" + dto.FundRate + "',PrmProdCon='" + dto.ProductCondition + "',PrmProdIntType='" + dto.ProductInterestType + "',PrmIntWithdrDays='" + dto.IntWithdrDays + "',PrmMinDeposit='" + dto.MinDepositAmt + "',PrmRoundFlag='" + dto.RoundFlag + "',PrmAccProcFees='" + dto.AccProcFees + "',PrmAccClosingFees='" + dto.AccClosingFees + "',PrmPeriod='" + dto.Period + "' where AccType='" + dto.AccType + "'";
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
