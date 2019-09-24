USE A2ZGLMCUS
GO

ALTER PROCEDURE [dbo].[SpM_GlGenerateTransactionList](
@fDate VARCHAR(10), 
@tDate VARCHAR(10),
@CashCode INT,
@AccountCode INT,
@TrnType INT,
@TrnMode INT,
@VchNo NVARCHAR(20),
@CSGLTransaction INT,
@UserId INT,
@AutoTransaction INT,
@TransactionAmount MONEY,
@nFlag INT)
AS
--
-- SpM_GlGenerateTransactionList '2016-04-01','2016-04-30',0,0,0,0,'0',0,0,0,0,0
-- SpM_GlGenerateTransactionList '2016-04-01','2016-04-30',0,0,0,0,'0',0,0,0,0,1
-- SpM_GlGenerateTransactionList '2016-04-01','2016-04-30',0,0,0,0,'0',0,0,0,0,2
-- SpM_GlGenerateTransactionList '2016-04-01','2016-04-30',0,0,0,0,'0',0,0,0,0,3
--
-- @nFlag = 0 = GL Detail Transaction List
-- @nFlag = 1 = GL Summary Transaction List
-- @nFlag = 2 = GL Summary Transaction List By GL Code
-- @nFlag = 3 = GL Summary Transaction List By Cash Code
--
--
BEGIN

	DECLARE @strSQL NVARCHAR(MAX);

	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @nCount INT;

	SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE UserID = 0;

	ALTER TABLE #WFA2ZTRANSACTION ADD GLAccDesc NVARCHAR(100);

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

	EXECUTE (@strSQL);
--================ End of Generate Current Transaction ====================

--================= Filter Data According to UI ============================
	IF @CashCode > 0 
		BEGIN
			DELETE FROM #WFA2ZTRANSACTION WHERE FromCashCode <> @CashCode AND GLAccNo <> 0;
		END

	IF @AccountCode > 0
		BEGIN
			DELETE FROM #WFA2ZTRANSACTION WHERE GLAccNo <> @AccountCode;
		END

	IF @TrnType > 0
		BEGIN
			IF @TrnType = 1
				BEGIN
					DELETE FROM #WFA2ZTRANSACTION WHERE TrnType <> 1;
				END
			IF @TrnType = 2
				BEGIN
					DELETE FROM #WFA2ZTRANSACTION WHERE TrnType <> 3;
				END
			IF @TrnType = 3
				BEGIN
					DELETE FROM #WFA2ZTRANSACTION WHERE TrnType <> 1 OR TrnVchType <> 'C';
				END
			IF @TrnType = 4
				BEGIN
					DELETE FROM #WFA2ZTRANSACTION WHERE TrnType <> 1;
				END
			IF @TrnType = 5
				BEGIN
					DELETE FROM #WFA2ZTRANSACTION WHERE TrnType <> 3;
				END
		END

	IF @TrnMode > 0
		BEGIN
			IF @TrnMode = 1
				BEGIN
					DELETE FROM #WFA2ZTRANSACTION WHERE TrnDrCr = 1;
				END
			IF @TrnMode = 2
				BEGIN
					DELETE FROM #WFA2ZTRANSACTION WHERE TrnDrCr = 0;
				END
		END

	IF @VchNo <> '0'
		BEGIN
			DELETE FROM #WFA2ZTRANSACTION WHERE VchNo <> @VchNo AND GLAccNo <> 0;
		END

	IF @CSGLTransaction > 0
		BEGIN
			IF @CSGLTransaction = 1
				BEGIN
					DELETE FROM #WFA2ZTRANSACTION WHERE TrnCSGL <> 0 AND GLAccNo <> 0;
				END
			IF @CSGLTransaction = 2
				BEGIN
					DELETE FROM #WFA2ZTRANSACTION WHERE TrnCSGL <> 1 AND GLAccNo <> 0;
				END
		END
	
	IF @UserId > 0 
		BEGIN
			DELETE FROM #WFA2ZTRANSACTION WHERE UserId <> @UserId;
		END

	IF @AutoTransaction > 0
		BEGIN
			DELETE FROM #WFA2ZTRANSACTION WHERE TrnCSGL <> @AutoTransaction;
		END	

	IF @TransactionAmount <> 0
		BEGIN
			DELETE FROM #WFA2ZTRANSACTION WHERE GLAmount <> @TransactionAmount;
		END

