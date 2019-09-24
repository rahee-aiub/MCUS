USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STSupplierPayment]    Script Date: 6/23/2018 10:18:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










CREATE PROCEDURE  [dbo].[Sp_STSupplierPayment](@VchNo nvarchar(20), @SupplierNo INT, @SupplierName nvarchar(50), 
					  @TrnNote nvarchar(MAX) ,@FuncOpt INT, @FuncOptDesc nvarchar(50),@TrnBankCode int,@TrnBankName nvarchar(50),
					  @TrnBankChqNo nvarchar(50), @TrnDate smalldatetime, @TrnType INT,@TrntypeDesc nvarchar(20), 
					  @UserId INT, @Amount Money, @TrnProcFlag INT,@TrnWarehouseNo INT,@TrnWarehouseName nvarchar(50))

AS
BEGIN



DECLARE @TransactionTypeDesc nvarchar(50);



DECLARE @PayType smallint;
DECLARE @TrnDesc nvarchar(50);
DECLARE @TrnDrCr tinyint;
DECLARE @ContraDrCr tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;

DECLARE @TrnGLContraCode int;


/*

EXECUTE Sp_STSupplierPayment 1,1 

*/


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

	
	IF @TrnType = 1 
	   BEGIN
			SET @TransactionTypeDesc = 'Cash';
			SET @TrnGLContraCode = @TrnWarehouseNo;
	   END

	IF @TrnType = 48 
	   BEGIN
		    SET @TransactionTypeDesc = 'Bank';
			SET @TrnGLContraCode = @TrnBankCode;
	   END


	IF @FuncOpt = 61
		BEGIN

			INSERT INTO A2ZSTTRANSACTION(TransactionDate,VchNo,FuncOpt,FuncOptDesc,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,
			TrnAmtDr,SupplierNo,SupplierName,TransactionType,TransactionTypeDesc,TransactionNote,TrnProcFlag,
			TrnBankCode,TrnBankName,TrnBankChqNo,UserId,TrnWarehouseNo,TrnWarehouseName) 

			VALUES(@TrnDate,@VchNo,@FuncOpt,'Supplier Payment',0,0,@Amount,@Amount,@SupplierNo,@SupplierName,
			@TrnType,@TrntypeDesc,@TrnNote,@TrnProcFlag,@TrnBankCode,@TrnBankName,@TrnBankChqNo,@UserId,@TrnWarehouseNo,@TrnWarehouseName);


            SET @PayType = (SELECT PayType FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 7);      
            SET @TrnDrCr= (SELECT TrnMode FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 7); 
            SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 7);
            SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 7);
	        SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 7);

			IF @TrnDrCr = 0
			   BEGIN
			       SET @ContraDrCr = 1;
			   END

            IF @TrnDrCr = 1
		       BEGIN
			       SET @ContraDrCr = 0;
			   END

						
			INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
			VALUES (@TrnDate,@VchNo,@FuncOpt,@PayType,@TrnType,@TrnDrCr,0,0,@TrnNote,1,@TrnGLAccNoDr,@TrnGLContraCode,@TrnGLAccNoDr,@Amount,@Amount,0,0,@TrnWarehouseNo,5,0,@UserId,0)
		                  			
            INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
			VALUES (@TrnDate,@VchNo,@FuncOpt,@PayType,@TrnType,@ContraDrCr,0,0,@TrnNote,1,@TrnGLAccNoDr,@TrnGLContraCode,@TrnGLContraCode,@Amount,0,@Amount,0,@TrnWarehouseNo,5,0,@UserId,0)
		END

	IF @FuncOpt = 62
		BEGIN

			INSERT INTO A2ZSTTRANSACTION(TransactionDate,VchNo,FuncOpt,FuncOptDesc,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,
			TrnAmtDr,SupplierNo,SupplierName,TransactionType,TransactionTypeDesc,TransactionNote,TrnProcFlag,
			TrnBankCode,TrnBankChqNo,UserId,TrnWarehouseNo,TrnWarehouseName) 

			VALUES(@TrnDate,@VchNo,@FuncOpt,'VAT Payment',@Amount,0,0,@Amount,@SupplierNo,@SupplierName,
			@TrnType,@TrntypeDesc,@TrnNote,@TrnProcFlag,@TrnBankCode,@TrnBankChqNo,@UserId,@TrnWarehouseNo,@TrnWarehouseName);

            SET @PayType = (SELECT PayType FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 8);      
            SET @TrnDrCr= (SELECT TrnMode FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 8); 
            SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 8);
            SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 8);
	        SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 8);

			IF @TrnDrCr = 0
			   BEGIN
			       SET @ContraDrCr = 1;
			   END

            IF @TrnDrCr = 1
		       BEGIN
			       SET @ContraDrCr = 0;
			   END

						
			INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
			VALUES (@TrnDate,@VchNo,@FuncOpt,@PayType,@TrnType,@TrnDrCr,0,0,@TrnNote,1,@TrnGLAccNoDr,@TrnGLContraCode,@TrnGLAccNoDr,@Amount,@Amount,0,0,@TrnWarehouseNo,5,0,@UserId,0)
		                  			
            INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
			VALUES (@TrnDate,@VchNo,@FuncOpt,@PayType,@TrnType,@ContraDrCr,0,0,@TrnNote,1,@TrnGLAccNoDr,@TrnGLContraCode,@TrnGLContraCode,@Amount,0,@Amount,0,@TrnWarehouseNo,5,0,@UserId,0) 

		END

	IF @FuncOpt = 63
		BEGIN

			INSERT INTO A2ZSTTRANSACTION(TransactionDate,VchNo,FuncOpt,FuncOptDesc,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,
			TrnAmtDr,SupplierNo,SupplierName,TransactionType,TransactionTypeDesc,TransactionNote,TrnProcFlag,
			TrnBankCode,TrnBankChqNo,UserId,TrnWarehouseNo,TrnWarehouseName) 

			VALUES(@TrnDate,@VchNo,@FuncOpt,'TAX Payment',0,@Amount,0,@Amount,@SupplierNo,@SupplierName,
			@TrnType,@TrntypeDesc,@TrnNote,@TrnProcFlag,@TrnBankCode,@TrnBankChqNo,@UserId,@TrnWarehouseNo,@TrnWarehouseName);

            SET @PayType = (SELECT PayType FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 9);      
            SET @TrnDrCr= (SELECT TrnMode FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 9); 
            SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 9);
            SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 9);
	        SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZSTTRNCTRL WHERE FuncNo=@FuncOpt AND PayType = 9);

			IF @TrnDrCr = 0
			   BEGIN
			       SET @ContraDrCr = 1;
			   END

            IF @TrnDrCr = 1
		       BEGIN
			       SET @ContraDrCr = 0;
			   END

						
			INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
			VALUES (@TrnDate,@VchNo,@FuncOpt,@PayType,@TrnType,@TrnDrCr,0,0,@TrnNote,1,@TrnGLAccNoDr,@TrnGLContraCode,@TrnGLAccNoDr,@Amount,@Amount,0,0,@TrnWarehouseNo,5,0,@UserId,0)
		                  			
            INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)
			VALUES (@TrnDate,@VchNo,@FuncOpt,@PayType,@TrnType,@ContraDrCr,0,0,@TrnNote,1,@TrnGLAccNoDr,@TrnGLContraCode,@TrnGLContraCode,@Amount,0,@Amount,0,@TrnWarehouseNo,5,0,@UserId,0) 

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

