USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAccTranTransfer]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[Sp_CSAccTranTransfer]
(
 @userID int
,@txtTranDate VARCHAR(10)
,@txtVchNo varchar(20)

--,@lblCuType int
--,@lblCuNo int
--,@txtMemNo int
--,@txtAccType int
--,@txtAccNo Bigint
--,@lblTrnferCuType int
--,@lblTrnferCuNo int
--,@txtTrnMemNo int
--,@txtTrnAccNo Bigint
--,@txtTrnAmount money
--,@lblPaytype int

)

AS
/*
EXECUTE Sp_CSAccTranTransfer 1,'2016-12-31','186498',3,685,0,12,123068500000001,3,712,0,1230712000000001,500,1



*/

BEGIN

DECLARE @atyclass INT;

DECLARE @fYear INT;
DECLARE @tYear INT;
DECLARE @trnDate smalldatetime;
DECLARE @openTable VARCHAR(30);
DECLARE @strSQL NVARCHAR(MAX);

DECLARE @ProcDate VARCHAR(10);


DECLARE @CuType    INT;
DECLARE @CuNo      INT;
DECLARE @MemNo     INT;
DECLARE @AccType   INT;
DECLARE @AccNo     Bigint;
DECLARE @ToCuType  INT;
DECLARE @ToCuNo    INT;
DECLARE @ToMemNo   INT;
DECLARE @ToAccType INT;
DECLARE @ToAccNo   Bigint;
DECLARE @GLAmount  MONEY;
DECLARE @PayType INT;
DECLARE @TrnDrCr INT;
DECLARE @ShowInterest INT;
DECLARE @TrnCSGL INT;


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))




IF  @txtTranDate = @trnDate
   BEGIN
      SET @openTable = 'A2ZCSMCUS' + '..A2ZTRANSACTION';
   END
ELSE
   BEGIN
      SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@txtTranDate) AS VARCHAR(4)) + '..A2ZTRANSACTION';
   END


--PRINT @openTable;

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,ToCuType,ToCuNo,ToMemNo,ToAccType,ToAccNo,GLAmount,PayType,TrnDrCr,ShowInterest,TrnCSGL
FROM WF_TRFTRANSACTION WHERE DelUserId=@userID;

OPEN accTable;
FETCH NEXT FROM accTable INTO
 @CuType,@CuNo,@MemNo,@AccType,@AccNo,@ToCuType,@ToCuNo,@ToMemNo,@ToAccType,@ToAccNo,@GLAmount,@PayType,@TrnDrCr,@ShowInterest,@TrnCSGL;


WHILE @@FETCH_STATUS = 0 
	BEGIN


SET @strSQL = 'UPDATE ' + @openTable + ' SET CuType='+ CAST(@ToCuType AS VARCHAR(2)) + 
',CuNo=' + CAST(@ToCuNo AS VARCHAR(6)) +
',MemNo=' + CAST(@ToMemNo AS VARCHAR(6)) +',AccNo=' + CAST(@ToAccNo AS VARCHAR(16)) + 

',ReTrnId=' + CAST(@userID AS VARCHAR(4)) +',ReTrnDate=' + '''' + CAST(@ProcDate AS VARCHAR(10)) + '''' + 

' WHERE CuType=' + CAST(@CuType AS VARCHAR(2)) + 
' AND CuNo=' + CAST(@CuNo AS VARCHAR(6)) +
' AND MemNo=' + CAST(@MemNo AS VARCHAR(6)) + ' AND AccType=' + CAST(@AccType AS VARCHAR(2)) + 
' AND AccNo= ' + CAST(@AccNo AS VARCHAR(16)) +
--' AND PayType= ' + CAST(@lblPaytype AS VARCHAR(3)) +
' AND TrnDate=' + '''' + CAST(@txtTranDate AS VARCHAR(10)) + '''' +
' AND VchNo=' + '''' + CAST(@txtVchNo AS varchar(20)) + '''';

--PRINT @strSQL;

EXECUTE (@strSQL);


SET @atyclass = (SELECT AccTypeClass FROM A2ZACCTYPE WHERE AccTypeCode = @AccType);

