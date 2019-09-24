USE [A2ZHRMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptEmployeeTaxRegister]    Script Date: 07/02/2019 2:12:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_rptEmployeeTaxRegister] (@fDate smalldatetime)

AS
BEGIN

--EXECUTE Sp_rptEmployeeTaxRegister '2019-04-30'

SELECT EmpCode, EmpName, sum(TDAmount8) as TaxAmt  
FROM  dbo.A2ZEMPFSALARY 
WHERE SalDate < @fDate AND TDAmount8 > 0 
GROUP BY EmpCode, EmpName
ORDER BY EmpCode

END




GO

