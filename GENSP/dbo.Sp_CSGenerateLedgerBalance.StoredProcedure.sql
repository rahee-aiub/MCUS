USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGenerateLedgerBalance]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSGenerateLedgerBalance](@AccType INT, @fDate VARCHAR(10), @CuType INT, @CuNo INT,
@AccStatFlag INT,@BalanceFlag INT, @fBalance MONEY, @tBalance MONEY, @nFlag INT,@CodeType INT)
AS
/*
									1,     2      ,3,4,5,6,7,8,9
EXECUTE Sp_CSGenerateLedgerBalance 52,'2015-10-01',2,0,0,0,0,0,0

EXECUTE Sp_CSGenerateLedgerBalance 51,'2016-01-31',0,0,0,0,0,0,0,0


@CuType = 0 = All Credit Union, Value = Credit Union
@CuType = Value = @CuNo = Value

@AccStatFlag = 0 = All Account Status, Value = Account Status

@BalanceFlag = 0 = All Balance
@BalanceFlag = 1 = Only Zero Balance
@BalanceFlag = 2 = Check Balance

@BalanceFlag = 2 = Check Balance = Between @fBalance and @tBalance

*/

BEGIN

	UPDATE A2ZACCOUNT SET AccOpBal = 0 WHERE AccType = @AccType; 

	DECLARE @trnDate SMALLDATETIME;
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);
    DECLARE @acctypeclass int;

    DECLARE @BegYear int;
    DECLARE @IYear int;

	DECLARE @nYear INT;	

	SET @nDate = @fDate;
    
    SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZCSPARAMETER);

    SET @IYear = YEAR(@fDate);


    SET @acctypeclass = (SELECT AccTypeClass FROM A2ZACCTYPE WHERE AccTypeCode=@AccType);


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


--	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-07-01';
--
--	SET @nYear = YEAR(@nDate);
--
--	IF MONTH(@nDate) < 7
--		BEGIN
--			SET @nYear = @nYear - 1;
--
--			SET @opDate = CAST(@nYear AS VARCHAR(4)) + '-07-01';
--		END	

	SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZCSOPBALANCE';

    SET @strSQL = 'UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ' +
		'ISNULL((SELECT SUM(TrnAmount) FROM ' + @openTable +  
		' WHERE A2ZCSOPBALANCE.CuType = A2ZACCOUNT.CuType ' + 
		' AND A2ZCSOPBALANCE.CuNo = A2ZACCOUNT.CuNo' +  
		' AND A2ZCSOPBALANCE.MemNo = A2ZACCOUNT.MemNo' + 
		' AND A2ZCSOPBALANCE.AccType = A2ZACCOUNT.AccType' + 
		' AND A2ZCSOPBALANCE.AccNo = A2ZACCOUNT.AccNo' + 
		'),0) FROM A2ZACCOUNT,' + @openTable +
		' WHERE A2ZACCOUNT.CuType = A2ZCSOPBALANCE.CuType'  +  
		' AND A2ZACCOUNT.CuNo = A2ZCSOPBALANCE.CuNo' + 
		' AND A2ZACCOUNT.MemNo = A2ZCSOPBALANCE.MemNo' +  
		' AND A2ZACCOUNT.AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
		' AND A2ZACCOUNT.AccNo = A2ZCSOPBALANCE.AccNo';

	EXECUTE (@strSQL);

--	IF MONTH(@nDate) <> 7
--		BEGIN
			EXECUTE Sp_CSGenerateTransactionDataAType @AccType,@opDate,@fDate,0;
---------- Credit
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1 AND WFA2ZTRANSACTION.TrnCredit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;
---------- Debit
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(GLDebitAmt) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 0 AND WFA2ZTRANSACTION.TrnDebit != 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=WFA2ZTRANSACTION.CuType AND A2ZACCOUNT.CuNo = WFA2ZTRANSACTION.CuNo AND 
			A2ZACCOUNT.MemNo = WFA2ZTRANSACTION.MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = WFA2ZTRANSACTION.AccNo;
--		END		

----- Generate Work Table for Ledger Balance
	TRUNCATE TABLE WFCSLEDGERBALANCE;
	
	INSERT INTO WFCSLEDGERBALANCE (CuType,CuNo,MemNo,AccNo,OpenDate,LastTrnDate,Balance,AccType,AccStatus,AccStatusDate)
	SELECT CuType,CuNo,MemNo,AccNo,AccOpenDate,AccLastTrnDateU,AccOpBal,AccType,AccStatus,AccStatusDate
	FROM A2ZACCOUNT WHERE AccType = @AccType;

     
       


	IF @CuType > 0
		BEGIN
			DELETE FROM WFCSLEDGERBALANCE WHERE CuType <> @CuType;
			DELETE FROM WFCSLEDGERBALANCE WHERE CuNo <> @CuNo;
		END

	IF @AccStatFlag = 0
		BEGIN
			DELETE FROM WFCSLEDGERBALANCE WHERE AccStatus > 97 AND AccStatusDate <= @fDate;
		END

    IF @AccStatFlag > 0
		BEGIN
			DELETE FROM WFCSLEDGERBALANCE WHERE AccStatus <> @AccStatFlag;
		END

    IF @AccStatFlag = 0 
		BEGIN
			UPDATE WFCSLEDGERBALANCE SET AccStatus = 1 WHERE
	          WFCSLEDGERBALANCE.AccStatus = 99;
		END
    

	IF @BalanceFlag = 2
		BEGIN
			DELETE FROM WFCSLEDGERBALANCE WHERE Balance !=0;
		END

	IF @BalanceFlag = 1 and (@acctypeclass = 5 or @acctypeclass = 6)
		BEGIN
			DELETE FROM WFCSLEDGERBALANCE WHERE ABS(Balance) < @fBalance;

			DELETE FROM WFCSLEDGERBALANCE WHERE ABS(Balance) > @tBalance;
		END
     ELSE
     IF @BalanceFlag = 1 and (@acctypeclass != 5 AND @acctypeclass != 6)
		BEGIN
			DELETE FROM WFCSLEDGERBALANCE WHERE Balance < @fBalance;

			DELETE FROM WFCSLEDGERBALANCE WHERE Balance > @tBalance;
		END



    IF @CodeType > 0
       BEGIN
			DELETE FROM WFCSLEDGERBALANCE WHERE right(AccNo, 4) !=@CodeType;
		END

            
----- End of Generate Work Table for Ledger Balance

----- Update Member Name/Status Description --------
	UPDATE WFCSLEDGERBALANCE SET MemName = (SELECT A2ZMEMBER.MemName FROM A2ZMEMBER WHERE 
	A2ZMEMBER.CuType = WFCSLEDGERBALANCE.CuType AND A2ZMEMBER.CuNo = WFCSLEDGERBALANCE.CuNo AND 
	A2ZMEMBER.MemNo = WFCSLEDGERBALANCE.MemNo);
    
    

    UPDATE WFCSLEDGERBALANCE SET GLCashCode = (SELECT A2ZCUNION.GLCashCode FROM A2ZCUNION WHERE 
	A2ZCUNION.CuType = WFCSLEDGERBALANCE.CuType AND A2ZCUNION.CuNo = WFCSLEDGERBALANCE.CuNo);
     
   

	UPDATE WFCSLEDGERBALANCE SET Status = (SELECT AccStatusDescription FROM A2ZACCSTATUS WHERE
	WFCSLEDGERBALANCE.AccStatus = A2ZACCSTATUS.AccStatusCode);


----- End of Update Member Name/Status Description --------

END

GO
