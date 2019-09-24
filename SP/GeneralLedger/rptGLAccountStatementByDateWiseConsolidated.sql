USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptGLAccountStatementByDateWiseConsolidated]    Script Date: 04/29/2015 15:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






/*
EXECUTE Sp_rptGLAccountStatementVchConsoleted '2014-12-15','2014-12-15'
*/

CREATE PROCEDURE [dbo].[Sp_rptGLAccountStatementByDateWiseConsolidated] (@fDate varchar(10), @tDate varchar(10), @CommonNo1 INT )
AS
BEGIN
SELECT     TrnDate, GLAccNo, SUM(GLCreditAmt) AS GLCreditAmt, SUM(GLDebitAmt) AS GLDebitAmt, TrnProcStat, TrnType
FROM         dbo.WFGLSTATEMENT
WHERE     (TrnDate BETWEEN @fDate AND @tDate) AND (GLAccNo = @CommonNo1) AND (TrnProcStat <> 1)
GROUP BY TrnDate, GLAccNo, TrnProcStat, TrnType

END




























