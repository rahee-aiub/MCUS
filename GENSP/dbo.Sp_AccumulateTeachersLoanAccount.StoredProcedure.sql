USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_AccumulateTeachersLoanAccount]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_AccumulateTeachersLoanAccount](@fYear INT, @tYear INT, @nFlag INT)
AS

/*

EXECUTE Sp_AccumulateTeachersLoanAccount 2015,2017,0


*/

BEGIN

	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @nCount INT;
	DECLARE @nYear INT;

	DECLARE @opTable NVARCHAR(50);
	DECLARE @trnTable NVARCHAR(50);

	WHILE @tYear >= @fYear
		BEGIN
			SET @nYear = @fYear;

--====================   YEAR WISE ACCOUNT NUMBER CONVERSION =================			
			SET @opTable = 'A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '..A2ZCSOPBALANCE';

			SET @trnTable = 'A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '..A2ZTRANSACTION';

			SET @strSQL = 'ALTER TABLE ' + @opTable + ' ADD AccOpenDate SMALLDATETIME';
			EXECUTE (@strSQL);
			
			SET @strSQL = 'ALTER TABLE ' + @opTable + ' ADD NewAccNo BIGINT';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @opTable + ' SET AccOpenDate = NULL,NewAccNo = NULL,TrnCode = NULL WHERE AccType = 53';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @opTable + ' SET TrnCode = 1 WHERE AccType = 53 AND RIGHT(AccNo,3) = 1';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @opTable + ' SET ' + @opTable + '.AccOpenDate =' + 
			' (SELECT A2ZCSMCUS..A2ZACCOUNT.AccOpenDate FROM A2ZCSMCUS..A2ZACCOUNT' +
			' WHERE A2ZCSMCUS..A2ZACCOUNT.AccNo = ' + @opTable + '.AccNo' +
			' AND A2ZCSMCUS..A2ZACCOUNT.AccType = 53 AND ' + @opTable + '.AccType = 53 AND' +
			' A2ZCSMCUS..A2ZACCOUNT.AccOpenDate < ' + '''' + '2017-07-01' + '''' + ')';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @opTable + ' SET NewAccNo = (AccNo - RIGHT(AccNo,3)) + 1 WHERE AccType = 53 AND AccOpenDate IS NOT NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @opTable + ' SET AccNo = NewAccNo WHERE AccType = 53 AND AccOpenDate IS NOT NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @opTable + ' SET NewAccNo = (AccNo - RIGHT(AccNo,3)) + 2 WHERE AccType = 53 AND AccOpenDate IS NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @opTable + ' SET AccNo = NewAccNo WHERE AccType = 53 AND AccOpenDate IS NULL';
			EXECUTE (@strSQL);
--====================   END OF YEAR WISE ACCOUNT NUMBER CONVERSION =================

--====================   YEAR WISE ACCOUNT BALANCE CONVERSION =================	

			SET @strSQL = 'SELECT * INTO ' + @opTable + '#A2ZCSOPBALANCE FROM ' + @opTable + ' WHERE ID = 0';
			EXECUTE (@strSQL); 

			SET @strSQL = 'INSERT INTO ' + @opTable + '#A2ZCSOPBALANCE (TrnDate,AccNo,TrnAmount)' + 
			' SELECT ' + '''' + '2017-01-01' + '''' + ',AccNo,SUM(TrnAmount) AS TrnAmount FROM ' + 
			@opTable + ' WHERE AccType = 53 AND TrnCode IS NULL GROUP BY AccNo';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @opTable + ' SET ' + @opTable + '.TrnAmount = ' + @opTable + '.TrnAmount +' +
			' (SELECT ' + @opTable + '#A2ZCSOPBALANCE.TrnAmount FROM ' + @opTable + '#A2ZCSOPBALANCE' +
			' WHERE ' + @opTable + '.AccNo = ' + @opTable + '#A2ZCSOPBALANCE.AccNo)' +
			' FROM ' + @opTable + ',' + @opTable + '#A2ZCSOPBALANCE' +
			' WHERE ' + @opTable + '.AccNo = ' + @opTable + '#A2ZCSOPBALANCE.AccNo AND ' +
			@opTable + '.AccType = 53 AND ' + @opTable + '.TrnCode = 1';
			EXECUTE (@strSQL);

			SET @strSQL = 'DELETE FROM ' + @opTable + ' WHERE AccType = 53 AND TrnCode IS NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'DROP TABLE ' + @opTable + '#A2ZCSOPBALANCE';
			EXECUTE (@strSQL);
--====================   END OF YEAR WISE ACCOUNT BALANCE CONVERSION =================	

