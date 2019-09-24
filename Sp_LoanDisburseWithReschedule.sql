USE A2ZCSMCUS
GO

ALTER PROCEDURE [dbo].[Sp_LoanDisburseWithReschedule] (@AccNo NVARCHAR(16), @UserId INT, 
@TransactionAmt MONEY, @nFlag INT)

--
--
-- Sp_LoanDisburseWithReschedule '5430044000000002',1,45000,0
--

AS
BEGIN

	INSERT INTO [A2ZCSMCUS].[dbo].[A2ZACCOUNTP]
	([AccType],[AccNo],[CuType],[CuNo],[MemNo],[AccOpenDate],[AccStatus],[AccStatusDate],[AccStatusNote],
	[AccStatusP],[AccStatusDateP],[AccMonthlyDeposit],[AccPeriod],[AccPrevPeriod],[AccDebitFlag],[AccIntFlag],
	[AccMatureDate],[AccPrevMatureDate],[AccMatureAmt],[AccIntType],[AccAutoRenewFlag],[AccIntRate],
	[AccPrevIntRate],[AccContractIntFlag],[AccSpecialNote],[AccCorrMemNo],[AccCorrAccType],[AccCorrAccNo],
	[AccAutoTrfFlag],[AccFixedAmt],[AccFixedMthInt],[AccBenefitDate],[AccPrevBenefitDate],[AccBalance],
	[AccPrincipal],[AccInterest],[AccTotalDep],[AccTotIntPaid],[AccTotPenaltyPaid],[AccLienAmt],[AccOpeningBal],
	[AccLastTrnTypeU],[AccLastTrnDateU],[AccLastTrnAmtU],[AccLastTrnTypeS],[AccLastTrnDateS],[AccLastTrnAmtS],
	[AccPrevTrnTypeU],[AccPrevTrnDateU],[AccPrevTrnAmtU],[AccPrevTrnTypeS],[AccPrevTrnDateS],[AccPrevTrnAmtS],
	[AccOrgAmt],[AccFirstRenwlDate],[AccRenwlDate],[AccPrevRenwlDate],[AccRenwlAmt],[AccPrevRenwlAmt],
	[AccAnniDate],[AccPrevAnniDate],[AccAnniAmt],[AccPrevAnniAmt],[AccIntWdrawn],[AccPrevIntWdrawn],
	[AccTotIntWdrawn],[AccPrevTotIntWdrawn],[AccNoAnni],[AccPrevNoAnni],[AccNoRenwl],[AccPrevNoRenwl],
	[AccNoBenefit],[AccPrevNoBenefit],[AccLastIntCr],[AccPrevLastIntCr],[AccPeriodDays],[AccPeriodMonths],
	[AccRaNetInt],[AccAdjChgd],[AccPrinChgd],[AccPrevRaCurr],[AccPrevRaLedger],[AccLoanAppNo],[AccLoanSancAmt],
	[AccLoanSancDate],[AccLoanExpiryDate],[AccNoInstl],[AccLoanInstlAmt],[AccLoanLastInstlAmt],
	[AccLoanFirstInstlDt],[AccLoanGrace],[AccDisbAmt],[AccDisbDate],[AccPrevDisbAmt],[AccPrevDisbDate],
	[AccODIntDate],[AccPrevODIntDate],[AccLoaneeAccType],[AccLoaneeMemNo],[AccProvBalance],[AccProvCalDate],
	[AccPrevProvBalance],[AccPrevProvCalDate],[AccAdjProvBalance],[AccDuePrincAmt],[AccDueIntAmt],
	[AccDuePenalAmt],[AccPrevDuePrincAmt],[AccPrevDueIntAmt],[AccLastDuePrincAmt],[AccLastDueIntAmt],
	[AccHoldIntAmt],[AccAtyClass],[AccOldNumber],[OldCuNo],[InputBy],[VerifyBy],[ApprovBy],[InputByDate],
	[VerifyByDate],[ApprovByDate],[ValueDate],[AccCertNo],[UserId],[CreateDate],[CalFDAmount],[CalLastDate],
	[CalFDate],[CalTDate],[CalNofDays],[CalUptoMthProvision],[CalCurrMthProvision],[CalIntRate],[CalOrgInterest],
	[CalPaidInterest],[CalInterest],[CalPeriod],[CalEncashmentInt],[CalEncashment],[CalProvAdjCr],[CalProvAdjDr],
	[CalClosingFees],[AccOpBal],[AccDRBal],[AccCRBal],[AccCloBal],[OldMemNo],[AccTrfAccNo],[AccOwnBalance],
	[AccOfficeBalance],[AccOfficeOpBal],[AccOwnOpBal],[AccOpInt],[ReInputBy],[ReInputByDate],[AccTodaysOpBalance],
	[CalPaidDeposit],[CalPaidPenal],[CalAccTotalDep],[CalDisbAmt],[CalDueInterest])
	SELECT 
	[AccType],[AccNo],[CuType],[CuNo],[MemNo],[AccOpenDate],[AccStatus],[AccStatusDate],[AccStatusNote],
	[AccStatusP],[AccStatusDateP],[AccMonthlyDeposit],[AccPeriod],[AccPrevPeriod],[AccDebitFlag],[AccIntFlag],
	[AccMatureDate],[AccPrevMatureDate],[AccMatureAmt],[AccIntType],[AccAutoRenewFlag],[AccIntRate],
	[AccPrevIntRate],[AccContractIntFlag],[AccSpecialNote],[AccCorrMemNo],[AccCorrAccType],[AccCorrAccNo],
	[AccAutoTrfFlag],[AccFixedAmt],[AccFixedMthInt],[AccBenefitDate],[AccPrevBenefitDate],[AccBalance],
	[AccPrincipal],[AccInterest],[AccTotalDep],[AccTotIntPaid],[AccTotPenaltyPaid],[AccLienAmt],[AccOpeningBal],
	[AccLastTrnTypeU],[AccLastTrnDateU],[AccLastTrnAmtU],[AccLastTrnTypeS],[AccLastTrnDateS],[AccLastTrnAmtS],
	[AccPrevTrnTypeU],[AccPrevTrnDateU],[AccPrevTrnAmtU],[AccPrevTrnTypeS],[AccPrevTrnDateS],[AccPrevTrnAmtS],
	[AccOrgAmt],[AccFirstRenwlDate],[AccRenwlDate],[AccPrevRenwlDate],[AccRenwlAmt],[AccPrevRenwlAmt],
	[AccAnniDate],[AccPrevAnniDate],[AccAnniAmt],[AccPrevAnniAmt],[AccIntWdrawn],[AccPrevIntWdrawn],
	[AccTotIntWdrawn],[AccPrevTotIntWdrawn],[AccNoAnni],[AccPrevNoAnni],[AccNoRenwl],[AccPrevNoRenwl],
	[AccNoBenefit],[AccPrevNoBenefit],[AccLastIntCr],[AccPrevLastIntCr],[AccPeriodDays],[AccPeriodMonths],
	[AccRaNetInt],[AccAdjChgd],[AccPrinChgd],[AccPrevRaCurr],[AccPrevRaLedger],[AccLoanAppNo],[AccLoanSancAmt],
	[AccLoanSancDate],[AccLoanExpiryDate],[AccNoInstl],[AccLoanInstlAmt],[AccLoanLastInstlAmt],
	[AccLoanFirstInstlDt],[AccLoanGrace],[AccDisbAmt],[AccDisbDate],[AccPrevDisbAmt],[AccPrevDisbDate],
	[AccODIntDate],[AccPrevODIntDate],[AccLoaneeAccType],[AccLoaneeMemNo],[AccProvBalance],[AccProvCalDate],
	[AccPrevProvBalance],[AccPrevProvCalDate],[AccAdjProvBalance],[AccDuePrincAmt],[AccDueIntAmt],
	[AccDuePenalAmt],[AccPrevDuePrincAmt],[AccPrevDueIntAmt],[AccLastDuePrincAmt],[AccLastDueIntAmt],
	[AccHoldIntAmt],[AccAtyClass],[AccOldNumber],[OldCuNo],[InputBy],[VerifyBy],[ApprovBy],[InputByDate],
	[VerifyByDate],[ApprovByDate],[ValueDate],[AccCertNo],[UserId],[CreateDate],[CalFDAmount],[CalLastDate],
	[CalFDate],[CalTDate],[CalNofDays],[CalUptoMthProvision],[CalCurrMthProvision],[CalIntRate],[CalOrgInterest],
	[CalPaidInterest],[CalInterest],[CalPeriod],[CalEncashmentInt],[CalEncashment],[CalProvAdjCr],[CalProvAdjDr],
	[CalClosingFees],[AccOpBal],[AccDRBal],[AccCRBal],[AccCloBal],[OldMemNo],[AccTrfAccNo],[AccOwnBalance],
	[AccOfficeBalance],[AccOfficeOpBal],[AccOwnOpBal],[AccOpInt],[ReInputBy],[ReInputByDate],[AccTodaysOpBalance],
	[CalPaidDeposit],[CalPaidPenal],[CalAccTotalDep],[CalDisbAmt],[CalDueInterest]
	FROM [A2ZCSMCUS].[dbo].[A2ZACCOUNT]
	WHERE AccNo = @AccNo;


	DECLARE @AccLoanExpiryDate SMALLDATETIME;
	DECLARE @ProcessDate SMALLDATETIME;
	DECLARE @RemaingMonth INT;

	DECLARE @AccBalance MONEY;
	DECLARE @DisburseAmount MONEY;
	DECLARE @NewInstlAmt MONEY;
	DECLARE @NewLastInstlAmt MONEY;

	DECLARE @TmpAmount MONEY;

	SET @ProcessDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);
	SET @AccLoanExpiryDate = (SELECT AccLoanExpiryDate FROM A2ZACCOUNT WHERE AccNo = @AccNo);

	SET @AccBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @AccNo);

	--SET @DisburseAmount = @TransactionAmt + ABS(@AccBalance);

	SET @DisburseAmount =  ABS(@AccBalance);

	SET @RemaingMonth = DATEDIFF(MONTH,@ProcessDate,@AccLoanExpiryDate);

	SET @NewInstlAmt = ROUND((@DisburseAmount / @RemaingMonth),0);

	SET @TmpAmount = @NewInstlAmt * @RemaingMonth;

	SET @NewLastInstlAmt = @NewInstlAmt;

	IF @TmpAmount < @DisburseAmount
		BEGIN
			SET @NewLastInstlAmt = @NewInstlAmt + (@DisburseAmount - @TmpAmount);
		END
	ELSE
		BEGIN
			SET @NewLastInstlAmt = @NewInstlAmt - (@TmpAmount - @DisburseAmount);
		END

	UPDATE A2ZACCOUNT SET AccNoInstl = @RemaingMonth,
		AccLoanInstlAmt = @NewInstlAmt, 
		AccLoanLastInstlAmt = @NewLastInstlAmt,
		AccLoanFirstInstlDt = @ProcessDate
		WHERE AccNo = @AccNo;


END
