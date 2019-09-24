
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
EXECUTE Sp_rptCSProvisionBalance ,11 
*/

ALTER PROCEDURE [dbo].[Sp_rptCSProvisionBalance] (@CommonNo1 int)
AS
BEGIN
SELECT     dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16)) as AccNo, dbo.A2ZACCOUNT.MemNo, dbo.A2ZMEMBER.MemName, 
                      dbo.A2ZACCOUNT.AccProvBalance, dbo.A2ZACCOUNT.AccProvCalDate
FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType
WHERE     (dbo.A2ZACCOUNT.AccType =@CommonNo1 AND dbo.A2ZACCOUNT.AccStatus < 97)


END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

