USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_STGenerateSingleItemBalance]    Script Date: 6/23/2018 10:18:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[SpM_STGenerateSingleItemBalance](@ItemCode INT, @fDate VARCHAR(10), @WareHouse INT, @nFlag INT)
AS
/*
EXECUTE SpM_STGenerateSingleItemBalance 1010001,'2018-03-01',10101268,0



*/

BEGIN

    SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION WHERE UserId = 0;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

	UPDATE A2ZSTMST SET STKUnitQty = 0, CalUnitQty = 0 WHERE STKItemCode = @ItemCode; 
	
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

	DECLARE @UnitQty int;
	DECLARE @UnitPrice money;
	DECLARE @UnitAvgCost money;

	DECLARE @nYear INT;	

	DECLARE @tqty INT;	

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
						   ' WHERE STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7)) + 
		                  
						   ' AND ' + @openTable + '.STKWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8)) +
		                   '),0) FROM A2ZSTMST,' + @openTable +
		                   ' WHERE A2ZSTMST.STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7));                 
	   END


	IF @WareHouse = 0
	   BEGIN
	         SET @strSQL = 'UPDATE A2ZSTMST SET A2ZSTMST.CalUnitQty = ' +
		                   'ISNULL((SELECT SUM(STKUnitQty) FROM ' + @openTable +  
						   ' WHERE STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7)) + 
		                   
		                   '),0) FROM A2ZSTMST,' + @openTable +
		                   ' WHERE A2ZSTMST.STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7)); 
	   END




	EXECUTE (@strSQL);



	SET @tqty = (SELECT CalUnitQty FROM A2ZSTMCUS..A2ZSTMST WHERE STKItemCode = @ItemCode);

	PRINT @tqty;


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


	------------------------------------------------------------------------------------------


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
        ' WHERE ItemCode = ' + CAST(@ItemCode AS VARCHAR(7)) +
		' AND TrnWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8)) +
		' AND TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		

		EXECUTE (@strSQL);     


---------- Credit Qty
			UPDATE A2ZSTMST SET A2ZSTMST.CalUnitQty = A2ZSTMST.CalUnitQty + 
            ISNULL((SELECT SUM(TrnQtyCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = @ItemCode),0)
			FROM A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = @ItemCode;

------------ Debit Qty
            UPDATE A2ZSTMST SET A2ZSTMST.CalUnitQty = A2ZSTMST.CalUnitQty - 
            ISNULL((SELECT SUM(TrnQtyDr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = @ItemCode),0)
			FROM A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = @ItemCode;


    SET @tqty = (SELECT CalUnitQty FROM A2ZSTMCUS..A2ZSTMST WHERE STKItemCode = @ItemCode);

	PRINT @tqty;

	
	UPDATE A2ZSTMST SET A2ZSTMST.STKUnitQty = A2ZSTMST.CalUnitQty WHERE STKItemCode = @ItemCode;
	
	EXECUTE SpM_STGenerateAvgCost @fDate, @nFlag			
	

END
















GO

