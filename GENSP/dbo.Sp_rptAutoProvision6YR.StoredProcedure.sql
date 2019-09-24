USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptAutoProvision6YR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Sp_rptAutoProvision6YR] (@CommonNo1 int)
AS
BEGIN

SELECT     TOP (100) PERCENT CuType, CuNo, MemNo, AccType, CAST(AccNo AS VARCHAR(16)) as AccNo, MemName, UptoMthProvision, UptoLastMthProvision, CurrMthProvision, AccIntRate,NoDays,CalFromDate, 
                      AccFDAmount
FROM         dbo.WFCSPROVISION6YR
WHERE (AccType=@CommonNo1)
ORDER BY CuType, CuNo


UPDATE WFCSPROVISION6YR SET WFCSPROVISION6YR.ProcStat = 2 
WHERE (AccType=@CommonNo1) AND WFCSPROVISION6YR.ProcStat <> 3;

END










GO
