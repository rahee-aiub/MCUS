
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSGenerateSingleAccountBalance](@AccNo BIGINT, @fDate VARCHAR(10), @nFlag INT)
AS
/*
EXECUTE Sp_CSGenerateSingleAccountBalance 1530564145660001,'2016-07-31',0



*/

BEGIN

	UPDATE A2ZACCOUNT SET AccOpBal = 0 WHERE AccNo = @AccNo; 
	
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
    DECLARE @tDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @Balance  money;

    DECLARE @fYear INT;
	DECLARE @tYear INT;

    DECLARE @BegYear int;
    DECLARE @IYear int;

	
	DECLARE @nCount INT;


	DECLARE @nYear INT;	

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

--	PRINT @openTable;
--	PRINT @CuType;
	
	SET @strSQL = 'UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ' +
		'ISNULL((SELECT SUM(TrnAmount) FROM ' + @openTable +  
		' WHERE AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
		'),0) FROM A2ZACCOUNT,' + @openTable +
		' WHERE A2ZACCOUNT.AccNo = ' + CAST(@AccNo AS VARCHAR(16));
	
--	PRINT @strSQL;

	EXECUTE (@strSQL);

     SET @Balance = (SELECT AccOpBal FROM A2ZACCOUNT WHERE AccNo = @accNo);

    
--	IF MONTH(@nDate) <> 7
--		BEGIN


            SET @tDate = @fDate;
            SET @fDate = @opDate; 


----------------------------------------------------------------------------------------------------------
			TRUNCATE TABLE WFA2ZTRANSACTION;
	
			SET @fYear = LEFT(@fDate,4);
			SET @tYear = LEFT(@tDate,4);

           
			SET @nCount = @fYear

           

			WHILE (@nCount <> 0)
			BEGIN
			    
				SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION ' +
        ' WHERE AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
       	' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'
			
			EXECUTE (@strSQL);




			SET @nCount = @nCount + 1;

           
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

		SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZCSMCUS..A2ZTRANSACTION ' + 
        ' WHERE AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
       	' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		
		EXECUTE (@strSQL);


-------------------------------------------------------------------------------------------------

--
--
--			EXECUTE Sp_CSGenerateTransactionDataSingle @CuType, @CuNo, @MemNo, @TrnCode, @AccType, @AccNo, @opDate,@fDate,0;

--            DELETE FROM WFA2ZTRANSACTION WHERE TrnDate = @fDate;	


--            SET @strSQL = 'DELETE FROM WFA2ZTRANSACTION WHERE TrnDate = ''' + @fDate + '''';			
--			EXECUTE (@strSQL);
---------- Credit
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
--			ISNULL((SELECT SUM(GLAmount) FROM WFA2ZTRANSACTION
            ISNULL((SELECT SUM(TrnCredit) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.AccNo = @AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = @AccNo;

            SET @Balance = (SELECT AccOpBal FROM A2ZACCOUNT WHERE AccNo = @accNo);

		


---------- Debit
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(TrnDebit) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.AccNo = @AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = @AccNo;


            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccBalance = A2ZACCOUNT.AccOpBal 
			WHERE A2ZACCOUNT.AccNo = @AccNo;
            

--		END		
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

