USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STInternalTransfer]    Script Date: 6/23/2018 10:16:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE  [dbo].[Sp_STInternalTransfer](@VchNo nvarchar(20), @IssWarehouseNo INT,
				  @FuncOpt INT, @TrnDate smalldatetime, @NGroup INT, @SGroup INT, @NCategory INT, @SCategory INT,
				  @NItemCode nvarchar(7),@NItemName nvarchar(MAX), @SItemCode nvarchar(7), @SItemName nvarchar(MAX),
				  @TrnQty INT, @TrnType INT, @UserId INT, @TrnProcFlag INT)

AS
BEGIN

 /*

 EXECUTE Sp_STInternalTransfer 34 "45' 345 1 "abc" 1 "dhaka office" "asfsadff" 1 "purchase" "2018-01-01" 1 1 0

 */


DECLARE @NItemGroupDesc nvarchar(50);
DECLARE @SItemGroupDesc nvarchar(50);
DECLARE @NItemCategoryDesc nvarchar(50);
DECLARE @SItemCategoryDesc nvarchar(50);
DECLARE @WarehouseName nvarchar(50);

DECLARE @NItemUnit INT;
DECLARE @NItemUnitDesc nvarchar(50);
DECLARE @SItemUnit INT;
DECLARE @SItemUnitDesc nvarchar(50);
DECLARE @BoothName nvarchar(50);
DECLARE @NItemUnitPrice money;
DECLARE @SItemUnitPrice money;
DECLARE @ItemTotalPrice money;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

		SET @NItemGroupDesc = (SELECT GrpDescription FROM A2ZGROUP WHERE GrpCode = @NGroup);
		SET @SItemGroupDesc = (SELECT GrpDescription FROM A2ZGROUP WHERE GrpCode = @SGroup);
		SET @NItemCategoryDesc = (SELECT SubGrpDescription FROM A2ZSUBGROUP WHERE GrpCode = @SGroup AND SubGrpCode = @NCategory);
		SET @SItemCategoryDesc = (SELECT SubGrpDescription FROM A2ZSUBGROUP WHERE GrpCode = @SGroup AND SubGrpCode = @SCategory);
		SET @NItemUnit = (SELECT STKUnit FROM A2ZSTMST WHERE STKItemCode = @NItemCode);
		SET @SItemUnit = (SELECT STKUnit FROM A2ZSTMST WHERE STKItemCode = @SItemCode);
		SET @NItemUnitDesc = (SELECT UnitDesc FROM A2ZUNITCODE WHERE UnitNo = @NItemUnit);
		SET @SItemUnitDesc = (SELECT UnitDesc FROM A2ZUNITCODE WHERE UnitNo = @SItemUnit);

		SET @BoothName = (SELECT A2ZCGLMST.GLAccDesc FROM A2ZGLMCUS..A2ZCGLMST where A2ZCGLMST.GLAccNo = @IssWarehouseNo);
		SET @NItemUnitPrice = (SELECT STKUnitAvgCost FROM A2ZSTMST WHERE STKItemCode = @NItemCode);
		SET @SItemUnitPrice = (SELECT STKUnitAvgCost FROM A2ZSTMST WHERE STKItemCode = @SItemCode);
		SET @ItemTotalPrice = (@NItemUnitPrice * @TrnQty);

	IF @FuncOpt = 20
		BEGIN

			INSERT INTO A2ZSTTRANSACTION(VchNo,TransactionDate,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,
			ItemCategoryNo,ItemCategoryDesc, ItemCode,ItemName,ItemUnit,ItemUnitDesc,TrnQtyDr,TrnQtyCr,IssWarehouseNo,
			RcvWarehouseNo,TrnWarehouseNo,IssWarehouseName,RcvWarehouseName,TrnWarehouseName,ItemUnitPrice,
			TrnProcFlag,TransactionType,TransactionTypeDesc,ItemTotalPrice,TrnAmtDr,TrnAmtCr,UserId)
			
			VALUES(@VchNo,@TrnDate,20,'Int Trn Send',@NGroup,@NItemGroupDesc,@NCategory,@NItemCategoryDesc,
			@NItemCode,@NItemName,@NItemUnit,@NItemUnitDesc,@TrnQty,0,@IssWarehouseNo,
			@IssWarehouseNo,@IssWarehouseNo,@BoothName,@BoothName,@BoothName,@NItemUnitPrice,
			0,0,'Internal',@ItemTotalPrice,@ItemTotalPrice,0,@UserId)
			


			INSERT INTO A2ZSTTRANSACTION(VchNo,TransactionDate,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,
			ItemCategoryNo, ItemCategoryDesc, ItemCode,ItemName,ItemUnit,ItemUnitDesc,TrnQtyDr,TrnQtyCr,IssWarehouseNo,
			RcvWarehouseNo,TrnWarehouseNo,IssWarehouseName,RcvWarehouseName,TrnWarehouseName,ItemUnitPrice,
			TrnProcFlag,TransactionType,TransactionTypeDesc,ItemTotalPrice,TrnAmtDr,TrnAmtCr,UserId)
			
			VALUES(@VchNo,@TrnDate,21,'Int Trn Rcv',@SGroup,@SItemGroupDesc,@SCategory, @SItemCategoryDesc, 
			@SItemCode,@SItemName,@SItemUnit,@SItemUnitDesc,0,@TrnQty,@IssWarehouseNo,
			@IssWarehouseNo,@IssWarehouseNo,@BoothName,@BoothName,@BoothName,@SItemUnitPrice,
			0,0,'Internal',@ItemTotalPrice,0,@ItemTotalPrice,@UserId)
			

		
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

