
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GLGenerateOpeningBalanceSingle](@AccountCode INT, @fDate VARCHAR(10), @nFlag INT)
AS
/*
EXECUTE Sp_GLGenerateOpeningBalanceSingle 40122001,'2015-04-03',0



*/

BEGIN

    UPDATE A2ZCGLMST SET GLOpBal = 0 WHERE GLAccNo = @AccountCode; 	
	
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30)

    DECLARE @BegYear int;
    DECLARE @IYear int;


	DECLARE @nYear INT;	

	SET @nDate = @fDate;

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZGLPARAMETER);

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

	SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZGLOPBALANCE';

	SET @strSQL = 'UPDATE A2ZCGLMST SET A2ZCGLMST.GLOpBal = ' + 
		'ISNULL((SELECT SUM(GLOpBal) FROM ' + @openTable +
		' WHERE ' + @openTable + '.GLAccNo=' + CAST(@AccountCode AS VARCHAR(8)) + '),0)' +
		' FROM A2ZCGLMST,' + @openTable + 
		' WHERE ' + @openTable + '.GLAccNo=' + CAST(@AccountCode AS VARCHAR(8));
	EXECUTE (@strSQL);

	
--    IF MONTH(@nDate) <> 7
--		BEGIN

            IF @nFlag = 0
               BEGIN
			       EXECUTE Sp_GLGenerateTransactionDataSingle @AccountCode, @opDate,@fDate,0;
               END
            ELSE
               BEGIN
                   EXECUTE Sp_GLGenerateStatementDataSingle @AccountCode, @opDate,@fDate,0;
               END           
			
			SET @strSQL = 'DELETE FROM WFA2ZTRANSACTION WHERE TrnDate = ''' + @fDate + '''';			
            EXECUTE (@strSQL);


			UPDATE A2ZCGLMST SET A2ZCGLMST.GLOpBal = A2ZCGLMST.GLOpBal + 
			ISNULL((SELECT SUM(GLAmount) FROM WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.GLAccNo=@AccountCode),0)
			FROM A2ZCGLMST,WFA2ZTRANSACTION
			WHERE WFA2ZTRANSACTION.GLAccNo=@AccountCode;
--		END		


END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