---------Trnfrom from-------------------------------------

IF @PayType != 302 AND @PayType != 352 AND @PayType != 402 AND @PayType != 404
   BEGIN
       UPDATE A2ZACCOUNT SET AccBalance = (AccBalance - ABS(@GLAmount))
	   WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo;
   END	
-------------------------------------------------------------

----------Trnfrom To-----------------------------------------
IF @PayType != 302 AND @PayType != 352 AND @PayType != 402 AND @PayType != 404
   BEGIN
       UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + ABS(@GLAmount))
	   WHERE CuType = @ToCuType AND CuNo = @ToCuNo AND MemNo = @ToMemNo AND AccType = @AccType AND AccNo = @ToAccNo;
END	
---------------------------------------------------------------

--  For OD Loan Withdrawal Transaction ---------
IF @PayType = 351
   BEGIN
		UPDATE A2ZACCOUNT SET AccPrincipal = (AccPrincipal - ABS(@GLAmount))			
		WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo;

        UPDATE A2ZACCOUNT SET AccPrincipal = (AccPrincipal + ABS(@GLAmount))			
    	WHERE CuType = @ToCuType AND CuNo = @ToCuNo AND MemNo = @ToMemNo AND AccType = @AccType AND AccNo = @ToAccNo;
   END


--  For OD Loan Interest Received Transaction ---------

IF @PayType = 352
   BEGIN
       UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid - ABS(@GLAmount)),
       AccDueIntAmt = (AccDueIntAmt + ABS(@GLAmount))                  				
	   WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo;

       UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + ABS(@GLAmount)),
       AccDueIntAmt = (AccDueIntAmt - ABS(@GLAmount))                       				
	   WHERE CuType = @ToCuType AND CuNo = @ToCuNo AND MemNo = @ToMemNo AND AccType = @AccType AND AccNo = @ToAccNo;
   END

--  For OD Loan Amount Received Transaction ---------
IF @PayType = 353 
   BEGIN
    	UPDATE A2ZACCOUNT SET AccPrincipal = (AccPrincipal - ABS(@GLAmount)),
        AccTotalDep = (AccTotalDep - @GLAmount)        				
		WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo;
		
        UPDATE A2ZACCOUNT SET AccPrincipal = (AccPrincipal + ABS(@GLAmount)),
        AccTotalDep = (AccTotalDep + ABS(@GLAmount))            				
		WHERE CuType = @ToCuType AND CuNo = @ToCuNo AND MemNo = @ToMemNo AND AccType = @AccType AND AccNo = @ToAccNo;

   END


IF @atyclass = 4 AND @PayType = 301 
   BEGIN
---  from
       UPDATE A2ZACCOUNT SET AccTotalDep = (AccTotalDep - ABS(@GLAmount))
       WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo;

       UPDATE A2ZPENSIONDEFAULTER SET PaidDepositAmt = (PaidDepositAmt - ABS(@GLAmount))                                                           				
		         WHERE AccNo=@AccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                      YEAR(@txtTranDate) = YEAR(@txtTranDate);     
       UPDATE A2ZPENSIONDEFAULTER SET CurrDueDepositAmt = (CurrDueDepositAmt + ABS(@GLAmount))                                                                      				
		           WHERE AccNo=@AccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate);  
        
       IF MONTH(@txtTranDate) <> MONTH(@trnDate)    
          BEGIN   
              UPDATE A2ZPENSIONDEFAULTER SET UptoDueDepositAmt = (UptoDueDepositAmt + ABS(@GLAmount))                                                           				
		      WHERE AccNo=@AccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);  
              UPDATE A2ZPENSIONDEFAULTER SET PayableDepositAmt = (PayableDepositAmt + ABS(@GLAmount))                                                           				
		      WHERE AccNo=@AccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);     
              UPDATE A2ZPENSIONDEFAULTER SET CurrDueDepositAmt = (CurrDueDepositAmt + ABS(@GLAmount))                                                                      				
		      WHERE AccNo=@AccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);          
          END
