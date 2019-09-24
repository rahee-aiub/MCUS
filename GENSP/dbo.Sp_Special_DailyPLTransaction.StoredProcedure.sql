USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_Special_DailyPLTransaction]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_Special_DailyPLTransaction] (@PLMonth INT)

AS

BEGIN

--EXECUTE Sp_Special_DailyPLTransaction 12


DECLARE @strSQL NVARCHAR(MAX);

DECLARE @PLCode INT;
DECLARE @PLIncome MONEY;
DECLARE @PLExpense MONEY;
DECLARE @processDate SMALLDATETIME;

DECLARE @plDate SMALLDATETIME;
DECLARE @PLNetAmount MONEY;
--DECLARE @PLMonth MONEY;
--------------------
TRUNCATE TABLE A2ZCSMCUS..WFPLDATE;

SET @processDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

--****** Change @PLMonth *****

--SET @PLMonth =9

--*****

INSERT INTO A2ZCSMCUS..WFPLDATE (PLDate)
SELECT A2ZCSMCUST2017..A2ZTRANSACTION.TrnDate
FROM A2ZCSMCUST2017..A2ZTRANSACTION
WHERE MONTH(A2ZCSMCUST2017..A2ZTRANSACTION.TrnDate) = @PLMonth
GROUP BY A2ZCSMCUST2017..A2ZTRANSACTION.TrnDate

SET @PLNetAmount = 0

SET @PLNetAmount = (SELECT SUM(GLAMOUNT)
FROM  A2ZCSMCUST2017..A2ZTRANSACTION
WHERE GLACCNO = 20708001 AND MONTH(A2ZCSMCUST2017..A2ZTRANSACTION.TrnDate) = @PLMonth);

UPDATE A2ZGLMCUS..A2ZCGLMST SET GLTodaysOpBalance = GLTodaysOpBalance - @PLNetAmount
WHERE GLACCNO = 20708001

DELETE FROM A2ZCSMCUST2017..A2ZTRANSACTION
WHERE     (VoucherNo = 'P/L-Income') AND (MONTH(TrnDate) = @PLMonth) OR
          (VoucherNo = 'P/L-Expense') AND (MONTH(TrnDate) = @PLMonth)

-----------------------


BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON


DECLARE PLTable CURSOR FOR
SELECT A2ZCSMCUS..WFPLDATE.PLDate
FROM A2ZCSMCUS..WFPLDATE
ORDER BY A2ZCSMCUS..WFPLDATE.PLDate;

OPEN PLTable;
FETCH NEXT FROM PLTable INTO
@plDate;


WHILE @@FETCH_STATUS = 0 
	BEGIN


-------   Generate Profit/Loss Transaction for P/L Code =========
	SET @PLCode = (SELECT PLGLCode FROM A2ZGLMCUS..A2ZGLPARAMETER);
--	SET @processDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

	SET @PLIncome = ISNULL((SELECT SUM(A2ZCSMCUST2017..A2ZTRANSACTION.GLAmount) AS 'TOTINCOME' FROM A2ZCSMCUST2017..A2ZTRANSACTION
					WHERE TrnDate = @plDate AND LEFT(GLAccNo, 1) = 4),0);

	SET @PLExpense = ISNULL((SELECT SUM(A2ZCSMCUST2017..A2ZTRANSACTION.GLAmount) AS 'TOTEXPENSE' FROM A2ZCSMCUST2017..A2ZTRANSACTION
							WHERE TrnDate = @plDate AND LEFT(GLAccNo, 1) = 5),0);
----------------------

