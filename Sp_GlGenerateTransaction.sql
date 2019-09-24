
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GlGenerateTransaction](@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT,@teller INT,@TrnAmount money)
AS
/*

EXECUTE Sp_GlGenerateAccountBalance '2014-08-25','2014-12-31',0


*/

BEGIN

DECLARE @strSQL NVARCHAR(MAX);
	
    TRUNCATE TABLE WFTRANSACTIONLIST;

	--EXECUTE Sp_GlGenerateOpeningBalance @fDate,0;
	
	EXECUTE Sp_GlGenerateTransactionData @fDate,@tDate,0,0;

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

    SET @strSQL = 'INSERT INTO WFTRANSACTIONLIST (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZGLMCUS..WFA2ZTRANSACTION WHERE TrnDate' +
				' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
		
             IF @teller <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND UserID = ' + CAST(@teller AS VARCHAR(6));
				END	   

             IF @TrnAmount <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND GLAmount = ' + CAST(@TrnAmount AS VARCHAR(14));
				END	  


		EXECUTE (@strSQL);


	--EXECUTE Sp_GlGenerateLayerWiseCOA 0;
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

