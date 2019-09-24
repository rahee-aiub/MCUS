
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSCalculateEncashmentMSplus](@cuType smallint,@cuNo int,@memNo int,@accType int,@accNo Bigint )  

--EXECUTE Sp_CSCalculateEncashmentMSplus 3,34,0,17,1730034000000003  


AS
BEGIN

--DECLARE @cuType smallint;
--DECLARE @cuNo int;
--DECLARE @memNo int;
--DECLARE @accType int;
--DECLARE @accNo Bigint;


DECLARE @ProcDate VARCHAR(10);

DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @Pre12MDate smalldatetime;


DECLARE @3MIntRate smallmoney;
DECLARE @6MIntRate smallmoney;

DECLARE @12MBenefitAmt money;
DECLARE @24MBenefitAmt money;

DECLARE @paidBenefit money;
DECLARE @calOrgInterest money;
DECLARE @calInterest money;
DECLARE @remInterest money;
DECLARE @calEncashment money;
DECLARE @AccTotIntWdrawn money;
DECLARE @calIntRate smallmoney;


DECLARE @accBalance money;
DECLARE @accProvBalance money;
DECLARE @accFixedAmt money;
DECLARE @accFixedMthInt money;
DECLARE @accPeriod smallint;
DECLARE @accNoBenefit smallint;


DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accBenefitDate smalldatetime;
DECLARE @accMatureDate smalldatetime;
DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;
DECLARE @noMonths int;
-----------------------------end--------------------------------


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


--
--SET @cuType = 3;
--SET @cuNo = 15;
--SET @memNo = 0;
--SET @accType = 17;
--SET @accNo = 173001500000002;


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

SET @tDate = @trnDate;

    EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;

    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);


    SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accOpenDate = (SELECT AccOpenDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accRenwlDate = (SELECT AccRenwlDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accBenefitDate = (SELECT AccBenefitDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accMatureDate = (SELECT AccMatureDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accPeriod = (SELECT AccPeriod FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @AccTotIntWdrawn = (SELECT AccTotIntWdrawn FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
--    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accProvBalance = (SELECT AccProvBalance FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);    

    SET @accFixedAmt = (SELECT AccFixedAmt FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accFixedMthInt = (SELECT AccFixedMthInt FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accNoBenefit = (SELECT AccNoBenefit FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    SET @3MIntRate = (SELECT AtyIntRate FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = 0 AND AtyRecords = 99 AND AtyPeriod = 3);
    
    SET @6MIntRate = (SELECT AtyIntRate FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = 0 AND AtyRecords = 99 AND AtyPeriod = 6);
    
    SET @12MBenefitAmt = (SELECT AtyMaturedAmt FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = @memType AND AtyPeriod = 12);

    SET @12MBenefitAmt = ((@accFixedAmt /100000) * @12MBenefitAmt);
    

    SET @24MBenefitAmt = (SELECT AtyMaturedAmt FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = @memType AND AtyPeriod = 24);

    SET @24MBenefitAmt = ((@accFixedAmt /100000) * @24MBenefitAmt);

    

    IF @accRenwlDate IS NULL
       BEGIN
       SET @fDate = @accOpenDate;
       END
    ELSE  
       BEGIN
       SET @fDate = @accRenwlDate;
       END
    
    SET @noDays = ((DATEDIFF(d, @fDate, @tDate)) + 0);
    SET @noMonths = ((DATEDIFF(m, @fDate, @tDate)) + 0);

    
--    PRINT @accOpenDate;
--    PRINT @accRenwlDate;
--    PRINT @fDate;
--    PRINT @tDate;
--    PRINT @noDays;
--    PRINT @noMonths;
--    print @12MBenefitAmt;
--    print @accNoBenefit;
--


    IF @noMonths < 3
       BEGIN
       SET @calInterest = 0;

       END
    IF @noMonths > 3 AND @noMonths < 6
       BEGIN
       SET @calIntRate = @3MIntRate;
       SET @calInterest = Round((((@accFixedAmt * @3MIntRate * @noDays) / 36500)), 0);
       END

    IF @noMonths > 6 AND @noMonths < 12
       BEGIN
       SET @calIntRate = @6MIntRate;
       SET @calInterest = Round((((@accFixedAmt * @6MIntRate * @noDays) / 36500)), 0);
       END
      
    IF @noMonths > 12 AND @noMonths < 24
       BEGIN
       SET @calInterest = ( @12MBenefitAmt * @accNoBenefit);
       
       END

    IF @noMonths > 24 AND @noMonths < 36
       BEGIN
       SET @calInterest = ( @24MBenefitAmt * @accNoBenefit);
       END 
    
--    print @calInterest;

    SET @calOrgInterest = @calInterest;

    SET @paidBenefit = ((@accFixedMthInt * @accNoBenefit) - @accProvBalance); 
    
--	print @calInterest;
--	print @accFixedMthInt;
--	print @accProvBalance;
--	print @AccTotIntWdrawn

--    SET @remInterest = (@calInterest - @AccTotIntWdrawn);   
SET @remInterest = (@calInterest - @paidBenefit);   
--	
    IF @remInterest < 0
       BEGIN
           SET @calEncashment = (@accFixedAmt + @remInterest); 
       END
    ELSE
       BEGIN    
           SET @calEncashment = (@accFixedAmt); 
       END
        PRINT @noMonths;
        PRINT @accNoBenefit;
		PRINT @12MBenefitAmt;
		PRINT @calInterest;
        PRINT @paidBenefit;
		PRINT @remInterest;
        PRINT @calEncashment;
	
    UPDATE A2ZACCOUNT SET CalInterest=0,CalEncashment=0   
    WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo;

    UPDATE A2ZACCOUNT SET CalPaidInterest=@paidBenefit,CalOrgInterest=@calOrgInterest,CalInterest=@remInterest,CalEncashment=@calEncashment   
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
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

