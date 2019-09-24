USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSAddDepositTransaction]    Script Date: 07/17/2018 3:13:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE  [dbo].[Sp_CSAddDepositTransaction](@userID INT, @vchNo nvarchar(20), @ProcStat smallint,@Module int)

--ALTER PROCEDURE  [dbo].[Sp_CSAddDepositTransaction]
AS
BEGIN


/*

EXECUTE Sp_CSAddDepositTransaction 1,4444,0,0,0,0,1

*/

--DECLARE @userID INT;
--DECLARE @vchNo nvarchar(20);
--DECLARE @ProcStat smallint;
--DECLARE @NewAccFlag smallint;
--DECLARE @NewAccType INT; 
--DECLARE @NewAccNo INT;


DECLARE @cuType INT;
DECLARE @cuNo INT;
DECLARE @memNo INT;
DECLARE @accType INT;
DECLARE @accNo BIGINT;
DECLARE @payType INT;
DECLARE @DueIntAmt MONEY;
DECLARE @creditAmount MONEY;
DECLARE @debitAmount MONEY;
DECLARE @TrnInterestAmt MONEY;
DECLARE @trnDate SMALLDATETIME;
DECLARE @valueDate SMALLDATETIME;
DECLARE @trnType INT;
DECLARE @FromCashCode INT;
DECLARE @LoanApplicationNo INT;


DECLARE @DueIntFlag INT;
DECLARE @ProvAdjFlag INT;

DECLARE @CurrDuePrincAmt MONEY;
DECLARE @CurrDueIntAmt MONEY;
DECLARE @UptoDuePrincAmt MONEY;
DECLARE @AccLoanInstlAmt MONEY;
DECLARE @NoDueInstalment MONEY;
DECLARE @PayablePrincAmt MONEY;
DECLARE @PayableIntAmt MONEY;
DECLARE @PaidPrincAmt MONEY;
DECLARE @PaidIntAmt MONEY;

DECLARE @CurrDueDepositAmt MONEY;
DECLARE @UptoDueDepositAmt MONEY;
DECLARE @AccMonthlyDeposit MONEY;
DECLARE @NoDueDeposit MONEY;
DECLARE @PayableDepositAmt MONEY;
DECLARE @PaidDepositAmt MONEY;

DECLARE @MemFlag smallint;
DECLARE @AccFlag smallint;
DECLARE @MemName nvarchar(50);
DECLARE @NewMemType INT;
DECLARE @NewAccPeriod INT;
DECLARE @NewAccIntRate INT;
DECLARE @NewAccContractIntFlag smallint; 
DECLARE @NewOpenDate SMALLDATETIME;
DECLARE @NewMaturityDate SMALLDATETIME;

DECLARE @NewFixedAmt MONEY;
DECLARE @NewFixedMthInt MONEY;
DECLARE @NewBenefitDate SMALLDATETIME;

DECLARE @trnCode1 int;
DECLARE @FuncOpt1 smallint;
DECLARE @PayType1 smallint;
DECLARE @TrnDesc1 nvarchar(50);
DECLARE @TrnType1 tinyint;
DECLARE @TrnDrCr1 tinyint;
DECLARE @ShowInterest1 tinyint;
DECLARE @TrnGLAccNoDr1 int;
DECLARE @TrnGLAccNoCr1 int;
DECLARE @ProvAdj INT;

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

SET @ProvAdjFlag = 0;
SET @DueIntFlag = 0;

--- Update Account Table base on Workfile ---
--
--SET @userID = 1;
--SET @vchNo = 1;
--SET @ProcStat = 1;
--SET @NewAccFlag = 0;
--SET @NewAccType = 0; 
--SET @NewAccNo = 1111111;



