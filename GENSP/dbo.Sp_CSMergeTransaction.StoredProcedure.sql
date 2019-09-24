USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSMergeTransaction]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE  [dbo].[Sp_CSMergeTransaction](@trnDate VARCHAR(10))

--ALTER PROCEDURE  [dbo].[Sp_CSMergeTransaction]
AS

BEGIN

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
DECLARE @transactionDate SMALLDATETIME;
DECLARE @valueDate SMALLDATETIME;
DECLARE @trnType INT;
DECLARE @FromCashCode INT;
DECLARE @LoanApplicationNo INT;
DECLARE @trnCode int;
DECLARE @trnCSGL smallint;
DECLARE @trnDrCr smallint;
DECLARE @GLAmount MONEY;

DECLARE @ProvAdjFlag INT;

DECLARE @NoVchCredit INT;
DECLARE @TotalCreditAmt MONEY;

DECLARE @NoVchDebit INT;
DECLARE @TotalDebitAmt MONEY;


DECLARE @MemFlag smallint;
DECLARE @AccFlag smallint;
DECLARE @CuFlag smallint;

DECLARE @MemName nvarchar(50);
DECLARE @NewMemType INT;
DECLARE @NewAccPeriod INT;
DECLARE @NewAccIntRate INT;
DECLARE @NewAccContractIntFlag smallint; 
DECLARE @NewOpenDate SMALLDATETIME;
DECLARE @NewMaturityDate SMALLDATETIME;

DECLARE @trnCode1 int;
DECLARE @FuncOpt1 smallint;
DECLARE @PayType1 smallint;
DECLARE @TrnDesc1 nvarchar(50);
DECLARE @CuName nvarchar(50);
DECLARE @TrnType1 tinyint;
DECLARE @TrnDrCr1 tinyint;
DECLARE @ShowInterest1 tinyint;
DECLARE @TrnGLAccNoDr1 int;
DECLARE @TrnGLAccNoCr1 int;



SET @ProvAdjFlag = 0;

--- Update Account Table base on Workfile ---
--
--SET @userID = 1;
--SET @vchNo = 1;
--SET @ProcStat = 1;
--SET @NewAccFlag = 0;
--SET @NewAccType = 0; 
--SET @NewAccNo = 1111111;



DECLARE wfTrnTable CURSOR FOR
SELECT TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnCSGL,TrnDrCr,GLAmount,PayType,TrnCredit,TrnDebit,TrnInterestAmt,TrnDate,TrnType,TrnDueIntAmt,NewMem,NewAcc,MemName,NewMemType,NewAccPeriod,NewAccIntRate,NewAccContractIntFlag,NewOpenDate,NewMaturityDate
FROM IA2ZTRANSACTION
WHERE TrnFlag = 0;


--DECLARE @trnDrCr smallint;
--DECLARE @GLAmount MONEY;


			
OPEN wfTrnTable; 
FETCH NEXT FROM wfTrnTable INTO @transactionDate,@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnCSGL,@trnDrCr,@GLAmount,@payType,@creditAmount,@debitAmount,@TrnInterestAmt,@transactionDate,@trnType,@DueIntAmt,@MemFlag,@AccFlag,@MemName,@NewMemType,@NewAccPeriod,@NewAccIntRate,@NewAccContractIntFlag,@NewOpenDate,@NewMaturityDate; 
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
            INSERT INTO A2ZACCOUNT (AccType,AccNo,CuType,CuNo,MemNo,AccOpenDate,AccStatus,AccStatusDate,AccPeriod,AccIntRate,AccContractIntFlag,AccMatureDate)
            VALUES (@accType,@accNo,@cuType,@cuNo,@memNo,@NewOpenDate,1,@NewOpenDate,@NewAccPeriod,@NewAccIntRate,@NewAccContractIntFlag,@NewMaturityDate);
         END

    	 
