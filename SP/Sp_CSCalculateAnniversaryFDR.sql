
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSCalculateAnniversaryFDR] (@userID INT)   

--ALTER PROCEDURE [dbo].[Sp_CSCalculateAnniversaryFDR]  
AS
BEGIN

--DECLARE @userID int;

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @accProvBalance money;

DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accPrincipal money;
DECLARE @accLastIntCr money;
DECLARE @accRenwlAmt money;
DECLARE @accIntRate smallmoney;
DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accAnniDate smalldatetime;
DECLARE @accPeriodMonths int;
DECLARE @accMatureDate smalldatetime;
DECLARE @accNoAnni int;
DECLARE @accNoRenwl int;
DECLARE @accAtyClass smallint;

DECLARE @trnDate smalldatetime;
DECLARE @ProcDate VARCHAR(10);

DECLARE @RoundFlag tinyint;

DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @calInterest money;
DECLARE @AdjChrg money;
DECLARE @NetInt money;

DECLARE @calInterestDue money;
DECLARE @newIntRate smallmoney;
DECLARE @newNoAnni int;
DECLARE @newAnniDate smalldatetime;
DECLARE @newLastIntCr money;
DECLARE @newAnniAmt money;

DECLARE @AdjProvAmt money;
DECLARE @AdjProvAmtCr money;
DECLARE @AdjProvAmtDr money;

DECLARE @fdAmount money;
DECLARE @noDays int;

DECLARE @memType int;
DECLARE @cuNumber nvarchar(10);
DECLARE @memName nvarchar(50);

DECLARE @FuncOpt smallint;
DECLARE @PayType smallint;
DECLARE @TrnDesc nvarchar(50);
DECLARE @TrnType tinyint;
DECLARE @TrnDrCr tinyint;
DECLARE @ShowInterest tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;



BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

--SET @userID = 1;

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);
SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

EXECUTE A2ZCSMCUS..Sp_CSAccountLedgerBalance 15,@ProcDate;

------------- READ A2ZTRNCTRL FILE -------------
SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 15);


SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=14);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=14);

SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=14);

SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=14);

SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=14);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=14);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=14);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=14);

---------  END OF READ A2ZTRNCTRL FILE -------------------------------

---------- Refresh Workfile ----------
TRUNCATE TABLE WFCSANNIVERSARYFDR;
---------- End of Refresh Workfile ----------

DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccOpBal,AccOrgAmt,AccPrincipal,AccLastIntCr,AccIntRate,
AccOpenDate,AccRenwlDate,AccAnniDate,AccRenwlAmt,AccPeriod,AccMatureDate,AccNoAnni,AccNoRenwl,AccAtyClass
FROM A2ZACCOUNT WHERE AccType = 15 AND AccBalance > 0 AND AccStatus < 97 AND @trnDate < AccMatureDate AND AccPeriod > 12;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOrgAmt,@accPrincipal,@accLastIntCr,@accIntRate,@accOpenDate,
@accRenwlDate,@accAnniDate,@accRenwlAmt,@accPeriodMonths,@accMatureDate,@accNoAnni,@accNoRenwl,@accAtyClass;

WHILE @@FETCH_STATUS = 0 
	BEGIN

    SET @cuNumber = LTRIM(STR(@cuType) + '-' +LTRIM(STR(@cuNo)));
--    SET @TrnDesc= (SELECT PayTypeDes FROM A2ZPAYTYPE WHERE AtyClass = @accAtyClass AND PayType=@PayType);
-------- Find Member Type ----------------------

	SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    
--    print @cuType;
--    print @cuNo;
--    print @memNo;
--    print @memtype;



    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
  
    


    SET @RoundFlag = (SELECT PrmRoundFlag FROM A2ZCSPARAM WHERE AccType = @accType);

-------- End of Find Member Type ----------------------

    SET @accProvBalance = (SELECT AccProvBalance FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo);
    
   
--    EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;
--
--    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);
--


