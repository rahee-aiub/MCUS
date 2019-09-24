USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSConversionPensionDefaulter]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_CSConversionPensionDefaulter]
AS
BEGIN

/*

EXECUTE Sp_CSConversionPensionDefaulter

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
    DECLARE @AccMonthlyDeposit MONEY;

    DECLARE @AccOpenDate smalldatetime;
    DECLARE @AccPeriod INT;	
    
    DECLARE @AccTotalDep MONEY;
   
    
    DECLARE @DuePrincAmt MONEY;
    DECLARE @DueIntAmt MONEY;

    
	DECLARE @ACCLASTDT SMALLDATETIME;
	
	DECLARE @duePrincipal MONEY;
	DECLARE @dueInterest MONEY;

    DECLARE @CalDepositAmt MONEY; 
    DECLARE @UptoDueDepositAmt MONEY; 
    DECLARE @PayableDepositAmt MONEY; 
    DECLARE @PayablePenalAmt MONEY; 
    DECLARE @PaidDepositAmt MONEY; 
    DECLARE @PaidPenalAmt MONEY; 
    
    DECLARE @NoDueDeposit MONEY; 

    DECLARE @TargetDeposit MONEY; 


    DECLARE @TrnDate smalldatetime;
    DECLARE @NewTrnDate smalldatetime;

    DECLARE @fDate smalldatetime;
    DECLARE @tDate smalldatetime;
    DECLARE @noMonths INT;
    DECLARE @paidMonths INT;
    DECLARE @unpaidMonths INT;
    DECLARE @dueMonths INT;
    DECLARE @DepositFlag INT;

     
    TRUNCATE TABLE A2ZPENSIONDEFAULTER;
    
    SET @TrnDate = (DATEADD(MONTH,1,@fDate ))

    SET @NewTrnDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @TrnDate)), DATEADD(mm, 1, @TrnDate)));

    SET @NewTrnDate = '2017-11-30';


	DECLARE tableAcc CURSOR FOR
	SELECT CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccPeriod,AccMonthlyDeposit,AccTotalDep FROM A2ZACCOUNT 
	WHERE AccStatus < 97 AND AccAtyClass = 4 AND CuNo!=0 AND AccTodaysOpBalance > 0;

	OPEN tableAcc;
	FETCH NEXT FROM tableAcc INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@AccOpenDate,@AccPeriod,@AccMonthlyDeposit,@AccTotalDep; 
		WHILE @@FETCH_STATUS = 0 
		BEGIN			

             SET @noMonths = 0;
             SET @paidMonths = 0;
             SET @unpaidMonths = 0;

             SET @fDate = @AccOpenDate
             SET @tDate = @NewTrnDate
             SET @noMonths = ((DATEDIFF(m, @fDate, @tDate)) + 0); 
             IF  @noMonths > @accPeriod
                 BEGIN
                     SET @noMonths = @accPeriod;
                 END

             SET @paidMonths = 0;

             IF @AccMonthlyDeposit !=0
                BEGIN
                     SET @paidMonths = (@AccTotalDep / @AccMonthlyDeposit);  
                END

             SET @TargetDeposit = (@AccMonthlyDeposit * @accPeriod);  


             SET @unpaidMonths = (@noMonths - @paidMonths); 

			 SET @CalDepositAmt = ABS(@AccMonthlyDeposit);
             
             SET @UptoDueDepositAmt = 0;

             IF @unpaidMonths > 0
                BEGIN
                    SET @UptoDueDepositAmt = ABS(@AccMonthlyDeposit * @unpaidMonths);
			    END 
              
             IF @unpaidMonths < 0
                BEGIN
                    SET @UptoDueDepositAmt = (@AccMonthlyDeposit * @unpaidMonths);
			    END 

                     
             SET @PayableDepositAmt = (@CalDepositAmt + @UptoDueDepositAmt);
              
             SET @PayablePenalAmt = 0;
             
             SET @NoDueDeposit = @unpaidMonths;    

             IF @UptoDueDepositAmt > 0
                BEGIN
                    SET @PayablePenalAmt = (5 * @NoDueDeposit);
                END   

             SET @PaidDepositAmt = 0;
             SET @PaidPenalAmt = 0;

             SET @DepositFlag = 0;

             IF @TargetDeposit = @AccTotalDep
                BEGIN
                    SET @UptoDueDepositAmt = 0;
                    SET @PayableDepositAmt = 0;
                    SET @PayablePenalAmt = 0;
                    SET @NoDueDeposit = 0;
                    SET @DepositFlag = 1; 
                END
       

         INSERT INTO A2ZPENSIONDEFAULTER(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,CaldepositAmt,UptoDueDepositAmt,PayableDepositAmt,PayablePenalAmt,PaidDepositAmt,PaidPenalAmt,CurrDueDepositAmt,NoDueDeposit,DepositFlag) 
    VALUES (@NewTrnDate,@CuType,@CuNo,@MemNo,@AccType,@AccNo,@CalDepositAmt,@UptoDueDepositAmt,@PayableDepositAmt,@PayablePenalAmt,@PaidDepositAmt,@PaidPenalAmt,@PayableDepositAmt,@NoDueDeposit,@DepositFlag);
		
    
		FETCH NEXT FROM tableAcc INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@AccOpenDate,@AccPeriod,@AccMonthlyDeposit,@AccTotalDep; 
		END

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
