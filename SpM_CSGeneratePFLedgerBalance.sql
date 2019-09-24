
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SpM_CSGeneratePFLedgerBalance](@AccType INT, @fDate VARCHAR(10), @CuType INT, @CuNo INT,
@AccStatFlag INT,@BalanceFlag INT, @fBalance MONEY, @tBalance MONEY, @nFlag INT,@CodeType INT)
AS
/*

EXECUTE SpM_CSGeneratePFLedgerBalance 51,'2016-01-31',0,0,0,0,0,0,0,0


@CuType = 0 = All Credit Union, Value = Credit Union
@CuType = Value = @CuNo = Value

@AccStatFlag = 0 = All Account Status, Value = Account Status

@BalanceFlag = 0 = All Balance
@BalanceFlag = 1 = Only Zero Balance
@BalanceFlag = 2 = Check Balance

@BalanceFlag = 2 = Check Balance = Between @fBalance and @tBalance

*/

BEGIN

	SET NOCOUNT ON;

---=============  DECLARATION ===============
	DECLARE @trnDate SMALLDATETIME;
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);
    DECLARE @acctypeclass int;

    DECLARE @BegYear int;
    DECLARE @IYear int;

	DECLARE @nYear INT;	

	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @nCount INT;
---============= END OF DECLARATION ===============

	SELECT * INTO #A2ZACCOUNT FROM A2ZACCOUNT WHERE AccType = @AccType;
	UPDATE #A2ZACCOUNT SET AccOpBal = 0,
                           AccOfficeOpBal = 0, 
                           AccOwnOpBal = 0, 
                           AccOpInt = 0; 

	SET @nDate = @fDate;
    
    SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZCSPARAMETER);

    SET @IYear = YEAR(@fDate);


    SET @acctypeclass = (SELECT AccTypeClass FROM A2ZACCTYPE WHERE AccTypeCode=@AccType);


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


	SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZCSOPBALANCE';

    SET @strSQL = 'UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = ' +
		'ISNULL((SELECT SUM(TrnAmount) FROM ' + @openTable +  
		' WHERE A2ZCSOPBALANCE.CuType = #A2ZACCOUNT.CuType ' + 
		' AND A2ZCSOPBALANCE.CuNo = #A2ZACCOUNT.CuNo' +  
		' AND A2ZCSOPBALANCE.MemNo = #A2ZACCOUNT.MemNo' + 
		' AND A2ZCSOPBALANCE.AccType = #A2ZACCOUNT.AccType' + 
		' AND A2ZCSOPBALANCE.AccNo = #A2ZACCOUNT.AccNo' + 
		'),0) FROM #A2ZACCOUNT,' + @openTable +
		' WHERE #A2ZACCOUNT.CuType = A2ZCSOPBALANCE.CuType'  +  
		' AND #A2ZACCOUNT.CuNo = A2ZCSOPBALANCE.CuNo' + 
		' AND #A2ZACCOUNT.MemNo = A2ZCSOPBALANCE.MemNo' +  
		' AND #A2ZACCOUNT.AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
		' AND #A2ZACCOUNT.AccNo = A2ZCSOPBALANCE.AccNo';

	EXECUTE (@strSQL);

    SET @strSQL = 'UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOfficeOpBal = ' +
		'ISNULL((SELECT SUM(TrnOfficeAmt) FROM ' + @openTable +  
		' WHERE A2ZCSOPBALANCE.CuType = #A2ZACCOUNT.CuType ' + 
		' AND A2ZCSOPBALANCE.CuNo = #A2ZACCOUNT.CuNo' +  
		' AND A2ZCSOPBALANCE.MemNo = #A2ZACCOUNT.MemNo' + 
		' AND A2ZCSOPBALANCE.AccType = #A2ZACCOUNT.AccType' + 
		' AND A2ZCSOPBALANCE.AccNo = #A2ZACCOUNT.AccNo' + 
		'),0) FROM #A2ZACCOUNT,' + @openTable +
		' WHERE #A2ZACCOUNT.CuType = A2ZCSOPBALANCE.CuType'  +  
		' AND #A2ZACCOUNT.CuNo = A2ZCSOPBALANCE.CuNo' + 
		' AND #A2ZACCOUNT.MemNo = A2ZCSOPBALANCE.MemNo' +  
		' AND #A2ZACCOUNT.AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
		' AND #A2ZACCOUNT.AccNo = A2ZCSOPBALANCE.AccNo';

	EXECUTE (@strSQL);

    SET @strSQL = 'UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOwnOpBal = ' +
		'ISNULL((SELECT SUM(TrnOwnAmt) FROM ' + @openTable +  
		' WHERE A2ZCSOPBALANCE.CuType = #A2ZACCOUNT.CuType ' + 
		' AND A2ZCSOPBALANCE.CuNo = #A2ZACCOUNT.CuNo' +  
		' AND A2ZCSOPBALANCE.MemNo = #A2ZACCOUNT.MemNo' + 
		' AND A2ZCSOPBALANCE.AccType = #A2ZACCOUNT.AccType' + 
		' AND A2ZCSOPBALANCE.AccNo = #A2ZACCOUNT.AccNo' + 
		'),0) FROM #A2ZACCOUNT,' + @openTable +
		' WHERE #A2ZACCOUNT.CuType = A2ZCSOPBALANCE.CuType'  +  
		' AND #A2ZACCOUNT.CuNo = A2ZCSOPBALANCE.CuNo' + 
		' AND #A2ZACCOUNT.MemNo = A2ZCSOPBALANCE.MemNo' +  
		' AND #A2ZACCOUNT.AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
		' AND #A2ZACCOUNT.AccNo = A2ZCSOPBALANCE.AccNo';

	EXECUTE (@strSQL);

    SET @strSQL = 'UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpInt = ' +
		'ISNULL((SELECT SUM(TrnInterestAmt) FROM ' + @openTable +  
		' WHERE A2ZCSOPBALANCE.CuType = #A2ZACCOUNT.CuType ' + 
		' AND A2ZCSOPBALANCE.CuNo = #A2ZACCOUNT.CuNo' +  
		' AND A2ZCSOPBALANCE.MemNo = #A2ZACCOUNT.MemNo' + 
		' AND A2ZCSOPBALANCE.AccType = #A2ZACCOUNT.AccType' + 
		' AND A2ZCSOPBALANCE.AccNo = #A2ZACCOUNT.AccNo' + 
		'),0) FROM #A2ZACCOUNT,' + @openTable +
		' WHERE #A2ZACCOUNT.CuType = A2ZCSOPBALANCE.CuType'  +  
		' AND #A2ZACCOUNT.CuNo = A2ZCSOPBALANCE.CuNo' + 
		' AND #A2ZACCOUNT.MemNo = A2ZCSOPBALANCE.MemNo' +  
		' AND #A2ZACCOUNT.AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
		' AND #A2ZACCOUNT.AccNo = A2ZCSOPBALANCE.AccNo';

	EXECUTE (@strSQL);

