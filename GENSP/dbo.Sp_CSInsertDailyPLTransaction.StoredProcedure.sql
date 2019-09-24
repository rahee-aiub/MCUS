USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSInsertDailyPLTransaction]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE  [dbo].[Sp_CSInsertDailyPLTransaction]

AS

BEGIN

--EXECUTE Sp_CSInsertDailyPLTransaction


DECLARE @strSQL NVARCHAR(MAX);

DECLARE @PLCode INT;
DECLARE @PLIncome MONEY;
DECLARE @PLExpense MONEY;
DECLARE @processDate SMALLDATETIME;

DECLARE @plDate SMALLDATETIME;
--------------------
TRUNCATE TABLE WFPLDATE;
SET @processDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

INSERT INTO WFPLDATE (PLDate)
SELECT TrnDate
FROM A2ZTRANSACTION
WHERE @processDate > TrnDate
GROUP BY TrnDate

DELETE FROM A2ZTRANSACTION WHERE VoucherNo = 'P/L-Income' OR VoucherNo = 'P/L-Expense' 

-----------------------


BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON


DECLARE PLTable CURSOR FOR
SELECT PLDate
FROM WFPLDATE;

OPEN PLTable;
FETCH NEXT FROM PLTable INTO
@plDate;


WHILE @@FETCH_STATUS = 0 
	BEGIN


-------   Generate Profit/Loss Transaction for P/L Code =========
	SET @PLCode = (SELECT PLGLCode FROM A2ZGLMCUS..A2ZGLPARAMETER);
--	SET @processDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

	SET @PLIncome = ISNULL((SELECT SUM(GLAmount) AS 'TOTINCOME' FROM A2ZTRANSACTION
					WHERE TrnDate = @plDate AND LEFT(GLAccNo, 1) = 4),0);

	SET @PLExpense = ISNULL((SELECT SUM(GLAmount) AS 'TOTEXPENSE' FROM A2ZTRANSACTION
							WHERE TrnDate = @plDate AND LEFT(GLAccNo, 1) = 5),0);
----------------------
	


---------------------
	IF @PLIncome > 0 
		BEGIN
			INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnType,TrnDrCr,TrnDesc,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnModule,ValueDate)
			VALUES (@plDate,'1','P/L-Income',3,1,'P/L Transfer Cr',2,0,@PLCode,@PLCode,@PLIncome,0,
			@PLIncome,2,@plDate);
		END

	IF @PLIncome < 0 
		BEGIN
			INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnType,TrnDrCr,TrnDesc,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnModule,ValueDate)
			VALUES (@plDate,'2','P/L-Income',3,0,'P/L Transfer Dr',2,@PLCode,0,@PLCode,@PLIncome,
			ABS(@PLIncome),0,2,@plDate);
		END

	IF @PLExpense > 0 
		BEGIN
			INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnType,TrnDrCr,TrnDesc,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnModule,ValueDate)
			VALUES (@plDate,'3','P/L-Expense',3,0,'P/L Transfer Dr',2,@PLCode,0,@PLCode,(0 - @PLExpense),
			@PLExpense,0,2,@plDate);
		END

	IF @PLExpense < 0 
		BEGIN
			INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnType,TrnDrCr,TrnDesc,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnModule,ValueDate)
			VALUES (@plDate,'4','P/L-Expense',3,1,'P/L Transfer Cr',2,0,@PLCode,@PLCode,ABS(@PLExpense),
			0,ABS(@PLExpense),2,@plDate);
		END
-------   End of Generate Profit/Loss Transaction for P/L Code =========


    FETCH NEXT FROM PLTable INTO @plDate;


	END

CLOSE PLTable; 
DEALLOCATE PLTable;


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
