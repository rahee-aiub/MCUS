USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptShareGuarantorReport]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_rptShareGuarantorReport](@CommonNo2 int)

AS
BEGIN

SELECT     LoanApplicationNo, CuType, CuNo, MemNo, AccType, CAST(dbo.A2ZSHGUAR.AccNo AS VARCHAR(16)) as AccNo, AccAmount, AccStat, AccStatDate
FROM         dbo.A2ZSHGUAR WHERE (LoanApplicationNo=@CommonNo2)

END

GO
