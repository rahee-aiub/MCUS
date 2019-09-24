USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSUpdateTransferMonthlyBenefit]    Script Date: 10/27/2017 22:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_CSUpdateTransferMonthlyBenefit](@accType int, @VoucherNo nvarchar(20),@VchNo nvarchar(20), @FromCashCode int)  
AS
--EXECUTE Sp_CSUpdateTransferMonthlyBenefit 1,BenefitMSplus,10101001

BEGIN


DECLARE @trnCode1 int;
DECLARE @FuncOpt1 smallint;
DECLARE @PayType1 smallint;
DECLARE @TrnDesc1 nvarchar(50);
DECLARE @TrnType1 tinyint;
DECLARE @TrnDrCr1 tinyint;
DECLARE @ShowInterest1 tinyint;
DECLARE @TrnGLAccNoDr1 int;
DECLARE @TrnGLAccNoCr1 int;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON



       SET @trnCode1= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @accType);    
       SET @FuncOpt1= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 210);
       SET @PayType1 = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 210);
       SET @TrnType1= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 210);
       SET @TrnDrCr1= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 210);
       SET @ShowInterest1= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 210);
       SET @TrnGLAccNoDr1= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 210);
       SET @TrnGLAccNoCr1= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 210);
       SET @TrnDesc1 = 'Benefit Adj.Cr.';

       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
       TrnCredit,TrnDesc,ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,TrnPayment,AccTypeMode)

       SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,@FuncOpt1,@PayType1,@TrnType1,1,0,
       0,@TrnDesc1,@ShowInterest1,AccAdjProvBalance,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoCr1,AccAdjProvBalance,0,AccAdjProvBalance,0,@FromCashCode,0,0,UserID,0,1 
       FROM WFCSMONTHLYBENEFITCREDIT WHERE AccAdjProvBalance > 0;

       --------CONTRA ------

       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

       SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,@FuncOpt1,@PayType1,@TrnType1,0,AccAdjProvBalance,
       0,@TrnDesc1,@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoDr1,AccAdjProvBalance,AccAdjProvBalance,0,1,@FromCashCode,0,0,UserID 
       FROM WFCSMONTHLYBENEFITCREDIT WHERE AccAdjProvBalance > 0;

       ---------  END OF ADJ PROVISION CR. ------------


       -------- NORMAL ADJ PROVISION DR. ----------------

       SET @trnCode1= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @accType);
       SET @FuncOpt1= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 211);
       SET @PayType1 = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 211);
       SET @TrnType1= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 211);
       SET @TrnDrCr1= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 211);
       SET @ShowInterest1= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 211);
       SET @TrnGLAccNoDr1= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 211);
       SET @TrnGLAccNoCr1= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=21 AND PayType = 211);
       SET @TrnDesc1 = 'Benefit Adj.Dr.';

       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
       TrnCredit,TrnDesc,ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,TrnPayment,AccTypeMode)

       SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,@FuncOpt1,@PayType1,@TrnType1,0,0, 
       0,@TrnDesc1,@ShowInterest1,AccAdjProvBalance,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoDr1,AccAdjProvBalance,(0-AccAdjProvBalance),0,0,@FromCashCode,0,0,UserID,0,1 
       FROM WFCSMONTHLYBENEFITCREDIT WHERE AccAdjProvBalance < 0;

       -----------CONTRA -----

       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

       SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,@FuncOpt1,@PayType1,@TrnType1,1,0,
       (0-AccAdjProvBalance),@TrnDesc1,@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoCr1,(0-AccAdjProvBalance),0,(0-AccAdjProvBalance),1,@FromCashCode,0,0,UserID 
       FROM WFCSMONTHLYBENEFITCREDIT WHERE AccAdjProvBalance < 0;





--------------- NOPRMAL TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,0,0,
0,('Benefit Transfer to' + ' ' + CAST(AccCorrAccNo AS NVARCHAR(16))),ShowInterest,(0-AccTotalBenefitAmt),0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoDr,(0-AccTotalBenefitAmt),AccTotalBenefitAmt,0,0,@FromCashCode,1,0,UserID,1
FROM WFCSMONTHLYBENEFITCREDIT;
--------------- END OF NOPRMAL TRANSACTION ------------

--------------- CONTRA TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccCorrAccType,AccCorrAccNo,TrnGLAccNoCr,FuncOpt,1,TrnType,1,0,
AccTotalBenefitAmt,('Benefit Transfer from' + ' ' + CAST(AccNo AS NVARCHAR(16))),ShowInterest,0,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoCr,AccTotalBenefitAmt,0,AccTotalBenefitAmt,0,@FromCashCode,1,0,UserID,1
FROM WFCSMONTHLYBENEFITCREDIT;
--------------- END OF CONTRA TRANSACTION ------------



       

----------------- UPDATE A2ZACCOUNT FILE --------------------
UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevProvBalance = A2ZACCOUNT.AccProvBalance,   
                      A2ZACCOUNT.AccProvBalance = 0
                      
FROM A2ZACCOUNT,WFCSMONTHLYBENEFITCREDIT
WHERE A2ZACCOUNT.AccType = WFCSMONTHLYBENEFITCREDIT.AccType AND A2ZACCOUNT.AccNo = WFCSMONTHLYBENEFITCREDIT.AccNo AND 
A2ZACCOUNT.CuType = WFCSMONTHLYBENEFITCREDIT.CuType AND A2ZACCOUNT.CuNo = WFCSMONTHLYBENEFITCREDIT.CuNo AND 
A2ZACCOUNT.MemNo = WFCSMONTHLYBENEFITCREDIT.MemNo;

----------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSMONTHLYBENEFITCREDIT SET WFCSMONTHLYBENEFITCREDIT.ProcStat = 3;
UPDATE WFCSMONTHLYBENEFITCREDIT SET WFCSMONTHLYBENEFITCREDIT.VoucherNo = @VoucherNo;

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

END





