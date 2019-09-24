USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSAddBalanceTransfer]    Script Date: 07/23/2018 12:08:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE  [dbo].[Sp_CSAddBalanceTransfer]
 @userID int
,@CtrlVoucherNo varchar(20)
,@CtrlProcStat smallint
,@txtTranDate smalldatetime
,@txtVchNo varchar(20)
,@lblCuType tinyint
,@lblCuNo int
,@txtMemNo int
,@txtAccType int
,@txtAccNo Bigint

,@lblTrnferCuType tinyint
,@lblTrnferCuNo int
,@txtTrnMemNo int
,@txtTrnAccType int
,@txtTrnAccNo Bigint
,@txtTrnAmount money
,@txtDescription varchar(50)    
,@CashCode int     
,@lblpaytype int    
,@lbltrnpaytype int    

,@lblTrnAtyClass int    
,@txtIntAmount money
,@txtPrincAmount money         


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
DECLARE @TransactionAmt money;
DECLARE @IntAmount money;

DECLARE @Type tinyint;


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

DECLARE @DueIntFlag INT;

DECLARE @CalInterest MONEY;
DECLARE @CalDate VARCHAR(10)

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
	 WHERE CuType = @lblcuType AND CuNo = @lblcuNo AND MemNo = @txtMemNo AND AccType = @txtAccType AND AccNo = @txtAccNo;
END		


SET @FuncOptDesc = 'A/c Balance Transfer';

SET @trnAmt = @txtTrnAmount;

SET @Type = LEFT(@TrnGLAccNoDr,1);
IF @Type = 2 OR @Type = 4
   BEGIN
       SET @trnAmt = (0-@txtTrnAmount);
   END

SET @CalInterest = 0;

IF @lblTrnAtyClass = 5
   BEGIN
        SET @CalDate = CAST(YEAR(@txtTranDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@txtTranDate) AS VARCHAR(2)) + '-' + CAST(DAY(@txtTranDate) AS VARCHAR(2))    

        EXECUTE Sp_CSCalculateODInterest @lblcuType,@lblCuNo,@txtMemNo,@txtAccType,@txtAccNo,@CalDate;

        SET @CalInterest= (SELECT CalInterest FROM A2ZACCOUNT WHERE AccNo = @txtAccNo);
   END


INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,
FuncOpt,FuncOptDesc,PayType,TrnType,TrnVchType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,ShowInterest,TrnInterestAmt,TrnDueIntAmt,
TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,TrnModule,FromCashCode,TrnPayment,UserID,AccTypeMode)

VALUES (@txtTranDate,@txtVchNo,@CtrlVoucherNo,0,@lblCuType,@lblCuNo,@txtMemNo,@txtAccType,@txtAccNo,@trnCode,
@FuncOpt,@FuncOptDesc,@PayType,3,'V',0,@txtDescription,0,@txtTrnAmount,0,0,abs(@CalInterest),
0,@TrnGLAccNoDr,0,@TrnGLAccNoDr,@trnAmt,@txtTrnAmount,0,0,@CtrlProcStat,1,@CashCode,@TrnPayment,@userID,1) 


------------ to transaction -----------------

SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @txtTrnAccType AND PayType = @lbltrnpaytype);

IF @lbltrnpaytype = 0
   BEGIN
        IF @lblTrnAtyClass = 0 AND @lblTrnAtyClass <> 5 AND @lblTrnAtyClass <> 6
            BEGIN
               SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
               SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
               SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
               SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1); 
               SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1); 
               SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
               SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
               SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1);
            END
         ELSE
         IF @lblTrnAtyClass = 5
            BEGIN
               SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 357);
               SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 357);
               SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 357);
               SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 357); 
               SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 357); 
               SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 357);
               SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 357);
               SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 357);
            END
         ELSE
         IF @lblTrnAtyClass = 6
            BEGIN
               SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 413);
               SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 413);
               SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 413);
               SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 413); 
               SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 413); 
               SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 413);
               SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 413);
               SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 413);
            END
   END
ELSE
   BEGIN
        SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype); 
        SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);    
        SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
        SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = @lbltrnpaytype);
   END


BEGIN
     UPDATE A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
				AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @txtTranDate, AccLastTrnAmtU = @txtTrnAmount,AccBalance = (AccBalance + @txtTrnAmount)
	 WHERE CuType = @lblTrnferCuType AND CuNo = @lblTrnferCuNo AND MemNo = @txtTrnMemNo AND AccType = @txtTrnAccType AND AccNo = @txtTrnAccNo;
END		


IF @lblTrnAtyClass = 5 or @lblTrnAtyClass = 6
   BEGIN
        SET @trnAmt = @txtIntAmount;
        SET @TransactionAmt = @txtIntAmount;
   END
ELSE
   BEGIN
        SET @trnAmt = @txtTrnAmount;
        SET @TransactionAmt = @txtTrnAmount;
   END

