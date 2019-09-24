USE A2ZGLMCUS
GO

ALTER PROCEDURE [dbo].[Sp_GlGenerateAccountBalance](@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
/*

EXECUTE Sp_GlGenerateAccountBalance '2015-12-31','2015-12-31',0

--
-- @nFlag = 8 = Before Year End IF @tDate = YYYY-06-30
-- Skip Those GL Transaction are P/L Transfer for Income/Expense
--
--


*/

BEGIN
	DECLARE @PLCode INT;
	DECLARE @backStat INT;
	DECLARE @PLIncome MONEY;
	DECLARE @PLExpense MONEY;
	DECLARE @processDate SMALLDATETIME;

	SET @PLCode = (SELECT PLGLCode FROM A2ZGLPARAMETER);
	SET @processDate = (SELECT ProcessDate FROM A2ZGLMCUS..A2ZGLPARAMETER);
	
	EXECUTE Sp_GlGenerateOpeningBalance @fDate,0;
	
	EXECUTE Sp_GlGenerateTransactionData @fDate,@tDate,0;

	IF @nFlag = 8
		BEGIN
			DELETE FROM WFA2ZTRANSACTION WHERE TrnCSGL = 1 AND FuncOpt = 999;
		END

	UPDATE A2ZCGLMST SET GLDrSumC = 0,GLDrSumT = 0, GLCrSumC = 0, GLCrSumT = 0;

	UPDATE A2ZCGLMST SET A2ZCGLMST.GLDrSumC =  
	ISNULL((SELECT SUM(GLDebitAmt) FROM WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLDebitAmt > 0 AND WFA2ZTRANSACTION.TrnType = 1 AND 
	WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo),0)
	FROM A2ZCGLMST,WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo;

	UPDATE A2ZCGLMST SET A2ZCGLMST.GLDrSumT =  
	ISNULL((SELECT SUM(GLDebitAmt) FROM WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLDebitAmt > 0 AND WFA2ZTRANSACTION.TrnType <> 1 AND 
	WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo),0)
	FROM A2ZCGLMST,WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo;

	UPDATE A2ZCGLMST SET A2ZCGLMST.GLCrSumC =  
	ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLCreditAmt > 0 AND WFA2ZTRANSACTION.TrnType = 1 AND 
	WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo),0)
	FROM A2ZCGLMST,WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo;

	UPDATE A2ZCGLMST SET A2ZCGLMST.GLCrSumT =  
	ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLCreditAmt > 0 AND WFA2ZTRANSACTION.TrnType <> 1 AND 
	WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo),0)
	FROM A2ZCGLMST,WFA2ZTRANSACTION
	WHERE WFA2ZTRANSACTION.GLAccNo=A2ZCGLMST.GLAccNo;

--================ Calculate P/L for Current TBf Transaction ============
	IF @processDate = @tDate
		BEGIN
			SET @PLIncome = ISNULL((SELECT SUM(GLAmount) AS 'TOTINCOME' FROM WFA2ZTRANSACTION
							WHERE LEFT(GLAccNo, 1) = 4 AND TrnDate = @tDate),0);

			SET @PLExpense = ISNULL((SELECT SUM(GLAmount) AS 'TOTEXPENSE' FROM WFA2ZTRANSACTION
							WHERE LEFT(GLAccNo, 1) = 5 AND TrnDate = @tDate),0);
					
			UPDATE A2ZCGLMST SET GLDrSumT = GLDrSumT + ABS(@PLIncome) 
			WHERE GLAccNo = @PLCode AND @PLIncome < 0;

			UPDATE A2ZCGLMST SET GLCrSumT = GLCrSumT + ABS(@PLIncome) 
			WHERE GLAccNo = @PLCode AND @PLIncome > 0;

			UPDATE A2ZCGLMST SET GLCrSumT = GLCrSumT + ABS(@PLExpense) 
			WHERE GLAccNo = @PLCode AND @PLExpense < 0;

			UPDATE A2ZCGLMST SET GLDrSumT = GLDrSumT + ABS(@PLExpense) 
			WHERE GLAccNo = @PLCode AND @PLExpense > 0;
		END;
--================ End of Calculate P/L for Current TBf Transaction ============

	UPDATE A2ZCGLMST SET GLClBal = (GLOpBal + GLDrSumC + GLDrSumT)  - (GLCrSumC + GLCrSumT)
	WHERE GLAccType IN (1,5);

	UPDATE A2ZCGLMST SET GLClBal = (GLOpBal + GLCrSumC + GLCrSumT) - (GLDrSumC + GLDrSumT)
	WHERE GLAccType IN (2,4);

	EXECUTE Sp_GlGenerateLayerWiseCOA 0;
END
