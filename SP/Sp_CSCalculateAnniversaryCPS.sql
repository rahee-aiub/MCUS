
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSCalculateAnniversaryCPS](@userID INT)  




AS
--EXECUTE Sp_CSCalculateAnniversaryCPS 1

BEGIN

--DECLARE @userID INT;
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
DECLARE @AnniAmt money;
DECLARE @accPeriodMonths int;
DECLARE @accMatureDate smalldatetime;
DECLARE @accNoAnni int;
DECLARE @accNoRenwl int;
DECLARE @accAtyClass smallint;


DECLARE @actualDep money;
DECLARE @accMonthlyDeposit money;
DECLARE @accTotalDep money;
DECLARE @actTotalDep money;


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

DECLARE @paidMonths smallint;
DECLARE @noMonths INT;
DECLARE @RnoMonths INT;
DECLARE @DnoMonths INT;
DECLARE @CalMonths INT;
DECLARE @noAnni INT;
DECLARE @ProdTotal money;
DECLARE @ProdTotalAnni money;
DECLARE @currMthProvision money;
DECLARE @ActualProvision money;
DECLARE @prmIntRate smallmoney;

declare @accMatureAmt money;
DECLARE @CalDep money;

DECLARE @fMM int;
DECLARE @tMM int;
DECLARE @fDD int;
DECLARE @tDD int;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

EXECUTE A2ZCSMCUS..Sp_CSAccountLedgerBalance 14,@ProcDate;

------------- READ A2ZTRNCTRL FILE -------------

SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 14);


SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=14);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=14);

SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=14);
SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=14);

SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=14);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=14);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=14);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=14);

---------  END OF READ A2ZTRNCTRL FILE -------------------------------

---------- Refresh Workfile ----------
TRUNCATE TABLE WFCSANNIVERSARYCPS;
---------- End of Refresh Workfile ----------

DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccOpBal,AccMonthlyDeposit,AccTotalDep,AccProvBalance,
AccOpenDate,AccAnniDate,AccPeriod,AccMatureDate,AccMatureAmt,AccNoAnni
FROM A2ZACCOUNT WHERE AccType = 14 AND AccBalance > 0 AND AccStatus < 97 AND AccPeriod > 12;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accMonthlyDeposit,@accTotalDep,@accProvBalance,
@accOpenDate,@accAnniDate,@accPeriodMonths,@accMatureDate,@accMatureAmt,@accNoAnni;

WHILE @@FETCH_STATUS = 0 
	BEGIN

    SET @cuNumber = LTRIM(STR(@cuType) + '-' + LTRIM(STR(@cuNo)));
--    SET @TrnDesc= (SELECT PayTypeDes FROM A2ZPAYTYPE WHERE AtyClass = @accAtyClass AND PayType=@PayType);
    
    

-------- Find Member Type ----------------------

	SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    SET @RoundFlag = (SELECT PrmRoundFlag FROM A2ZCSPARAM WHERE AccType = @accType);


--    EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;
--
--    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);



-------- End of Find Member Type ----------------------

--    SET @accProvBalance = (SELECT AccProvBalance FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo);

--------  Interest Calculation -----------------
    SET @tDate = @trnDate;

    
    IF @accAnniDate IS NULL
		BEGIN
			SET @fDate = @accOpenDate
        END
    ELSE
        BEGIN
			SET @fDate = @accAnniDate
		END
 
    
    SET @ProdTotal = 0;
    SET @currMthProvision = 0;
	SET @newAnniDate = (DATEADD(year,1,@fDate))
    
IF @tDate >= @newAnniDate AND @accAnniDate <> @accMatureDate
       BEGIN
          SET @tDate = @newAnniDate;

    IF @accPeriodMonths = 60
       BEGIN                 
            SET @prmIntRate = 12.00;
       END
    
    IF @accPeriodMonths = 120
       BEGIN
            SET @prmIntRate = 12.00;
       END
     
    SET @actualDep = (@accPeriodMonths * @accMonthlyDeposit);    
    
    IF @accTotalDep = 0 OR @accMonthlyDeposit = 0
       BEGIN
           SET @paidMonths = 0;
       END
    ELSE
       BEGIN
           SET @paidMonths = (@accTotalDep / @accMonthlyDeposit); 
       END
   
    SET @noMonths = ((DATEDIFF(m, @fDate, @tDate)) + 0); 
	SET @noDays = ((DATEDIFF(d, @fDate, @tDate)) + 1);
    
    IF  @noMonths > @accPeriodMonths
        BEGIN
            SET @noMonths = @accPeriodMonths;
        END

    SET @RnoMonths = (@noMonths - @paidMonths); 
    SET @fMM = MONTH(@fDate);
    SET @fDD = DAY(@fDate);
    SET @tMM = MONTH(@tDate);
    SET @tDD = DAY(@tDate);

    
    IF @fMM = @tMM AND @tDD < @fDD
       BEGIN
           SET @noMonths = (@noMonths - 1);
       END
	     
    SET @noAnni = (@noMonths / 12);
    SET @CalMonths = (@noAnni * 12);
    SET @CalDep = (@CalMonths * @accMonthlyDeposit);  
      

    IF @tDate >= @accMatureDate AND @RnoMonths < 4
       BEGIN
           SET @currMthProvision = (@accMatureAmt - @actualDep); 
           SET @calInterest = (@accBalance - @accTotalDep); 
           SET @ActualProvision = (@currMthProvision - @calInterest);
           SET @newAnniDate = @accMatureDate;
           SET @newAnniAmt = 0;
       END
    ELSE
    IF @tDate < @accMatureDate AND @RnoMonths < 4
       BEGIN
           SET @ProdTotalAnni=(@noMonths * @AnniAmt);  
		   SET @ProdTotal = ((@CalMonths * (@CalMonths + 1) / 2) * @accMonthlyDeposit);  
		   SET @ProdTotal = (@ProdTotal + @ProdTotalAnni);
