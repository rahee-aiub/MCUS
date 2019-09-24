USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateMonthlyBenefit]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSCalculateMonthlyBenefit](@userID INT)  

--ALTER PROCEDURE [dbo].[Sp_CSCalculateMonthlyBenefit]  

/*

EXECUTE Sp_CSCalculateMonthlyBenefit 1

*/

AS
BEGIN

--DECLARE @userID int;


DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @accBalance money;

DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accBenefitDate smalldatetime;
DECLARE @accMatureDate smalldatetime;
DECLARE @accPeriod int;
DECLARE @accNoBenefit int;
DECLARE @accAtyClass smallint;

DECLARE @accFixedAmt money;
DECLARE @accFixedMthInt money;

DECLARE @trnDate smalldatetime;
DECLARE @currentDate smalldatetime;
DECLARE @lastDate smalldatetime;

DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @calBenefit money;
DECLARE @newNoBenefit int;
DECLARE @NewBenefitDate smalldatetime;
DECLARE @CalBenefitDate smalldatetime;
DECLARE @noMonths int;
DECLARE @QnoMonths int;
DECLARE @EnoMonths int;
DECLARE @RnoMonths int;
DECLARE @XnoMonths int;

DECLARE @LastBenefitDate smalldatetime;

DECLARE @tDD int;
DECLARE @tMM int;
DECLARE @tYY int;
DECLARE @fDD int;
DECLARE @fMM int;
DECLARE @fYY int;

DECLARE @SkipFlag int;
DECLARE @NoFlag int;

DECLARE @ProcDate VARCHAR(10);


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


--BEGIN TRY
--	BEGIN TRANSACTION
--		SET NOCOUNT ON


--set @userID = 1;


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

--EXECUTE A2ZCSMCUS..Sp_CSAccountLedgerBalance 17,@ProcDate;

SET @currentDate  = @trnDate;
SET @currentDate  = @trnDate;
SET @lastDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));



------------- READ A2ZTRNCTRL FILE -------------
SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 17);


SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=16);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=16);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=16);
SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=16);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=16);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=16);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=16);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=16);

---------  END OF READ A2ZTRNCTRL FILE -------------------------------

---------- Refresh Workfile ----------
TRUNCATE TABLE WFCSMONTHLYBENEFIT;
---------- End of Refresh Workfile ----------

DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccTodaysOpBalance,AccOpenDate,AccBenefitDate,AccRenwlDate,AccPeriod,AccMatureDate,AccNoBenefit,AccAtyClass,AccFixedAmt,AccFixedMthInt
FROM A2ZACCOUNT WHERE AccType = 17 AND AccTodaysOpBalance > 0 AND AccStatus < 97 AND @trnDate >= AccBenefitDate;


OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOpenDate,@accBenefitDate,@accRenwlDate,@accPeriod,@accMatureDate,@accNoBenefit,@accAtyClass,@accFixedAmt,@accFixedMthInt;

WHILE @@FETCH_STATUS = 0 
	BEGIN

    SET @cuNumber = LTRIM(STR(@cuType) + '-' + LTRIM(STR(@cuNo)));
--    SET @TrnDesc= (SELECT PayTypeDes FROM A2ZPAYTYPE WHERE AtyClass = @accAtyClass AND PayType=@PayType);
-------- Find Member Type ----------------------

	SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

--    EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;
--
--    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);
--


  
-------- End of Find Member Type ----------------------

--------  Interest Calculation -----------------
    SET @tDate = @trnDate;

    IF @accBenefitDate IS NULL
       IF @accRenwlDate IS NULL
          SET @fDate = @accOpenDate;
       ELSE  
          SET @fDate = @accRenwlDate;
    ELSE	      
	   SET @fDate = @accBenefitDate;
        
   
    SET @tDD = DAY(@tDate);
    SET @tMM = MONTH(@tDate);
    SET @tYY = YEAR(@tDate);

    SET @fDD = DAY(@fDate);
    SET @fMM = MONTH(@fDate);
    SET @fYY = YEAR(@fDate);
    
       
           
       SET @QnoMonths = (DATEDIFF(MONTH,@fDate,@trnDate)); 
       SET @noMonths = (@QnoMonths + 1);
       
             
      
       SET @EnoMonths = (DATEDIFF(MONTH,@fDate,@accMatureDate));
       SET @EnoMonths = (@EnoMonths + 1);

       SET @RnoMonths = (@EnoMonths - @noMonths);
       
       IF @RnoMonths < 0
          BEGIN
             SET @noMonths = 0;
          END

--       SET @XnoMonths = (@noMonths - 1);       

--       SET @CalBenefitDate = (DATEADD(MONTH,@XnoMonths,@accBenefitDate));
--
--       IF @CalBenefitDate > @tDate 
--          BEGIN
--              
--              SET @noMonths = (@noMonths - 1);
--              SET @CalBenefitDate = (DATEADD(MONTH,@noMonths,@accBenefitDate));
--          END

      

       SET @calBenefit = (@accFixedMthInt * @noMonths);
       SET @NewNoBenefit = (@accNoBenefit + @noMonths);
      
       IF @noMonths > 0
          BEGIN
              SET @newBenefitDate = (DATEADD(MONTH,@noMonths,@accBenefitDate))
              SET @CalBenefitDate = (DATEADD(MONTH,-1,@accBenefitDate));
          END
    

-------- Insert Record to Workfile ---------------
    
    IF @noMonths > 0
       BEGIN 
	     INSERT INTO WFCSMONTHLYBENEFIT
	        (CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDate,AccBalance,AccOpenDate,AccBenefitDate,AccRenwlDate,AccPeriod,AccMatureDate,AccNoBenefit,
             CalBenefit,NewNoBenefit,NewBenefitDate,AccFixedAmt,AccFixedMthInt,NoMonths,CuNumber,MemName,FuncOpt,PayType,TrnDesc,TrnType,TrnDrCr,ShowInterest,
             TrnGLAccNoDr,TrnGLAccNoCr,UserId,ProcStat,CalBenefitDate)
    	
         VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnDate,@accBalance,@accOpenDate,@accBenefitDate,@accRenwlDate,@accPeriod,@accMatureDate,@accNoBenefit,
            @calBenefit,@newNoBenefit,@newBenefitDate,@accFixedAmt,@accFixedMthInt,@noMonths,@cuNumber,@memName,@FuncOpt,@PayType,@TrnDesc,@TrnType,@TrnDrCr,@ShowInterest,
            @TrnGLAccNoDr,@TrnGLAccNoCr,@userID,1,@CalBenefitDate);
       END
-------- End of Insert Record to Workfile ---------------

	FETCH NEXT FROM accTable INTO
        @cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOpenDate,@accBenefitDate,@accRenwlDate,@accPeriod,@accMatureDate,@accNoBenefit,@accAtyClass,@accFixedAmt,@accFixedMthInt;
		

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

----exec Sp_CSCalculateAnniversaryFDR

GO
