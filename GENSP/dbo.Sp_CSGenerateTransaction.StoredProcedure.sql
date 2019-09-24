USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGenerateTransaction]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_CSGenerateTransaction](@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS

/*

EXECUTE Sp_CSGenerateAccountBalance '2014-08-25','2014-12-31',0


*/

BEGIN

DECLARE @strSQL NVARCHAR(MAX);
	
    TRUNCATE TABLE WFTRANSACTIONLIST;

	
	EXECUTE Sp_CSGenerateTransactionData @fDate,@tDate,0;

	
    SET @strSQL = 'INSERT INTO WFTRANSACTIONLIST (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,UserIP,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,UserIP,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZCSMCUS..WFA2ZTRANSACTION WHERE TrnDate' +
				' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
		
		EXECUTE (@strSQL);


	
END








GO
