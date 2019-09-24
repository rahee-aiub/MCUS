USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptSTTransactionVch]    Script Date: 06/06/2018 10:08:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*
EXECUTE Sp_rptSTTransactionVch 1 
*/


CREATE PROCEDURE [dbo].[Sp_rptSTTransactionVch] (@CommonNo1 tinyint,@fDate SMALLDATETIME) 
AS
BEGIN

DECLARE @trnDate SMALLDATETIME;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nYear INT;	

SET @trnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);


SET @nYear = YEAR(@fDate);


SELECT     TOP (100) PERCENT A2ZCSMCUS.dbo.A2ZTRANSACTION.VchNo, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnFlag, SUM(A2ZCSMCUS.dbo.A2ZTRANSACTION.GLDebitAmt) AS GLDebitAmt, 
                      SUM(A2ZCSMCUS.dbo.A2ZTRANSACTION.GLCreditAmt) AS GLCreditAmt, A2ZCSMCUS.dbo.A2ZTRANSACTION.UserID, A2ZCSMCUS.dbo.A2ZTRANSACTION.VerifyUserID, 
                      A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, A2ZCSMCUS.dbo.A2ZTRANSACTION.GLAccNo, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnCSGL, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnDate 
                      
FROM         A2ZCSMCUS.dbo.A2ZTRANSACTION LEFT OUTER JOIN
                      A2ZGLMCUS.dbo.A2ZCGLMST ON A2ZCSMCUS.dbo.A2ZTRANSACTION.GLAccNo = A2ZGLMCUS.dbo.A2ZCGLMST.GLAccNo
WHERE     (A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnCSGL = @CommonNo1)

GROUP BY A2ZCSMCUS.dbo.A2ZTRANSACTION.VchNo, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnFlag, A2ZCSMCUS.dbo.A2ZTRANSACTION.UserID, A2ZCSMCUS.dbo.A2ZTRANSACTION.VerifyUserID, 
                      A2ZGLMCUS.dbo.A2ZCGLMST.GLAccDesc, A2ZCSMCUS.dbo.A2ZTRANSACTION.GLAccNo, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnCSGL, A2ZCSMCUS.dbo.A2ZTRANSACTION.TrnDate 
                     
ORDER BY A2ZCSMCUS.dbo.A2ZTRANSACTION.GLAccNo


--SET @strSQL = SELECT        dbo.A2ZCGLMST.GLAccNo, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.VchNo, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.TrnFlag, SUM(A2ZCSMCUST2018.dbo.A2ZTRANSACTION.GLDebitAmt) AS GLDebitAmt, 
--                         SUM(A2ZCSMCUST2018.dbo.A2ZTRANSACTION.GLCreditAmt) AS GLCreditAmt, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.UserID, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.VerifyUserID, 
--                         dbo.A2ZCGLMST.GLAccDesc, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.TrnCSGL, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.TrnDate
--FROM            dbo.A2ZCGLMST INNER JOIN
--                         A2ZCSMCUST2018.dbo.A2ZTRANSACTION ON dbo.A2ZCGLMST.GLAccNo = A2ZCSMCUST2018.dbo.A2ZTRANSACTION.GLAccNo
--GROUP BY dbo.A2ZCGLMST.GLAccNo, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.VchNo, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.TrnFlag, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.UserID, 
--                         A2ZCSMCUST2018.dbo.A2ZTRANSACTION.VerifyUserID, dbo.A2ZCGLMST.GLAccDesc, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.TrnCSGL, A2ZCSMCUST2018.dbo.A2ZTRANSACTION.TrnDate






--SET @strSQL = ' SELECT ' +
--			  'VchNo,TrnFlag,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,ItemCode,ItemName,ItemUnit,' +
--			  'ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,' +
--			  'TrnQtyDr,TrnQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,SupplierName,RcvWarehouseNo,RcvWarehouseName,' +
--			  'IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId' +			  	   	          


--			  ' FROM A2ZSTMCUST' + CAST(@nYear AS VARCHAR(4)) + '..A2ZSTTRANSACTION WHERE TrnProcFlag = 0 AND TransactionDate' +
--			  ' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @fDate + ''''; 
					 
--EXECUTE (@strSQL);



END



GO

