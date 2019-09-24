USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GLGenerateTransactionDataSingle]    Script Date: 04/29/2015 15:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_GLGenerateTransactionDataSingle](@AccountCode INT, @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS

/*

EXECUTE Sp_GLGenerateTransactionDataSingle 101001,'2014-12-27','2015-03-01',0


*/

BEGIN

	TRUNCATE TABLE WFA2ZTRANSACTION;
	
	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @nCount INT;

	SET @fYear = LEFT(@fDate,4);
	SET @tYear = LEFT(@tDate,4);

	SET @nCount = @fYear
	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,CreateDate' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8)) + 
					' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

			
			EXECUTE (@strSQL);

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

        SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,CreateDate' +
				' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8)) + 
				' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'
	
		EXECUTE (@strSQL);
END