SET @Type = LEFT(@TrnGLAccNoCr,1);
IF @Type = 1 OR @Type = 5
   BEGIN
       SET @trnAmt = (0-@TransactionAmt);
   END


IF @lblTrnAtyClass <> 5 AND @lblTrnAtyClass <> 6
   BEGIN
        INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,
        FuncOpt,FuncOptDesc,PayType,TrnType,TrnVchType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,ShowInterest,TrnInterestAmt,
        TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,TrnModule,FromCashCode,TrnPayment,UserID,AccTypeMode)

        VALUES (@txtTranDate,@txtVchNo,@CtrlVoucherNo,0,@lblTrnferCuType,@lblTrnferCuNo,@txtTrnMemNo,@txtTrnAccType,@txtTrnAccNo,@trnCode,
        @FuncOpt,@FuncOptDesc,@PayType,3,'V',1,@txtDescription,@TransactionAmt,0,@ShowInterest,0,
        0,0,@TrnGLAccNoCr,@TrnGLAccNoCr,@trnAmt,0,@TransactionAmt,0,@CtrlProcStat,1,@CashCode,@TrnPayment,@userID,1) 
   END
--------------- LOAN INTEREST AMOUNT ---------------------------------

IF @txtPrincAmount = 0
   BEGIN
      SET @IntAmount = @txtIntAmount;
	  SET @ShowInterest = 2;
   END
ELSE
   BEGIN
      SET @IntAmount = 0;
	  SET @ShowInterest = 1;
   END


IF @txtIntAmount <> 0
   BEGIN
        INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,
        FuncOpt,FuncOptDesc,PayType,TrnType,TrnVchType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,ShowInterest,TrnInterestAmt,
        TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,TrnModule,FromCashCode,TrnPayment,UserID,AccTypeMode)

        VALUES (@txtTranDate,@txtVchNo,@CtrlVoucherNo,0,@lblTrnferCuType,@lblTrnferCuNo,@txtTrnMemNo,@txtTrnAccType,@txtTrnAccNo,@trnCode,
        @FuncOpt,@FuncOptDesc,@PayType,3,'V',1,@txtDescription,@TransactionAmt,0,@ShowInterest,@IntAmount,
        0,0,@TrnGLAccNoCr,@TrnGLAccNoCr,@trnAmt,0,@TransactionAmt,0,@CtrlProcStat,1,@CashCode,@TrnPayment,@userID,1) 
------------------------------ OD  Loan Account -----------------------
        IF @lblTrnAtyClass = 5
           BEGIN
		    	SET @DueIntFlag = 1;
                UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + @TransactionAmt)
                       --AccPrevDueIntAmt = AccDueIntAmt,	
                       --AccDueIntAmt = (AccDueIntAmt - @TransactionAmt),
                       --AccPrevODIntDate = AccODIntDate, AccODIntDate = @txtTranDate,
                       --AccHoldIntAmt = 0        				
		  	    WHERE AccNo = @txtTrnAccNo;
	
           END
        ELSE
            BEGIN 
----------------------------Term Loan Account----------------------------------------------------------
               	UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + @TransactionAmt)			                      
		        WHERE AccNo = @txtTrnAccNo;
		
--                UPDATE A2ZLOANDEFAULTER SET PaidIntAmt = (PaidIntAmt + @TransactionAmt)                                                           				
--	            WHERE AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                      YEAR(TrnDate) = YEAR(@txtTranDate);
--
--                SET @PayableIntAmt = (SELECT PayableIntAmt FROM A2ZLOANDEFAULTER WHERE 
--                       AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                       YEAR(TrnDate) = YEAR(@txtTranDate));
--             
--                SET @PaidIntAmt = (SELECT PaidIntAmt FROM A2ZLOANDEFAULTER WHERE 
--                       AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                       YEAR(TrnDate) = YEAR(@txtTranDate));
--            
--                SET @CurrDueIntAmt = (@PayableIntAmt - @PaidIntAmt);
--
--                IF @CurrDueIntAmt < 0
--                   BEGIN
--                        SET @CurrDueIntAmt = 0;
--                   END
--
--                UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = @CurrDueIntAmt                                                                 				
--		        WHERE AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                         YEAR(TrnDate) = YEAR(@txtTranDate);    

            END        

   END

