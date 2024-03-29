
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSUpdateRenewalMSplus](@VoucherNo nvarchar(20),@VchNo nvarchar(20), @FromCashCode int)  
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

--SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 17);
--SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=51);
--SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=51);
--SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=51);
--SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=51);
--SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=51);
--SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=51);
--SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 17 AND FuncOpt=51);
--SET @TrnDesc = 'Provision Adj.Cr.';

--INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
--
--SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,1,0,
--CalAdjProvCr,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,CalAdjProvCr,0,CalAdjProvCr,0,@FromCashCode,1,1,UserID,1
--FROM WFCSRENEWFDR WHERE CalAdjProvCr <> 0;
--
----------CONTRA ------
--
--INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
--
--SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,0,CalAdjProvCr,
--0,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,CalAdjProvCr,CalAdjProvCr,0,1,@FromCashCode,1,1,UserID,1
--FROM WFCSRENEWFDR WHERE CalAdjProvCr <> 0;

---------  END OF ADJ PROVISION CR. ------------


-------- NORMAL ADJ PROVISION DR. ----------------

--SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 15);
--SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=52);
--SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=52);
--SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=52);
--SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=52);
--SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=52);
--SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=52);
--SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 15 AND FuncOpt=52);
--SET @TrnDesc = 'Provision Adj.Dr.';
--
--
--INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
--
--SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,0,CalAdjProvDr,
--0,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,(0-CalAdjProvDr),CalAdjProvDr,0,0,@FromCashCode,1,1,UserID,1
--FROM WFCSRENEWFDR WHERE CalAdjProvDr <> 0;

-----------CONTRA -----
--
--INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
--
--SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,1,0,
--CalAdjProvDr,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,CalAdjProvDr,0,CalAdjProvDr,1,@FromCashCode,1,1,UserID,1
--FROM WFCSRENEWFDR WHERE CalAdjProvDr <> 0;

---------  END OF ADJ PROVISION DR. ------------

--------------- NORMAL TRANSACTION ------------

--
--INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
--
--SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,0,
--CalInterest,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoCr,CalInterest,0,CalInterest,0,@FromCashCode,1,1,UserID,1
--FROM WFCSRENEWFDR WHERE CalInterest <> 0;
----------------- END OF NOPRMAL TRANSACTION ------------

--------------- CONTRA TRANSACTION ------------

--
--INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
--
--SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnContraDrCr,CalInterest,
--0,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoDr,(0-CalInterest),CalInterest,0,1,@FromCashCode,1,1,UserID,1
--FROM WFCSRENEWFDR WHERE CalInterest <> 0;
--------------- END OF CONTRA TRANSACTION ------------

----------------- UPDATE A2ZACCOUNT FILE --------------------
UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevRenwlDate = A2ZACCOUNT.AccRenwlDate,
                      A2ZACCOUNT.AccPrevRenwlAmt = A2ZACCOUNT.AccRenwlAmt,
                      A2ZACCOUNT.AccPrevNoRenwl = A2ZACCOUNT.AccNoRenwl,            
                      A2ZACCOUNT.AccPrevPeriod = A2ZACCOUNT.AccPeriod,     
        
                      A2ZACCOUNT.AccRenwlDate = WFCSRENEWMSPlus.NewRenwlDate,
                      A2ZACCOUNT.AccRenwlAmt = WFCSRENEWMSPlus.NewRenwlAmt,
                      A2ZACCOUNT.AccNoRenwl  = WFCSRENEWMSPlus.NewNoRenwl,
                      A2ZACCOUNT.AccPeriod  = WFCSRENEWMSPlus.AccPeriodMonths,
                      A2ZACCOUNT.AccMatureDate = WFCSRENEWMSPlus.NewMatureDate,
                      A2ZACCOUNT.AccFixedMthInt = WFCSRENEWMSPlus.NewFixedMthInt,
                      A2ZACCOUNT.AccNoBenefit = 0
 


FROM A2ZACCOUNT,WFCSRENEWMSPlus
WHERE A2ZACCOUNT.AccType = WFCSRENEWMSPlus.AccType AND A2ZACCOUNT.AccNo = WFCSRENEWMSPlus.AccNo AND 
A2ZACCOUNT.CuType = WFCSRENEWMSPlus.CuType AND A2ZACCOUNT.CuNo = WFCSRENEWMSPlus.CuNo AND 
A2ZACCOUNT.MemNo = WFCSRENEWMSPlus.MemNo;
----------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSRENEWMSPlus SET WFCSRENEWMSPlus.ProcStat = 3;
UPDATE WFCSRENEWMSPlus SET WFCSRENEWMSPlus.VoucherNo = @VoucherNo;

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

