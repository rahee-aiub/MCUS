USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptCSNewAccountSlip]    Script Date: 01/30/2017 14:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






/*
EXECUTE Sp_rptCSNewAccountSlip '1130001000000001'
*/


CREATE PROCEDURE [dbo].[Sp_rptCSNewAccountSlip](@CommonNo1 varchar(16)) 
AS
BEGIN

SELECT     dbo.A2ZACCOUNT.MemNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16))AS AccNo, 
                      dbo.A2ZACCOUNT.AccOpenDate, dbo.A2ZMEMBER.MemName, dbo.A2ZACCTYPE.AccTypeDescription
FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.A2ZACCOUNT.AccType = dbo.A2ZACCTYPE.AccTypeCode LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType AND 
                      dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo


WHERE     (AccNo = @CommonNo1)
END





