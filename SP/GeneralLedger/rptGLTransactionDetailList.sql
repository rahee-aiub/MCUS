USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptGLTransactionDetailList]    Script Date: 04/29/2015 15:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/*
EXECUTE Sp_rptGLTransactionDetailList '2014-12-15','2014-12-15'
*/


CREATE PROCEDURE [dbo].[Sp_rptGLTransactionDetailList] (@fDate varchar(10), @tDate varchar(10))
AS
BEGIN

SELECT     dbo.WFTRANSACTIONLIST.TrnFlag, dbo.WFTRANSACTIONLIST.UserID, dbo.WFTRANSACTIONLIST.TrnDate, dbo.WFTRANSACTIONLIST.GLAccNo, 
                      dbo.WFTRANSACTIONLIST.VoucherNo, dbo.WFTRANSACTIONLIST.CuType, dbo.WFTRANSACTIONLIST.CuNo, dbo.WFTRANSACTIONLIST.TrnDesc, 
                      dbo.WFTRANSACTIONLIST.GLDebitAmt, dbo.WFTRANSACTIONLIST.GLCreditAmt, dbo.WFTRANSACTIONLIST.BatchNo, dbo.A2ZCGLMST.GLAccDesc, 
                      dbo.WFTRANSACTIONLIST.TrnProcStat, dbo.WFTRANSACTIONLIST.TrnType
FROM         dbo.A2ZCGLMST RIGHT OUTER JOIN
                      dbo.WFTRANSACTIONLIST ON dbo.A2ZCGLMST.GLAccNo = dbo.WFTRANSACTIONLIST.GLAccNo
WHERE   (dbo.WFTRANSACTIONLIST.TrnDate BETWEEN @fDate AND @tDate) AND  dbo.WFTRANSACTIONLIST.TrnProcStat <> 1  AND  dbo.WFTRANSACTIONLIST.TrnType <> 0
END











