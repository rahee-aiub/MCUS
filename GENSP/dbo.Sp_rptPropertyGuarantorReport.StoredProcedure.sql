USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptPropertyGuarantorReport]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_rptPropertyGuarantorReport](@CommonNo2 int)

AS
BEGIN

SELECT     LoanApplicationNo, PrSRL, PrName, FileNo, PrAmount, PrDesc, AccStat, AccStatDate
FROM         dbo.A2ZPRGUAR WHERE (LoanApplicationNo=@CommonNo2)

END

GO
