USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_STItemStatement]    Script Date: 06/25/2018 10:17:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[SpM_STItemStatement](@CommonNo1 INT,@CommonNo2 INT, @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
--- @nFlag = 0 = Normal CS Account Statement
---EXECUTE SpM_STItemStatement 10101001,1010001,'2018-03-01','2018-03-01',0



BEGIN	
	SET NOCOUNT ON;

	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
    DECLARE @xDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @BegYear int;
    DECLARE @IYear int;

	DECLARE @nYear INT;	

    DECLARE @opBalance MONEY;
	DECLARE @debitAmt MONEY;
	DECLARE @creditAmt MONEY;
    
    DECLARE @pYear INT;
	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @nCount INT;

    DECLARE @ReadFlag INT;
    DECLARE @processDate SMALLDATETIME;

    DECLARE @tmm INT;
    DECLARE @tdd INT;
    DECLARE @xFlag INT;
    DECLARE @yFlag INT;


--===========  For Opening Balance ========================
	SELECT * INTO #A2ZSTMST FROM A2ZSTMST WHERE STKItemCode = @CommonNo2;

	UPDATE #A2ZSTMST SET CalUnitQty = 0;


    SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION WHERE ID = 0;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

    
 
	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-01-01';
    
	SET @openTable = 'A2ZSTMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZSTOPBALANCE';

	SET @strSQL = 'UPDATE #A2ZSTMST SET #A2ZSTMST.CalUnitQty = ' +
		'ISNULL((SELECT SUM(STKUnitQty) FROM ' + @openTable +  
		' WHERE STKWarehouseNo = ' + CAST(@CommonNo1 AS VARCHAR(8)) + 
		' AND STKItemCode = ' + CAST(@CommonNo2 AS VARCHAR(7)) +
		'),0) FROM #A2ZSTMST,' + @openTable +
		' WHERE #A2ZSTMST.STKItemCode = ' + CAST(@CommonNo2 AS VARCHAR(7)); 
		
	
	EXECUTE (@strSQL);
         
--==========  Get Transaction Data For Opening Balance ==========
	SET @fYear = LEFT(@opDate,4);
	SET @tYear = LEFT(@fDate,4);

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
					  ' FROM A2ZSTMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZSTTRANSACTION WHERE TrnProcFlag = 0' +
					  ' AND ItemCode = ' + CAST(@CommonNo2 AS VARCHAR(7)) +
					  ' AND TransactionDate BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + ''''; 

									 
					  IF @CommonNo1 <> 0
		                 BEGIN
		                       SET @strSQL = @strSQL + ' AND TrnWarehouseNo = ' + CAST(@CommonNo1 AS VARCHAR(8));
		                 END

			
			          EXECUTE (@strSQL);

			          SET @nCount = @nCount + 1;
			          IF @nCount > @tYear
				      BEGIN
					       SET @nCount = 0;
				      END
		          END


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
        ' WHERE ItemCode = ' + CAST(@CommonNo2 AS VARCHAR(7)) +
		' AND TrnWarehouseNo = ' + CAST(@CommonNo1 AS VARCHAR(8)) +
		' AND TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @fDate + '''' + ')'
		
		EXECUTE (@strSQL);


	
--	EXECUTE Sp_CSGenerateTransactionDataSingle @CuType, @CuNo, @MemNo, @TrnCode, @AccType, @AccNo, @opDate,@fDate,0;

	SET @strSQL = 'DELETE FROM #WFA2ZSTTRANSACTION WHERE TransactionDate = ''' + @fDate + '''';
	EXECUTE (@strSQL);
--==========  Get Transaction Data For Opening Balance ==========

---------- Credit
			UPDATE #A2ZSTMST SET #A2ZSTMST.CalUnitQty = #A2ZSTMST.CalUnitQty + 
            ISNULL((SELECT SUM(TrnQtyCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = @CommonNo2 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM #A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE #A2ZSTMST.STKItemCode = @CommonNo2;
---------- Debit
            UPDATE #A2ZSTMST SET #A2ZSTMST.CalUnitQty = #A2ZSTMST.CalUnitQty - 
            ISNULL((SELECT SUM(TrnQtyDr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = @CommonNo2 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM #A2ZSTMST,#WFA2ZSTTRANSACTION
			WHERE #A2ZSTMST.STKItemCode = @CommonNo2;


--    END

--



--===========  End of For Opening Balance ========================

--===============  Find Out Debit Credit Amount ============
	
	SET @opBalance = (SELECT CalUnitQty FROM #A2ZSTMST WHERE STKItemCode = @CommonNo2);

	SET @debitAmt = 0;
	SET @creditAmt = 0;
	
	IF @opBalance > 0
	   BEGIN
			SET @creditAmt = ABS(@opBalance);
	   END
	ELSE
		BEGIN
			SET @debitAmt = ABS(@opBalance);
		END
	
	--=============  Find Out Debit Credit Amount ============
	DROP TABLE #A2ZSTMST;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

	SELECT * INTO #WFSTSTATEMENT FROM WFSTSTATEMENT WHERE ID = 0;
	TRUNCATE TABLE #WFSTSTATEMENT;

	INSERT INTO #WFSTSTATEMENT (TrnDate,ItemCode,TrnQtyDebit,TrnQtyCredit,TrnDesc,TrnType)
	VALUES (@fDate,@CommonNo2,@debitAmt,@creditAmt,'=== Opening Balance ===',0)
		
--=========== Get Transaction Data For Account Statement ================
	SET @fYear = LEFT(@fDate,4);
	SET @tYear = LEFT(@tDate,4);

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
					  ' FROM A2ZSTMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZSTTRANSACTION WHERE TrnProcFlag = 0' +
					  ' AND ItemCode = ' + CAST(@CommonNo2 AS VARCHAR(7)) +
					  ' AND TransactionDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + ''''; 

									 
					  IF @CommonNo1 <> 0
		                 BEGIN
		                       SET @strSQL = @strSQL + ' AND TrnWarehouseNo = ' + CAST(@CommonNo1 AS VARCHAR(8));
		                 END
			
			EXECUTE (@strSQL);

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 


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
        ' WHERE ItemCode = ' + CAST(@CommonNo2 AS VARCHAR(7)) +
		' AND TrnWarehouseNo = ' + CAST(@CommonNo1 AS VARCHAR(8)) +
		' AND TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'


		
		EXECUTE (@strSQL);
--=========== End of Get Transaction Data For Account Statement ================


	INSERT INTO #WFSTSTATEMENT (TrnDate,VchNo,VoucherNo,ItemCode,TrnType,TrnQtyDebit,TrnQtyCredit,TrnDesc,
	UserID)
	SELECT
	TransactionDate,VchNo,VoucherNo,ItemCode,TransactionType,TrnQtyDr,TrnQtyCr,TransactionNote,
	UserID
	FROM #WFA2ZSTTRANSACTION;

	
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

--============  Using Query ==============
--- @nFlag = 0 = Normal CS Account Statement
	IF @nFlag = 0
		BEGIN
			SELECT #WFSTSTATEMENT.Id,#WFSTSTATEMENT.VchNo, #WFSTSTATEMENT.VoucherNo, 
			#WFSTSTATEMENT.TrnDate, #WFSTSTATEMENT.TrnQtyDebit, #WFSTSTATEMENT.TrnQtyCredit, #WFSTSTATEMENT.TrnDesc, 
			#WFSTSTATEMENT.ItemCode, #WFSTSTATEMENT.TrnType
			FROM #WFSTSTATEMENT;
			
			
		END



END


GO

