USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptGLYETransactionVch]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







/*
EXECUTE Sp_rptGLYETransactionVch '2017-06-29','N1',1 

*/


CREATE PROCEDURE [dbo].[Sp_rptGLYETransactionVch] (@fDate VARCHAR(10),@VchNo NVARCHAR(10),@CommonNo1 TINYINT) 
AS
BEGIN

DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nCount INT;

SET @nCount = YEAR(@fDate);

TRUNCATE TABLE WFA2ZTRANSACTION;

SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (TrnDate,VchNo,VoucherNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
		'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
		'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
		'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
		' SELECT ' +
		'TrnDate,VchNo,VoucherNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
		'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
		'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
		'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
		' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION' + 
        ' WHERE TrnCSGL = ' + CAST(@CommonNo1 AS VARCHAR(2)) + ' AND TrnDate = ' + '''' + @fDate + '''' + ' AND VchNo = ' + 
		'''' + @VchNo + '''';

EXECUTE (@strSQL);

SELECT     TOP (100) PERCENT dbo.WFA2ZTRANSACTION.VchNo, dbo.WFA2ZTRANSACTION.TrnFlag, SUM(dbo.WFA2ZTRANSACTION.GLDebitAmt) AS GLDebitAmt, 
                      SUM(dbo.WFA2ZTRANSACTION.GLCreditAmt) AS GLCreditAmt, dbo.WFA2ZTRANSACTION.UserID, dbo.WFA2ZTRANSACTION.VerifyUserID, 
                      A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, dbo.WFA2ZTRANSACTION.GLAccNo, dbo.WFA2ZTRANSACTION.TrnCSGL, dbo.WFA2ZTRANSACTION.TrnDate 
                      
FROM         dbo.WFA2ZTRANSACTION LEFT OUTER JOIN
                      A2ZGLMCUS.dbo.A2ZCGLMST ON dbo.WFA2ZTRANSACTION.GLAccNo = A2ZGLMCUS.dbo.A2ZCGLMST.GLAccNo
WHERE     (dbo.WFA2ZTRANSACTION.TrnCSGL = @CommonNo1)

GROUP BY dbo.WFA2ZTRANSACTION.VchNo, dbo.WFA2ZTRANSACTION.TrnFlag, dbo.WFA2ZTRANSACTION.UserID, dbo.WFA2ZTRANSACTION.VerifyUserID, 
                      A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, dbo.WFA2ZTRANSACTION.GLAccNo, dbo.WFA2ZTRANSACTION.TrnCSGL, dbo.WFA2ZTRANSACTION.TrnDate 
                     
ORDER BY dbo.WFA2ZTRANSACTION.TrnFlag
END






GO
