USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptApproveLoanReport]    Script Date: 09/03/2018 11:02:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [dbo].[Sp_rptApproveLoanReport](@CommonNo2 int)

AS
BEGIN

SELECT     dbo.A2ZLOAN.LoanApplicationNo, dbo.A2ZLOAN.LoanApplicationDate, dbo.A2ZLOAN.AccType, dbo.A2ZACCTYPE.AccTypeDescription, dbo.A2ZLOAN.CuType, 
                      dbo.A2ZLOAN.CuNo,dbo.A2ZCUNION.CuName, dbo.A2ZLOAN.MemNo, dbo.A2ZMEMBER.MemName, dbo.A2ZLOAN.LoanApplicationAmt, dbo.A2ZLOAN.LoanIntRate, dbo.A2ZLOAN.LoanGrace, 
                      dbo.A2ZLOAN.LoanInstlAmt, dbo.A2ZLOAN.LoanLastInstlAmt, dbo.A2ZLOAN.LoanNoInstl, dbo.A2ZLOAN.LoanFirstInstlDt, dbo.A2ZLOAN.LoanPurpose, 
                      dbo.A2ZLOAN.LoanCategory, dbo.A2ZLOAN.LoanExpiryDate, dbo.A2ZLOAN.LoanSuretyMemNo, dbo.A2ZLOAN.InputBy, A2ZSYSIDS_1.IdsName AS [Input By Name], 
                      dbo.A2ZLOAN.InputByDate, dbo.A2ZLOAN.ApprovBy, dbo.A2ZSYSIDS.IdsName AS [Approved By Name], dbo.A2ZLOAN.ApprovByDate,dbo.A2ZLOAN.AccPeriod,  
                      dbo.A2ZACCTYPE.AccTypeClass
FROM         dbo.A2ZLOAN LEFT OUTER JOIN
                      dbo.A2ZSYSIDS ON dbo.A2ZLOAN.ApprovBy = dbo.A2ZSYSIDS.IdsNo LEFT OUTER JOIN
                      dbo.A2ZSYSIDS AS A2ZSYSIDS_1 ON dbo.A2ZLOAN.InputBy = A2ZSYSIDS_1.IdsNo LEFT OUTER JOIN
                      dbo.A2ZCUNION ON dbo.A2ZLOAN.CuType = dbo.A2ZCUNION.CuType AND dbo.A2ZLOAN.CuNo = dbo.A2ZCUNION.CuNo RIGHT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZLOAN.CuType = dbo.A2ZMEMBER.CuType AND dbo.A2ZLOAN.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.A2ZLOAN.MemNo = dbo.A2ZMEMBER.MemNo RIGHT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.A2ZLOAN.AccType = dbo.A2ZACCTYPE.AccTypeCode

WHERE (LoanApplicationNo=@CommonNo2)

END










GO

