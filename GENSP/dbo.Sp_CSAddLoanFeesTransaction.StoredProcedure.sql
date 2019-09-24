USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAddLoanFeesTransaction]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[Sp_CSAddLoanFeesTransaction](@accType INT, @userID INT, @vchNo nvarchar(20),@VoucherNo nvarchar(20),@CashCode int,@LoanFeesAmt Money,@cuType int,@cuNo int,@memNo int,@MemName nvarchar(40))

--ALTER PROCEDURE  [dbo].[Sp_CSAddLoanFeesTransaction]
AS
BEGIN


DECLARE @trnDate SMALLDATETIME;

DECLARE @trnCode int;
DECLARE @FuncOpt smallint;
DECLARE @PayType smallint;
DECLARE @TrnDesc nvarchar(50);
DECLARE @TrnType tinyint;
DECLARE @TrnDrCr tinyint;
DECLARE @ShowInterest tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;

DECLARE @CTYPE varchar(20);
DECLARE @CNO varchar(20);
DECLARE @Input varchar(10);
DECLARE @Result varchar(10);
DECLARE @CountAccNo varchar(20);
DECLARE @AccountNo varchar(20);

   
SET @CTYPE = @cuType;  
SET @CNO=@cuNo;
SET @Input=len(@CNO);
     
IF(@Input='1')
   BEGIN
    	SET @Result='000'; 
   END
     
IF(@Input='2')
	BEGIN
		SET @Result='00'; 
	END
IF(@Input='3')
    BEGIN
    	SET @Result='0';
    END

IF(@Input!='4')
   BEGIN
		SET @AccountNo='99'+@CTYPE+@Result+@CNO+'00000'+'0'+'507'; 
   END

IF(@Input='4')
   BEGIN
		SET @AccountNo='99'+@CTYPE+@CNO+'00000'+'0'+'507'; 
   END
      
          
SET @CountAccNo= (SELECT AccNo FROM A2ZACCOUNT WHERE  AccNo=@AccountNo);
      

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @trnCode= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 99 AND PayType=507);

SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 99 AND PayType=507);

SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 99 AND PayType=507);
SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = 99 AND PayType=507);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 99 AND PayType=507);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 99 AND PayType=507);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 99 AND PayType=507);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 99 AND PayType=507);

SET @TrnDesc = (@TrnDesc + ' ' + 'from' + ' ' + @MemName);

--
---- Move Workfile Transaction to Original Transaction Table ----

INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,
TrnDrCr,TrnDesc,TrnCredit,TrnDebit,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,TrnPayment,ValueDate,UserID)

VALUES (@trnDate,@vchNo,@VoucherNo,0,@cuType,@cuNo,@memNo,99,@CountAccNo,@trnCode,@FuncOpt,507,@TrnType,
@TrnDrCr,@TrnDesc,@LoanFeesAmt,0,0,@CashCode,@TrnGLAccNoCr,@TrnGLAccNoCr,@LoanFeesAmt,
0,@LoanFeesAmt,0,0,@CashCode,1,1,@trnDate,@userID) 


INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,
TrnDrCr,TrnDesc,TrnCredit,TrnDebit,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,TrnPayment,ValueDate,UserID)

VALUES (@trnDate,@vchNo,@VoucherNo,0,@cuType,@cuNo,@memNo,99,@CountAccNo,@trnCode,@FuncOpt,507,@TrnType,
@TrnDrCr,@TrnDesc,0,@LoanFeesAmt,0,@CashCode,@TrnGLAccNoCr,@CashCode,@LoanFeesAmt,
@LoanFeesAmt,0,1,0,@CashCode,1,1,@trnDate,@userID) 






END;

GO
