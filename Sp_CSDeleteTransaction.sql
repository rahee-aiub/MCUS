USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSDeleteTransaction]    Script Date: 07/17/2018 3:14:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE  [dbo].[Sp_CSDeleteTransaction](@VoucherNo nvarchar(20),@UserId int,@Module int)


AS
BEGIN

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
DECLARE @trnType INT;
DECLARE @LoanApplicationNo INT;

DECLARE @UptoDuePrincAmt MONEY;
DECLARE @CurrDuePrincAmt MONEY;
DECLARE @CurrDueIntAmt MONEY;
DECLARE @AccLoanInstlAmt MONEY;
DECLARE @NoDueInstalment MONEY;
DECLARE @PayablePrincAmt MONEY;
DECLARE @PayableIntAmt MONEY;
DECLARE @PaidPrincAmt MONEY;
DECLARE @PaidIntAmt MONEY;

DECLARE @UptoDueDepositAmt MONEY;
DECLARE @CurrDueDepositAmt MONEY;
DECLARE @AccMonthlyDeposit MONEY;
DECLARE @NoDueDeposit MONEY;
DECLARE @PayableDepositAmt MONEY;
DECLARE @PaidDepositAmt MONEY;
DECLARE @probAdjFlag INT;
DECLARE @FuncOpt INT;


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);
-----------------------REWRITE A2ZACCOUNT---------------------------------
DECLARE wfTrnTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,PayType,TrnCredit,TrnDebit,TrnInterestAmt,TrnDueIntAmt,TrnDate,TrnType,ProvAdjFlag,FuncOpt
FROM WF_REVERSETRANSACTION 
WHERE DelUserId = @userId AND TrnModule = @Module;

			
OPEN wfTrnTable; 
FETCH NEXT FROM wfTrnTable INTO @cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@TrnInterestAmt,@DueIntAmt,@trnDate,@trnType,@probAdjFlag,@FuncOpt; 
WHILE @@FETCH_STATUS = 0 
BEGIN
	 
--  For All Account Transaction --------
		BEGIN
			UPDATE A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

--  For Share Account Transaction --------
IF @payType = 1 OR @payType = 2 OR @payType > 500
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance - (@creditAmount - @debitAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
--  End of For Share Account Transaction ---------
    

IF @payType = 3
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid - @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;



--  For Loan Account Transaction ---------

IF @payType = 401
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance - (@creditAmount - @debitAmount),
					AccPrincipal = AccPrincipal - (@creditAmount - @debitAmount),
                    AccDisbAmt = AccDisbAmt + (@creditAmount - @debitAmount),
					AccDisbDate = Null
					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

IF @payType = 402
		BEGIN
			UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid - @creditAmount)                   				
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		
--            UPDATE A2ZLOANDEFAULTER SET PaidIntAmt = (PaidIntAmt - @creditAmount)                                                              				
--			WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                 YEAR(TrnDate) = YEAR(@trnDate);
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



IF @payType = 403
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance - @creditAmount),
					AccPrincipal = (AccPrincipal - @creditAmount),
                    AccTotalDep = (AccTotalDep - @creditAmount)
                                                       				
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		
--            UPDATE A2ZLOANDEFAULTER SET PaidPrincAmt = (PaidPrincAmt - @creditAmount)                                                                				
--		    	WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                   AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                   YEAR(TrnDate) = YEAR(@trnDate);
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
--                    SET @CurrDuePrincAmt = (SELECT CurrDuePrincAmt FROM A2ZLOANDEFAULTER WHERE 
--                              CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                              AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                              YEAR(TrnDate) = YEAR(@trnDate));
--            
--                    SET @AccLoanInstlAmt = (SELECT AccLoanInstlAmt FROM A2ZACCOUNT WHERE 
--                              CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
--                              AccType = @accType AND AccNo = @accNo);                          
--          
--                    SET @NoDueInstalment = (@CurrDuePrincAmt / ABS(@AccLoanInstlAmt));
--            
--                    UPDATE A2ZLOANDEFAULTER SET NoDueInstalment = @NoDueInstalment                                                                 				
--		            WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                          AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                          YEAR(TrnDate) = YEAR(@trnDate);
--               END
        END;

--  For OD Loan Withdrawal Transaction ---------

IF @payType = 351
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance - (@creditAmount - @debitAmount),
					AccPrincipal = AccPrincipal - (@creditAmount - @debitAmount),
                    AccHoldIntAmt = (AccHoldIntAmt - @DueIntAmt)						
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;


