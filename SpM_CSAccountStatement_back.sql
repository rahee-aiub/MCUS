
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SpM_CSAccountStatement_back](@CuType INT,@CuNo INT, @MemNo INT, @TrnCode INT, @AccType INT, 
@AccNo VARCHAR(16), @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
--- @nFlag = 0 = Normal CS Account Statement
---EXECUTE SpM_CSAccountStatement 3,12,0,20201001,12,1230012000000001,'2016-04-01','2016-10-31',0

--- Staff Account Statement
--- @nFlag = 1 = Staff - Normal CS Account Statement
---EXECUTE SpM_CSAccountStatement 0,0,403,10504007,65,6504030001,'2016-04-01','2016-10-31',1
---
--- @nFlag = 2 = Interst Penalty - Normal CS Account Statement
---EXECUTE SpM_CSAccountStatement 3,5,0,20201001,12,1230005000000001,'2016-04-01','2016-10-31',2

--- @nFlag = 3 = Interest Penalty - Staff - Normal CS Account Statement
---EXECUTE SpM_CSAccountStatement 0,0,403,10504007,65,6504030001,'2016-04-01','2016-10-31',3

BEGIN	
	SET NOCOUNT ON;

	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @BegYear int;
    DECLARE @IYear int;

	DECLARE @nYear INT;	

    DECLARE @opBalance MONEY;
	DECLARE @debitAmt MONEY;
	DECLARE @creditAmt MONEY;

	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @nCount INT;

    DECLARE @ReadFlag INT;
    DECLARE @processDate SMALLDATETIME;

--	EXECUTE Sp_CSGenerateOpeningBalanceSingle @CuType,@CuNo,@MemNo,@TrnCode,@AccType,@AccNo, @fDate,0;

--===========  For Opening Balance ========================
	SELECT * INTO #A2ZACCOUNT FROM A2ZACCOUNT WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND 
	AccType = @AccType AND AccNo = @AccNo;

	UPDATE #A2ZACCOUNT SET AccOpBal = 0;

	SET @nDate = @fDate;

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZCSPARAMETER);

    SET @processDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

    IF @fDate = @processDate
       BEGIN
            SET @ReadFlag = 0;
       END
    ELSE
       BEGIN
            SET @ReadFlag = 1;
       END


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

    IF @ReadFlag = 1
       BEGIN

	SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZCSOPBALANCE';

	SET @strSQL = 'UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = ' +
		'ISNULL((SELECT SUM(TrnAmount) FROM ' + @openTable +  
		' WHERE CuType = ' + CAST(@CuType AS VARCHAR(2)) + 
		' AND CuNo = ' + CAST(@CuNo AS VARCHAR(6)) + 
		' AND MemNo = ' + CAST(@MemNo AS VARCHAR(6)) + 
		' AND AccType = ' + CAST(@AccType AS VARCHAR(2))  + 
		' AND AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
		'),0) FROM #A2ZACCOUNT,' + @openTable +
		' WHERE #A2ZACCOUNT.CuType = ' + CAST(@CuType AS VARCHAR(2))  + 
		' AND #A2ZACCOUNT.CuNo = ' + CAST(@CuNo AS VARCHAR(6)) + 
		' AND #A2ZACCOUNT.MemNo = ' + CAST(@MemNo AS VARCHAR(6)) + 
		' AND #A2ZACCOUNT.AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
		' AND #A2ZACCOUNT.AccNo = ' + CAST(@AccNo AS VARCHAR(16));
	
	EXECUTE (@strSQL);
         END

	SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE ID = 0;
	TRUNCATE TABLE #WFA2ZTRANSACTION;
