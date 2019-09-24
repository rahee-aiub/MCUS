USE [A2ZHRMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_HREmployeeSettlementPost]    Script Date: 06/26/2019 3:32:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_HREmployeeSettlementPost] (@EmpCode INT, @VchNo NVARCHAR(20), @UserID INT, @nFlag INT, @FromCashCode INT)  

AS
BEGIN

	BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON
--
-- Last Modified Date on June 25, 2019
--
-- Sp_HREmployeeSettlementPost 403,'123',101,0
--

	
	DECLARE @TrnDate SMALLDATETIME;

	DECLARE @AccountHead NVARCHAR(100);	
	DECLARE @Amount MONEY;	
	DECLARE @Flag INT;
	DECLARE @GLCodeDr INT;
	DECLARE @GLCodeCr INT;	
	DECLARE @AccNo BIGINT;
	DECLARE @ShowInterest INT;
	DECLARE @TrnInterestAmt MONEY;	
	DECLARE @MemName NVARCHAR(100);		
	DECLARE @NetAmount MONEY;	
	DECLARE @VoucherNo NVARCHAR(20);

	DECLARE @LastVchNo INT;

	
	UPDATE A2ZCSMCUS..A2ZRECCTRLNO SET CtrlRecLastNo = CtrlRecLastNo + 1
	WHERE CtrlGLCashCode = @FromCashCode AND CtrlRecType = 1;

	SET @LastVchNo = (SELECT CtrlRecLastNo FROM A2ZCSMCUS..A2ZRECCTRLNO
		WHERE CtrlGLCashCode = @FromCashCode AND CtrlRecType = 1);

	SET @VoucherNo =  'C' + CAST(@FromCashCode AS NVARCHAR(8)) + '-'  + CAST(@LastVchNo AS NVARCHAR(6));
	
	SET @TrnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

	SET @NetAmount = (SELECT SUM(Amount) AS Benefit FROM A2ZHRMCUS..WFSETTLEMENT
	GROUP BY Flag HAVING (Flag = 1)) - (SELECT SUM(Amount) AS Benefit FROM A2ZHRMCUS..WFSETTLEMENT
	GROUP BY Flag HAVING (Flag = 2));

	DECLARE accTable CURSOR FOR 
	SELECT EmpCode,AccountHead,Amount,Flag,GLCodeDr,GLCodeCr,AccNo,ShowInterest,TrnInterestAmt
	FROM A2ZHRMCUS..WFSETTLEMENT;

	OPEN accTable;
	FETCH NEXT FROM accTable INTO
		@EmpCode,@AccountHead,@Amount,@Flag,@GLCodeDr,@GLCodeCr,@AccNo,@ShowInterest,@TrnInterestAmt;

			WHILE @@FETCH_STATUS = 0 
				BEGIN
					SET @MemName = (SELECT EmpName FROM A2ZHRMCUS..A2ZEMPLOYEE WHERE EmpCode = @EmpCode); 

					IF @Amount > 0 AND @Flag = 1 
						BEGIN
							IF LEFT(@GLCodeDr,1) IN (2,4)
								BEGIN
									INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,
									PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,ShowInterest,TrnInterestAmt,TrnPenalAmt,TrnChargeAmT,
									TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,
									TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,TrnPayment,UserID,VerifyUserID,MemName,
									ProvAdjFlag,AccTypeMode)
									VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,LEFT(@AccNo,2),@AccNo,@GLCodeDr,75,'Payroll Deposit',
									2,3,0,@Amount,0,'Emp Final Settlement','V',@ShowInterest,@TrnInterestAmt,0,0,
									0,0,0,0,@GLCodeDr,0,0,@GLCodeDr,(0-@Amount),@Amount,0,
									0,0,@FromCashCode,0,0,4,@TrnDate,1,@UserID,0,@MemName,0,2);
								END

							IF LEFT(@GLCodeDr,1) IN (1,5)
								BEGIN
									INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,
									PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,ShowInterest,TrnInterestAmt,TrnPenalAmt,TrnChargeAmT,
									TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,
									TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,TrnPayment,UserID,VerifyUserID,MemName,
									ProvAdjFlag,AccTypeMode)
									VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,LEFT(@AccNo,2),@AccNo,@GLCodeDr,75,'Payroll Deposit',
									2,3,0,@Amount,0,'Emp Final Settlement','V',@ShowInterest,@TrnInterestAmt,0,0,
									0,0,0,0,@GLCodeDr,0,0,@GLCodeDr,@Amount,@Amount,0,
									0,0,@FromCashCode,0,0,4,@TrnDate,1,@UserID,0,@MemName,0,2);
								END
						END

					IF @Amount > 0 AND @Flag = 2 
						BEGIN
							IF LEFT(@GLCodeCr,1) IN (1,5)
								BEGIN
									INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,
									PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,ShowInterest,TrnInterestAmt,TrnPenalAmt,TrnChargeAmT,
									TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,
									TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,TrnPayment,UserID,VerifyUserID,MemName,
									ProvAdjFlag,AccTypeMode)
									VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,LEFT(@AccNo,2),@AccNo,@GLCodeCr,75,'Payroll Deposit',
									2,3,1,0,@Amount,'Emp Final Settlement','V',@ShowInterest,@TrnInterestAmt,0,0,
									0,0,0,0,0,@GLCodeCr,0,@GLCodeCr,(0-@Amount),0,@Amount,
									0,0,@FromCashCode,0,0,4,@TrnDate,1,@UserID,0,@MemName,0,2);
								END

							IF LEFT(@GLCodeCr,1) IN (2,4)
								BEGIN
									INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,
									PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,ShowInterest,TrnInterestAmt,TrnPenalAmt,TrnChargeAmT,
									TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,
									TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,TrnPayment,UserID,VerifyUserID,MemName,
									ProvAdjFlag,AccTypeMode)
									VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,LEFT(@AccNo,2),@AccNo,@GLCodeCr,75,'Payroll Deposit',
									2,3,1,0,@Amount,'Emp Final Settlement','V',@ShowInterest,@TrnInterestAmt,0,0,
									0,0,0,0,0,@GLCodeCr,0,@GLCodeCr,@Amount,0,@Amount,
									0,0,@FromCashCode,0,0,4,@TrnDate,1,@UserID,0,@MemName,0,2);
								END
						END

	FETCH NEXT FROM accTable INTO
		@EmpCode,@AccountHead,@Amount,@Flag,@GLCodeDr,@GLCodeCr,@AccNo,@ShowInterest,@TrnInterestAmt;

	END

	CLOSE accTable; 
	DEALLOCATE accTable;
					
	IF @NetAmount > 0
		BEGIN
			INSERT INTO A2ZCSMCUS..A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,
			PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,ShowInterest,TrnInterestAmt,TrnPenalAmt,TrnChargeAmT,
			TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,
			TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,TrnPayment,UserID,VerifyUserID,MemName,
			ProvAdjFlag,AccTypeMode)
			VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,0,@AccNo,@GLCodeDr,75,'Payroll Deposit',
			2,3,0,0,0,'Emp Final Settlement','V',0,0,0,0,
			0,0,0,0,0,@FromCashCode,0,@FromCashCode,(0-@NetAmount),0,@NetAmount,
			0,0,@FromCashCode,0,0,4,@TrnDate,1,@UserID,0,@MemName,0,2);
		END

	UPDATE A2ZCSMCUS..A2ZTRANSACTION SET TrnCode = 20307001 
	WHERE TrnDate = @TrnDate AND VchNo = @VchNo AND AccType = 26 AND AccNo > 0;


	UPDATE A2ZHRMCUS..A2ZEMPLOYEE SET PrevStatus = Status, PrevStatusDate = StatusDate, Status = 9, StatusDate = @TrnDate WHERE EmpCode = @EmpCode;


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


GO