----IF @payType = 352
----		BEGIN
----			UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid - @creditAmount),
----                                  AccDueIntAmt = (AccDueIntAmt + @creditAmount)	
------                                  AccODIntDate = AccPrevODIntDate
				
----			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
----		END;


IF @payType = 353
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance - @creditAmount),
					AccPrincipal = (AccPrincipal - @creditAmount),
                    AccTotalDep = (AccTotalDep - @creditAmount)
--                    AccODIntDate = AccPrevODIntDate
                   				
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;



IF @payType = 404
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
            
--            UPDATE A2ZLOANDEFAULTER SET PaidPenalAmt = (PaidPenalAmt - @creditAmount)
--                                                                                                    				
--		    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                 YEAR(TrnDate) = YEAR(@trnDate);

		END;


--
--
--
--IF @payType = 404
--		BEGIN
--         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid - @creditAmount)
--			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
--		END;

--  For Pension Account Transaction ---------

IF @payType = 301
		BEGIN
         	UPDATE A2ZACCOUNT SET AccBalance = (AccBalance - @creditAmount),
            AccTotalDep = (AccTotalDep - @creditAmount)
            
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;

            
--            UPDATE A2ZPENSIONDEFAULTER SET PaidDepositAmt = (PaidDepositAmt - @creditAmount)                                                                				
--		    	WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                   AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                   YEAR(TrnDate) = YEAR(@trnDate);
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
--            IF @CurrDueDepositAmt < 0
--               BEGIN
--                  SET @CurrDueDepositAmt = 0;
--               END
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
--                    SET @CurrDueDepositAmt = (SELECT CurrDueDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
--                              CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                              AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                              YEAR(TrnDate) = YEAR(@trnDate));
--            
--                    SET @AccMonthlyDeposit = (SELECT AccMonthlyDeposit FROM A2ZACCOUNT WHERE 
--                              CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
--                              AccType = @accType AND AccNo = @accNo);                          
--          
--                    SET @NoDueDeposit = (@CurrDueDepositAmt / ABS(@AccMonthlyDeposit));
--            
--                    UPDATE A2ZPENSIONDEFAULTER SET NoDueDeposit = @NoDueDeposit                                                                 				
--		            WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                          AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                          YEAR(TrnDate) = YEAR(@trnDate);
--               END

		END;

--  End Of Pension Account Transaction ---------

IF @payType = 302
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid - @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;

--            UPDATE A2ZPENSIONDEFAULTER SET PaidPenalAmt = (PaidPenalAmt - @creditAmount)
--                                                                                                    				
--		    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
--                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
--                 YEAR(TrnDate) = YEAR(@trnDate);

		END;

IF @payType = 306 
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccPrevRaLedger                     					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

-- For Time Deposite Account Transaction --------- 

IF @payType = 101 OR @payType = 201
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance - (@creditAmount - @debitAmount),
					AccPrincipal = AccPrincipal - (@creditAmount - @debitAmount),
                    AccOrgAmt = AccOrgAmt - (@creditAmount - @debitAmount)
					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

------ End Of Time Deposite Account Transaction -------

-----  Time Deposit Interest Withdrawn ------------------
IF @payType = 105
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @debitAmount),
                    AccRenwlAmt = (AccRenwlAmt + @debitAmount)
                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
-----  End of Time Deposit Interest Withdrawn ------------------

-----  Time Deposit Encashment ------------------
IF @payType = 110 
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccPrevRaLedger                     					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
-----  End of Time Deposit Interest Withdrawn ------------------


----  Loan Settlement --------------------------------------

IF @payType = 406 
		BEGIN
			            
            UPDATE A2ZACCOUNT SET AccBalance = AccPrevRaLedger, AccStatus = 1, AccStatusDate = AccStatusDateP, AccStatusNote = ''                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		