--==========  Get Transaction Data For Opening Balance ==========
	SET @fYear = LEFT(@opDate,4);
	SET @tYear = LEFT(@fDate,4);

	SET @nCount = @fYear

    IF @ReadFlag = 1
       BEGIN
    

	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION ' +
					' WHERE CuType = ' + CAST(@CuType AS VARCHAR(2))  + 
					' AND CuNo = ' + CAST(@CuNo AS VARCHAR(6)) + 
					' AND MemNo = ' + CAST(@MemNo AS VARCHAR(6)) + 
					' AND AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
					' AND AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
					' AND TrnCode = ' + CAST(@TrnCode AS VARCHAR(8)) + 
					' AND (TrnDate' + ' BETWEEN ' + '''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' + ')'
			
			EXECUTE (@strSQL);

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

		SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZCSMCUS..A2ZTRANSACTION ' + 
				' WHERE CuType = ' + CAST(@CuType AS VARCHAR(2))  + 
				' AND CuNo = ' + CAST(@CuNo AS VARCHAR(6)) + 
				' AND MemNo = ' + CAST(@MemNo AS VARCHAR(6)) + 
				' AND AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
				' AND AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
				' AND TrnCode = ' + CAST(@TrnCode AS VARCHAR(8)) +
				' AND (TrnDate' + ' BETWEEN ' + '''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' + ')'
		
		EXECUTE (@strSQL);

