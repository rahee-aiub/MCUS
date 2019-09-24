USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptItemStockRequisitionList]    Script Date: 07/09/2019 1:03:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_rptItemStockRequisitionList] (@CommonNo1 INT,@CommonNo2 INT,@CommonNo3 INT,@CommonNo4 INT) 
AS
-- Execute Sp_rptItemStockRequisitionList 1,10101160,1,1
BEGIN


SELECT        Id, ReqDate, ReqNo, ReqWarehouseNo, ReqWarehouseName, ReqItemGroupNo, ReqItemGroupDesc, ReqItemCategoryNo, ReqItemCategoryDesc, ReqItemCode, ReqItemName, ReqUnitQtyBalance, ReqReqUnitQty,ReqNote
FROM            dbo.A2ZITEMREQUIRE
WHERE ReqNo = @CommonNo1 AND ReqWarehouseNo = @CommonNo2 AND ReqItemGroupNo = @CommonNo3 AND ReqItemCategoryNo = @CommonNo4;

END;







GO