DECLARE wfTrnTable CURSOR FOR
SELECT TrnDate,CuType,CuNo,MemNo,AccType,AccNo,PayTypeCode,Credit,Debit,TrnInterestAmt,TrnDate,TrnTypeCode,TrnDueIntAmt,NewMem,NewAcc,MemName,NewMemType,NewAccPeriod,NewAccIntRate,NewAccContractIntFlag,NewOpenDate,NewMaturityDate,NewFixedAmt,NewFixedMthInt,NewBenefitDate,ProvAdjFlag
FROM WF_Transaction
WHERE TrnFlag = 0 AND UserId = @userId;

			
OPEN wfTrnTable; 
FETCH NEXT FROM wfTrnTable INTO @trnDate,@cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@TrnInterestAmt,@trnDate,@trnType,@DueIntAmt,@MemFlag,@AccFlag,@MemName,@NewMemType,@NewAccPeriod,@NewAccIntRate,@NewAccContractIntFlag,@NewOpenDate,@NewMaturityDate,@NewFixedAmt,@NewFixedMthInt,@NewBenefitDate,@ProvAdj; 
WHILE @@FETCH_STATUS = 0 
BEGIN
      
    	 
--  For All Account Transaction --------
		BEGIN
			UPDATE A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = @trnType,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = (@creditAmount + @debitAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

--  For Share Account Transaction --------

IF @payType = 1 OR @payType = 2 OR @payType > 500
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance + (@creditAmount - @debitAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
--  End of For Share Account Transaction ---------
    

IF @payType = 3
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;




--  For Loan Interest Received Transaction ---------

IF @payType = 402
		BEGIN
			UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + @creditAmount)			                      
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		
            
--            UPDATE A2ZLOANDEFAULTER SET PaidIntAmt = (PaidIntAmt + @creditAmount)                                                           				
--			    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                      AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                      YEAR(TrnDate) = YEAR(@trnDate);
--
--            SET @PayableIntAmt = (SELECT PayableIntAmt FROM A2ZLOANDEFAULTER WHERE 
--                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                       YEAR(TrnDate) = YEAR(@trnDate));
--             
--            SET @PaidIntAmt = (SELECT PaidIntAmt FROM A2ZLOANDEFAULTER WHERE 
--                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                       YEAR(TrnDate) = YEAR(@trnDate));
--            
--            SET @CurrDueIntAmt = (@PayableIntAmt - @PaidIntAmt);
--
--            IF @CurrDueIntAmt < 0
--               BEGIN
--                  SET @CurrDueIntAmt = 0;
--               END
--
--            UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = @CurrDueIntAmt                                                                 				
--		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                         YEAR(TrnDate) = YEAR(@trnDate);            

        END;



--  For Loan Amount Received Transaction ---------

IF @payType = 403 
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @creditAmount),
					AccPrincipal = (AccPrincipal + @creditAmount),
                    AccTotalDep = (AccTotalDep + @creditAmount)               
	
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
                              AccType = @accType AND AccNo = @accNo;   

--            UPDATE A2ZLOANDEFAULTER SET PaidPrincAmt = (PaidPrincAmt + @creditAmount)                                                              				
--		         WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                      AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                      YEAR(TrnDate) = YEAR(@trnDate);
--                               
--            SET @PayablePrincAmt = (SELECT PayablePrincAmt FROM A2ZLOANDEFAULTER WHERE 
--                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                       YEAR(TrnDate) = YEAR(@trnDate));
--             
--            SET @PaidPrincAmt = (SELECT PaidPrincAmt FROM A2ZLOANDEFAULTER WHERE 
--                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                       YEAR(TrnDate) = YEAR(@trnDate));
--            
--            SET @CurrDuePrincAmt = (@PayablePrincAmt - @PaidPrincAmt);
--
--            IF @CurrDuePrincAmt < 0
--               BEGIN
--                  SET @CurrDuePrincAmt = 0;
--               END
--
--            UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = @CurrDuePrincAmt                                                                 				
--		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                         YEAR(TrnDate) = YEAR(@trnDate);            
--
--            SET @UptoDuePrincAmt = (SELECT UptoDuePrincAmt FROM A2ZLOANDEFAULTER WHERE 
--                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                       YEAR(TrnDate) = YEAR(@trnDate));
--        
--            IF @UptoDuePrincAmt > 0
--               BEGIN
--                   SET @CurrDuePrincAmt = (SELECT CurrDuePrincAmt FROM A2ZLOANDEFAULTER WHERE 
--                               CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                               AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                               YEAR(TrnDate) = YEAR(@trnDate));
--            
--                   SET @AccLoanInstlAmt = (SELECT AccLoanInstlAmt FROM A2ZACCOUNT WHERE 
--                               CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
--                                   AccType = @accType AND AccNo = @accNo);                          
--
--            
--                   SET @NoDueInstalment = (@CurrDuePrincAmt / ABS(@AccLoanInstlAmt));
--            
--                   UPDATE A2ZLOANDEFAULTER SET NoDueInstalment = @NoDueInstalment                                                                 				
--		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                         YEAR(TrnDate) = YEAR(@trnDate);
--               END

		END;

--  For Loan Amount Penalty Transaction ---------

IF @payType = 404
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;

--            UPDATE A2ZLOANDEFAULTER SET PaidPenalAmt = (PaidPenalAmt + @creditAmount)
--                                                                                                    				
--		    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                 YEAR(TrnDate) = YEAR(@trnDate);


		END;




--  For OD Loan Interest Received Transaction ---------

----IF @payType = 352
----		BEGIN
----			SET @DueIntFlag = 1;
----            UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + @creditAmount),
----                   AccPrevDueIntAmt = AccDueIntAmt,	
----                   AccDueIntAmt = @DueIntAmt,
----                   AccPrevODIntDate = AccODIntDate, AccODIntDate = @trnDate,
----                   AccHoldIntAmt = 0
                   				
----			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
----		END;

--  For OD Loan Amount Received Transaction ---------

IF @payType = 353 
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @creditAmount),
					AccPrincipal = (AccPrincipal + @creditAmount),
                    AccTotalDep = (AccTotalDep + @creditAmount)
                    --AccPrevODIntDate = AccODIntDate, AccODIntDate = @trnDate	                				
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		
           ---- IF @DueIntFlag = 0
           ----    BEGIN
           ----         UPDATE A2ZACCOUNT SET AccDueIntAmt = @DueIntAmt,  AccHoldIntAmt = 0             				
			        ----WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
           ----    END

        END;



