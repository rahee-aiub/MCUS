USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STGenerateConsolidated]    Script Date: 6/23/2018 10:15:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_STGenerateConsolidated](@WareHouse INT,@GrpCode INT, @SubGrpCode INT, @fDate SMALLDATETIME, @UserId INT, @nFlag INT)
AS
/*

EXECUTE Sp_STGenerateConsolidated 10101001,1,1,'2018-03-01',3,0

*/

BEGIN

DECLARE @fYear INT;
DECLARE @tYear INT;
DECLARE @tDate VARCHAR(10);
DECLARE @nCount INT;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @LastOpDate SMALLDATETIME;
DECLARE @opDate VARCHAR(10);


    
	SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION WHERE UserId = 0;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;


    DELETE FROM A2ZSTCONSOLIDATED  WHERE UserId = @UserId;

	INSERT INTO A2ZSTCONSOLIDATED(STKItemCode,STKItemName,STKItemUnit, UserId) 
	SELECT STKItemCode,STKItemName,STKUnit, @UserId FROM A2ZSTMST WHERE STKGroup = @GrpCode AND STKSubGroup = @SubGrpCode


	UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKItemUnitDesc = A2ZUNITCODE.UnitDesc FROM A2ZUNITCODE WHERE A2ZSTCONSOLIDATED.STKItemUnit = A2ZUNITCODE.UnitNo AND A2ZSTCONSOLIDATED.UserId = @UserId;
	
---------------------------------------------------------------------------------------------------------------

    EXECUTE Sp_STGenerateOpeningBalance @WareHouse,@GrpCode, @SubGrpCode, @fDate, @nFlag


	UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKOpUnitQty = A2ZSTMST.CalUnitQty, A2ZSTCONSOLIDATED.STKOpUnitRate = A2ZSTMST.STKUnitAvgCost	        
	FROM A2ZSTMST 
	WHERE A2ZSTCONSOLIDATED.STKItemCode = A2ZSTMST.STKItemCode AND A2ZSTCONSOLIDATED.UserId = @UserId AND A2ZSTMST.CalUnitQty > 0;

	UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKOpUnitAmt = (STKOpUnitQty * STKOpUnitRate)
	WHERE A2ZSTCONSOLIDATED.UserId = @UserId AND A2ZSTCONSOLIDATED.STKOpUnitQty > 0;
		

-----------------------------------------------------------------------------------------------------------------

    SET @LastOpDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @fDate)), DATEADD(mm, 1, @fDate)));

	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@fDate) AS VARCHAR(2)) + '-' + CAST(DAY(@fDate) AS VARCHAR(2))
    SET @tDate = CAST(YEAR(@LastOpDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@LastOpDate) AS VARCHAR(2)) + '-' + CAST(DAY(@LastOpDate) AS VARCHAR(2))

    SET @fYear = LEFT(@tDate,4);
	SET @tYear = LEFT(@tDate,4);

	SET @nCount = @fYear
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


