USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STGenerateOpeningBalance]    Script Date: 06/25/2019 2:58:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Sp_STGenerateOpeningBalance](@WareHouse INT,@GrpCode INT, @SubGrpCode INT, @fDate SMALLDATETIME, @nFlag INT)
AS
/*

EXECUTE Sp_STGenerateOpeningBalance 0,1,1,'2019-01-01',0

*/

BEGIN


    SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION WHERE UserId = 0;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

	UPDATE A2ZSTMST SET CalUnitQty = 0, CalUnitCost = 0; 
	
	DECLARE @OpeningDate SMALLDATETIME;
	DECLARE @LastOpDate SMALLDATETIME;

	DECLARE @trnDate SMALLDATETIME;
	DECLARE @nDate SMALLDATETIME;
	
    DECLARE @tDate VARCHAR(10);
    DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @Balance  money;

    DECLARE @fYear INT;
	DECLARE @tYear INT;

    DECLARE @BegYear int;
    DECLARE @IYear int;

	
	DECLARE @nCount INT;


	DECLARE @nYear INT;	


	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-01-01';


	SET @OpeningDate = (DATEADD(MONTH,-1,@fDate));

	IF YEAR(@OpeningDate) < YEAR(@opDate)
	   BEGIN
	         SET @OpeningDate =  @opDate;
	   END


	SET @LastOpDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @OpeningDate)), DATEADD(mm, 1, @OpeningDate)));

	
	SET @tDate = CAST(YEAR(@OpeningDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@OpeningDate) AS VARCHAR(2)) + '-' + CAST(DAY(@LastOpDate) AS VARCHAR(2))

    
	SET @openTable = 'A2ZSTMCUST' + CAST(YEAR(@fDate) AS VARCHAR(4)) + '..A2ZSTOPBALANCE';
	


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

    SET @fYear = LEFT(@opDate,4);
	SET @tYear = LEFT(@tDate,4);

	SET @nCount = @fYear

	PRINT @opDate;
	PRINT @tDate;



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

                      SET @strSQL = @strSQL + ' AND ItemGroupNo = ' + CAST(@GrpCode AS VARCHAR(2));

					  SET @strSQL = @strSQL + ' AND ItemCategoryNo = ' + CAST(@SubGrpCode AS VARCHAR(2));
			
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

        SET @strSQL = @strSQL + ' AND ItemGroupNo = ' + CAST(@GrpCode AS VARCHAR(2));

		SET @strSQL = @strSQL + ' AND ItemCategoryNo = ' + CAST(@SubGrpCode AS VARCHAR(2));

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

			
			EXECUTE SpM_STGenerateAvgCost @tDate, @nFlag		

			
END





















GO

