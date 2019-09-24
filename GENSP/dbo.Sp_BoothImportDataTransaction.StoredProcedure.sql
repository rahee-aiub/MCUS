USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_BoothImportDataTransaction]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE  [dbo].[Sp_BoothImportDataTransaction](@userID INT)

--ALTER PROCEDURE  [dbo].[Sp_CSAddTransaction]
AS

BEGIN

--DECLARE @userID INT;
--DECLARE @vchNo nvarchar(20);
DECLARE @ProcStat smallint;
DECLARE @cuType INT;
DECLARE @cuNo INT;
DECLARE @memNo INT;
DECLARE @accType INT;
DECLARE @accNo INT;
DECLARE @payType INT;
DECLARE @creditAmount MONEY;
DECLARE @debitAmount MONEY;
DECLARE @trnDate SMALLDATETIME;
DECLARE @trnType INT;
DECLARE @FromCashCode INT;
DECLARE @LoanApplicationNo INT;

--- Update Account Table base on Workfile ---

--SET @userID = 3;
--SET @vchNo = 1;
SET @ProcStat = 0;


DECLARE wfTrnTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,PayTypeCode,Credit,Debit,TrnDate,TrnTypeCode
FROM WF_Transaction
WHERE TrnFlag = 0 AND UserId = @userId;

			
OPEN wfTrnTable; 
FETCH NEXT FROM wfTrnTable INTO @cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@trnDate,@trnType; 
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

IF @payType = 403 OR @payType = 353 
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
		END;

IF @payType = 352
		BEGIN
			UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + @creditAmount),AccPrevODIntDate = AccODIntDate, AccODIntDate = @trnDate
				
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;

IF @payType = 404
		BEGIN
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + @creditAmount)
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
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
					AccPrincipal = (AccPrincipal - @debitAmount)
                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
		END;
-----  End of Time Deposit Interest Withdrawn ------------------

-----  Time Deposit Encashment Amount ------------------
IF @payType = 110  
		BEGIN
			UPDATE A2ZACCOUNT SET AccPrevRaLedger = AccBalance, AccStatusDateP = AccStatus, AccStatusP = AccStatus                    					
			WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo;
            
            UPDATE A2ZACCOUNT SET AccBalance = 0, AccStatus = 99, AccStatusDate = @trnDate, AccStatusNote = 'Encashment'                    					
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
			UPDATE A2ZACCOUNT SET AccProvBalance = (AccProvBalance - @debitAmount)
                    					
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



FETCH NEXT FROM wfTrnTable INTO @cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@trnDate,@trnType; 

END;	       
CLOSE wfTrnTable; 
DEALLOCATE wfTrnTable;

--- End of Update Account Table base on Workfile ---

-- Move Workfile Transaction to Original Transaction Table ----
INSERT INTO A2ZTRANSACTION(TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,
TrnDrCr,TrnDesc,TrnCredit,TrnDebit,ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,UserID)
SELECT TrnDate,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,FuncOptDes,PayTypeCode,TrnTypeCode,
TrnMode,TrnTypeDes,Credit,Debit,ShowInt,TrnInterestAmt,TrnCSGL,GLAccNoDR,GLAccNoCR,GLAccNo,GLAmount,
GLDebitAmt,GLCreditAmt,TrnFlag,@ProcStat,FromCashCode,UserID 
FROM WF_Transaction 
WHERE UserId = @userId;
-- End of Move Workfile Transaction to Original Transaction Table ----

DELETE FROM WF_Transaction WHERE UserId = @userId;

END;
























































GO