------------------------------------------------------------------------------------------
            SET @LoanApplicationNo = (SELECT AccLoanAppNo FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo);

            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccStatus = A2ZACCOUNT.AccStatusP,A2ZACCOUNT.AccStatusDate = A2ZACCOUNT.AccStatusDateP
			FROM A2ZACCOUNT,A2ZACGUAR
			WHERE A2ZACCOUNT.AccType = A2ZACGUAR.AccType AND A2ZACCOUNT.AccNo = A2ZACGUAR.AccNo AND 
            A2ZACCOUNT.CuType = A2ZACGUAR.CuType AND A2ZACCOUNT.CuNo = A2ZACGUAR.CuNo AND 
            A2ZACCOUNT.MemNo = A2ZACGUAR.MemNo AND A2ZACGUAR.LoanApplicationNo = @LoanApplicationNo AND
			A2ZACCOUNT.AccLienAmt = 0;       

            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccLienAmt = A2ZACCOUNT.AccLienAmt + 
			(SELECT A2ZACGUAR.AccAmount FROM A2ZACGUAR
            WHERE A2ZACCOUNT.AccType = A2ZACGUAR.AccType AND A2ZACCOUNT.AccNo = A2ZACGUAR.AccNo AND 
            A2ZACCOUNT.CuType = A2ZACGUAR.CuType AND A2ZACCOUNT.CuNo = A2ZACGUAR.CuNo AND 
            A2ZACCOUNT.MemNo = A2ZACGUAR.MemNo AND A2ZACGUAR.LoanApplicationNo = @LoanApplicationNo)
			FROM A2ZACCOUNT,A2ZACGUAR
			WHERE A2ZACCOUNT.AccType = A2ZACGUAR.AccType AND A2ZACCOUNT.AccNo = A2ZACGUAR.AccNo AND 
            A2ZACCOUNT.CuType = A2ZACGUAR.CuType AND A2ZACCOUNT.CuNo = A2ZACGUAR.CuNo AND 
            A2ZACCOUNT.MemNo = A2ZACGUAR.MemNo AND A2ZACGUAR.LoanApplicationNo = @LoanApplicationNo;            
		

        END;


------- End of Loan Settlement ----------------------




-----  Time Deposit Benefit Withdrawn ------------------
IF @payType = 203
		BEGIN
			UPDATE A2ZACCOUNT SET AccProvBalance = (AccProvBalance - @TrnInterestAmt),
                                  AccToTIntWdrawn = (AccToTIntWdrawn + @TrnInterestAmt)    
                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;


-----  End of Time Deposit Benefit Withdrawn ------------------

-----  Time Deposit Benefit Withdrawn ------------------
IF @payType = 210 or @payType = 211 
		BEGIN
			UPDATE A2ZACCOUNT SET AccProvBalance = (AccProvBalance - @TrnInterestAmt),
                                  AccAdjProvBalance = @TrnInterestAmt                     					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;


-----  End of Time Deposit Benefit Withdrawn ------------------

-----  Time Deposit Benefit Encashment Amount ------------------
IF @payType = 207  
	   BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccPrevRaLedger                     					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;	

-----  End of Time Deposit Benefit Encashment Amount ------------------

--  For Adjustment Transaction --------

IF (@payType = 4 OR @payType = 5 OR  
   @payType = 208 OR @payType = 209 OR 
   @payType = 111 OR @payType = 112 OR
   @payType = 303 OR @payType = 304 OR    
   @payType = 354 OR @payType = 355 OR
   @payType = 407 OR @payType = 408) 
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance - (@creditAmount - @debitAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

IF ((@payType = 208 OR @payType = 209 OR 
   @payType = 111 OR @payType = 112 OR
   @payType = 303 OR @payType = 304) AND @probAdjFlag = 1)   
   BEGIN
		UPDATE A2ZACCOUNT SET AccProvBalance = AccProvBalance + (@creditAmount - @debitAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
   END;
--  End of For Adjustment Transaction ---------


--- Encashment Transaction for Valid Account

IF @FuncOpt = 9 OR @FuncOpt = 10 
   BEGIN
		UPDATE A2ZACCOUNT SET AccStatus = 1,AccStatusDate = AccStatusDateP,AccStatusNote = '',AccProvBalance = AccPrevProvBalance                     					
    	WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
   END;



FETCH NEXT FROM wfTrnTable INTO @cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@TrnInterestAmt,@DueIntAmt,@trnDate,@trnType,@probAdjFlag,@FuncOpt; 

END;	       
CLOSE wfTrnTable; 
DEALLOCATE wfTrnTable;

--- End of Update Account Table base on Workfile ---

--------------------------------------------------------------------------

DELETE FROM A2ZTRANSACTION  WHERE VchNo = @VoucherNo AND TrnModule = @Module;


DELETE FROM WF_REVERSETRANSACTION WHERE DelUserId = @UserId AND TrnModule = @Module;
END

GO

