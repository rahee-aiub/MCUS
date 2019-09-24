USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptStaffLedgerBalance]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










/*
EXECUTE Sp_rptCSLedgerBalance 
*/


CREATE PROCEDURE [dbo].[Sp_rptStaffLedgerBalance] 
AS
BEGIN

SELECT     dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16)) as AccNo, dbo.A2ZACCOUNT.MemNo, 
                      dbo.A2ZACCOUNT.AccOpenDate, dbo.A2ZACCOUNT.AccStatus, dbo.A2ZACCOUNT.AccBalance, dbo.A2ZACCSTATUS.AccStatusDescription, 
                      dbo.A2ZACCOUNT.AccLastTrnDateU, dbo.A2ZMEMBER.MemName, dbo.A2ZACCOUNT.AccOpBal
FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo LEFT OUTER JOIN
                      dbo.A2ZACCSTATUS ON dbo.A2ZACCOUNT.AccStatus = dbo.A2ZACCSTATUS.AccStatusCode
WHERE     (dbo.A2ZACCOUNT.CuNo = 0) AND (dbo.A2ZACCOUNT.CuType = 0) ORDER BY dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.CuNo

END









GO
