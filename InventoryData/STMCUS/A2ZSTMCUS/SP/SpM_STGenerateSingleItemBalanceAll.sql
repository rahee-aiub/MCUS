USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_STGenerateSingleItemBalanceAll]    Script Date: 6/23/2018 10:19:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[SpM_STGenerateSingleItemBalanceAll](@ItemCode INT, @fDate VARCHAR(10), @nFlag INT)
AS
/*
EXECUTE SpM_STGenerateSingleItemBalanceAll 1010001, '2018-03-01',0



*/

BEGIN

    SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION WHERE UserId = 0;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;





	--UPDATE A2ZSTMST SET STKUnitQty = 0, CalUnitQty = 0 WHERE STKItemCode = @ItemCode; 
	
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

	DECLARE @IdsNo INT;
	DECLARE @GLCashCode INT;
	DECLARE @Warehouse INT;
	DECLARE @WarehouseName VARCHAR(100);

	TRUNCATE TABLE WFDTLSTMST;


	DECLARE glmstTable CURSOR FOR
    SELECT GLAccNo,GLAccDesc
    FROM A2ZGLMCUS..A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC;

    OPEN glmstTable;
    FETCH NEXT FROM glmstTable INTO
    @Warehouse,@WarehouseName;

    WHILE @@FETCH_STATUS = 0 
	   BEGIN

		      INSERT INTO WFDTLSTMST (STKWareHouse,STKWareHouseName,STKItemCode,STKItemName,STKGroup,STKSubGroup,
	          STKUnit,STKUnitQty,STKUnitAvgCost,STKUnitCost,STKTPUnitQty,STKTPUnitCost)
	          SELECT
	          @Warehouse,@WarehouseName,STKItemCode,STKItemName,STKGroup,STKSubGroup,
	          STKUnit,0,0,0,0,0
	          FROM A2ZSTMST where STKItemCode = @ItemCode;
		 

	FETCH NEXT FROM glmstTable INTO
		@Warehouse,@WarehouseName;


	  END

   CLOSE glmstTable; 
   DEALLOCATE glmstTable;



	SET @trnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);
	SET @tYear = YEAR(@trnDate);


    SET @tDate = @fDate;


	SET @nDate = @fDate;

	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-01-01';


	SET @openTable = 'A2ZSTMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZSTOPBALANCE';


	
	         SET @strSQL = 'UPDATE WFDTLSTMST SET WFDTLSTMST.STKUnitQty = ' +
		                   'ISNULL((SELECT SUM(STKUnitQty) FROM ' + @openTable +  
						   ' WHERE STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7)) +             
						   ' AND ' + @openTable + '.STKWarehouseNo = WFDTLSTMST.STKWareHouse' + 
		                   '),0) FROM WFDTLSTMST,' + @openTable +
		                   ' WHERE WFDTLSTMST.STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7)) +
						   ' AND ' + @openTable + '.STKWarehouseNo = WFDTLSTMST.STKWareHouse';                 
	

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
					 
					  --IF @WareHouse <> 0
		     --            BEGIN
		     --                  SET @strSQL = @strSQL + ' AND TrnWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8));
		     --            END

			
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
		--' AND TrnWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8)) +
		' AND TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		

		EXECUTE (@strSQL);     


---------- Credit Qty
			UPDATE WFDTLSTMST SET WFDTLSTMST.STKUnitQty = WFDTLSTMST.STKUnitQty + 
            ISNULL((SELECT SUM(TrnQtyCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = @ItemCode AND 
			#WFA2ZSTTRANSACTION.TrnWarehouseNo = WFDTLSTMST.STKWareHouse ),0)
			FROM WFDTLSTMST,#WFA2ZSTTRANSACTION
			WHERE WFDTLSTMST.STKItemCode = @ItemCode AND
			WFDTLSTMST.STKWareHouse = #WFA2ZSTTRANSACTION.TrnWarehouseNo;

------------ Debit Qty
            UPDATE WFDTLSTMST SET WFDTLSTMST.STKUnitQty = WFDTLSTMST.STKUnitQty - 
            ISNULL((SELECT SUM(TrnQtyDr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = @ItemCode AND 
			#WFA2ZSTTRANSACTION.TrnWarehouseNo = WFDTLSTMST.STKWareHouse ),0)
			FROM WFDTLSTMST,#WFA2ZSTTRANSACTION
			WHERE WFDTLSTMST.STKItemCode = @ItemCode AND
			WFDTLSTMST.STKWareHouse = #WFA2ZSTTRANSACTION.TrnWarehouseNo;


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
                ' WHERE ItemCode = ' + CAST(@ItemCode AS VARCHAR(7)) +
		      --' AND TrnWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8)) +
		        ' AND TrnProcFlag = 1' +
       	        ' AND (TransactionDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		

		EXECUTE (@strSQL);    
		
		
		---------- Credit Qty
			UPDATE WFDTLSTMST SET WFDTLSTMST.STKTPUnitQty = WFDTLSTMST.STKTPUnitQty + 
            ISNULL((SELECT SUM(TrnQtyCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = @ItemCode AND 
			#WFA2ZSTTRANSACTION.TrnWarehouseNo = WFDTLSTMST.STKWareHouse ),0)
			FROM WFDTLSTMST,#WFA2ZSTTRANSACTION
			WHERE WFDTLSTMST.STKItemCode = @ItemCode AND
			WFDTLSTMST.STKWareHouse = #WFA2ZSTTRANSACTION.TrnWarehouseNo; 

        ---------- Credit Amt
			UPDATE WFDTLSTMST SET WFDTLSTMST.STKTPUnitCost = WFDTLSTMST.STKTPUnitCost + 
            ISNULL((SELECT SUM(TrnAmtCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = @ItemCode AND 
			#WFA2ZSTTRANSACTION.TrnWarehouseNo = WFDTLSTMST.STKWareHouse ),0)
			FROM WFDTLSTMST,#WFA2ZSTTRANSACTION
			WHERE WFDTLSTMST.STKItemCode = @ItemCode AND
			WFDTLSTMST.STKWareHouse = #WFA2ZSTTRANSACTION.TrnWarehouseNo; 


		
	EXECUTE SpM_STGenerateSingleAvgCostAll @ItemCode, @fDate, @nFlag			
	

END

















GO

