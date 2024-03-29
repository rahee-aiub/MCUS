USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_NewGenerateLoanDefaulter2]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_NewGenerateLoanDefaulter2] (@AccNo BIGINT)

AS
BEGIN

DECLARE @ID INT;
DECLARE @TrnDate SMALLDATETIME;
DECLARE @CalPrincAmt MONEY;
DECLARE @OpeningBalance MONEY;
DECLARE @InterestRate MONEY;
DECLARE @PaidPrincAmt MONEY;
DECLARE @PaidIntAmt MONEY;
DECLARE @DisbursementAmt MONEY;

DECLARE @CalInterest MONEY;
DECLARE @CalOpening MONEY;

DECLARE @CurrDueP MONEY;
DECLARE @CurrDueI MONEY;

DECLARE @PayablePrincAmt MONEY;
DECLARE @PayableIntAmt MONEY;

DECLARE @CurrDuePrincAmt MONEY;
DECLARE @CurrDueIntAmt MONEY;

DECLARE @UptoDuePrincAmt MONEY;
DECLARE @UptoDueIntAmt MONEY;

DECLARE @AccLoanSancAmt MONEY;
DECLARE @AccDisbAmt MONEY;

DECLARE @nFlag INT;

SET @CalOpening = 0;
SET @nFlag = 0;

DECLARE defTable CURSOR FOR
SELECT ID,TrnDate,OpeningBalance,InterestRate,PaidPrincAmt,PaidIntAmt,
DisbursementAmt,CalPrincAmt,PayablePrincAmt,PayableIntAmt,
CurrDuePrincAmt,CurrDueIntAmt,UptoDuePrincAmt,UptoDueIntAmt,AccLoanSancAmt,AccDisbAmt
FROM A2ZLOANDEFAULTER
WHERE AccNo = @AccNo;

OPEN defTable;
FETCH NEXT FROM defTable INTO @ID,@TrnDate,@OpeningBalance,@InterestRate,@PaidPrincAmt,@PaidIntAmt,
@DisbursementAmt,@CalPrincAmt,@PayablePrincAmt,@PayableIntAmt,
@CurrDuePrincAmt,@CurrDueIntAmt,@UptoDuePrincAmt,@UptoDueIntAmt,@AccLoanSancAmt,@AccDisbAmt;
WHILE @@FETCH_STATUS = 0 
	BEGIN
		SET @CalOpening = @CalOpening + (@OpeningBalance)
		SET @CalInterest = ROUND(((ABS(@CalOpening) * @InterestRate) / 1200),0);
		
			IF ABS(@CalOpening) <= @CalPrincAmt AND @AccDisbAmt >= @AccLoanSancAmt
					BEGIN
					UPDATE A2ZLOANDEFAULTER SET CalPrincAmt = ABS(@CalOpening)
					WHERE ID = @ID;
						--SET @CurrDueP = 0;
						--SET @CurrDueI = 0;
						SET @CalPrincAmt = ABS(@CalOpening);
						--SET @CalInterest = 0;
					END
			
			--IF @OpeningBalance = 0 AND @DisbursementAmt <> 0 
				IF @CalOpening >= 0 
					BEGIN
					UPDATE A2ZLOANDEFAULTER SET CalPrincAmt = 0
					WHERE ID = @ID;
						SET @CurrDueP = 0;
						--SET @CurrDueI = 0;
						SET @CalPrincAmt = 0;
						SET @CalInterest = 0;
					END

		IF @nFlag = 0
			BEGIN
				SET @nFlag = 1;
				SET @CurrDueP = @CalPrincAmt + @UptoDuePrincAmt - @PaidPrincAmt;
				SET @CurrDueI = @CalInterest + @UptoDueIntAmt - @PaidIntAmt;

				

				UPDATE A2ZLOANDEFAULTER SET CalIntAmt = @CalInterest,
					PayablePrincAmt = @CalPrincAmt + @UptoDuePrincAmt,
					PayableIntAmt = @CalInterest + @UptoDueIntAmt,
					CurrDuePrincAmt = @CurrDueP,
					CurrDueIntAmt =   @CurrDueI				
				WHERE ID = @ID;
			END
		ELSE
			BEGIN
				IF @CurrDueP < 0 
					BEGIN
						SET @CurrDueP = 0;
					END			
				IF @CurrDueI < 0 
					BEGIN
						SET @CurrDueI = 0;
					END		

				IF @CalPrincAmt < 0 
					BEGIN
						SET @CalPrincAmt = 0;
					END			
				IF @CalInterest < 0 
					BEGIN
						SET @CalInterest = 0;
					END				

				UPDATE A2ZLOANDEFAULTER SET CalIntAmt = @CalInterest,
					UptoDuePrincAmt = @CurrDueP,
					UptoDueIntAmt = @CurrDueI,
					PayablePrincAmt = @CalPrincAmt + @CurrDueP,
					PayableIntAmt = @CalInterest + @CurrDueI,
					CurrDuePrincAmt = @CalPrincAmt + @CurrDueP - @PaidPrincAmt,
					CurrDueIntAmt =   @CalInterest + @CurrDueI - @PaidIntAmt,
					OpeningBalance = @CalOpening				
				WHERE ID = @ID;

				IF ABS(@CalOpening) <= @CalPrincAmt  AND @AccDisbAmt >= @AccLoanSancAmt AND @CurrDueP >= ABS(@CalOpening)
					BEGIN
					UPDATE A2ZLOANDEFAULTER SET UptoDuePrincAmt = ABS(@CalOpening),
						   CalPrincAmt = 0,PayablePrincAmt = ABS(@CalOpening),CurrDuePrincAmt = ABS(@CalOpening)
					WHERE ID = @ID;
		END
				
				IF ABS(@CalOpening) <= @PayablePrincAmt  
					BEGIN
					UPDATE A2ZLOANDEFAULTER SET PayablePrincAmt = ABS(@CalOpening),CurrDuePrincAmt = ABS(@CalOpening)
					WHERE ID = @ID;
		END

		IF (@CalOpening) >= 0  
					BEGIN
					UPDATE A2ZLOANDEFAULTER SET UptoDuePrincAmt = 0
					WHERE ID = @ID;
		END

				SET @CurrDueP = @CalPrincAmt + @CurrDueP - @PaidPrincAmt;
				SET @CurrDueI = @CalInterest + @CurrDueI - @PaidIntAmt;
		END

		SET @CalOpening = @CalOpening + @PaidPrincAmt - @DisbursementAmt;

		FETCH NEXT FROM defTable INTO @ID,@TrnDate,@OpeningBalance,@InterestRate,@PaidPrincAmt,@PaidIntAmt,
		@DisbursementAmt,@CalPrincAmt,@PayablePrincAmt,@PayableIntAmt,
		@CurrDuePrincAmt,@CurrDueIntAmt,@UptoDuePrincAmt,@UptoDueIntAmt,@AccLoanSancAmt,@AccDisbAmt;
	END

CLOSE defTable; 
DEALLOCATE defTable;

--UPDATE A2ZLOANDEFAULTER SET UptoDuePrincAmt = 0 WHERE UptoDuePrincAmt < 0;
--UPDATE A2ZLOANDEFAULTER SET UptoDueIntAmt = 0 WHERE UptoDueIntAmt < 0;

END






GO
