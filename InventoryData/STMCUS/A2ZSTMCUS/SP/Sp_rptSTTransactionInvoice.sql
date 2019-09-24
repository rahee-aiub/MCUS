USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptSTTransactionInvoice]    Script Date: 6/23/2018 10:06:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/*
EXECUTE Sp_rptSTTransactionInvoice '2018-03-01' 
*/


CREATE PROCEDURE [dbo].[Sp_rptSTTransactionInvoice] (@fDate nvarchar(10)) 
AS
BEGIN

DECLARE @trnDate SMALLDATETIME;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nYear INT;	

SET @trnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);



SET @nYear = YEAR(@fDate);


IF YEAR(@trnDate) = YEAR(@fDate) AND MONTH(@trnDate) = MONTH(@fDate) AND DAY(@trnDate) = DAY(@fDate)
   BEGIN

        SELECT     *      
        FROM       A2ZSTTRANSACTION;
        
  END

ELSE
  BEGIN

        SET @strSQL = 'SELECT * FROM A2ZSTMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZSTTRANSACTION' +
		' WHERE A2ZSTMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZSTTRANSACTION.TransactionDate' +
	    ' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @fDate + '''';
								          
	   EXECUTE (@strSQL);

   END



END






GO

