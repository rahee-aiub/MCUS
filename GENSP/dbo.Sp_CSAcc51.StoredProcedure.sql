USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAcc51]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[Sp_CSAcc51] 

AS

BEGIN



DECLARE @cuType smallint;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType smallint;
DECLARE @accNo Bigint;

DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @fdAmount money;
DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accRenwlAmt money;
DECLARE @accPeriod smallint;


DECLARE @accRenwlDate smalldatetime;
DECLARE @accNoRenwl smallint;
DECLARE @accAnniDate smalldatetime;
DECLARE @newRenwlDate smalldatetime;
DECLARE @newNoRenwl smallint;
DECLARE @newRenwlAmt money;


DECLARE @AccIntRate smallmoney;
DECLARE @AccNoInstl int;
DECLARE @AccLoanInstlAmt money;
DECLARE @AccDisbAmt money;

DECLARE @accLoanExpiryDate smalldatetime;
DECLARE @accOpenDate smalldatetime;

DECLARE @accMatureDate smalldatetime;
DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;
DECLARE @noMonths int;
DECLARE @countR int;
-----------------------------end--------------------------------


DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccIntRate,AccNoInstl,AccLoanInstlAmt,AccDisbAmt,AccOpenDate
FROM A2ZACCOUNT WHERE AccType = 51 AND AccStatus < 97;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@AccIntRate,@AccNoInstl,@AccLoanInstlAmt,@AccDisbAmt,@accOpenDate;

WHILE @@FETCH_STATUS = 0 
	BEGIN
 
        
        SET @accLoanExpiryDate = (DATEADD(MONTH,@AccNoInstl,@accOpenDate))        


        IF @AccIntRate = 0  
           BEGIN
               SET @AccIntRate = 18;
           END 
        
        IF @AccLoanInstlAmt = 0 AND @AccDisbAmt IS NOT NULL AND @AccNoInstl IS NOT NULL AND @AccNoInstl !=0  
           BEGIN
               SET @AccLoanInstlAmt = (@AccDisbAmt / @AccNoInstl);
           END 
                    
      UPDATE A2ZACCOUNT SET AccIntRate=@AccIntRate,AccLoanInstlAmt=@AccLoanInstlAmt,AccLoanExpiryDate=@accLoanExpiryDate 
             WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo; 

    
      FETCH NEXT FROM accTable INTO
		    @cuType,@cuNo,@memNo,@accType,@accNo,@AccIntRate,@AccNoInstl,@AccLoanInstlAmt,@AccDisbAmt,@accOpenDate;


	END


CLOSE accTable; 
DEALLOCATE accTable;

END



















































GO
