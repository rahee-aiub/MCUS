USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STIssTransferTransaction]    Script Date: 06/30/2019 1:33:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE  [dbo].[Sp_STIssTransferTransaction](@OrderNo nvarchar(20), @VchNo nvarchar(20),@ChalanNo nvarchar(20), @SupplierNo INT, @SupplierName nvarchar(50), 
                 @RcvWarehouseNo INT, @RcvWarehouseName nvarchar(50),@IssWarehouseNo INT, @IssWarehouseName nvarchar(50),
				  @TrnNote nvarchar(MAX) ,@FuncOpt INT, @FuncOptDesc nvarchar(50),
				 @TrnDate smalldatetime, @TrnType INT, @UserId INT, @TrnProcFlag INT)

AS
BEGIN

 /*

 EXECUTE Sp_STIssTransferTransaction 34 "45' 345 1 "abc" 1 "dhaka office" "asfsadff" 1 "purchase" "2018-01-01" 1 1 0

 */

DECLARE @TransactionTypeDesc nvarchar(50);
		




BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

		IF @TrnType = 1 
			BEGIN
				SET @TransactionTypeDesc = 'Cash';
			END

		IF @TrnType = 48 
			BEGIN
				SET @TransactionTypeDesc = 'Bank';
			END


	

	IF @FuncOpt = 11 OR @FuncOpt = 12
		BEGIN
			INSERT INTO A2ZSTTRANSACTION(ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,
			ItemUnit,ItemUnitDesc,ItemPurchaseQty,ItemUnitPrice,ItemNetCostPrice,ItemSellPrice,ItemTotalPrice,VchNo,TrnAmtDr,TrnQtyDr,TrnAmtCr,TrnQtyCr)

			SELECT ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,
			ItemUnit,ItemUnitDesc,ItemQty,ItemUnitPrice,ItemNetCostPrice,ItemSellPrice,ItemTotalPrice,VchNo,ItemTotalPrice,ItemQty,0,0 
			FROM WFTRN WHERE UserId = @UserId AND VchNo = @VchNo;

			

			UPDATE  A2ZSTTRANSACTION SET OrderNo = @OrderNo, FuncOpt= @FuncOpt, FuncOptDesc = 'Transfer Transaction', TransactionType = @TrnType, 
			TransactionTypeDesc = @TransactionTypeDesc,
			TransactionNote = @TrnNote, ChalanNo = @ChalanNo, SupplierNo = @SupplierNo, SupplierName = @SupplierName, 
			IssWarehouseNo = @IssWarehouseNo, IssWarehouseName = @IssWarehouseName, UserId = @UserId, 
			RcvWarehouseNo = @RcvWarehouseNo, RcvWarehouseName = @RcvWarehouseName,TrnWarehouseNo = @IssWarehouseNo, TrnWarehouseName = @IssWarehouseName,
			TransactionDate = @TrnDate, TrnProcFlag = 0 WHERE VchNo = @VchNo
		END

	
	IF @FuncOpt = 11 OR @FuncOpt = 12
		BEGIN
			INSERT INTO A2ZSTTRANSFER(ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,
			ItemUnit,ItemUnitDesc,ItemPurchaseQty,ItemUnitPrice,ItemNetCostPrice,ItemSellPrice,ItemTotalPrice,VchNo,TrnAmtDr,TrnQtyDr,TrnAmtCr,TrnQtyCr)

			SELECT ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,
			ItemUnit,ItemUnitDesc,ItemQty,ItemUnitPrice,ItemNetCostPrice,ItemSellPrice,ItemTotalPrice,VchNo,0,0,ItemTotalPrice,ItemQty 
			FROM WFTRN WHERE UserId = @UserId AND VchNo = @VchNo;

			
			UPDATE  A2ZSTTRANSFER SET OrderNo = @OrderNo, FuncOpt= 2, FuncOptDesc = 'Rcv.Transfer', TransactionType = @TrnType, 
			TransactionTypeDesc = @TransactionTypeDesc,
			TransactionNote = @TrnNote, ChalanNo = @ChalanNo, SupplierNo = @SupplierNo, SupplierName = @SupplierName, 
			IssWarehouseNo = @IssWarehouseNo, IssWarehouseName = @IssWarehouseName, UserId = @UserId, 
			RcvWarehouseNo = @RcvWarehouseNo, RcvWarehouseName = @RcvWarehouseName,TrnWarehouseNo = @RcvWarehouseNo, TrnWarehouseName = @RcvWarehouseName,
			TransactionDate = @TrnDate, TrnProcFlag = @TrnProcFlag WHERE VchNo = @VchNo AND TrnQtyCr > 0
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

