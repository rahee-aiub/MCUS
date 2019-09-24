
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSUpdateManualRenewalFDR](@VoucherNo nvarchar(20),@FromCashCode int, @Atype INT, @accNo Bigint)  
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

DECLARE @VchNo nvarchar(20);


-------- NORMAL ADJ PROVISION CR. ----------------


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @Atype);
SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=51);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=51);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=51);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=51);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=51);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=51);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=51);



SET @TrnDesc = 'Provision Adj.Cr.';

SET @VchNo = 'ManualRenewal';

INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,1,0,
CalAdjProvCr,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,CalAdjProvCr,0,CalAdjProvCr,0,@FromCashCode,1,1,UserID,1
FROM WFCSMANUALRENEWFDR WHERE AccNo = @accNo AND CalAdjProvCr <> 0;

--------CONTRA ------

INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,0,CalAdjProvCr,
0,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,CalAdjProvCr,CalAdjProvCr,0,1,@FromCashCode,1,1,UserID,1
FROM WFCSMANUALRENEWFDR WHERE AccNo = @accNo AND CalAdjProvCr <> 0;

---------  END OF ADJ PROVISION CR. ------------


-------- NORMAL ADJ PROVISION DR. ----------------

SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @Atype);
SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=52);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=52);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=52);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=52);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=52);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=52);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @Atype AND FuncOpt=52);
SET @TrnDesc = 'Provision Adj.Dr.';


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,0,CalAdjProvDr,
0,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,(0-CalAdjProvDr),CalAdjProvDr,0,0,@FromCashCode,1,1,UserID,1
FROM WFCSMANUALRENEWFDR WHERE AccNo = @accNo AND CalAdjProvDr <> 0;

-----------CONTRA -----

INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode,@FuncOpt,@PayType,@TrnType,1,0,
CalAdjProvDr,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,CalAdjProvDr,0,CalAdjProvDr,1,@FromCashCode,1,1,UserID,1
FROM WFCSMANUALRENEWFDR WHERE AccNo = @accNo AND CalAdjProvDr <> 0;

---------  END OF ADJ PROVISION DR. ------------

--------------- NORMAL TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,0,
CalInterest,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoCr,CalInterest,0,CalInterest,0,@FromCashCode,1,1,UserID,1
FROM WFCSMANUALRENEWFDR WHERE AccNo = @accNo;
--------------- END OF NOPRMAL TRANSACTION ------------

--------------- CONTRA TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnContraDrCr,CalInterest,
0,TrnDesc,ShowInterest,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoDr,(0-CalInterest),CalInterest,0,1,@FromCashCode,1,1,UserID,1
FROM WFCSMANUALRENEWFDR WHERE AccNo = @accNo;
--------------- END OF CONTRA TRANSACTION ------------

----------------- UPDATE A2ZACCOUNT FILE --------------------
UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevRenwlDate = A2ZACCOUNT.AccRenwlDate,
                      A2ZACCOUNT.AccPrevRenwlAmt = A2ZACCOUNT.AccRenwlAmt,
                      A2ZACCOUNT.AccPrevNoRenwl = A2ZACCOUNT.AccNoRenwl,
                      A2ZACCOUNT.AccPrevNoAnni = A2ZACCOUNT.AccNoAnni,
                      A2ZACCOUNT.AccPrevAnniDate = A2ZACCOUNT.AccAnniDate,
                      A2ZACCOUNT.AccPrevIntRate = A2ZACCOUNT.AccIntRate,
                      A2ZACCOUNT.AccPrevPeriod = A2ZACCOUNT.AccPeriod,
                      A2ZACCOUNT.AccPrevMatureDate = A2ZACCOUNT.AccMatureDate,   
                      A2ZACCOUNT.AccPrevRaCurr = A2ZACCOUNT.AccPrincipal,   
                      A2ZACCOUNT.AccPrevRaLedger = A2ZACCOUNT.AccBalance, 
                      A2ZACCOUNT.AccPrevProvBalance = A2ZACCOUNT.AccProvBalance, 
                      
                      A2ZACCOUNT.AccRenwlDate = WFCSMANUALRENEWFDR.NewRenwlDate,
                      A2ZACCOUNT.AccRenwlAmt = WFCSMANUALRENEWFDR.NewRenwlAmt,
                      A2ZACCOUNT.AccNoRenwl  = WFCSMANUALRENEWFDR.NewNoRenwl,
                      A2ZACCOUNT.AccNoAnni  = WFCSMANUALRENEWFDR.NewNoAnni,
                      A2ZACCOUNT.AccAnniDate  = NULL,
                      A2ZACCOUNT.AccPeriod  = WFCSMANUALRENEWFDR.AccPeriodMonths,
                      A2ZACCOUNT.AccIntRate = WFCSMANUALRENEWFDR.NewIntRate,
                      A2ZACCOUNT.AccMatureDate = WFCSMANUALRENEWFDR.NewMatureDate,
                      A2ZACCOUNT.AccPrincipal = WFCSMANUALRENEWFDR.FDAmount,
                      A2ZACCOUNT.AccBalance = WFCSMANUALRENEWFDR.NewRenwlAmt,
                      A2ZACCOUNT.AccProvBalance = ((A2ZACCOUNT.AccProvBalance + WFCSMANUALRENEWFDR.CalAdjProvCr)- WFCSMANUALRENEWFDR.CalInterest) 

FROM A2ZACCOUNT,WFCSMANUALRENEWFDR
WHERE A2ZACCOUNT.AccType = WFCSMANUALRENEWFDR.AccType AND A2ZACCOUNT.AccNo = WFCSMANUALRENEWFDR.AccNo AND 
A2ZACCOUNT.CuType = WFCSMANUALRENEWFDR.CuType AND A2ZACCOUNT.CuNo = WFCSMANUALRENEWFDR.CuNo AND 
A2ZACCOUNT.MemNo = WFCSMANUALRENEWFDR.MemNo AND WFCSMANUALRENEWFDR.AccNo = @accNo;

----------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSMANUALRENEWFDR SET WFCSMANUALRENEWFDR.ProcStat = 3 WHERE AccNo = @accNo;
UPDATE WFCSMANUALRENEWFDR SET WFCSMANUALRENEWFDR.VoucherNo = @VoucherNo WHERE AccNo = @accNo;

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

