
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSCalculateManualRenewal6YR](@userID INT,@aType INT, @accNo Bigint,@trnDate VARCHAR(10))  

AS
BEGIN

/*

EXECUTE Sp_CSManualCalculateRenewal6YR 1

*/

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @trnCode int;
DECLARE @accProvBalance money;
DECLARE @accPeriod int;

DECLARE @ProcDate VARCHAR(10);

DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accPrincipal money;
DECLARE @accLastIntCr money;
DECLARE @accIntRate smallmoney;
DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accAnniDate smalldatetime;
DECLARE @accRenwlAmt money;
DECLARE @accPeriodMonths int;
DECLARE @accMatureDate smalldatetime;
DECLARE @accNoAnni int;
DECLARE @accNoRenwl int;
DECLARE @accIntWdrawn money;
DECLARE @accContractIntFlag int;

--DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @calInterest money;
DECLARE @calLastIntCr money;

DECLARE @RoundFlag tinyint;


DECLARE @calInterestPaid money;
DECLARE @calInterestDue money;
DECLARE @newIntRate smallmoney;
DECLARE @newNoRenwl int;
DECLARE @newNoAnni int;
DECLARE @newRenwlDate smalldatetime;
DECLARE @newRenwlAmt money;

DECLARE @newMatureDate smalldatetime;

DECLARE @AdjProvAmt money;
DECLARE @AdjProvAmtCr money;
DECLARE @AdjProvAmtDr money;

DECLARE @AdjChrg money;
DECLARE @NetInt money;
DECLARE @fdAmount money;

DECLARE @noDays int;
DECLARE @noMonths INT;

DECLARE @NewPeriod  int;
DECLARE @NetAmount money;

DECLARE @memType int;
DECLARE @cuNumber nvarchar(10);
DECLARE @memName nvarchar(50);

DECLARE @FuncOpt smallint;
DECLARE @PayType smallint;
DECLARE @TrnDesc nvarchar(50);
DECLARE @TrnType tinyint;
DECLARE @TrnDrCr tinyint;
DECLARE @TrnContraDrCr tinyint;
DECLARE @ShowInterest tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


--SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);


------------- READ A2ZTRNCTRL FILE -------------
SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @aType);


SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @aType AND FuncOpt=15);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @aType AND FuncOpt=15);
SET @TrnDesc= (SELECT TrnDesc FROM A2ZTRNCTRL WHERE AccType = @aType AND FuncOpt=15);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @aType AND FuncOpt=15);
SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @aType AND FuncOpt=15);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @aType AND FuncOpt=15);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @aType AND FuncOpt=15);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @aType AND FuncOpt=15);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @aType AND FuncOpt=15);

IF @trnDrCr = 0     
   BEGIN 
      SET @TrnContraDrCr = 1
   END
ELSE
  BEGIN
     SET @TrnContraDrCr = 0
  END

---------  END OF READ A2ZTRNCTRL FILE -------------------------------

--EXECUTE Sp_CSRebuildFDRMatureDate @AccNo;

---------- Refresh Workfile ----------
--TRUNCATE TABLE WFCSMANUALRENEWFDR;

DELETE FROM WFCSMANUALRENEWFDR WHERE AccNo = @accNo;

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

---------- End of Refresh Workfile ----------

DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccPeriod,AccBalance,AccOrgAmt,AccPrincipal,AccLastIntCr,AccIntRate,
AccOpenDate,AccRenwlDate,AccAnniDate,AccRenwlAmt,AccPeriod,AccMatureDate,AccNoAnni,AccNoRenwl,AccContractIntFlag
FROM A2ZACCOUNT WHERE AccNo = @accNo AND AccBalance > 0 AND AccStatus < 97 AND @trnDate >= AccMatureDate;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accPeriod,@accBalance,@accOrgAmt,@accPrincipal,@accLastIntCr,@accIntRate,
@accOpenDate,@accRenwlDate,@accAnniDate,@accRenwlAmt,@accPeriodMonths,@accMatureDate,@accNoAnni,@accNoRenwl,@accContractIntFlag;

WHILE @@FETCH_STATUS = 0 
	BEGIN

    SET @cuNumber = LTRIM(STR(@cuType) + '-' +LTRIM(STR(@cuNo)));
-------- Find Member Type ----------------------

	SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    SET @RoundFlag = (SELECT PrmRoundFlag FROM A2ZCSPARAM WHERE AccType = @accType);
  

