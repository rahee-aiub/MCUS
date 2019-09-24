USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSReCalculatePensionDefaulter]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_CSReCalculatePensionDefaulter](@accNo BIGINT)
AS
BEGIN


BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

	DECLARE @nCount TINYINT;
	DECLARE @sql NVARCHAR(MAX)

    DECLARE @CuType INT;	
    DECLARE @CuNo INT;
    DECLARE @MemNo INT;	
    DECLARE @AccType INT;	
   
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


    DECLARE @trnDate smalldatetime;
    DECLARE @NewTrnDate smalldatetime;

    DECLARE @fDate smalldatetime;
    DECLARE @tDate smalldatetime;
    DECLARE @noMonths INT;
    DECLARE @paidMonths INT;
    DECLARE @unpaidMonths INT;
    DECLARE @dueMonths INT;

    
    SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);


    SET @NewTrnDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @trnDate)), DATEADD(mm, 1, @trnDate)));

    
	DECLARE tableAcc CURSOR FOR
	SELECT CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccPeriod,AccMonthlyDeposit,AccTotalDep FROM A2ZACCOUNT 
	WHERE AccNo=@accNo;

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

             IF @UptoDueDepositAmt > 0
                BEGIN
                    SET @PayablePenalAmt = 5;
                END 
             
             SET @NoDueDeposit = @unpaidMonths;      

             SET @PaidDepositAmt = 0;
             SET @PaidPenalAmt = 0;
       

         
    UPDATE A2ZPENSIONDEFAULTER SET CaldepositAmt=@CalDepositAmt, 
                                   UptoDueDepositAmt=@UptoDueDepositAmt, 
                                   PayableDepositAmt=@PayableDepositAmt, 
                                   PayablePenalAmt=@PayablePenalAmt,
                                   CurrDueDepositAmt=@PayableDepositAmt,
                                   NoDueDeposit=@NoDueDeposit
    WHERE TrnDate = @NewTrnDate AND AccNo = @accNo; 

    
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
