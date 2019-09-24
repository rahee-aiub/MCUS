
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
EXECUTE Sp_rptGLAccountStatementVchConsoleted '2014-12-15','2014-12-15'
*/

ALTER PROCEDURE [dbo].[Sp_rptGLAccountStatementByDateWiseConsolidated] (@fDate varchar(10), @tDate varchar(10), @CommonNo1 INT )
AS
BEGIN
SELECT     TrnDate, GLAccNo, SUM(GLCreditAmt) AS GLCreditAmt, SUM(GLDebitAmt) AS GLDebitAmt, TrnProcStat, TrnType
FROM         dbo.WFGLSTATEMENT
WHERE     (TrnDate BETWEEN @fDate AND @tDate) AND (GLAccNo = @CommonNo1) AND (TrnProcStat <> 1) AND (TrnType <> 0)
GROUP BY TrnDate, GLAccNo, TrnProcStat, TrnType

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

