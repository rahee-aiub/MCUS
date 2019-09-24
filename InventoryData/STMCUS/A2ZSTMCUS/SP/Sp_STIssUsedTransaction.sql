USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STIssUsedTransaction]    Script Date: 6/23/2018 10:16:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE  [dbo].[Sp_STIssUsedTransaction](@OrderNo nvarchar(20), @VchNo nvarchar(20),@ChalanNo nvarchar(20), @SupplierNo INT, @SupplierName nvarchar(50), 
                 @RcvWarehouseNo INT, @RcvWarehouseName nvarchar(50), @TrnNote nvarchar(MAX) ,@FuncOpt INT, @FuncOptDesc nvarchar(50),
				 @TrnDate smalldatetime, @TrnType INT, @UserId INT, @TrnProcFlag INT)

AS
BEGIN

 /*

 EXECUTE Sp_STIssUsedTransaction 34 "45' 345 1 "abc" 1 "dhaka office" "asfsadff" 1 "purchase" "2018-01-01" 1 1 0

 */

DECLARE @TransactionTypeDesc nvarchar(50);

DECLARE @ItemGroupNo     int;
DECLARE @ItemCategoryNo  int;
DECLARE @ItemNetCostPrice      money;
		
DECLARE @PayType smallint;
DECLARE @TrnDesc nvarchar(50);
DECLARE @TrnDrCr tinyint;
DECLARE @ContraDrCr tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;



BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

		IF @TrnType = 1 
			BEGIN
				SET @TransactionTypeDesc = 'Cash';
			END

        IF @TrnType = 3 
			BEGIN
				SET @TransactionTypeDesc = 'Trf.';
			END

		IF @TrnType = 48 
			BEGIN
				SET @TransactionTypeDesc = 'Bank';
			END


	

	
			INSERT INTO A2ZSTTRANSACTION(ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,
			ItemUnit,ItemUnitDesc,ItemPurchaseQty,ItemUnitPrice,ItemNetCostPrice,ItemSellPrice,ItemTotalPrice,VchNo,TrnAmtDr,TrnQtyDr,TrnAmtCr,TrnQtyCr)

			SELECT ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,
			ItemUnit,ItemUnitDesc,ItemQty,ItemUnitPrice,ItemNetCostPrice,ItemSellPrice,ItemTotalPrice,VchNo,ItemTotalPrice,ItemQty,0,0 
			FROM WFTRN WHERE VchNo = @VchNo;

			
			UPDATE  A2ZSTTRANSACTION SET OrderNo = @OrderNo, FuncOpt= @FuncOpt, FuncOptDesc = 'Use Transaction', TransactionType = @TrnType, 
			TransactionTypeDesc = @TransactionTypeDesc,
			TransactionNote = @TrnNote, ChalanNo = @ChalanNo, SupplierNo = @SupplierNo, SupplierName = @SupplierName, 
			IssWarehouseNo = @RcvWarehouseNo, IssWarehouseName = @RcvWarehouseName,TrnWarehouseNo = @RcvWarehouseNo, TrnWarehouseName = @RcvWarehouseName, UserId = @UserId, 
			TransactionDate = @TrnDate, TrnProcFlag = @TrnProcFlag WHERE VchNo = @VchNo
		
----------------------------------------------------------------------------

		    DECLARE WFTRNTable CURSOR FOR
            SELECT ItemGroupNo,ItemCategoryNo,SUM(ItemNetCostPrice) AS ItemNetCostPrice
            FROM WFTRN WHERE VchNo = @VchNo
			GROUP BY ItemGroupNo, ItemCategoryNo;

            OPEN WFTRNTable;
            FETCH NEXT FROM WFTRNTable INTO
            @ItemGroupNo,@ItemCategoryNo,@ItemNetCostPrice;

            WHILE @@FETCH_STATUS = 0 
	           BEGIN

			       IF @ItemNetCostPrice <> 0
                      BEGIN
       
                           SET @PayType = (SELECT PayType FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 5);      
                           SET @TrnDrCr= (SELECT TrnMode FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 5); 
                           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 5);
                           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 5);
	                       SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 5);

						   IF @TrnDrCr = 0
						      BEGIN
							      SET @ContraDrCr = 1;
							  END

                           IF @TrnDrCr = 1
						      BEGIN
							      SET @ContraDrCr = 0;
							  END
						   --------------- NORMAL TRANSACTION ------------

                           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
                           VALUES (@TrnDate,@VchNo,@TrnType,@TrnDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@ItemNetCostPrice,0,@ItemNetCostPrice,0,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF NORMAL TRANSACTION ------------

							--------------- CONTRA TRANSACTION ------------

                           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
                           VALUES (@TrnDate,@VchNo,@TrnType,@ContraDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,@ItemNetCostPrice,@ItemNetCostPrice,0,1,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF CONTRA TRANSACTION ------------
                      END

                   

			       FETCH NEXT FROM WFTRNTable INTO
                   @ItemGroupNo,@ItemCategoryNo,@ItemNetCostPrice;        


	           END

           CLOSE WFTRNTable; 
           DEALLOCATE WFTRNTable;





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

