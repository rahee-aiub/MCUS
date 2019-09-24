
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GlGenerateLB](@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
/*

EXECUTE Sp_GlGenerateLB '2015-12-31','2015-12-31',0

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



END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

