USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_STGenerateSingleAvgCostAll]    Script Date: 6/23/2018 10:18:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SpM_STGenerateSingleAvgCostAll](@ItemCode INT, @fDate VARCHAR(10), @nFlag INT)
AS
/*
EXECUTE SpM_STGenerateSingleAvgCostAll '2018-03-01',0



*/

BEGIN

    SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION WHERE UserId = 0;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

	UPDATE A2ZSTMST SET STKUnitAvgCost = 0, CalUnitCost = 0; 
	
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

    SET @tDate = @fDate;


	SET @nDate = @fDate;

	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-01-01';

    
	SET @openTable = 'A2ZSTMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZSTOPAVGCOST';
	
	UPDATE A2ZSTMST SET CalAvgUnitQty = 0; 


	SET @strSQL = 'UPDATE A2ZSTMST SET A2ZSTMST.CalAvgUnitQty = ' +
	              'ISNULL((SELECT SUM(STKUnitQty) FROM ' + @openTable +  
	              ' WHERE A2ZSTMST.STKItemCode = ' + @openTable + '.STKItemCode ' +
	              '),0) FROM A2ZSTMST,' + @openTable +
	              ' WHERE A2ZSTMST.STKItemCode = ' + @openTable + '.STKItemCode ';
	  
  
	EXECUTE (@strSQL);

	
	SET @strSQL = 'UPDATE A2ZSTMST SET A2ZSTMST.CalUnitCost = ' +
	              'ISNULL((SELECT SUM(STKUnitCost) FROM ' + @openTable +  
	              ' WHERE A2ZSTMST.STKItemCode = ' + @openTable + '.STKItemCode ' +
	              '),0) FROM A2ZSTMST,' + @openTable +
	              ' WHERE A2ZSTMST.STKItemCode = ' + @openTable + '.STKItemCode ';
	  
	EXECUTE (@strSQL);

-----------------------------------------------------------------------------

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
					  ' FROM A2ZSTMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZSTTRANSACTION WHERE TrnProcFlag = 0 AND FuncOpt = 1 AND TransactionDate' +
					  ' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @tDate + ''''; 
					 
					  			
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
        ' WHERE TrnProcFlag = 0 AND FuncOpt = 1' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		
		EXECUTE (@strSQL);     


------------ Credit
			UPDATE A2ZSTMST SET A2ZSTMST.CalAvgUnitQty = A2ZSTMST.CalAvgUnitQty + 
            ISNULL((SELECT SUM(TrnQtyCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTMST.STKItemCode),0)
			FROM A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode;



---------- Credit Amount
			UPDATE A2ZSTMST SET A2ZSTMST.CalUnitCost = A2ZSTMST.CalUnitCost + 
            ISNULL((SELECT SUM(TrnAmtCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTMST.STKItemCode),0)
			FROM A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode;


			UPDATE A2ZSTMST SET A2ZSTMST.CalUnitCost = A2ZSTMST.CalUnitCost + A2ZSTMST.STKTPUnitCost;

			UPDATE A2ZSTMST SET A2ZSTMST.CalAvgUnitQty = A2ZSTMST.CalAvgUnitQty + A2ZSTMST.STKTPUnitCost;

		
            UPDATE A2ZSTMST SET A2ZSTMST.STKUnitAvgCost = round((A2ZSTMST.CalUnitCost / A2ZSTMST.CalAvgUnitQty),2) WHERE A2ZSTMST.CalAvgUnitQty > 0;
           
		    UPDATE WFDTLSTMST SET WFDTLSTMST.STKUnitAvgCost = (SELECT STKUnitAvgCost FROM A2ZSTMST
			WHERE A2ZSTMST.STKItemCode = WFDTLSTMST.STKItemCode)
			FROM A2ZSTMST,WFDTLSTMST
			WHERE A2ZSTMST.STKItemCode = WFDTLSTMST.STKItemCode;

			UPDATE WFDTLSTMST SET WFDTLSTMST.STKUnitCost = round((WFDTLSTMST.STKUnitQty * WFDTLSTMST.STKUnitAvgCost),2) WHERE WFDTLSTMST.STKUnitQty > 0;
			
			UPDATE WFDTLSTMST SET WFDTLSTMST.STKUnitCost = WFDTLSTMST.STKUnitCost + 
            ISNULL((SELECT SUM(STKTPUnitCost) FROM A2ZSTMST
			WHERE A2ZSTMST.STKItemCode = WFDTLSTMST.STKItemCode),0)
			FROM A2ZSTMST,WFDTLSTMST
			WHERE A2ZSTMST.STKItemCode = WFDTLSTMST.STKItemCode;


END



















GO

