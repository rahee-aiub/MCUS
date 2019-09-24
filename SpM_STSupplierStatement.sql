USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_STSupplierStatement]    Script Date: 06/25/2018 10:16:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [dbo].[SpM_STSupplierStatement](@CommonNo1 INT, @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS

---EXECUTE SpM_STSupplierStatement 1,'2018-03-01','2018-03-01',0



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
	SELECT * INTO #A2ZSUPPLIER FROM A2ZSUPPLIER WHERE SuppCode = @CommonNo1;

	UPDATE #A2ZSUPPLIER SET SuppBalance = 0;


    SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION WHERE ID = 0;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

    
 
	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-01-01';
    
	SET @openTable = 'A2ZSTMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZSTSUPPOPBAL';

	SET @strSQL = 'UPDATE #A2ZSUPPLIER SET #A2ZSUPPLIER.SuppBalance = ' +
		'ISNULL((SELECT SUM(STKSuppBalance) FROM ' + @openTable +  
		' WHERE STKSuppCode = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
		'),0) FROM #A2ZSUPPLIER,' + @openTable +
		' WHERE #A2ZSUPPLIER.SuppCode = ' + CAST(@CommonNo1 AS VARCHAR(7)); 
		
	
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
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)' +
				' SELECT ' +
				'TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +
				' FROM A2ZSTMCUS..A2ZSTTRANSACTION ' + 
        ' WHERE SupplierNo = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
		' AND TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@opDate + '''' + ' AND ' + '''' + @fDate + '''' + ')'
		
		EXECUTE (@strSQL);

--	EXECUTE Sp_CSGenerateTransactionDataSingle @CuType, @CuNo, @MemNo, @TrnCode, @AccType, @AccNo, @opDate,@fDate,0;

    
	SET @strSQL = 'DELETE FROM #WFA2ZSTTRANSACTION WHERE TransactionDate = ''' + @fDate + '''';
	EXECUTE (@strSQL);
--==========  Get Transaction Data For Opening Balance ==========

---------- Credit
			UPDATE #A2ZSUPPLIER SET #A2ZSUPPLIER.SuppBalance = #A2ZSUPPLIER.SuppBalance + 
            ISNULL((SELECT SUM(ItemNetCostPrice) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.SupplierNo = @CommonNo1 AND 
			#WFA2ZSTTRANSACTION.FuncOpt = 1 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM #A2ZSUPPLIER,#WFA2ZSTTRANSACTION
			WHERE #A2ZSUPPLIER.SuppCode = @CommonNo1;
---------- Debit
            UPDATE #A2ZSUPPLIER SET #A2ZSUPPLIER.SuppBalance = #A2ZSUPPLIER.SuppBalance - 
            ISNULL((SELECT SUM(ItemNetCostPrice) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.SupplierNo = @CommonNo1 AND 
			#WFA2ZSTTRANSACTION.FuncOpt = 61 AND #WFA2ZSTTRANSACTION.TrnProcFlag = 0),0)
			FROM #A2ZSUPPLIER,#WFA2ZSTTRANSACTION
			WHERE #A2ZSUPPLIER.SuppCode = @CommonNo1;


--    END

--



--===========  End of For Opening Balance ========================

--===============  Find Out Debit Credit Amount ============
	
	SET @opBalance = (SELECT SuppBalance FROM #A2ZSUPPLIER WHERE SuppCode = @CommonNo1);

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
	DROP TABLE #A2ZSUPPLIER;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

	SELECT * INTO #WFSTSUPPSTATEMENT FROM WFSTSUPPSTATEMENT WHERE ID = 0;
	TRUNCATE TABLE #WFSTSUPPSTATEMENT;

	INSERT INTO #WFSTSUPPSTATEMENT (TrnDate,SuppCode,TrnAmtDebit,TrnAmtCredit,TrnDesc,TrnType)
	VALUES (@fDate,@CommonNo1,@debitAmt,@creditAmt,'=== Opening Balance ===',0)

		
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
				      'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,0,ItemNetCostPrice,' +
				      'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				      'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +			  	   	          
					  ' FROM A2ZSTMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZSTTRANSACTION WHERE FuncOpt = 1 AND TrnProcFlag = 0' +
					  ' AND SupplierNo = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
					  ' AND TransactionDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + ''''; 

			EXECUTE (@strSQL);

			SET @strSQL = 'INSERT INTO #WFA2ZSTTRANSACTION (TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				      'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				      'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				      'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)' +
				      ' SELECT ' +
				      'TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				      'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,ItemNetCostPrice,0,' +
				      'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				      'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +			  	   	          
					  ' FROM A2ZSTMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZSTTRANSACTION WHERE FuncOpt = 61 AND TrnProcFlag = 0' +
					  ' AND SupplierNo = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
					  ' AND TransactionDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + ''''; 

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
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,0,ItemNetCostPrice,' +
				'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +
				' FROM A2ZSTMCUS..A2ZSTTRANSACTION ' + 
        ' WHERE SupplierNo = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
		' AND FuncOpt = 1 AND TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		EXECUTE (@strSQL);


		SET @strSQL = 'INSERT INTO #WFA2ZSTTRANSACTION (TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
				'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)' +
				' SELECT ' +
				'TransactionDate,VchNo,VoucherNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
				'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,ItemNetCostPrice,0,' +
				'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
				'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +
				' FROM A2ZSTMCUS..A2ZSTTRANSACTION ' + 
        ' WHERE SupplierNo = ' + CAST(@CommonNo1 AS VARCHAR(7)) +
		' AND FuncOpt = 61 AND TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'

		EXECUTE (@strSQL);

--=========== End of Get Transaction Data For Account Statement ================


	INSERT INTO #WFSTSUPPSTATEMENT (TrnDate,VchNo,VoucherNo,SuppCode,TrnType,TrnAmtDebit,TrnAmtCredit,TrnDesc,
	UserID)
	SELECT
	TransactionDate,VchNo,VoucherNo,SupplierNo,TransactionType,TrnAmtDr,TrnAmtCr,TransactionNote,
	UserID
	FROM #WFA2ZSTTRANSACTION;


		
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;

--============  Using Query ==============
--- @nFlag = 0 = Normal CS Account Statement
	IF @nFlag = 0
		BEGIN
			SELECT #WFSTSUPPSTATEMENT.Id,#WFSTSUPPSTATEMENT.VchNo, #WFSTSUPPSTATEMENT.VoucherNo, 
			#WFSTSUPPSTATEMENT.TrnDate, #WFSTSUPPSTATEMENT.TrnAmtDebit, #WFSTSUPPSTATEMENT.TrnAmtCredit, #WFSTSUPPSTATEMENT.TrnDesc, 
			#WFSTSUPPSTATEMENT.SuppCode, #WFSTSUPPSTATEMENT.TrnType
			FROM #WFSTSUPPSTATEMENT;
			
			
		END



END



GO

