USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_STGenerateSingleLedgerBalance]    Script Date: 6/23/2018 10:19:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SpM_STGenerateSingleLedgerBalance](@CommonNo1 INT,@fDate VARCHAR(10), @nFlag INT)
AS
/*

EXECUTE SpM_STGenerateSingleLedgerBalance '2018-03-01',0



*/

BEGIN

	SET NOCOUNT ON;

---=============  DECLARATION ===============
	DECLARE @trnDate SMALLDATETIME;
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
    DECLARE @tDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);
    DECLARE @acctypeclass int;

    DECLARE @BegYear int;
    DECLARE @IYear int;

    DECLARE @fYear int;
    
	DECLARE @nYear INT;	
    DECLARE @pYear INT;	
	
	DECLARE @tYear INT;
	DECLARE @nCount INT;

    DECLARE @ReadFlag INT;

    DECLARE @tmm INT;
    DECLARE @tdd INT;

    DECLARE @xFlag INT;
    
    
---============= END OF DECLARATION ===============

    
	UPDATE A2ZSUPPLIER SET SuppBalance = 0,SuppVATAmt = 0,SuppTAXAmt = 0 WHERE SuppCode = @CommonNo1; 

    SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION WHERE ID = 0;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;


	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-01-01';
    
	SET @openTable = 'A2ZSTMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZSTSUPPOPBAL';

	SET @strSQL = 'UPDATE A2ZSUPPLIER SET A2ZSUPPLIER.SuppBalance = ' +
		'ISNULL((SELECT SUM(STKSuppBalance) FROM ' + @openTable +  
		' WHERE STKSuppCode = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
		'),0) FROM A2ZSUPPLIER,' + @openTable +
	    ' WHERE A2ZSUPPLIER.SuppCode = ' + CAST(@CommonNo1 AS VARCHAR(7));                 
		
	EXECUTE (@strSQL);


	SET @strSQL = 'UPDATE A2ZSUPPLIER SET A2ZSUPPLIER.SuppVATAmt = ' +
		'ISNULL((SELECT SUM(STKSuppVATAmt) FROM ' + @openTable +  
		' WHERE STKSuppCode = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
		'),0) FROM A2ZSUPPLIER,' + @openTable +
		' WHERE A2ZSUPPLIER.SuppCode = ' + CAST(@CommonNo1 AS VARCHAR(7));          
		
	EXECUTE (@strSQL);

	SET @strSQL = 'UPDATE A2ZSUPPLIER SET A2ZSUPPLIER.SuppTAXAmt = ' +
		'ISNULL((SELECT SUM(STKSuppTAXAmt) FROM ' + @openTable +  
		' WHERE STKSuppCode = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
		'),0) FROM A2ZSUPPLIER,' + @openTable +
		' WHERE A2ZSUPPLIER.SuppCode = ' + CAST(@CommonNo1 AS VARCHAR(7));                 
		
	EXECUTE (@strSQL);


	SET @fYear = LEFT(@opDate,4);
	SET @tYear = LEFT(@fDate,4);

	SET @nCount = @fYear

	         WHILE (@nCount <> 0)
		         BEGIN
			
			          SET @strSQL = 'INSERT INTO #WFA2ZSTTRANSACTION (TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				      'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				      'TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				      'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)' +
				      ' SELECT ' +
				      'TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				      'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				      'TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				      'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +			  	   	          
					  ' FROM A2ZSTMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZSTTRANSACTION WHERE TrnProcFlag = 0' +
					  ' AND SupplierNo = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
					  ' AND TransactionDate BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + ''''; 

								
			          EXECUTE (@strSQL);

			          SET @nCount = @nCount + 1;
			          IF @nCount > @tYear
				      BEGIN
					       SET @nCount = 0;
				      END
		          END

        
		 SET @strSQL = 'INSERT INTO #WFA2ZSTTRANSACTION (TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				'TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)' +
				' SELECT ' +
				'TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemVATAmt,ItemTAXAmt,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				'TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +
				' FROM A2ZSTMCUS..A2ZSTTRANSACTION ' + 
				' WHERE SupplierNo = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
        ' AND TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @fDate + '''' + ')'
		
		EXECUTE (@strSQL);


---------- Credit
			UPDATE A2ZSUPPLIER SET A2ZSUPPLIER.SuppBalance = A2ZSUPPLIER.SuppBalance + 
            ISNULL((SELECT SUM(ItemNetCostPrice) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.SupplierNo = @CommonNo1 AND #WFA2ZSTTRANSACTION.FuncOpt = 1 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM A2ZSUPPLIER,#WFA2ZSTTRANSACTION
			WHERE A2ZSUPPLIER.SuppCode = @CommonNo1;
---------- Debit
           
		    UPDATE A2ZSUPPLIER SET A2ZSUPPLIER.SuppBalance = A2ZSUPPLIER.SuppBalance - 
            ISNULL((SELECT SUM(ItemNetCostPrice) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.SupplierNo = @CommonNo1 AND #WFA2ZSTTRANSACTION.FuncOpt = 61 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM A2ZSUPPLIER,#WFA2ZSTTRANSACTION
			WHERE A2ZSUPPLIER.SuppCode = @CommonNo1; 

---------- VAT Credit
			UPDATE A2ZSUPPLIER SET A2ZSUPPLIER.SuppVATAmt = A2ZSUPPLIER.SuppVATAmt + 
            ISNULL((SELECT SUM(ItemVATAmt) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.SupplierNo = @CommonNo1 AND #WFA2ZSTTRANSACTION.FuncOpt = 1 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM A2ZSUPPLIER,#WFA2ZSTTRANSACTION
			WHERE A2ZSUPPLIER.SuppCode = @CommonNo1;

---------- VAT Debit
			UPDATE A2ZSUPPLIER SET A2ZSUPPLIER.SuppVATAmt = A2ZSUPPLIER.SuppVATAmt -
            ISNULL((SELECT SUM(ItemVATAmt) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.SupplierNo = @CommonNo1 AND #WFA2ZSTTRANSACTION.FuncOpt = 62 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM A2ZSUPPLIER,#WFA2ZSTTRANSACTION
			WHERE A2ZSUPPLIER.SuppCode = @CommonNo1;

---------- Tax Credit
			UPDATE A2ZSUPPLIER SET A2ZSUPPLIER.SuppTAXAmt = A2ZSUPPLIER.SuppTAXAmt + 
            ISNULL((SELECT SUM(ItemTAXAmt) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.SupplierNo = @CommonNo1 AND #WFA2ZSTTRANSACTION.FuncOpt = 1 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM A2ZSUPPLIER,#WFA2ZSTTRANSACTION
			WHERE A2ZSUPPLIER.SuppCode = @CommonNo1;

---------- Tax Debit
			UPDATE A2ZSUPPLIER SET A2ZSUPPLIER.SuppTAXAmt = A2ZSUPPLIER.SuppTAXAmt -
            ISNULL((SELECT SUM(ItemTAXAmt) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.SupplierNo = @CommonNo1 AND #WFA2ZSTTRANSACTION.FuncOpt = 63 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM A2ZSUPPLIER,#WFA2ZSTTRANSACTION
			WHERE A2ZSUPPLIER.SuppCode = @CommonNo1;

	
END



GO

