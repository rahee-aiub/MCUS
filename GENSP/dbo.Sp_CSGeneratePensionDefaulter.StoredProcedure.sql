USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGeneratePensionDefaulter]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_CSGeneratePensionDefaulter] (@fDate VARCHAR(10))
AS
BEGIN

/*

EXECUTE Sp_CSGeneratePensionDefaulter '2017-09-30'

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
       
	DECLARE @ACCLASTDT SMALLDATETIME;
	
    DECLARE @CalDepositAmt MONEY; 
    DECLARE @UptoDueDepositAmt MONEY; 
    DECLARE @PayableDepositAmt MONEY; 
    DECLARE @PayablePenalAmt MONEY; 
    DECLARE @PaidDepositAmt MONEY; 
    DECLARE @PaidPenalAmt MONEY; 
    
    DECLARE @NoDueDeposit MONEY; 

    DECLARE @TrnDate smalldatetime;
    DECLARE @NewTrnDate smalldatetime;
  

    SET @TrnDate = (DATEADD(MONTH,1,@fDate ))

    SET @NewTrnDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @TrnDate)), DATEADD(mm, 1, @TrnDate)));

 

	DECLARE tableAccount CURSOR FOR
	SELECT CuType,CuNo,MemNo,AccType,AccNo,AccMonthlyDeposit FROM A2ZACCOUNT 
	WHERE AccStatus < 97 AND AccAtyClass = 4 AND CuNo !=0;



	OPEN tableAccount;
	FETCH NEXT FROM tableAccount INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@AccMonthlyDeposit; 
		WHILE @@FETCH_STATUS = 0 
		BEGIN

           SET @UptoDueDepositAmt = (SELECT CurrDueDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
                       CuType=@CuType AND CuNo=@CuNo AND MemNo=@MemNo AND 
                       AccType=@AccType AND AccNo=@AccNo AND MONTH(TrnDate) = MONTH(@fDate) AND 
                       YEAR(TrnDate) = YEAR(@fDate));

                        
            IF @UptoDueDepositAmt IS NULL
               BEGIN
                    SET @UptoDueDepositAmt = 0;
               END                

			SET @CalDepositAmt = ABS(@AccMonthlyDeposit);
			
            
--             IF @UptoDueDepositAmt < 0
--                BEGIN
--                     SET @UptoDueDepositAmt = 0;
--                END         

             SET @PayableDepositAmt = (@CalDepositAmt + @UptoDueDepositAmt);
                          
             SET @PayablePenalAmt = 0;

             
             		 
         SET @NoDueDeposit = 0;
    
         IF @UptoDueDepositAmt > 0
            BEGIN
                SET @NoDueDeposit = (@UptoDueDepositAmt / ABS(@AccMonthlyDeposit));
            END

        IF @UptoDueDepositAmt > 0
                BEGIN
                    SET @PayablePenalAmt = (5 * @NoDueDeposit);
                END 

        SET @PaidDepositAmt = 0;
        
        SET @PaidPenalAmt = 0;

         INSERT INTO A2ZPENSIONDEFAULTER(TrnDate,CuType,CuNo,MemNo,AccType,AccNo,CalDepositAmt,UptoDueDepositAmt,PayableDepositAmt,PayablePenalAmt,PaidDepositAmt,PaidPenalAmt,CurrDueDepositAmt,NoDueDeposit) 
    VALUES (@NewTrnDate,@CuType,@CuNo,@MemNo,@AccType,@AccNo,@CalDepositAmt,@UptoDueDepositAmt,@PayableDepositAmt,@PayablePenalAmt,@PaidDepositAmt,@PaidPenalAmt,@PayableDepositAmt,@NoDueDeposit);
		
		FETCH NEXT FROM tableAccount INTO @CuType,@CuNo,@MemNo,@AccType,@AccNo,@AccMonthlyDeposit; 
		END;

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
