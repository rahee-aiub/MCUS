USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGenerateTransactionDataAType]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO












CREATE PROCEDURE [dbo].[Sp_CSGenerateTransactionDataAType](@AccType INT, @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS

/*

EXECUTE Sp_CSGenerateTransactionDataAType 51,'2016-01-31','2016-01-31',0


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
			
			SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE TrnDate' +
					' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''' + ' AND AccType = ' + CAST(@AccType AS VARCHAR(2));
			
			EXECUTE (@strSQL);

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

		SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE TrnDate' +
				' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''' + ' AND AccType = ' + CAST(@AccType AS VARCHAR(2));
		
		EXECUTE (@strSQL);
END








GO
