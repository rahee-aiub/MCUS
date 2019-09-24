USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateRenewalMSplus]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSCalculateRenewalMSplus](@userID INT)  

AS
BEGIN

/*

EXECUTE Sp_CSCalculateRenewalMSplus 1

*/

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @accProvBalance money;
DECLARE @accPeriod int;


DECLARE @accBalance money;
DECLARE @accFixedAmt money;
DECLARE @accFixedMthInt money;
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

DECLARE @ProcDate VARCHAR(10);

DECLARE @trnDate smalldatetime;
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

DECLARE @atyRecords money; 
DECLARE @atyMaturedAmt money; 
DECLARE @newMthBenefit money;



--BEGIN TRY
--	BEGIN TRANSACTION
--		SET NOCOUNT ON


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

--EXECUTE A2ZCSMCUS..Sp_CSAccountLedgerBalance 17,@ProcDate;

------------- READ A2ZTRNCTRL FILE -------------
SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 17);


SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=15);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=15);
SET @TrnDesc= (SELECT TrnDesc FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=15);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=15);
SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=15);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=15);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=15);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=15);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=15);

IF @trnDrCr = 0     
   BEGIN 
      SET @TrnContraDrCr = 1
   END
ELSE
  BEGIN
     SET @TrnContraDrCr = 0
  END

---------  END OF READ A2ZTRNCTRL FILE -------------------------------


---------- Refresh Workfile ----------
TRUNCATE TABLE WFCSRENEWMSplus;
---------- End of Refresh Workfile ----------

DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccPeriod,AccTodaysOpBalance,AccFixedAmt,AccPrincipal,AccFixedMthInt,AccLastIntCr,AccIntRate,
AccOpenDate,AccRenwlDate,AccAnniDate,AccRenwlAmt,AccPeriod,AccMatureDate,AccNoAnni,AccNoRenwl,AccContractIntFlag
FROM A2ZACCOUNT WHERE AccType = 17 AND AccTodaysOpBalance > 0 AND AccStatus < 97 AND @trnDate >= AccMatureDate;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accPeriod,@accBalance,@accFixedAmt,@accPrincipal,@accFixedMthInt,@accLastIntCr,@accIntRate,
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

--    EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;
--
--    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);


--------- CALCULATE NO OF DAYS -----------------------------
    
    SET @tDate = @trnDate;

--   IF @accAnniDate IS NULL
--      IF @accRenwlDate IS NULL
--         BEGIN			
--              SET @fdAmount = @accOrgAmt
--              SET @fDate = @accOpenDate
--         END
--       ELSE	 
--          BEGIN
--              SET @fdAmount =  @accRenwlAmt
--		      SET @fDate = @accRenwlDate
--          END
--	ELSE
    IF @accRenwlDate IS NULL
		BEGIN
            SET @fdAmount =  @accFixedAmt
			SET @fDate = @accOpenDate
        END
    ELSE
        BEGIN
            SET @fdAmount =  @accRenwlAmt
			SET @fDate = @accRenwlDate
		END    


---     IF @accPeriodMonths > 12
---		BEGIN
---			SET @fdAmount = @accPrincipal
---			SET @fDate = (DATEADD(year,@accNoAnni,@fDate))
---		END
 
---       SET @fDate = (@fDate + (10000 * @accNoAnni));

--     IF @tDate > @accMatureDate
--		BEGIN     
			SET @tDate = @accMatureDate;
--		END

     SET @noDays = ((DATEDIFF(d, @fDate, @tDate)) + 0);

     SET @noMonths = (DATEDIFF(MONTH,@fDate,@tDate));

 
-------- Generate New Benefit Amount -------------
	SET @atyRecords = (SELECT AtyRecords FROM A2ZATYSLAB WHERE AtyAccType = 17 AND
						AtyFlag = @memType AND AtyPeriod = @accPeriodMonths);

    SET @atyMaturedAmt = (SELECT AtyMaturedAmt FROM A2ZATYSLAB WHERE AtyAccType = 17 AND
						AtyFlag = @memType AND AtyPeriod = @accPeriodMonths);
        

     set @newMthBenefit = ((@atyMaturedAmt / @atyRecords) * @accFixedAmt)


    IF @accContractIntFlag = 1
       BEGIN  
       SET @newMthBenefit = @accFixedMthInt
       END


-----------------------------------------------------------


    SET @newNoRenwl = Round(( @accNoRenwl + 1), 0);
    SET @newNoAnni = 0;

    SET @newRenwlDate = (DATEADD(day,0,@accMatureDate))
    SET @newMatureDate = (DATEADD(month,@accPeriodMonths,@accMatureDate))

