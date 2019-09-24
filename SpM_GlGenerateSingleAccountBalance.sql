USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[SpM_GlGenerateSingleAccountBalance]    Script Date: 07/14/2017 16:42:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[SpM_GlGenerateSingleAccountBalance](@AccountCode INT,@fDate VARCHAR(10), @nFlag INT)
AS
--
-- SpM_GlGenerateSingleAccountBalance 10101001,'2017-06-29',0
--
BEGIN
	SET NOCOUNT ON;

	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
    DECLARE @tDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30)

	DECLARE @fYear INT;
	DECLARE @tYear INT;
    DECLARE @nYear INT;
	DECLARE @nCount INT;

    DECLARE @BegYear int;
    DECLARE @IYear int;

	SELECT * INTO #A2ZCGLMST FROM A2ZCGLMST WHERE GLAccNo = @AccountCode;

--================   Generate Opening Balance ===============
	UPDATE #A2ZCGLMST SET GLOpBal = 0;
	
	SET @tDate = @fDate;

--=================  Generate Year Wise Transaction Data For Opeing Balance ===========
		SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE UserID = 0;
		TRUNCATE TABLE #WFA2ZTRANSACTION;

		
	
				SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE' +
					' TrnProcStat = 0 AND' +
					' TrnDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''' +
					' AND GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8));
				
				EXECUTE (@strSQL);
		
				UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = #A2ZCGLMST.GLOpBal + 
				ISNULL((SELECT SUM(GLAmount) FROM #WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo),0)
				FROM #A2ZCGLMST,#WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo;
--================= End of Generate Transaction Data For Opeing Balance ===========

     UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = #A2ZCGLMST.GLTodaysOpBalance + #A2ZCGLMST.GLOpBal           
            FROM #A2ZCGLMST
			WHERE #A2ZCGLMST.GLAccNo = @AccountCode;

SELECT GLOpBal,GLBalanceType FROM #A2ZCGLMST;
            

END





