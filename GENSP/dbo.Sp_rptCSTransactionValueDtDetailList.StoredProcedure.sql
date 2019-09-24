USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptCSTransactionValueDtDetailList]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
EXECUTE Sp_rptCSTransactionValueDtDetailList '2015-03-05','2015-03-05'
*/


CREATE PROCEDURE [dbo].[Sp_rptCSTransactionValueDtDetailList] (@fDate varchar(10), @tDate varchar(10))
AS
BEGIN
SELECT     dbo.WFTRANSACTIONLIST.CuNo, dbo.WFTRANSACTIONLIST.CuType, dbo.WFTRANSACTIONLIST.MemNo, dbo.A2ZMEMBER.MemName, 
                      dbo.WFTRANSACTIONLIST.VoucherNo, dbo.WFTRANSACTIONLIST.AccType, dbo.WFTRANSACTIONLIST.TrnDate, dbo.WFTRANSACTIONLIST.TrnCredit, 
                      dbo.WFTRANSACTIONLIST.TrnDebit, dbo.WFTRANSACTIONLIST.TrnDesc, dbo.WFTRANSACTIONLIST.AccNo, dbo.WFTRANSACTIONLIST.TrnType, 
                      dbo.A2ZTRNTYPE.TrnTypeDes, dbo.WFTRANSACTIONLIST.PayType, dbo.A2ZPAYTYPE.PayTypeDes, dbo.WFTRANSACTIONLIST.TrnFlag, 
                      dbo.WFTRANSACTIONLIST.TrnCSGL, dbo.WFTRANSACTIONLIST.UserID, dbo.WFTRANSACTIONLIST.VerifyUserID, dbo.A2ZACCTYPE.AccTypeDescription, 
                      dbo.WFTRANSACTIONLIST.TrnProcStat, dbo.WFTRANSACTIONLIST.TrnModule, dbo.WFTRANSACTIONLIST.FromCashCode, dbo.WFTRANSACTIONLIST.VchNo, 
                      dbo.WFTRANSACTIONLIST.ValueDate, dbo.WFTRANSACTIONLIST.TrnSysUser, dbo.WFTRANSACTIONLIST.TrnChqPrx, dbo.WFTRANSACTIONLIST.TrnChqNo
FROM         dbo.WFTRANSACTIONLIST LEFT OUTER JOIN
                      dbo.A2ZPAYTYPE ON dbo.WFTRANSACTIONLIST.PayType = dbo.A2ZPAYTYPE.PayType LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.WFTRANSACTIONLIST.AccType = dbo.A2ZACCTYPE.AccTypeCode LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.WFTRANSACTIONLIST.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.WFTRANSACTIONLIST.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.WFTRANSACTIONLIST.CuType = dbo.A2ZMEMBER.CuType LEFT OUTER JOIN
                      dbo.A2ZTRNTYPE ON dbo.WFTRANSACTIONLIST.TrnType = dbo.A2ZTRNTYPE.TrnType
WHERE  (( (dbo.WFTRANSACTIONLIST.TrnDate BETWEEN @fDate AND @tDate) AND (dbo.WFTRANSACTIONLIST.TrnDate)<>  dbo.WFTRANSACTIONLIST.ValueDate AND  (dbo.WFTRANSACTIONLIST.TrnFlag = 0)) AND
(dbo.WFTRANSACTIONLIST.TrnDebit <>0 OR dbo.WFTRANSACTIONLIST.TrnCredit <>0 ) ) 
END

GO
