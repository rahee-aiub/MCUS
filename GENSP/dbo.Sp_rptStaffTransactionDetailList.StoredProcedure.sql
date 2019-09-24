USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptStaffTransactionDetailList]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
EXECUTE Sp_rptCSTransactionDetailList '2014-12-15','2014-12-15',1,20
*/


CREATE PROCEDURE [dbo].[Sp_rptStaffTransactionDetailList] (@fDate varchar(10), @tDate varchar(10),@CommonNo8 int)
AS
BEGIN
SELECT     dbo.WFTRANSACTIONLIST.CuNo, dbo.WFTRANSACTIONLIST.CuType, dbo.WFTRANSACTIONLIST.MemNo, dbo.A2ZMEMBER.MemName, 
                      dbo.WFTRANSACTIONLIST.VoucherNo, dbo.WFTRANSACTIONLIST.AccType, dbo.WFTRANSACTIONLIST.TrnDate, dbo.WFTRANSACTIONLIST.TrnCredit, 
                      dbo.WFTRANSACTIONLIST.TrnDebit, dbo.WFTRANSACTIONLIST.TrnDesc, CAST(dbo.WFTRANSACTIONLIST.AccNo AS VARCHAR(16)) AS AccNo,dbo.WFTRANSACTIONLIST.TrnType, 
                      dbo.A2ZTRNTYPE.TrnTypeDes, dbo.WFTRANSACTIONLIST.PayType, dbo.A2ZPAYTYPE.PayTypeDes, dbo.WFTRANSACTIONLIST.TrnFlag, 
                      dbo.WFTRANSACTIONLIST.TrnCSGL, dbo.WFTRANSACTIONLIST.UserID, dbo.A2ZACCTYPE.AccTypeDescription, dbo.WFTRANSACTIONLIST.TrnProcStat,dbo.WFTRANSACTIONLIST.TrnModule,dbo.WFTRANSACTIONLIST.VchNo
FROM         dbo.WFTRANSACTIONLIST LEFT OUTER JOIN
                      dbo.A2ZPAYTYPE ON dbo.WFTRANSACTIONLIST.PayType = dbo.A2ZPAYTYPE.PayType LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.WFTRANSACTIONLIST.AccType = dbo.A2ZACCTYPE.AccTypeCode LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.WFTRANSACTIONLIST.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.WFTRANSACTIONLIST.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.WFTRANSACTIONLIST.CuType = dbo.A2ZMEMBER.CuType LEFT OUTER JOIN
                      dbo.A2ZTRNTYPE ON dbo.WFTRANSACTIONLIST.TrnType = dbo.A2ZTRNTYPE.TrnType

WHERE  (dbo.WFTRANSACTIONLIST.RepUserID = @CommonNo8) 


--
--WHERE  (( (dbo.WFTRANSACTIONLIST.TrnDate BETWEEN @fDate AND @tDate) AND (dbo.WFTRANSACTIONLIST.TrnFlag = 0 AND dbo.WFTRANSACTIONLIST.TrnCSGL = 0 AND dbo.WFTRANSACTIONLIST.TrnProcStat = 0 AND dbo.WFTRANSACTIONLIST.ShowInterest = 0)) AND
--(dbo.WFTRANSACTIONLIST.TrnDebit <>0 OR dbo.WFTRANSACTIONLIST.TrnCredit <>0 ) ) 


END

GO