--================= End of Filter Data According to UI ============================

	UPDATE #WFA2ZTRANSACTION SET #WFA2ZTRANSACTION.GLAccDesc = (SELECT A2ZCGLMST.GLAccDesc FROM A2ZCGLMST 
	WHERE A2ZCGLMST.GLAccNo = #WFA2ZTRANSACTION.GLAccNo);

-- @nFlag = 0 = GL Detail Transaction List
	IF @nFlag = 0
		BEGIN
			SELECT * FROM #WFA2ZTRANSACTION;
		END

-- @nFlag = 1 = GL Summary Transaction List
	IF @nFlag = 1
		BEGIN
			DELETE FROM #WFA2ZTRANSACTION WHERE TrnType = 0 OR FromCashCode = 0;

			SELECT #WFA2ZTRANSACTION.TrnDate, #WFA2ZTRANSACTION.GLAccNo, SUM(#WFA2ZTRANSACTION.GLDebitAmt) AS GLDebitAmt, 
			SUM(#WFA2ZTRANSACTION.GLCreditAmt) AS GLCreditAmt, A2ZCGLMST.GLAccDesc, #WFA2ZTRANSACTION.TrnProcStat                      
			FROM #WFA2ZTRANSACTION LEFT OUTER JOIN
			A2ZCGLMST ON #WFA2ZTRANSACTION.GLAccNo = A2ZCGLMST.GLAccNo
			WHERE (#WFA2ZTRANSACTION.TrnDate BETWEEN @fDate AND @tDate) AND (#WFA2ZTRANSACTION.TrnProcStat <> 1)                      
			GROUP BY #WFA2ZTRANSACTION.TrnDate, #WFA2ZTRANSACTION.GLAccNo, A2ZCGLMST.GLAccDesc, #WFA2ZTRANSACTION.TrnProcStat                       
			ORDER BY #WFA2ZTRANSACTION.TrnDate;
		END

-- @nFlag = 2 = GL Summary Transaction List By GL Code
	IF @nFlag = 2
		BEGIN
			DELETE FROM #WFA2ZTRANSACTION WHERE TrnType = 0 OR FromCashCode = 0;

			SELECT #WFA2ZTRANSACTION.GLAccNo, SUM(#WFA2ZTRANSACTION.GLDebitAmt) AS GLDebitAmt, 
			SUM(#WFA2ZTRANSACTION.GLCreditAmt) AS GLCreditAmt, A2ZCGLMST.GLAccDesc, #WFA2ZTRANSACTION.TrnProcStat                     
			FROM #WFA2ZTRANSACTION LEFT OUTER JOIN
			A2ZCGLMST ON #WFA2ZTRANSACTION.GLAccNo = A2ZCGLMST.GLAccNo
			WHERE (#WFA2ZTRANSACTION.TrnProcStat <> 1)
			GROUP BY #WFA2ZTRANSACTION.GLAccNo, A2ZCGLMST.GLAccDesc, #WFA2ZTRANSACTION.TrnProcStat 
			ORDER BY #WFA2ZTRANSACTION.GLAccNo;
		END

-- @nFlag = 3 = GL Summary Transaction List By Cash Code
	IF @nFlag = 3
		BEGIN
			SELECT #WFA2ZTRANSACTION.TrnDate, #WFA2ZTRANSACTION.GLAccNo, #WFA2ZTRANSACTION.FromCashCode, 
			SUM(#WFA2ZTRANSACTION.GLDebitAmt) AS GLDebitAmt, SUM(#WFA2ZTRANSACTION.GLCreditAmt) AS GLCreditAmt, 
			dbo.A2ZCGLMST.GLAccDesc, #WFA2ZTRANSACTION.TrnProcStat, #WFA2ZTRANSACTION.TrnType
			FROM #WFA2ZTRANSACTION LEFT OUTER JOIN
			A2ZCGLMST ON #WFA2ZTRANSACTION.GLAccNo = A2ZCGLMST.GLAccNo
			WHERE (#WFA2ZTRANSACTION.TrnDate BETWEEN @fDate AND @tDate) AND 
			(#WFA2ZTRANSACTION.TrnProcStat <> 1) AND (#WFA2ZTRANSACTION.TrnType <> 0) AND 
			(#WFA2ZTRANSACTION.FromCashCode <> 0)
			GROUP BY #WFA2ZTRANSACTION.TrnDate, #WFA2ZTRANSACTION.GLAccNo, dbo.A2ZCGLMST.GLAccDesc, 
			#WFA2ZTRANSACTION.TrnProcStat, #WFA2ZTRANSACTION.TrnType, #WFA2ZTRANSACTION.FromCashCode;
		END



END