--------  Interest Calculation -----------------
    SET @tDate = @trnDate;

    IF @accAnniDate IS NULL
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
	ELSE
    IF @accRenwlDate IS NULL
		BEGIN
            SET @fdAmount =  @accOrgAmt
			SET @fDate = @accAnniDate
        END
    ELSE
        BEGIN
            SET @fdAmount =  @accRenwlAmt
			SET @fDate = @accAnniDate
		END
    

    SET @newAnniDate = (DATEADD(year,1,@fDate))
    
 ----   SELECT @aDate,@tDate;

    IF @tDate >= @newAnniDate
       BEGIN
            SET @tDate = @newAnniDate;
            SET @noDays = ((DATEDIFF(d, @fDate, @tDate)) + 0);

--------  Interest Calculation -----------------
    IF @RoundFlag = 1
       BEGIN    
--       SET @calInterest = Round((((@fdAmount * @accIntRate * @noDays) / 36500)), 0);
       SET @calInterest = Round((((@fdAmount * @accIntRate) / 100)), 0);
       END
    
    IF @RoundFlag = 2
       BEGIN
--       SET @calInterest = floor(((@fdAmount * @accIntRate * @noDays) / 36500));
       SET @calInterest = floor(((@fdAmount * @accIntRate) / 100));
       END

    IF @RoundFlag = 3
       BEGIN
--       SET @calInterest = ((@fdAmount * @accIntRate * @noDays) / 36500);
       SET @calInterest = ((@fdAmount * @accIntRate) / 100);
       END    
         
    IF @calInterest < 0  
       BEGIN
           SET @calInterest = 0;
       END     
    


--------  End of Interest Calculation -----------

-----   NEW NO OF ANNIVERSARY -----------------

        SET @newNoAnni = Round(( @accNoAnni + 1), 0);

        SET @newLastIntCr = Round(((@accLastIntCr + @calInterest)), 0);

        SET @AdjChrg = Round(((@AccPrincipal - @AccBalance)), 0);
        SET @NetInt = Round(((@calInterest - @AdjChrg)), 0);
        SET @newAnniAmt = Round(((@AccPrincipal + @NetInt)), 0);

------------- Provision Adjustment process

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
        

        
-----   END OF NEW NO OF ANNIVERSARY -----------------








-------- Insert Record to Workfile ---------------

	INSERT INTO WFCSANNIVERSARYFDR
	(CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDate,AccBalance,AccOrgAmt,AccPrincipal,AccIntRate,AccOpenDate,
	AccRenwlDate,AccAnniDate,AccRenwlAmt,AccPeriodMonths,AccMatureDate,AccNoAnni,AccNoRenwl,CalAdjProvCr,CalAdjProvDr,CalInterest,NewNoAnni,NewAnniDate,NewLastIntCr,NewAnniAmt,FDAmount,NoDays,CuNumber,MemName,
    FuncOpt,PayType,TrnDesc,TrnType,TrnDrCr,ShowInterest,TrnGLAccNoDr,TrnGLAccNoCr,UserId,ProcStat,CalFromDate)
	
    VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnDate,@accBalance,@accOrgAmt,@accPrincipal,@accIntRate,@accOpenDate,
	@accRenwlDate,@accAnniDate,@accRenwlAmt,@accPeriodMonths,@accMatureDate,@accNoAnni,@accNoRenwl,@AdjProvAmtCr,@AdjProvAmtDr,@calInterest,@newNoAnni,@newAnniDate,@newLastIntCr,@newAnniAmt,@fdAmount,@noDays,@cuNumber,@memName,
    @FuncOpt,@PayType,@TrnDesc,@TrnType,@TrnDrCr,@ShowInterest,@TrnGLAccNoDr,@TrnGLAccNoCr,@UserID,1,@fDate);

-------- End of Insert Record to Workfile ---------------

    END

	FETCH NEXT FROM accTable INTO
		@cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOrgAmt,@accPrincipal,@accLastIntCr,@accIntRate,@accOpenDate,
		@accRenwlDate,@accAnniDate,@accRenwlAmt,@accPeriodMonths,@accMatureDate,@accNoAnni,@accNoRenwl,@accAtyClass;


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

----exec Sp_CSCalculateAnniversaryFDR
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