--  For All Account Transaction --------
		BEGIN
			UPDATE A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = @trnType,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = (@creditAmount + @debitAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

--  For Share Account Transaction --------

IF @payType = 1 OR @payType = 2
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

--  For OD Loan Withdrawal Transaction ---------

IF @payType = 351
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance + (@creditAmount - @debitAmount),
					AccPrincipal = AccPrincipal + (@creditAmount - @debitAmount)				
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

--  For Loan Disbursement Transaction ---------

IF @payType = 401
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance + (@creditAmount - @debitAmount),
					AccPrincipal = AccPrincipal + (@creditAmount - @debitAmount),
                    AccDisbAmt = AccDisbAmt - (@creditAmount - @debitAmount),
					AccDisbDate = @trnDate
					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

--  For Loan Amount Received Transaction ---------

IF @payType = 403 
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @creditAmount),
					AccPrincipal = (AccPrincipal + @creditAmount),
                    AccTotalDep = (AccTotalDep + @creditAmount)               
	
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
                              AccType = @accType AND AccNo = @accNo;
            
            
            UPDATE A2ZLOANDEFAULTER SET PaidPrincAmt = (PaidPrincAmt + @creditAmount),
                                        CurrDuePrincAmt = (CurrDuePrincAmt - @creditAmount)                                                                 				
		    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                 YEAR(TrnDate) = YEAR(@trnDate);
             

		END;


--  For OD Loan Amount Received Transaction ---------

IF @payType = 353 
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @creditAmount),
					AccPrincipal = (AccPrincipal + @creditAmount),
                    AccTotalDep = (AccTotalDep + @creditAmount)
                    				
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

--  For Loan Interest Received Transaction ---------

IF @payType = 402
		BEGIN
			UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + @creditAmount)			                      
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		
            
            UPDATE A2ZLOANDEFAULTER SET PaidIntAmt = (PaidIntAmt + @creditAmount),
                                        CurrDueIntAmt = (CurrDueIntAmt - @creditAmount)                                                                    				
			 WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                 YEAR(TrnDate) = YEAR(@trnDate);


        END;

--  For OD Loan Interest Received Transaction ---------

IF @payType = 352
		BEGIN
			UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + @creditAmount),
                   AccPrevODIntDate = AccODIntDate, AccODIntDate = @trnDate,
                   AccPrevDueIntAmt = AccDueIntAmt,	
                   AccDueIntAmt = @DueIntAmt	
				
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

IF @payType = 404
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;

            UPDATE A2ZLOANDEFAULTER SET PaidPenalAmt = (PaidPenalAmt + @creditAmount)
                                                                                                    				
		    WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@memNo AND 
                 AccType=@accType AND AccNo=@accNo AND MONTH(TrnDate) = MONTH(@trnDate) AND 
                 YEAR(TrnDate) = YEAR(@trnDate);


		END;

--  For Pension Account Transaction ---------

IF @payType = 301
		BEGIN
         	UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @creditAmount),
            AccTotalDep = (AccTotalDep + @creditAmount)
            
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

--  End Of Pension Account Transaction ---------

IF @payType = 302
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
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

-----  Time Deposit Interest Withdrawn ------------------
IF @payType = 105
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance - @debitAmount),
                    AccRenwlAmt = (AccRenwlAmt - @debitAmount)
                   
                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
-----  End of Time Deposit Interest Withdrawn ------------------

-----  Time Deposit Encashment Amount ------------------
IF @payType = 107
   BEGIN
       SET @ProvAdjFlag = 1;
   END

IF @payType = 110  
		BEGIN
			UPDATE A2ZACCOUNT SET AccPrevRaLedger = AccBalance, AccStatusDateP = AccStatus, AccStatusP = AccStatus,AccPrevProvBalance = AccProvBalance                     					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
            
            UPDATE A2ZACCOUNT SET AccBalance = 0, AccStatus = 99, AccStatusDate = @trnDate, AccStatusNote = 'Encashment',AccProvBalance = 0                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
-----  End of Time Deposit Encashment Amount ------------------


----  Loan Settlement --------------------------------------

IF @payType = 406  
		BEGIN

			UPDATE A2ZACCOUNT SET AccPrevRaLedger = AccBalance, AccStatusDateP = AccStatus, AccStatusP = AccStatus                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
            
            UPDATE A2ZACCOUNT SET AccBalance = 0, AccStatus = 99, AccStatusDate = @trnDate, AccStatusNote = 'Settlement'                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
