USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSManualTransactionTransfer]    Script Date: 06/26/2019 3:33:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_CSManualTransactionTransfer] (@TrnDate VARCHAR(10),@VoucherNo NVARCHAR(20),@VchNo NVARCHAR(20),@TrnDrCr INT,
														@TrnDesc VARCHAR(100),@GLContraCode INT,@GLCashCode INT)
AS
BEGIN
/*

EXECUTE Sp_CSManualTransactionTransfer '2019-04-04','C10101','23',11,0,'asas',50101001,10101001;

*/

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

DECLARE @TrnCode INT;

DECLARE @CuType INT;
DECLARE @CreditUnionNo INT;
DECLARE @DepositorNo INT;
DECLARE @AccNo BIGINT;
DECLARE @TrnAmount MONEY;

DECLARE @PayType INT;

DECLARE @GLDebitCode INT;
DECLARE @GLCreditCode INT;
DECLARE @GLCode INT;

DECLARE @ContraGLDebitCode INT;
DECLARE @ContraGLCreditCode INT;
DECLARE @ContraGLCode INT;
DECLARE @ContraDrCr INT;

DECLARE @DebitAmt MONEY;
DECLARE @CreditAmt MONEY;

DECLARE @ContraDebitAmt MONEY;
DECLARE @ContraCreditAmt MONEY;

DECLARE @GLCodeType INT;
DECLARE @GLAmount MONEY;
DECLARE @ContraGLAmount MONEY;
	
DECLARE @AccType INT;
DECLARE @UserId INT;

		

				DECLARE wfTable CURSOR FOR
				SELECT CuType,CreditUnionNo,DepositorNo,AccNo,TrnAmount,UserId FROM WFTRANSFERTRN;

				OPEN wfTable;
				FETCH NEXT FROM wfTable INTO
				@CuType,@CreditUnionNo,@DepositorNo,@AccNo,@TrnAmount,@UserId;

				WHILE @@FETCH_STATUS = 0 
					BEGIN

					SET @AccType = LEFT(@AccNo,2); 
					SET @TrnCode = (SELECT TOP(1) TrnCode FROM A2ZTRNCODE WHERE AccType = @AccType);

					
					----------------------------------------
							IF @TrnDrCr = 0
							   BEGIN
								   SET @GLDebitCode = @TrnCode;
								   SET @GLCreditCode = @GLContraCode;
								   SET @GLCode = @TrnCode;

								   SET @ContraGLDebitCode = @TrnCode;
								   SET @ContraGLCreditCode = @GLContraCode;
								   SET @ContraGLCode = @GLContraCode;

								   SET @ContraDrCr = 1;

								   SET @PayType = 2;

								   IF(LEFT(@GLCode,1) = 2 OR LEFT(@GLCode,1) = 4)
									  BEGIN
										   SET @GLAmount = (0-@TrnAmount);
									  END
								   ELSE
									  BEGIN
										   SET @GLAmount = @TrnAmount;
									   END

                                   IF(LEFT(@ContraGLCode,1) = 2 OR LEFT(@ContraGLCode,1) = 4)
									  BEGIN
										   SET @ContraGLAmount = (0-@TrnAmount);
									  END
								   ELSE
									  BEGIN
										   SET @ContraGLAmount = @TrnAmount;
									   END

							   END

							IF @TrnDrCr = 1
							   BEGIN
								   SET @GLDebitCode = @GLContraCode;
								   SET @GLCreditCode = @TrnCode;
								   SET @GLCode = @TrnCode;

								   
								   SET @ContraGLCode = @GLContraCode;

								   SET @ContraDrCr = 0;

								   SET @PayType = 1;


								   IF(LEFT(@GLCode,1) = 1 OR LEFT(@GLCode,1) = 5)
									  BEGIN
										   SET @GLAmount = (0 - @TrnAmount);
									  END
								   ELSE
									  BEGIN
										   SET @GLAmount = @TrnAmount;
									   END
                                   
								   IF(LEFT(@ContraGLCode,1) = 1 OR LEFT(@ContraGLCode,1) = 5)
									  BEGIN
										   SET @ContraGLAmount = (0 - @TrnAmount);
									  END
								   ELSE
									  BEGIN
										   SET @ContraGLAmount = @TrnAmount;
									   END

							   END
					----------------------------------------
					    IF @TrnDrCr = 0
						   BEGIN
						        SET @DebitAmt = @TrnAmount;
								SET @CreditAmt = 0;
								SET @ContraDebitAmt = 0;
								SET @ContraCreditAmt = @TrnAmount;
						   END

                        IF @TrnDrCr = 1
						   BEGIN
						        SET @DebitAmt = 0;
								SET @CreditAmt = @TrnAmount;
								SET @ContraDebitAmt = @TrnAmount;
								SET @ContraCreditAmt = 0;								
						   END

						INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDrCr,TrnDebit,TrnCredit,
						TrnDesc,FuncOpt,FuncOptDesc,PayType,TrnType,TrnCSGL,TrnProcStat,TrnFlag,TrnModule,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,UserId,VerifyUserID)
			
						VALUES (@TrnDate,@VchNo,@VoucherNo,@CuType,@CreditUnionNo,@DepositorNo,@AccType,@AccNo,@TrnCode,@TrnDrCr,@DebitAmt,@CreditAmt,
						@TrnDesc,25,'Multi Transaction',@PayType,3,0,0,0,1,@GLDebitCode,@GLCreditCode,@GLCode,@GLAmount,@DebitAmt,@CreditAmt,@GLCashCode,@UserId,@UserId);
	

						INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDrCr,TrnDebit,TrnCredit,
						TrnDesc,FuncOpt,FuncOptDesc,PayType,TrnType,TrnCSGL,TrnProcStat,TrnFlag,TrnModule,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,UserId,VerifyUserID)
				
						VALUES (@TrnDate,@VchNo,@VoucherNo,@CuType,@CreditUnionNo,@DepositorNo,@AccType,@AccNo,@TrnCode,@ContraDrCr,@ContraDebitAmt,@ContraCreditAmt,
						@TrnDesc,25,'Multi Transaction',@PayType,3,0,0,1,1,@GLDebitCode,@GLCreditCode,@ContraGLCode,@ContraGLAmount,@ContraDebitAmt,@ContraCreditAmt,@GLCashCode,@UserId,@UserId);
	


					  FETCH NEXT FROM wfTable INTO
				 			@CuType,@CreditUnionNo,@DepositorNo,@AccNo,@TrnAmount,@UserId;

					END


				CLOSE wfTable; 
				DEALLOCATE wfTable;
		
		

    TRUNCATE TABLE WFTRANSFERTRN;

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

