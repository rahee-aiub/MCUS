USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptStkOfficeSuppliesNonSaleable]    Script Date: 6/23/2018 10:06:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Sp_rptStkOfficeSuppliesNonSaleable]
AS
-- Execute Sp_rptStkOfficeSuppliesNonSaleable
BEGIN


SELECT        Id, STKItemCode, STKItemName, STKGroup, STKSubGroup, STKUnit, STKUnitQty, STKUnitAvgCost, STKUnitAvgCostDate, STKStatus, STKStatusDesc, STKStatusDate, STKReOrderLevel
FROM            dbo.A2ZSTMST

END;






GO