---  to

       UPDATE A2ZACCOUNT SET AccTotalDep = (AccTotalDep + ABS(@GLAmount))
	   WHERE CuType = @ToCuType AND CuNo = @ToCuNo AND MemNo = @ToMemNo AND AccType = @ToAccType AND AccNo = @ToAccNo;

       UPDATE A2ZPENSIONDEFAULTER SET PaidDepositAmt = (PaidDepositAmt + ABS(@GLAmount))                                                           				
		         WHERE AccNo=@ToAccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                      YEAR(@txtTranDate) = YEAR(@txtTranDate);     
       UPDATE A2ZPENSIONDEFAULTER SET CurrDueDepositAmt = (CurrDueDepositAmt - ABS(@GLAmount))                                                                      				
		           WHERE AccNo=@ToAccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate); 
---
       IF MONTH(@txtTranDate) <> MONTH(@trnDate)    
          BEGIN
                 UPDATE A2ZPENSIONDEFAULTER SET UptoDueDepositAmt = (UptoDueDepositAmt - ABS(@GLAmount))                                                           				
		         WHERE AccNo=@ToAccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);  
                 UPDATE A2ZPENSIONDEFAULTER SET PayableDepositAmt = (PayableDepositAmt - ABS(@GLAmount))                                                           				
		         WHERE AccNo=@ToAccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);       
                 UPDATE A2ZPENSIONDEFAULTER SET CurrDueDepositAmt = (CurrDueDepositAmt - ABS(@GLAmount))                                                                      				
		         WHERE AccNo=@ToAccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                         YEAR(@trnDate) = YEAR(@trnDate);      
           END

   END

IF @atyclass = 4 AND @PayType = 302 
   BEGIN
----  from

       UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid - ABS(@GLAmount))
       WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo;

       UPDATE A2ZPENSIONDEFAULTER SET PaidPenalAmt = (PaidPenalAmt - ABS(@GLAmount))                                                                                                  				
		    WHERE AccNo=@AccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                 YEAR(@txtTranDate) = YEAR(@txtTranDate);     
    
---- to

       UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + ABS(@GLAmount))
	   WHERE CuType = @ToCuType AND CuNo = @ToCuNo AND MemNo = @ToMemNo AND AccType = @ToAccType AND AccNo = @ToAccNo;

       UPDATE A2ZPENSIONDEFAULTER SET PaidPenalAmt = (PaidPenalAmt + ABS(@GLAmount))                                                                                                  				
		    WHERE AccNo=@ToAccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                 YEAR(@txtTranDate) = YEAR(@txtTranDate);  

   END

IF @payType = 402
		BEGIN
------  from
			UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid - ABS(@GLAmount))			                      
			WHERE AccNo = @AccNo;
	   
            UPDATE A2ZLOANDEFAULTER SET PaidIntAmt = (PaidIntAmt - ABS(@GLAmount))                                                           				
			    WHERE AccNo=@AccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                      YEAR(@txtTranDate) = YEAR(@txtTranDate);
            UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = (CurrDueIntAmt + ABS(@GLAmount))                                                                 				
		           WHERE AccNo=@AccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate);     
            UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = 0                                                                 				
		           WHERE AccNo=@AccNo AND CurrDueIntAmt < 0 AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate); 

-----
            IF MONTH(@txtTranDate) <> MONTH(@trnDate)    
               BEGIN
                    UPDATE A2ZLOANDEFAULTER SET UptoDueIntAmt = (UptoDueIntAmt + ABS(@GLAmount))                                                           				
			        WHERE AccNo=@AccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);
                    UPDATE A2ZLOANDEFAULTER SET PayableIntAmt = (PayableIntAmt + ABS(@GLAmount))                                                           				
			        WHERE AccNo=@AccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);
                    UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = (CurrDueIntAmt + ABS(@GLAmount))                                                                 				
		            WHERE AccNo=@AccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                         YEAR(@trnDate) = YEAR(@trnDate);     
                    UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = 0                                                                 				
		            WHERE AccNo=@AccNo AND CurrDueIntAmt < 0 AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                         YEAR(@trnDate) = YEAR(@trnDate); 
               END      
