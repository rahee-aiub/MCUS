USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[SpM_CSGenerateSingleAccountBalance]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SpM_CSGenerateSingleAccountBalance](@AccNo BIGINT, @fDate VARCHAR(10), @nFlag INT)
AS
/*
EXECUTE SpM_CSGenerateSingleAccountBalance 1530564145660001,'2017-06-29',0



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

	   
    SET @tDate = @fDate;
           


----------------------------------------------------------------------------------------------------------
    	SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE UserID = 0;
		TRUNCATE TABLE #WFA2ZTRANSACTION;

     
						

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
        ' WHERE AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
       	' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		
		EXECUTE (@strSQL);



---------- Credit
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
            ISNULL((SELECT SUM(TrnCredit) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.AccNo = @AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 1),0)
			FROM A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = @AccNo;

           
---------- Debit
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(TrnDebit) FROM #WFA2ZTRANSACTION
			WHERE #WFA2ZTRANSACTION.AccNo = @AccNo AND #WFA2ZTRANSACTION.TrnCSGL = 0 AND #WFA2ZTRANSACTION.ShowInterest = 0 AND 
			#WFA2ZTRANSACTION.TrnFlag = 0 AND #WFA2ZTRANSACTION.TrnProcStat = 0 AND #WFA2ZTRANSACTION.TrnDrCr = 0),0)
			FROM A2ZACCOUNT,#WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = @AccNo;


            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccTodaysOpBalance + A2ZACCOUNT.AccOpBal 
			WHERE A2ZACCOUNT.AccNo = @AccNo;

            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccBalance = A2ZACCOUNT.AccOpBal 
			WHERE A2ZACCOUNT.AccNo = @AccNo;
            

--		END		
END

GO