--============  For Opening Balance ==================
	SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE ID = 0;
	TRUNCATE TABLE #WFA2ZTRANSACTION;

	SET @fYear = LEFT(@opDate,4);
	SET @tYear = LEFT(@fDate,4);

	SET @nCount = @fYear
	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE TrnDate' +
					' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' + ' AND AccType = ' + CAST(@AccType AS VARCHAR(2));
			
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
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE TrnDate' +
				' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' + ' AND AccType = ' + CAST(@AccType AS VARCHAR(2));
		
		EXECUTE (@strSQL);

--============  End of For Opening Balance ==================

--	IF MONTH(@nDate) <> 7
--		BEGIN
--			EXECUTE Sp_CSGenerateTransactionDataAType @AccType,@opDate,@fDate,0;
---------- Credit
			UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = #A2ZACCOUNT.AccOpBal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 1 AND #WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;
---------- Debit
            UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = #A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 0 AND #WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;
--		END		

---------- Credit Office
			UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOfficeOpBal = #A2ZACCOUNT.AccOfficeOpBal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.PayType = 10 AND #WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 1 AND #WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;
---------- Debit Office
            UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOfficeOpBal = #A2ZACCOUNT.AccOfficeOpBal - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.PayType = 10 AND #WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 0 AND #WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;

---------- Credit Own
			UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOwnOpBal = #A2ZACCOUNT.AccOwnOpBal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.PayType = 11 AND #WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 1 AND #WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;
---------- Debit Own
            UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOwnOpBal = #A2ZACCOUNT.AccOwnOpBal - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.PayType = 11 AND #WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 0 AND #WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;

---------- Credit Int
			UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpInt = #A2ZACCOUNT.AccOpInt + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.PayType = 13 AND #WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 1 AND #WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;
---------- Debit Int
            UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpInt = #A2ZACCOUNT.AccOpInt - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.PayType = 13 AND #WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 0 AND #WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;