---------- Credit Qty.
			UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKRcvUnitQty = ISNULL(A2ZSTCONSOLIDATED.STKRcvUnitQty,0) + 
            ISNULL((SELECT SUM(TrnQtyCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTCONSOLIDATED.STKItemCode),0)
			FROM A2ZSTCONSOLIDATED,#WFA2ZSTTRANSACTION
			WHERE A2ZSTCONSOLIDATED.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode AND 
			A2ZSTCONSOLIDATED.UserId = @UserId; 

---------- Credit Amt.
			UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKRcvUnitAmt = ISNULL(A2ZSTCONSOLIDATED.STKRcvUnitAmt,0) + 
            ISNULL((SELECT SUM(TrnAmtCr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTCONSOLIDATED.STKItemCode),0)
			FROM A2ZSTCONSOLIDATED,#WFA2ZSTTRANSACTION
			WHERE A2ZSTCONSOLIDATED.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode AND 
			A2ZSTCONSOLIDATED.UserId = @UserId; 
		
---------- Rcv Unit Cost
            
			
			UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKRcvUnitRate = round((A2ZSTCONSOLIDATED.STKRcvUnitAmt / A2ZSTCONSOLIDATED.STKRcvUnitQty),2)
	        WHERE A2ZSTCONSOLIDATED.UserId = @UserId AND A2ZSTCONSOLIDATED.STKRcvUnitQty > 0;


------------ Debit Qty.
            UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKIssUnitQty = ISNULL(A2ZSTCONSOLIDATED.STKIssUnitQty,0) + 
            ISNULL((SELECT SUM(TrnQtyDr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTCONSOLIDATED.STKItemCode),0)
			FROM A2ZSTCONSOLIDATED,#WFA2ZSTTRANSACTION
			WHERE A2ZSTCONSOLIDATED.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode AND
			A2ZSTCONSOLIDATED.UserId = @UserId; 

------------ Debit Amt.
            UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKIssUnitAmt = ISNULL(A2ZSTCONSOLIDATED.STKIssUnitAmt,0) + 
            ISNULL((SELECT SUM(ItemNetCostPrice) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTCONSOLIDATED.STKItemCode AND #WFA2ZSTTRANSACTION.TrnAmtDr > 0),0)
			FROM A2ZSTCONSOLIDATED,#WFA2ZSTTRANSACTION
			WHERE A2ZSTCONSOLIDATED.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode AND
			A2ZSTCONSOLIDATED.UserId = @UserId; 

------------ Total Sell Amount
            UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKTotalSellAmt = ISNULL(A2ZSTCONSOLIDATED.STKTotalSellAmt,0) + 
            ISNULL((SELECT SUM(TrnAmtDr) FROM #WFA2ZSTTRANSACTION
			WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTCONSOLIDATED.STKItemCode AND #WFA2ZSTTRANSACTION.TrnAmtDr > 0),0)
			FROM A2ZSTCONSOLIDATED,#WFA2ZSTTRANSACTION
			WHERE A2ZSTCONSOLIDATED.STKItemCode = #WFA2ZSTTRANSACTION.ItemCode AND
			A2ZSTCONSOLIDATED.UserId = @UserId; 


---------- Iss. Unit Cost
            

			
			UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKIssUnitRate = round((A2ZSTCONSOLIDATED.STKIssUnitAmt / A2ZSTCONSOLIDATED.STKIssUnitQty),2)
	        WHERE A2ZSTCONSOLIDATED.UserId = @UserId AND A2ZSTCONSOLIDATED.STKIssUnitQty > 0;



    UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKUnitTotalAmt = (ISNULL(A2ZSTCONSOLIDATED.STKOpUnitAmt,0) + ISNULL(STKRcvUnitAmt,0))
	WHERE A2ZSTCONSOLIDATED.UserId = @UserId;

    UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKCloUnitQty = ((ISNULL(A2ZSTCONSOLIDATED.STKOpUnitQty,0) + ISNULL(STKRcvUnitQty,0)) - ISNULL(STKIssUnitQty,0))
	WHERE A2ZSTCONSOLIDATED.UserId = @UserId;

	UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKCloUnitAmt = ((ISNULL(A2ZSTCONSOLIDATED.STKOpUnitAmt,0) + ISNULL(STKRcvUnitAmt,0)) - ISNULL(STKIssUnitAmt,0))
	WHERE A2ZSTCONSOLIDATED.UserId = @UserId;

	UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKCloUnitRate = round((A2ZSTCONSOLIDATED.STKCloUnitAmt / STKCloUnitQty),2)
	WHERE A2ZSTCONSOLIDATED.UserId = @UserId AND STKCloUnitQty > 0;


	UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKSellingPrice = #WFA2ZSTTRANSACTION.ItemSellPrice
	FROM #WFA2ZSTTRANSACTION
	WHERE #WFA2ZSTTRANSACTION.ItemCode = A2ZSTCONSOLIDATED.STKItemCode AND A2ZSTCONSOLIDATED.UserId = @UserId;
	
	UPDATE A2ZSTCONSOLIDATED SET A2ZSTCONSOLIDATED.STKTotalProfit = ((ISNULL(A2ZSTCONSOLIDATED.STKIssUnitQty,0) * ISNULL(STKSellingPrice,0)) - ISNULL(STKIssUnitAmt,0))
	WHERE A2ZSTCONSOLIDATED.UserId = @UserId;

END



















GO

