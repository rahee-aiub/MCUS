USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateODInterest]    Script Date: 07/23/2018 12:02:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Sp_CSCalculateODInterest](@cuType smallint,@cuNo int,@memNo int,@accType smallint,@accNo Bigint,@trnDate VARCHAR(10))  

AS
BEGIN

----exec Sp_CSCalculateODInterest 3,538,883,52,5230538008830001,'2018-07-17'



DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @ProcDate VARCHAR(10);

DECLARE @calInterest money;
DECLARE @calEncashment money;


DECLARE @accBalance money;

DECLARE @accOpenDate smalldatetime;
DECLARE @accIntRate smallmoney;
DECLARE @accODIntDate smalldatetime;

DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;

DECLARE @AccountNo   BIGINT;

DECLARE @TrnInterestAmt money;
DECLARE @TrnDueIntAmt money;

DECLARE @accHoldIntAmt money;
DECLARE @accDueIntAmt money;
DECLARE @calDueInterest   money;
-----------------------------end--------------------------------



BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


--SET @ProcDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @calDueInterest = 0;

SET @tDate = @trnDate;

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

    SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accOpenDate = (SELECT AccOpenDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accIntRate = (SELECT AccIntRate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo); 
    SET @accODIntDate = (SELECT AccODIntDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
	SET @accDueIntAmt = (SELECT AccDueIntAmt FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
	SET @accHoldIntAmt = (SELECT AccHoldIntAmt FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    
	IF @accDueIntAmt < 0
	   BEGIN
	       SET @accDueIntAmt = 0;
	   END
	    
    --EXECUTE SpM_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;

    SET @accBalance = (SELECT AccTodaysOpBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);


	--SET @calDueInterest = (SELECT TOP(1) TrnDueIntAmt FROM A2ZTRANSACTION WHERE AccNo = @accNo AND TrnFlag = 0 ORDER BY TrnDueIntAmt ASC);

	

	SET @TrnInterestAmt = (SELECT SUM(TrnInterestAmt) FROM A2ZTRANSACTION WHERE AccNo = @accNo AND TrnFlag = 0);
	SET @TrnDueIntAmt = (SELECT SUM(TrnDueIntAmt) FROM A2ZTRANSACTION WHERE AccNo = @accNo AND TrnFlag = 0 AND TrnInterestAmt = 0 AND (PayType = 351 or PayType = 355));
    
	
	IF @TrnDueIntAmt = 0 OR @TrnDueIntAmt IS NULL
	   BEGIN

             IF @accODIntDate = 0
                BEGIN			
                    SET @fDate = @accOpenDate
                END
             ELSE	 
                BEGIN
	                SET @fDate = @accODIntDate
                END
    
	                 

             SET @noDays = ((DATEDIFF(d, @fDate, @tDate)) + 0);

			
      
             SET @calInterest = Round((((@accBalance * @accIntRate * @noDays) / 36500)), 0);  

			
			
	   END

	

	
-----------------------------------------------------------------------

   
    IF @TrnInterestAmt <> 0 AND @TrnInterestAmt IS NOT NULL
	   BEGIN
	      
	       IF @TrnDueIntAmt = 0 OR @TrnDueIntAmt IS NULL
		      BEGIN
			       
			       SET @calDueInterest = ((ABS(@calInterest) + ISNULL(@accDueIntAmt,0) + ISNULL(@accHoldIntAmt,0)) - ISNULL(@TrnInterestAmt,0));
		           SET @calInterest = 0;
			  END
           ELSE
		      BEGIN
			      
			       SET @calDueInterest = ((ABS(@TrnDueIntAmt) + ISNULL(@accDueIntAmt,0) + ISNULL(@accHoldIntAmt,0)) - (ISNULL(@TrnInterestAmt,0) + ISNULL(@TrnDueIntAmt,0)));
		           SET @calInterest = @TrnDueIntAmt;
			  END      
	   END
    ELSE
	   BEGIN
	       
	       IF @TrnDueIntAmt = 0 OR @TrnDueIntAmt IS NULL
		      BEGIN
	               SET @calDueInterest = (@accDueIntAmt);
		      END
           ELSE
		      BEGIN
			      SET @calDueInterest = (@accDueIntAmt);
				  SET @calInterest = @TrnDueIntAmt;
			  END

		   
	   END
    
	SET @calEncashment = (@accBalance - @calInterest);     
		          
    UPDATE A2ZACCOUNT SET CalInterest=0,calDueInterest=0,CalEncashment=0   
    WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo;

    UPDATE A2ZACCOUNT SET CalInterest=@calInterest,CalDueInterest=@calDueInterest,CalEncashment=@calEncashment   
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

