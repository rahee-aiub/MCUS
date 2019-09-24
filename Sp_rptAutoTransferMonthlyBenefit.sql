USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptAutoTransferMonthlyBenefit]    Script Date: 10/24/2017 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_rptAutoTransferMonthlyBenefit] (@CommonNo1 int)
AS
BEGIN

SELECT     TOP (100) PERCENT CuType, CuNo, CuNumber, MemNo, AccType, CAST(AccNo AS VARCHAR(16)) as AccNo, MemName, AccMthBenefitAmt, NoMonths, AccTotalBenefitAmt, AccCorrAccNo
FROM         dbo.WFCSMONTHLYBENEFITCREDIT
WHERE     (AccType = @CommonNo1)
ORDER BY CuType, CuNo

UPDATE WFCSMONTHLYBENEFITCREDIT SET WFCSMONTHLYBENEFITCREDIT.ProcStat = 2 
WHERE (AccType=@CommonNo1) AND WFCSMONTHLYBENEFITCREDIT.ProcStat <> 3;

END