--		   SET @ProdTotal = (@ProdTotal/(@noMonths * 30) * @noDays);  

           SET @currMthProvision = Round((((@ProdTotal * @prmIntRate ) / 1200)), 0);
           SET @calInterest = (@accBalance - @accTotalDep); 
           SET @ActualProvision = (@currMthProvision - @calInterest);
           SET @newAnniAmt = (@currMthProvision + @CalDep);
           IF @noAnni > 0
              BEGIN
                  SET @newAnniDate = (DATEADD(year,@noAnni,@fDate));
              END
          
       END   
    ELSE
    IF @RnoMonths > 3
       BEGIN
           SET @ActualProvision = @accProvBalance;
           SET @newAnniDate = (DATEADD(year,@noAnni,@fDate));
           SET @newAnniAmt = 0;
       END   
	IF @accProvBalance < 0
	   BEGIN
		   SET @ActualProvision = 0;
	   END 
  
--        SET @AdjProvAmt = Round(((@accProvBalance - @currMthProvision)), 0);
--        
--        IF @AdjProvAmt = 0 OR @AdjProvAmt > 0
--           BEGIN	
--              SET @AdjProvAmtCr = 0;
--              SET @AdjProvAmtDr = 0;
--           END
--        
--        IF @AdjProvAmt <0           
--           BEGIN	
--              SET @AdjProvAmtCr = 0;
--              SET @AdjProvAmtDr = Abs(@AdjProvAmt);
--           END

        SET @AdjProvAmtCr = 0;
        SET @AdjProvAmtDr = 0;

        IF @tDate >= @accMatureDate AND @RnoMonths < 4
           BEGIN
              SET @AdjProvAmt = Round(((@ActualProvision - @accProvBalance)), 0);
              
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
        
             IF @AdjProvAmt < 0           
                BEGIN	
                    SET @AdjProvAmtCr = 0;
                    SET @AdjProvAmtDr = Abs(@AdjProvAmt);
                END

        END


      
        
-----   END OF NEW NO OF ANNIVERSARY -----------------

-------- Insert Record to Workfile ---------------

	INSERT INTO WFCSANNIVERSARYCPS
	(CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDate,AccBalance,AccOpenDate,AccMonthlyDeposit,AccTotalDep,
	AccAnniDate,AccPeriodMonths,AccMatureDate,AccNoAnni,CalAdjProvCr,CalAdjProvDr,CalInterest,CalCurrentAmt,NewNoAnni,NewAnniDate,NewAnniAmt,CuNumber,MemName,
    FuncOpt,PayType,TrnDesc,TrnType,TrnDrCr,ShowInterest,TrnGLAccNoDr,TrnGLAccNoCr,UserId,ProcStat,CalFromDate)
	
    VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnDate,@accBalance,@accOpenDate,@accMonthlyDeposit,@accTotalDep,
	@accAnniDate,@accPeriodMonths,@accMatureDate,@accNoAnni,@AdjProvAmtCr,@AdjProvAmtDr,@currMthProvision,@ActualProvision,@newNoAnni,@newAnniDate,@newAnniAmt,@cuNumber,@memName,
    @FuncOpt,@PayType,@TrnDesc,@TrnType,@TrnDrCr,@ShowInterest,@TrnGLAccNoDr,@TrnGLAccNoCr,@UserID,1,@fDate);

-------- End of Insert Record to Workfile ---------------
END

	FETCH NEXT FROM accTable INTO
        @cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accMonthlyDeposit,@accTotalDep,@accProvBalance,
        @accOpenDate,@accAnniDate,@accPeriodMonths,@accMatureDate,@accMatureAmt,@accNoAnni;
		
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

----exec Sp_CSCalculateAnniversary6YR
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

