USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGenerateOpeningBalanceAType]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_CSGenerateOpeningBalanceAType](@AccType INT,@fDate VARCHAR(10),@nFlag INT)
AS

/*
Opening Balance for All Account

EXECUTE Sp_CSGenerateOpeningBalanceAType 11,'2015-07-01',0

*/

IF ISNULL(@accType,0) = 0
	BEGIN
		RAISERROR ('Account Type is Null',10,1)
		RETURN;
	END
BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

	UPDATE A2ZACCOUNT SET AccOpBal = 0 WHERE AccType = @AccType; 
	
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30)

	DECLARE @nYear INT;	

	SET @nDate = @fDate;

	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-07-01';

	SET @nYear = YEAR(@nDate);

	IF MONTH(@nDate) < 7
		BEGIN
			SET @nYear = @nYear - 1;

			SET @opDate = CAST(@nYear AS VARCHAR(4)) + '-07-01';
		END	

	SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZCSOPBALANCE';

	SET @strSQL = 'UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = ' +
		'ISNULL((SELECT SUM(TrnAmount) FROM ' + @openTable +  
		' WHERE A2ZACCOUNT.CuType = ' + @openTable + '.CuType ' +
		' AND A2ZACCOUNT.CuNo = ' + @openTable + '.CuNo ' +
		' AND A2ZACCOUNT.MemNo = ' + @openTable + '.MemNo ' +
		' AND A2ZACCOUNT.AccNo = ' + @openTable + '.AccNo ' +
		' AND A2ZACCOUNT.AccType = ' + @openTable + '.AccType ' +
		'),0) FROM A2ZACCOUNT,' + @openTable +
		' WHERE A2ZACCOUNT.CuType = ' + @openTable + '.CuType ' +
		' AND A2ZACCOUNT.CuNo = ' + @openTable + '.CuNo ' + 
		' AND A2ZACCOUNT.MemNo = ' + @openTable + '.MemNo ' +
		' AND A2ZACCOUNT.AccNo = ' + @openTable + '.AccNo ' +
		' AND A2ZACCOUNT.AccType = ' + CAST(@AccType AS VARCHAR(2));
	
---	PRINT @strSQL;

	EXECUTE (@strSQL);

--	IF MONTH(@nDate) <> 7
--		BEGIN

			EXECUTE Sp_CSGenerateTransactionDataAType @AccType,@opDate,@fDate,0;
			
            SET @strSQL = 'DELETE FROM WFA2ZTRANSACTION WHERE TrnDate = ''' + @fDate + '''';			
			EXECUTE (@strSQL);

---------- Credit
			UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal + 
			ISNULL((SELECT SUM(TrnCredit) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND 
			WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND 
			WFA2ZTRANSACTION.AccType = A2ZACCOUNT.AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			WFA2ZTRANSACTION.TrnCSGL = 0 AND 
			WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND 
			WFA2ZTRANSACTION.TrnProcStat = 0 AND 
			WFA2ZTRANSACTION.TrnDrCr = 1),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND 
			WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND 
			WFA2ZTRANSACTION.AccType = A2ZACCOUNT.AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			WFA2ZTRANSACTION.TrnCSGL = 0 AND 
			WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND 
			WFA2ZTRANSACTION.TrnProcStat = 0 AND 
			WFA2ZTRANSACTION.TrnDrCr = 1;
---------- Debit
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccOpBal = A2ZACCOUNT.AccOpBal - 
			ISNULL((SELECT SUM(TrnDebit) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND 
			WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND 
			WFA2ZTRANSACTION.AccType = A2ZACCOUNT.AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			WFA2ZTRANSACTION.TrnCSGL = 0 AND 
			WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND 
			WFA2ZTRANSACTION.TrnProcStat = 0 AND 
			WFA2ZTRANSACTION.TrnDrCr = 0),0)
			FROM A2ZACCOUNT,WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.CuType = A2ZACCOUNT.CuType AND 
			WFA2ZTRANSACTION.CuNo = A2ZACCOUNT.CuNo AND 
			WFA2ZTRANSACTION.MemNo = A2ZACCOUNT.MemNo AND 
			WFA2ZTRANSACTION.AccType = A2ZACCOUNT.AccType AND 
			WFA2ZTRANSACTION.AccNo = A2ZACCOUNT.AccNo AND 
			WFA2ZTRANSACTION.TrnCSGL = 0 AND 
			WFA2ZTRANSACTION.ShowInterest = 0 AND 
			WFA2ZTRANSACTION.TrnFlag = 0 AND 
			WFA2ZTRANSACTION.TrnProcStat = 0 AND 
			WFA2ZTRANSACTION.TrnDrCr = 0;
--		END	

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
	




GO
