
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSAccountLedgerBalance](@AccType INT, @fDate VARCHAR(10))
AS
/*
							

EXECUTE Sp_CSAccountLedgerBalance 12,'2016-07-02'


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

    PRINT @IYear;
    PRINT @BegYear;


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
    
    PRINT @opDate;

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
        UPDATE A2ZACCOUNT SET AccOpBal = 0 WHERE AccType = @AccType AND AccStatus > 97 AND AccStatusDate <= @fDate; 

       	
	
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

