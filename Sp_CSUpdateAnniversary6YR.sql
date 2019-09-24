
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSUpdateAnniversary6YR](@VoucherNo nvarchar(20),@VchNo nvarchar(20),@FromCashCode int)  
AS
BEGIN

DECLARE @trnCode int;
DECLARE @FuncOpt smallint;
DECLARE @PayType smallint;
DECLARE @TrnDesc nvarchar(50);
DECLARE @TrnType tinyint;
DECLARE @TrnDrCr tinyint;
DECLARE @ShowInterest tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;


-------- NORMAL ADJ PROVISION CR. ----------------

--BEGIN TRY
--	BEGIN TRANSACTION
--		SET NOCOUNT ON

SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 16);
SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=51);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=51);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=51);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=51);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=51);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=51);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=51);
SET @TrnDesc = 'Provision Adj.Cr.';

INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,1,0,
CalAdjProvCr,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,CalAdjProvCr,0,CalAdjProvCr,0,@FromCashCode,1,1,UserID
FROM WFCSANNIVERSARY6YR WHERE CalAdjProvCr <> 0;

--------CONTRA ------

INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,0,CalAdjProvCr,
0,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,CalAdjProvCr,CalAdjProvCr,0,1,@FromCashCode,1,1,UserID
FROM WFCSANNIVERSARY6YR WHERE CalAdjProvCr <> 0;

---------  END OF ADJ PROVISION CR. ------------


-------- NORMAL ADJ PROVISION DR. ----------------

SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 16);
SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=52);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=52);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=52);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=52);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=52);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=52);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 16 AND FuncOpt=52);
SET @TrnDesc = 'Provision Adj.Dr.';


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,0,CalAdjProvDr,
0,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,(0-CalAdjProvDr),CalAdjProvDr,0,0,@FromCashCode,1,1,UserID,1
FROM WFCSANNIVERSARY6YR WHERE CalAdjProvDr <> 0;

-----------CONTRA -----

INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,1,0,
CalAdjProvDr,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,CalAdjProvDr,0,CalAdjProvDr,1,@FromCashCode,1,1,UserID,1
FROM WFCSANNIVERSARY6YR WHERE CalAdjProvDr <> 0;

---------  END OF ADJ PROVISION DR. ------------


--------------- NORMAL TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,1,0,
CalInterest,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoCr,CalInterest,0,CalInterest,0,@FromCashCode,1,1,UserID,1
FROM WFCSANNIVERSARY6YR WHERE CalInterest <> 0;
--------------- END OF NORMAL TRANSACTION ------------

--------------- NORMAL CONTRA TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,0,CalInterest,
0,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoDr,(0-CalInterest),CalInterest,0,1,@FromCashCode,1,1,UserID,1
FROM WFCSANNIVERSARY6YR WHERE CalInterest <> 0;
--------------- END OF NORMAL CONTRA TRANSACTION ------------

----------------- UPDATE A2ZACCOUNT FILE --------------------
UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevAnniDate = A2ZACCOUNT.AccAnniDate,
                      A2ZACCOUNT.AccPrevAnniAmt = A2ZACCOUNT.AccAnniAmt,      
                      A2ZACCOUNT.AccPrevNoAnni = A2ZACCOUNT.AccNoAnni,                   
                      A2ZACCOUNT.AccPrevRaCurr = A2ZACCOUNT.AccPrincipal,   
                      A2ZACCOUNT.AccPrevRaLedger = A2ZACCOUNT.AccBalance,   
                      A2ZACCOUNT.AccPrevProvBalance = A2ZACCOUNT.AccProvBalance,       

                      A2ZACCOUNT.AccAnniDate = WFCSANNIVERSARY6YR.NewAnniDate,                 
                      A2ZACCOUNT.AccNoAnni  = WFCSANNIVERSARY6YR.NewNoAnni,                     
--                      A2ZACCOUNT.AccPrincipal = WFCSANNIVERSARYFDR.NewAnniAmt,
                      A2ZACCOUNT.AccBalance = WFCSANNIVERSARY6YR.NewAnniAmt,
                      A2ZACCOUNT.AccAnniAmt = WFCSANNIVERSARY6YR.NewAnniAmt,
                      A2ZACCOUNT.AccProvBalance = 0



FROM A2ZACCOUNT,WFCSANNIVERSARY6YR
WHERE A2ZACCOUNT.AccType = WFCSANNIVERSARY6YR.AccType AND A2ZACCOUNT.AccNo = WFCSANNIVERSARY6YR.AccNo AND 
A2ZACCOUNT.CuType = WFCSANNIVERSARY6YR.CuType AND A2ZACCOUNT.CuNo = WFCSANNIVERSARY6YR.CuNo AND 
A2ZACCOUNT.MemNo = WFCSANNIVERSARY6YR.MemNo;
----------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSANNIVERSARY6YR SET WFCSANNIVERSARY6YR.ProcStat = 3;
UPDATE WFCSANNIVERSARY6YR SET WFCSANNIVERSARY6YR.VoucherNo = @VoucherNo;

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

