USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptAutoAnniversaryFDR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_rptAutoAnniversaryFDR] (@CommonNo1 int)
AS
BEGIN
SELECT     TOP (100) PERCENT CuType, CuNo, MemNo, AccType, CAST(AccNo AS VARCHAR(16)) as AccNo, AccIntRate, CalInterest, FDAmount, MemName,CalFromDate
FROM         dbo.WFCSANNIVERSARYFDR
WHERE (AccType=@CommonNo1)
ORDER BY CuType, CuNo

UPDATE WFCSANNIVERSARYFDR SET WFCSANNIVERSARYFDR.ProcStat = 2 
WHERE (AccType=@CommonNo1) AND WFCSANNIVERSARYFDR.ProcStat <> 3;

END







GO
