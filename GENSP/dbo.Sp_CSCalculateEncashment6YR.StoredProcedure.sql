USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateEncashment6YR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSCalculateEncashment6YR](@cuType smallint,@cuNo int,@memNo int,@accType int,@accNo Bigint )  

----ALTER PROCEDURE [dbo].[Sp_CSCalculateEncashment6YR] 

AS
BEGIN

DECLARE @accProvBalance money;

--DECLARE @cuType smallint;
--DECLARE @cuNo int;
--DECLARE @memNo int;
--DECLARE @accType smallint;
--DECLARE @accNo int;

DECLARE @ProcDate VARCHAR(10);

DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @Pre12MDate smalldatetime;


DECLARE @IntRate smallmoney;
DECLARE @1IntRate smallmoney;
DECLARE @2IntRate smallmoney;

DECLARE @AdjProvAmt money;
DECLARE @AdjProvAmtCr money;
DECLARE @AdjProvAmtDr money;

DECLARE @calInterest money;
DECLARE @calEncashment money;

DECLARE @CalIntRate smallmoney;

DECLARE @fdAmount money;
DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accRenwlAmt money;
DECLARE @newRenwlAmt money;
DECLARE @accPeriod smallint;


DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accAnniDate smalldatetime;
DECLARE @newRenwlDate smalldatetime;

DECLARE @accMatureDate smalldatetime;
DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;
DECLARE @noMonths int;
DECLARE @countR int;
-----------------------------end--------------------------------


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


--SET @cuType = 1;
--SET @cuNo = 1;
--SET @memNo = 5030;
--SET @accType = 16;
--SET @accNo = 5030;


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

SET @tDate = @trnDate;

    EXECUTE SpM_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;

    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);



    SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accOpenDate = (SELECT AccOpenDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accRenwlDate = (SELECT AccRenwlDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accAnniDate = (SELECT AccAnniDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accMatureDate = (SELECT AccMatureDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accPeriod = (SELECT AccPeriod FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
--    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accOrgAmt = (SELECT AccOrgAmt FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accRenwlAmt = (SELECT AccRenwlAmt FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

	SET @IntRate = (SELECT PrmIntRate FROM A2ZCSPARAM WHERE AccType = 12);

    SET @1IntRate = (SELECT AtyIntRate FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = @memType AND AtyPeriod = 12);
  
    SET @IntRate = (SELECT AtyIntRate FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = @memType AND AtyRecords = 99);

     
    SET @accProvBalance = (SELECT AccProvBalance FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo);    


    IF @accRenwlDate IS NULL
       BEGIN			
           SET @fdAmount = @accOrgAmt
           SET @fDate = @accOpenDate
       END
    ELSE	 
       BEGIN
           SET @fdAmount =  @accRenwlAmt
	       SET @fDate = @accRenwlDate
       END
	

   
    SET @Pre12MDate = (DATEADD(month,12,@fDate));
    

    SET @noDays = ((DATEDIFF(d, @fDate, @tDate)) + 1);
  
    
    IF @accAnniDate IS NULL
       BEGIN
           SET @calInterest = 0
           SET @calEncashment = (@fdAmount + @calInterest) 
           SET @calInterest = (@calEncashment - @accBalance)
           IF  @calInterest < 0
               BEGIN 
                   SET @calEncashment = (@accBalance + @calInterest)
               END
           ELSE
               BEGIN
                   SET @calEncashment = (@accBalance)
               END               
       END
    ELSE
       BEGIN
           SET @CalIntRate = @IntRate;  
           SET @calInterest = Round((((@fdAmount * @IntRate * @noDays) / 36500)), 0);  
           SET @calEncashment = (@fdAmount + @calInterest) 
           SET @calInterest = (@calEncashment - @accBalance)
           IF  @calInterest < 0
               BEGIN 
                   SET @calEncashment = (@accBalance + @calInterest)
               END
           ELSE
               BEGIN
                   SET @calEncashment = (@accBalance)
               END                
       END       


--    IF  @calInterest > 0
--        BEGIN
           SET @AdjProvAmt = Round(((@calInterest - @accProvBalance)), 0);

           IF @AdjProvAmt = 0
              BEGIN	
                  SET @AdjProvAmtCr = 0;
                  SET @AdjProvAmtDr = 0;
              END
        
           IF @AdjProvAmt > 0
              BEGIN	
                  SET @AdjProvAmtCr = @AdjProvAmt;
                  SET @AdjProvAmtDr = 0;
              END
        
           IF @AdjProvAmt <0           
              BEGIN	
                  SET @AdjProvAmtCr = 0;
                  SET @AdjProvAmtDr = Abs(@AdjProvAmt);
              END

--        END

        
--    PRINT @calInterest;
--    PRINT @calEncashment;
   
        
    ---UPDATE A2ZACCOUNT SET CalInterest=0,CalEncashment=0   
    ---WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo;
    
    

    UPDATE A2ZACCOUNT SET CalIntRate=@CalIntRate,CalInterest=@calInterest,CalEncashment=@calEncashment,
    CalFDate=@fDate,CalNofDays=@noDays,CalFDAmount = @fdAmount,CalProvAdjCr=@AdjProvAmtCr,CalProvAdjDr=@AdjProvAmtDr        
    WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo;
   

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

----exec Sp_CSProcessRenewalFDR

GO
