USE A2ZCSMCUS
GO


ALTER FUNCTION [dbo].[FNCheckLiquidityLoanAccount] (@CuType INT, @CuNo INT, @MemNo INT, @nFlag INT)

RETURNS INT

--
--	@Result = 0 Application Allow
--
--	@Result = 1 Not Allow Application
--  
--	@Result = 2 Current Due Interest Available in Defaulter Table
--
--  SELECT DBO.FNCheckLiquidityLoanAccount(3,10,0,0) 
--

AS
BEGIN

--================================

	DECLARE @AccNo BIGINT;

	DECLARE @TrnAmount MONEY;
	DECLARE @AccBalance MONEY;
	DECLARE @CurrDueIntAmt MONEY;

	DECLARE @Result INT;

	DECLARE @ProcessDate DATETIME;

	--SET @CuType = 3;
	--SET @CuNo = 44
	--SET @MemNo = 0;

	SET @ProcessDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

	SET @AccNo = (SELECT AccNo FROM A2ZACCOUNT
	WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = 54 AND AccStatus < 98);

	SET @TrnAmount = (SELECT (SUM(TrnCredit) - SUM(TrnDebit)) FROM A2ZTRANSACTION
	WHERE AccNo = @AccNo AND TrnFlag = 0);

	SET @AccBalance = (ISNULL((SELECT SUM(AccTodaysOpBalance) FROM A2ZACCOUNT
	WHERE AccNo = @AccNo),0)) + @TrnAmount;
	
	SET @Result = 0;

	IF @AccBalance < 0
		BEGIN
			SET @Result = 1;	-- Account Balance < 0
			GOTO ExitFNCheckLiquidityLoanAccount;
		END

	SET @CurrDueIntAmt = (SELECT CurrDueIntAmt FROM A2ZLOANDEFAULTER
	WHERE AccNo = @AccNo AND YEAR(TrnDate) = YEAR(@ProcessDate) AND
	MONTH(TrnDate) = MONTH(@ProcessDate));

	IF @CurrDueIntAmt > 0
		BEGIN
			SET @Result = 2;	-- Current Due Interest Available
			GOTO ExitFNCheckLiquidityLoanAccount;
		END


ExitFNCheckLiquidityLoanAccount:

	RETURN @Result;
END


