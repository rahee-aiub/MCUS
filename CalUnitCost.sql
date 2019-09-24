USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptStkItemList]    Script Date: 05/15/2018 3:52:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_rptStkItemList] (@CommonNo1 int,@CommonNo2 int)
AS
-- Execute Sp_rptStkItemList 1,2
BEGIN

SELECT        dbo.A2ZSTMST.STKItemCode, dbo.A2ZSTMST.STKItemName, dbo.A2ZSTMST.STKGroup, dbo.A2ZSTMST.STKSubGroup, dbo.A2ZSTMST.STKUnit, dbo.A2ZUNITCODE.UnitDesc, dbo.A2ZSTMST.STKUnitQty, 
                         dbo.A2ZSTMST.STKUnitAvgCost,dbo.A2ZSTMST.CalUnitCost
FROM            dbo.A2ZSTMST LEFT OUTER JOIN
                         dbo.A2ZUNITCODE ON dbo.A2ZSTMST.STKUnit = dbo.A2ZUNITCODE.UnitNo
WHERE        (dbo.A2ZSTMST.STKGroup = @CommonNo1) AND (dbo.A2ZSTMST.STKSubGroup = @CommonNo2)

END;





GO

