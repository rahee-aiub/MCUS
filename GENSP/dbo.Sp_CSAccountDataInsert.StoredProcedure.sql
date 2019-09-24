USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAccountDataInsert]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSAccountDataInsert]
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
@SpInstruction varchar(50),
@CorrAccType smallint,
@CorrAccNo int,
@AutoTransferSavings smallint,
@OldAccountNo varchar(50),
@FixedDepositAmount money,
@FixedMthInt money,
@AccStatus int,
@AccAtyClass smallint,
@AccBenefitDate smalldatetime,
@InputBy smallint,
@InputByDate datetime
)

AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON


INSERT INTO dbo.A2ZACCOUNT(AccType, AccNo,CuType,CuNo,MemNo,AccOpenDate,AccMonthlyDeposit,AccPeriod,
       AccDebitFlag,AccIntFlag,AccMatureDate,AccMatureAmt,AccIntType,AccAutoRenewFlag,
       AccLoanSancAmt,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccIntRate,
       AccContractIntFlag,AccLoanGrace,AccLoaneeAccType,AccLoaneeMemNo,AccSpecialNote,
       AccCorrAccType,AccCorrAccNo,AccAutoTrfFlag,AccOldNumber,AccFixedAmt,AccFixedMthInt,
       AccStatus,AccAtyClass,AccBenefitDate,InputBy,InputByDate,AccTodaysOpBalance) 

VALUES(@AccType,@AccNo,@CuType,@CuNo,@MemberNo,@Opendate,@DepositAmount,@Period,
       @WithDrawalAC,@InterestCalculation,@MatruityDate,@MatruityAmount,@InterestWithdrawal,@AutoRenewal,
       @LoanAmount,@NoInstallment,@MonthlyInstallment,@LastInstallment,@InterestRate,
       @ContractInt,@GracePeriod,@LoaneeACType,@LoaneeMemberNo,@SpInstruction,
       @CorrAccType,@CorrAccNo,@AutoTransferSavings,@OldAccountNo,@FixedDepositAmount,@FixedMthInt,
       @AccStatus,@AccAtyClass,@AccBenefitDate,@InputBy,@InputByDate,0)


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
