USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptAutoAnniversary6YR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_rptAutoAnniversary6YR] (@CommonNo1 int)


AS
BEGIN


SELECT     CuType, CuNo, MemNo, AccType, CAST(AccNo AS VARCHAR(16)) as AccNo, AccIntRate, CalInterest, FDAmount, MemName, CalFromDate
FROM         dbo.WFCSANNIVERSARY6YR
WHERE (AccType=@CommonNo1)
 ORDER BY CuType,CuNo

UPDATE WFCSANNIVERSARY6YR SET WFCSANNIVERSARY6YR.ProcStat = 2 
WHERE (AccType=@CommonNo1) AND WFCSANNIVERSARY6YR.ProcStat <> 3;

END











GO
