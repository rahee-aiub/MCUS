USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSUpdateMonthlyBenefit]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSUpdateMonthlyBenefit](@VoucherNo nvarchar(20),@VchNo nvarchar(20), @FromCashCode int)  
AS
--EXECUTE Sp_CSUpdateMonthlyBenefit 1,BenefitMSplus,10101001

BEGIN


--BEGIN TRY
--	BEGIN TRANSACTION
--		SET NOCOUNT ON

--------------- NOPRMAL TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,1,0,
0,TrnDesc,ShowInterest,CalBenefit,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoCr,CalBenefit,0,CalBenefit,0,@FromCashCode,1,1,UserID,1
FROM WFCSMONTHLYBENEFIT;
--------------- END OF NOPRMAL TRANSACTION ------------

--------------- CONTRA TRANSACTION ------------


INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID,AccTypeMode)

SELECT TrnDate,@VchNo,@VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,0,0,
0,TrnDesc,ShowInterest,CalBenefit,0,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLAccNoDr,CalBenefit,CalBenefit,0,1,@FromCashCode,1,1,UserID,1
FROM WFCSMONTHLYBENEFIT;
--------------- END OF CONTRA TRANSACTION ------------

----------------- UPDATE A2ZACCOUNT FILE --------------------
UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccPrevBenefitDate = A2ZACCOUNT.AccBenefitDate,
                      A2ZACCOUNT.AccPrevProvBalance = A2ZACCOUNT.AccProvBalance,   
                      A2ZACCOUNT.AccPrevNoBenefit = A2ZACCOUNT.AccNoBenefit,     
                      
                      A2ZACCOUNT.AccBenefitDate = WFCSMONTHLYBENEFIT.NewBenefitDate,
                      A2ZACCOUNT.AccProvBalance = (A2ZACCOUNT.AccProvBalance + WFCSMONTHLYBENEFIT.CalBenefit),
                      A2ZACCOUNT.AccNoBenefit = WFCSMONTHLYBENEFIT.NewNoBenefit,
                      A2ZACCOUNT.AccProvCalDate = WFCSMONTHLYBENEFIT.CalBenefitDate


FROM A2ZACCOUNT,WFCSMONTHLYBENEFIT
WHERE A2ZACCOUNT.AccType = WFCSMONTHLYBENEFIT.AccType AND A2ZACCOUNT.AccNo = WFCSMONTHLYBENEFIT.AccNo AND 
A2ZACCOUNT.CuType = WFCSMONTHLYBENEFIT.CuType AND A2ZACCOUNT.CuNo = WFCSMONTHLYBENEFIT.CuNo AND 
A2ZACCOUNT.MemNo = WFCSMONTHLYBENEFIT.MemNo;

----------------- END OF UPDATE A2ZACCOUNT FILE --------------------

UPDATE WFCSMONTHLYBENEFIT SET WFCSMONTHLYBENEFIT.ProcStat = 3;
UPDATE WFCSMONTHLYBENEFIT SET WFCSMONTHLYBENEFIT.VoucherNo = @VoucherNo;

--COMMIT TRANSACTION
--		SET NOCOUNT OFF
--END TRY
--
--BEGIN CATCH
--		ROLLBACK TRANSACTION
--
--		DECLARE @ErrorSeverity INT
--		DECLARE @ErrorState INT
--		DECLARE @ErrorMessage NVARCHAR(4000);	  
--		SELECT 
--			@ErrorMessage = ERROR_MESSAGE(),
--			@ErrorSeverity = ERROR_SEVERITY(),
--			@ErrorState = ERROR_STATE();	  
--		RAISERROR 
--		(
--			@ErrorMessage, -- Message text.
--			@ErrorSeverity, -- Severity.
--			@ErrorState -- State.
--		);	
--END CATCH

END

GO