--====================   YEAR WISE TRANSACTION CONVERSION =================	

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET ReTrnDate = NULL,UserIP = NULL WHERE AccType = 53';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET ' + @trnTable + '.ReTrnDate =' +
			' (SELECT A2ZCSMCUS..A2ZACCOUNT.AccOpenDate FROM A2ZCSMCUS..A2ZACCOUNT' +
			' WHERE A2ZCSMCUS..A2ZACCOUNT.AccNo = ' + @trnTable + '.AccNo' +
			' AND A2ZCSMCUS..A2ZACCOUNT.AccType = 53 AND ' + @trnTable + '.AccType = 53 AND' +
			' A2ZCSMCUS..A2ZACCOUNT.AccOpenDate < ' + '''' + '2017-07-01' + '''' + ')';
			EXECUTE (@strSQL);
	
			SET @strSQL = 'UPDATE ' + @trnTable + ' SET UserIP = (AccNo - RIGHT(AccNo,3)) + 1 WHERE AccType = 53 AND ReTrnDate IS NOT NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET AccNo = UserIP WHERE AccType = 53 AND ReTrnDate IS NOT NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET UserIP = (AccNo - RIGHT(AccNo,3)) + 2 WHERE AccType = 53 AND ReTrnDate IS NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET AccNo = UserIP WHERE AccType = 53 AND ReTrnDate IS NULL';
			EXECUTE (@strSQL);

--====================   END OF YEAR WISE TRANSACTION CONVERSION =================	
			
			SET @fYear = @fYear + 1;		
		END

--====================   CURRENT YEAR TRANSACTION CONVERSION =================	
			SET @trnTable = 'A2ZCSMCUS..A2ZTRANSACTION';

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET ReTrnDate = NULL,UserIP = NULL WHERE AccType = 53';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET ' + @trnTable + '.ReTrnDate =' +
			' (SELECT A2ZCSMCUS..A2ZACCOUNT.AccOpenDate FROM A2ZCSMCUS..A2ZACCOUNT' +
			' WHERE A2ZCSMCUS..A2ZACCOUNT.AccNo = ' + @trnTable + '.AccNo' +
			' AND A2ZCSMCUS..A2ZACCOUNT.AccType = 53 AND ' + @trnTable + '.AccType = 53 AND' +
			' A2ZCSMCUS..A2ZACCOUNT.AccOpenDate < ' + '''' + '2017-07-01' + '''' + ')';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET ' + @trnTable + '.ReTrnDate =' +
			' (SELECT A2ZCSMCUS..A2ZACCOUNT.AccOpenDate FROM A2ZCSMCUS..A2ZACCOUNT' +
			' WHERE RIGHT(A2ZCSMCUS..A2ZACCOUNT.AccNo,3) = 1 AND A2ZCSMCUS..A2ZACCOUNT.AccNo = ' + @trnTable + '.AccNo' +
			' AND A2ZCSMCUS..A2ZACCOUNT.AccType = 53 AND ' + @trnTable + '.AccType = 53 AND' +
			' A2ZCSMCUS..A2ZACCOUNT.AccOpenDate > ' + '''' + '2017-06-30' + '''' + ')';
			EXECUTE (@strSQL);
	
			SET @strSQL = 'UPDATE ' + @trnTable + ' SET UserIP = (AccNo - RIGHT(AccNo,3)) + 1 WHERE AccType = 53 AND ReTrnDate IS NOT NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET AccNo = UserIP WHERE AccType = 53 AND ReTrnDate IS NOT NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET UserIP = (AccNo - RIGHT(AccNo,3)) + 2 WHERE AccType = 53 AND ReTrnDate IS NULL';
			EXECUTE (@strSQL);

			SET @strSQL = 'UPDATE ' + @trnTable + ' SET AccNo = UserIP WHERE AccType = 53 AND ReTrnDate IS NULL';
			EXECUTE (@strSQL);

--====================   END OF CURRENT YEAR TRANSACTION CONVERSION =================	

