USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_SuretyAllGuarantorInformation]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_SuretyAllGuarantorInformation](@CommonNo1 int,@CommonNo2 int,@CommonNo3 int)

AS
BEGIN


SELECT   dbo.A2ZACCOUNT.CuType,dbo.A2ZACCOUNT.CuNo,dbo.A2ZLOAN.AccType, dbo.A2ZACCOUNT.AccNo, dbo.A2ZACCOUNT.MemNo,dbo.A2ZMEMBER.MemName,dbo.A2ZACCOUNT.AccBalance,dbo.A2ZLOAN.LoanApplicationNo, dbo.A2ZLOAN.LoanApplicationDate, 
                       dbo.A2ZLOAN.LoanApplicationAmt,  dbo.A2ZACGUAR.AccType AS GuarantyAllAccType, 
                      dbo.A2ZACGUAR.AccNo AS GuarantyAllAccNo, dbo.A2ZACGUAR.AccAmount
FROM         dbo.A2ZACCOUNT INNER JOIN
                      dbo.A2ZLOAN ON dbo.A2ZACCOUNT.AccLoanAppNo = dbo.A2ZLOAN.LoanApplicationNo AND dbo.A2ZACCOUNT.AccType = dbo.A2ZLOAN.AccType INNER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo INNER JOIN
                      dbo.A2ZACGUAR ON dbo.A2ZACCOUNT.AccLoanAppNo = dbo.A2ZACGUAR.LoanApplicationNo AND 
                      dbo.A2ZLOAN.LoanApplicationNo = dbo.A2ZACGUAR.LoanApplicationNo
WHERE dbo.A2ZACCOUNT.CuType=@CommonNo1 AND A2ZACCOUNT.CuNo=@CommonNo2 AND dbo.A2ZACGUAR.MemNo=@CommonNo3
END






GO
