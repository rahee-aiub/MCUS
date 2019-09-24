USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGenerateOpeningBalanceSingle]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSGenerateOpeningBalanceSingle](@CuType INT, @CuNo INT, @MemNo INT, @TrnCode INT, @AccType INT, @AccNo BIGINT, @fDate VARCHAR(10), @nFlag INT)
AS
/*
EXECUTE Sp_CSGenerateOpeningBalanceSingle 3,5,0,20201001,12,1230005000000001,'2015-10-01',0



*/

BEGIN

	UPDATE A2ZACCOUNT SET AccOpBal = 0 WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo; 
	
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @BegYear int;
    DECLARE @IYear int;


	DECLARE @nYear INT;	

	SET @nDate = @fDate;


    SET @BegYear = (SELECT FinancialBegYear FROM A2ZCSPARAMETER);

    SET @IYear = YEAR(@fDate);

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

	SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZCSOPBALANCE';

--	PRINT @openTable;
--	PRINT @CuType;
	
	SET @strSQL = 'UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ' +
		'ISNULL((SELECT SUM(TrnAmount) FROM ' + @openTable +  
		' WHERE CuType = ' + CAST(@CuType AS VARCHAR(2)) + 
		' AND CuNo = ' + CAST(@CuNo AS VARCHAR(6)) + 
		' AND MemNo = ' + CAST(@MemNo AS VARCHAR(6)) + 
		' AND AccType = ' + CAST(@AccType AS VARCHAR(2))  + 
		' AND AccNo = ' + CAST(@AccNo AS VARCHAR(16)) +
		'),0) FROM A2ZACCOUNT,' + @openTable +
		' WHERE A2ZACCOUNT.CuType = ' + CAST(@CuType AS VARCHAR(2))  + 
		' AND A2ZACCOUNT.CuNo = ' + CAST(@CuNo AS VARCHAR(6)) + 
		' AND A2ZACCOUNT.MemNo = ' + CAST(@MemNo AS VARCHAR(6)) + 
		' AND A2ZACCOUNT.AccType = ' + CAST(@AccType AS VARCHAR(2)) + 
		' AND A2ZACCOUNT.AccNo = ' + CAST(@AccNo AS VARCHAR(16));
	
--	PRINT @strSQL;

	EXECUTE (@strSQL);

--	IF MONTH(@nDate) <> 7
--		BEGIN
			EXECUTE Sp_CSGenerateTransactionDataSingle @CuType, @CuNo, @MemNo, @TrnCode, @AccType, @AccNo, @opDate,@fDate,0;

--            DELETE FROM WFA2ZTRANSACTION WHERE TrnDate = @fDate;	


            SET @strSQL = 'DELETE FROM WFA2ZTRANSACTION WHERE TrnDate = ''' + @fDate + '''';			
			EXECUTE (@strSQL);
---------- Credit
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
--			ISNULL((SELECT SUM(GLAmount) FROM WFA2ZTRANSACTION
            ISNULL((SELECT SUM(TrnCredit) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = @CuType AND WFA2ZTRANSACTION.CuNo = @CuNo AND 
			WFA2ZTRANSACTION.MemNo = @MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = @AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 1),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=@CuType AND A2ZACCOUNT.CuNo = @CuNo AND 
			A2ZACCOUNT.MemNo = @MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = @AccNo;
---------- Debit
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(TrnDebit) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = @CuType AND WFA2ZTRANSACTION.CuNo = @CuNo AND 
			WFA2ZTRANSACTION.MemNo = @MemNo AND WFA2ZTRANSACTION.AccType = @AccType AND 
			WFA2ZTRANSACTION.AccNo = @AccNo AND WFA2ZTRANSACTION.TrnCSGL = 0 AND WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND WFA2ZTRANSACTION.TrnProcStat = 0 AND WFA2ZTRANSACTION.TrnDrCr = 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE A2ZACCOUNT.CuType=@CuType AND A2ZACCOUNT.CuNo = @CuNo AND 
			A2ZACCOUNT.MemNo = @MemNo AND A2ZACCOUNT.AccType = @AccType AND 
			A2ZACCOUNT.AccNo = @AccNo;
--		END		
END

GO
