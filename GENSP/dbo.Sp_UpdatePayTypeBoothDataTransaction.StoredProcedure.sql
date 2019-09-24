USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdatePayTypeBoothDataTransaction]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROCEDURE  [dbo].[Sp_UpdatePayTypeBoothDataTransaction](@userID INT)

--ALTER PROCEDURE  [dbo].[Sp_CSAddTransaction]
AS

BEGIN

--DECLARE @userID INT;
--DECLARE @vchNo nvarchar(20);
DECLARE @ProcStat smallint;
DECLARE @ID INT;
DECLARE @cuType INT;
DECLARE @cuNo INT;
DECLARE @memNo INT;
DECLARE @accType INT;
DECLARE @accNo BIGINT;
DECLARE @payType INT;
DECLARE @creditAmount MONEY;
DECLARE @debitAmount MONEY;
DECLARE @trnDate SMALLDATETIME;
DECLARE @trnType INT;
DECLARE @FromCashCode INT;
DECLARE @LoanApplicationNo INT;
DECLARE @GLAmount MONEY;

--- Update Account Table base on Workfile ---

--SET @userID = 3;
--SET @vchNo = 1;
SET @ProcStat = 0;


DECLARE wfTrnTable CURSOR FOR
SELECT Id,CuType,CuNo,MemNo,AccType,AccNo,PayTypeCode,Credit,Debit,TrnDate,TrnTypeCode,GLAmount
FROM WF_Transaction
WHERE TrnFlag = 0 AND UserId = @userId;

			
OPEN wfTrnTable; 
FETCH NEXT FROM wfTrnTable INTO @ID, @cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@trnDate,@trnType,@GLAmount; 
WHILE @@FETCH_STATUS = 0 
BEGIN
	 

IF @payType = 11 OR @payType = 12 OR @payType = 13 OR 
   @payType = 18 OR @payType = 19 OR @payType = 21 OR 
   @payType = 23 OR @payType = 24 
		BEGIN
			UPDATE WF_Transaction SET PayTypeCode =1 Where GLAmount<0  
	        AND PayTypeCode=@payType 
		END;
IF @payType = 11 OR @payType = 12 OR @payType = 13 OR 
   @payType = 18 OR @payType = 19 OR @payType = 21 OR 
   @payType = 23 OR @payType = 24 
BEGIN
   UPDATE WF_Transaction SET PayTypeCode =2 Where GLAmount>0 
	        AND PayTypeCode=@payType 
END ;

IF @payType = 2
		BEGIN
         	UPDATE WF_Transaction SET PayTypeCode =110 Where GLAmount<0 
	        AND AccType=15 OR AccType=16 AND PayTypeCode=@payType 
            UPDATE WF_Transaction SET PayTypeCode =101 Where GLAmount>0 
	         AND  AccType=15 OR AccType=16 AND  PayTypeCode=@payType 

            UPDATE WF_Transaction SET PayTypeCode =206 Where GLAmount<0 
	        AND AccType=17 AND PayTypeCode=@payType  
            UPDATE WF_Transaction SET PayTypeCode =201 Where GLAmount>0 
	        AND AccType=17 AND PayTypeCode=@payType  
		END;
IF @payType = 3 OR @payType = 4
        BEGIN
        UPDATE WF_Transaction SET PayTypeCode =402 Where GLAmount>0 
	        AND  AccType=51 AND PayTypeCode=@payType 
        UPDATE WF_Transaction SET PayTypeCode =352 Where GLAmount>0 
	        AND  AccType=52 AND PayTypeCode=@payType 
        UPDATE WF_Transaction SET PayTypeCode =402 Where GLAmount>0 
	        AND AccType=53 AND PayTypeCode=@payType 
        UPDATE WF_Transaction SET PayTypeCode =402 Where GLAmount>0 
	        AND AccType=54 AND PayTypeCode=@payType  

        UPDATE WF_Transaction SET PayTypeCode =203 Where GLAmount<0 
	        AND AccType=17 AND PayTypeCode=@payType 
        END;
IF @payType = 2 OR @accType>50 
        BEGIN
        UPDATE WF_Transaction SET PayTypeCode =401 Where GLAmount<0 
	        AND  AccType=51 AND PayTypeCode=@payType 
        UPDATE WF_Transaction SET PayTypeCode =403 Where GLAmount>0 
	        AND AccType=51 AND PayTypeCode=@payType 
        UPDATE WF_Transaction SET PayTypeCode =351 Where GLAmount<0 
	        AND  AccType=52 AND PayTypeCode=@payType 
        UPDATE WF_Transaction SET PayTypeCode =353 Where GLAmount>0 
 	        AND AccType=52 AND PayTypeCode=@payType 
        UPDATE WF_Transaction SET PayTypeCode =401 Where GLAmount<0 
	        AND  AccType=53 OR AccType=54 AND PayTypeCode=@payType 
        UPDATE WF_Transaction SET PayTypeCode =403 Where GLAmount>0 
	        AND  AccType=53 OR AccType=54 AND PayTypeCode=@payType  
        END;

FETCH NEXT FROM wfTrnTable INTO  @ID,@cuType,@cuNo,@memNo,@accType,@accNo,@payType,@creditAmount,@debitAmount,@trnDate,@trnType,@GLAmount; 

END;	       
CLOSE wfTrnTable; 
DEALLOCATE wfTrnTable;

END;





























































GO
