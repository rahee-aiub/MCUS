USE A2ZCSMCUS
GO


ALTER FUNCTION [dbo].[FNCheckLiquidityLoanAmount] (@CuType INT, @CuNo INT, @MemNo INT, 
@LoanAmount MONEY, @nFlag INT)

RETURNS INT

--
--	@Result = 0 Loan Approved
--
--	@Result = 1 Loan Amount > Total Deposit
--	@Result = 2 Loan Amount > (Total Share + Saving)
--  @Result = 3 Loan Amount > Total Share
--
--	@Result = 9 Cheque Book Not Found
--  
--  SELECT DBO.FNCheckLiquidityLoanAmount(3,45,0,1000000000,0) 
--

AS
BEGIN

--================================
	--DECLARE @CuType INT;
	--DECLARE @CuNo INT;
	--DECLARE @MemNo INT;
	--DECLARE @LoanAmount MONEY;

	DECLARE @TrnAmount MONEY;
	DECLARE @TotalDeposit MONEY;
	DECLARE @TotalShareSavings MONEY;
	DECLARE @TotalShare MONEY;

	DECLARE @Result INT;

	DECLARE @LoanAmount20 MONEY;
	DECLARE @LoanAmount10 MONEY;
	DECLARE @LoanAmount25 MONEY;

	--SET @CuType = 3;
	--SET @CuNo = 23
	--SET @MemNo = 0;
	--SET @LoanAmount = 50000000;

	SET @LoanAmount20 = (@LoanAmount * 20) / 100;
	SET @LoanAmount10 = (@LoanAmount * 10) / 100;
	SET @LoanAmount25 = (@LoanAmount * 2.5) / 100;

	SET @TrnAmount = ISNULL((SELECT (SUM(TrnCredit) - SUM(TrnDebit)) FROM A2ZTRANSACTION
	WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType BETWEEN 11 AND 17),0);

	SET @TotalDeposit = (ISNULL((SELECT SUM(AccTodaysOpBalance) FROM A2ZACCOUNT
	WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo
	AND AccTodaysOpBalance > 0 AND AccAtyClass < 5 AND AccStatus < 98),0)) + @TrnAmount;

	SET @TrnAmount = ISNULL((SELECT (SUM(TrnCredit) - SUM(TrnDebit)) FROM A2ZTRANSACTION
	WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType IN (11,12)),0);

	SET @TotalShareSavings = (ISNULL((SELECT SUM(AccTodaysOpBalance) FROM A2ZACCOUNT
	WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo
	AND AccTodaysOpBalance > 0 AND AccType IN (11,12)),0)) + @TrnAmount;

	SET @TrnAmount = ISNULL((SELECT (SUM(TrnCredit) - SUM(TrnDebit)) FROM A2ZTRANSACTION
	WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = 11),0);

	SET @TotalShare = (ISNULL((SELECT SUM(AccTodaysOpBalance) FROM A2ZACCOUNT
	WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo
	AND AccTodaysOpBalance > 0 AND AccType = 11),0)) + @TrnAmount;

	--SET @TotalShareSavings = (@TotalShareSavings * 10) / 100;
	--SET @TotalShare = (@TotalShare * 2.5) / 100;

	--SELECT @LoanAmount,@TotalDeposit,@TotalShareSavings,@TotalShare

	SET @Result = 0;

	IF @LoanAmount20 > @TotalDeposit
		BEGIN
			SET @Result = 1;	-- Loan Amount > Total Deposit
			GOTO ExitFNCheckLiquidityLoanAmount;
		END

	IF @LoanAmount10 > @TotalShareSavings
		BEGIN
			SET @Result = 2;	-- Loan Amount > (Total Share + Saving)
			GOTO ExitFNCheckLiquidityLoanAmount;
		END

	IF @LoanAmount25 > @TotalShare
		BEGIN
			SET @Result = 3;	-- Loan Amount > Total Share
			GOTO ExitFNCheckLiquidityLoanAmount;
		END

ExitFNCheckLiquidityLoanAmount:

	RETURN @Result;
END


