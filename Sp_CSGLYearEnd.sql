USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSGLYearEnd]    Script Date: 07/16/2018 11:47:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Sp_CSGLYearEnd] (@EndYear INT)
AS
/*

Sp_CSGLYearEnd 2018

*/

BEGIN

	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @AccType INT;
	DECLARE @fDate VARCHAR(10);
    DECLARE @tDate VARCHAR(10);
    DECLARE @opDate VARCHAR(10);
    DECLARE @NewDate VARCHAR(10);

    DECLARE @PLGLCode INT;
    DECLARE @UnDisPLGLCode INT;

    DECLARE @NetPLAmount MONEY;
	DECLARE @NetUnDisPLAmount MONEY;
    DECLARE @PLAmount MONEY;

    DECLARE @NewYear INT;
    DECLARE @YearEndDate smalldatetime;

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON


    SET @PLGLCode = (SELECT PLGLCode FROM A2ZGLMCUS..A2ZGLPARAMETER);
    SET @UnDisPLGLCode = (SELECT UnDisPLGLCode FROM A2ZGLMCUS..A2ZGLPARAMETER);

--    SET @YearEndDate = (SELECT PrmYearEndDate FROM A2ZHKMCUS..A2ZERPSYSPRM);


	SET @fDate = CAST(@EndYear AS VARCHAR(4)) + '-07-01';
    SET @opDate = CAST(@EndYear AS VARCHAR(4)) + '-06-30';
    SET @tDate = CAST(@EndYear AS VARCHAR(4)) + '-06-30';

    SET @NewYear = (@EndYear + 1);
    SET @NewDate = @fDate;

    
--===============  CUSTOMER SERVICES ================
--	DECLARE accTypeTable CURSOR FOR
--	SELECT AccTypeCode FROM A2ZACCTYPE WHERE AccTypeCode > 0;
--
--	OPEN accTypeTable;
--	FETCH NEXT FROM accTypeTable INTO @AccType;
--
--	WHILE @@FETCH_STATUS = 0 
--		BEGIN
--
--			EXECUTE Sp_CSGenerateLedgerBalance @AccType,@tDate,0,0,0,0,0,0,0,0;
--
--			FETCH NEXT FROM accTypeTable INTO @AccType;	    
--		END
--
--	CLOSE accTypeTable; 
--	DEALLOCATE accTypeTable;

    EXECUTE SpM_CSGenerateLedgerBal @tDate;



	SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '.dbo.A2ZCSOPBALANCE' +
	' (TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnAmount,CuOldNo)' +
	' SELECT ' + '''' + @NewDate + '''' + ',CuType,CuNo,MemNo,AccType,AccNo,0,AccOpBal,OldCuNo' +
	' FROM A2ZACCOUNT WHERE AccStatus < 90 OR AccStatusDate >= ' + '''' + @tDate + '''';

	EXECUTE (@strSQL);
--===============  END OF CUSTOMER SERVICES ================

--===============  GENERAL LEDGER SERVICES ================
--	EXECUTE A2ZGLMCUS..Sp_GlGenerateAccountBalance @opDate,@tDate,0;

    EXECUTE A2ZGLMCUS..Sp_GlGenerateLB @opDate,@tDate,0;

	SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZGLOPBALANCE' +
	' (GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLOpBal,GLOldAccNo)' +
	' SELECT GLCoNo,GLAccType,GLAccNo,GLRecType,GLPrtPos,GLOpBal,GLOldAccNo' +
	' FROM A2ZGLMCUS..A2ZCGLMST';

	EXECUTE (@strSQL);

	SET @strSQL = 'UPDATE A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZGLOPBALANCE' +
	' SET GLOpBal = 0 WHERE GLAccType > 3';
	
    EXECUTE (@strSQL);
