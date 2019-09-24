USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptGLTransactionSummaryList]    Script Date: 04/29/2015 15:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*
EXECUTE Sp_rptGLTransactionSummaryList '2014-12-15','2014-12-15'
*/


CREATE  PROCEDURE [dbo].[Sp_rptGLTransactionSummaryList] (@fDate varchar(10), @tDate varchar(10))
AS
BEGIN
SELECT     dbo.WFTRANSACTIONLIST.TrnDate, dbo.WFTRANSACTIONLIST.GLAccNo, SUM(dbo.WFTRANSACTIONLIST.GLDebitAmt) AS DebitAmt, 
                      SUM(dbo.WFTRANSACTIONLIST.GLCreditAmt) AS CreditAmt, dbo.A2ZCGLMST.GLAccDesc, dbo.WFTRANSACTIONLIST.TrnProcStat, 
                      dbo.WFTRANSACTIONLIST.TrnType
FROM         dbo.WFTRANSACTIONLIST LEFT OUTER JOIN
                      dbo.A2ZCGLMST ON dbo.WFTRANSACTIONLIST.GLAccNo = dbo.A2ZCGLMST.GLAccNo
WHERE     (dbo.WFTRANSACTIONLIST.TrnDate BETWEEN @fDate AND @tDate) AND (dbo.WFTRANSACTIONLIST.TrnProcStat <> 1) AND (dbo.WFTRANSACTIONLIST.TrnType <> 0)
GROUP BY dbo.WFTRANSACTIONLIST.TrnDate, dbo.WFTRANSACTIONLIST.GLAccNo, dbo.A2ZCGLMST.GLAccDesc, dbo.WFTRANSACTIONLIST.TrnProcStat, 
                      dbo.WFTRANSACTIONLIST.TrnType
END