--  For Pension Account Transaction ---------

IF @payType = 301
   BEGIN
      	UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @creditAmount),
            AccTotalDep = (AccTotalDep + @creditAmount)
            
    	WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
        
--            UPDATE A2ZPENSIONDEFAULTER SET PaidDepositAmt = (PaidDepositAmt + @creditAmount)                                                              				
--		         WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                      AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                      YEAR(TrnDate) = YEAR(@trnDate);
--                               
--            SET @PayableDepositAmt = (SELECT PayableDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
--                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                       YEAR(TrnDate) = YEAR(@trnDate));
--             
--            SET @PaidDepositAmt = (SELECT PaidDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
--                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                       YEAR(TrnDate) = YEAR(@trnDate));
--            
--            SET @CurrDueDepositAmt = (@PayableDepositAmt - @PaidDepositAmt);
--
--            UPDATE A2ZPENSIONDEFAULTER SET CurrDueDepositAmt = @CurrDueDepositAmt                                                                 				
--		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                         YEAR(TrnDate) = YEAR(@trnDate);            
--
--            SET @UptoDueDepositAmt = (SELECT UptoDueDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
--                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                       YEAR(TrnDate) = YEAR(@trnDate));
--        
--            IF @UptoDueDepositAmt > 0
--               BEGIN
--                   SET @CurrDueDepositAmt = (SELECT CurrDueDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
--                               CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                               AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                               YEAR(TrnDate) = YEAR(@trnDate));
--            
--                   SET @AccMonthlyDeposit = (SELECT AccMonthlyDeposit FROM A2ZACCOUNT WHERE 
--                               CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
--                                   AccType = @accType AND AccNo = @accNo);                          
--
--            
--                   SET @NoDueDeposit = (@CurrDueDepositAmt / ABS(@AccMonthlyDeposit));
--            
--                   UPDATE A2ZPENSIONDEFAULTER SET NoDueDeposit = @NoDueDeposit                                                                 				
--		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                         YEAR(TrnDate) = YEAR(@trnDate);
--               END        

	END;

--  End Of Pension Account Transaction ---------

IF @payType = 302
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;

--            UPDATE A2ZPENSIONDEFAULTER SET PaidPenalAmt = (PaidPenalAmt + @creditAmount)
--                                                                                                    				
--		    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                 YEAR(TrnDate) = YEAR(@trnDate);        

		END;

-- For Time Deposite Account Transaction --------- 

IF @payType = 101 OR @payType = 201
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance + (@creditAmount - @debitAmount),
					AccPrincipal = AccPrincipal + (@creditAmount - @debitAmount),
                    AccOrgAmt = AccOrgAmt + (@creditAmount - @debitAmount)
					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

------ End Of Time Deposite Account Transaction -------



FETCH NEXT FROM wfTrnTable INTO @trnDate,@cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@TrnInterestAmt,@trnDate,@trnType,@DueIntAmt,@MemFlag,@AccFlag,@MemName,@NewMemType,@NewAccPeriod,@NewAccIntRate,@NewAccContractIntFlag,@NewOpenDate,@NewMaturityDate,@NewFixedAmt,@NewFixedMthInt,@NewBenefitDate,@ProvAdj; 


END;	       
CLOSE wfTrnTable; 
DEALLOCATE wfTrnTable;


-- Move Workfile Transaction to Original Transaction Table ----

INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,
TrnDrCr,TrnDesc,TrnVchType,TrnCredit,TrnDebit,ShowInterest,TrnInterestAmt,TrnPenalAmt,TrnDueIntAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,TrnPayment,UserID,CuName,MemName,NewCU,NewMem,NewAcc,NewMemType,NewAccPeriod,
NewAccIntRate,NewAccContractIntFlag,NewOpenDate,NewMaturityDate,NewFixedAmt,NewFixedMthInt,NewBenefitDate,ProvAdjFlag,AccTypeMode,TrnEditFlag,TrnOrgAmt)

SELECT TrnDate,VchNo,@vchNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,PayTypeCode,TrnTypeCode,
TrnMode,PayTypeDes,TrnVchType,Credit,Debit,ShowInt,TrnInterestAmt,TrnPenalAmt,TrnDueIntAmt,TrnCSGL,GLAccNoDR,GLAccNoCR,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,@ProcStat,FromCashCode,TrnModule,ValueDate,TrnPayment,UserID,CuName,MemName,NewCU,NewMem,NewAcc,NewMemType,
NewAccPeriod,NewAccIntRate,NewAccContractIntFlag,NewOpenDate,NewMaturityDate,NewFixedAmt,NewFixedMthInt,NewBenefitDate,@ProvAdj,AccTypeMode,TrnEditFlag,TrnOrgAmt 
FROM WF_Transaction WHERE UserId = @userId;
-- End of Move Workfile Transaction to Original Transaction Table ----

DELETE FROM WF_Transaction WHERE UserId = @userId;

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

GO