-----  to
            UPDATE A2ZACCOUNT SET AccTotIntPaid = (AccTotIntPaid + ABS(@GLAmount))			                      
			WHERE AccNo = @ToAccNo;	 
  
            UPDATE A2ZLOANDEFAULTER SET PaidIntAmt = (PaidIntAmt + ABS(@GLAmount))                                                           				
			    WHERE AccNo=@ToAccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                      YEAR(@txtTranDate) = YEAR(@txtTranDate);
            UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = (CurrDueIntAmt - ABS(@GLAmount))                                                                 				
		           WHERE AccNo=@ToAccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate);     
            UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = 0                                                                 				
		           WHERE AccNo=@ToAccNo AND CurrDueIntAmt < 0 AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate);   
           
----
            IF MONTH(@txtTranDate) <> MONTH(@trnDate)    
               BEGIN
                    UPDATE A2ZLOANDEFAULTER SET UptoDueIntAmt = (UptoDueIntAmt - ABS(@GLAmount))                                                           				
			        WHERE AccNo=@ToAccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);
                    UPDATE A2ZLOANDEFAULTER SET PayableIntAmt = (PayableIntAmt - ABS(@GLAmount))                                                           				
			        WHERE AccNo=@ToAccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);
                    UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = (CurrDueIntAmt - ABS(@GLAmount))                                                                 				
		            WHERE AccNo=@ToAccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                         YEAR(@trnDate) = YEAR(@trnDate);     
                    UPDATE A2ZLOANDEFAULTER SET CurrDueIntAmt = 0                                                                 				
		            WHERE AccNo=@ToAccNo AND CurrDueIntAmt < 0 AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                         YEAR(@trnDate) = YEAR(@trnDate);   
               END
     
        END;


IF @payType = 403 
		BEGIN
---- from
			UPDATE A2ZACCOUNT SET AccBalance = (AccBalance - ABS(@GLAmount)),
					AccPrincipal = (AccPrincipal - ABS(@GLAmount)),
                    AccTotalDep = (AccTotalDep - ABS(@GLAmount))               
			WHERE AccNo = @AccNo;   

            UPDATE A2ZLOANDEFAULTER SET PaidPrincAmt = (PaidPrincAmt - ABS(@GLAmount))                                                        				
		         WHERE AccNo=@AccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                      YEAR(@txtTranDate) = YEAR(@txtTranDate);
            UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = (CurrDuePrincAmt + ABS(@GLAmount))                                                                 				
		           WHERE AccNo=@AccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate);   
            UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = 0                                                                 				
		           WHERE AccNo=@AccNo AND CurrDuePrincAmt < 0 AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate);  

-----
            IF MONTH(@txtTranDate) <> MONTH(@trnDate)    
               BEGIN
                    UPDATE A2ZLOANDEFAULTER SET UptoDuePrincAmt = (UptoDuePrincAmt + ABS(@GLAmount))                                                        				
		            WHERE AccNo=@AccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);
                    UPDATE A2ZLOANDEFAULTER SET PayablePrincAmt = (PayablePrincAmt + ABS(@GLAmount))                                                        				
		            WHERE AccNo=@AccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);
                    UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = (CurrDuePrincAmt + ABS(@GLAmount))                                                                 				
		            WHERE AccNo=@AccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                         YEAR(@trnDate) = YEAR(@trnDate);   
                    UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = 0                                                                 				
		            WHERE AccNo=@AccNo AND CurrDuePrincAmt < 0 AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                         YEAR(@trnDate) = YEAR(@trnDate);   
                END                  
----  to
            UPDATE A2ZACCOUNT SET AccBalance = (AccBalance + ABS(@GLAmount)),
					AccPrincipal = (AccPrincipal - ABS(@GLAmount)),
                    AccTotalDep = (AccTotalDep - ABS(@GLAmount))               
			WHERE AccNo = @ToAccNo;   

            UPDATE A2ZLOANDEFAULTER SET PaidPrincAmt = (PaidPrincAmt + ABS(@GLAmount))                                  
		         WHERE AccNo=@ToAccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                      YEAR(@txtTranDate) = YEAR(@txtTranDate);
            UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = (CurrDuePrincAmt - ABS(@GLAmount))                                                                 				
		           WHERE AccNo=@ToAccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate);   
            UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = 0                                                                 				
		           WHERE AccNo=@ToAccNo AND CurrDuePrincAmt < 0 AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                         YEAR(@txtTranDate) = YEAR(@txtTranDate);         

