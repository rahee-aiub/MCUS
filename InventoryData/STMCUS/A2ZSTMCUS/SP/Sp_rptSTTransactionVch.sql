USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptSTTransactionVch]    Script Date: 6/23/2018 10:06:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*
EXECUTE Sp_rptSTTransactionVch 1, '2018-02-01' 
*/


CREATE PROCEDURE [dbo].[Sp_rptSTTransactionVch] (@CommonNo1 tinyint,@fDate nvarchar(10)) 
AS
BEGIN

DECLARE @trnDate SMALLDATETIME;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nYear INT;	

SET @trnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);



SET @nYear = YEAR(@fDate);


IF YEAR(@trnDate) = YEAR(@fDate) AND MONTH(@trnDate) = MONTH(@fDate) AND DAY(@trnDate) = DAY(@fDate)
   BEGIN

        SELECT     TOP (100) PERCENT A2ZCSMCUS.dbo.A2ZTRANSACTION.VchNo, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnFlag, SUM(A2ZCSMCUS.dbo.A2ZTRANSACTION.GLDebitAmt) AS GLDebitAmt, 
                   SUM(A2ZCSMCUS.dbo.A2ZTRANSACTION.GLCreditAmt) AS GLCreditAmt, A2ZCSMCUS.dbo.A2ZTRANSACTION.UserID, A2ZCSMCUS.dbo.A2ZTRANSACTION.VerifyUserID, 
                   A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, A2ZCSMCUS.dbo.A2ZTRANSACTION.GLAccNo, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnCSGL, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnDate 
                      
        FROM       A2ZCSMCUS.dbo.A2ZTRANSACTION LEFT OUTER JOIN
                   A2ZGLMCUS.dbo.A2ZCGLMST ON A2ZCSMCUS.dbo.A2ZTRANSACTION.GLAccNo = A2ZGLMCUS.dbo.A2ZCGLMST.GLAccNo
        WHERE     (A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnCSGL = @CommonNo1)

        GROUP BY A2ZCSMCUS.dbo.A2ZTRANSACTION.VchNo, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnFlag, A2ZCSMCUS.dbo.A2ZTRANSACTION.UserID, A2ZCSMCUS.dbo.A2ZTRANSACTION.VerifyUserID, 
                      A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, A2ZCSMCUS.dbo.A2ZTRANSACTION.GLAccNo, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnCSGL, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnDate 
                     
        ORDER BY A2ZCSMCUS.dbo.A2ZTRANSACTION.GLAccNo
  END

ELSE
  BEGIN

       SET @strSQL = 'SELECT A2ZGLMCUS.dbo.A2ZCGLMST.GLAccNo, A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.VchNo,' +
	                 ' A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.TrnFlag,' +
	                 ' SUM(A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.GLDebitAmt) AS GLDebitAmt,' +
                     ' SUM(A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.GLCreditAmt) AS GLCreditAmt,' +
	                 ' A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.UserID, A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + 
	                 '.dbo.A2ZTRANSACTION.VerifyUserID, A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + 
	                 '.dbo.A2ZTRANSACTION.TrnCSGL, A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.TrnDate' +
	                 ' FROM A2ZGLMCUS.dbo.A2ZCGLMST INNER JOIN' +
	                 ' A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION ON' +
	                 ' A2ZGLMCUS.dbo.A2ZCGLMST.GLAccNo = A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.GLAccNo' +
	                 ' WHERE A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.TrnCSGL = ' + CAST(@CommonNo1 AS VARCHAR(4)) +
	                 ' AND A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.TrnDate' +
	                 ' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @fDate + '''' +
	                 ' GROUP BY A2ZGLMCUS.dbo.A2ZCGLMST.GLAccNo, A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.VchNo,' +
	                 ' A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.TrnFlag, A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) +
	                 '.dbo.A2ZTRANSACTION.UserID, A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + 
	                 '.dbo.A2ZTRANSACTION.VerifyUserID, A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + 
	                 '.dbo.A2ZTRANSACTION.TrnCSGL, A2ZCSMCUST' + CAST(@nYear AS VARCHAR(4)) + '.dbo.A2ZTRANSACTION.TrnDate';
	
	   EXECUTE (@strSQL);

   END



END





GO

