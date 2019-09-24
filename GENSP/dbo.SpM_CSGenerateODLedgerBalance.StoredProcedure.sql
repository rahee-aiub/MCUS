USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[SpM_CSGenerateODLedgerBalance]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SpM_CSGenerateODLedgerBalance](@AccType INT, @fDate VARCHAR(10), @CuType INT, @CuNo INT,
@MemNo INT,@nFlag INT)
AS
/*

EXECUTE SpM_CSGenerateODLedgerBalance 52,'2017-07-03',0,0,0,0


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
	UPDATE #A2ZACCOUNT SET AccOpBal = 0, CalFDate=NULL; 

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

----- *****************************************************************************


      UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.CalFDate =A2ZODINTDUEHST.TrnDate, #A2ZACCOUNT.AccDueIntAmt = (SELECT TOP 1 A2ZODINTDUEHST.AccODDueInt           
      FROM A2ZODINTDUEHST        
	  WHERE A2ZODINTDUEHST.CuType = #A2ZACCOUNT.CuType AND 
            A2ZODINTDUEHST.CuNo = #A2ZACCOUNT.CuNo AND 
            A2ZODINTDUEHST.MemNo = #A2ZACCOUNT.MemNo AND 
            A2ZODINTDUEHST.AccType = #A2ZACCOUNT.AccType AND 
            A2ZODINTDUEHST.AccNo = #A2ZACCOUNT.AccNo AND
            A2ZODINTDUEHST.AccDrCr = 1 AND
            A2ZODINTDUEHST.TrnDate <= @fDate ORDER BY Trndate DESC)
      FROM #A2ZACCOUNT,A2ZODINTDUEHST        
	  WHERE #A2ZACCOUNT.CuType = A2ZODINTDUEHST.CuType AND 
            #A2ZACCOUNT.CuNo = A2ZODINTDUEHST.CuNo AND 
            #A2ZACCOUNT.MemNo = A2ZODINTDUEHST.MemNo AND 
            #A2ZACCOUNT.AccType = A2ZODINTDUEHST.AccType AND 
            #A2ZACCOUNT.AccNo = A2ZODINTDUEHST.AccNo;


      UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.CalFDate =A2ZODINTDUEHST.TrnDate, #A2ZACCOUNT.AccDisbAmt = (SELECT TOP 1 A2ZODINTDUEHST.AccODDisbAmt           
      FROM A2ZODINTDUEHST        
	  WHERE A2ZODINTDUEHST.CuType = #A2ZACCOUNT.CuType AND 
            A2ZODINTDUEHST.CuNo = #A2ZACCOUNT.CuNo AND 
            A2ZODINTDUEHST.MemNo = #A2ZACCOUNT.MemNo AND 
            A2ZODINTDUEHST.AccType = #A2ZACCOUNT.AccType AND 
            A2ZODINTDUEHST.AccNo = #A2ZACCOUNT.AccNo AND
            A2ZODINTDUEHST.AccDrCr = 0 AND
            A2ZODINTDUEHST.TrnDate <= @fDate ORDER BY Trndate DESC)
      FROM #A2ZACCOUNT,A2ZODINTDUEHST        
	  WHERE #A2ZACCOUNT.CuType = A2ZODINTDUEHST.CuType AND 
            #A2ZACCOUNT.CuNo = A2ZODINTDUEHST.CuNo AND 
            #A2ZACCOUNT.MemNo = A2ZODINTDUEHST.MemNo AND 
            #A2ZACCOUNT.AccType = A2ZODINTDUEHST.AccType AND 
            #A2ZACCOUNT.AccNo = A2ZODINTDUEHST.AccNo;


----- ******************************************************************************



----- Generate Work Table for Ledger Balance
	SELECT * INTO #WFCSODLEDGERBALANCE FROM WFCSODLEDGERBALANCE WHERE ID = 0;
	TRUNCATE TABLE #WFCSODLEDGERBALANCE;

      UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.CalFDate = #A2ZACCOUNT.AccODIntDate           
      FROM #A2ZACCOUNT        
	  WHERE #A2ZACCOUNT.AccType = @AccType AND #A2ZACCOUNT.AccODIntDate IS NOT NULL 
      AND #A2ZACCOUNT.AccODIntDate > #A2ZACCOUNT.CalFDate 
      AND #A2ZACCOUNT.AccODIntDate <= @fDate;

    
      UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.CalFDate = #A2ZACCOUNT.AccOpenDate           
      FROM #A2ZACCOUNT        
	  WHERE #A2ZACCOUNT.AccType = @AccType AND  #A2ZACCOUNT.CalFDate IS NULL;

           
      UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.CalTDate = @fDate           
      FROM #A2ZACCOUNT        
	  WHERE #A2ZACCOUNT.AccType = @AccType;

      UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.CalNofDays = ((DATEDIFF(d, CalFDate, CalTDate)) + 0)         
      FROM #A2ZACCOUNT        
	  WHERE #A2ZACCOUNT.AccType = @AccType;

      UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.CalInterest = abs(Round((((AccOpBal * AccIntRate * CalNofDays) / 36500)), 0))       
      FROM #A2ZACCOUNT        
	  WHERE #A2ZACCOUNT.AccType = @AccType;

      UPDATE #A2ZACCOUNT SET #A2ZACCOUNT.CalNofDays = 0       
      FROM #A2ZACCOUNT        
	  WHERE #A2ZACCOUNT.AccType = @AccType AND #A2ZACCOUNT.CalInterest = 0;
     
	
	INSERT INTO #WFCSODLEDGERBALANCE (CuType,CuNo,MemNo,AccNo,OpenDate,LastTrnDate,Balance,AccType,AccStatus,AccStatusDate,DueIntAmount,CurrIntAmount,AccIntRate,IntNoDays,AccODDisbAmt)
	SELECT CuType,CuNo,MemNo,AccNo,AccOpenDate,CalFDate,AccOpBal,AccType,AccStatus,AccStatusDate,AccDueIntAmt,CalInterest,AccIntRate,CalNofDays,AccDisbAmt
	FROM #A2ZACCOUNT WHERE AccType = @AccType;

    
    UPDATE #WFCSODLEDGERBALANCE SET NetIntAmount = DueIntAmount + CurrIntAmount;

   
	DROP TABLE #A2ZACCOUNT;
	DROP TABLE #WFA2ZTRANSACTION;

	IF @CuType > 0
		BEGIN
			DELETE FROM #WFCSODLEDGERBALANCE WHERE CuType <> @CuType;
			DELETE FROM #WFCSODLEDGERBALANCE WHERE CuNo <> @CuNo;
		END

    IF @MemNo > 0
		BEGIN
			DELETE FROM #WFCSODLEDGERBALANCE WHERE CuType <> @CuType AND CuNo <> @CuNo AND MemNo <> @MemNo;
			
		END

    IF @nFlag > 0
		BEGIN
			DELETE FROM #WFCSODLEDGERBALANCE WHERE Balance = 0;
			
		END

    UPDATE #WFCSODLEDGERBALANCE SET AccStatus = 1 WHERE #WFCSODLEDGERBALANCE.AccStatus <> 1 AND @fDate < AccStatusDate;

	
    DELETE FROM #WFCSODLEDGERBALANCE WHERE AccStatus > 97;     
            
----- End of Generate Work Table for Ledger Balance

----- Update Member Name/Status Description --------
	UPDATE #WFCSODLEDGERBALANCE SET MemName = (SELECT A2ZMEMBER.MemName FROM A2ZMEMBER WHERE 
	A2ZMEMBER.CuType = #WFCSODLEDGERBALANCE.CuType AND A2ZMEMBER.CuNo = #WFCSODLEDGERBALANCE.CuNo AND 
	A2ZMEMBER.MemNo = #WFCSODLEDGERBALANCE.MemNo);
    
    UPDATE #WFCSODLEDGERBALANCE SET GLCashCode = (SELECT A2ZCUNION.GLCashCode FROM A2ZCUNION WHERE 
	A2ZCUNION.CuType = #WFCSODLEDGERBALANCE.CuType AND A2ZCUNION.CuNo = #WFCSODLEDGERBALANCE.CuNo);


    UPDATE #WFCSODLEDGERBALANCE SET CuNumber = LTRIM(STR(CuType) + '-' + LTRIM(STR(CuNo)));




----- End of Update Member Name/Status Description --------

	SELECT * FROM #WFCSODLEDGERBALANCE ORDER BY CuType,CuNo,MemNo;

END

GO
