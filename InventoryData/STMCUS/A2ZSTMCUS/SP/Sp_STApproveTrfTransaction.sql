USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STApproveTrfTransaction]    Script Date: 6/23/2018 10:07:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE  [dbo].[Sp_STApproveTrfTransaction](@VchNo nvarchar(20), @TrnDate smalldatetime,@Warehouse int, @FuncOpt int)

AS
BEGIN

 /*

 EXECUTE Sp_STApproveTrfTransaction 34 "45' 345 1 "abc" 1 "dhaka office" "asfsadff" 1 "purchase" "2018-01-01" 1 1 0

 */

DECLARE @TransactionTypeDesc nvarchar(50);
		



BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

	
	
			INSERT INTO A2ZSTTRANSACTION(TransactionDate,VchNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,
			ItemCode,ItemName,ItemUnit,ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,
			TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,
			SupplierName,RcvWarehouseNo,RcvWarehouseName,IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)

			SELECT @TrnDate,VchNo,@FuncOpt,'Received Transfer',ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,
			ItemCode,ItemName,ItemUnit,ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,
			TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,
			SupplierName,RcvWarehouseNo,RcvWarehouseName,IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId 

			FROM A2ZSTTRANSFER WHERE VchNo = @VchNo AND TrnWarehouseNo = @Warehouse;

		



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

