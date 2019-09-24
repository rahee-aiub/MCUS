USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSMoveDailyTransaction]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE  [dbo].[Sp_CSMoveDailyTransaction] (@fYY VARCHAR(4))

AS
--EXECUTE Sp_CSMoveDailyTransaction 2017

BEGIN


DECLARE @strSQL NVARCHAR(MAX);

DECLARE @PLCode INT;
DECLARE @PLIncome MONEY;
DECLARE @PLExpense MONEY;
DECLARE @processDate SMALLDATETIME;

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

-------   Generate Profit/Loss Transaction for P/L Code =========
	SET @PLCode = (SELECT PLGLCode FROM A2ZGLMCUS..A2ZGLPARAMETER);
	SET @processDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

	SET @PLIncome = ISNULL((SELECT SUM(GLAmount) AS 'TOTINCOME' FROM A2ZTRANSACTION
					WHERE TrnDate = @processDate AND LEFT(GLAccNo, 1) = 4),0);

	SET @PLExpense = ISNULL((SELECT SUM(GLAmount) AS 'TOTEXPENSE' FROM A2ZTRANSACTION
							WHERE TrnDate = @processDate AND LEFT(GLAccNo, 1) = 5),0);

	IF @PLIncome > 0 
		BEGIN
			INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnType,TrnDrCr,TrnDesc,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnModule,ValueDate)
			VALUES (@processDate,'1','P/L-Income',3,1,'P/L Transfer Cr',2,0,@PLCode,@PLCode,@PLIncome,0,
			@PLIncome,2,@processDate);
		END

	IF @PLIncome < 0 
		BEGIN
			INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnType,TrnDrCr,TrnDesc,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnModule,ValueDate)
			VALUES (@processDate,'2','P/L-Income',3,0,'P/L Transfer Dr',2,@PLCode,0,@PLCode, @PLIncome,
			ABS(@PLIncome),0,2,@processDate);
		END

	IF @PLExpense > 0 
		BEGIN
			INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnType,TrnDrCr,TrnDesc,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnModule,ValueDate)
			VALUES (@processDate,'3','P/L-Expense',3,0,'P/L Transfer Dr',2,@PLCode,0,@PLCode,(0 - @PLExpense),
			@PLExpense,0,2,@processDate);
		END

	IF @PLExpense < 0 
		BEGIN
			INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnType,TrnDrCr,TrnDesc,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnModule,ValueDate)
			VALUES (@processDate,'4','P/L-Expense',3,1,'P/L Transfer Cr',2,0,@PLCode,@PLCode,ABS(@PLExpense),
			0,ABS(@PLExpense),2,@processDate);
		END
-------   End of Generate Profit/Loss Transaction for P/L Code =========

	SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(@fYY AS VARCHAR(4)) + '..A2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,GLSlmCode,TrnPayment,UserIP,UserID,VerifyUserID,CreateDate,CuName,MemName,NewCU,NewMem,NewAcc,NewMemType,NewAccPeriod,NewAccIntRate,NewAccContractIntFlag,NewOpenDate,NewMaturityDate,NewFixedAmt,NewFixedMthInt,NewBenefitDate,AccTypeMode,TrnEditFlag,TrnOrgAmt)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,GLSlmCode,TrnPayment,UserIP,UserID,VerifyUserID,CreateDate,CuName,MemName,NewCU,NewMem,NewAcc,NewMemType,NewAccPeriod,NewAccIntRate,NewAccContractIntFlag,NewOpenDate,NewMaturityDate,NewFixedAmt,NewFixedMthInt,NewBenefitDate,AccTypeMode,TrnEditFlag,TrnOrgAmt' +
					' FROM A2ZCSMCUS..A2ZTRANSACTION ';

	EXECUTE (@strSQL);


----------  Create CS Opening Account Balance

        TRUNCATE TABLE WFA2ZTRANSACTION;
          
        SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,UserID,VerifyUserID,CreateDate' +
				' FROM A2ZCSMCUS..A2ZTRANSACTION ';
		
		EXECUTE (@strSQL);

        UPDATE A2ZACCOUNT SET AccOpBal = 0,CalPaidDeposit=0,CalPaidInterest=0,CalPaidPenal=0; 

---------- Credit
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = A2ZACCOUNT.AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = WFA2ZTRANSACTION.AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;
---------- Debit
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = A2ZACCOUNT.AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 0 AND WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = WFA2ZTRANSACTION.AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;
--		END		

 
           UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccTodaysOpBalance = ISNULL(A2ZACCOUNT.AccTodaysOpBalance,0) + A2ZACCOUNT.AccOpBal           
           FROM A2ZACCOUNT;        
		     
----------  Create GL Opening Account Balance

          UPDATE A2ZGLMCUS..A2ZCGLMST SET GLOpBal = 0; 

          UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLOpBal = A2ZGLMCUS..A2ZCGLMST.GLOpBal + 
	      ISNULL((SELECT SUM(GLAmount) FROM WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo),0)
		  FROM A2ZGLMCUS..A2ZCGLMST,WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo;

          UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = ISNULL(A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance,0) + A2ZGLMCUS..A2ZCGLMST.GLOpBal           
          FROM A2ZGLMCUS..A2ZCGLMST;   


---------Update Pension Defaulter File----------------------------------------------------------

