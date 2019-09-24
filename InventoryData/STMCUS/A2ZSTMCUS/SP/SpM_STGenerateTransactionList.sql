USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_STGenerateTransactionList]    Script Date: 6/23/2018 10:19:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SpM_STGenerateTransactionList] (@fDate VARCHAR(10),@tDate VARCHAR(10),
@CommonNo1 INT,@CommonNo2 INT,@CommonNo3 INT,@TrnType SMALLINT,@VchNo NVARCHAR(20),@CommonNo4 INT,@nFlag INT)

AS
/*
EXECUTE SpM_STGenerateTransactionList '2018-03-01','2018-03-01',0,0,0,0,0,0,1


*/

BEGIN

	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @nCount INT;
	DECLARE @trnDrCr SMALLINT;
	DECLARE @VType NVARCHAR(1);

	SELECT * INTO #WFA2ZSTTRANSACTION FROM WFA2ZSTTRANSACTION;
	TRUNCATE TABLE #WFA2ZSTTRANSACTION;



    SET @VType = 'C';    

    SET @fYear = LEFT(@fDate,4);
	SET @tYear = LEFT(@tDate,4);

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
				' FROM A2ZSTMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZSTTRANSACTION' + 
        ' WHERE TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'


		    IF @CommonNo1 <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND TrnWarehouseNo = ' + CAST(@CommonNo1 AS VARCHAR(8));
				END		
					
            IF @CommonNo2 <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND ItemGroupNo = ' + CAST(@CommonNo2 AS VARCHAR(2));
				END		
				
		    IF @CommonNo3 <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND ItemCategoryNo = ' + CAST(@CommonNo3 AS VARCHAR(2));
				END			
				
             
            IF @TrnType <> 0
				BEGIN
                    IF @TrnType = 2
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1';
                       END
                    IF @TrnType = 3
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3';
                       END
				END

            IF @VchNo <> '0'
				BEGIN         
					 SET @strSQL = @strSQL + ' AND VchNo = ' + '''' + CAST(@VchNo AS VARCHAR(20)) + '''';          
				END	

                    
            IF @CommonNo4 <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND SupplierNo = ' + CAST(@CommonNo4 AS VARCHAR(4));
				END		
            
           
			SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@nFlag AS VARCHAR(2));
				
            
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
        ' WHERE TrnProcFlag = 0' +
       	' AND (TransactionDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'


		IF @CommonNo1 <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND TrnWarehouseNo = ' + CAST(@CommonNo1 AS VARCHAR(8));
				END		

		IF @CommonNo2 <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND ItemGroupNo = ' + CAST(@CommonNo2 AS VARCHAR(2));
				END		
				
		    IF @CommonNo3 <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND ItemCategoryNo = ' + CAST(@CommonNo3 AS VARCHAR(2));
				END			
				
             
            IF @TrnType <> 0
				BEGIN
                    IF @TrnType = 2
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1';
                       END
                    IF @TrnType = 3
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3';
                       END
				END

            IF @VchNo <> '0'
				BEGIN         
					 SET @strSQL = @strSQL + ' AND VchNo = ' + '''' + CAST(@VchNo AS VARCHAR(20)) + '''';          
				END	

                    
            IF @CommonNo4 <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND SupplierNo = ' + CAST(@CommonNo4 AS VARCHAR(4));
				END		
         
            SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@nFlag AS VARCHAR(2));
	
	EXECUTE (@strSQL);
       
	
	SELECT * FROM #WFA2ZSTTRANSACTION;
          	
END







GO

