USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSUpdateInterestAmountYearly]    Script Date: 06/28/2018 11:04:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









ALTER PROCEDURE [dbo].[Sp_CSUpdateInterestAmountYearly](@accType INT,@VoucherNo nvarchar(20),@VchNo nvarchar(20), @FromCashCode int,@DescTitle nvarchar(30), @UserId INT)  

AS
--EXECUTE Sp_CSUpdateInterestAmount 1,ProvisionCPS,10101001

--UPDATE WFCSPROVISIONCPS SET CurrMthProvision=0 WHERE CurrMthProvision IS NULL

BEGIN


DECLARE @trnDate smalldatetime;

DECLARE @trnCode int;
DECLARE @FuncOpt smallint;
DECLARE @PayType smallint;
DECLARE @TrnDesc nvarchar(50);
DECLARE @TrnDescription nvarchar(50);

DECLARE @TrnType tinyint;
DECLARE @TrnDrCr tinyint;
DECLARE @ShowInterest tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;

DECLARE @currMonth int;
DECLARE @CurrYear int;

DECLARE @strSQL NVARCHAR(MAX);


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @currMonth = (SELECT CurrentMonth FROM A2ZCSPARAMETER);

SET @CurrYear = YEAR(@trnDate);


SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @accType);
SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=18);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=18);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=18);
SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=18);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=18);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=18);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=18);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=18);


SET @TrnDescription = @TrnDesc + '' + @DescTitle;


IF @currMonth = 6
   BEGIN

        INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
        TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

        SELECT @trnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@trnCode,@FuncOpt,@PayType,@TrnType,1,0,
        AmtInterest,@TrnDescription,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,AmtInterest,0,AmtInterest,0,@FromCashCode,1,1,@UserId,1
        FROM WFCSINTEREST WHERE AccType = @accType;
------------- END OF NOPRMAL TRANSACTION ------------

------------- CONTRA TRANSACTION ------------
        INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,
        TrnDebit,TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

        SELECT @trnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@trnCode,@FuncOpt,@PayType,@TrnType,0,AmtInterest,
        0,@TrnDescription,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,AmtInterest,AmtInterest,0,1,@FromCashCode,1,1,@UserId,1
        FROM WFCSINTEREST WHERE AccType = @accType;
------------- END OF CONTRA TRANSACTION ------------

   END



IF @currMonth <> 6
   BEGIN

        --------------- NOPRMAL TRANSACTION ------------

SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(@CurrYear AS VARCHAR(4)) + '..A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,' +
              'TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)' +
              ' SELECT ' +
			  @trnDate + ',' + @VchNo + ',' + @VoucherNo + ',CuType,CuNo,MemNo,AccType,AccNo,' + CAST(@trnCode AS VARCHAR(3)) + ',' + CAST(@FuncOpt AS NVARCHAR(3)) + ',' + CAST(@PayType AS NVARCHAR(3)) + ',' + CAST(@TrnType AS NVARCHAR(3)) + ',1,0,' +
              'AmtInterest,' + @TrnDescription + ',' + CAST(@ShowInterest AS NVARCHAR(3)) + ',0,' + CAST(@TrnGLAccNoDr AS NVARCHAR(8)) + ',' + CAST(@TrnGLAccNoCr AS NVARCHAR(8)) + ',' + CAST(@TrnGLAccNoCr AS NVARCHAR(8)) + ',AmtInterest,0,AmtInterest,0,' + CAST(@FromCashCode AS NVARCHAR(8)) + ',1,1,' + CAST(@UserId AS NVARCHAR(6)) + ',1' +
              ' FROM WFCSINTEREST WHERE AccType = ' + CAST(@accType AS VARCHAR(2));

EXECUTE (@strSQL);


--------------- CONTRA TRANSACTION ------------

SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(@CurrYear AS VARCHAR(4)) + '..A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,' +
              'TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)' +
              ' SELECT ' +
			  @trnDate + ',' + @VchNo + ',' + @VoucherNo + ',CuType,CuNo,MemNo,AccType,AccNo,' + CAST(@trnCode AS VARCHAR(3)) + ',' + CAST(@FuncOpt AS NVARCHAR(3)) + ',' + CAST(@PayType AS NVARCHAR(3)) + ',' + CAST(@TrnType AS NVARCHAR(3)) + ',0,AmtInterest,' +
              '0,' + @TrnDescription + ',' + CAST(@ShowInterest AS NVARCHAR(3)) + ',0,' + CAST(@TrnGLAccNoDr AS NVARCHAR(8)) + ',' + CAST(@TrnGLAccNoCr AS NVARCHAR(8)) + ',' + CAST(@TrnGLAccNoDr AS NVARCHAR(8)) + ',AmtInterest,AmtInterest,0,1,' + CAST(@FromCashCode AS NVARCHAR(8)) + ',1,1,' + CAST(@UserId AS NVARCHAR(6)) + ',1' +
              ' FROM WFCSINTEREST WHERE AccType = ' + CAST(@accType AS VARCHAR(2));

EXECUTE (@strSQL);

   END





------------------- UPDATE A2ZACCOUNT FILE --------------------
--UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevProvBalance = A2ZACCOUNT.AccProvBalance,
--A2ZACCOUNT.AccPrevProvCalDate = A2ZACCOUNT.AccProvCalDate,
--A2ZACCOUNT.AccProvBalance = A2ZACCOUNT.AccProvBalance + WFCSPROVISIONCPS.CurrMthProvision,
--A2ZACCOUNT.AccProvCalDate = WFCSPROVISIONCPS.TrnDate
--FROM A2ZACCOUNT,WFCSPROVISIONCPS
--WHERE A2ZACCOUNT.AccType = WFCSPROVISIONCPS.AccType AND A2ZACCOUNT.AccNo = WFCSPROVISIONCPS.AccNo AND 
--A2ZACCOUNT.CuType = WFCSPROVISIONCPS.CuType AND A2ZACCOUNT.CuNo = WFCSPROVISIONCPS.CuNo AND 
--A2ZACCOUNT.MemNo = WFCSPROVISIONCPS.MemNo;
------------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSINTEREST SET WFCSINTEREST.ProcStat = 3 WHERE WFCSINTEREST.AccType = @accType;
--UPDATE WFCSINTEREST SET WFCSINTEREST.VoucherNo = @VoucherNo;

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

