
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSConversionLoanDefaulter] (@fDate VARCHAR(10))
AS
BEGIN

/*

EXECUTE Sp_CSConversionLoanDefaulter '2016-04-30'

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
    DECLARE @AccType INT;	
    DECLARE @AccNo BIGINT;	
    DECLARE @AccBalance MONEY;
    DECLARE @AccIntRate MONEY;
    DECLARE @AccLoanInstlAmt MONEY;
    DECLARE @AccLoanLastInstlAmt MONEY;
    DECLARE @AccDuePrincAmt MONEY;
    DECLARE @AccDueIntAmt MONEY;
    DECLARE @NoDueInstalment MONEY;

    DECLARE @EstimatePrincAmt MONEY;
    DECLARE @EstimateIntAmt MONEY;
    DECLARE @PaidPrincAmt MONEY;
    DECLARE @PaidIntAmt MONEY;
    DECLARE @PaidPenalAmt MONEY;
    DECLARE @DuePrincAmt MONEY;
    DECLARE @DueIntAmt MONEY;

    
	DECLARE @ACCLASTDT SMALLDATETIME;
	
	DECLARE @duePrincipal MONEY;
	DECLARE @dueInterest MONEY;

    DECLARE @CalPrincAmt MONEY;
    DECLARE @CalIntAmt MONEY;
    DECLARE @UptoDuePrincAmt MONEY;
    DECLARE @UptoDueIntAmt MONEY;
    DECLARE @PayablePrincAmt MONEY;
    DECLARE @PayableIntAmt MONEY;
    DECLARE @PayablePenalAmt MONEY; 

    DECLARE @RoundFlag tinyint;

    DECLARE @TrnDate smalldatetime;
    DECLARE @NewTrnDate smalldatetime;
     
    TRUNCATE TABLE A2ZLOANDEFAULTER;
    
    SET @TrnDate = (DATEADD(MONTH,1,@fDate ))

    SET @NewTrnDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @TrnDate)), DATEADD(mm, 1, @TrnDate)));

    SET @NewTrnDate = '2017-07-31';

--	UPDATE A2ZACCOUNT SET AccDuePrincAmt = 0 WHERE AccAtyClass = 6 AND AccDuePrincAmt < 0;
--	UPDATE A2ZACCOUNT SET AccDueIntAmt = 0 WHERE AccAtyClass = 6 AND AccDueIntAmt < 0;

--=============  UPDATE Principal and Interest Amount ==========
	DECLARE tableAcc CURSOR FOR
	SELECT CuType,CuNo,MemNo,AccType,AccNo,AccTodaysOpBalance,AccIntRate,AccLoanInstlAmt,AccLoanLastInstlAmt FROM A2ZACCOUNT 
	WHERE AccTodaysOpBalance <> 0 AND AccStatus < 97 AND AccAtyClass = 6 AND CuNo!=0 AND AccOpenDate < '2017-07-01';

	OPEN tableAcc;
	FETCH NEXT FROM tableAcc INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@AccBalance,@AccIntRate,@AccLoanInstlAmt,@AccLoanLastInstlAmt; 
		WHILE @@FETCH_STATUS = 0 
		BEGIN				

			SET @RoundFlag = (SELECT PrmRoundFlag FROM A2ZCSPARAM WHERE AccType = @AccType);

            SET @CalIntAmt = 0;


            IF @RoundFlag = 1
               BEGIN    
                   SET @CalIntAmt = Round(ABS(((@AccBalance * @AccIntRate) / 1200)),0); 
               END

            IF @RoundFlag = 2
               BEGIN    
                  SET @CalIntAmt = floor(ABS((@AccBalance * @AccIntRate) / 1200)); 
               END
            IF @RoundFlag = 3
               BEGIN    
                  SET @CalIntAmt = ABS((@AccBalance * @AccIntRate) / 1200); 
               END
                
           

			SET @CalPrincAmt = ABS(@AccLoanInstlAmt);
			IF @AccLoanInstlAmt > ABS(@AccBalance)
				BEGIN
					SET @CalPrincAmt = ABS(@AccBalance)
				END
            

                          
             SET @PayablePrincAmt = @CalPrincAmt;
             SET @PayableIntAmt = @CalIntAmt;


             SET @UptoDuePrincAmt = 0;
             SET @UptoDueIntAmt = 0;
             SET @PayablePenalAmt = 0;
             SET @PaidPrincAmt = 0;
             SET @PaidIntAmt = 0;
             SET @PaidPenalAmt = 0;
             SET @NoDueInstalment = 0;

             
--             IF @UptoDuePrincAmt != 0 OR @UptoDueIntAmt != 0
--                BEGIN
--                    SET @PayablePenalAmt = 10;
--                END 
		

         INSERT INTO A2ZLOANDEFAULTER(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,CalPrincAmt,CalIntAmt,UptoDuePrincAmt,UptoDueIntAmt,PayablePrincAmt,PayableIntAmt,PayablePenalAmt,PaidPrincAmt,PaidIntAmt,PaidPenalAmt,CurrDuePrincAmt,CurrDueIntAmt,NoDueInstalment) 
    VALUES (@NewTrnDate,@CuType,@CuNo,@MemNo,@AccType,@AccNo,@CalPrincAmt,@CalIntAmt,@UptoDuePrincAmt,@UptoDueIntAmt,@PayablePrincAmt,@PayableIntAmt,@PayablePenalAmt,@PaidPrincAmt,@PaidIntAmt,@PaidPenalAmt,@PayablePrincAmt,@PayableIntAmt,@NoDueInstalment);
		
    
		FETCH NEXT FROM tableAcc INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@AccBalance,@AccIntRate,@AccLoanInstlAmt,@AccLoanLastInstlAmt; 
		END
--=============  End of UPDATE Principal and Interest Amount ==========
	CLOSE tableAcc;
	DEALLOCATE tableAcc;


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

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

