
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GlUpdateYETransaction](@userID INT, @vchNo nvarchar(20), @ProcStat smallint) 
AS
/*

EXECUTE Sp_GlUpdateYETransaction 1,'GGG',0


*/

BEGIN

DECLARE @PrmYearEndDate SMALLDATETIME;
DECLARE @IYear int;
DECLARE @strSQL NVARCHAR(MAX);

SET @PrmYearEndDate = (SELECT PrmYearEndDate FROM A2ZHKMCUS..A2ZERPSYSPRM);

SET @IYear = YEAR(@PrmYearEndDate);


SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,' +
				'TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnChqNo,TrnModule,FuncOpt,' +
				'TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,ValueDate,FromCashCode,TrnProcStat,TrnSysUser,TrnStatus,TrnGLFlag,UserID)' +
				' SELECT ' +
				'VoucherDate,VchNo,''' + @vchNo + ''',' +
				'TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnChqNo,TrnModule,FuncOpt,' +
				'TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,ValueDate,FromCashCode,' + CAST(@ProcStat AS VARCHAR(2)) + ',0,0,0,UserID' +
				' FROM A2ZGLMCUS.dbo.WFGLTrannsaction WHERE' +
				' UserID =' + CAST(@userID AS VARCHAR(4));

EXECUTE (@strSQL);


          UPDATE A2ZGLMCUS..A2ZCGLMST SET GLOpBal = 0; 

          UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLOpBal = A2ZGLMCUS..A2ZCGLMST.GLOpBal + 
	      ISNULL((SELECT SUM(GLAmount) FROM WFGLTrannsaction
		  WHERE A2ZGLMCUS..WFGLTrannsaction.UserID = @userID AND A2ZGLMCUS..WFGLTrannsaction.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo),0)
		  FROM A2ZGLMCUS..A2ZCGLMST,WFGLTrannsaction
		  WHERE A2ZGLMCUS..WFGLTrannsaction.UserID = @userID AND A2ZGLMCUS..WFGLTrannsaction.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo;

          UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance + A2ZGLMCUS..A2ZCGLMST.GLOpBal           
          FROM A2ZGLMCUS..A2ZCGLMST WHERE GLOpBal <> 0;   




END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