---------------------
	IF @PLIncome > 0 
		BEGIN
			INSERT INTO A2ZCSMCUST2017..A2ZTRANSACTION (BatchNo, TrnDate, VchNo, VoucherNo, CuType, CuNo, MemNo, AccType,
			AccNo, FuncOpt, PayType, TrnType, TrnDrCr, TrnDebit, TrnCredit, TrnDesc, TrnChqNo,ShowInterest, TrnInterestAmt,
			TrnPenalAmt, TrnChargeAmT, TrnDueIntAmt, TrnODAmount, BranchNo, TrnCSGL, TrnGLAccNoDr, TrnGLAccNoCr, TrnGLFlag,
			GLAccNo,GLAmount, GLDebitAmt, GLCreditAmt, TrnFlag, TrnStatus, FromCashCode, TrnProcStat, TrnSysUser,
			TrnModule, ValueDate, UserID, VerifyUserID)
			VALUES (0,@plDate,'1','P/L-Income',0,0,0,0,0,0,0,3,1,0,0,'P/L Transfer Cr',0,0,0,0,0,0,0,0,2,0,@PLCode,0,
			@PLCode,@PLIncome,0,@PLIncome,0,0,0,0,0,2,@plDate,0,0);
		END

	IF @PLIncome < 0 
		BEGIN
			 INSERT INTO A2ZCSMCUST2017..A2ZTRANSACTION ( BatchNo, TrnDate, VchNo, VoucherNo, CuType, CuNo, MemNo, AccType,
			 AccNo, FuncOpt, PayType, TrnType, TrnDrCr, TrnDebit, TrnCredit, TrnDesc, TrnChqNo,ShowInterest, TrnInterestAmt,
			 TrnPenalAmt, TrnChargeAmT, TrnDueIntAmt, TrnODAmount, BranchNo, TrnCSGL, TrnGLAccNoDr, TrnGLAccNoCr, TrnGLFlag,
			 GLAccNo,GLAmount, GLDebitAmt, GLCreditAmt, TrnFlag, TrnStatus, FromCashCode, TrnProcStat, TrnSysUser,
			 TrnModule, ValueDate, UserID, VerifyUserID)
			 VALUES (0,@plDate,'2','P/L-Income',0,0,0,0,0,0,0,3,0,0,0,'P/L Transfer Dr',0,0,0,0,0,0,0,0,2,@PLCode,0,
			 0,@PLCode,@PLIncome,ABS(@PLIncome),0,0,0,0,0,0,2,@plDate,0,0);
		END

	IF @PLExpense > 0 
		BEGIN
			 INSERT INTO A2ZCSMCUST2017..A2ZTRANSACTION ( BatchNo, TrnDate, VchNo, VoucherNo, CuType, CuNo, MemNo, AccType,
			 AccNo, FuncOpt, PayType, TrnType, TrnDrCr, TrnDebit, TrnCredit, TrnDesc, TrnChqNo,ShowInterest, TrnInterestAmt,
			 TrnPenalAmt, TrnChargeAmT, TrnDueIntAmt, TrnODAmount, BranchNo, TrnCSGL, TrnGLAccNoDr, TrnGLAccNoCr, TrnGLFlag,
			 GLAccNo,GLAmount, GLDebitAmt, GLCreditAmt, TrnFlag, TrnStatus, FromCashCode, TrnProcStat, TrnSysUser,
			 TrnModule, ValueDate, UserID, VerifyUserID)
			 VALUES (0,@plDate,'3','P/L-Expense',0,0,0,0,0,0,0,3,0,0,0,'P/L Transfer Dr',0,0,0,0,0,0,0,0,2,@PLCode,0,
			 0,@PLCode,(0-@PLExpense),@PLExpense,0,0,0,0,0,0,2,@plDate,0,0);
		END

	IF @PLExpense < 0 
		BEGIN
			INSERT INTO A2ZCSMCUST2017..A2ZTRANSACTION ( BatchNo, TrnDate, VchNo, VoucherNo, CuType, CuNo, MemNo, AccType,
			AccNo, FuncOpt, PayType, TrnType, TrnDrCr, TrnDebit, TrnCredit, TrnDesc, TrnChqNo,ShowInterest, TrnInterestAmt,
			TrnPenalAmt, TrnChargeAmT, TrnDueIntAmt, TrnODAmount, BranchNo, TrnCSGL, TrnGLAccNoDr, TrnGLAccNoCr, TrnGLFlag,
			GLAccNo,GLAmount, GLDebitAmt, GLCreditAmt, TrnFlag, TrnStatus, FromCashCode, TrnProcStat, TrnSysUser,
			TrnModule, ValueDate, UserID, VerifyUserID)
			VALUES (0,@plDate,'4','P/L-Expense',0,0,0,0,0,0,0,3,1,0,0,'P/L Transfer Cr',0,0,0,0,0,0,0,0,2,0,@PLCode,
			0,@PLCode,ABS(@PLExpense),0,ABS(@PLExpense),0,0,0,0,0,2,@plDate,0,0);
		END

-------   End of Generate Profit/Loss Transaction for P/L Code =========


    FETCH NEXT FROM PLTable INTO @plDate;


	END

CLOSE PLTable; 
DEALLOCATE PLTable;


COMMIT TRANSACTION
		SET NOCOUNT OFF

SET @PLNetAmount = 0

SET @PLNetAmount = (SELECT SUM(GLAMOUNT)
FROM  A2ZCSMCUST2017..A2ZTRANSACTION
WHERE GLACCNO = 20708001 AND MONTH(A2ZCSMCUST2017..A2ZTRANSACTION.TrnDate) = @PLMonth);

UPDATE A2ZGLMCUS..A2ZCGLMST SET GLTodaysOpBalance = GLTodaysOpBalance + @PLNetAmount
WHERE GLACCNO = 20708001

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
