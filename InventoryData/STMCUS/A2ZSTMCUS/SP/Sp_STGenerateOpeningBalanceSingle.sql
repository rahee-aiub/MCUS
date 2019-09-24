USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STGenerateOpeningBalanceSingle]    Script Date: 6/23/2018 10:15:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_STGenerateOpeningBalanceSingle](@Warehouse INT, @ItemCode INT, @fDate VARCHAR(10), @nFlag INT)
AS
/*
EXECUTE Sp_STGenerateOpeningBalanceSingle 3,5,0,20201001,12,1230005000000001,'2015-10-01',0



*/

BEGIN

	UPDATE A2ZSTMST SET STKOpUnitQty = 0 WHERE STKItemCode = @ItemCode; 
	
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);

    DECLARE @BegYear int;
    DECLARE @IYear int;


	DECLARE @nYear INT;	

	SET @nDate = @fDate;

	SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-01-01';

    SET @openTable = 'A2ZSTMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZSTOPBALANCE';
	
	IF @WareHouse <> 0
	   BEGIN
	        SET @strSQL = 'UPDATE A2ZSTMST SET A2ZSTMST.STKOpUnitQty = ' +
		    'ISNULL((SELECT SUM(STKUnitQty) FROM ' + @openTable +  
		    ' WHERE STKWarehouseNo = ' + CAST(@WareHouse AS VARCHAR(8)) + 
		    ' AND STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7)) + 
		    '),0) FROM A2ZSTMST,' + @openTable +
		    ' WHERE A2ZSTMST.STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7)); 
	   END


    IF @WareHouse = 0
	   BEGIN
	        SET @strSQL = 'UPDATE A2ZSTMST SET A2ZSTMST.STKOpUnitQty = ' +
		    'ISNULL((SELECT SUM(STKUnitQty) FROM ' + @openTable +  
		    ' WHERE STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7)) + 
		    '),0) FROM A2ZSTMST,' + @openTable +
		    ' WHERE A2ZSTMST.STKItemCode = ' + CAST(@ItemCode AS VARCHAR(7)); 
	   END

	
	EXECUTE (@strSQL);



			--EXECUTE Sp_CSGenerateTransactionDataSingle @CuType, @CuNo, @MemNo, @TrnCode, @AccType, @AccNo, @opDate,@fDate,0;


            SET @strSQL = 'DELETE FROM WFA2ZSTTRANSACTION WHERE TrnDate = ''' + @fDate + '''';			
			EXECUTE (@strSQL);


---------- Credit Qty
			UPDATE A2ZSTMST SET A2ZSTMST.STKOpUnitQty = A2ZSTMST.STKOpUnitQty + 
            ISNULL((SELECT SUM(TrnQtyCr) FROM WFA2ZSTTRANSACTION
			WHERE WFA2ZSTTRANSACTION.ItemCode = @ItemCode),0)
			FROM A2ZSTMST,WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = @ItemCode;

------------ Debit Qty
            UPDATE A2ZSTMST SET A2ZSTMST.STKOpUnitQty = A2ZSTMST.STKOpUnitQty - 
            ISNULL((SELECT SUM(TrnQtyDr) FROM WFA2ZSTTRANSACTION
			WHERE WFA2ZSTTRANSACTION.ItemCode = @ItemCode),0)
			FROM A2ZSTMST,WFA2ZSTTRANSACTION
			WHERE A2ZSTMST.STKItemCode = @ItemCode;

END


GO

