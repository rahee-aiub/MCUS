USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptGLTransactionSummaryListByGLCode]    Script Date: 12/03/2016 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
EXECUTE Sp_rptGLTransactionSummaryListByGLCode '2014-12-15','2014-12-15'
*/

CREATE PROCEDURE [dbo].[Sp_rptGLTransactionSummaryListByGLCode] (@fDate varchar(10), @tDate varchar(10))
AS
BEGIN

DELETE FROM WFTRANSACTIONLIST WHERE TrnType = 0 OR FromCashCode = 0;


SELECT     dbo.WFTRANSACTIONLIST.GLAccNo, SUM(dbo.WFTRANSACTIONLIST.GLDebitAmt) AS DebitAmt, 
                      SUM(dbo.WFTRANSACTIONLIST.GLCreditAmt) AS CreditAmt, dbo.A2ZCGLMST.GLAccDesc, dbo.WFTRANSACTIONLIST.TrnProcStat 
                     
FROM         dbo.WFTRANSACTIONLIST LEFT OUTER JOIN
                      dbo.A2ZCGLMST ON dbo.WFTRANSACTIONLIST.GLAccNo = dbo.A2ZCGLMST.GLAccNo
WHERE     (dbo.WFTRANSACTIONLIST.TrnProcStat <> 1) 
                      
GROUP BY dbo.WFTRANSACTIONLIST.GLAccNo, dbo.A2ZCGLMST.GLAccDesc, dbo.WFTRANSACTIONLIST.TrnProcStat 
                      
ORDER BY dbo.WFTRANSACTIONLIST.GLAccNo
END

