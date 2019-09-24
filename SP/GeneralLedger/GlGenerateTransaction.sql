USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GlGenerateTransaction]    Script Date: 04/29/2015 15:40:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_GlGenerateTransaction](@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS

/*

EXECUTE Sp_GlGenerateAccountBalance '2014-08-25','2014-12-31',0


*/

BEGIN

DECLARE @strSQL NVARCHAR(MAX);
	
    TRUNCATE TABLE WFTRANSACTIONLIST;

	--EXECUTE Sp_GlGenerateOpeningBalance @fDate,0;
	
	EXECUTE Sp_GlGenerateTransactionData @fDate,@tDate,0;

	UPDATE A2ZCGLMST SET GLDrSumC = 0,GLDrSumT = 0, GLCrSumC = 0, GLCrSumT = 0;

	UPDATE A2ZCGLMST SET A2ZCGLMST.GLDrSumC =  
	ISNULL((SELECT SUM(GLDebitAmt) FROM WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLDebitAmt > 0 AND WFA2ZTRANSACTION.TrnType = 1 AND 
	WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo),0)
	FROM A2ZCGLMST,WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo;

	UPDATE A2ZCGLMST SET A2ZCGLMST.GLDrSumT =  
	ISNULL((SELECT SUM(GLDebitAmt) FROM WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLDebitAmt > 0 AND WFA2ZTRANSACTION.TrnType <> 1 AND 
	WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo),0)
	FROM A2ZCGLMST,WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo;

	UPDATE A2ZCGLMST SET A2ZCGLMST.GLCrSumC =  
	ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLCreditAmt > 0 AND WFA2ZTRANSACTION.TrnType = 1 AND 
	WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo),0)
	FROM A2ZCGLMST,WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo;

	UPDATE A2ZCGLMST SET A2ZCGLMST.GLCrSumT =  
	ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLCreditAmt > 0 AND WFA2ZTRANSACTION.TrnType <> 1 AND 
	WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo),0)
	FROM A2ZCGLMST,WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo;

	UPDATE A2ZCGLMST SET GLClBal = (GLOpBal + GLDrSumC + GLDrSumT)  - (GLCrSumC + GLCrSumT)
	WHERE GLAccType IN (1,5);

	UPDATE A2ZCGLMST SET GLClBal = (GLOpBal + GLCrSumC + GLCrSumT) - (GLDrSumC + GLDrSumT)
	WHERE GLAccType IN (2,4);

    SET @strSQL = 'INSERT INTO WFTRANSACTIONLIST (BatchNo,TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,CreateDate' +
				' FROM A2ZGLMCUS..WFA2ZTRANSACTION WHERE TrnDate' +
				' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
		
		EXECUTE (@strSQL);


	--EXECUTE Sp_GlGenerateLayerWiseCOA 0;
END




