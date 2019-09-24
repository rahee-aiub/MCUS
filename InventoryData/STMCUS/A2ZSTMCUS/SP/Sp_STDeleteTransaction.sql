USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STDeleteTransaction]    Script Date: 6/23/2018 10:07:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_STDeleteTransaction](@VoucherNo nvarchar(20),@UserId int,@lblFuncOpt int,@Warehouse int)


AS
BEGIN

DECLARE @cuType INT;
DECLARE @cuNo INT;



DELETE FROM A2ZSTTRANSACTION  WHERE VchNo = @VoucherNo AND TrnWarehouseNo = @Warehouse;

DELETE FROM A2ZCSMCUS..A2ZTRANSACTION  WHERE VchNo = @VoucherNo AND TrnModule = 5;


DELETE FROM WF_REVA2ZSTTRANSACTION WHERE DelUserId = @UserId;


IF @lblFuncOpt = 2
   BEGIN
      UPDATE A2ZSTTRANSFER SET TrnProcFlag = 1 WHERE VchNo = @VoucherNo AND TrnWarehouseNo = @Warehouse; 
   END

IF @lblFuncOpt = 12
   BEGIN
      DELETE FROM A2ZSTTRANSFER  WHERE VchNo = @VoucherNo AND IssWarehouseNo = @Warehouse;
   END


END


GO

