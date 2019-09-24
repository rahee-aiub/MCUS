USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[SpM_CSConvertLedgerBalance]    Script Date: 07/14/2017 14:41:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SpM_CSConvertLedgerBalance](@fDate VARCHAR(10), @CuType INT, @CuNo INT,
@AccStatFlag INT,@BalanceFlag INT, @fBalance MONEY, @tBalance MONEY, @nFlag INT,@CodeType INT)
AS
/*

EXECUTE SpM_CSConvertLedgerBalance '2017-06-29',0,0,0,0,0,0,0,0




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

	SELECT * INTO #A2ZACCOUNT FROM A2ZACCOUNT;
	UPDATE #A2ZACCOUNT SET AccOpBal = 0; 

	SET @nDate = @fDate;
    
    SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZCSPARAMETER);

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
		' AND #A2ZACCOUNT.AccType = A2ZCSOPBALANCE.AccType' +  
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
					' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''';
			
			EXECUTE (@strSQL);

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

--		SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
--				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
--				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
--				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate)' +
--				' SELECT ' +
--				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
--				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
--				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
--				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate' +
--				' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE TrnDate' +
--				' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' + ' AND AccType = ' + CAST(@AccType AS VARCHAR(2));
--		
--		EXECUTE (@strSQL);



---------- Credit
			UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = #A2ZACCOUNT.AccOpBal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = #A2ZACCOUNT.AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 1 AND #WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = #WFA2ZTRANSACTION.AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;
---------- Debit
            UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = #A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = #A2ZACCOUNT.AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 0 AND #WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = #WFA2ZTRANSACTION.AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;
--		END		

----- Generate Work Table for Ledger Balance
	SELECT * INTO #WFCSLEDGERBALANCE FROM WFCSLEDGERBALANCE WHERE ID = 0;
	TRUNCATE TABLE #WFCSLEDGERBALANCE;
	
	INSERT INTO #WFCSLEDGERBALANCE (CuType,CuNo,MemNo,AccNo,OpenDate,LastTrnDate,Balance,AccType,AccStatus,AccStatusDate)
	SELECT CuType,CuNo,MemNo,AccNo,AccOpenDate,AccLastTrnDateU,AccOpBal,AccType,AccStatus,AccStatusDate
    FROM #A2ZACCOUNT;
	
	DROP TABLE #A2ZACCOUNT;
	DROP TABLE #WFA2ZTRANSACTION;
  

    

    UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccTodaysOpBalance = #WFCSLEDGERBALANCE.Balance 
    FROM A2ZACCOUNT,#WFCSLEDGERBALANCE
    WHERE A2ZACCOUNT.CuType = #WFCSLEDGERBALANCE.CuType AND A2ZACCOUNT.CuNo = #WFCSLEDGERBALANCE.CuNo AND 
	A2ZACCOUNT.MemNo = #WFCSLEDGERBALANCE.MemNo AND A2ZACCOUNT.AccType = #WFCSLEDGERBALANCE.AccType AND
    A2ZACCOUNT.AccNo = #WFCSLEDGERBALANCE.AccNo; 



END




