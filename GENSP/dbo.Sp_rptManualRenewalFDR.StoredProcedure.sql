USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptManualRenewalFDR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_rptManualRenewalFDR] (@CommonNo1 int)
AS
BEGIN
SELECT     TOP (100) PERCENT CuType, CuNo, MemNo, AccType, CAST(AccNo AS VARCHAR(16)) as AccNo, MemName, AccIntRate, CalInterest, FDAmount,AccPeriodMonths,CalFromDate
FROM         dbo.WFCSMANUALRENEWFDR
WHERE (AccType=@CommonNo1)
ORDER BY CuType, CuNo


UPDATE WFCSMANUALRENEWFDR SET WFCSMANUALRENEWFDR.ProcStat = 2 
WHERE (AccType=@CommonNo1) AND WFCSMANUALRENEWFDR.ProcStat <> 3;

END










GO
