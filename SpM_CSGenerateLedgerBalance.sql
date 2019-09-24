USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_CSGenerateLedgerBalance]    Script Date: 05/29/2018 2:21:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[SpM_CSGenerateLedgerBalance](@AccType INT, @fDate VARCHAR(10), @CuType INT, @CuNo INT,
@AccStatFlag INT,@BalanceFlag INT, @fBalance MONEY, @tBalance MONEY, @nFlag INT,@CodeType INT)
AS
/*

EXECUTE SpM_CSGenerateLedgerBalance 11,'2017-06-04',0,0,0,0,0,0,0,0


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
    DECLARE @tDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);
    DECLARE @acctypeclass int;

    DECLARE @BegYear int;
    DECLARE @IYear int;

    DECLARE @fYear int;
    
	DECLARE @nYear INT;	
    DECLARE @pYear INT;	
	
	DECLARE @tYear INT;
	DECLARE @nCount INT;

    DECLARE @ReadFlag INT;

    DECLARE @tmm INT;
    DECLARE @tdd INT;

    DECLARE @xFlag INT;
    
    
---============= END OF DECLARATION ===============

    SELECT * INTO #A2ZACCOUNT FROM A2ZACCOUNT WHERE AccType = @AccType;
	UPDATE #A2ZACCOUNT SET AccOpBal = 0; 

    SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE ID = 0;
	TRUNCATE TABLE #WFA2ZTRANSACTION;

    SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);
    SET @pYear = YEAR(@trnDate);
    SET @fYear = LEFT(@fDate,4);

    SET @xFlag = 2;

    IF @pYear <> @fYear
       BEGIN
          SET @xFlag = 1;
       END

    

----- ********************************************************************************

    IF @xFlag = 1
       BEGIN


    SET @nDate = @fDate;
   

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

    END


----************************************************************************************
	

    IF @xFlag = 2
       BEGIN 

    IF @fDate = @trnDate
       BEGIN
            SET @ReadFlag = 0;
       END
    ELSE
       BEGIN
            SET @ReadFlag = 1;
       END  
    
    SET @tmm = MONTH(@trnDate);
    SET @tdd = DAY(@trnDate);

    IF @tmm < 10 AND @tdd < 10
       BEGIN
            SET @tDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' + '0' + CAST(MONTH(@trnDate) AS VARCHAR(1)) + '-' + '0' +  CAST(DAY(@trnDate) AS VARCHAR(1));
       END

    IF @tmm < 10 AND @tdd > 9
       BEGIN
            SET @tDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' + '0' + CAST(MONTH(@trnDate) AS VARCHAR(1)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2));
       END

    IF @tmm > 9 AND @tdd < 10
       BEGIN
            SET @tDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' + CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + '0' + CAST(DAY(@trnDate) AS VARCHAR(1));
       END
    
    IF @tmm > 9 AND @tdd > 9
       BEGIN
            SET @tDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' + CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2));
       END

    
    SET @acctypeclass = (SELECT AccTypeClass FROM A2ZACCTYPE WHERE AccTypeCode=@AccType);

    
    SET @fYear = LEFT(@fDate,4);
	SET @nCount = LEFT(@tDate,4);

    IF @fYear = @nCount
       BEGIN
            SET @opDate = @fDate
       END
    ELSE
       BEGIN
            SET @opDate = CAST(@nCount AS VARCHAR(4)) + '-01-01';
       END

   
-----------------------------------------------------------------------
    IF @ReadFlag = 1
       BEGIN   

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
					  ' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @tDate + '''' + ' AND AccType = ' + CAST(@AccType AS VARCHAR(2));
			
			          EXECUTE (@strSQL);

                      SET @nCount = @nCount - 1;

                      IF @nCount > @fYear
                         BEGIN
                              SET @opDate = CAST(@nCount AS VARCHAR(4)) + '-01-01';
                         END

                      IF @nCount = @fYear
                         BEGIN
                              SET @opDate = CAST(@nCount AS VARCHAR(4)) + '-' + CAST(MONTH(@fDate) AS VARCHAR(2)) + '-' + CAST(DAY(@fDate) AS VARCHAR(2));
                         END

			          IF @nCount < @fYear
				      BEGIN
					       SET @nCount = 0;
				      END
              END

          DELETE FROM #WFA2ZTRANSACTION WHERE TrnDate = @fDate;
 
                                                 
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


            UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = #A2ZACCOUNT.AccTodaysOpBalance - #A2ZACCOUNT.AccOpBal           
            FROM #A2ZACCOUNT        
		    WHERE #A2ZACCOUNT.AccType = @AccType;

       END

----------------------------------------------------------------------

        IF @ReadFlag = 0
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
				' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE TrnDate' +
				' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' + ' AND AccType = ' + CAST(@AccType AS VARCHAR(2));
		
		EXECUTE (@strSQL);


        

--============  End of For Opening Balance ==================

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
		END		



     IF @ReadFlag = 0
       BEGIN
           UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccOpBal = #A2ZACCOUNT.AccTodaysOpBalance + #A2ZACCOUNT.AccOpBal           
           FROM #A2ZACCOUNT        
		   WHERE #A2ZACCOUNT.AccType = @AccType;
     
       END   


END


----- Generate Work Table for Ledger Balance
	SELECT * INTO #WFCSLEDGERBALANCE FROM WFCSLEDGERBALANCE WHERE ID = 0;
	TRUNCATE TABLE #WFCSLEDGERBALANCE;
	
	INSERT INTO #WFCSLEDGERBALANCE (CuType,CuNo,MemNo,AccNo,OpenDate,LastTrnDate,Balance,AccType,AccStatus,AccStatusDate)
	SELECT CuType,CuNo,MemNo,AccNo,AccOpenDate,AccLastTrnDateU,AccOpBal,AccType,AccStatus,AccStatusDate
	FROM #A2ZACCOUNT WHERE AccType = @AccType;

   
	DROP TABLE #A2ZACCOUNT;
	DROP TABLE #WFA2ZTRANSACTION;

	IF @CuType > 0
		BEGIN
			DELETE FROM #WFCSLEDGERBALANCE WHERE CuType <> @CuType;
			DELETE FROM #WFCSLEDGERBALANCE WHERE CuNo <> @CuNo;
		END


    DELETE FROM #WFCSLEDGERBALANCE WHERE AccStatus = 98;

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

