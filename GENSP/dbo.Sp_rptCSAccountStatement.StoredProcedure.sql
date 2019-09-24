USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptCSAccountStatement]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





/*
EXECUTE Sp_rptCSTransactionDetailList '2014-12-15','2014-12-15',1,20
*/

CREATE  PROCEDURE [dbo].[Sp_rptCSAccountStatement] (@fDate varchar(10), @tDate varchar(10), @CuNo Int, @CuType smallInt, @AccType Int, @MemNo Int)
AS
BEGIN
SELECT     dbo.WFCSSTATEMENT.CuType, dbo.WFCSSTATEMENT.MemNo, dbo.A2ZMEMBER.MemName, dbo.WFCSSTATEMENT.VchNo, dbo.WFCSSTATEMENT.VoucherNo, 
                      dbo.WFCSSTATEMENT.AccType, dbo.WFCSSTATEMENT.TrnDate, dbo.WFCSSTATEMENT.TrnCredit, dbo.WFCSSTATEMENT.TrnDebit, dbo.WFCSSTATEMENT.TrnDesc, 
                      dbo.WFCSSTATEMENT.AccNo, dbo.WFCSSTATEMENT.TrnType, dbo.A2ZTRNTYPE.TrnTypeDes, dbo.WFCSSTATEMENT.PayType, dbo.A2ZPAYTYPE.PayTypeDes, 
                      dbo.WFCSSTATEMENT.TrnFlag, dbo.WFCSSTATEMENT.TrnCSGL, dbo.WFCSSTATEMENT.UserID, dbo.A2ZACCTYPE.AccTypeDescription, 
                      dbo.WFCSSTATEMENT.ShowInterest, dbo.WFCSSTATEMENT.TrnProcStat, dbo.A2ZACCOUNT.AccAnniDate, dbo.WFCSSTATEMENT.ValueDate,
                      dbo.WFCSSTATEMENT.FuncOpt, dbo.WFCSSTATEMENT.TrnChqNo
FROM         dbo.A2ZTRNTYPE RIGHT OUTER JOIN
                      dbo.WFCSSTATEMENT LEFT OUTER JOIN
                      dbo.A2ZACCOUNT ON dbo.WFCSSTATEMENT.AccType = dbo.A2ZACCOUNT.AccType AND dbo.WFCSSTATEMENT.CuNo = dbo.A2ZACCOUNT.CuNo AND 
                      dbo.WFCSSTATEMENT.CuType = dbo.A2ZACCOUNT.CuType AND dbo.WFCSSTATEMENT.MemNo = dbo.A2ZACCOUNT.MemNo AND 
                      dbo.WFCSSTATEMENT.AccNo = dbo.A2ZACCOUNT.AccNo LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.WFCSSTATEMENT.AccType = dbo.A2ZACCTYPE.AccTypeCode ON 
                      dbo.A2ZTRNTYPE.TrnType = dbo.WFCSSTATEMENT.TrnType LEFT OUTER JOIN
                      dbo.A2ZPAYTYPE ON dbo.WFCSSTATEMENT.PayType = dbo.A2ZPAYTYPE.PayType LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.WFCSSTATEMENT.CuType = dbo.A2ZMEMBER.CuType AND dbo.WFCSSTATEMENT.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.WFCSSTATEMENT.MemNo = dbo.A2ZMEMBER.MemNo

WHERE     (dbo.WFCSSTATEMENT.TrnDate BETWEEN @fDate AND @tDate) AND (dbo.WFCSSTATEMENT.TrnFlag = 0) AND (dbo.WFCSSTATEMENT.TrnCSGL = 0) AND 
                      (dbo.WFCSSTATEMENT.CuNo = @CuNo) AND (dbo.WFCSSTATEMENT.CuType = @CuType) AND (dbo.WFCSSTATEMENT.MemNo = @MemNo) AND (dbo.WFCSSTATEMENT.AccType =  @AccType) AND 
                      (dbo.WFCSSTATEMENT.ShowInterest <> 1 AND dbo.WFCSSTATEMENT.TrnProcStat <> 1 )
                      
END
































GO
