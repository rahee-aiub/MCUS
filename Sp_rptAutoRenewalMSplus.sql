USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptAutoRenewalMSplus]    Script Date: 05/16/2017 22:42:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_rptAutoRenewalMSplus] (@CommonNo1 int)
AS
BEGIN
SELECT     TOP (100) PERCENT CuType, CuNo, MemNo, AccType, CAST(AccNo AS VARCHAR(16)) as AccNo, MemName, NewMatureDate, NewFixedMthInt, AccFixedAmt,AccPeriodMonths,CuNumber,CalFromDate
FROM         dbo.WFCSRENEWMSplus
WHERE (AccType=@CommonNo1)
ORDER BY CuType, CuNo

UPDATE WFCSRENEWMSplus SET WFCSRENEWMSplus.ProcStat = 2 
WHERE (AccType=@CommonNo1) AND WFCSRENEWMSplus.ProcStat <> 3;

END