--    SET @CalLastIntCr = Round(((@accLastIntCr + @CalInterest)), 0);

     
--    SET @AdjChrg = Round(((@AccPrincipal - @AccBalance)), 0);
--    SET @NetInt = Round(((@calInterest - @AdjChrg)), 0);
--    SET @newRenwlAmt = Round(((@AccPrincipal + @NetInt)), 0);
--
--    SET @AdjProvAmt = Round(((@calInterest - @accProvBalance)), 0);
--        
--        IF @AdjProvAmt = 0
--           BEGIN	
--              SET @AdjProvAmtCr = 0;
--              SET @AdjProvAmtDr = 0;
--           END
--        
--        IF @AdjProvAmt > 0
--           BEGIN	
--              SET @AdjProvAmtCr = @AdjProvAmt;
--              SET @AdjProvAmtDr = 0;
--           END
--        
--        IF @AdjProvAmt <0           
--           BEGIN	
--              SET @AdjProvAmtCr = 0;
--              SET @AdjProvAmtDr = Abs(@AdjProvAmt);
--           END
     
   
-------- Insert Record to Workfile ---------------

    SET @TrnDesc = ('Renewal Interest' + ' ' + CAST(@accIntRate AS nvarchar(7)));

	        INSERT INTO WFCSRENEWMSplus
	        (CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDate,AccBalance,AccFixedAmt,AccFixedMthInt,AccPrincipal,AccIntRate,AccOpenDate,
	        AccRenwlDate,AccAnniDate,AccRenwlAmt,AccPeriodMonths,AccMatureDate,AccNoAnni,AccNoRenwl,CalAdjProvCr,CalAdjProvDr,CalInterest,
            CalLastIntCr,NewIntRate,NewNoRenwl,NewNoAnni,NewRenwlDate,NewMatureDate,NewRenwlAmt,NewFixedMthInt,NoDays,FDAmount,CuNumber,MemName,
            FuncOpt,PayType,TrnDesc,TrnType,TrnDrCr,TrnContraDrCr,ShowInterest,TrnGLAccNoDr,TrnGLAccNoCr,UserId,ProcStat,CalFromDate)
	
            VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnDate,@accBalance,@accFixedAmt,@accFixedMthInt,@accPrincipal,@accIntRate,@accOpenDate,
	        @accRenwlDate,@accAnniDate,@accRenwlAmt,@accPeriodMonths,@accMatureDate,@accNoAnni,@accNoRenwl,@AdjProvAmtCr,@AdjProvAmtDr,@calInterest,
            @calLastIntCr,@newIntRate,@newNoRenwl,@newNoAnni,@newRenwlDate,@newMatureDate,@newRenwlAmt,@newMthBenefit,@noDays,@fdAmount,@cuNumber,@memName,
            @FuncOpt,@PayType,@TrnDesc,@TrnType,@TrnDrCr,@TrnContraDrCr,@ShowInterest,@TrnGLAccNoDr,@TrnGLAccNoCr,@userID,1,@fDate);
    

-------- End of Insert Record to Workfile ---------------


	FETCH NEXT FROM accTable INTO
		@cuType,@cuNo,@memNo,@accType,@accNo,@accPeriod,@accBalance,@accFixedAmt,@accPrincipal,@accFixedMthInt,@accLastIntCr,@accIntRate,
        @accOpenDate,@accRenwlDate,@accAnniDate,@accRenwlAmt,@accPeriodMonths,@accMatureDate,@accNoAnni,@accNoRenwl,@accContractIntFlag;


	END

CLOSE accTable; 
DEALLOCATE accTable;


--COMMIT TRANSACTION
--		SET NOCOUNT OFF
--END TRY
--
--BEGIN CATCH
--		ROLLBACK TRANSACTION
--
--		DECLARE @ErrorSeverity INT
--		DECLARE @ErrorState INT
--		DECLARE @ErrorMessage NVARCHAR(4000);	  
--		SELECT 
--			@ErrorMessage = ERROR_MESSAGE(),
--			@ErrorSeverity = ERROR_SEVERITY(),
--			@ErrorState = ERROR_STATE();	  
--		RAISERROR 
--		(
--			@ErrorMessage, -- Message text.
--			@ErrorSeverity, -- Severity.
--			@ErrorState -- State.
--		);	
--END CATCH

END

----exec Sp_CSProcessRenewalFDR

GO
