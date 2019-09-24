USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSAccountDataUpdate]    Script Date: 08/12/2018 3:55:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Sp_CSAccountDataUpdate]
(
@AccType int,
@AccNo Bigint,
@CuType smallint,
@CuNo int,
@MemberNo int,
@Opendate smalldatetime,
@DepositAmount money,
@Period smallint,
@WithDrawalAC smallint,
@InterestCalculation smallint,
@MatruityDate smalldatetime,
@MatruityAmount money,
@InterestWithdrawal smallint,
@AutoRenewal smallint,
@LoanAmount money,
@NoInstallment smallint,
@MonthlyInstallment money,
@LastInstallment money,
@InterestRate money,
@ContractInt smallint,
@GracePeriod smallint,
@LoaneeACType int,
@LoaneeMemberNo int,
@AccCertNo varchar(50),
@SpInstruction varchar(50),
@CorrAccType smallint,
@CorrAccNo int,
@AutoTransferSavings smallint,
@OldAccountNo varchar(50),
@FixedDepositAmount money,
@FixedMthInt money,
@AccAtyClass smallint,
@AccBenefitDate smalldatetime,
@AccOrgAmt money,
@AccPrincipal money,
@AccRenwlAmt money,
@AccRenwlDate smalldatetime,
@AccAnniDate smalldatetime,
@AccTotalDep money,
@lblDepositFlag smallint,
@AccDueIntAmt money,
@LastODIntDate smalldatetime
)

AS
BEGIN

DECLARE @trnDate smalldatetime;
DECLARE @CurrDepTotAmt money;
DECLARE @RestDepTotAmt money;

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

IF @AccAtyClass = 4
   BEGIN
       SET @CurrDepTotAmt = (SELECT AccTotalDep FROM A2ZACCOUNT WHERE 
       AccType=@AccType AND AccNo=@AccNo AND CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemberNo);
       SET @RestDepTotAmt = (@AccTotalDep - @CurrDepTotAmt); 
   END

UPDATE dbo.A2ZACCOUNT SET

AccType=@AccType, 
AccNo=@AccNo,
CuType=@CuType, 
CuNo=@CuNo,
MemNo=@MemberNo, 
AccOpenDate=@Opendate,
AccMonthlyDeposit=@DepositAmount,
AccPeriod=@Period,
AccDebitFlag=@WithDrawalAC,
AccIntFlag=@InterestCalculation,
AccMatureDate=@MatruityDate,
AccMatureAmt=@MatruityAmount,
AccIntType=@InterestWithdrawal,
AccAutoRenewFlag=@AutoRenewal,
AccLoanSancAmt=@LoanAmount,
AccNoInstl=@NoInstallment,
AccLoanInstlAmt=@MonthlyInstallment,
AccLoanLastInstlAmt=@LastInstallment,
AccIntRate=@InterestRate,
AccContractIntFlag=@ContractInt,
AccLoanGrace=@GracePeriod,
AccLoaneeAccType=@LoaneeACType,
AccLoaneeMemNo=@LoaneeMemberNo,
AccCertNo=@AccCertNo,
AccSpecialNote=@SpInstruction,
AccCorrAccType=@CorrAccType,
AccCorrAccNo=@CorrAccNo,
AccAutoTrfFlag=@AutoTransferSavings,
AccOldNumber=@OldAccountNo,
AccFixedAmt=@FixedDepositAmount,
AccFixedMthInt=@FixedMthInt,
AccAtyClass=@AccAtyClass,
AccBenefitDate=@AccBenefitDate, 
AccOrgAmt=@AccOrgAmt, 
AccPrincipal=@AccPrincipal, 
AccRenwlAmt=@AccRenwlAmt, 
AccRenwlDate=@AccRenwlDate,
AccAnniDate=@AccAnniDate,
AccTotalDep=@AccTotalDep, 
AccDueIntAmt=@AccDueIntAmt, 
AccODIntDate=@LastODIntDate 

WHERE AccType=@AccType AND AccNo=@AccNo AND CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemberNo 


IF @AccAtyClass = 4
   BEGIN
        IF @lblDepositFlag = 1
           BEGIN
                UPDATE A2ZPENSIONDEFAULTER SET DepositFlag = 1,
                UptoDueDepositAmt = (UptoDueDepositAmt - @RestDepTotAmt),
                PayableDepositAmt = ((UptoDueDepositAmt - @RestDepTotAmt) + CalDepositAmt),
                CurrDueDepositAmt = ((UptoDueDepositAmt - @RestDepTotAmt + CalDepositAmt) - PaidDepositAmt)                                                                                                           				
                WHERE CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemberNo AND 
                AccType=@AccType AND AccNo=@AccNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                YEAR(TrnDate) = YEAR(@trnDate);
           END
        ELSE
            BEGIN
                UPDATE A2ZPENSIONDEFAULTER SET DepositFlag = 0, 
                UptoDueDepositAmt = (UptoDueDepositAmt - @RestDepTotAmt),
                PayableDepositAmt = ((UptoDueDepositAmt - @RestDepTotAmt) + CalDepositAmt),
                CurrDueDepositAmt = ((UptoDueDepositAmt - @RestDepTotAmt + CalDepositAmt) - PaidDepositAmt)                                                                       				
                WHERE CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemberNo AND 
                AccType=@AccType AND AccNo=@AccNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                YEAR(TrnDate) = YEAR(@trnDate);
            END

         UPDATE A2ZPENSIONDEFAULTER SET PayablePenalAmt = 0 
         WHERE CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemberNo AND 
         AccType=@AccType AND AccNo=@AccNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
         YEAR(TrnDate) = YEAR(@trnDate) AND (UptoDueDepositAmt = 0 OR UptoDueDepositAmt < 0);


   END


COMMIT TRANSACTION
	SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH

END

GO

