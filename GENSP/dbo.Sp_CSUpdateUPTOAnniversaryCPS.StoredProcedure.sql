USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSUpdateUPTOAnniversaryCPS]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSUpdateUPTOAnniversaryCPS](@VoucherNo nvarchar(20),@VchNo nvarchar(20),@FromCashCode int)  
AS
--EXECUTE Sp_CSUpdateUPTOAnniversaryCPS 1,AnniversaryCPS,10101001

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

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 14);
SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=51);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=51);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=51);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=51);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=51);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=51);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=51);
SET @TrnDesc = 'Provision Adj.Cr.';

INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,1,0,
CalAdjProvCr,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,CalAdjProvCr,0,CalAdjProvCr,0,@FromCashCode,1,1,UserID
FROM WFCSANNIVERSARYCPS WHERE CalAdjProvCr <> 0;

--------CONTRA ------

INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,0,CalAdjProvCr,
0,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,CalAdjProvCr,CalAdjProvCr,0,1,@FromCashCode,1,1,UserID
FROM WFCSANNIVERSARYCPS WHERE CalAdjProvCr <> 0;

---------  END OF ADJ PROVISION CR. ------------


-------- NORMAL ADJ PROVISION DR. ----------------

SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 14);
SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=52);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=52);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=52);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=52);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=52);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=52);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 14 AND FuncOpt=52);
SET @TrnDesc = 'Provision Adj.Dr.';


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,0,CalAdjProvDr,
0,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,(0-CalAdjProvDr),CalAdjProvDr,0,0,@FromCashCode,1,1,UserID
FROM WFCSANNIVERSARYCPS WHERE CalAdjProvDr <> 0;

-----------CONTRA -----

INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,1,0,
CalAdjProvDr,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,CalAdjProvDr,0,CalAdjProvDr,1,@FromCashCode,1,1,UserID
FROM WFCSANNIVERSARYCPS WHERE CalAdjProvDr <> 0;

---------  END OF ADJ PROVISION DR. ------------


--------------- NORMAL TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,1,0,
CalCurrentAmt,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoCr,CalCurrentAmt,0,CalCurrentAmt,0,@FromCashCode,1,1,UserID
FROM WFCSANNIVERSARYCPS;
--------------- END OF NORMAL TRANSACTION ------------

--------------- NORMAL CONTRA TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,0,CalCurrentAmt,
0,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoDr,(0-CalCurrentAmt),CalCurrentAmt,0,1,@FromCashCode,1,1,UserID
FROM WFCSANNIVERSARYCPS;
--------------- END OF NORMAL CONTRA TRANSACTION ------------

----------------- UPDATE A2ZACCOUNT FILE --------------------
UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevAnniDate = A2ZACCOUNT.AccAnniDate,
                      A2ZACCOUNT.AccPrevNoAnni = A2ZACCOUNT.AccNoAnni,                                        
                      A2ZACCOUNT.AccPrevProvBalance = A2ZACCOUNT.AccProvBalance,       

                      A2ZACCOUNT.AccAnniDate = WFCSANNIVERSARYCPS.NewAnniDate,                 
                      A2ZACCOUNT.AccNoAnni  = WFCSANNIVERSARYCPS.NewNoAnni,                     
                      A2ZACCOUNT.AccAnniAmt = WFCSANNIVERSARYCPS.NewAnniAmt,
                      A2ZACCOUNT.AccBalance = (A2ZACCOUNT.AccBalance + WFCSANNIVERSARYCPS.CalCurrentAmt),
                      A2ZACCOUNT.AccProvBalance = (A2ZACCOUNT.AccProvBalance - (WFCSANNIVERSARYCPS.CalCurrentAmt + WFCSANNIVERSARYCPS.CalAdjProvDr - WFCSANNIVERSARYCPS.CalAdjProvCr))
                     


FROM A2ZACCOUNT,WFCSANNIVERSARYCPS
WHERE A2ZACCOUNT.AccType = WFCSANNIVERSARYCPS.AccType AND A2ZACCOUNT.AccNo = WFCSANNIVERSARYCPS.AccNo AND 
A2ZACCOUNT.CuType = WFCSANNIVERSARYCPS.CuType AND A2ZACCOUNT.CuNo = WFCSANNIVERSARYCPS.CuNo AND 
A2ZACCOUNT.MemNo = WFCSANNIVERSARYCPS.MemNo;
----------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSANNIVERSARYCPS SET WFCSANNIVERSARYCPS.ProcStat = 3;
UPDATE WFCSANNIVERSARYCPS SET WFCSANNIVERSARYCPS.VoucherNo = @VoucherNo;

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
