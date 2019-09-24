USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateUPTOProvisionCPS]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_CSCalculateUPTOProvisionCPS](@userID INT) 
AS
--EXECUTE Sp_CSCalculateUPTOProvisionCPS 1


BEGIN

DECLARE @CcuType int;
DECLARE @CcuNo int;

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accPrincipal money;
declare @accMatureAmt money;

DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accAnniDate smalldatetime;
DECLARE @accRenwlAmt money;
DECLARE @accPeriodMonths int;
DECLARE @accMatureDate smalldatetime;
DECLARE @accNoAnni smallint;
DECLARE @accNoRenwl int;
DECLARE @accIntWdrawn money;
DECLARE @accProvBalance money;
DECLARE @accAtyClass smallint;

DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;
DECLARE @uptoMthProvision money;
DECLARE @uptoLastMthProvision money;
DECLARE @currMthProduct money;
DECLARE @currMthProvision money;
DECLARE @fdAmount money;
DECLARE @noDays int;
DECLARE @noMonths INT;
DECLARE @RnoMonths INT;
DECLARE @DnoMonths INT;

DECLARE @paidMonths smallint;

DECLARE @calInterest money;

DECLARE @actualDep money;
DECLARE @accMonthlyDeposit money;
DECLARE @accTotalDep money;

declare @ProdTotal money;

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
DECLARE @accLastTrnDateU smalldatetime;
DECLARE @firstDay int;
DECLARE @lastDay int;

DECLARE @firstDate smalldatetime;
DECLARE @lastDate smalldatetime;
DECLARE @calTillDate smalldatetime;

DECLARE @prmIntRate smallmoney;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);


SET @trnDate = '2016-03-31';


SET @currentDate  = @trnDate;
SET @firstDay = DAY(DATEADD(dd, -DAY(@currentDate) + 1, @currentDate));
SET @lastDay = DAY(DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));

SET @firstDate = (DATEADD(dd, -DAY(@currentDate) + 1, @currentDate));
SET @lastDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));
----CurrentMonth = MONTH(@trnDate);

------------- READ A2ZTRNCTRL FILE -------------
SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 14);

SET @prmIntRate= (SELECT PrmIntRate FROM A2ZCSPARAM WHERE AccType = 14);

SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=13);

---------  END OF READ A2ZTRNCTRL FILE -------------------------------


UPDATE A2ZACCOUNT SET AccProvBalance = 0,AccProvCalDate=null where acctype=14;


---------- Refresh Workfile ----------
TRUNCATE TABLE WFCSPROVISIONCPS;
---------- End of Refresh Workfile ----------

DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccBalance,AccOpenDate,AccPeriod,AccMatureDate,AccMatureAmt,AccMonthlyDeposit,AccTotalDep,
AccProvBalance,AccAtyClass,AccLastTrnDateU
FROM A2ZACCOUNT WHERE AccType = 14 AND AccBalance > 0 AND AccStatus < 97;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOpenDate,@accPeriodMonths,@accMatureDate,@accMatureAmt,@accMonthlyDeposit,@accTotalDep,
@uptoLastMthProvision,@accAtyClass,@accLastTrnDateU;


WHILE @@FETCH_STATUS = 0 
	BEGIN

    
    SET @cuNumber = LTRIM(STR(@cuType) + '-' +LTRIM(STR(@cuNo)));

--    SET @TrnDesc= (SELECT PayTypeDes FROM A2ZPAYTYPE WHERE AtyClass = @accAtyClass AND PayType = @PayType);
-------- Find Member Type ----------------------

	SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

--    PRINT @memType;

    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    
--    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    
    SET @RoundFlag = (SELECT PrmRoundFlag FROM A2ZCSPARAM WHERE AccType = @accType);

    
    SET @fDate = @AccOpenDate

    SET @tDate = @trnDate;

--    SET @tDate = @accLastTrnDateU;


--    SET @RnoMonths = ((DATEDIFF(m, @tDate, @trnDate)) + 1); 

   -------------------------------------------------------------------
    
     IF @accTotalDep = 0 OR @accMonthlyDeposit = 0
       BEGIN
           SET @paidMonths = 0;
       END
    ELSE
       BEGIN
           SET @paidMonths = (@accTotalDep / @accMonthlyDeposit); 
       END

    SET @actualDep = (@accPeriodMonths * @accMonthlyDeposit);
           
    SET @noMonths = ((DATEDIFF(m, @fDate, @tDate)) + 1); 

    PRINT @fDate;
    PRINT @tDate;
    PRINT @noMonths;

    IF  @noMonths > @accPeriodMonths
        BEGIN
            SET @noMonths = @accPeriodMonths;
        END

    SET @RnoMonths = (@noMonths - @paidMonths); 

    IF @RnoMonths > 3
       BEGIN
            SET @noMonths = @paidMonths;
       END
    
     
    SET @ProdTotal = 0;
    SET @currMthProvision = 0;

    SET @ProdTotal = ((@noMonths * (@noMonths + 1) / 2) * @accMonthlyDeposit);  

    IF @accPeriodMonths = 60
       BEGIN
           SET @prmIntRate = 13.90;
       END
    
    IF @accPeriodMonths = 120
       BEGIN
           SET @prmIntRate = 17.23;
       END
 

    SET @calTillDate = (DATEADD(month,(@noMonths -1),@fDate))
    SET @calTillDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @calTillDate)), DATEADD(mm, 1, @calTillDate)));

    PRINT @noMonths;
    PRINT @fDate;
    PRINT @calTillDate;

    IF @tDate <= @accMatureDate AND @accTotalDep >= @actualDep
       BEGIN
           SET @currMthProvision = (@accMatureAmt - @accTotalDep);
           SET @calInterest = (@accBalance - @accTotalDep); 
           SET @currMthProvision = (@currMthProvision - @calInterest);
       END
    ELSE       
       BEGIN    
           SET @currMthProvision = Round((((@ProdTotal * @prmIntRate ) / 1200)), 0);
           SET @calInterest = (@accBalance - @accTotalDep); 
           SET @currMthProvision = (@currMthProvision - @calInterest);
       END
       



--  IF @RnoMonths > 3 AND @noMonths < 60
--     BEGIN  
--          SET @ProdTotal = 0;
--          SET @currMthProvision = 0;  
--     END 


	INSERT INTO WFCSPROVISIONCPS
	(CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDate,AccBalance,AccIntRate,AccOpenDate,AccMatureDate,
    AccPeriodMonths,CurrMthProduct,CurrMthProvision,CuNumber,MemName,FuncOpt,PayType,TrnDesc,TrnType,
    TrnDrCr,ShowInterest,TrnGLAccNoDr,TrnGLAccNoCr,UserId,ProcStat,CalFromDate,CalTillDate)
	
    VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnDate,@accBalance,@prmIntRate,@accOpenDate,@accMatureDate,
    @accPeriodMonths,@ProdTotal,@currMthProvision,@cuNumber,@memName,@FuncOpt,@PayType,@TrnDesc,@TrnType,
    @TrnDrCr,@ShowInterest,@TrnGLAccNoDr,@TrnGLAccNoCr,@userID,1,@fDate,@calTillDate);

-------- End of Insert Record to Workfile ---------------


	FETCH NEXT FROM accTable INTO
        @cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOpenDate,@accPeriodMonths,@accMatureDate,@accMatureAmt,@accMonthlyDeposit,@accTotalDep,
        @uptoLastMthProvision,@accAtyClass,@accLastTrnDateU;

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

----exec Sp_CSCalculateProvisionCPS



GO
