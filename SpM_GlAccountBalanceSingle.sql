
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SpM_GlAccountBalanceSingle](@AccountCode INT,@fDate VARCHAR(10), @nFlag INT)
AS
--
-- SpM_GlAccountBalanceSingle 10101001,'2017-12-25',0
--
BEGIN
	SET NOCOUNT ON;

	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
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
	
	SET @nDate = @fDate;

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZGLPARAMETER);

    SET @IYear = YEAR(@fDate);


    IF @IYear > @BegYear
       BEGIN
            SET @opDate = CAST(@BegYear AS VARCHAR(4)) + '-07-01';
       END
    ELSE
       BEGIN

	        SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-07-01';

	        SET @nYear = YEAR(@nDate);

	        IF MONTH(@nDate) < 7
		       BEGIN
			        SET @nYear = @nYear - 1;

			        SET @opDate = CAST(@nYear AS VARCHAR(4)) + '-07-01';
		       END

       END	

	SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZGLOPBALANCE';

	SET @strSQL = 'UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = ' + 
		'ISNULL((SELECT SUM(GLOpBal) FROM ' + @openTable +
		' WHERE ' + @openTable + '.GLAccNo = #A2ZCGLMST.GLAccNo),0)' +
		' FROM #A2ZCGLMST,' + @openTable + 
		' WHERE ' + @openTable + '.GLAccNo = #A2ZCGLMST.GLAccNo';
	EXECUTE (@strSQL);

--=================  Generate Year Wise Transaction Data For Opeing Balance ===========
		SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE UserID = 0;
		TRUNCATE TABLE #WFA2ZTRANSACTION;

		SET @fYear = LEFT(@opDate,4);
		SET @tYear = LEFT(@fDate,4);
   
		SET @nCount = @fYear
		WHILE (@nCount <> 0)
			BEGIN
				SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE' +
					' TrnProcStat = 0 AND TrnDate' +
					' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' +
					' AND GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8));
			
				EXECUTE (@strSQL);

				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
				END 
	--=================  Generate Current Month Transaction Data For Opeing Balance ===========
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
					' TrnDate BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' +
					' AND GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8));
				
				EXECUTE (@strSQL);
		
				UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = #A2ZCGLMST.GLOpBal + 
				ISNULL((SELECT SUM(GLAmount) FROM #WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo),0)
				FROM #A2ZCGLMST,#WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo;
	--================= End of Generate Transaction Data For Opeing Balance ===========

	SELECT GLOpBal,GLBalanceType FROM #A2ZCGLMST;

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

