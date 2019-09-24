USE A2ZCSMCUS
GO

ALTER PROCEDURE [dbo].[Sp_InsertPreviousAccount] (@AccNo NVARCHAR(16), @UserId INT, @nFlag INT)

--
-- @nFlag = 1 = Delete Account from Account Table 
--
-- Sp_InsertPreviousAccount '5430044000000002',1,0
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


	IF @nFlag = 1
		BEGIN
			DELETE FROM [A2ZCSMCUS].[dbo].[A2ZACCOUNT] WHERE AccNo = @AccNo;
		END

	DECLARE @ID INT;

	SET @ID = (SELECT TOP 1 ID FROM A2ZLOANDEFAULTER WHERE AccNo = @AccNo ORDER BY ID DESC)

	UPDATE A2ZLOANDEFAULTER SET CalPrincAmt = 0, CalIntAmt = 0, UptoDuePrincAmt = 0, UptoDueIntAmt = 0,
	PayablePrincAmt = 0, PayableIntAmt = 0, PayablePenalAmt = 0, PaidPrincAmt = 0, PaidIntAmt = 0,
	PaidPenalAmt = 0, CurrDuePrincAmt = 0, CurrDueIntAmt = 0, NoDueInstalment = 0
	WHERE ID = @ID;

END
