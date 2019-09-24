USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptLoanDefaulter]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[Sp_rptLoanDefaulter](@fDate SMALLDATETIME,@AccType int,@MthFrom money,@MthTill money)
AS
BEGIN

/*

EXECUTE Sp_rptLoanDefaulter '2015-09-30'

*/

--IF ISNULL(@fDate,0) = 0
--	BEGIN
--		RAISERROR ('Account Type is Null',10,1)
--		RETURN;
--	END
BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

	DECLARE @nCount TINYINT;
	DECLARE @sql NVARCHAR(MAX)

    DECLARE @CuType INT;	
    DECLARE @CuNo INT;
    DECLARE @MemNo INT;	
    
    DECLARE @AccNo BIGINT;	
    
    DECLARE @AccLoanInstlAmt MONEY;
    DECLARE @AccLoanLastInstlAmt MONEY;
    DECLARE @AccNoInstl INT;	
    
    DECLARE @EstimatePrincAmt MONEY;
    DECLARE @EstimateIntAmt MONEY;
    DECLARE @PaidPrincAmt MONEY;
    DECLARE @PaidIntAmt MONEY;
    DECLARE @DuePrincAmt MONEY;
    DECLARE @DueIntAmt MONEY;

    
	DECLARE @ACCLASTDT SMALLDATETIME;
	
	DECLARE @duePrincipal MONEY;
	DECLARE @dueInterest MONEY;

    DECLARE @CalPrincAmt MONEY;
    DECLARE @CalIntAmt MONEY;
    DECLARE @UptoDuePrincAmt MONEY;
    DECLARE @UptoDueIntAmt MONEY;
    DECLARE @CurrDuePrincAmt MONEY;
    DECLARE @CurrDueIntAmt MONEY;
    DECLARE @PayablePenalAmt MONEY; 

    DECLARE @PrevPayablePrincAmt MONEY;
    DECLARE @PrevPayableIntAmt MONEY;

    DECLARE @NoDueInstalment MONEY;	

    DECLARE @MemName nvarchar(50);
    DECLARE @CuNumber nvarchar(20);

           
    DECLARE @RoundFlag tinyint;

    DECLARE @TrnDate smalldatetime;
    DECLARE @NewTrnDate smalldatetime;
  
    DECLARE @AccOpenDate smalldatetime;
    DECLARE @AccLoanExpiryDate smalldatetime;
    DECLARE @AccLoanSancAmt money;
    DECLARE @AccBalance money;
    DECLARE @AccIntRate smallmoney;
     
    TRUNCATE TABLE WFLOANDEFAULTER;

--    SET @fDate = '2015-09-30';

--=============  UPDATE Principal and Interest Amount ==========
	DECLARE tableAccount CURSOR FOR
	SELECT CuType,CuNo,MemNo,AccType,AccNo,NoDueInstalment,CurrDuePrincAmt,CurrDueIntAmt FROM A2ZLOANDEFAULTER 
	WHERE AccType = @AccType AND NoDueInstalment BETWEEN @MthFrom AND @MthTill AND MONTH(TrnDate) = MONTH(@fDate) AND YEAR(TrnDate) = YEAR(@fDate) ORDER BY NoDueInstalment,CuType,CuNo;

   
	OPEN tableAccount;
	FETCH NEXT FROM tableAccount INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@NoDueInstalment,@CurrDuePrincAmt,@CurrDueIntAmt; 
		WHILE @@FETCH_STATUS = 0 
		BEGIN				
        
            SET @MemName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo);
            SET @CuNumber = LTRIM(STR(@CuType) + '-' + LTRIM(STR(@CuNo)));
			
            SET @AccOpenDate= (SELECT AccOpenDate FROM A2ZACCOUNT WHERE AccType = @AccType AND AccNo = @AccNo AND CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo);
            SET @AccLoanExpiryDate= (SELECT AccLoanExpiryDate FROM A2ZACCOUNT WHERE AccType = @AccType AND AccNo = @AccNo AND CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo);
            SET @AccLoanSancAmt= (SELECT AccLoanSancAmt FROM A2ZACCOUNT WHERE AccType = @AccType AND AccNo = @AccNo AND CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo);
            SET @AccBalance= (SELECT AccBalance FROM A2ZACCOUNT WHERE AccType = @AccType AND AccNo = @AccNo AND CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo);
            SET @AccIntRate= (SELECT AccIntRate FROM A2ZACCOUNT WHERE AccType = @AccType AND AccNo = @AccNo AND CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo);
            SET @AccNoInstl= (SELECT AccNoInstl FROM A2ZACCOUNT WHERE AccType = @AccType AND AccNo = @AccNo AND CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo);

              
         INSERT INTO WFLOANDEFAULTER(CuType,CuNo,MemNo,AccType,AccNo,MemName,DueNoInstl,DuePrincAmt,DueIntAmt,CuNumber,AccOpenDate,AccLoanExpiryDate,AccLoanSancAmt,AccIntRate,AccNoInstl,AccBalance) 
    VALUES (@CuType,@CuNo,@MemNo,@AccType,CAST(@AccNo AS VARCHAR(16)),@MemName,@NoDueInstalment,@CurrDuePrincAmt,@CurrDueIntAmt,@CuNumber,@AccOpenDate,@AccLoanExpiryDate,@AccLoanSancAmt,@AccIntRate,@AccNoInstl,@AccBalance);
		
		FETCH NEXT FROM tableAccount INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@NoDueInstalment,@CurrDuePrincAmt,@CurrDueIntAmt; 
		END;
--=============  End of UPDATE Principal and Interest Amount ==========
	CLOSE tableAccount;
	DEALLOCATE tableAccount;


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
