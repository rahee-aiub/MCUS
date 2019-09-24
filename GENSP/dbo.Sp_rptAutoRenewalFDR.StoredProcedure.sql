USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptAutoRenewalFDR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_rptAutoRenewalFDR] (@CommonNo1 int)
AS
BEGIN
SELECT     TOP (100) PERCENT CuType, CuNo, MemNo, AccType, CAST(AccNo AS VARCHAR(16)) as AccNo, MemName, AccIntRate, CalInterest, FDAmount,AccPeriodMonths,CalFromDate
FROM         dbo.WFCSRENEWFDR
WHERE (AccType=@CommonNo1)
ORDER BY CuType, CuNo


UPDATE WFCSRENEWFDR SET WFCSRENEWFDR.ProcStat = 2 
WHERE (AccType=@CommonNo1) AND WFCSRENEWFDR.ProcStat <> 3;

END










GO
