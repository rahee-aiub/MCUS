USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSUpdateProvisionFDR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSUpdateProvisionFDR](@VoucherNo nvarchar(20),@VchNo nvarchar(20), @FromCashCode int)  

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
FROM WFCSPROVISIONFDR WHERE CurrMthProvision <> 0;
--------------- END OF NOPRMAL TRANSACTION ------------

--------------- CONTRA TRANSACTION ------------
INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,
TrnDebit,TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,0,CurrMthProvision,
0,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoDr,CurrMthProvision,CurrMthProvision,0,1,@FromCashCode,1,1,UserID,1
FROM WFCSPROVISIONFDR WHERE CurrMthProvision <> 0;
--------------- END OF CONTRA TRANSACTION ------------

----------------- UPDATE A2ZACCOUNT FILE --------------------
UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevProvBalance = A2ZACCOUNT.AccProvBalance,
A2ZACCOUNT.AccPrevProvCalDate = A2ZACCOUNT.AccProvCalDate,
A2ZACCOUNT.AccProvBalance = A2ZACCOUNT.AccProvBalance + WFCSPROVISIONFDR.CurrMthProvision,
A2ZACCOUNT.AccProvCalDate = WFCSPROVISIONFDR.TrnDate
FROM A2ZACCOUNT,WFCSPROVISIONFDR
WHERE A2ZACCOUNT.AccType = WFCSPROVISIONFDR.AccType AND A2ZACCOUNT.AccNo = WFCSPROVISIONFDR.AccNo AND 
A2ZACCOUNT.CuType = WFCSPROVISIONFDR.CuType AND A2ZACCOUNT.CuNo = WFCSPROVISIONFDR.CuNo AND 
A2ZACCOUNT.MemNo = WFCSPROVISIONFDR.MemNo;
----------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSPROVISIONFDR SET WFCSPROVISIONFDR.ProcStat = 3;
UPDATE WFCSPROVISIONFDR SET WFCSPROVISIONFDR.VoucherNo = @VoucherNo;

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
