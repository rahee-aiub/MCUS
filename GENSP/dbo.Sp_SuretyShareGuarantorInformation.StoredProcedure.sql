USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_SuretyShareGuarantorInformation]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_SuretyShareGuarantorInformation](@CommonNo1 int,@CommonNo2 int,@CommonNo3 int)

AS
BEGIN


SELECT     dbo.A2ZLOAN.LoanApplicationNo,dbo.A2ZLOAN.LoanApplicationDate,dbo.A2ZACCOUNT.CuType,dbo.A2ZACCOUNT.CuNo , dbo.A2ZACCOUNT.MemNo,dbo.A2ZMEMBER.MemName,dbo.A2ZACCOUNT.AccType, dbo.A2ZACCOUNT.AccNo,   
                      dbo.A2ZLOAN.LoanApplicationAmt,dbo.A2ZACCOUNT.AccBalance ,dbo.A2ZSHGUAR.AccType AS GuarantySingleAccType, dbo.A2ZSHGUAR.AccNo AS GuarantySingleAccNo, dbo.A2ZSHGUAR.AccAmount 
                      
FROM         dbo.A2ZACCOUNT INNER JOIN
                      dbo.A2ZLOAN ON dbo.A2ZACCOUNT.AccLoanAppNo = dbo.A2ZLOAN.LoanApplicationNo INNER JOIN
                      dbo.A2ZSHGUAR ON dbo.A2ZLOAN.LoanApplicationNo = dbo.A2ZSHGUAR.LoanApplicationNo INNER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType
WHERE dbo.A2ZACCOUNT.CuType=@CommonNo1 AND A2ZACCOUNT.CuNo=@CommonNo2 AND dbo.A2ZSHGUAR.MemNo=@CommonNo3


END






GO
