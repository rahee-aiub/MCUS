USE A2ZCSMCUS
GO

ALTER PROCEDURE [dbo].[Sp_LoanDisburseWithRescheduleReverse] (@AccNo NVARCHAR(16), @UserId INT, 
@VchNo NVARCHAR(50), @nFlag INT)

--
--
-- Sp_LoanDisburseWithRescheduleReverse '5430784000000001',1,'37841',0
--


AS
BEGIN

	DECLARE @AccLoanExpiryDate SMALLDATETIME;
	DECLARE @ProcessDate SMALLDATETIME;
	DECLARE @RemaingMonth INT;

	DECLARE @AccBalance MONEY;
	DECLARE @DisburseAmount MONEY;
	DECLARE @NewInstlAmt MONEY;
	DECLARE @NewLastInstlAmt MONEY;
	DECLARE @AccPrevDisbDate SMALLDATETIME;

	DECLARE @TmpAmount MONEY;

	DECLARE @TotTrnCount INT;
	DECLARE @TrnAmount MONEY;
	DECLARE @VoucherNo NVARCHAR(50);

	SET @TotTrnCount = (SELECT COUNT(AccNo) FROM A2ZTRANSACTION
						WHERE AccNo = @AccNo AND TrnDebit > 0 AND PayType = 401);

	SET @TrnAmount = (SELECT TrnDebit FROM A2ZTRANSACTION
						WHERE AccNo = @AccNo AND TrnDebit > 0 AND PayType = 401 AND VchNo = @VchNo);

	SET @VoucherNo = (SELECT VoucherNo FROM A2ZTRANSACTION
						WHERE AccNo = @AccNo AND TrnDebit > 0 AND PayType = 401 AND VchNo = @VchNo);

	SET @ProcessDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);
	SET @AccLoanExpiryDate = (SELECT AccLoanExpiryDate FROM A2ZACCOUNT WHERE AccNo = @AccNo);

	SET @AccBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @AccNo) + @TrnAmount;

	IF @TotTrnCount = 1
		BEGIN
			UPDATE A2ZACCOUNT 
			SET AccBalance = AccP.AccBalance + @TrnAmount,
			AccPrincipal = AccP.AccPrincipal + @TrnAmount,
			AccDisbAmt = AccP.AccDisbAmt - @TrnAmount,
			AccDisbDate = AccP.AccPrevDisbDate,
			AccNoInstl = AccP.AccNoInstl,
			AccLoanInstlAmt = AccP.AccLoanInstlAmt,
			AccLoanLastInstlAmt = AccP.AccLoanLastInstlAmt,
			AccLoanFirstInstlDt = AccP.AccLoanFirstInstlDt		
			FROM A2ZACCOUNTP AccP
			INNER JOIN A2ZACCOUNT Acc ON Acc.AccNo = AccP.AccNo
			WHERE Acc.AccNo = @AccNo;

			DELETE FROM A2ZACCOUNTP WHERE AccNo = @AccNo AND AccCertNo = @VoucherNo;

			DELETE FROM A2ZTRANSACTION 
			WHERE AccNo = @AccNo AND PayType = 401 AND VoucherNo = @VoucherNo;
		END 
	ELSE
		BEGIN
			IF @TotTrnCount > 1
				BEGIN
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

					UPDATE A2ZACCOUNT SET AccBalance = @AccBalance,
							AccPrincipal = AccPrincipal + @TrnAmount,
							AccDisbAmt = AccDisbAmt - @TrnAmount,
							AccDisbDate = AccPrevDisbDate,
							AccLoanInstlAmt = @NewInstlAmt,
							AccLoanLastInstlAmt = @NewLastInstlAmt
							WHERE AccNo = @AccNo;

					DELETE FROM A2ZACCOUNTP WHERE AccNo = @AccNo AND AccCertNo = @VoucherNo;

					DELETE FROM A2ZTRANSACTION 
					WHERE AccNo = @AccNo AND PayType = 401 AND VoucherNo = @VoucherNo;
				END
		END

END