-------- End of Find Member Type ----------------------
    SET @accProvBalance = (SELECT AccProvBalance FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo);

   
    EXECUTE SpM_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;

    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);



--------- CALCULATE NO OF DAYS -----------------------------
    
    SET @tDate = @trnDate;
    
    IF @accRenwlDate IS NULL
       BEGIN			
            SET @fdAmount = @accOrgAmt;
            SET @fDate = @accOpenDate;
       END
          
    IF @accRenwlDate IS NOT NULL
       BEGIN			
            SET @fdAmount =  @accRenwlAmt;
		    SET @fDate = @accRenwlDate;
       END
  

     SET @NetAmount = (@fdAmount * 2);

     SET @CalInterest = Round(((@NetAmount - @accBalance)), 0);

     IF @CalInterest < 0  
       BEGIN
           SET @CalInterest = 0;
       END     


     SET @newRenwlAmt = Round(((@accBalance + @CalInterest)), 0);


-------- Generate New Interest Rate -------------

    
	SET @newIntRate = (SELECT AtyIntRate FROM A2ZATYSLAB WHERE AtyAccType = @aType AND
						AtyFlag = @memType AND AtyPeriod = @accPeriodMonths);


    SET @NewPeriod = (SELECT TOP 1 AtyPeriod FROM A2ZATYSLAB WHERE AtyAccType = @aType AND
						AtyFlag = @memType AND AtyRecords = 0);
       
    
    IF @accContractIntFlag = 1
       BEGIN  
       SET @NewPeriod = @accPeriodMonths;
       SET @newIntRate = @accIntRate
       END

-------- End of Generate New Interest Rate -------

       SET @newMatureDate = (DATEADD(month,@NewPeriod,@accMatureDate))

       SET @newNoRenwl = Round(( @accNoRenwl + 1), 0);
       SET @newNoAnni = 0;

       SET @newRenwlDate = (DATEADD(day,0,@accMatureDate))


       SET @AdjProvAmt = Round(((@CalInterest - @accProvBalance)), 0);
        
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
     
   
-------- Insert Record to Workfile ---------------
    
        
    SET @TrnDesc = (@TrnDesc + ' ' + CAST(@accIntRate AS nvarchar(7)));

	INSERT INTO WFCSMANUALRENEWFDR
	(CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDate,AccBalance,AccOrgAmt,AccPrincipal,AccIntRate,AccOpenDate,
	AccRenwlDate,AccAnniDate,AccRenwlAmt,AccPeriodMonths,AccMatureDate,AccNoAnni,AccNoRenwl,CalAdjProvCr,CalAdjProvDr,CalInterest,
    CalLastIntCr,NewIntRate,NewNoRenwl,NewNoAnni,NewRenwlDate,NewMatureDate,NewRenwlAmt,NoDays,FDAmount,CuNumber,MemName,
    FuncOpt,PayType,TrnDesc,TrnType,TrnDrCr,TrnContraDrCr,ShowInterest,TrnGLAccNoDr,TrnGLAccNoCr,UserId,ProcStat,CalFromDate)
	
    VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnDate,@accBalance,@accOrgAmt,@accPrincipal,@accIntRate,@accOpenDate,
	@accRenwlDate,@accAnniDate,@accRenwlAmt,@NewPeriod,@accMatureDate,@accNoAnni,@accNoRenwl,@AdjProvAmtCr,@AdjProvAmtDr,@calInterest,
    @calLastIntCr,@newIntRate,@newNoRenwl,@newNoAnni,@newRenwlDate,@newMatureDate,@newRenwlAmt,@noDays,@fdAmount,@cuNumber,@memName,
    @FuncOpt,@PayType,@TrnDesc,@TrnType,@TrnDrCr,@TrnContraDrCr,@ShowInterest,@TrnGLAccNoDr,@TrnGLAccNoCr,@userID,2,@fDate);

-------- End of Insert Record to Workfile ---------------


	FETCH NEXT FROM accTable INTO
		@cuType,@cuNo,@memNo,@accType,@accNo,@accPeriod,@accBalance,@accOrgAmt,@accPrincipal,@accLastIntCr,@accIntRate,
        @accOpenDate,@accRenwlDate,@accAnniDate,@accRenwlAmt,@accPeriodMonths,@accMatureDate,@accNoAnni,@accNoRenwl,@accContractIntFlag;


	END

CLOSE accTable; 
DEALLOCATE accTable;


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