----- Generate Work Table for Ledger Balance
	SELECT * INTO #WFCSLEDGERBALANCE FROM WFCSLEDGERBALANCE WHERE ID = 0;
	TRUNCATE TABLE #WFCSLEDGERBALANCE;
	
	INSERT INTO #WFCSLEDGERBALANCE (CuType,CuNo,MemNo,AccNo,OpenDate,LastTrnDate,Balance,OfficeBalance,OwnBalance,IntBalance,AccType,AccStatus,AccStatusDate)
	SELECT CuType,CuNo,MemNo,AccNo,AccOpenDate,AccLastTrnDateU,AccOpBal,AccOfficeOpBal,AccOwnOpBal,AccOpInt,AccType,AccStatus,AccStatusDate
	FROM #A2ZACCOUNT WHERE AccType = @AccType;

	DROP TABLE #A2ZACCOUNT;
	DROP TABLE #WFA2ZTRANSACTION;

	IF @CuType > 0
		BEGIN
			DELETE FROM #WFCSLEDGERBALANCE WHERE CuType <> @CuType;
			DELETE FROM #WFCSLEDGERBALANCE WHERE CuNo <> @CuNo;
		END


    UPDATE #WFCSLEDGERBALANCE SET AccStatus = 1 WHERE #WFCSLEDGERBALANCE.AccStatus <> 1 AND @fDate < AccStatusDate;

	IF @AccStatFlag = 0
		BEGIN
			DELETE FROM #WFCSLEDGERBALANCE WHERE AccStatus > 97;
--            DELETE FROM #WFCSLEDGERBALANCE WHERE AccStatus > 97 AND AccStatusDate <= @fDate;
		END

    IF @AccStatFlag > 0
		BEGIN
			DELETE FROM #WFCSLEDGERBALANCE WHERE AccStatus <> @AccStatFlag;       
		END

--    IF @AccStatFlag = 0 
--		BEGIN
--			UPDATE #WFCSLEDGERBALANCE SET AccStatus = 1 WHERE #WFCSLEDGERBALANCE.AccStatus = 99;
--		END
    
	IF @BalanceFlag = 2
		BEGIN
			DELETE FROM #WFCSLEDGERBALANCE WHERE Balance !=0;
		END

	IF @BalanceFlag = 1 and (@acctypeclass = 5 or @acctypeclass = 6)
		BEGIN
--			DELETE FROM #WFCSLEDGERBALANCE WHERE ABS(Balance) < @fBalance;
--			DELETE FROM #WFCSLEDGERBALANCE WHERE ABS(Balance) > @tBalance;

            IF @fBalance < 0
               BEGIN           
                    DELETE FROM #WFCSLEDGERBALANCE WHERE Balance NOT BETWEEN @tBalance AND @fBalance; 
               END
            ELSE
               BEGIN           
                    DELETE FROM #WFCSLEDGERBALANCE WHERE Balance NOT BETWEEN @fBalance AND @tBalance; 
               END


		END
     ELSE
     IF @BalanceFlag = 1 and (@acctypeclass != 5 AND @acctypeclass != 6)
		BEGIN
--			DELETE FROM #WFCSLEDGERBALANCE WHERE Balance < @fBalance;
--			DELETE FROM #WFCSLEDGERBALANCE WHERE Balance > @tBalance;
            IF @fBalance < 0
               BEGIN           
                    DELETE FROM #WFCSLEDGERBALANCE WHERE Balance NOT BETWEEN @tBalance AND @fBalance; 
               END
            ELSE
               BEGIN           
                    DELETE FROM #WFCSLEDGERBALANCE WHERE Balance NOT BETWEEN @fBalance AND @tBalance; 
               END

		END

    IF @CodeType > 0
       BEGIN
			DELETE FROM #WFCSLEDGERBALANCE WHERE right(AccNo, 4) !=@CodeType;
		END
            
----- End of Generate Work Table for Ledger Balance

----- Update Member Name/Status Description --------
	UPDATE #WFCSLEDGERBALANCE SET MemName = (SELECT A2ZMEMBER.MemName FROM A2ZMEMBER WHERE 
	A2ZMEMBER.CuType = #WFCSLEDGERBALANCE.CuType AND A2ZMEMBER.CuNo = #WFCSLEDGERBALANCE.CuNo AND 
	A2ZMEMBER.MemNo = #WFCSLEDGERBALANCE.MemNo);
    
    UPDATE #WFCSLEDGERBALANCE SET GLCashCode = (SELECT A2ZCUNION.GLCashCode FROM A2ZCUNION WHERE 
	A2ZCUNION.CuType = #WFCSLEDGERBALANCE.CuType AND A2ZCUNION.CuNo = #WFCSLEDGERBALANCE.CuNo);

	UPDATE #WFCSLEDGERBALANCE SET Status = (SELECT AccStatusDescription FROM A2ZACCSTATUS WHERE
	#WFCSLEDGERBALANCE.AccStatus = A2ZACCSTATUS.AccStatusCode);

----- End of Update Member Name/Status Description --------

	SELECT * FROM #WFCSLEDGERBALANCE ORDER BY CuType,CuNo,MemNo;

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

