USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptCUTypeReport]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_rptCUTypeReport](@CommonNo10 int, @CommonNo11 int)
AS
BEGIN

IF @CommonNo11 = 0
   BEGIN
        SELECT     CuType, CuNo, CuName, CuOldCuNo, CuDivi, CuDist, CuUpzila, CuThana
        FROM         dbo.A2ZCUNION
        WHERE     (CuType = @CommonNo10) AND (CuStatus <> 9)
   END
ELSE
   BEGIN
        SELECT     CuType, CuNo, CuName, CuOldCuNo, CuDivi, CuDist, CuUpzila, CuThana
        FROM         dbo.A2ZCUNION
        WHERE     (CuType = @CommonNo10) AND (CuStatus <> 9 AND GLCashCode = @CommonNo11)
   END

END

GO
