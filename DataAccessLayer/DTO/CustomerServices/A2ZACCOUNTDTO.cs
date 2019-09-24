using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using DataAccessLayer.Conn;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZACCOUNTDTO
    {
        #region Propertise
        public Int16 Id { set; get; }
        public Int16 BranchNo { set; get; }
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public Int16 CuType { set; get; }
        public int CuNo { set; get; }
        public String CreditUnionName { set; get; }
        public int MemberNo { set; get; }
        public String MemberName { set; get; }
        public DateTime Opendate { set; get; }
        public decimal DepositAmount { set; get; }
        public decimal TotDepositAmount { set; get; }
        public decimal FixedDepositAmount { set; get; }
        public decimal FixedMthInt { set; get; }
        public Int16 Period { set; get; }
        public Int16 WithDrawalAC { set; get; }
        public Int16 InterestCalculation { set; get; }
        public DateTime MatruityDate { set; get; }
        public decimal MatruityAmount { set; get; }
        public Int16 InterestWithdrawal { set; get; }
        public Int16 AutoRenewal { set; get; }
        public decimal LoanAmount { set; get; }
        public Int16 NoInstallment { set; get; }
        public decimal MonthlyInstallment { set; get; }
        public decimal LastInstallment { set; get; }
        public decimal InterestRate { set; get; }
        public Int16 ContractInt { set; get; }
        public Int16 GracePeriod { set; get; }
        public int LoaneeACType { set; get; }
        public int LoaneeMemberNo { set; get; }

        public String SpInstruction { set; get; }
        public Int16 CorrAccType { set; get; }
        public int CorrAccNo { set; get; }
        public Int16 AutoTransferSavings { set; get; }
        public String OldAccountNo { set; get; }
        public Int16 AccAtyClass { set; get; }
        public int AccStatus { set; get; }
        public String AccStatusNote { set; get; }
        public DateTime AccStatusDate { set; get; }
        public Int16 a { set; get; }
        public int MemType { set; get; }
        public int MemAccTypeClass { set; get; }
        public decimal AccBalance { set; get; }

        public decimal AccOpBal { set; get; }
        public decimal AccLienAmt { set; get; }
        public decimal AccProvBalance { set; get; }
        public decimal AccPrincipal { set; get; }
        public decimal AccRenwlAmt { set; get; }
        public DateTime AccRenwlDate { set; get; }
        public decimal AccOrgAmt { set; get; }
        public DateTime LastTrnDate { set; get; }
        public DateTime LastODIntDate { set; get; }
        public decimal AccDisbAmt { set; get; }
        public decimal AccDueIntAmt { set; get; }
        public String AccCertNo { set; get; }
        public DateTime AccBenefitDate { set; get; }
        public Int16 AccNoAnni { set; get; }
        public DateTime AccAnniDate { set; get; }
        public Int16 InputBy { set; get; }
        public DateTime InputByDate { set; get; }
        public String OpenNulldate { set; get; }
        public String MatruityNullDate { set; get; }
        public String AccStatusNullDate { set; get; }
        public String AccRenwlNullDate { set; get; }
        public String LastTrnNullDate { set; get; }
        public String AccBenefitNullDate { set; get; }
        public String AccAnniNullDate { set; get; }
        public String AccODIntNullDate { set; get; }
        public Int16 lblDepFlage { set; get; }


        #endregion


        public static int InsertInformation(A2ZACCOUNTDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@Opendate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@MatruityDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@AccBenefitDate", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@InputByDate", DBNull.Value);
           

            if (dto.OpenNulldate == "")
            {

                param1 = new SqlParameter("@Opendate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@Opendate", dto.Opendate);
            }

            if (dto.MatruityNullDate == "")
            {

                param2 = new SqlParameter("@MatruityDate", DBNull.Value);
            }
            else
            {
                param2 = new SqlParameter("@MatruityDate", dto.MatruityDate);
            }

            if (dto.AccBenefitNullDate == "")
            {

                param3 = new SqlParameter("@AccBenefitDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@AccBenefitDate", dto.AccBenefitDate);
            }

            param4 = new SqlParameter("@InputByDate", dto.InputByDate);


            


            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"), "Sp_CSAccountDataInsert", new object[] { dto.AccType, dto.AccNo, dto.CuType, dto.CuNo, dto.MemberNo, param1, dto.DepositAmount, dto.Period, dto.WithDrawalAC, dto.InterestCalculation, param2, dto.MatruityAmount, dto.InterestWithdrawal, dto.AutoRenewal, dto.LoanAmount, dto.NoInstallment, dto.MonthlyInstallment, dto.LastInstallment, dto.InterestRate, dto.ContractInt, dto.GracePeriod, dto.LoaneeACType, dto.LoaneeMemberNo, dto.SpInstruction, dto.CorrAccType, dto.CorrAccNo, dto.AutoTransferSavings, dto.OldAccountNo, dto.FixedDepositAmount, dto.FixedMthInt, dto.AccStatus, dto.AccAtyClass, param3, dto.InputBy, param4 });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }


        public static int Insert(A2ZACCOUNTDTO dto)
        {
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZACCOUNT(AccType, AccNo,CuType,CuNo,MemNo,AccStatus,AccAtyClass,AccOpenDate) values('" + dto.AccType + "','" +
                       dto.AccNo + "','" + dto.CuType + "','" + dto.CuNo + "','" + dto.MemberNo + "','" + dto.AccStatus + "','" + dto.AccAtyClass + "','" + dto.Opendate + "')";
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


        public static A2ZACCOUNTDTO GetInformation(int AccType, Int64 AccountNo, Int16 CuType, int CreditNo, int MemberNo)
        {

            var prm = new object[5];

            prm[0] = AccType;
            prm[1] = AccountNo;
            prm[2] = CuType;
            prm[3] = CreditNo;
            prm[4] = MemberNo;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccount", prm, "A2ZCSMCUS");


            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
            //    AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {
                //  p.BranchNo = Converter.GetSmallInteger(dt.Rows[0]["BranchNo"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
                p.CuNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.DepositAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.FixedDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccFixedAmt"]);
                p.FixedMthInt = Converter.GetDecimal(dt.Rows[0]["AccFixedMthInt"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AccPeriod"]);
                p.WithDrawalAC = Converter.GetSmallInteger(dt.Rows[0]["AccDebitFlag"]);
                p.InterestCalculation = Converter.GetSmallInteger(dt.Rows[0]["AccIntFlag"]);
                p.MatruityDate = Converter.GetDateTime(dt.Rows[0]["AccMatureDate"]);
                p.MatruityAmount = Converter.GetDecimal(dt.Rows[0]["AccMatureAmt"]);
                p.InterestWithdrawal = Converter.GetSmallInteger(dt.Rows[0]["AccIntType"]);
                p.AutoRenewal = Converter.GetSmallInteger(dt.Rows[0]["AccAutoRenewFlag"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                p.NoInstallment = Converter.GetSmallInteger(dt.Rows[0]["AccNoInstl"]);
                p.MonthlyInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.LastInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanLastInstlAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);
                p.ContractInt = Converter.GetSmallInteger(dt.Rows[0]["AccContractIntFlag"]);
                p.GracePeriod = Converter.GetSmallInteger(dt.Rows[0]["AccLoanGrace"]);
                p.LoaneeACType = Converter.GetInteger(dt.Rows[0]["AccLoaneeAccType"]);
                p.LoaneeMemberNo = Converter.GetInteger(dt.Rows[0]["AccLoaneeMemNo"]);

                p.SpInstruction = Converter.GetString(dt.Rows[0]["AccSpecialNote"]);
                p.CorrAccType = Converter.GetSmallInteger(dt.Rows[0]["AccCorrAccType"]);
                p.CorrAccNo = Converter.GetInteger(dt.Rows[0]["AccCorrAccNo"]);
                p.AutoTransferSavings = Converter.GetSmallInteger(dt.Rows[0]["AccAutoTrfFlag"]);
                p.OldAccountNo = Converter.GetString(dt.Rows[0]["AccOldNumber"]);
                p.AccBalance = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["AccOrgAmt"]);
                p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["AccLienAmt"]);
                p.AccDisbAmt = Converter.GetDecimal(dt.Rows[0]["AccDisbAmt"]);
                p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccLastTrnDateU"]);
                p.TotDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccTotalDep"]);
                p.AccAtyClass = Converter.GetSmallInteger(dt.Rows[0]["AccAtyClass"]);
                p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatus"]);
                p.AccStatusDate = Converter.GetDateTime(dt.Rows[0]["AccStatusDate"]);
                p.AccStatusNote = Converter.GetString(dt.Rows[0]["AccStatusNote"]);
                p.AccDueIntAmt = Converter.GetDecimal(dt.Rows[0]["AccDueIntAmt"]);
                p.AccRenwlDate = Converter.GetDateTime(dt.Rows[0]["AccRenwlDate"]);
                p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["AccRenwlAmt"]);
                p.AccCertNo = Converter.GetString(dt.Rows[0]["AccCertNo"]);

                p.AccBenefitDate = Converter.GetDateTime(dt.Rows[0]["AccBenefitDate"]);


                p.a = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.AccType = 0;
                p.AccNo = 0;
                p.CuNo = 0;
                p.MemberNo = 0;
                p.a = 0;
            }


            return p;

        }

        public static A2ZACCOUNTDTO GetInfo(int AccType, Int16 CuType, int CreditNo, int MemberNo)
        {

            var prm = new object[4];

            prm[0] = AccType;
            prm[1] = CuType;
            prm[2] = CreditNo;
            prm[3] = MemberNo;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAcc", prm, "A2ZCSMCUS");


            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
            //    AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {
                //  p.BranchNo = Converter.GetSmallInteger(dt.Rows[0]["BranchNo"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
                p.CuNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.DepositAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.FixedDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccFixedAmt"]);
                p.FixedMthInt = Converter.GetDecimal(dt.Rows[0]["AccFixedMthInt"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AccPeriod"]);
                p.WithDrawalAC = Converter.GetSmallInteger(dt.Rows[0]["AccDebitFlag"]);
                p.InterestCalculation = Converter.GetSmallInteger(dt.Rows[0]["AccIntFlag"]);
                p.MatruityDate = Converter.GetDateTime(dt.Rows[0]["AccMatureDate"]);
                p.MatruityAmount = Converter.GetDecimal(dt.Rows[0]["AccMatureAmt"]);
                p.InterestWithdrawal = Converter.GetSmallInteger(dt.Rows[0]["AccIntType"]);
                p.AutoRenewal = Converter.GetSmallInteger(dt.Rows[0]["AccAutoRenewFlag"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                p.NoInstallment = Converter.GetSmallInteger(dt.Rows[0]["AccNoInstl"]);
                p.MonthlyInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.LastInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanLastInstlAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);
                p.ContractInt = Converter.GetSmallInteger(dt.Rows[0]["AccContractIntFlag"]);
                p.GracePeriod = Converter.GetSmallInteger(dt.Rows[0]["AccLoanGrace"]);
                p.LoaneeACType = Converter.GetInteger(dt.Rows[0]["AccLoaneeAccType"]);
                p.LoaneeMemberNo = Converter.GetInteger(dt.Rows[0]["AccLoaneeMemNo"]);
                p.SpInstruction = Converter.GetString(dt.Rows[0]["AccSpecialNote"]);
                p.CorrAccType = Converter.GetSmallInteger(dt.Rows[0]["AccCorrAccType"]);
                p.CorrAccNo = Converter.GetInteger(dt.Rows[0]["AccCorrAccNo"]);
                p.AutoTransferSavings = Converter.GetSmallInteger(dt.Rows[0]["AccAutoTrfFlag"]);
                p.OldAccountNo = Converter.GetString(dt.Rows[0]["AccOldNumber"]);
                p.AccBalance = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["AccOrgAmt"]);
                p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["AccLienAmt"]);
                p.AccDisbAmt = Converter.GetDecimal(dt.Rows[0]["AccDisbAmt"]);
                p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccLastTrnDateU"]);
                p.TotDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccTotalDep"]);
                p.AccAtyClass = Converter.GetSmallInteger(dt.Rows[0]["AccAtyClass"]);
                p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatus"]);
                p.AccStatusDate = Converter.GetDateTime(dt.Rows[0]["AccStatusDate"]);
                p.AccStatusNote = Converter.GetString(dt.Rows[0]["AccStatusNote"]);
                p.AccDueIntAmt = Converter.GetDecimal(dt.Rows[0]["AccDueIntAmt"]);
                p.AccRenwlDate = Converter.GetDateTime(dt.Rows[0]["AccRenwlDate"]);
                p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["AccRenwlAmt"]);
                p.AccCertNo = Converter.GetString(dt.Rows[0]["AccCertNo"]);

                p.AccBenefitDate = Converter.GetDateTime(dt.Rows[0]["AccBenefitDate"]);


                p.a = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.AccType = 0;
                p.AccNo = 0;
                p.CuNo = 0;
                p.MemberNo = 0;
                p.a = 0;
            }


            return p;

        }

        public static A2ZACCOUNTDTO GetInfoAccNo(Int64 AccountNo)
        {

            A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
            string date1 = dt2.ToString("dd/MM/yyyy");

            var prm1 = new object[3];
            prm1[0] = AccountNo;
            prm1[1] = Converter.GetDateToYYYYMMDD(date1);
            prm1[2] = 0;

            DataTable dt3 = BLL.CommonManager.Instance.GetDataTableBySpWithParams("SpM_CSGenerateSingleAccountBalance", prm1, "A2ZCSMCUS");


            var prm = new object[1];
            prm[0] = AccountNo;

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccountNo", prm, "A2ZCSMCUS");


            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE AccType = '" + AccType + "' and AccNo = '" +
            //    AccountNo + "' and CuType='" + CuType + "' and CuNo='" + CreditNo + "' and MemNo='" + MemberNo + "'", "A2ZCSMCUS");


            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {
                //  p.BranchNo = Converter.GetSmallInteger(dt.Rows[0]["BranchNo"]);
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);
                p.CuType = Converter.GetSmallInteger(dt.Rows[0]["CuType"]);
                p.CuNo = Converter.GetInteger(dt.Rows[0]["CuNo"]);
                p.MemberNo = Converter.GetInteger(dt.Rows[0]["MemNo"]);
                p.Opendate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);
                p.DepositAmount = Converter.GetDecimal(dt.Rows[0]["AccMonthlyDeposit"]);
                p.FixedDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccFixedAmt"]);
                p.FixedMthInt = Converter.GetDecimal(dt.Rows[0]["AccFixedMthInt"]);
                p.Period = Converter.GetSmallInteger(dt.Rows[0]["AccPeriod"]);
                p.WithDrawalAC = Converter.GetSmallInteger(dt.Rows[0]["AccDebitFlag"]);
                p.InterestCalculation = Converter.GetSmallInteger(dt.Rows[0]["AccIntFlag"]);
                p.MatruityDate = Converter.GetDateTime(dt.Rows[0]["AccMatureDate"]);
                p.MatruityAmount = Converter.GetDecimal(dt.Rows[0]["AccMatureAmt"]);
                p.InterestWithdrawal = Converter.GetSmallInteger(dt.Rows[0]["AccIntType"]);
                p.AutoRenewal = Converter.GetSmallInteger(dt.Rows[0]["AccAutoRenewFlag"]);
                p.LoanAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                p.NoInstallment = Converter.GetSmallInteger(dt.Rows[0]["AccNoInstl"]);
                p.MonthlyInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.LastInstallment = Converter.GetDecimal(dt.Rows[0]["AccLoanLastInstlAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);
                p.ContractInt = Converter.GetSmallInteger(dt.Rows[0]["AccContractIntFlag"]);
                p.GracePeriod = Converter.GetSmallInteger(dt.Rows[0]["AccLoanGrace"]);
                p.LoaneeACType = Converter.GetInteger(dt.Rows[0]["AccLoaneeAccType"]);
                p.LoaneeMemberNo = Converter.GetInteger(dt.Rows[0]["AccLoaneeMemNo"]);
                p.SpInstruction = Converter.GetString(dt.Rows[0]["AccSpecialNote"]);
                p.CorrAccType = Converter.GetSmallInteger(dt.Rows[0]["AccCorrAccType"]);
                p.CorrAccNo = Converter.GetInteger(dt.Rows[0]["AccCorrAccNo"]);
                p.AutoTransferSavings = Converter.GetSmallInteger(dt.Rows[0]["AccAutoTrfFlag"]);
                p.OldAccountNo = Converter.GetString(dt.Rows[0]["AccOldNumber"]);
                p.AccBalance = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["AccOrgAmt"]);
                p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["AccLienAmt"]);
                p.AccDisbAmt = Converter.GetDecimal(dt.Rows[0]["AccDisbAmt"]);
                p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccLastTrnDateU"]);
                p.TotDepositAmount = Converter.GetDecimal(dt.Rows[0]["AccTotalDep"]);
                p.AccAtyClass = Converter.GetSmallInteger(dt.Rows[0]["AccAtyClass"]);
                p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatus"]);
                p.AccStatusDate = Converter.GetDateTime(dt.Rows[0]["AccStatusDate"]);
                p.AccStatusNote = Converter.GetString(dt.Rows[0]["AccStatusNote"]);
                p.AccDueIntAmt = Converter.GetDecimal(dt.Rows[0]["AccDueIntAmt"]);
                p.AccRenwlDate = Converter.GetDateTime(dt.Rows[0]["AccRenwlDate"]);
                p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["AccRenwlAmt"]);
                p.AccCertNo = Converter.GetString(dt.Rows[0]["AccCertNo"]);

                p.AccBenefitDate = Converter.GetDateTime(dt.Rows[0]["AccBenefitDate"]);
                p.AccOpBal = Converter.GetDecimal(dt.Rows[0]["AccOpBal"]);

                p.AccNoAnni = Converter.GetSmallInteger(dt.Rows[0]["AccNoAnni"]);
                p.AccAnniDate = Converter.GetDateTime(dt.Rows[0]["AccAnniDate"]);

                p.LastODIntDate = Converter.GetDateTime(dt.Rows[0]["AccODIntDate"]);

                

                //p.LastTrnDate = Converter.GetDateTime(dt.Rows[0]["AccPrevTrnDateU"]);
                //p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["CalAccPrincipal"]);
                //p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["CalAccOrgAmt"]);
                //p.AccDisbAmt = Converter.GetDecimal(dt.Rows[0]["CalAccDisbAmt"]);
                //p.TotDepositAmount = Converter.GetDecimal(dt.Rows[0]["CalAccTotalDep"]);
                //p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["CalAccRenwlAmt"]);
                //p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["CalAccProvBalance"]);
                //p.AccDueIntAmt = Converter.GetDecimal(dt.Rows[0]["AccPrevDueIntAmt"]);
                //p.AccStatus = Converter.GetSmallInteger(dt.Rows[0]["AccStatusP"]);
                //p.AccStatusDate = Converter.GetDateTime(dt.Rows[0]["AccStatusDateP"]);
                //p.AccStatusNote = Converter.GetString(dt.Rows[0]["AccStatusDateP"]);
                //p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["CalAccLienAmt"]);


                p.AccBalance = p.AccOpBal;


                p.a = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.AccType = 0;
                p.AccNo = 0;
                p.CuNo = 0;
                p.MemberNo = 0;
                p.a = 0;
            }


            return p;

        }



        public static A2ZACCOUNTDTO GetInforbyAccType(Int16 CType, int CNo, int memNo, int AType)
        {

            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE CuType = 0 AND CuNo = 0 AND MemNo = '" + memNo + "' AND AccType = '" + AType + "'", "A2ZCSMCUS");


            var p = new A2ZACCOUNTDTO();
            if (dt.Rows.Count > 0)
            {

                p.AccNo = Converter.GetLong(dt.Rows[0]["AccNo"]);

                return p;
            }
            p.AccNo = 0;

            return p;

        }
        public static int UpdateInformation(A2ZACCOUNTDTO dto)
        {

            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@Opendate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@MatruityDate", DBNull.Value);
            SqlParameter param3 = new SqlParameter("@AccBenefitDate", DBNull.Value);
            SqlParameter param4 = new SqlParameter("@AccRenwlDate", DBNull.Value);
            SqlParameter param5 = new SqlParameter("@AccAnniDate", DBNull.Value);
            SqlParameter param6 = new SqlParameter("@LastODIntDate", DBNull.Value);

            if (dto.OpenNulldate == "")
            {

                param1 = new SqlParameter("@Opendate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@Opendate", dto.Opendate);
            }

            if (dto.MatruityNullDate == "")
            {

                param2 = new SqlParameter("@MatruityDate", DBNull.Value);
            }
            else
            {
                param2 = new SqlParameter("@MatruityDate", dto.MatruityDate);
            }

            if (dto.AccBenefitNullDate == "")
            {

                param3 = new SqlParameter("@AccBenefitDate", DBNull.Value);
            }
            else
            {
                param3 = new SqlParameter("@AccBenefitDate", dto.AccBenefitDate);
            }

            if (dto.AccRenwlNullDate == "")
            {

                param4 = new SqlParameter("@AccRenwlDate", DBNull.Value);
            }
            else
            {
                param4 = new SqlParameter("@AccRenwlDate", dto.AccRenwlDate);
            }

            if (dto.AccAnniNullDate == "")
            {

                param5 = new SqlParameter("@AccAnniDate", DBNull.Value);
            }
            else
            {
                param5 = new SqlParameter("@AccAnniDate", dto.AccAnniDate);
            }

            if (dto.AccODIntNullDate == "")
            {

                param6 = new SqlParameter("@LastODIntDate", DBNull.Value);
            }
            else
            {
                param6 = new SqlParameter("@LastODIntDate", dto.LastODIntDate);
            }

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZCSMCUS"), "Sp_CSAccountDataUpdate", new object[] { dto.AccType, dto.AccNo, dto.CuType, dto.CuNo, dto.MemberNo, param1, dto.DepositAmount, dto.Period, dto.WithDrawalAC, dto.InterestCalculation, param2, dto.MatruityAmount, dto.InterestWithdrawal, dto.AutoRenewal, dto.LoanAmount, dto.NoInstallment, dto.MonthlyInstallment, dto.LastInstallment, dto.InterestRate, dto.ContractInt, dto.GracePeriod, dto.LoaneeACType, dto.LoaneeMemberNo, dto.AccCertNo, dto.SpInstruction, dto.CorrAccType, dto.CorrAccNo, dto.AutoTransferSavings, dto.OldAccountNo, dto.FixedDepositAmount, dto.FixedMthInt, dto.AccAtyClass, param3, dto.AccOrgAmt, dto.AccPrincipal, dto.AccRenwlAmt, param4, param5, dto.TotDepositAmount, dto.lblDepFlage, dto.AccDueIntAmt,param6 });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }

        public static int Update1(A2ZACCOUNTDTO dto)
        {

            int rowEffect = 0;
            string strQuery = "UPDATE A2ZACCOUNT SET AccAtyClass ='" + dto.AccAtyClass + "', CuType ='" + dto.CuType + "',CuNo ='" + dto.CuNo + "' where  Id='" + dto.Id + "' ";
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

        public static int UpdateAccStatus(A2ZACCOUNTDTO dto)
        {

            int rowEffect = 0;
            string strQuery = "UPDATE A2ZACCOUNT SET AccStatus ='" + dto.AccStatus + "' where  AccNo='" + dto.AccNo + "' ";
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
