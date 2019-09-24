USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateDailyProvisionCPS]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_CSCalculateDailyProvisionCPS](@userID INT) 
AS
BEGIN

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accPrincipal money;
DECLARE @accIntRate smallmoney;
DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accAnniDate smalldatetime;
DECLARE @accAnniAmt money;
DECLARE @accRenwlAmt money;
DECLARE @accPeriodMonths int;
DECLARE @accMatureDate smalldatetime;
DECLARE @accNoAnni smallint;
DECLARE @accNoRenwl int;
DECLARE @accIntWdrawn money;
DECLARE @accAtyClass smallint;

DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;
DECLARE @uptoMthProvision money;
DECLARE @uptoLastMthProvision money;
DECLARE @currMthProvision money;
DECLARE @fdAmount money;
DECLARE @noDays int;

DECLARE @RoundFlag tinyint;

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

DECLARE @currentDate smalldatetime;
DECLARE @firstDay int;
DECLARE @lastDay int;

DECLARE @firstDate smalldatetime;
DECLARE @lastDate smalldatetime;

DECLARE @actualDep money;
DECLARE @accMonthlyDeposit money;
DECLARE @accTotalDep money;
DECLARE @actTotalDep money;

declare @accMatureAmt money;
DECLARE @CalDep money;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON



SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);


SET @currentDate  = @trnDate;
SET @firstDay = DAY(DATEADD(dd, -DAY(@currentDate) + 1, @currentDate));
SET @lastDay = DAY(DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));

SET @firstDate = (DATEADD(dd, -DAY(@currentDate) + 1, @currentDate));
SET @lastDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));
----CurrentMonth = MONTH(@trnDate);

------------- READ A2ZTRNCTRL FILE -------------
SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 14);


SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);

SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);

---------  END OF READ A2ZTRNCTRL FILE -------------------------------


---------- Refresh Workfile ----------
TRUNCATE TABLE WFCSPROVISIONCPS;
---------- End of Refresh Workfile ----------

DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccBalance,AccMonthlyDeposit,AccTotalDep,
AccOpenDate,AccAnniDate,AccAnniAmt,AccPeriod,AccMatureDate,AccMatureAmt,AccProvBalance,AccAtyClass
FROM A2ZACCOUNT WHERE AccType = 14 AND AccBalance > 0 AND AccStatus < 97;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accMonthlyDeposit,@accTotalDep,
@accOpenDate,@accAnniDate,@accAnniAmt,@accPeriodMonths,@accMatureDate,@accMatureAmt,@uptoLastMthProvision,@accAtyClass;

WHILE @@FETCH_STATUS = 0 
	BEGIN

    SET @cuNumber = LTRIM(STR(@cuType) + '-' + LTRIM(STR(@cuNo)));

--    SET @TrnDesc= (SELECT PayTypeDes FROM A2ZPAYTYPE WHERE AtyClass = @accAtyClass AND PayType = @PayType);
-------- Find Member Type ----------------------

	SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    SET @RoundFlag = (SELECT PrmRoundFlag FROM A2ZCSPARAM WHERE AccType = @accType);

--    SET @tDate = @lastDate;

    SET @tDate = @trnDate;

-------- End of Find Member Type ----------------------

   IF @accAnniDate IS NULL
		BEGIN
			SET @fDate = @accOpenDate
        END
    ELSE
        BEGIN
			SET @fDate = @accAnniDate
		END





----     IF @accPeriodMonths > 12
----		BEGIN
----			SET @fdAmount = @accPrincipal
----			SET @fDate = (DATEADD(year,@accNoAnni,@fDate))
----		END
 
---       SET @fDate = (@fDate + (10000 * @accNoAnni));

----     PRINT @fDate;

--     IF @tDate > @accMatureDate
--		BEGIN     
--			SET @tDate = @accMatureDate
--		END

     SET @noDays = ((DATEDIFF(d, @fDate, @tDate)) + 1);

 ----PRINT @fDate;
       

---------- end no of days -------------------

--------  Interest Calculation -----------------
    
    IF @RoundFlag = 1
       BEGIN    
       SET @uptoMthProvision = Round((((@fdAmount * @accIntRate * @noDays) / 36500)), 0);
       END
    
    IF @RoundFlag = 2
       BEGIN
       SET @uptoMthProvision = floor(((@fdAmount * @accIntRate * @noDays) / 36500));
       END

    IF @RoundFlag = 3
       BEGIN
       SET @uptoMthProvision = ((@fdAmount * @accIntRate * @noDays) / 36500);
       END

     
    SET @currMthProvision = (@uptoMthProvision - @uptoLastMthProvision);    


-- SET @currMthProvision = Round(((((@ProdTotal / @lastDay) * @prmIntRate ) / 1200) * 1), 0);


---------- Insert Record to Workfile ---------------

	INSERT INTO WFCSPROVISIONCPS
	(CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDate,AccBalance,AccOrgAmt,AccPrincipal,AccFDAmount,AccIntRate,AccOpenDate,
    AccRenwlDate,AccAnniDate,AccRenwlAmt,AccPeriodMonths,AccMatureDate,AccNoAnni,AccNoRenwl,AccIntWdrawn,
    UptoMthProvision,UptoLastMthProvision,CurrMthProvision,NoDays,CuNumber,MemName,FuncOpt,PayType,TrnDesc,TrnType,
    TrnDrCr,ShowInterest,TrnGLAccNoDr,TrnGLAccNoCr,UserId,ProcStat,CalFromDate)
	
    VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnDate,@accBalance,@accOrgAmt,@accPrincipal,@fdAmount,@accIntRate,@accOpenDate,
    @accRenwlDate,@accAnniDate,@accRenwlAmt,@accPeriodMonths,@accMatureDate,@accNoAnni,@accNoRenwl,@accIntWdrawn,
    @uptoMthProvision,@uptoLastMthProvision,@currMthProvision,@noDays,@cuNumber,@memName,@FuncOpt,@PayType,@TrnDesc,@TrnType,
    @TrnDrCr,@ShowInterest,@TrnGLAccNoDr,@TrnGLAccNoCr,@userID,1,@fDate);

---------- End of Insert Record to Workfile ---------------


	FETCH NEXT FROM accTable INTO
        @cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accMonthlyDeposit,@accTotalDep,
        @accOpenDate,@accAnniDate,@accAnniAmt,@accPeriodMonths,@accMatureDate,@accMatureAmt,@uptoLastMthProvision,@accAtyClass;        


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

----exec Sp_CSCalculateProvision6YR





GO
