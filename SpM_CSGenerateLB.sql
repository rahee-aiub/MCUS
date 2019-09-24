USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_CSGenerateLB]    Script Date: 05/29/2018 12:06:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[SpM_CSGenerateLB](@AccType INT, @fDate VARCHAR(10))
AS
/*

EXECUTE SpM_CSGenerateLB 11,'2017-12-31'




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

    
	UPDATE A2ZACCOUNT SET AccOpBal = 0; 

   
	TRUNCATE TABLE WFA2ZTRANSACTION;

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

			print @openTable;

            SET @strSQL = 'UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ' +
		    'ISNULL((SELECT SUM(TrnAmount) FROM ' + @openTable +  
		    ' WHERE A2ZCSOPBALANCE.CuType = A2ZACCOUNT.CuType ' + 
		    ' AND A2ZCSOPBALANCE.CuNo = A2ZACCOUNT.CuNo' +  
		    ' AND A2ZCSOPBALANCE.MemNo = A2ZACCOUNT.MemNo' + 
		    ' AND A2ZCSOPBALANCE.AccType = A2ZACCOUNT.AccType' + 
		    ' AND A2ZCSOPBALANCE.AccNo = A2ZACCOUNT.AccNo' + 
		    '),0) FROM A2ZACCOUNT,' + @openTable +
		    ' WHERE A2ZACCOUNT.CuType = A2ZCSOPBALANCE.CuType'  +  
		    ' AND A2ZACCOUNT.CuNo = A2ZCSOPBALANCE.CuNo' + 
		    ' AND A2ZACCOUNT.MemNo = A2ZCSOPBALANCE.MemNo' +  
		    ' AND A2ZACCOUNT.AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
		    ' AND A2ZACCOUNT.AccNo = A2ZCSOPBALANCE.AccNo';

	        EXECUTE (@strSQL);
       

    SET @fYear = LEFT(@opDate,4);
	SET @tYear = LEFT(@fDate,4);

	SET @nCount = @fYear

	         WHILE (@nCount <> 0)
		         BEGIN
			
		   	          SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
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
      

		SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
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
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;
---------- Debit
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 0 AND WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;

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
            SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
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

          DELETE FROM WFA2ZTRANSACTION WHERE TrnDate = @fDate;
 
                                                 
---------- Credit
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;
---------- Debit
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 0 AND WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;


            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccTodaysOpBalance - A2ZACCOUNT.AccOpBal           
            FROM A2ZACCOUNT
			WHERE A2ZACCOUNT.AccType = @AccType;       
		    

       END

----------------------------------------------------------------------

        IF @ReadFlag = 0
            BEGIN


		SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
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
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;
---------- Debit
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 0 AND WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;
		END		



     IF @ReadFlag = 0
       BEGIN
           UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccTodaysOpBalance + A2ZACCOUNT.AccOpBal           
           FROM A2ZACCOUNT
		   WHERE A2ZACCOUNT.AccType = @AccType;       
     
       END   


END




END

GO