--====================   CONVERSION ACCOUNT TABLE FOR ACCOUNT NO. =================	
	UPDATE A2ZACCOUNT SET AccCertNo = NULL WHERE AccType = 53;

	TRUNCATE TABLE WFA2ZTRANSACTION;

	INSERT INTO WFA2ZTRANSACTION(CuNo,CuType,AccNo)
	SELECT CuType,CuNo,MAX(AccNo) FROM A2ZACCOUNT
	WHERE AccType = 53 AND AccOpenDate < '2017-07-01'
	GROUP BY CuType,CuNo;

	UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccCertNo = 
	(SELECT WFA2ZTRANSACTION.AccNo FROM WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate < '2017-07-01')
	FROM A2ZACCOUNT,WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate < '2017-07-01';

	TRUNCATE TABLE WFA2ZTRANSACTION;

	INSERT INTO WFA2ZTRANSACTION(CuNo,CuType,AccNo)
	SELECT CuType,CuNo,MAX(AccNo) FROM A2ZACCOUNT
	WHERE AccType = 53 AND AccOpenDate > '2017-06-30'
	GROUP BY CuType,CuNo;

	UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccCertNo = 
	(SELECT WFA2ZTRANSACTION.AccNo FROM WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate > '2017-06-30')
	FROM A2ZACCOUNT,WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate > '2017-06-30';

	TRUNCATE TABLE WFA2ZTRANSACTION;

	INSERT INTO WFA2ZTRANSACTION(CuNo,CuType,AccNo,TrnDebit,TrnCredit)
	SELECT CuType,CuNo,MAX(AccNo),SUM(AccDisbAmt),SUM(AccLoanSancAmt) FROM A2ZACCOUNT
	WHERE AccType = 53 AND AccOpenDate < '2017-07-01' AND AccStatus < 98
	GROUP BY CuType,CuNo;

	UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccDisbAmt = 
	(SELECT WFA2ZTRANSACTION.TrnDebit FROM WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate < '2017-07-01')
	FROM A2ZACCOUNT,WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate < '2017-07-01';

	UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccLoanSancAmt = 
	(SELECT WFA2ZTRANSACTION.TrnCredit FROM WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate < '2017-07-01')
	FROM A2ZACCOUNT,WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate < '2017-07-01';

	TRUNCATE TABLE WFA2ZTRANSACTION;

	INSERT INTO WFA2ZTRANSACTION(CuNo,CuType,AccNo,TrnDebit,TrnCredit)
	SELECT CuType,CuNo,MAX(AccNo),SUM(AccDisbAmt),SUM(AccLoanSancAmt) FROM A2ZACCOUNT
	WHERE AccType = 53 AND AccOpenDate > '2017-06-30' AND AccStatus < 98
	GROUP BY CuType,CuNo;

	UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccDisbAmt = 
	(SELECT WFA2ZTRANSACTION.TrnDebit FROM WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate > '2017-06-30')
	FROM A2ZACCOUNT,WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate > '2017-06-30';

	UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccLoanSancAmt = 
	(SELECT WFA2ZTRANSACTION.TrnCredit FROM WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate > '2017-06-30')
	FROM A2ZACCOUNT,WFA2ZTRANSACTION
	WHERE A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo AND
	A2ZACCOUNT.AccType = 53 AND A2ZACCOUNT.AccOpenDate > '2017-06-30';

	DELETE FROM A2ZACCOUNT WHERE AccType = 53 AND AccCertNo IS NULL;

	UPDATE A2ZACCOUNT SET AccCertNo = (AccNo - RIGHT(AccNo,3)) + 1 
	WHERE AccType = 53 AND RIGHT(AccNo,3) > 1 AND AccOpenDate < '2017-07-01'

	UPDATE A2ZACCOUNT SET AccCertNo = (AccNo - RIGHT(AccNo,3)) + 2 WHERE AccType = 53 AND RIGHT(AccCertNo,3) <> 1;

	UPDATE A2ZACCOUNT SET AccNo = AccCertNo WHERE AccType = 53 AND RIGHT(AccNo,3) <> 1;
	
--===============  UPDATE ACCOUNT INSTALLMENT AMOUNT ===============
	EXECUTE Sp_CSGenerateLedgerBalance 53,'2017-08-31',0,0,0,0,0,0,0,0

	UPDATE A2ZACCOUNT SET AccStatus = 1 WHERE AccType = 53 AND AccStatus = 99 AND AccOpBal <> 0;

	UPDATE A2ZACCOUNT SET AccLoanInstlAmt = ABS(ROUND(AccOpBal / 60,0))	WHERE AccType = 53;

	UPDATE A2ZACCOUNT SET AccLoanLastInstlAmt = ABS(AccOpBal) - (AccLoanInstlAmt * 59) WHERE AccType = 53;

	UPDATE A2ZACCOUNT SET AccLoanLastInstlAmt = 0 WHERE AccType = 53 AND AccLoanLastInstlAmt < 0
--===============  END OF UPDATE ACCOUNT INSTALLMENT AMOUNT ===============

    

--====================   CONVERSION ACCOUNT TABLE FOR ACCOUNT NO. =================	

END



GO
