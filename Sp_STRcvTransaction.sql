USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STRcvTransaction]    Script Date: 06/30/2019 1:34:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE  [dbo].[Sp_STRcvTransaction](@VchNo nvarchar(20),@OrderNo nvarchar(20),@ChalanNo nvarchar(20), @SupplierNo INT, @SupplierName nvarchar(50), 
                 @RcvWarehouseNo INT, @RcvWarehouseName nvarchar(50), @TrnNote nvarchar(MAX) ,@FuncOpt INT, @FuncOptDesc nvarchar(50),
				 @TrnDate smalldatetime, @TrnType INT, @UserId INT, @TrnProcFlag INT)

AS
BEGIN



 /*

 EXECUTE Sp_STRcvTransaction 1,1,1,1,"abc", 10101001, "dhaka office", "asfsadff", 1, "purchase", "2018-01-01", 3, 1, 0

 */

DECLARE @TransactionTypeDesc nvarchar(50);


DECLARE @ItemGroupNo     int;
DECLARE @ItemCategoryNo  int;
DECLARE @ItemTotalPrice  money;
DECLARE @ItemVATAmt      money;
DECLARE @ItemTAXAmt      money;
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

		IF @TrnType = 48 
			BEGIN
				SET @TransactionTypeDesc = 'Bank';
				
			END


			
	
			INSERT INTO A2ZSTTRANSACTION(ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,
			ItemUnit,ItemUnitDesc,ItemPurchaseQty,ItemUnitPrice,ItemSellPrice,ItemTotalPrice,VchNo,TrnAmtCr,TrnQtyCr,TrnAmtDr,TrnQtyDr,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice)
			SELECT ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,
			ItemUnit,ItemUnitDesc,ItemQty,ItemUnitPrice,ItemSellPrice,ItemTotalPrice,VchNo,ItemTotalPrice,ItemQty,0,0,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice 
			FROM WFTRN WHERE UserId = @UserId AND VchNo = @VchNo;
	
			UPDATE  A2ZSTTRANSACTION SET OrderNo = @OrderNo, FuncOpt= @FuncOpt, FuncOptDesc = 'Purchased Transaction', TransactionType = @TrnType, 
			TransactionTypeDesc = @TransactionTypeDesc,
			TransactionNote = @TrnNote, ChalanNo = @ChalanNo, SupplierNo = @SupplierNo, SupplierName = @SupplierName, 
			RcvWarehouseNo = @RcvWarehouseNo, RcvWarehouseName = @RcvWarehouseName, TrnWarehouseNo = @RcvWarehouseNo, TrnWarehouseName = @RcvWarehouseName, UserId = @UserId, 
			TransactionDate = @TrnDate, TrnProcFlag = @TrnProcFlag WHERE VchNo = @VchNo
			

