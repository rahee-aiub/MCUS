USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptStaffApproveLoanReport]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_rptStaffApproveLoanReport](@CommonNo2 int)

AS
BEGIN

SELECT     dbo.A2ZSTAFFLOAN.LoanApplicationNo, dbo.A2ZSTAFFLOAN.LoanApplicationDate, dbo.A2ZSTAFFLOAN.AccType, dbo.A2ZACCTYPE.AccTypeDescription, dbo.A2ZSTAFFLOAN.CuType, 
                      dbo.A2ZSTAFFLOAN.CuNo, dbo.A2ZSTAFFLOAN.MemNo, dbo.A2ZMEMBER.MemName, dbo.A2ZSTAFFLOAN.LoanApplicationAmt, dbo.A2ZSTAFFLOAN.LoanIntRate, dbo.A2ZSTAFFLOAN.LoanGrace, 
                      dbo.A2ZSTAFFLOAN.LoanInstlAmt, dbo.A2ZSTAFFLOAN.LoanLastInstlAmt, dbo.A2ZSTAFFLOAN.LoanNoInstl, dbo.A2ZSTAFFLOAN.LoanFirstInstlDt, dbo.A2ZSTAFFLOAN.LoanPurpose, 
                      dbo.A2ZSTAFFLOAN.LoanCategory, dbo.A2ZSTAFFLOAN.LoanExpiryDate, dbo.A2ZSTAFFLOAN.LoanSuretyMemNo, dbo.A2ZSTAFFLOAN.InputBy, A2ZSYSIDS_1.IdsName AS [Input By Name], 
                      dbo.A2ZSTAFFLOAN.InputByDate, dbo.A2ZSTAFFLOAN.ApprovBy, dbo.A2ZSYSIDS.IdsName AS [Approved By Name], dbo.A2ZSTAFFLOAN.ApprovByDate, 
                      dbo.A2ZACCTYPE.AccTypeClass
FROM         dbo.A2ZSTAFFLOAN LEFT OUTER JOIN
                      dbo.A2ZSYSIDS ON dbo.A2ZSTAFFLOAN.ApprovBy = dbo.A2ZSYSIDS.IdsNo LEFT OUTER JOIN
                      dbo.A2ZSYSIDS AS A2ZSYSIDS_1 ON dbo.A2ZSTAFFLOAN.InputBy = A2ZSYSIDS_1.IdsNo LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZSTAFFLOAN.CuType = dbo.A2ZMEMBER.CuType AND dbo.A2ZSTAFFLOAN.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.A2ZSTAFFLOAN.MemNo = dbo.A2ZMEMBER.MemNo RIGHT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.A2ZSTAFFLOAN.AccType = dbo.A2ZACCTYPE.AccTypeCode

WHERE (LoanApplicationNo=@CommonNo2)

END











GO
