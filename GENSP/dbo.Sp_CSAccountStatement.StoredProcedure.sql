USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAccountStatement]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO












CREATE PROCEDURE [dbo].[Sp_CSAccountStatement](@CuType INT,@CuNo INT, @MemNo INT, @TrnCode INT, @AccType INT, @AccNo BIGINT, @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)


AS



---EXECUTE Sp_CSAccountStatement 3,5,0,20201001,12,1230005000000001,'2015-10-01','2015-10-01',0



BEGIN

	
	


    DECLARE @opBalance MONEY;
	DECLARE @debitAmt MONEY;
	DECLARE @creditAmt MONEY;

	TRUNCATE TABLE WFCSSTATEMENT;	

    


	EXECUTE Sp_CSGenerateOpeningBalanceSingle @CuType,@CuNo,@MemNo,@TrnCode,@AccType,@AccNo, @fDate,0;

--===============  Find Out Debit Credit Amount ============
	
	SET @opBalance = (SELECT AccOpBal FROM A2ZACCOUNT WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo);

	SET @debitAmt = 0;
	SET @creditAmt = 0;

	
	IF @opBalance > 0
	   BEGIN
			SET @creditAmt = ABS(@opBalance);
	   END
	ELSE
		BEGIN
			SET @debitAmt = ABS(@opBalance);
		END
	
	--=============  Find Out Debit Credit Amount ============

	EXECUTE Sp_CSGenerateTransactionDataSingle @CuType,@CuNo,@MemNo,@TrnCode,@AccType,@AccNo, @fDate,@tDate,0;

	INSERT INTO WFCSSTATEMENT (TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnDebit,TrnCredit,TrnDesc,TrnType)
	VALUES (@fDate,@CuType,@CuNo,@MemNo,@AccType,@AccNo,@debitAmt,@creditAmt,'=== Opening Balance ===',0)

	INSERT INTO WFCSSTATEMENT (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,
	UserID,CreateDate)
	SELECT
	BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,
	UserID,CreateDate
	FROM WFA2ZTRANSACTION;

	

END












GO
