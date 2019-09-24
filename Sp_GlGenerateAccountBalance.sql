
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GlGenerateAccountBalance](@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT, @sFlag INT)
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
    DECLARE @UNDPLCode INT;
	DECLARE @backStat INT;
	DECLARE @PLIncome MONEY;
	DECLARE @PLExpense MONEY;
	DECLARE @processDate SMALLDATETIME;
    DECLARE @ReadFlag INT;

    DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30)

    DECLARE @pYear INT;
    DECLARE @fYear INT;
	DECLARE @tYear INT;
    DECLARE @nYear INT;
	DECLARE @nCount INT;

    DECLARE @tmm INT;
    DECLARE @tdd INT;
    DECLARE @xDate VARCHAR(10);

    DECLARE @xFlag INT;


	SET @PLCode = (SELECT PLGLCode FROM A2ZGLPARAMETER);
    SET @UNDPLCode = (SELECT UnDisPLGLCode FROM A2ZGLPARAMETER);
	SET @processDate = (SELECT ProcessDate FROM A2ZGLMCUS..A2ZGLPARAMETER);

    SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE UserID = 0;
    TRUNCATE TABLE #WFA2ZTRANSACTION;

    SET @pYear = YEAR(@processDate);
    SET @fYear = LEFT(@fDate,4);

    SET @xFlag = 2;

    IF @pYear <> @fYear
        BEGIN
           SET @xFlag = 1;
        END

    
-----***********************************************************************************
    IF @xFlag = 1
       BEGIN
          EXECUTE Sp_GlGenerateOpeningBalance @fDate,0;
       END


-----***********************************************************************************

    IF @xFlag = 2
       BEGIN

    TRUNCATE TABLE WFA2ZTRANSACTION;

    UPDATE A2ZCGLMST SET GLOpBal = 0;

    SET @tmm = MONTH(@processDate);
    SET @tdd = DAY(@processDate);
    

    IF @tmm < 10 AND @tdd < 10
       BEGIN
            SET @xDate = CAST(YEAR(@processDate) AS VARCHAR(4)) + '-' + '0' + CAST(MONTH(@processDate) AS VARCHAR(1)) + '-' + '0' +  CAST(DAY(@processDate) AS VARCHAR(1));
       END

    IF @tmm < 10 AND @tdd > 9
       BEGIN
            SET @xDate = CAST(YEAR(@processDate) AS VARCHAR(4)) + '-' + '0' + CAST(MONTH(@processDate) AS VARCHAR(1)) + '-' + CAST(DAY(@processDate) AS VARCHAR(2));
       END

    IF @tmm > 9 AND @tdd < 10
       BEGIN
            SET @xDate = CAST(YEAR(@processDate) AS VARCHAR(4)) + '-' + CAST(MONTH(@processDate) AS VARCHAR(2)) + '-' + '0' + CAST(DAY(@processDate) AS VARCHAR(1));
       END

    IF @tmm > 9 AND @tdd > 9
       BEGIN
            SET @xDate = CAST(YEAR(@processDate) AS VARCHAR(4)) + '-' + CAST(MONTH(@processDate) AS VARCHAR(2)) + '-' + CAST(DAY(@processDate) AS VARCHAR(2));
       END


    IF @fDate = @processDate
       BEGIN
            SET @ReadFlag = 0;
       END
    ELSE
       BEGIN
            SET @ReadFlag = 1;
       END
--------------------------------------------------------------------------------
  
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

        

        IF @ReadFlag = 1
           BEGIN

		        WHILE (@nCount <> 0)
			         BEGIN
				          SET @strSQL = 'INSERT INTO WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					      'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					      'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					      'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					      ' SELECT ' +
					      'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					      'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					      'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					      'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					      ' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE' +
					      ' TrnProcStat = 0 AND TrnDate' +
					      ' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @xDate + '''';
					     			
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
				
				UPDATE A2ZCGLMST SET A2ZCGLMST.GLOpBal = A2ZCGLMST.GLOpBal + 
				ISNULL((SELECT SUM(GLAmount) FROM WFA2ZTRANSACTION
				WHERE WFA2ZTRANSACTION.GLAccNo = A2ZCGLMST.GLAccNo),0)
				FROM A2ZCGLMST,WFA2ZTRANSACTION
				WHERE WFA2ZTRANSACTION.GLAccNo = A2ZCGLMST.GLAccNo;

                UPDATE A2ZCGLMST SET A2ZCGLMST.GLOpBal = A2ZCGLMST.GLTodaysOpBalance - A2ZCGLMST.GLOpBal           
                FROM A2ZCGLMST; 
                
        END

        IF @ReadFlag = 0
           BEGIN
               UPDATE A2ZCGLMST SET A2ZCGLMST.GLOpBal = A2ZCGLMST.GLTodaysOpBalance           
               FROM A2ZCGLMST;    
           END 

END


-------------------------------------------------------------------------------    
	
	EXECUTE Sp_GlGenerateTransactionData @fDate,@tDate,0,@sFlag;

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


--=======================  glcode Order by PL Code

    UPDATE WFINCEXPREP SET GLCodeOrder = 0 
                            WHERE GLHead != 206 and GLHead != 207;



    UPDATE WFINCEXPREP SET GLCodeOrder = 1 
                            WHERE GLHead = 206 or GLHead = 207;

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