---------------------------------------------------

			DECLARE WFTRNTable CURSOR FOR
            SELECT ItemGroupNo,ItemCategoryNo,SUM(ItemTotalPrice) AS ItemTotalPrice,SUM(ItemVATAmt) AS ItemVATAmt,SUM(ItemTAXAmt) AS ItemTAXAmt,SUM(ItemNetCostPrice) AS ItemNetCostPrice
            FROM WFTRN WHERE UserId = @UserId AND VchNo = @VchNo
			GROUP BY ItemGroupNo, ItemCategoryNo;

            OPEN WFTRNTable;
            FETCH NEXT FROM WFTRNTable INTO
            @ItemGroupNo,@ItemCategoryNo,@ItemTotalPrice,@ItemVATAmt,@ItemTAXAmt,@ItemNetCostPrice;

            WHILE @@FETCH_STATUS = 0 
	           BEGIN

			       
                   IF @ItemNetCostPrice <> 0
                      BEGIN
       
                           SET @PayType = (SELECT PayType FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 1);      
                           SET @TrnDrCr= (SELECT TrnMode FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 1); 
                           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 1);
                           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 1);
	                       SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 1);

						   IF @TrnDrCr = 0
						      BEGIN
							      SET @ContraDrCr = 1;
							  END

                           IF @TrnDrCr = 1
						      BEGIN
							      SET @ContraDrCr = 0;
							  END

						   ----------------- NORMAL TRANSACTION ------------

         --                  INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
         --                  VALUES (@TrnDate,@VchNo,@TrnType,@TrnDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,@ItemTotalPrice,@ItemTotalPrice,0,0,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF NORMAL TRANSACTION ------------

							--------------- CONTRA TRANSACTION ------------

                           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
                           VALUES (@TrnDate,@VchNo,@TrnType,@ContraDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@ItemNetCostPrice,0,@ItemNetCostPrice,1,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF CONTRA TRANSACTION ------------
                      END


                   IF @ItemVATAmt <> 0
                      BEGIN
       
                           SET @PayType = (SELECT PayType FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 2);      
                           SET @TrnDrCr= (SELECT TrnMode FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 2); 
                           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 2);
                           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 2);
	                       SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 2);

						   IF @TrnDrCr = 0
						      BEGIN
							      SET @ContraDrCr = 1;
							  END

                           IF @TrnDrCr = 1
						      BEGIN
							      SET @ContraDrCr = 0;
							  END

						   --------------- NORMAL TRANSACTION ------------

                           --INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
                           --VALUES (@TrnDate,@VchNo,@TrnType,@TrnDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,@ItemVATAmt,@ItemVATAmt,0,0,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF NORMAL TRANSACTION ------------

							--------------- CONTRA TRANSACTION ------------

                           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
                           VALUES (@TrnDate,@VchNo,@TrnType,@ContraDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@ItemVATAmt,0,@ItemVATAmt,1,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF CONTRA TRANSACTION ------------
                      END

                   IF @ItemTAXAmt <> 0
                      BEGIN
       
                           SET @PayType = (SELECT PayType FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 3);      
                           SET @TrnDrCr= (SELECT TrnMode FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 3); 
                           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 3);
                           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 3);
	                       SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 3);

						   IF @TrnDrCr = 0
						      BEGIN
							      SET @ContraDrCr = 1;
							  END

                           IF @TrnDrCr = 1
						      BEGIN
							      SET @ContraDrCr = 0;
							  END

						   --------------- NORMAL TRANSACTION ------------

                           --INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
                           --VALUES (@TrnDate,@VchNo,@TrnType,@TrnDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,@ItemTAXAmt,@ItemTAXAmt,0,0,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF NORMAL TRANSACTION ------------

							--------------- CONTRA TRANSACTION ------------

                           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
                           VALUES (@TrnDate,@VchNo,@TrnType,@ContraDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@ItemTAXAmt,0,@ItemTAXAmt,1,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF CONTRA TRANSACTION ------------
                      END


					  IF @ItemTotalPrice <> 0
                         BEGIN
       
                           SET @PayType = (SELECT PayType FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 4);      
                           SET @TrnDrCr= (SELECT TrnMode FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 4); 
                           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 4);
                           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 4);
	                       SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND GroupCode = @ItemGroupNo AND SubGroupCode = @ItemCategoryNo AND PayType = 4);

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
                           VALUES (@TrnDate,@VchNo,@TrnType,@TrnDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,@ItemTotalPrice,@ItemTotalPrice,0,0,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF NORMAL TRANSACTION ------------

							----------------- CONTRA TRANSACTION ------------

       --                    INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
       --                    VALUES (@TrnDate,@VchNo,@TrnType,@ContraDrCr,0,0,@TrnDesc,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@ItemNetCostPrice,0,@ItemNetCostPrice,1,@RcvWarehouseNo,5,0,@UserId,0)
		                   
						    --------------- END OF CONTRA TRANSACTION ------------
                      END


			       FETCH NEXT FROM WFTRNTable INTO
                   @ItemGroupNo,@ItemCategoryNo,@ItemTotalPrice,@ItemVATAmt,@ItemTAXAmt,@ItemNetCostPrice;        


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

