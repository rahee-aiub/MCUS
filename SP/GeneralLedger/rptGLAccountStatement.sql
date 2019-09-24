USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptGLAccountStatement]    Script Date: 04/29/2015 15:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/*
EXECUTE Sp_rptGLAccountStatement '2014-12-15','2014-12-15'
*/

CREATE PROCEDURE [dbo].[Sp_rptGLAccountStatement] (@fDate varchar(10), @tDate varchar(10), @CommonNo1 INT )
AS
BEGIN
SELECT     dbo.WFGLSTATEMENT.TrnFlag, dbo.WFGLSTATEMENT.UserID, dbo.WFGLSTATEMENT.TrnDate, dbo.WFGLSTATEMENT.GLAccNo, dbo.WFGLSTATEMENT.VoucherNo, 
                      dbo.WFGLSTATEMENT.CuType, dbo.WFGLSTATEMENT.CuNo, dbo.WFGLSTATEMENT.TrnDesc, dbo.WFGLSTATEMENT.GLDebitAmt, 
                      dbo.WFGLSTATEMENT.GLCreditAmt, dbo.WFGLSTATEMENT.BatchNo, dbo.A2ZCGLMST.GLAccDesc, dbo.WFGLSTATEMENT.TrnProcStat, 
                      dbo.WFGLSTATEMENT.TrnType, dbo.WFGLSTATEMENT.TrnGLAccNoDr, dbo.WFGLSTATEMENT.TrnGLAccNoCr, dbo.WFGLSTATEMENT.TrnCSGL, 
                      dbo.WFGLSTATEMENT.GLAmount
FROM         dbo.A2ZCGLMST RIGHT OUTER JOIN
                      dbo.WFGLSTATEMENT ON dbo.A2ZCGLMST.GLAccNo = dbo.WFGLSTATEMENT.GLAccNo
WHERE   (dbo.WFGLSTATEMENT.TrnDate BETWEEN @fDate AND @tDate) AND dbo.WFGLSTATEMENT.GLAccNo = @CommonNo1  AND  dbo.WFGLSTATEMENT.TrnProcStat <> 1 
END
