--===============  END OF GENERAL LEDGER SERVICES ================

	-- P/L Transfer - Update Income Debit Transaction
	SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZTRANSACTION' 
	+ ' (TrnDate,GLAccNo,VchNo,VoucherNo,TrnDesc,GLAmount,GLDebitAmt,TrnCSGL,FuncOpt,TrnDrCr,GLCreditAmt)' 
	+ ' SELECT ' + '''' + @tDate + '''' + ',GLAccNo,' + '''' + 'P/L-01' + '''' + ',' + '''' + 'P/L-01' + ''''  
    + ',' + '''' + 'P/L Transfer' + '''' + ',(0 - GLOpBal),GLOpBal,1,999,0,0' 
	+ ' FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccType = 4 AND GLOpBal > 0';

	EXECUTE (@strSQL);


    UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = (A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance - A2ZGLMCUS..A2ZCGLMST.GLOpBal)
    FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccType = 4 AND GLOpBal > 0;   



	-- P/L Transfer - Update Income Credit Transaction
	SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZTRANSACTION' 
	+ ' (TrnDate,GLAccNo,VchNo,VoucherNo,TrnDesc,GLAmount,GLCreditAmt,TrnCSGL,FuncOpt,TrnDrCr,GLDebitAmt)' 
	+ ' SELECT ' + '''' + @tDate + '''' + ',GLAccNo,' + '''' + 'P/L-02' + '''' + ',' + '''' + 'P/L-02' + ''''  
    + ',' + '''' + 'P/L Transfer' + '''' + ',ABS(GLOpBal),ABS(GLOpBal),1,999,1,0' 
	+ ' FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccType = 4 AND GLOpBal < 0';

	EXECUTE (@strSQL);

    UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = (A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance + A2ZGLMCUS..A2ZCGLMST.GLOpBal)
    FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccType = 4 AND GLOpBal < 0;   



	-- P/L Transfer - Update Expense Credit Transaction
	SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZTRANSACTION' 
	+ ' (TrnDate,GLAccNo,VchNo,VoucherNo,TrnDesc,GLAmount,GLCreditAmt,TrnCSGL,FuncOpt,TrnDrCr,GLDebitAmt)' 
	+ ' SELECT ' + '''' + @tDate + '''' + ',GLAccNo,' + '''' + 'P/L-03' + '''' + ',' + '''' + 'P/L-03' + ''''  
    + ',' + '''' + 'P/L Transfer' + '''' + ',(0 - GLOpBal),GLOpBal,1,999,1,0' 
	+ ' FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccType = 5 AND GLOpBal > 0';

	EXECUTE (@strSQL);

    UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = (A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance - A2ZGLMCUS..A2ZCGLMST.GLOpBal)
    FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccType = 5 AND GLOpBal > 0;   



	-- P/L Transfer - Update Expense Debit Transaction
	SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZTRANSACTION' 
	+ ' (TrnDate,GLAccNo,VchNo,VoucherNo,TrnDesc,GLAmount,GLDebitAmt,TrnCSGL,FuncOpt,TrnDrCr,GLCreditAmt)' 
	+ ' SELECT ' + '''' + @tDate + '''' + ',GLAccNo,' + '''' + 'P/L-04' + '''' + ',' + '''' + 'P/L-04' + ''''  
    + ',' + '''' + 'P/L Transfer' + '''' + ',ABS(GLOpBal),ABS(GLOpBal),1,999,0,0' 
	+ ' FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccType = 5 AND GLOpBal < 0';

	EXECUTE (@strSQL);

    UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = (A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance + A2ZGLMCUS..A2ZCGLMST.GLOpBal)
    FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccType = 5 AND GLOpBal < 0;   



	-- Transfer (Net Profit/Loss Amount) to Undistributed Dividend ======

	SET @NetPLAmount = (SELECT ISNULL(GLOpBal,0) FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo = @PLGLCode);
	SET @NetUnDisPLAmount = (SELECT ISNULL(GLOpBal,0) FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo = @UnDisPLGLCode);

	IF @NetPLAmount > 0
		BEGIN
			SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZTRANSACTION' 
			+ ' (TrnDate,GLAccNo,VchNo,VoucherNo,TrnDesc,GLAmount,GLDebitAmt,TrnCSGL,FuncOpt,TrnDrCr,GLCreditAmt)' 
			+ ' SELECT ' + '''' + @tDate + '''' + ',GLAccNo,' + '''' + 'P/L-05' + '''' + ',' + '''' + 'P/L-05' + ''''  
			+ ',' + '''' + 'P/L Transfer' + '''' + ',(0 - GLOpBal),ABS(GLOpBal),1,999,0,0' 
			+ ' FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo = ' + CAST(@PLGLCode AS VARCHAR(8));

			EXECUTE (@strSQL);  

			SET @strSQL = 'INSERT INTO A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZTRANSACTION' 
			+ ' (TrnDate,GLAccNo,VchNo,VoucherNo,TrnDesc,GLAmount,GLCreditAmt,TrnCSGL,FuncOpt,TrnDrCr,GLDebitAmt)' 
			+ ' SELECT ' + '''' + @tDate + '''' + ',' + CAST(@UnDisPLGLCode AS VARCHAR(8)) + ',' + '''' + 'P/L-06' + '''' + 
			',' + '''' + 'P/L-06' + ''''  
			+ ',' + '''' + 'P/L Transfer' + '''' + ',GLOpBal,GLOpBal,1,999,0,0' 
			+ ' FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo = ' + CAST(@PLGLCode AS VARCHAR(8));

			EXECUTE (@strSQL);
           
            
            SET @PLAmount = (SELECT GLOpBal FROM A2ZGLMCUS..A2ZCGLMST where GLAccNo = @PLGLCode);

            UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = (A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance - @PLAmount)
            FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo = @PLGLCode;   



            UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = (A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance + @PLAmount)
            FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo = @UnDisPLGLCode;   

            


		END

	SET @strSQL = 'UPDATE A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZGLOPBALANCE' +
	' SET GLOpBal = GLOpBal + ' + CAST(@NetPLAmount AS VARCHAR(12)) + ' WHERE GLAccNo = ' + CAST(@UnDisPLGLCode AS VARCHAR(8));

	EXECUTE (@strSQL);

	SET @strSQL = 'UPDATE A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZGLOPBALANCE' +
	' SET GLOpBal = 0 WHERE GLAccNo = ' + CAST(@PLGLCode AS VARCHAR(8));
 
	EXECUTE (@strSQL);

	-- End of Transfer (Net Profit/Loss Amount) to Undistributed Dividend ======

	-- Update Zero Fields For P/L ======
	SET @strSQL = 'UPDATE A2ZCSMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZTRANSACTION' +
	' SET BatchNo=0,CuType=0,CuNo=0,MemNo=0,AccType=0,AccNo=0,TrnCode=0,PayType=0,TrnType=3,TrnDebit=0,' +
	' TrnCredit=0,ShowInterest=0,TrnInterestAmt=0,TrnPenalAmt=0,TrnChargeAmT=0,TrnDueIntAmt=0,' +
	' TrnODAmount=0,BranchNo=0,TrnGLAccNoDr=0,TrnGLAccNoCr=0,TrnGLFlag=0,TrnFlag=0,TrnStatus=0,' +
	' FromCashCode=0,TrnProcStat=0,TrnSysUser=0,TrnModule=2,ValueDate=' + '''' + @tDate + '''' +
	',GLSlmCode=0,TrnPayment=0,UserID=0' +
	' WHERE TrnCSGL = 1 AND FuncOpt = 999';

	EXECUTE (@strSQL);

--===============  CHANGE YEAR END PROCESS  ================
	UPDATE A2ZCSMCUS..A2ZCSPARAMETER SET FinancialBegYear = FinancialEndYear, FinancialEndYear = @NewYear;

	UPDATE A2ZGLMCUS..A2ZGLPARAMETER SET FinancialBegYear = FinancialEndYear, FinancialEndYear = @NewYear;

	UPDATE A2ZHKMCUS..A2ZERPSYSPRM SET PrmYearEndStat = 0,PrmYearEndDate=null;

	COMMIT TRANSACTION
	SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;
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

END

GO

