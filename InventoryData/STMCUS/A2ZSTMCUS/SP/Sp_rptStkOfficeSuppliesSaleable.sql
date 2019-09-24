USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptStkOfficeSuppliesSaleable]    Script Date: 6/23/2018 10:06:27 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_rptStkOfficeSuppliesSaleable]
AS
-- Execute Sp_rptStkOfficeSuppliesSaleable
BEGIN


SELECT        Id, STKItemCode, STKItemName, STKGroup, STKSubGroup, STKUnit, STKUnitQty, STKUnitAvgCost, STKUnitAvgCostDate, STKStatus, STKStatusDesc, STKStatusDate, STKReOrderLevel
FROM            dbo.A2ZSTMST

END;





GO

