
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSUpdateProvision6YR](@VoucherNo nvarchar(20),@VchNo nvarchar(20), @FromCashCode int)  
AS
BEGIN


--BEGIN TRY
--	BEGIN TRANSACTION
--		SET NOCOUNT ON

--------------- NOPRMAL TRANSACTION ------------
INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,1,0,
CurrMthProvision,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoCr,CurrMthProvision,0,CurrMthProvision,0,@FromCashCode,1,1,UserID,1
FROM WFCSPROVISION6YR WHERE CurrMthProvision <> 0;
--------------- END OF NOPRMAL TRANSACTION ------------

--------------- CONTRA TRANSACTION ------------
INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,
TrnDebit,TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,0,CurrMthProvision,
0,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoDr,CurrMthProvision,CurrMthProvision,0,1,@FromCashCode,1,1,UserID,1
FROM WFCSPROVISION6YR WHERE CurrMthProvision <> 0;
--------------- END OF CONTRA TRANSACTION ------------

----------------- UPDATE A2ZACCOUNT FILE --------------------
UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevProvBalance = A2ZACCOUNT.AccProvBalance,
A2ZACCOUNT.AccPrevProvCalDate = A2ZACCOUNT.AccProvCalDate,
A2ZACCOUNT.AccProvBalance = A2ZACCOUNT.AccProvBalance + WFCSPROVISION6YR.CurrMthProvision,
A2ZACCOUNT.AccProvCalDate = WFCSPROVISION6YR.TrnDate
FROM A2ZACCOUNT,WFCSPROVISION6YR
WHERE A2ZACCOUNT.AccType = WFCSPROVISION6YR.AccType AND A2ZACCOUNT.AccNo = WFCSPROVISION6YR.AccNo AND 
A2ZACCOUNT.CuType = WFCSPROVISION6YR.CuType AND A2ZACCOUNT.CuNo = WFCSPROVISION6YR.CuNo AND 
A2ZACCOUNT.MemNo = WFCSPROVISION6YR.MemNo;
----------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSPROVISION6YR SET WFCSPROVISION6YR.ProcStat = 3;
UPDATE WFCSPROVISION6YR SET WFCSPROVISION6YR.VoucherNo = @VoucherNo;

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
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

