USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGenerateLoanDefaulter]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_CSGenerateLoanDefaulter] (@fDate VARCHAR(10))
AS
BEGIN

/*

EXECUTE Sp_CSGenerateLoanDefaulter '2017-10-31'

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
    
	DECLARE @AccDisbAmt MONEY;
	DECLARE @AccLoanSancAmt MONEY;

    DECLARE @EstimatePrincAmt MONEY;
    DECLARE @EstimateIntAmt MONEY;
    
    DECLARE @DuePrincAmt MONEY;
    DECLARE @DueIntAmt MONEY;

    DECLARE @ProcDate VARCHAR(10);

    DECLARE @tDate VARCHAR(10);

    
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

    DECLARE @PaidPrincAmt MONEY;
    DECLARE @PaidIntAmt MONEY;
    DECLARE @PaidPenalAmt MONEY; 

    DECLARE @PrevPayablePrincAmt MONEY;
    DECLARE @PrevPayableIntAmt MONEY;

    DECLARE @NoDueInstalment MONEY;	

    
    DECLARE @RoundFlag tinyint;

    DECLARE @TrnDate smalldatetime;
    DECLARE @NewTrnDate smalldatetime;
  

  

--    SET @ProcDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@fDate) AS VARCHAR(2)) + '-' + CAST(DAY(@fDate) AS VARCHAR(2))

    SET @TrnDate = (DATEADD(MONTH,1,@fDate ))

    SET @NewTrnDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @TrnDate)), DATEADD(mm, 1, @TrnDate)));


--    EXECUTE A2ZCSMCUS..Sp_CSAccountBalanceCheck @fDate;

    
--=============  UPDATE Principal and Interest Amount ==========
	DECLARE tableAccount CURSOR FOR
	SELECT CuType,CuNo,MemNo,AccType,AccNo,AccTodaysOpBalance,AccIntRate,AccLoanInstlAmt,AccLoanLastInstlAmt,AccDisbAmt,AccLoanSancAmt FROM A2ZACCOUNT 
	WHERE AccTodaysOpBalance <> 0 AND AccStatus < 97 AND AccAtyClass = 6 AND CuNo !=0;

--    AccDuePrincAmt < ABS(AccBalance)

	OPEN tableAccount;
	FETCH NEXT FROM tableAccount INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@AccBalance,@AccIntRate,@AccLoanInstlAmt,@AccLoanLastInstlAmt,
				@AccDisbAmt,@AccLoanSancAmt; 
		WHILE @@FETCH_STATUS = 0 
		BEGIN

--           EXECUTE Sp_CSGenerateSingleAccountBalance @AccNo,@ProcDate,0;

--           SET @AccBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @AccNo);


           SET @UptoDuePrincAmt = (SELECT CurrDuePrincAmt FROM A2ZLOANDEFAULTER WHERE 
                       CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemNo AND 
                       AccType=@AccType AND AccNo=@AccNo AND MONTH(TrnDate) = MONTH(@fDate) AND 
                       YEAR(TrnDate) = YEAR(@fDate));

            SET @UptoDueIntAmt = (SELECT CurrDueIntAmt FROM A2ZLOANDEFAULTER WHERE 
                       CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemNo AND 
                       AccType=@AccType AND AccNo=@AccNo AND MONTH(TrnDate) = MONTH(@fDate) AND 
                       YEAR(TrnDate) = YEAR(@fDate));

            IF @AccBalance >= 0 AND @AccDisbAmt >= @AccLoanSancAmt
			BEGIN
				SET @UptoDuePrincAmt = 0;
			END

            IF @UptoDuePrincAmt IS NULL
               BEGIN
                    SET @UptoDuePrincAmt = 0;
               END

            IF @UptoDueIntAmt IS NULL
               BEGIN
                    SET @UptoDueIntAmt = 0;
               END

            SET @RoundFlag = (SELECT PrmRoundFlag FROM A2ZCSPARAM WHERE AccType = @AccType);

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
			IF @AccLoanInstlAmt > ABS(@AccBalance) AND @AccDisbAmt >= @AccLoanSancAmt
				BEGIN
					SET @CalPrincAmt = ABS(@AccBalance)
				END
            
			IF @AccBalance > 0 AND @AccDisbAmt >= @AccLoanSancAmt
			BEGIN
				SET @CalPrincAmt = 0;
			END
			
			IF @AccBalance > 0 
			BEGIN
				SET @CalIntAmt = 0;
			END

             IF @UptoDuePrincAmt < 0
                BEGIN
                     SET @UptoDuePrincAmt = 0;
                END

             IF @UptoDueIntAmt < 0
                BEGIN
                     SET @UptoDueIntAmt = 0;
                END 

             IF @UptoDuePrincAmt = ABS(@AccBalance)
                BEGIN
                     SET @CalPrincAmt = 0;
                END            

             SET @PayablePrincAmt = (@CalPrincAmt + @UptoDuePrincAmt);
             SET @PayableIntAmt = (@CalIntAmt + @UptoDueIntAmt);
             
             SET @PayablePenalAmt = 0;

             IF @UptoDuePrincAmt != 0 OR @UptoDueIntAmt != 0
                BEGIN
                    SET @PayablePenalAmt = 10;
                END 
             		 
         SET @NoDueInstalment = 0;
    
         IF @UptoDuePrincAmt !=0
            BEGIN
                SET @NoDueInstalment = (@UptoDuePrincAmt / ABS(@AccLoanInstlAmt));
            END


        SET @PaidPrincAmt = 0;
        SET @PaidIntAmt = 0;
        SET @PaidPenalAmt = 0;

--		IF @AccBalance > 0
--			BEGIN
--				SET @CalPrincAmt = 0;
--				SET @CalIntAmt = 0;
--			END

         INSERT INTO A2ZLOANDEFAULTER(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,CalPrincAmt,CalIntAmt,UptoDuePrincAmt,UptoDueIntAmt,PayablePrincAmt,PayableIntAmt,PayablePenalAmt,PaidPrincAmt,PaidIntAmt,PaidPenalAmt,CurrDuePrincAmt,CurrDueIntAmt,NoDueInstalment) 
    VALUES (@NewTrnDate,@CuType,@CuNo,@MemNo,@AccType,@AccNo,@CalPrincAmt,@CalIntAmt,@UptoDuePrincAmt,@UptoDueIntAmt,@PayablePrincAmt,@PayableIntAmt,@PayablePenalAmt,@PaidPrincAmt,@PaidIntAmt,@PaidPenalAmt,@PayablePrincAmt,@PayableIntAmt,@NoDueInstalment);
		
		FETCH NEXT FROM tableAccount INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@AccBalance,@AccIntRate,@AccLoanInstlAmt,@AccLoanLastInstlAmt,@AccDisbAmt,@AccLoanSancAmt; 
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
