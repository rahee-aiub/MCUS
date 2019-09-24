USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_CSGenerateUptoDateLB]    Script Date: 05/31/2018 10:48:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[SpM_CSGenerateUptoDateLB](@AccType INT, @fDate VARCHAR(10), @CuType INT, @CuNo INT,
@AccStatFlag INT,@BalanceFlag INT, @fBalance MONEY, @tBalance MONEY, @nFlag INT,@CodeType INT)
AS
/*

EXECUTE SpM_CSGenerateUptoDateLB 56,'2017-12-31',0,0,0,0,0,0,0,0


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

    EXECUTE SpM_CSGenerateLB @AccType, @fDate;



    SELECT * INTO #A2ZACCOUNT FROM A2ZACCOUNT WHERE AccType = @AccType;
	UPDATE #A2ZACCOUNT SET AccBalance = 0, AccDRBal = 0, AccCRBal = 0, AccCloBal = 0; 

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


    SET @strSQL = 'UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccBalance = ' +
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


---------------------------------------------------------------------------------------

         

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
			UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccCRBal = #A2ZACCOUNT.AccCRBal + 
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
            UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccDRBal = #A2ZACCOUNT.AccDRBal + 
			ISNULL((SELECT SUM(GLDebitAmt) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.CuType = #A2ZACCOUNT.CuType AND #WFA2ZTRANSACTION.CuNo = #A2ZACCOUNT.CuNo AND 
			#WFA2ZTRANSACTION.MemNo = #A2ZACCOUNT.MemNo AND #WFA2ZTRANSACTION.AccType = @AccType AND 
			#WFA2ZTRANSACTION.AccNo = #A2ZACCOUNT.AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 0 AND #WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM #A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE #A2ZACCOUNT.CuType=#WFA2ZTRANSACTION.CuType AND #A2ZACCOUNT.CuNo = #WFA2ZTRANSACTION.CuNo AND 
			#A2ZACCOUNT.MemNo = #WFA2ZTRANSACTION.MemNo AND #A2ZACCOUNT.AccType = @AccType AND 
			#A2ZACCOUNT.AccNo = #WFA2ZTRANSACTION.AccNo;





------------------------------------------------------------------------------------------------------------------


     UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.AccCloBal = ((#A2ZACCOUNT.AccBalance + #A2ZACCOUNT.AccCRBal) - #A2ZACCOUNT.AccDRBal)           
     FROM #A2ZACCOUNT        
	 WHERE #A2ZACCOUNT.AccType = @AccType;




----- Generate Work Table for Ledger Balance
	SELECT * INTO #WFCSLEDGERBALANCE FROM WFCSLEDGERBALANCE WHERE ID = 0;
	TRUNCATE TABLE #WFCSLEDGERBALANCE;
	
	INSERT INTO #WFCSLEDGERBALANCE (CuType,CuNo,MemNo,AccNo,OpenDate,LastTrnDate,Balance,AccType,AccStatus,AccStatusDate,AccOpBal,AccDRBal,AccCRBal,AccCloBal)
	SELECT CuType,CuNo,MemNo,AccNo,AccOpenDate,AccLastTrnDateU,AccOpBal,AccType,AccStatus,AccStatusDate,AccBalance,AccDRBal,AccCRBal,AccCloBal
	FROM #A2ZACCOUNT WHERE AccType = @AccType;

   
	DROP TABLE #A2ZACCOUNT;
	DROP TABLE #WFA2ZTRANSACTION;

	IF @CuType > 0
		BEGIN
			DELETE FROM #WFCSLEDGERBALANCE WHERE CuType <> @CuType;
			DELETE FROM #WFCSLEDGERBALANCE WHERE CuNo <> @CuNo;
		END

   
    DELETE FROM #WFCSLEDGERBALANCE WHERE AccStatus = 98;

    --UPDATE #WFCSLEDGERBALANCE SET AccStatus = 1 WHERE #WFCSLEDGERBALANCE.AccStatus <> 1 AND @fDate < AccStatusDate;

--	IF @AccStatFlag = 0
--		BEGIN
--			DELETE FROM #WFCSLEDGERBALANCE WHERE AccStatus > 97;
----            DELETE FROM #WFCSLEDGERBALANCE WHERE AccStatus > 97 AND AccStatusDate <= @fDate;
--		END

--    IF @AccStatFlag > 0
--		BEGIN
--			DELETE FROM #WFCSLEDGERBALANCE WHERE AccStatus <> @AccStatFlag;       
--		END

--    IF @AccStatFlag = 0 
--		BEGIN
--			UPDATE #WFCSLEDGERBALANCE SET AccStatus = 1 WHERE #WFCSLEDGERBALANCE.AccStatus = 99;
--		END
    
	IF @BalanceFlag = 2
		BEGIN
			DELETE FROM #WFCSLEDGERBALANCE WHERE ABS(AccOpBal) != 0 OR ABS(AccDRBal) != 0 OR  ABS(AccCRBal) != 0;
		END



	IF @BalanceFlag = 1
		BEGIN
			DELETE FROM #WFCSLEDGERBALANCE WHERE ABS(AccOpBal) = 0 AND ABS(AccDRBal) = 0 AND  ABS(AccCRBal) = 0;
		END


--     ELSE
--     IF @BalanceFlag = 1 and (@acctypeclass != 5 AND @acctypeclass != 6)
--		BEGIN
----			DELETE FROM #WFCSLEDGERBALANCE WHERE Balance < @fBalance;
----			DELETE FROM #WFCSLEDGERBALANCE WHERE Balance > @tBalance;
--            IF @fBalance < 0
--               BEGIN           
--                    DELETE FROM #WFCSLEDGERBALANCE WHERE Balance NOT BETWEEN @tBalance AND @fBalance; 
--               END
--            ELSE
--               BEGIN           
--                    DELETE FROM #WFCSLEDGERBALANCE WHERE Balance NOT BETWEEN @fBalance AND @tBalance; 
--               END

--		END

  --  IF @CodeType > 0
  --     BEGIN
		--	DELETE FROM #WFCSLEDGERBALANCE WHERE right(AccNo, 4) !=@CodeType;
		--END
            
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