------------------------------------------

            SET @LoanApplicationNo = (SELECT AccLoanAppNo FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo);

            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccLienAmt = A2ZACCOUNT.AccLienAmt - 
			(SELECT A2ZACGUAR.AccAmount FROM A2ZACGUAR
            WHERE A2ZACCOUNT.AccType = A2ZACGUAR.AccType AND A2ZACCOUNT.AccNo = A2ZACGUAR.AccNo AND 
            A2ZACCOUNT.CuType = A2ZACGUAR.CuType AND A2ZACCOUNT.CuNo = A2ZACGUAR.CuNo AND 
            A2ZACCOUNT.MemNo = A2ZACGUAR.MemNo AND A2ZACGUAR.LoanApplicationNo = @LoanApplicationNo)
			FROM A2ZACCOUNT,A2ZACGUAR
			WHERE A2ZACCOUNT.AccType = A2ZACGUAR.AccType AND A2ZACCOUNT.AccNo = A2ZACGUAR.AccNo AND 
            A2ZACCOUNT.CuType = A2ZACGUAR.CuType AND A2ZACCOUNT.CuNo = A2ZACGUAR.CuNo AND 
            A2ZACCOUNT.MemNo = A2ZACGUAR.MemNo AND A2ZACGUAR.LoanApplicationNo = @LoanApplicationNo;            
		
            UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccStatusP = A2ZACCOUNT.AccStatus,A2ZACCOUNT.AccStatusDateP = A2ZACCOUNT.AccStatusDate,
                                  A2ZACCOUNT.AccStatus = 1,A2ZACCOUNT.AccStatusDate = NULL
			FROM A2ZACCOUNT,A2ZACGUAR
			WHERE A2ZACCOUNT.AccType = A2ZACGUAR.AccType AND A2ZACCOUNT.AccNo = A2ZACGUAR.AccNo AND 
            A2ZACCOUNT.CuType = A2ZACGUAR.CuType AND A2ZACCOUNT.CuNo = A2ZACGUAR.CuNo AND 
            A2ZACCOUNT.MemNo = A2ZACGUAR.MemNo AND A2ZACGUAR.LoanApplicationNo = @LoanApplicationNo AND
			A2ZACCOUNT.AccLienAmt = 0;            
       END;


------- End of Loan Settlement ----------------------


-----  Time Deposit Benefit Withdrawn ------------------
IF @payType = 203
		BEGIN
			UPDATE A2ZACCOUNT SET AccProvBalance = (AccProvBalance + @TrnInterestAmt),
                                  AccToTIntWdrawn = (AccToTIntWdrawn - @TrnInterestAmt)                 					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;


-----  End of Time Deposit Benefit Withdrawn ------------------

-----  Time Deposit Benefit Encashment Amount ------------------
IF @payType = 207  
		BEGIN
			UPDATE A2ZACCOUNT SET AccPrevRaLedger = AccBalance,AccPrevProvBalance = AccProvBalance, AccStatusDateP = AccStatus, AccStatusP = AccStatus                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
            
            UPDATE A2ZACCOUNT SET AccBalance = 0, AccProvBalance = 0, AccStatus = 99, AccStatusDate = @trnDate, AccStatusNote = 'Encashment'                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
-----  End of Time Deposit Benefit Encashment Amount ------------------