--------- Deposit

          UPDATE A2ZPENSIONDEFAULTER SET A2ZPENSIONDEFAULTER.PaidDepositAmt = A2ZPENSIONDEFAULTER.PaidDepositAmt + 
		  ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.AccNo = A2ZPENSIONDEFAULTER.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
          MONTH(A2ZPENSIONDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZPENSIONDEFAULTER.TrnDate) = YEAR(@processDate) AND
		  WFA2ZTRANSACTION.PayType = 301 AND WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
		  FROM A2ZPENSIONDEFAULTER,WFA2ZTRANSACTION
		  WHERE A2ZPENSIONDEFAULTER.AccNo = WFA2ZTRANSACTION.AccNo AND 
          MONTH(A2ZPENSIONDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZPENSIONDEFAULTER.TrnDate) = YEAR(@processDate);

          UPDATE A2ZPENSIONDEFAULTER SET A2ZPENSIONDEFAULTER.CurrDueDepositAmt = A2ZPENSIONDEFAULTER.CurrDueDepositAmt - 
		  ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.AccNo = A2ZPENSIONDEFAULTER.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
          MONTH(A2ZPENSIONDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZPENSIONDEFAULTER.TrnDate) = YEAR(@processDate) AND
		  WFA2ZTRANSACTION.PayType = 301 AND WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
		  FROM A2ZPENSIONDEFAULTER,WFA2ZTRANSACTION
		  WHERE A2ZPENSIONDEFAULTER.AccNo = WFA2ZTRANSACTION.AccNo AND 
          MONTH(A2ZPENSIONDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZPENSIONDEFAULTER.TrnDate) = YEAR(@processDate);
     
--------- Penal
     
          UPDATE A2ZPENSIONDEFAULTER SET A2ZPENSIONDEFAULTER.PaidPenalAmt = A2ZPENSIONDEFAULTER.PaidPenalAmt + 
		  ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.AccNo = A2ZPENSIONDEFAULTER.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 1 AND 
          MONTH(A2ZPENSIONDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZPENSIONDEFAULTER.TrnDate) = YEAR(@processDate) AND
		  WFA2ZTRANSACTION.PayType = 302 AND WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
		  FROM A2ZPENSIONDEFAULTER,WFA2ZTRANSACTION
		  WHERE A2ZPENSIONDEFAULTER.AccNo = WFA2ZTRANSACTION.AccNo AND 
          MONTH(A2ZPENSIONDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZPENSIONDEFAULTER.TrnDate) = YEAR(@processDate);

-------------------------------------------------------------------------------

---------Update Loan Defaulter File----------------------------------------------------------

--------- Principal

          UPDATE A2ZLOANDEFAULTER SET A2ZLOANDEFAULTER.PaidPrincAmt = A2ZLOANDEFAULTER.PaidPrincAmt + 
		  ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.AccNo = A2ZLOANDEFAULTER.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate) AND
		  (WFA2ZTRANSACTION.PayType = 403 OR WFA2ZTRANSACTION.PayType = 407) AND WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
		  FROM A2ZLOANDEFAULTER,WFA2ZTRANSACTION
		  WHERE A2ZLOANDEFAULTER.AccNo = WFA2ZTRANSACTION.AccNo AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate);

          UPDATE A2ZLOANDEFAULTER SET A2ZLOANDEFAULTER.CurrDuePrincAmt = A2ZLOANDEFAULTER.CurrDuePrincAmt - 
		  ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.AccNo = A2ZLOANDEFAULTER.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate) AND
		  (WFA2ZTRANSACTION.PayType = 403 OR WFA2ZTRANSACTION.PayType = 407) AND WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
		  FROM A2ZLOANDEFAULTER,WFA2ZTRANSACTION
		  WHERE A2ZLOANDEFAULTER.AccNo = WFA2ZTRANSACTION.AccNo AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate);

----- Interest          

          UPDATE A2ZLOANDEFAULTER SET A2ZLOANDEFAULTER.PaidIntAmt = A2ZLOANDEFAULTER.PaidIntAmt + 
		  ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.AccNo = A2ZLOANDEFAULTER.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest <> 0 AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate) AND
		  (WFA2ZTRANSACTION.PayType = 402 OR WFA2ZTRANSACTION.PayType = 413) AND WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
		  FROM A2ZLOANDEFAULTER,WFA2ZTRANSACTION
		  WHERE A2ZLOANDEFAULTER.AccNo = WFA2ZTRANSACTION.AccNo AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate);

          UPDATE A2ZLOANDEFAULTER SET A2ZLOANDEFAULTER.CurrDueIntAmt = A2ZLOANDEFAULTER.CurrDueIntAmt - 
		  ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.AccNo = A2ZLOANDEFAULTER.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest <> 0 AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate) AND
		  (WFA2ZTRANSACTION.PayType = 402 OR WFA2ZTRANSACTION.PayType = 413) AND WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
		  FROM A2ZLOANDEFAULTER,WFA2ZTRANSACTION
		  WHERE A2ZLOANDEFAULTER.AccNo = WFA2ZTRANSACTION.AccNo AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate);

----- Penal          

          UPDATE A2ZLOANDEFAULTER SET A2ZLOANDEFAULTER.PaidPenalAmt = A2ZLOANDEFAULTER.PaidPenalAmt + 
		  ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
		  WHERE WFA2ZTRANSACTION.AccNo = A2ZLOANDEFAULTER.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest <> 0 AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate) AND
		  WFA2ZTRANSACTION.PayType = 404 AND WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
		  FROM A2ZLOANDEFAULTER,WFA2ZTRANSACTION
		  WHERE A2ZLOANDEFAULTER.AccNo = WFA2ZTRANSACTION.AccNo AND 
          MONTH(A2ZLOANDEFAULTER.TrnDate) = MONTH(@processDate) AND 
          YEAR(A2ZLOANDEFAULTER.TrnDate) = YEAR(@processDate);

-------------------------------------------------------------------------------

TRUNCATE TABLE A2ZCSMCUS..A2ZTRANSACTION;

COMMIT TRANSACTION
		SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH


END;


GO
