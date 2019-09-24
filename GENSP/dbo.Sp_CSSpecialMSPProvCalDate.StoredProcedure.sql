USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSSpecialMSPProvCalDate]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Sp_CSSpecialMSPProvCalDate]  

--ALTER PROCEDURE [dbo].[Sp_CSSpecialMSPProvCalDate]  

/*

EXECUTE Sp_CSSpecialMSPProvCalDate

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

DECLARE @LastBenefitDate smalldatetime;
DECLARE @accProvCalDate smalldatetime;

DECLARE @tDD int;
DECLARE @tMM int;
DECLARE @tYY int;
DECLARE @fDD int;
DECLARE @fMM int;
DECLARE @fYY int;

DECLARE @SkipFlag int;
DECLARE @NoFlag int;


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


DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccBalance,AccOpenDate,AccBenefitDate,AccRenwlDate,AccPeriod,AccMatureDate,AccNoBenefit,AccAtyClass,AccFixedAmt,AccFixedMthInt,AccProvCalDate
FROM A2ZACCOUNT WHERE AccType = 17 AND AccBalance > 0 AND AccStatus < 97;


OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOpenDate,@accBenefitDate,@accRenwlDate,@accPeriod,@accMatureDate,@accNoBenefit,@accAtyClass,@accFixedAmt,@accFixedMthInt,@accProvCalDate;

WHILE @@FETCH_STATUS = 0 
	BEGIN
       
       IF @accProvCalDate IS NOT NULL
          BEGIN
               SET @accProvCalDate = (DATEADD(MONTH,-1,@accProvCalDate));
               UPDATE A2ZACCOUNT SET AccProvCalDate = @accProvCalDate WHERE AccNo = @accNo;
          END


	FETCH NEXT FROM accTable INTO
        @cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOpenDate,@accBenefitDate,@accRenwlDate,@accPeriod,@accMatureDate,@accNoBenefit,@accAtyClass,@accFixedAmt,@accFixedMthInt,@accProvCalDate;
		

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
