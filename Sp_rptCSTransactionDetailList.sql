
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
EXECUTE Sp_rptCSTransactionDetailList '2016-02-01','2016-02-01', 0
*/


ALTER PROCEDURE [dbo].[Sp_rptCSTransactionDetailList](@CommonNo8 int)
AS
BEGIN


           SELECT     dbo.WFTRANSACTIONLIST.CuNo, dbo.WFTRANSACTIONLIST.CuType, dbo.WFTRANSACTIONLIST.MemNo, dbo.A2ZMEMBER.MemName, 
                      dbo.WFTRANSACTIONLIST.VoucherNo, dbo.WFTRANSACTIONLIST.AccType, dbo.WFTRANSACTIONLIST.TrnDate, dbo.WFTRANSACTIONLIST.TrnCredit, 
                      dbo.WFTRANSACTIONLIST.TrnDebit, dbo.WFTRANSACTIONLIST.TrnDesc, CAST(dbo.WFTRANSACTIONLIST.AccNo AS VARCHAR(16)) AS AccNo, 
                      dbo.WFTRANSACTIONLIST.TrnType, dbo.WFTRANSACTIONLIST.TrnSysUser, dbo.A2ZTRNTYPE.TrnTypeDes, dbo.WFTRANSACTIONLIST.PayType, 
                      dbo.A2ZPAYTYPE.PayTypeDes, dbo.WFTRANSACTIONLIST.TrnFlag, dbo.WFTRANSACTIONLIST.TrnCSGL, dbo.WFTRANSACTIONLIST.UserID, 
                      dbo.WFTRANSACTIONLIST.VerifyUserID, dbo.A2ZACCTYPE.AccTypeDescription, dbo.WFTRANSACTIONLIST.TrnProcStat, dbo.WFTRANSACTIONLIST.TrnModule, 
                      dbo.WFTRANSACTIONLIST.FromCashCode, dbo.WFTRANSACTIONLIST.VchNo, dbo.WFTRANSACTIONLIST.ValueDate, dbo.WFTRANSACTIONLIST.TrnChqNo, 
                      dbo.WFTRANSACTIONLIST.TrnChqPrx,dbo.WFTRANSACTIONLIST.TrnInterestAmt
           FROM         dbo.WFTRANSACTIONLIST LEFT OUTER JOIN
                      dbo.A2ZPAYTYPE ON dbo.WFTRANSACTIONLIST.PayType = dbo.A2ZPAYTYPE.PayType LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.WFTRANSACTIONLIST.AccType = dbo.A2ZACCTYPE.AccTypeCode LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.WFTRANSACTIONLIST.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.WFTRANSACTIONLIST.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.WFTRANSACTIONLIST.CuType = dbo.A2ZMEMBER.CuType LEFT OUTER JOIN
                      dbo.A2ZTRNTYPE ON dbo.WFTRANSACTIONLIST.TrnType = dbo.A2ZTRNTYPE.TrnType
           WHERE  (dbo.WFTRANSACTIONLIST.RepUserID = @CommonNo8) 
  

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