--------------------LOAN PRINCIPAL AMOUNT---------------------------------

 IF @lblTrnAtyClass = 5 OR @lblTrnAtyClass = 6
    BEGIN
         IF @lblTrnAtyClass = 5
            BEGIN
               SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 354);
               SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 354);
               SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 354);
               SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 354); 
               SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 354); 
               SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 354);
               SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 354);
               SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 354);
            END
         ELSE
         IF @lblTrnAtyClass = 6
            BEGIN
               SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 407);
               SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 407);
               SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 407);
               SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 407); 
               SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 407); 
               SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 407);
               SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 407);
               SET @TrnPayment= (SELECT TrnPayment FROM A2ZTRNCTRL WHERE AccType = @txtTrnAccType AND FuncOpt=20 AND TrnMode = 1 AND PayType = 407);
            END

         
        SET @trnAmt = @txtPrincAmount;
        SET @TransactionAmt = @txtPrincAmount;
  
        SET @Type = LEFT(@TrnGLAccNoCr,1);
        IF @Type = 1 OR @Type = 5
           BEGIN
               SET @trnAmt = (0-@TransactionAmt);
           END

        
       
        IF @txtPrincAmount <> 0
            BEGIN
                 INSERT INTO A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,TrnChqNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,
                 FuncOpt,FuncOptDesc,PayType,TrnType,TrnVchType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,ShowInterest,TrnInterestAmt,
                 TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,TrnModule,FromCashCode,TrnPayment,UserID,AccTypeMode)

                 VALUES (@txtTranDate,@txtVchNo,@CtrlVoucherNo,0,@lblTrnferCuType,@lblTrnferCuNo,@txtTrnMemNo,@txtTrnAccType,@txtTrnAccNo,@trnCode,
                 @FuncOpt,@FuncOptDesc,@PayType,3,'V',1,@txtDescription,@TransactionAmt,0,@ShowInterest,@txtIntAmount,
                 0,0,@TrnGLAccNoCr,@TrnGLAccNoCr,@trnAmt,0,@TransactionAmt,0,@CtrlProcStat,1,@CashCode,@TrnPayment,@userID,1) 

          --------------------------OD Loan Account-----------------------------------------------------
                 IF @lblTrnAtyClass = 5
                    BEGIN
                        UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @TransactionAmt),
				     	AccPrincipal = (AccPrincipal + @TransactionAmt),
                        AccTotalDep = (AccTotalDep + @TransactionAmt)
                        --AccPrevODIntDate = AccODIntDate, AccODIntDate = @txtTranDate	                				
			            WHERE AccNo = @txtTrnAccNo;
		
                     --   IF @DueIntFlag = 0
                     --      BEGIN
                     --         UPDATE A2ZACCOUNT SET AccDueIntAmt = (AccDueIntAmt - @TransactionAmt),  AccHoldIntAmt = 0             				
			                  --WHERE AccNo = @txtTrnAccNo;
                     --      END
                    END
                 ELSE
                     BEGIN
         --------------------------Term Loan Account-----------------------------------------------------
                          UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + @TransactionAmt),
					      AccPrincipal = (AccPrincipal + @TransactionAmt),
                          AccTotalDep = (AccTotalDep + @TransactionAmt)               
			              WHERE AccNo = @txtTrnAccNo;   

--                          UPDATE A2ZLOANDEFAULTER SET PaidPrincAmt = (PaidPrincAmt + @TransactionAmt)                                                              				
--		                  WHERE AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                            YEAR(TrnDate) = YEAR(@txtTranDate);
--                               
--                          SET @PayablePrincAmt = (SELECT PayablePrincAmt FROM A2ZLOANDEFAULTER WHERE 
--                              AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                          YEAR(TrnDate) = YEAR(@txtTranDate));
--             
--                          SET @PaidPrincAmt = (SELECT PaidPrincAmt FROM A2ZLOANDEFAULTER WHERE 
--                              AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                          YEAR(TrnDate) = YEAR(@txtTranDate));
--            
--                          SET @CurrDuePrincAmt = (@PayablePrincAmt - @PaidPrincAmt);
--
--                          IF @CurrDuePrincAmt < 0
--                             BEGIN
--                                 SET @CurrDuePrincAmt = 0;
--                             END
--
--                          UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = @CurrDuePrincAmt                                                                 				
--		                  WHERE AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                          YEAR(TrnDate) = YEAR(@txtTranDate);            
--
--                          SET @UptoDuePrincAmt = (SELECT UptoDuePrincAmt FROM A2ZLOANDEFAULTER WHERE 
--                          AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                          YEAR(TrnDate) = YEAR(@txtTranDate));
--        
--                          IF @UptoDuePrincAmt > 0
--                             BEGIN
--                               SET @CurrDuePrincAmt = (SELECT CurrDuePrincAmt FROM A2ZLOANDEFAULTER WHERE 
--                               AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                               YEAR(TrnDate) = YEAR(@txtTranDate));
--            
--                          SET @AccLoanInstlAmt = (SELECT AccLoanInstlAmt FROM A2ZACCOUNT WHERE 
--                               AccNo = @txtTrnAccNo);                          
--
--                          SET @NoDueInstalment = (@CurrDuePrincAmt / ABS(@AccLoanInstlAmt));
--            
--                          UPDATE A2ZLOANDEFAULTER SET NoDueInstalment = @NoDueInstalment                                                                 				
--		                  WHERE AccNo=@txtTrnAccNo AND MONTH(TrnDate) = MONTH(@txtTranDate) AND 
--                          YEAR(TrnDate) = YEAR(@txtTranDate);
--                    END
              END
         END
    END

------------------------------------------------------------------------------

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