--	EXECUTE Sp_CSGenerateTransactionDataSingle @CuType, @CuNo, @MemNo, @TrnCode, @AccType, @AccNo, @opDate,@fDate,0;

	SET @strSQL = 'DELETE FROM #WFA2ZTRANSACTION WHERE TrnDate = ''' + @fDate + '''';
	EXECUTE (@strSQL);
--==========  Get Transaction Data For Opening Balance ==========

---------- Credit
			UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = #A2ZACCOUNT.AccOpBal + 
            ISNULL((SELECT SUM(TrnCredit) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = @CuType AND #WFA2ZTRANSACTION.CuNo = @CuNo AND 
			#WFA2ZTRANSACTION.MemNo = @MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = @AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 1),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType = @CuType AND #A2ZACCOUNT.CuNo = @CuNo AND 
			#A2ZACCOUNT.MemNo = @MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = @AccNo;
---------- Debit
            UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = #A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(TrnDebit) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = @CuType AND #WFA2ZTRANSACTION.CuNo = @CuNo AND 
			#WFA2ZTRANSACTION.MemNo = @MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = @AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=@CuType AND #A2ZACCOUNT.CuNo = @CuNo AND 
			#A2ZACCOUNT.MemNo = @MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = @AccNo;

    END

     IF @ReadFlag = 0
           BEGIN
              UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = #A2ZACCOUNT.AccTodaysOpBalance           
              FROM #A2ZACCOUNT;    
           END  


--===========  End of For Opening Balance ========================

--===============  Find Out Debit Credit Amount ============
	
	SET @opBalance = (SELECT AccOpBal FROM #A2ZACCOUNT WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo);

	SET @debitAmt = 0;
	SET @creditAmt = 0;
	
	IF @opBalance > 0
	   BEGIN
			SET @creditAmt = ABS(@opBalance);
	   END
	ELSE
		BEGIN
			SET @debitAmt = ABS(@opBalance);
		END
	
	--=============  Find Out Debit Credit Amount ============
	DROP TABLE #A2ZACCOUNT;
	TRUNCATE TABLE #WFA2ZTRANSACTION;

	SELECT * INTO #WFCSSTATEMENT FROM WFCSSTATEMENT WHERE ID = 0;
	TRUNCATE TABLE #WFCSSTATEMENT;

	INSERT INTO #WFCSSTATEMENT (TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnDebit,TrnCredit,TrnDesc,TrnType,ShowInterest,TrnProcStat,TrnCSGL,TrnFlag,FuncOpt)
	VALUES (@fDate,@CuType,@CuNo,@MemNo,@AccType,@AccNo,@debitAmt,@creditAmt,'=== Opening Balance ===',0,0,0,0,0,0)
		
--=========== Get Transaction Data For Account Statement ================
	SET @fYear = LEFT(@fDate,4);
	SET @tYear = LEFT(@tDate,4);

	SET @nCount = @fYear
	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION ' +
					' WHERE CuType = ' + CAST(@CuType AS VARCHAR(2))  + 
					' AND CuNo = ' + CAST(@CuNo AS VARCHAR(6)) + 
					' AND MemNo = ' + CAST(@MemNo AS VARCHAR(6)) + 
					' AND AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
					' AND AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
					' AND TrnCode = ' + CAST(@TrnCode AS VARCHAR(8)) + 
					' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'
			
			EXECUTE (@strSQL);

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

		SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZCSMCUS..A2ZTRANSACTION ' + 
				' WHERE CuType = ' + CAST(@CuType AS VARCHAR(2))  + 
				' AND CuNo = ' + CAST(@CuNo AS VARCHAR(6)) + 
				' AND MemNo = ' + CAST(@MemNo AS VARCHAR(6)) + 
				' AND AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
				' AND AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
				' AND TrnCode = ' + CAST(@TrnCode AS VARCHAR(8)) +
				' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'
		
		EXECUTE (@strSQL);
--=========== End of Get Transaction Data For Account Statement ================

--	EXECUTE Sp_CSGenerateTransactionDataSingle @CuType,@CuNo,@MemNo,@TrnCode,@AccType,@AccNo, @fDate,@tDate,0;

	INSERT INTO #WFCSSTATEMENT (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,
	UserID,CreateDate)
	SELECT
	BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,
	UserID,CreateDate
	FROM #WFA2ZTRANSACTION WHERE #WFA2ZTRANSACTION.TrnFlag = 0;

	IF @nFlag < 2
		BEGIN
			DELETE FROM #WFCSSTATEMENT WHERE ShowInterest = 1;
		END

	TRUNCATE TABLE #WFA2ZTRANSACTION;

--============  Using Query ==============
--- @nFlag = 0 = Normal CS Account Statement
	IF @nFlag = 0
		BEGIN
			SELECT #WFCSSTATEMENT.CuType, #WFCSSTATEMENT.MemNo, A2ZMEMBER.MemName, #WFCSSTATEMENT.VchNo, #WFCSSTATEMENT.VoucherNo, 
			#WFCSSTATEMENT.AccType, #WFCSSTATEMENT.TrnDate, #WFCSSTATEMENT.TrnCredit, #WFCSSTATEMENT.TrnDebit, #WFCSSTATEMENT.TrnDesc, 
			#WFCSSTATEMENT.AccNo, #WFCSSTATEMENT.TrnType, A2ZTRNTYPE.TrnTypeDes, #WFCSSTATEMENT.PayType, A2ZPAYTYPE.PayTypeDes, 
			#WFCSSTATEMENT.TrnFlag, #WFCSSTATEMENT.TrnCSGL, #WFCSSTATEMENT.UserID, A2ZACCTYPE.AccTypeDescription, 
			#WFCSSTATEMENT.ShowInterest, #WFCSSTATEMENT.TrnProcStat, A2ZACCOUNT.AccAnniDate, #WFCSSTATEMENT.ValueDate,
			#WFCSSTATEMENT.FuncOpt, #WFCSSTATEMENT.TrnChqNo
			FROM A2ZTRNTYPE RIGHT OUTER JOIN
			#WFCSSTATEMENT LEFT OUTER JOIN
			A2ZACCOUNT ON #WFCSSTATEMENT.AccType = A2ZACCOUNT.AccType AND #WFCSSTATEMENT.CuNo = A2ZACCOUNT.CuNo AND 
			#WFCSSTATEMENT.CuType = A2ZACCOUNT.CuType AND #WFCSSTATEMENT.MemNo = A2ZACCOUNT.MemNo AND 
			#WFCSSTATEMENT.AccNo = A2ZACCOUNT.AccNo LEFT OUTER JOIN
			A2ZACCTYPE ON #WFCSSTATEMENT.AccType = A2ZACCTYPE.AccTypeCode ON 
			A2ZTRNTYPE.TrnType = #WFCSSTATEMENT.TrnType LEFT OUTER JOIN
			A2ZPAYTYPE ON #WFCSSTATEMENT.PayType = A2ZPAYTYPE.PayType LEFT OUTER JOIN
			A2ZMEMBER ON #WFCSSTATEMENT.CuType = A2ZMEMBER.CuType AND #WFCSSTATEMENT.CuNo = A2ZMEMBER.CuNo AND 
			#WFCSSTATEMENT.MemNo = A2ZMEMBER.MemNo;
		END
--============  End of Using Query ==============
--- @nFlag = 1 = Staff - Normal CS Account Statement
	IF @nFlag = 1
		BEGIN
			SELECT #WFCSSTATEMENT.CuNo, #WFCSSTATEMENT.CuType, #WFCSSTATEMENT.MemNo, A2ZMEMBER.MemName, #WFCSSTATEMENT.VchNo,#WFCSSTATEMENT.VoucherNo, 
			#WFCSSTATEMENT.AccType, #WFCSSTATEMENT.TrnDate, #WFCSSTATEMENT.TrnCredit, #WFCSSTATEMENT.TrnDebit, #WFCSSTATEMENT.TrnDesc, 
			#WFCSSTATEMENT.AccNo, #WFCSSTATEMENT.TrnType, A2ZTRNTYPE.TrnTypeDes, #WFCSSTATEMENT.PayType, A2ZPAYTYPE.PayTypeDes, 
			#WFCSSTATEMENT.TrnFlag, #WFCSSTATEMENT.TrnCSGL, #WFCSSTATEMENT.UserID, A2ZACCTYPE.AccTypeDescription, 
			#WFCSSTATEMENT.ShowInterest, #WFCSSTATEMENT.TrnProcStat, A2ZACCOUNT.AccAnniDate
			FROM A2ZACCTYPE RIGHT OUTER JOIN
			A2ZTRNTYPE RIGHT OUTER JOIN
			A2ZACCOUNT RIGHT OUTER JOIN
			#WFCSSTATEMENT ON A2ZACCOUNT.CuType = #WFCSSTATEMENT.CuType AND A2ZACCOUNT.CuNo = #WFCSSTATEMENT.CuNo AND 
			A2ZACCOUNT.MemNo = #WFCSSTATEMENT.MemNo AND A2ZACCOUNT.AccType = #WFCSSTATEMENT.AccType AND 
			A2ZACCOUNT.AccNo = #WFCSSTATEMENT.AccNo ON A2ZTRNTYPE.TrnType = #WFCSSTATEMENT.TrnType ON 
			A2ZACCTYPE.AccTypeCode = #WFCSSTATEMENT.AccType LEFT OUTER JOIN
			A2ZPAYTYPE ON #WFCSSTATEMENT.PayType = A2ZPAYTYPE.PayType LEFT OUTER JOIN
			A2ZMEMBER ON #WFCSSTATEMENT.CuType = A2ZMEMBER.CuType AND #WFCSSTATEMENT.CuNo = A2ZMEMBER.CuNo AND 
			#WFCSSTATEMENT.MemNo = A2ZMEMBER.MemNo
			WHERE (#WFCSSTATEMENT.TrnDate BETWEEN @fDate AND @tDate) AND (#WFCSSTATEMENT.TrnFlag = 0) AND (#WFCSSTATEMENT.TrnCSGL = 0) AND 
			(#WFCSSTATEMENT.CuNo = @CuNo) AND (#WFCSSTATEMENT.CuType = @CuType) AND (#WFCSSTATEMENT.MemNo = @MemNo) AND (#WFCSSTATEMENT.AccType =  @AccType) AND 
			(#WFCSSTATEMENT.AccNo = @AccNo) AND (#WFCSSTATEMENT.ShowInterest <> 1 AND #WFCSSTATEMENT.TrnProcStat <> 1);
		END

--- @nFlag = 2 = Interst Penalty - Normal CS Account Statement
	IF @nFlag = 2
		BEGIN
			SELECT #WFCSSTATEMENT.CuNo, #WFCSSTATEMENT.CuType, #WFCSSTATEMENT.MemNo, A2ZMEMBER.MemName, #WFCSSTATEMENT.VchNo,#WFCSSTATEMENT.VoucherNo,  
			#WFCSSTATEMENT.AccType, #WFCSSTATEMENT.TrnDate, #WFCSSTATEMENT.TrnCredit, #WFCSSTATEMENT.TrnDebit, #WFCSSTATEMENT.TrnDesc, 
			#WFCSSTATEMENT.AccNo, #WFCSSTATEMENT.TrnType, A2ZTRNTYPE.TrnTypeDes, #WFCSSTATEMENT.PayType, A2ZPAYTYPE.PayTypeDes, 
			#WFCSSTATEMENT.TrnFlag, #WFCSSTATEMENT.TrnCSGL, #WFCSSTATEMENT.UserID, A2ZACCTYPE.AccTypeDescription, 
			#WFCSSTATEMENT.ShowInterest, #WFCSSTATEMENT.TrnProcStat, #WFCSSTATEMENT.TrnInterestAmt, #WFCSSTATEMENT.TrnPenalAmt,
			#WFCSSTATEMENT.FuncOpt
			FROM A2ZPAYTYPE RIGHT OUTER JOIN
			A2ZACCTYPE RIGHT OUTER JOIN
			A2ZTRNTYPE RIGHT OUTER JOIN
			#WFCSSTATEMENT ON A2ZTRNTYPE.TrnType = #WFCSSTATEMENT.TrnType ON A2ZACCTYPE.AccTypeCode = #WFCSSTATEMENT.AccType ON 
			A2ZPAYTYPE.PayType = #WFCSSTATEMENT.PayType LEFT OUTER JOIN
			A2ZMEMBER ON #WFCSSTATEMENT.CuType = A2ZMEMBER.CuType AND #WFCSSTATEMENT.CuNo = A2ZMEMBER.CuNo AND 
			#WFCSSTATEMENT.MemNo = A2ZMEMBER.MemNo
			WHERE (#WFCSSTATEMENT.TrnDate BETWEEN @fDate AND @tDate) AND (#WFCSSTATEMENT.TrnFlag = 0) AND (#WFCSSTATEMENT.TrnCSGL = 0) AND 
			(#WFCSSTATEMENT.CuNo = @CuNo) AND (#WFCSSTATEMENT.CuType = @CuType) AND (#WFCSSTATEMENT.MemNo = @MemNo) AND 
			(#WFCSSTATEMENT.AccType = @AccType) AND (#WFCSSTATEMENT.ShowInterest <> 1) AND 
			(#WFCSSTATEMENT.TrnProcStat <> 1);
		END

--- @nFlag = 3 = Interest Penalty - Staff - Normal CS Account Statement
	IF @nFlag = 3
		BEGIN
			SELECT #WFCSSTATEMENT.CuNo, #WFCSSTATEMENT.CuType, #WFCSSTATEMENT.MemNo, A2ZMEMBER.MemName, #WFCSSTATEMENT.VchNo,#WFCSSTATEMENT.VoucherNo,  
			#WFCSSTATEMENT.AccType, #WFCSSTATEMENT.TrnDate, #WFCSSTATEMENT.TrnCredit, #WFCSSTATEMENT.TrnDebit, #WFCSSTATEMENT.TrnDesc, 
			#WFCSSTATEMENT.AccNo, #WFCSSTATEMENT.TrnType, A2ZTRNTYPE.TrnTypeDes, #WFCSSTATEMENT.PayType, A2ZPAYTYPE.PayTypeDes, 
			#WFCSSTATEMENT.TrnFlag, #WFCSSTATEMENT.TrnCSGL, #WFCSSTATEMENT.UserID, A2ZACCTYPE.AccTypeDescription, 
			#WFCSSTATEMENT.ShowInterest, #WFCSSTATEMENT.TrnProcStat, #WFCSSTATEMENT.TrnInterestAmt, #WFCSSTATEMENT.TrnPenalAmt,
			#WFCSSTATEMENT.FuncOpt
			FROM A2ZPAYTYPE RIGHT OUTER JOIN
			A2ZACCTYPE RIGHT OUTER JOIN
			A2ZTRNTYPE RIGHT OUTER JOIN
			#WFCSSTATEMENT ON A2ZTRNTYPE.TrnType = #WFCSSTATEMENT.TrnType ON A2ZACCTYPE.AccTypeCode = #WFCSSTATEMENT.AccType ON 
			A2ZPAYTYPE.PayType = #WFCSSTATEMENT.PayType LEFT OUTER JOIN
			A2ZMEMBER ON #WFCSSTATEMENT.CuType = A2ZMEMBER.CuType AND #WFCSSTATEMENT.CuNo = A2ZMEMBER.CuNo AND 
			#WFCSSTATEMENT.MemNo = A2ZMEMBER.MemNo
			WHERE (#WFCSSTATEMENT.TrnDate BETWEEN @fDate AND @tDate) AND (#WFCSSTATEMENT.TrnFlag = 0) AND (#WFCSSTATEMENT.TrnCSGL = 0) AND 
			(#WFCSSTATEMENT.CuNo = @CuNo) AND (#WFCSSTATEMENT.CuType = @CuType) AND (#WFCSSTATEMENT.MemNo = @MemNo) AND 
			(#WFCSSTATEMENT.AccType = @AccType) AND (#WFCSSTATEMENT.ShowInterest <> 1) AND 
			(#WFCSSTATEMENT.TrnProcStat <> 1);
		END

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

