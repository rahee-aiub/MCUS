
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSUpdateProvisionLOAN](@VoucherNo nvarchar(20),@VchNo nvarchar(20), @FromCashCode int,@AccountType int)  

AS
BEGIN


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


--------------- NOPRMAL TRANSACTION ------------
INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,0,CurrMthProvision,
0,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoCr,CurrMthProvision,CurrMthProvision,0,0,@FromCashCode,1,1,UserID,1
FROM WFCSPROVISIONLOAN WHERE CuType != 0 AND AccType = @AccountType;
--------------- END OF NOPRMAL TRANSACTION ------------

--------------- CONTRA TRANSACTION ------------
INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,
TrnDebit,TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,1,0,
CurrMthProvision,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoDr,CurrMthProvision,0,CurrMthProvision,1,@FromCashCode,1,1,UserID,1
FROM WFCSPROVISIONLOAN WHERE CuType != 0 AND AccType = @AccountType;
--------------- END OF CONTRA TRANSACTION ------------

----------------- UPDATE A2ZACCOUNT FILE --------------------
UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevProvBalance = A2ZACCOUNT.AccProvBalance,
A2ZACCOUNT.AccPrevProvCalDate = A2ZACCOUNT.AccProvCalDate,
A2ZACCOUNT.AccProvBalance = A2ZACCOUNT.AccProvBalance + WFCSPROVISIONLOAN.CurrMthProvision,
A2ZACCOUNT.AccProvCalDate = WFCSPROVISIONLOAN.TrnDate
FROM A2ZACCOUNT,WFCSPROVISIONLOAN
WHERE A2ZACCOUNT.AccType = WFCSPROVISIONLOAN.AccType AND A2ZACCOUNT.AccNo = WFCSPROVISIONLOAN.AccNo AND 
A2ZACCOUNT.CuType = WFCSPROVISIONLOAN.CuType AND A2ZACCOUNT.CuNo = WFCSPROVISIONLOAN.CuNo AND 
A2ZACCOUNT.MemNo = WFCSPROVISIONLOAN.MemNo;
----------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSPROVISIONLOAN SET WFCSPROVISIONLOAN.ProcStat = 3 WHERE CuType = 0 AND AccType = @AccountType;
UPDATE WFCSPROVISIONLOAN SET WFCSPROVISIONLOAN.VoucherNo = @VoucherNo WHERE CuType = 0 AND AccType = @AccountType;

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

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

