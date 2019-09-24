
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
EXECUTE Sp_rptCSTransactionVch 1,2015
*/


ALTER PROCEDURE [dbo].[Sp_rptCSTransactionVch] @CommonNo5 int, @CommonNo6 int
AS
BEGIN

DECLARE @openTable VARCHAR(30)
DECLARE @strSQL NVARCHAR(MAX);

IF @CommonNo5 = 0
   BEGIN
        SELECT     CuNo, CuType, MemNo, CAST(AccNo AS VARCHAR(16)) AS AccNo, VchNo, VoucherNo, AccType, TrnDate, TrnCredit, TrnDebit, TrnDesc, TrnType, PayType, TrnFlag, 
                      TrnCSGL, UserID, VerifyUserID, TrnProcStat, TrnModule, FromCashCode, GLAmount, TrnCode, TrnPayment, TrnChqPrx, TrnChqNo, TrnGLAccNoDr, TrnGLAccNoCr
        FROM         dbo.A2ZTRANSACTION
        WHERE     (TrnFlag = 0 AND TrnCSGL = 0 AND TrnPayment = 1)
   END

IF @CommonNo5 = 1
   BEGIN
        SET @openTable = 'A2ZCSMCUST' + CAST(@CommonNo6 AS VARCHAR(4))  + '..A2ZTRANSACTION';

        PRINT @openTable;

--        SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZCSOPBALANCE';

        SET @strSQL = 'SELECT CuNo, CuType, MemNo, CAST(AccNo AS VARCHAR(16)) AS AccNo, VchNo, VoucherNo, AccType, TrnDate, TrnCredit, TrnDebit, TrnDesc, TrnType, PayType, TrnFlag, 
                      TrnCSGL, UserID, VerifyUserID, TrnProcStat, TrnModule, FromCashCode, GLAmount, TrnCode, TrnPayment, TrnChqPrx, TrnChqNo, TrnGLAccNoDr, TrnGLAccNoCr
        FROM ' + @openTable +
        ' WHERE (TrnFlag = 0 AND TrnCSGL = 0 AND TrnPayment = 1)'
        
        
		EXECUTE (@strSQL);
   END

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