--  Adjustment Transaction --------
IF @payType = 4 OR @payType = 5 OR @payType = 111 OR 
   @payType = 112 OR @payType = 208 OR @payType = 209 OR 
   @payType = 303 OR @payType = 304 OR @payType = 354 OR 
   @payType = 355 OR @payType = 407 OR @payType = 408
		BEGIN
			UPDATE A2ZACCOUNT SET AccBalance = AccBalance + (@creditAmount - @debitAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
--  End of For Adjustment Transaction ---------



FETCH NEXT FROM wfTrnTable INTO @transactionDate,@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnCSGL,@trnDrCr,@GLAmount,@payType,@creditAmount,@debitAmount,@TrnInterestAmt,@transactionDate,@trnType,@DueIntAmt,@MemFlag,@AccFlag,@MemName,@NewMemType,@NewAccPeriod,@NewAccIntRate,@NewAccContractIntFlag,@NewOpenDate,@NewMaturityDate; 

END;	       
CLOSE wfTrnTable; 
DEALLOCATE wfTrnTable;

--- End of Update Account Table base on Workfile ---

--IF @ProvAdjFlag = 1
--   BEGIN
--   
--        -------- NORMAL ADJ PROVISION CR. ----------------
--
--       SET @trnCode1= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @accType);    
--       SET @FuncOpt1= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
--       SET @PayType1 = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
--       SET @TrnType1= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
--       SET @TrnDrCr1= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
--       SET @ShowInterest1= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
--       SET @TrnGLAccNoDr1= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
--       SET @TrnGLAccNoCr1= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=51);
--       SET @TrnDesc1 = 'Provision Adj.Cr.';
--
--       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnModule,TrnSysUser,ValueDate,UserID)
--
--       SELECT TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,FuncOpt,@PayType1,@TrnType1,1,0,
--       CalProvAdjCr,@TrnDesc1,@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoCr1,CalProvAdjCr,0,CalProvAdjCr,0,1,1,ValueDate,UserID 
--       FROM IA2ZTRANSACTION WHERE CalProvAdjCr <> 0;
--
--       --------CONTRA ------
--
--       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnModule,TrnSysUser,ValueDate,UserID)
--
--       SELECT TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,FuncOpt,@PayType1,@TrnType1,0,CalProvAdjCr,
--       0,@TrnDesc1,@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoDr1,CalProvAdjCr,CalProvAdjCr,0,1,1,1,ValueDate,UserID 
--       FROM IA2ZTRANSACTION WHERE  CalProvAdjCr <> 0;
--
--       ---------  END OF ADJ PROVISION CR. ------------
--
--
--       -------- NORMAL ADJ PROVISION DR. ----------------
--
--       SET @trnCode1= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @accType);
--       SET @FuncOpt1= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
--       SET @PayType1 = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
--       SET @TrnType1= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
--       SET @TrnDrCr1= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
--       SET @ShowInterest1= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
--       SET @TrnGLAccNoDr1= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
--       SET @TrnGLAccNoCr1= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @accType AND FuncOpt=52);
--       SET @TrnDesc1 = 'Provision Adj.Dr.';
--
--
--       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnModule,TrnSysUser,ValueDate,UserID)
--
--       SELECT TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,FuncOpt,@PayType1,@TrnType1,0,CalProvAdjDr, 
--       0,@TrnDesc1,@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoDr1,CalProvAdjDr,CalProvAdjDr,0,0,1,1,ValueDate,UserID 
--       FROM IA2ZTRANSACTION WHERE  CalProvAdjDr <> 0;
--
--       -----------CONTRA -----
--
--       INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
--       TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnModule,TrnSysUser,ValueDate,UserID)
--
--       SELECT TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,@TrnCode1,FuncOpt,@PayType1,@TrnType1,1,0,
--       CalProvAdjDr,@TrnDesc1,@ShowInterest1,0,@TrnGLAccNoDr1,@TrnGLAccNoCr1,@TrnGLAccNoCr1,CalProvAdjDr,0,CalProvAdjDr,1,1,1,ValueDate,UserID 
--       FROM IA2ZTRANSACTION WHERE  CalProvAdjDr <> 0;
--
--       ---------  END OF ADJ PROVISION DR. ------------   

--   END

SET @valueDate = 0;

IF @transactionDate != @trnDate
   BEGIN
     SET @valueDate = @transactionDate;
   END


-- Move Workfile Transaction to Original Transaction Table ----

INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc, 
                      TrnVchType, TrnChqPrx, TrnChqNo, ShowInterest, TrnInterestAmt, TrnPenalAmt, TrnChargeAmT, TrnDueIntAmt, TrnODAmount, BranchNo, TrnCSGL, TrnGLAccNoDr, 
                      TrnGLAccNoCr, TrnGLFlag, GLAccNo, GLAmount, GLDebitAmt, GLCreditAmt, TrnFlag, TrnStatus, FromCashCode, TrnProcStat, TrnSysUser, TrnModule, ValueDate, 
                      GLSlmCode, TrnPayment, UserIP, UserID, VerifyUserID, CreateDate, CuName, MemName, NewCU, NewMem, NewAcc, NewMemType, NewAccPeriod, NewAccIntRate, 
                      NewAccContractIntFlag, NewOpenDate, NewMaturityDate)

SELECT @TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc, 
                      TrnVchType, TrnChqPrx, TrnChqNo, ShowInterest, TrnInterestAmt, TrnPenalAmt, TrnChargeAmT, TrnDueIntAmt, TrnODAmount, BranchNo, TrnCSGL, TrnGLAccNoDr, 
                      TrnGLAccNoCr, TrnGLFlag, GLAccNo, GLAmount, GLDebitAmt, GLCreditAmt, TrnFlag, TrnStatus, FromCashCode, TrnProcStat, TrnSysUser, TrnModule, @valueDate, 
                      GLSlmCode, TrnPayment, UserIP, UserID, VerifyUserID, CreateDate, CuName, MemName, NewCU, NewMem, NewAcc, NewMemType, NewAccPeriod, NewAccIntRate, 
                      NewAccContractIntFlag, NewOpenDate, NewMaturityDate
FROM   dbo.IA2ZTRANSACTION

-- End of Move Workfile Transaction to Original Transaction Table ----

--DELETE FROM WF_Transaction WHERE UserId = @userId;

END;
































































































GO
