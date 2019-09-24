USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STAdjustmentTransaction]    Script Date: 06/27/2019 4:51:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE  [dbo].[Sp_STAdjustmentTransaction](@VchNo nvarchar(20), @IssWarehouseNo INT,
				  @FuncOpt INT, @TrnDate smalldatetime, @Group INT, @Category INT,
				  @ItemCode nvarchar(7),@ItemName nvarchar(MAX),
				  @TrnQty INT, @TrnType INT, @UserId INT, @TrnProcFlag INT)

AS
BEGIN

 /*

 EXECUTE Sp_STAdjustmentTransaction 34 "45' 345 1 "abc" 1 "dhaka office" "asfsadff" 1 "purchase" "2018-01-01" 1 1 0

 */


DECLARE @ItemGroupDesc nvarchar(50);
DECLARE @ItemCategoryDesc nvarchar(50);
DECLARE @WarehouseName nvarchar(50);
DECLARE @ItemUnit INT;
DECLARE @ItemUnitDesc nvarchar(50);
DECLARE @BoothName nvarchar(50);
DECLARE @ItemUnitPrice money;
DECLARE @ItemTotalPrice money;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

		SET @ItemGroupDesc = (SELECT GrpDescription FROM A2ZGROUP WHERE GrpCode = @Group);
		SET @ItemCategoryDesc = (SELECT SubGrpDescription FROM A2ZSUBGROUP WHERE GrpCode = @Group AND SubGrpCode = @Category);
		SET @ItemUnit = (SELECT STKUnit FROM A2ZSTMST WHERE STKItemCode = @ItemCode);
		SET @ItemUnitDesc = (SELECT UnitDesc FROM A2ZUNITCODE WHERE UnitNo = @ItemUnit);
		SET @BoothName = (SELECT A2ZCGLMST.GLAccDesc FROM A2ZGLMCUS..A2ZCGLMST where A2ZCGLMST.GLAccNo = @IssWarehouseNo);
		SET @ItemUnitPrice = (SELECT STKUnitAvgCost FROM A2ZSTMST WHERE STKItemCode = @ItemCode);
		SET @ItemTotalPrice = (@ItemUnitPrice * @TrnQty);

	
		

		IF @TrnType = 0
			BEGIN	
				INSERT INTO A2ZSTTRANSACTION(VchNo,TransactionDate,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,
				ItemCategoryNo,ItemCategoryDesc, ItemCode,ItemName,ItemUnit,ItemUnitDesc,TrnQtyDr,TrnQtyCr,IssWarehouseNo,
				RcvWarehouseNo,TrnWarehouseNo,IssWarehouseName,RcvWarehouseName,TrnWarehouseName,ItemUnitPrice,ItemNetCostPrice,
				TrnProcFlag,TransactionType,TransactionTypeDesc,ItemTotalPrice,TrnAmtDr,TrnAmtCr,UserId,ItemPurchaseQty)
			
				VALUES(@VchNo,@TrnDate,20,'Adjustment',@Group,@ItemGroupDesc,@Category,@ItemCategoryDesc,
				@ItemCode,@ItemName,@ItemUnit,@ItemUnitDesc,@TrnQty,0,@IssWarehouseNo,
				@IssWarehouseNo,@IssWarehouseNo,@BoothName,@BoothName,@BoothName,@ItemUnitPrice,@ItemTotalPrice,
				0,3,'Internal Adj',@ItemTotalPrice,@ItemTotalPrice,0,@UserId,@TrnQty)
			END		
		

		IF @TrnType = 1
			BEGIN
				INSERT INTO A2ZSTTRANSACTION(VchNo,TransactionDate,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,
				ItemCategoryNo,ItemCategoryDesc, ItemCode,ItemName,ItemUnit,ItemUnitDesc,TrnQtyDr,TrnQtyCr,IssWarehouseNo,
				RcvWarehouseNo,TrnWarehouseNo,IssWarehouseName,RcvWarehouseName,TrnWarehouseName,ItemUnitPrice,
				TrnProcFlag,TransactionType,TransactionTypeDesc,ItemTotalPrice,TrnAmtDr,TrnAmtCr,UserId,ItemPurchaseQty)
			
				VALUES(@VchNo,@TrnDate,20,'Adjustment',@Group,@ItemGroupDesc,@Category,@ItemCategoryDesc,
				@ItemCode,@ItemName,@ItemUnit,@ItemUnitDesc,0,@TrnQty,@IssWarehouseNo,
				@IssWarehouseNo,@IssWarehouseNo,@BoothName,@BoothName,@BoothName,@ItemUnitPrice,
				0,3,'Internal Adj',@ItemTotalPrice,0,@ItemTotalPrice,@UserId,@TrnQty)

			END
		
COMMIT TRANSACTION
		SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH

END;




GO

