USE [A2ZHRMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptViewSalaryTransactionVch]    Script Date: 03/24/2017 23:15:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_rptViewSalaryTransactionVch]

AS
BEGIN

SELECT GLCode, GLDesc, GLDrCr, GLAmount
FROM  dbo.WFVIEWSALARY 
ORDER BY GLCode
END


