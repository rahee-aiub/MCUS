
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SpM_GlGenerateCashBookSingle](@AccountCode INT,@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
--
-- @nFlag = 1 = Summary Report
-- @nFlag = 9 = Summary Report AND Before Year End
-- @nFlag = 8 = Before Year End IF @tDate = YYYY-06-30
-- Skip Those GL Transaction are P/L Transfer for Income/Expense
--
--
BEGIN
	DECLARE @PLCode INT;
	DECLARE @backStat INT;
	DECLARE @PLIncome MONEY;
	DECLARE @PLExpense MONEY;
	DECLARE @processDate SMALLDATETIME;

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



	SET @PLCode = (SELECT PLGLCode FROM A2ZGLPARAMETER);
	SET @processDate = (SELECT ProcessDate FROM A2ZGLMCUS..A2ZGLPARAMETER);
	
	SELECT * INTO #A2ZCGLMST FROM A2ZCGLMST;

	ALTER TABLE #A2ZCGLMST ADD CodeFlag TINYINT DEFAULT(0);
	ALTER TABLE #A2ZCGLMST ADD GLType TINYINT DEFAULT(0);


	UPDATE #A2ZCGLMST SET GLType = 1 WHERE LEFT(GLAccNo,5) = 10101;	         -- Cash Account Code
	UPDATE #A2ZCGLMST SET GLType = 2 WHERE LEFT(GLAccNo,5) IN (10106,20801); -- Bank Account Code
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

--	IF MONTH(@nDate) <> 7
--		BEGIN
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
					' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''';

				IF @AccountCode > 0 
					BEGIN
						SET @strSQL = @strSQL + ' AND FromCashCode = ' + CAST(@AccountCode AS VARCHAR(8))
					END
			
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
					' TrnDate BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''';
			
				IF @AccountCode > 0 
					BEGIN
						SET @strSQL = @strSQL + ' AND FromCashCode = ' + CAST(@AccountCode AS VARCHAR(8))
					END

				EXECUTE (@strSQL);

				SET @strSQL = 'DELETE FROM #WFA2ZTRANSACTION WHERE TrnDate = ''' + @fDate + '''';
				EXECUTE (@strSQL);

				IF @nFlag = 8 OR @nFlag = 9
					BEGIN 
						DELETE FROM #WFA2ZTRANSACTION WHERE TrnCSGL = 1 AND FuncOpt = 999;
					END
			
				UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = #A2ZCGLMST.GLOpBal + 
				ISNULL((SELECT SUM(GLAmount) FROM #WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo),0)
				FROM #A2ZCGLMST,#WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo;
--			END		
	--================= End of Generate Transaction Data For Opeing Balance ===========

--================   End of Generate Opening Balance ===============
		TRUNCATE TABLE #WFA2ZTRANSACTION;
--================ Generate Current Transaction ====================
		SET @fYear = LEFT(@fDate,4);
		SET @tYear = LEFT(@tDate,4);
   
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
					' TrnProcStat = 0 AND' +
					' TrnDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
			
				IF @AccountCode > 0 
					BEGIN
						SET @strSQL = @strSQL + ' AND FromCashCode = ' + CAST(@AccountCode AS VARCHAR(8))
					END

				EXECUTE (@strSQL);

				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
			END 

		SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
			'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
			'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
			'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
			' SELECT ' +
			'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
			'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
			'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
			'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
			' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE TrnProcStat = 0 AND TrnDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';

		IF @AccountCode > 0 
			BEGIN
				SET @strSQL = @strSQL + ' AND FromCashCode = ' + CAST(@AccountCode AS VARCHAR(8))
			END

		EXECUTE (@strSQL);
--================ End of Generate Current Transaction ====================
	IF @nFlag = 8 OR @nFlag = 9
		BEGIN 
			DELETE FROM #WFA2ZTRANSACTION WHERE TrnCSGL = 1 AND FuncOpt = 999;
		END

	UPDATE #A2ZCGLMST SET GLDrSumC = 0,GLDrSumT = 0, GLCrSumC = 0, GLCrSumT = 0;

	UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLDrSumC =  
	ISNULL((SELECT SUM(GLDebitAmt) FROM #WFA2ZTRANSACTION
	WHERE #WFA2ZTRANSACTION.GLDebitAmt > 0 AND #WFA2ZTRANSACTION.TrnType = 1 AND 
	#WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo),0)
	FROM #A2ZCGLMST,#WFA2ZTRANSACTION
	WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo;

	UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLDrSumT =  
	ISNULL((SELECT SUM(GLDebitAmt) FROM #WFA2ZTRANSACTION
	WHERE #WFA2ZTRANSACTION.GLDebitAmt > 0 AND #WFA2ZTRANSACTION.TrnType <> 1 AND 
	#WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo),0)
	FROM #A2ZCGLMST,#WFA2ZTRANSACTION
	WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo;

	UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLCrSumC =  
	ISNULL((SELECT SUM(GLCreditAmt) FROM #WFA2ZTRANSACTION
	WHERE #WFA2ZTRANSACTION.GLCreditAmt > 0 AND #WFA2ZTRANSACTION.TrnType = 1 AND 
	#WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo),0)
	FROM #A2ZCGLMST,#WFA2ZTRANSACTION
	WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo;

	UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLCrSumT =  
	ISNULL((SELECT SUM(GLCreditAmt) FROM #WFA2ZTRANSACTION
	WHERE #WFA2ZTRANSACTION.GLCreditAmt > 0 AND #WFA2ZTRANSACTION.TrnType <> 1 AND 
	#WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo),0)
	FROM #A2ZCGLMST,#WFA2ZTRANSACTION
	WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo;

--================ Calculate P/L for Current TBf Transaction ============
	IF @processDate = @tDate
		BEGIN
			SET @PLIncome = ISNULL((SELECT SUM(GLAmount) AS 'TOTINCOME' FROM #WFA2ZTRANSACTION
							WHERE LEFT(GLAccNo, 1) = 4 AND TrnDate = @tDate),0);

			SET @PLExpense = ISNULL((SELECT SUM(GLAmount) AS 'TOTEXPENSE' FROM #WFA2ZTRANSACTION
							WHERE LEFT(GLAccNo, 1) = 5 AND TrnDate = @tDate),0);
					
			UPDATE #A2ZCGLMST SET GLDrSumT = GLDrSumT + ABS(@PLIncome) 
			WHERE GLAccNo = @PLCode AND @PLIncome < 0;

			UPDATE #A2ZCGLMST SET GLCrSumT = GLCrSumT + ABS(@PLIncome) 
			WHERE GLAccNo = @PLCode AND @PLIncome > 0;

			UPDATE #A2ZCGLMST SET GLCrSumT = GLCrSumT + ABS(@PLExpense) 
			WHERE GLAccNo = @PLCode AND @PLExpense < 0;

			UPDATE #A2ZCGLMST SET GLDrSumT = GLDrSumT + ABS(@PLExpense) 
			WHERE GLAccNo = @PLCode AND @PLExpense > 0;
		END;
--================ End of Calculate P/L for Current TBf Transaction ============

	UPDATE #A2ZCGLMST SET GLClBal = (GLOpBal + GLDrSumC + GLDrSumT)  - (GLCrSumC + GLCrSumT)
	WHERE GLAccType IN (1,5);

	UPDATE #A2ZCGLMST SET GLClBal = (GLOpBal + GLCrSumC + GLCrSumT) - (GLDrSumC + GLDrSumT)
	WHERE GLAccType IN (2,4);

--================ Generate Layer Wise Chart of Account ==============

	DELETE FROM #A2ZCGLMST WHERE (ABS(GLOpBal) + GLDrSumC + GLDrSumT + GLCrSumC + GLCrSumT) = 0;

	DELETE FROM #A2ZCGLMST WHERE (GLDrSumC + GLDrSumT + GLCrSumC + GLCrSumT) = 0 AND GLAccType > 1;

	DELETE FROM #A2ZCGLMST WHERE GLAccNo = @PLCode;

	IF @AccountCode > 0 
		BEGIN
			DELETE FROM #A2ZCGLMST WHERE GLAccNo = @AccountCode AND GLType = 2;
		END	

	IF @AccountCode > 0 
		BEGIN
			DELETE FROM #A2ZCGLMST WHERE (ABS(GLOpBal) + GLDrSumC + GLDrSumT + GLCrSumC + GLCrSumT) = 0 AND
			GLAccNo <> @AccountCode AND GLAccNo > 10106000 AND GLAccNo < 20000001;
		END

	UPDATE #A2ZCGLMST SET GLAccType = 1,CodeFlag = 2;

	UPDATE #A2ZCGLMST SET GLAccType = 0,CodeFlag = 1 WHERE GLType = 2;

	UPDATE #A2ZCGLMST SET GLAccType = 2,CodeFlag = 2 WHERE GLType = 1;

	INSERT INTO #A2ZCGLMST ([GLCoNo],[GLAccType],[GLAccNo],[GLRecType],[GLPrtPos],[GLAccDesc],
	[GLAccMode],[GLBgtType],[GLOpBal],[GLDrSumC],[GLDrSumT],[GLCrSumC],[GLCrSumT],[GLClBal],[GLHead],[GLMainHead],
	[GLSubHead],[GLHeadDesc],[GLMainHeadDesc],[GLSubHeadDesc],[GLOldAccNo],[LastVoucherNo],[GLBalanceType],[CodeFlag])
	SELECT [GLCoNo],2,[GLAccNo],[GLRecType],[GLPrtPos],[GLAccDesc],
	[GLAccMode],[GLBgtType],[GLOpBal],[GLDrSumC],[GLDrSumT],[GLCrSumC],[GLCrSumT],[GLClBal],[GLHead],[GLMainHead],
	[GLSubHead],[GLHeadDesc],[GLMainHeadDesc],[GLSubHeadDesc],[GLOldAccNo],[LastVoucherNo],[GLBalanceType],2
	FROM #A2ZCGLMST WHERE GLType = 2; 

	DELETE FROM #A2ZCGLMST WHERE (ABS(GLOpBal) + GLDrSumC + GLDrSumT + GLCrSumC + GLCrSumT) = 0 AND
	GLType = 2;

	DELETE FROM #A2ZCGLMST WHERE (GLDrSumC + GLDrSumT + GLCrSumC + GLCrSumT) = 0 AND
	GLAccType IN(0,1);

	IF @AccountCode > 0
		BEGIN
			DELETE FROM #A2ZCGLMST WHERE (GLDrSumC + GLDrSumT + GLCrSumC + GLCrSumT) = 0 AND
			GLAccNo <> @AccountCode AND GLAccType = 2;
		END


	IF @nFlag = 1 OR @nFlag = 9	-- Summary Report
		BEGIN
			INSERT INTO #A2ZCGLMST (GLSubHead,GLSubHeadDesc,GLOPBAL,GLDRSUMC,GLDRSUMT,GLCRSUMC,GLCRSUMT,GLCLBAL,
			GLAccType,CodeFlag)
			SELECT GLSubHead,GLSubHeadDesc,SUM(GLOPBAL) AS GLOPBAL,SUM(GLDRSUMC) AS GLDRSUMC,SUM(GLDRSUMT) AS GLDRSUMT,
			SUM(GLCRSUMC) AS GLCRSUMC,SUM(GLCRSUMT) AS GLCRSUMT,SUM(GLCLBAL) AS GLCLBAL,NULL,NULL
			FROM #A2ZCGLMST WHERE (GLAccType = 2 AND CodeFlag = 2) GROUP BY GLSubHead,GLSubHeadDesc;

			DELETE FROM #A2ZCGLMST WHERE GLAccType = 2 AND CodeFlag = 2;

			UPDATE #A2ZCGLMST SET GLAccType = 2, CodeFlag = 2 WHERE GLAccType IS NULL AND CodeFlag IS NULL;

			UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLCoNo = A2ZCGLMST.GLCoNo,
			#A2ZCGLMST.GLAccNo = A2ZCGLMST.GLAccNo,#A2ZCGLMST.GLAccDesc = A2ZCGLMST.GLAccDesc
			FROM #A2ZCGLMST,A2ZCGLMST
			WHERE #A2ZCGLMST.GLSubHead = A2ZCGLMST.GLSubHead AND #A2ZCGLMST.GLCoNo IS NULL;
		END

	SELECT * FROM #A2ZCGLMST;

--================ End of Generate Layer Wise Chart of Account ==============

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

