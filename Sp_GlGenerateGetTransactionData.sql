
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GlGenerateGetTransactionData](@vchNo VARCHAR(20), @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
/*

EXECUTE Sp_GlGenerateGetTransactionData '2017-01-31','2017-12-31',0


*/

BEGIN

	TRUNCATE TABLE WFEDITA2ZTRANSACTION;
	
	DECLARE @fYear INT;
	DECLARE @tYear INT;
    DECLARE @nYear INT;
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @nCount INT;

	SET @fYear = LEFT(@fDate,4);
	SET @tYear = LEFT(@tDate,4);

   
	SET @nCount = @fYear
	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO WFEDITA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,EditId)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,Id' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE TrnProcStat = 0 AND TrnCSGL=1 AND TrnDate' +
					' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
            
             SET @strSQL = @strSQL + ' AND VchNo = ' + '''' + CAST(@vchNo AS VARCHAR(20)) + '''';


			
			EXECUTE (@strSQL);

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

		SET @strSQL = 'INSERT INTO WFEDITA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,EditId)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,Id' +
				' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE TrnProcStat = 0 AND TrnCSGL=1 AND TrnDate' +
				' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';

            SET @strSQL = @strSQL + ' AND VchNo = ' + '''' + CAST(@vchNo AS VARCHAR(20)) + '''';
		
		EXECUTE (@strSQL);


       UPDATE WFEDITA2ZTRANSACTION SET WFEDITA2ZTRANSACTION.GLAccDrDesc = A2ZCGLMST.GLAccDesc 
       FROM A2ZCGLMST,WFEDITA2ZTRANSACTION
       WHERE WFEDITA2ZTRANSACTION.TrnGLAccNoDr=A2ZCGLMST.GLAccNo;

       UPDATE WFEDITA2ZTRANSACTION SET WFEDITA2ZTRANSACTION.GLAccCrDesc = A2ZCGLMST.GLAccDesc 
       FROM A2ZCGLMST,WFEDITA2ZTRANSACTION
       WHERE WFEDITA2ZTRANSACTION.TrnGLAccNoCr=A2ZCGLMST.GLAccNo;

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

