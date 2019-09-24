USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetTransaction]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[Sp_CSGetTransaction](@VoucherNo nvarchar(20),@UserId int,@Module int,@TrnDate VARCHAR(10))


AS
/*

EXECUTE Sp_CSGetTransaction '3',1,1,'2016-04-02'


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

DELETE FROM WF_REVERSETRANSACTION WHERE DelUserId = @UserId OR VchNo = @VoucherNo;

INSERT INTO WF_REVERSETRANSACTION(DelUserId,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,PayType,TrnType,
TrnDrCr,TrnDesc,TrnMode,TrnCredit,TrnDebit,TrnInterestAmt,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,TrnSysUser,TrnModule,UserID,FromCashCode)
SELECT @UserId,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,PayType,TrnType,
TrnDrCr,TrnDesc,TrnDrCr,TrnCredit,TrnDebit,TrnInterestAmt,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,TrnSysUser,TrnModule,UserID,FromCashCode
FROM WFA2ZTRANSACTION 
WHERE VchNo = @VoucherNo AND TrnCSGL = 0 AND TrnFlag = 0 AND TrnDate = @TrnDate AND PayType !=101 AND PayType !=201 AND FuncOpt != 7 AND FuncOpt != 8 AND FuncOpt != 9 AND FuncOpt != 10 AND FuncOpt != 13 AND FuncOpt != 14 AND FuncOpt != 15 AND FuncOpt != 16 AND FuncOpt != 75   



DECLARE trnTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,FuncOpt
FROM WF_REVERSETRANSACTION;

OPEN trnTable;
FETCH NEXT FROM trnTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@FuncOpt;

WHILE @@FETCH_STATUS = 0 
	BEGIN

    
	SET @AccStatus = (SELECT AccStatus FROM A2ZACCOUNT WHERE AccNo = @accNo);

    IF @AccStatus = 99 
       BEGIN
           IF @FuncOpt = 2 OR @FuncOpt = 9 OR @FuncOpt = 10
              BEGIN
                  UPDATE WF_REVERSETRANSACTION SET TrnRevFlag = 0 WHERE AccNo = @accNo;
              END
           ELSE
              BEGIN
                  UPDATE WF_REVERSETRANSACTION SET TrnRevFlag = 1 WHERE AccNo = @accNo;
              END
       END

	FETCH NEXT FROM trnTable INTO
		@cuType,@cuNo,@memNo,@accType,@accNo,@FuncOpt;


	END

CLOSE trnTable; 
DEALLOCATE trnTable;



END

GO
