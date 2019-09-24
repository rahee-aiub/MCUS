
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSCalculateODInterest](@cuType smallint,@cuNo int,@memNo int,@accType smallint,@accNo Bigint,@trnDate VARCHAR(10))  

AS
BEGIN


--DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @calInterest money;
DECLARE @calEncashment money;

DECLARE @ProcDate VARCHAR(10);

DECLARE @accBalance money;

DECLARE @accOpenDate smalldatetime;
DECLARE @accIntRate smallmoney;
DECLARE @accODIntDate smalldatetime;

DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;
-----------------------------end--------------------------------



BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


--SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @tDate = @trnDate;

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

    SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accOpenDate = (SELECT AccOpenDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accIntRate = (SELECT AccIntRate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo); 
    SET @accODIntDate = (SELECT AccODIntDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
        
    EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;

    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);


--    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
--     
    
    IF @accODIntDate IS NULL
       BEGIN			
           SET @fDate = @accOpenDate
       END
    ELSE	 
       BEGIN
	       SET @fDate = @accODIntDate
       END
    

    SET @noDays = ((DATEDIFF(d, @fDate, @tDate)) + 0);
      
    SET @calInterest = Round((((@accBalance * @accIntRate * @noDays) / 36500)), 0);  

    SET @calEncashment = (@accBalance - @calInterest);        
        
    UPDATE A2ZACCOUNT SET CalInterest=0,CalEncashment=0   
    WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo;

    UPDATE A2ZACCOUNT SET CalInterest=@calInterest,CalEncashment=@calEncashment   
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

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

