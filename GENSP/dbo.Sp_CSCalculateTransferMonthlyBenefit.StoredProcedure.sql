USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateTransferMonthlyBenefit]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_CSCalculateTransferMonthlyBenefit](@userID INT, @accType int)  

--ALTER PROCEDURE [dbo].[Sp_CSCalculateTransferMonthlyBenefit]  

/*

EXECUTE Sp_CSCalculateTransferMonthlyBenefit 1,17

*/

AS
BEGIN

--DECLARE @userID int;


DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;

DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @accProvBalance money;
DECLARE @accAdjProvBalance money;
DECLARE @NetProvBalance money;

DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accBenefitDate smalldatetime;
DECLARE @accMatureDate smalldatetime;
DECLARE @accPeriod int;
DECLARE @noMonths int;
DECLARE @accAtyClass smallint;

DECLARE @accFixedAmt money;
DECLARE @accFixedMthInt money;

DECLARE @PrmCorrType int;

DECLARE @CorrAccType int;
DECLARE @CorrccNo Bigint;
DECLARE @CorrtrnCode int;

DECLARE @trnDate smalldatetime;


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


------------- READ A2ZTRNCTRL FILE -------------
SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @accType);

SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType=203);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType=203);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType=203);
SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType=203);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType=203);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType=203);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType=203);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType=203);

---------  END OF READ A2ZTRNCTRL FILE -------------------------------

---------- Refresh Workfile ----------
TRUNCATE TABLE WFCSMONTHLYBENEFITCREDIT;
---------- End of Refresh Workfile ----------

DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccProvBalance,AccAdjProvBalance,AccAtyClass,AccFixedMthInt
FROM A2ZACCOUNT WHERE AccType = @accType AND AccProvBalance > 0 AND AccStatus < 97 AND AccAutoTrfFlag = 1;


OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accProvBalance,@accAdjProvBalance,@accAtyClass,@accFixedMthInt;

WHILE @@FETCH_STATUS = 0 
	BEGIN

    SET @PrmCorrType = (SELECT AccCorrType FROM A2ZACCTYPE WHERE AccTypeCode = @accType);

    print @PrmCorrType;
    print @accType;

   
    SET @cuNumber = LTRIM(STR(@cuType) + '-' + LTRIM(STR(@cuNo)));

	SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    
    SET @CorrccNo = (SELECT AccNo FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType=@PrmCorrType AND AccStatus < 98);

    SET @CorrtrnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @PrmCorrType);

    SET @NetProvBalance = (@accProvBalance + @accAdjProvBalance);

    SET @noMonths = (@NetProvBalance / @accFixedMthInt);
    

-------- Insert Record to Workfile ---------------
    
    IF @CorrccNo <> 0
       BEGIN    

	     INSERT INTO WFCSMONTHLYBENEFITCREDIT
	        (CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDate,AccMthBenefitAmt,NoMonths,AccTotalBenefitAmt,AccAdjProvBalance,CuNumber,MemName,FuncOpt,PayType,TrnDesc,TrnType,TrnDrCr,ShowInterest,
             TrnGLAccNoDr,TrnGLAccNoCr,UserId,ProcStat,AccCorrAccType,AccCorrAccNo)
    	
         VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnDate,@accFixedMthInt,@noMonths,@NetProvBalance,@accAdjProvBalance,@cuNumber,@memName,@FuncOpt,@PayType,@TrnDesc,@TrnType,@TrnDrCr,@ShowInterest,
            @TrnGLAccNoDr,@CorrtrnCode,@userID,1,@PrmCorrType,@CorrccNo);

       END
       
-------- End of Insert Record to Workfile ---------------

	FETCH NEXT FROM accTable INTO
        @cuType,@cuNo,@memNo,@accType,@accNo,@accProvBalance,@accAdjProvBalance,@accAtyClass,@accFixedMthInt;
		

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