----
            IF MONTH(@txtTranDate) <> MONTH(@trnDate)    
               BEGIN
                    UPDATE A2ZLOANDEFAULTER SET UptoDuePrincAmt = (UptoDuePrincAmt - ABS(@GLAmount))                                                        				
		            WHERE AccNo=@ToAccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);
                    UPDATE A2ZLOANDEFAULTER SET PayablePrincAmt = (PayablePrincAmt - ABS(@GLAmount))                                                        				
		            WHERE AccNo=@ToAccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                      YEAR(@trnDate) = YEAR(@trnDate);
                    UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = (CurrDuePrincAmt - ABS(@GLAmount))                                                                 				
		            WHERE AccNo=@ToAccNo AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                         YEAR(@trnDate) = YEAR(@trnDate);   
                    UPDATE A2ZLOANDEFAULTER SET CurrDuePrincAmt = 0                                                                 				
		            WHERE AccNo=@ToAccNo AND CurrDuePrincAmt < 0 AND MONTH(@trnDate) = MONTH(@trnDate) AND 
                         YEAR(@trnDate) = YEAR(@trnDate); 
               END

		END;

IF @payType = 404
		BEGIN
-----  from
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid - ABS(@GLAmount)) 
			WHERE AccNo = @AccNo;

            UPDATE A2ZLOANDEFAULTER SET PaidPenalAmt = (PaidPenalAmt - ABS(@GLAmount))                                                                                                    				
		    WHERE AccNo=@AccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                 YEAR(@txtTranDate) = YEAR(@txtTranDate);


-----  to
         	UPDATE A2ZACCOUNT SET AccTotPenaltyPaid = (AccTotPenaltyPaid + ABS(@GLAmount)) 
			WHERE AccNo = @ToAccNo;

            UPDATE A2ZLOANDEFAULTER SET PaidPenalAmt = (PaidPenalAmt + ABS(@GLAmount))                                                                                                    				
		    WHERE AccNo=@ToAccNo AND MONTH(@txtTranDate) = MONTH(@txtTranDate) AND 
                 YEAR(@txtTranDate) = YEAR(@txtTranDate);

		END;

--------------------------------------------------------------------------------

    IF @TrnDrCr = 0 AND @ShowInterest = 0 AND @TrnCSGL = 0
        BEGIN
             UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccTodaysOpBalance = AccTodaysOpBalance + abs(@GLAmount)
             WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo;
    
             UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccTodaysOpBalance = AccTodaysOpBalance - abs(@GLAmount)
             WHERE CuType = @ToCuType AND CuNo = @ToCuNo AND MemNo = @ToMemNo AND AccType = @ToAccType AND AccNo = @ToAccNo;
        END
    
    IF @TrnDrCr = 1 AND @ShowInterest = 0 AND @TrnCSGL = 0
        BEGIN
             UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccTodaysOpBalance = AccTodaysOpBalance - abs(@GLAmount)
             WHERE CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo;
    
             UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccTodaysOpBalance = AccTodaysOpBalance + abs(@GLAmount)
             WHERE CuType = @ToCuType AND CuNo = @ToCuNo AND MemNo = @ToMemNo AND AccType = @ToAccType AND AccNo = @ToAccNo;
        END

--------------------------------------------------------------------------------


FETCH NEXT FROM accTable INTO
		@CuType,@CuNo,@MemNo,@AccType,@AccNo,@ToCuType,@ToCuNo,@ToMemNo,@ToAccType,@ToAccNo,@GLAmount,@PayType,@TrnDrCr,@ShowInterest,@TrnCSGL;


	END

CLOSE accTable; 
DEALLOCATE accTable;




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
