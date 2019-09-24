USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptCSUpgradeInformationReport]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
EXECUTE Sp_rptCSTransactionDetailList '2016-02-01','2016-02-01', 0
*/


CREATE PROCEDURE [dbo].[Sp_rptCSUpgradeInformationReport] (@CommonNo11 int)
AS
BEGIN

IF @CommonNo11 = 0
   BEGIN

        SELECT     dbo.A2ZMEMBERHELP.OldCuNo, dbo.A2ZMEMBERHELP.CuType, dbo.A2ZMEMBERHELP.CuNo, dbo.A2ZMEMBERHELP.OldMemNo, 
                      dbo.A2ZMEMBERHELP.MemNo, dbo.A2ZMEMBERHELP.MemName, dbo.A2ZMEMBERHELP.AccType, dbo.A2ZMEMBERHELP.OldAccNumber, 
                      CAST(dbo.A2ZMEMBERHELP.AccNo AS VARCHAR(16)) AS AccNo,dbo.A2ZACCTYPE.AccTypeDescription
        FROM         dbo.A2ZMEMBERHELP LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.A2ZMEMBERHELP.AccType = dbo.A2ZACCTYPE.AccTypeCode
   END
ELSE
   BEGIN

        SELECT     dbo.A2ZMEMBERHELP.OldCuNo, dbo.A2ZMEMBERHELP.CuType, dbo.A2ZMEMBERHELP.CuNo, dbo.A2ZMEMBERHELP.OldMemNo, 
                      dbo.A2ZMEMBERHELP.MemNo, dbo.A2ZMEMBERHELP.MemName, dbo.A2ZMEMBERHELP.AccType, dbo.A2ZMEMBERHELP.OldAccNumber, 
                      CAST(dbo.A2ZMEMBERHELP.AccNo AS VARCHAR(16)) AS AccNo,dbo.A2ZACCTYPE.AccTypeDescription
        FROM         dbo.A2ZMEMBERHELP LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.A2ZMEMBERHELP.AccType = dbo.A2ZACCTYPE.AccTypeCode
                      WHERE dbo.A2ZMEMBERHELP.GLCashCode = @CommonNo11
   END
      

END

GO
