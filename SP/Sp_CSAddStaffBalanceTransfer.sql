USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAddStaffBalanceTransfer]    Script Date: 11/03/2016 11:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE  [dbo].[Sp_CSAddStaffBalanceTransfer]
 @userID int
,@CtrlVoucherNo varchar(20)
,@CtrlProcStat smallint
,@txtTranDate smalldatetime
,@txtVchNo varchar(20)
,@txtMemNo int
,@txtAccType int
,@txtAccNo Bigint

,@txtTrnMemNo int
,@txtTrnAccType int
,@txtTrnAccNo Bigint
,@txtTrnAmount money
,@txtDescription varchar(50)    
,@CashCode int     
,@lblpaytype int    
,@lbltrnpaytype int             


AS
BEGIN


DECLARE @trnCode int;
DECLARE @FuncOpt smallint;
DECLARE @FuncOptDesc nvarchar(50);
DECLARE @PayType smallint;
DECLARE @TrnDesc nvarchar(50);
DECLARE @TrnType tinyint;
DECLARE @TrnDrCr tinyint;
DECLARE @ShowInterest tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;
DECLARE @TrnPayment tinyint;
DECLARE @trnDate smalldatetime;

DECLARE @trnAmt money;

DECLARE @Type tinyint;

------------ from transaction -----------------


BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON



SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @txtAccType AND PayType = @lblpaytype);

IF @lblpaytype = 0
   BEGIN
        SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0);
        SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0 );
        SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0);
        SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0);
        SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0);
        SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0);
        SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0);
   END
ELSE
   BEGIN
        SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0 AND PayType = @lblpaytype);
        SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0 AND PayType = @lblpaytype);
        SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0 AND PayType = @lblpaytype);
        SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0 AND PayType = @lblpaytype);
        SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0 AND PayType = @lblpaytype);
        SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0 AND PayType = @lblpaytype);
        SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtAccType AND FuncOpt=20 AND TrnMode = 0 AND PayType = @lblpaytype);
   END

BEGIN
     UPDATE A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
				AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @txtTranDate, AccLastTrnAmtU = @txtTrnAmount,AccBalance = (AccBalance - @txtTrnAmount)
	 WHERE CuType = 0 AND CuNo = 0 AND MemNo = @txtMemNo AND AccType = @txtAccType AND AccNo = @txtAccNo;
END		


SET @FuncOptDesc = 'A/c Balance Transfer';

SET @trnAmt = @txtTrnAmount;

SET @Type = LEFT(@TrnGLAccNoDr,1);
IF @Type = 2
   BEGIN
       SET @trnAmt = (0-@txtTrnAmount);
   END

INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,
FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,ShowInterest,TrnInterestAmt,
TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,TrnModule,FromCashCode,TrnPayment,UserID)

VALUES (@txtTranDate,@txtVchNo,@CtrlVoucherNo,0,0,0,@txtMemNo,@txtAccType,@txtAccNo,@trnCode,
@FuncOpt,@FuncOptDesc,@PayType,3,0,@txtDescription,0,@txtTrnAmount,0,0,
0,@TrnGLAccNoDr,0,@TrnGLAccNoDr,@trnAmt,@txtTrnAmount,0,0,@CtrlProcStat,1,@CashCode,@TrnPayment,@userID) 


------------ to transaction -----------------

SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @txtTrnAccType AND PayType = @lbltrnpaytype);

IF @lbltrnpaytype = 0
   BEGIN
        SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
        SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
        SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
        SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1); 
        SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
        SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
        SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
   END
ELSE
   BEGIN
        SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype); 
        SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
   END



BEGIN
     UPDATE A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
				AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @txtTranDate, AccLastTrnAmtU = @txtTrnAmount,AccBalance = (AccBalance + @txtTrnAmount)
	 WHERE CuType = 0 AND CuNo = 0 AND MemNo = @txtTrnMemNo AND AccType = @txtTrnAccType AND AccNo = @txtTrnAccNo;
END		

SET @trnAmt = @txtTrnAmount;

SET @Type = LEFT(@TrnGLAccNoCr,1);
IF @Type = 1
   BEGIN
       SET @trnAmt = (0-@txtTrnAmount);
   END


INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,
FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,ShowInterest,TrnInterestAmt,
TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,TrnModule,FromCashCode,TrnPayment,UserID)

VALUES (@txtTranDate,@txtVchNo,@CtrlVoucherNo,0,0,0,@txtTrnMemNo,@txtTrnAccType,@txtTrnAccNo,@trnCode,
@FuncOpt,@FuncOptDesc,@PayType,3,1,@txtDescription,@txtTrnAmount,0,0,0,
0,0,@TrnGLAccNoCr,@TrnGLAccNoCr,@trnAmt,0,@txtTrnAmount,0,@CtrlProcStat,1,@CashCode,@TrnPayment,@userID) 


COMMIT TRANSACTION
	SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH

END;

