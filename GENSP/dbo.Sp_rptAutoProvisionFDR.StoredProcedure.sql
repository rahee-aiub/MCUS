USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptAutoProvisionFDR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_rptAutoProvisionFDR] (@CommonNo1 int)
AS
BEGIN

SELECT     TOP (100) PERCENT CuType, CuNo, MemNo, AccType, CAST(AccNo AS VARCHAR(16)) as AccNo, MemName, UptoMthProvision, UptoLastMthProvision, CurrMthProvision, AccIntRate,NoDays,CalFromDate, 
                      AccFDAmount
FROM         dbo.WFCSPROVISIONFDR
WHERE (AccType=@CommonNo1)
ORDER BY CuType, CuNo


UPDATE WFCSPROVISIONFDR SET WFCSPROVISIONFDR.ProcStat = 2 
WHERE (AccType=@CommonNo1) AND WFCSPROVISIONFDR.ProcStat <> 3;

END











GO
