USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STMoveDailyTransaction]    Script Date: 6/23/2018 10:16:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE  [dbo].[Sp_STMoveDailyTransaction] (@fYY VARCHAR(4))

AS
--EXECUTE Sp_STMoveDailyTransaction 2017

BEGIN


DECLARE @strSQL NVARCHAR(MAX);

DECLARE @PLCode INT;
DECLARE @PLIncome MONEY;
DECLARE @PLExpense MONEY;
DECLARE @processDate SMALLDATETIME;

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON



	SET @strSQL = 'INSERT INTO A2ZSTMCUST' + CAST(@fYY AS VARCHAR(4)) + '..A2ZSTTRANSACTION (TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				  'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TrnBankChqNo,TrnBankCode,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				  'TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				  'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)' +
				  ' SELECT ' +
			      'TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				  'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TrnBankChqNo,TrnBankCode,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				  'TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				  'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +
				  ' FROM A2ZSTMCUS..A2ZSTTRANSACTION ';

	EXECUTE (@strSQL);




TRUNCATE TABLE A2ZSTMCUS..A2ZSTTRANSACTION;

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

