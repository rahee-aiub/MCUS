USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSGetTrfTransaction]    Script Date: 07/25/2018 1:08:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE  [dbo].[Sp_CSGetTrfTransaction](@VoucherNo nvarchar(20),@UserId int,@Module int,@TrnDate VARCHAR(10))


AS
/*

EXECUTE Sp_CSGetTrfTransaction '3',1,1,'2016-04-02'


*/


BEGIN

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @accNo Bigint;
DECLARE @FuncOpt smallint;

DECLARE @AccStatus smallint;


EXECUTE Sp_CSGenerateGetTransactionData @TrnDate,@TrnDate,0;

DELETE FROM WF_TRFTRANSACTION WHERE DelUserId = @UserId OR VchNo = @VoucherNo;

INSERT INTO WF_TRFTRANSACTION(DelUserId,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,PayType,TrnType,
TrnDrCr,TrnDesc,TrnMode,TrnCredit,TrnDebit,TrnInterestAmt,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,TrnSysUser,TrnModule,UserID,FromCashCode)
SELECT @UserId,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,PayType,TrnType,
TrnDrCr,TrnDesc,TrnDrCr,TrnCredit,TrnDebit,TrnInterestAmt,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,TrnSysUser,TrnModule,UserID,FromCashCode
FROM WFA2ZTRANSACTION 
WHERE VchNo = @VoucherNo AND TrnCSGL = 0 AND TrnFlag = 0 AND TrnDate = @TrnDate AND PayType !=101 AND PayType !=201 AND FuncOpt != 7 AND FuncOpt != 8 AND FuncOpt != 9 AND FuncOpt != 10 AND FuncOpt != 13 AND FuncOpt != 14 AND FuncOpt != 15 AND FuncOpt != 16 AND FuncOpt != 20 AND FuncOpt != 75   




END



GO

