USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_STGenerateAllItemBalance]    Script Date: 6/23/2018 10:18:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[SpM_STGenerateAllItemBalance](@fDate VARCHAR(10), @WareHouse INT, @nFlag INT)
AS
/*
EXECUTE SpM_STGenerateAllItemBalance '2018-03-01',0,0



*/

BEGIN

    SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION WHERE UserId = 0;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

	UPDATE A2ZSTMST SET STKUnitQty = 0, STKTPUnitQty = 0, STKTPUnitCost = 0, CalUnitQty = 0; 
	
	DECLARE @trnDate SMALLDATETIME;
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
    DECLARE @tDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @Balance  money;

    DECLARE @fYear INT;
	DECLARE @tYear INT;

    DECLARE @BegYear int;
    DECLARE @IYear int;

	
	DECLARE @nCount INT;


	DECLARE @nYear INT;	

	SET @trnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);
	SET @tYear = YEAR(@trnDate);

    SET @tDate = @fDate;


	SET @nDate = @fDate;

	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-01-01';

    
	SET @openTable = 'A2ZSTMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZSTOPBALANCE';
	

	IF @WareHouse <> 0
	   BEGIN
	         SET @strSQL = 'UPDATE A2ZSTMST SET A2ZSTMST.CalUnitQty = ' +
		                   'ISNULL((SELECT SUM(STKUnitQty) FROM ' + @openTable +  
		                   ' WHERE A2ZSTMST.STKItemCode = ' + @openTable + '.STKItemCode ' +
						   ' AND ' + @openTable + '.STKWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8)) +
		                   '),0) FROM A2ZSTMST,' + @openTable +
		                   ' WHERE A2ZSTMST.STKItemCode = ' + @openTable + '.STKItemCode ';	                 
	   END


	IF @WareHouse = 0
	   BEGIN
	         SET @strSQL = 'UPDATE A2ZSTMST SET A2ZSTMST.CalUnitQty = ' +
		                   'ISNULL((SELECT SUM(STKUnitQty) FROM ' + @openTable +  
		                   ' WHERE A2ZSTMST.STKItemCode = ' + @openTable + '.STKItemCode ' +
		                   '),0) FROM A2ZSTMST,' + @openTable +
		                   ' WHERE A2ZSTMST.STKItemCode = ' + @openTable + '.STKItemCode ';
	   END
  
	EXECUTE (@strSQL);

----------------------------------------------------------------------------
     SET @nCount = @fYear

	         WHILE (@nCount <> 0)
		         BEGIN
			
			          SET @strSQL = 'INSERT INTO #WFA2ZSTTRANSACTION (TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				      'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				      'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				      'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)' +
				      ' SELECT ' +
				      'TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				      'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				      'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				      'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +			  	   	          
					  ' FROM A2ZSTMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZSTTRANSACTION WHERE TrnProcFlag = 0 AND TransactionDate' +
					  ' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @tDate + ''''; 
					 
					  IF @WareHouse <> 0
		                 BEGIN
		                       SET @strSQL = @strSQL + ' AND TrnWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8));
		                 END

			
			          EXECUTE (@strSQL);

			          SET @nCount = @nCount + 1;
			          IF @nCount > @tYear
				      BEGIN
					       SET @nCount = 0;
				      END
		          END


----------------------------------------------------------------------------

   

    SET @strSQL = 'INSERT INTO #WFA2ZSTTRANSACTION (TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)' +
				' SELECT ' +
				'TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +
				' FROM A2ZSTMCUS..A2ZSTTRANSACTION ' + 
        ' WHERE TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		IF @WareHouse <> 0
		   BEGIN
		        SET @strSQL = @strSQL + ' AND TrnWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8));
		   END

		EXECUTE (@strSQL);     


---------- Credit
			UPDATE A2ZSTMST SET A2ZSTMST.CalUnitQty = A2ZSTMST.CalUnitQty + 
            ISNULL((SELECT SUM(TrnQtyCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTMST.STKItemCode),0)
			FROM A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode;


		
------------ Debit
            UPDATE A2ZSTMST SET A2ZSTMST.CalUnitQty = A2ZSTMST.CalUnitQty - 
            ISNULL((SELECT SUM(TrnQtyDr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTMST.STKItemCode),0)
			FROM A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode;


			UPDATE A2ZSTMST SET A2ZSTMST.STKUnitQty = A2ZSTMST.CalUnitQty;

			
			TRUNCATE TABLE #WFA2ZSTTRANSACTION;

			SET @strSQL = 'INSERT INTO #WFA2ZSTTRANSACTION (TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)' +
				' SELECT ' +
				'TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +
				' FROM A2ZSTMCUS..A2ZSTTRANSFER ' + 
                ' WHERE TrnProcFlag = 1' +
       	        ' AND (TransactionDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		   IF @WareHouse <> 0
		       BEGIN
		          SET @strSQL = @strSQL + ' AND TrnWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8));
		       END

		   EXECUTE (@strSQL);  

		   ---------- Credit Qty.

			UPDATE A2ZSTMST SET A2ZSTMST.STKTPUnitQty = A2ZSTMST.STKTPUnitQty + 
            ISNULL((SELECT SUM(TrnQtyCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTMST.STKItemCode),0)
			FROM A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode;

			---------- Credit Amt.

			UPDATE A2ZSTMST SET A2ZSTMST.STKTPUnitCost = A2ZSTMST.STKTPUnitCost + 
            ISNULL((SELECT SUM(TrnAmtCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTMST.STKItemCode),0)
			FROM A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode;


		   EXECUTE SpM_STGenerateAvgCost @fDate, @nFlag		

END

















GO

