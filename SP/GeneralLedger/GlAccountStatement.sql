USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GlAccountStatement]    Script Date: 04/29/2015 15:38:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_GlAccountStatement](@AccountCode INT, @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS

/*

EXECUTE Sp_GLAccountStatement 101001,'2014-12-27','2015-03-01',0



*/

BEGIN
	DECLARE @accType INT;
	DECLARE @opBalance MONEY;
	DECLARE @debitAmt MONEY;
	DECLARE @creditAmt MONEY;

	TRUNCATE TABLE WFGLSTATEMENT;	

	EXECUTE Sp_GLGenerateOpeningBalanceSingle @AccountCode, @fDate,0;

--===============  Find Out Debit Credit Amount ============
	SET @accType = (SELECT GLAccType FROM A2ZCGLMST WHERE GLAccNo = @AccountCode);
	SET @opBalance = (SELECT GLOpBal FROM A2ZCGLMST WHERE GLAccNo = @AccountCode);

	SET @debitAmt = 0;
	SET @creditAmt = 0;

	IF @accType = 1 OR @accType = 5
		BEGIN
			IF @opBalance > 0
				BEGIN
					SET @debitAmt = ABS(@opBalance);
				END
			ELSE
				BEGIN
					SET @creditAmt = ABS(@opBalance);
				END
		END
	
	IF @accType = 2 OR @accType = 4
		BEGIN
			IF @opBalance > 0
				BEGIN
					SET @creditAmt = ABS(@opBalance);
				END
			ELSE
				BEGIN
					SET @debitAmt = ABS(@opBalance);
				END
		END
--===============  Find Out Debit Credit Amount ============

	EXECUTE Sp_GLGenerateTransactionDataSingle @AccountCode, @fDate,@tDate,0;

	

	INSERT INTO WFGLSTATEMENT (BatchNo,TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,
	UserID,CreateDate)
	SELECT
	BatchNo,TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,
	UserID,CreateDate
	FROM WFA2ZTRANSACTION;

	INSERT INTO WFGLSTATEMENT (TrnDate,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnDesc)
	VALUES (@fDate,@AccountCode,@opBalance,@debitAmt,@creditAmt,'=== Opening Balance ===')

END


