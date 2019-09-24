USE A2ZCSMCUS
GO

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


ALTER PROCEDURE  [dbo].[Sp_CSAddTransaction](@userID INT, @vchNo nvarchar(20), @ProcStat smallint,@NewAccFlag smallint,@NewAccType INT, @NewAccNo BIGINT,@Module int)

--ALTER PROCEDURE  [dbo].[Sp_CSAddTransaction]
AS
BEGIN


/*

EXECUTE Sp_CSAddTransaction 1,4444,0,0,0,0,1

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

DECLARE @LastaccNo BIGINT;


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
DECLARE @FuncOpt INT;

DECLARE @WriteFlag INT;

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

SET @ProvAdjFlag = 0;
SET @DueIntFlag = 0;
SET @WriteFlag = 0;
SET @LastaccNo = 0;

--- Update Account Table base on Workfile ---
--
--SET @userID = 1;
--SET @vchNo = 1;
--SET @ProcStat = 1;
--SET @NewAccFlag = 0;
--SET @NewAccType = 0; 
--SET @NewAccNo = 1111111;


IF @NewAccFlag = 1
   BEGIN
	  UPDATE WF_Transaction SET AccNo = @NewAccNo WHERE AccType = @NewAccType AND UserId = @userId;
   END


---============= For Liquidity Loan Account ===================
	DECLARE @LiquidityAccNo NVARCHAR(16);
	DECLARE @LiquidityAtyClass INT;
	DECLARE @LiquidityTrnAmt MONEY;
	DECLARE @AccDisbDate SMALLDATETIME;
	DECLARE @AccLoanSancAmt MONEY;
	DECLARE @AccDisbAmt MONEY;

	SET @LiquidityAccNo = ISNULL((SELECT AccNo FROM WF_Transaction WHERE TrnFlag = 0 AND UserId = @userId
					AND FuncOpt = 7 AND PayTypeCode = 401),0);

	SET @LiquidityAtyClass = ISNULL((SELECT AccAtyClass FROM A2ZACCOUNT WHERE AccNo = @LiquidityAccNo),0);

	SET @LiquidityTrnAmt = ISNULL((SELECT Debit FROM WF_Transaction WHERE TrnFlag = 0 AND UserId = @userId
					AND FuncOpt = 7 AND PayTypeCode = 401),0);

	SET @AccDisbDate = (SELECT AccDisbDate FROM A2ZACCOUNT WHERE AccNo = @LiquidityAccNo);

	SET @AccLoanSancAmt = (SELECT AccLoanSancAmt FROM A2ZACCOUNT WHERE AccNo = @LiquidityAccNo);

	SET @AccDisbAmt = (SELECT AccDisbAmt FROM A2ZACCOUNT WHERE AccNo = @LiquidityAccNo);
---============= For Liquidity Loan Account ===================


DECLARE wfTrnTable CURSOR FOR
SELECT TrnDate,CuType,CuNo,MemNo,AccType,AccNo,PayTypeCode,Credit,Debit,TrnInterestAmt,TrnDate,TrnTypeCode,TrnDueIntAmt,NewMem,NewAcc,MemName,NewMemType,NewAccPeriod,NewAccIntRate,NewAccContractIntFlag,NewOpenDate,NewMaturityDate,NewFixedAmt,NewFixedMthInt,NewBenefitDate,ProvAdjFlag,FuncOpt
FROM WF_Transaction
WHERE TrnFlag = 0 AND UserId = @userId;

			
OPEN wfTrnTable; 
FETCH NEXT FROM wfTrnTable INTO @trnDate,@cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@TrnInterestAmt,@trnDate,@trnType,@DueIntAmt,@MemFlag,@AccFlag,@MemName,@NewMemType,@NewAccPeriod,@NewAccIntRate,@NewAccContractIntFlag,@NewOpenDate,@NewMaturityDate,@NewFixedAmt,@NewFixedMthInt,@NewBenefitDate,@ProvAdj,@FuncOpt; 
WHILE @@FETCH_STATUS = 0 
BEGIN
      
      IF @MemFlag = 1
         BEGIN
            INSERT INTO A2ZMEMBER (CuType,CuNo,MemNo,MemName,MemOpenDate,MemType)
            VALUES (@cuType,@cuNo,@memNo,@MemName,@NewOpenDate,@NewMemType);

            INSERT INTO A2ZACCOUNT (AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccStatus)
            VALUES (99,0,@cuType,@cuNo,@memNo,@NewOpenDate,1);

         END

      IF @AccFlag = 1
         BEGIN
            INSERT INTO A2ZACCOUNT (AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccStatus,AccStatusDate,AccPeriod,AccIntRate,AccContractIntFlag,AccMatureDate,AccFixedAmt,AccFixedMthInt,AccBenefitDate)
            VALUES (@accType,@accNo,@cuType,@cuNo,@memNo,@NewOpenDate,1,@NewOpenDate,@NewAccPeriod,@NewAccIntRate,@NewAccContractIntFlag,@NewMaturityDate,@NewFixedAmt,@NewFixedMthInt,@NewBenefitDate);
         END

    	 
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



--  For Loan Disbursement Transaction ---------

IF @payType = 401
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance + (@creditAmount - @debitAmount),
					AccPrincipal = AccPrincipal + (@creditAmount - @debitAmount),
                    AccDisbAmt = AccDisbAmt - (@creditAmount - @debitAmount),
					AccPrevDisbDate = AccDisbDate, AccDisbDate = @trnDate					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

--  For Loan Interest Received Transaction ---------

IF @payType = 402
		BEGIN
			UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + @creditAmount)			                      
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		
            
            UPDATE A2ZLOANDEFAULTER SET PaidIntAmt = (PaidIntAmt + @creditAmount)                                                           				
			    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                      AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                      YEAR(TrnDate) = YEAR(@trnDate);

            SET @PayableIntAmt = (SELECT PayableIntAmt FROM A2ZLOANDEFAULTER WHERE 
                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                       YEAR(TrnDate) = YEAR(@trnDate));
             
            SET @PaidIntAmt = (SELECT PaidIntAmt FROM A2ZLOANDEFAULTER WHERE 
                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                       YEAR(TrnDate) = YEAR(@trnDate));
            
            SET @CurrDueIntAmt = (@PayableIntAmt - @PaidIntAmt);

            IF @CurrDueIntAmt < 0
               BEGIN
                  SET @CurrDueIntAmt = 0;
               END

            UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = @CurrDueIntAmt                                                                 				
		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                         YEAR(TrnDate) = YEAR(@trnDate);            

        END;



--  For Loan Amount Received Transaction ---------

IF @payType = 403 
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @creditAmount),
					AccPrincipal = (AccPrincipal + @creditAmount),
                    AccTotalDep = (AccTotalDep + @creditAmount)               
	
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
                              AccType = @accType AND AccNo = @accNo;   

            UPDATE A2ZLOANDEFAULTER SET PaidPrincAmt = (PaidPrincAmt + @creditAmount)                                                              				
		         WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                      AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                      YEAR(TrnDate) = YEAR(@trnDate);
                               
            SET @PayablePrincAmt = (SELECT PayablePrincAmt FROM A2ZLOANDEFAULTER WHERE 
                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                       YEAR(TrnDate) = YEAR(@trnDate));
             
            SET @PaidPrincAmt = (SELECT PaidPrincAmt FROM A2ZLOANDEFAULTER WHERE 
                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                       YEAR(TrnDate) = YEAR(@trnDate));
            
            SET @CurrDuePrincAmt = (@PayablePrincAmt - @PaidPrincAmt);

            IF @CurrDuePrincAmt < 0
               BEGIN
                  SET @CurrDuePrincAmt = 0;
               END

            UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = @CurrDuePrincAmt                                                                 				
		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                         YEAR(TrnDate) = YEAR(@trnDate);            

            SET @UptoDuePrincAmt = (SELECT UptoDuePrincAmt FROM A2ZLOANDEFAULTER WHERE 
                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                       YEAR(TrnDate) = YEAR(@trnDate));
        
            IF @UptoDuePrincAmt > 0
               BEGIN
                   SET @CurrDuePrincAmt = (SELECT CurrDuePrincAmt FROM A2ZLOANDEFAULTER WHERE 
                               CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                               AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                               YEAR(TrnDate) = YEAR(@trnDate));
            
                   SET @AccLoanInstlAmt = (SELECT AccLoanInstlAmt FROM A2ZACCOUNT WHERE 
                               CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
                                   AccType = @accType AND AccNo = @accNo);                          

            
                   SET @NoDueInstalment = (@CurrDuePrincAmt / ABS(@AccLoanInstlAmt));
            
                   UPDATE A2ZLOANDEFAULTER SET NoDueInstalment = @NoDueInstalment                                                                 				
		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                         YEAR(TrnDate) = YEAR(@trnDate);
               END

		END;

--  For Loan Amount Penalty Transaction ---------

IF @payType = 404
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;

            UPDATE A2ZLOANDEFAULTER SET PaidPenalAmt = (PaidPenalAmt + @creditAmount)
                                                                                                    				
		    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                 YEAR(TrnDate) = YEAR(@trnDate);


		END;


----  Loan Settlement --------------------------------------



IF @payType = 406  
		BEGIN

			UPDATE A2ZACCOUNT SET AccPrevRaLedger = AccBalance, AccStatusDateP = AccStatus, AccStatusP = AccStatus                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
            
            UPDATE A2ZACCOUNT SET AccBalance = 0, AccStatus = 99, AccStatusDate = @trnDate, AccStatusNote = 'Settlement'                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
---------------------Deposit Guarantor Release-------------------

            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccLienAmt = A2ZACCOUNT.AccLienAmt - 
			(SELECT A2ZACGUAR.AccAmount FROM A2ZACGUAR
            WHERE A2ZACGUAR.AccNo = @accNo)
			FROM A2ZACCOUNT,A2ZACGUAR
			WHERE A2ZACCOUNT.AccNo = @accNo;



            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccStatusP = A2ZACCOUNT.AccStatus,A2ZACCOUNT.AccStatusDateP = A2ZACCOUNT.AccStatusDate,
                                  A2ZACCOUNT.AccStatus = 1,A2ZACCOUNT.AccStatusDate = NULL
			FROM A2ZACCOUNT,A2ZACGUAR
			WHERE A2ZACCOUNT.AccNo = A2ZACGUAR.AccNo AND A2ZACCOUNT.AccLienAmt = 0;      


--------------------Share Guarantor Release------------------

            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccLienAmt = A2ZACCOUNT.AccLienAmt - 
			(SELECT A2ZSHGUAR.AccAmount FROM A2ZSHGUAR
            WHERE A2ZSHGUAR.AccNo=@accNo)
			FROM A2ZACCOUNT,A2ZSHGUAR
			WHERE A2ZACCOUNT.AccNo = @accNo;

            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccStatusP = A2ZACCOUNT.AccStatus,A2ZACCOUNT.AccStatusDateP = A2ZACCOUNT.AccStatusDate,
                                  A2ZACCOUNT.AccStatus = 1,A2ZACCOUNT.AccStatusDate = NULL
			FROM A2ZACCOUNT,A2ZSHGUAR
			WHERE A2ZACCOUNT.AccNo = A2ZSHGUAR.AccNo AND A2ZACCOUNT.AccLienAmt = 0;      
    
       END;


------- End of Loan Settlement ----------------------




--  For OD Loan Withdrawal Transaction ---------

IF @payType = 351
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance + (@creditAmount - @debitAmount),
					AccPrincipal = AccPrincipal + (@creditAmount - @debitAmount)
                    ----AccHoldIntAmt = (AccHoldIntAmt + @DueIntAmt),
                    ----AccPrevODIntDate = AccODIntDate, AccODIntDate = @trnDate				
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
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
        
        UPDATE A2ZPENSIONDEFAULTER SET PaidDepositAmt = (PaidDepositAmt + @creditAmount)                                                              				
		         WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                      AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                      YEAR(TrnDate) = YEAR(@trnDate);
                               
            SET @PayableDepositAmt = (SELECT PayableDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                       YEAR(TrnDate) = YEAR(@trnDate));
             
            SET @PaidDepositAmt = (SELECT PaidDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                       YEAR(TrnDate) = YEAR(@trnDate));
            
            SET @CurrDueDepositAmt = (@PayableDepositAmt - @PaidDepositAmt);

--            IF @CurrDueDepositAmt < 0
--               BEGIN
--                  SET @CurrDueDepositAmt = 0;
--               END

            UPDATE A2ZPENSIONDEFAULTER SET CurrDueDepositAmt = @CurrDueDepositAmt                                                                 				
		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                         YEAR(TrnDate) = YEAR(@trnDate);            

            SET @UptoDueDepositAmt = (SELECT UptoDueDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
                       CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                       AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                       YEAR(TrnDate) = YEAR(@trnDate));
        
            IF @UptoDueDepositAmt > 0
               BEGIN
                   SET @CurrDueDepositAmt = (SELECT CurrDueDepositAmt FROM A2ZPENSIONDEFAULTER WHERE 
                               CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                               AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                               YEAR(TrnDate) = YEAR(@trnDate));
            
                   SET @AccMonthlyDeposit = (SELECT AccMonthlyDeposit FROM A2ZACCOUNT WHERE 
                               CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
                                   AccType = @accType AND AccNo = @accNo);                          

            
                   SET @NoDueDeposit = (@CurrDueDepositAmt / ABS(@AccMonthlyDeposit));
            
                   UPDATE A2ZPENSIONDEFAULTER SET NoDueDeposit = @NoDueDeposit                                                                 				
		           WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                         AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                         YEAR(TrnDate) = YEAR(@trnDate);
               END        

	END;

--  End Of Pension Account Transaction ---------

IF @payType = 302
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;

            UPDATE A2ZPENSIONDEFAULTER SET PaidPenalAmt = (PaidPenalAmt + @creditAmount)
                                                                                                    				
		    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                 YEAR(TrnDate) = YEAR(@trnDate);        

		END;

--IF @payType = 306  
--		BEGIN
----            SET @ProvAdjFlag = 1;
----			UPDATE A2ZACCOUNT SET AccPrevRaLedger = AccBalance, AccStatusDateP = AccStatus, AccStatusP = AccStatus,AccPrevProvBalance = AccProvBalance                     					
----			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
--            
----            UPDATE A2ZACCOUNT SET AccBalance = 0, AccStatus = 99, AccStatusDate = @trnDate, AccStatusNote = 'Encashment',AccProvBalance = 0                    					
----			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
--		END;

-- For Time Deposite Account Transaction --------- 

IF @payType = 101 OR @payType = 201
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance + (@creditAmount - @debitAmount),
					AccPrincipal = AccPrincipal + (@creditAmount - @debitAmount),
                    AccOrgAmt = AccOrgAmt + (@creditAmount - @debitAmount)
					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

------ End Of Time Deposite Account Transaction -------

-----  Time Deposit Interest Withdrawn ------------------
IF @payType = 105
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance - @debitAmount),
                    AccRenwlAmt = (AccRenwlAmt - @debitAmount)
                   
                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
-----  End of Time Deposit Interest Withdrawn ------------------

-----  Time Deposit Encashment Amount ------------------
--IF @payType = 107
--   BEGIN
--       SET @ProvAdjFlag = 1;
--   END

--IF @payType = 110  
--		BEGIN
----            SET @ProvAdjFlag = 1;
----			UPDATE A2ZACCOUNT SET AccPrevRaLedger = AccBalance, AccStatusDateP = AccStatus, AccStatusP = AccStatus,AccPrevProvBalance = AccProvBalance                     					
----			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
----            
----            UPDATE A2ZACCOUNT SET AccBalance = 0, AccStatus = 99, AccStatusDate = @trnDate, AccStatusNote = 'Encashment',AccProvBalance = 0                    					
----			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
--		END;
-----  End of Time Deposit Encashment Amount ------------------


-----  MS+ Deposit Benefit Withdrawn ------------------
IF @payType = 203
		BEGIN
			UPDATE A2ZACCOUNT SET AccProvBalance = (AccProvBalance + @TrnInterestAmt),
                                  AccToTIntWdrawn = (AccToTIntWdrawn - @TrnInterestAmt)                 					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

-----  End of MS+ Deposit Benefit Withdrawn ------------------

-----  MS+ Deposit Benefit Adj Cr.& Dr. ------------------
IF @payType = 210 or @payType = 211
		BEGIN
			UPDATE A2ZACCOUNT SET AccProvBalance = (AccProvBalance + @TrnInterestAmt),
                                  AccAdjProvBalance = 0                 					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

-----  End of MS+ Deposit Benefit Adj Cr. & Dr.------------------

-----  MS+ Deposit Benefit Encashment Amount ------------------
--IF @payType = 207  
--		BEGIN
----            SET @ProvAdjFlag = 1;
----			UPDATE A2ZACCOUNT SET AccPrevRaLedger = AccBalance,AccPrevProvBalance = AccProvBalance, AccStatusDateP = AccStatus, AccStatusP = AccStatus                    					
----			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
----            
----            UPDATE A2ZACCOUNT SET AccBalance = 0, AccProvBalance = 0, AccStatus = 99, AccStatusDate = @trnDate, AccStatusNote = 'Encashment'                    					
----			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
--		END;
-----  End of MS+ Deposit Benefit Encashment Amount ------------------


--  Adjustment Transaction --------

IF (@payType = 4 OR @payType = 5 OR  
   @payType = 208 OR @payType = 209 OR 
   @payType = 111 OR @payType = 112 OR
   @payType = 303 OR @payType = 304 OR    
   @payType = 354 OR @payType = 355 OR
   @payType = 407 OR @payType = 408) 
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance + (@creditAmount - @debitAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

IF ((@payType = 208 OR @payType = 209 OR 
   @payType = 111 OR @payType = 112 OR
   @payType = 303 OR @payType = 304) AND @ProvAdj = 1)   
   BEGIN
		UPDATE A2ZACCOUNT SET AccProvBalance = AccProvBalance - (@creditAmount - @debitAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
   END;
--  End of For Adjustment Transaction ---------


--  Close Account FOR Encashment Transaction ---------

IF (@FuncOpt = 9 OR @FuncOpt = 10) AND @accNo <> @LastaccNo
   BEGIN
        SET @WriteFlag = 1;
        SET @ProvAdjFlag = 1;
		SET @LastaccNo = @accNo; 

        UPDATE A2ZACCOUNT SET AccPrevRaLedger = AccBalance, AccStatusDateP = AccStatus, AccStatusP = AccStatus,AccPrevProvBalance = AccProvBalance                     					
    	WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;


        UPDATE A2ZACCOUNT SET AccBalance = 0, AccStatus = 99, AccStatusDate = @trnDate, AccStatusNote = 'Encashment',AccProvBalance = 0                    					
		WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
   END



FETCH NEXT FROM wfTrnTable INTO @trnDate,@cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@TrnInterestAmt,@trnDate,@trnType,@DueIntAmt,@MemFlag,@AccFlag,@MemName,@NewMemType,@NewAccPeriod,@NewAccIntRate,@NewAccContractIntFlag,@NewOpenDate,@NewMaturityDate,@NewFixedAmt,@NewFixedMthInt,@NewBenefitDate,@ProvAdj,@FuncOpt; 


END;	       
CLOSE wfTrnTable; 
DEALLOCATE wfTrnTable;

--- End of Update Account Table base on Workfile ---



IF @ProvAdjFlag = 1
   BEGIN
   
        -------- NORMAL ADJ PROVISION CR. ----------------

       SET @trnCode1= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @accType);    
       SET @FuncOpt1= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
       SET @PayType1 = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
       SET @TrnType1= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
       SET @TrnDrCr1= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
       SET @ShowInterest1= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
       SET @TrnGLAccNoDr1= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
       SET @TrnGLAccNoCr1= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
       SET @TrnDesc1 = 'Provision Adj.Cr.';

       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,ValueDate,UserID,AccTypeMode)

       SELECT TrnDate,VchNo,@vchNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,@FuncOpt1,@PayType1,@TrnType1,1,0,
       CalProvAdjCr,(@TrnDesc1 + ' '+ CAST(AccNo AS nvarchar(16))),@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoCr1,CalProvAdjCr,0,CalProvAdjCr,0,FromCashCode,@Module,0,ValueDate,UserID,AccTypeMode 
       FROM WF_Transaction WHERE UserId = @userId AND CalProvAdjCr <> 0;

       --------CONTRA ------

       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,ValueDate,UserID)

       SELECT TrnDate,VchNo,@vchNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,@FuncOpt1,@PayType1,@TrnType1,0,CalProvAdjCr,
       0,(@TrnDesc1 + ' '+ CAST(AccNo AS nvarchar(16))),@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoDr1,CalProvAdjCr,CalProvAdjCr,0,1,FromCashCode,@Module,0,ValueDate,UserID 
       FROM WF_Transaction WHERE UserId = @userId AND CalProvAdjCr <> 0;

       ---------  END OF ADJ PROVISION CR. ------------


       -------- NORMAL ADJ PROVISION DR. ----------------

       SET @trnCode1= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @accType);
       SET @FuncOpt1= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
       SET @PayType1 = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
       SET @TrnType1= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
       SET @TrnDrCr1= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
       SET @ShowInterest1= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
       SET @TrnGLAccNoDr1= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
       SET @TrnGLAccNoCr1= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
       SET @TrnDesc1 = 'Provision Adj.Dr.';

       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,ValueDate,UserID,AccTypeMode)

       SELECT TrnDate,VchNo,@vchNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,@FuncOpt1,@PayType1,@TrnType1,0,CalProvAdjDr, 
       0,(@TrnDesc1 + ' '+ CAST(AccNo AS nvarchar(16))),@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoDr1,(0-CalProvAdjDr),CalProvAdjDr,0,0,FromCashCode,@Module,0,ValueDate,UserID,AccTypeMode 
       FROM WF_Transaction WHERE UserId = @userId AND CalProvAdjDr <> 0;

       -----------CONTRA -----

       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,ValueDate,UserID)

       SELECT TrnDate,VchNo,@vchNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,@FuncOpt1,@PayType1,@TrnType1,1,0,
       CalProvAdjDr,(@TrnDesc1 + ' '+ CAST(AccNo AS nvarchar(16))),@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoCr1,CalProvAdjDr,0,CalProvAdjDr,1,FromCashCode,@Module,0,ValueDate,UserID 
       FROM WF_Transaction WHERE UserId = @userId AND CalProvAdjDr <> 0;

       ---------  END OF ADJ PROVISION DR. ------------   

   END


-- Move Workfile Transaction to Original Transaction Table ----

INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,
FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCredit,TrnDebit,ShowInterest,TrnInterestAmt,
TrnPenalAmt,TrnDueIntAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,
TrnProcStat,FromCashCode,TrnModule,ValueDate,TrnPayment,UserID,CuName,MemName,NewCU,NewMem,NewAcc,NewMemType,
NewAccPeriod,NewAccIntRate,NewAccContractIntFlag,NewOpenDate,NewMaturityDate,NewFixedAmt,NewFixedMthInt
,NewBenefitDate,ProvAdjFlag,AccTypeMode,TrnOrgAmt)

SELECT TrnDate,VchNo,@vchNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,
FuncOptDes,PayTypeCode,TrnTypeCode,TrnMode,PayTypeDes,TrnVchType,Credit,Debit,ShowInt,TrnInterestAmt,
TrnPenalAmt,TrnDueIntAmt,TrnCSGL,GLAccNoDR,GLAccNoCR,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,
@ProcStat,FromCashCode,TrnModule,ValueDate,TrnPayment,UserID,CuName,MemName,NewCU,NewMem,NewAcc,NewMemType,
NewAccPeriod,NewAccIntRate,NewAccContractIntFlag,NewOpenDate,NewMaturityDate,NewFixedAmt,NewFixedMthInt,
NewBenefitDate,@ProvAdj,AccTypeMode,TrnOrgAmt
FROM WF_Transaction WHERE UserId = @userId;
-- End of Move Workfile Transaction to Original Transaction Table ----

---============= For Liquidity Loan Account ===================
	IF @LiquidityAtyClass = 8 AND @AccDisbAmt >= @AccLoanSancAmt
		BEGIN
			UPDATE A2ZACCOUNT SET AccCertNo = @vchNo WHERE AccNo = @LiquidityAccNo;

			EXECUTE Sp_LoanDisburseWithReschedule @LiquidityAccNo,@userId,@LiquidityTrnAmt,0;
		END	
---============= End For Liquidity Loan Account ===================

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








