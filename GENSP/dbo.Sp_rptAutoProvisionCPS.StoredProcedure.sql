USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptAutoProvisionCPS]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_rptAutoProvisionCPS] (@CommonNo1 int)
AS
BEGIN

SELECT     TOP (100) PERCENT CuNo, MemNo, AccType, MemName, AccIntRate, CurrMthProduct, CurrMthProvision, CuType, CAST(AccNo AS VARCHAR(16)) as AccNo, ProcStat
FROM         dbo.WFCSPROVISIONCPS
WHERE     (AccType = @CommonNo1)
ORDER BY CuType, CuNo
UPDATE WFCSPROVISIONCPS SET WFCSPROVISIONCPS.ProcStat = 2 
WHERE (AccType=@CommonNo1) AND WFCSPROVISIONCPS.ProcStat <> 3;

END

















GO
