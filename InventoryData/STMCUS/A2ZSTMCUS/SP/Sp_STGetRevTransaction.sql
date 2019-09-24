USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STGetRevTransaction]    Script Date: 6/23/2018 10:15:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE  [dbo].[Sp_STGetRevTransaction](@VoucherNo nvarchar(20),@UserId int)


AS
/*

EXECUTE Sp_STGetRevTransaction '3',1,1,'2016-04-02'


*/


BEGIN

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @accNo Bigint;
DECLARE @FuncOpt smallint;

DECLARE @AccStatus smallint;
DECLARE @strSQL NVARCHAR(MAX);



DELETE FROM WF_REVA2ZSTTRANSACTION WHERE DelUserId = @UserId OR VchNo = @VoucherNo;


INSERT INTO WF_REVA2ZSTTRANSACTION(DelUserId,TransactionDate,VchNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,
ItemCode,ItemName,ItemUnit,ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,
TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,
SupplierName,RcvWarehouseNo,RcvWarehouseName,IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId)

SELECT @UserId,TransactionDate,VchNo,FuncOpt,FuncOptDesc,ItemGroupNo,ItemGroupDesc,ItemCategoryNo,ItemCategoryDesc,
ItemCode,ItemName,ItemUnit,ItemUnitDesc,ItemUnitPrice,ItemSellPrice,ItemPurchaseQty,ItemNetCostPrice,ItemTotalPrice,TransactionType,
TransactionTypeDesc,TransactionNote,TrnAmtDr,TrnAmtCr,TrnQtyDr,TrnQtyCr,TrnMissQtyCr,TrnProcFlag,OrderNo,ChalanNo,SupplierNo,
SupplierName,RcvWarehouseNo,RcvWarehouseName,IssWarehouseNo,IssWarehouseName,TrnWarehouseNo,TrnWarehouseName,UserId 

FROM A2ZSTTRANSACTION WHERE VchNo = @VoucherNo;



END






GO

