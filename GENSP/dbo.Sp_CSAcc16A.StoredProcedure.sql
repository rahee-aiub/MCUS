USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAcc16A]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_CSAcc16A]

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

DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accNoRenwl smallint;

DECLARE @newRenwlDate smalldatetime;
DECLARE @newNoRenwl smallint;
DECLARE @newRenwlAmt money;
DECLARE @AccIntRate money;


DECLARE @accAnniDate smalldatetime;
DECLARE @accNoAnni smallint;
DECLARE @accAnniAmt money;

DECLARE @accMatureDate smalldatetime;
DECLARE @dummyRDate smalldatetime;
DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;
DECLARE @noMonths int;
DECLARE @countR int;
DECLARE @countM int;

DECLARE @CalDate int;
-----------------------------end--------------------------------

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);


DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccBalance,AccOrgAmt,AccOpenDate,AccRenwlDate,AccRenwlAmt,AccPeriod,AccIntRate,AccMatureDate,AccNoRenwl,AccAnniDate,AccNoAnni,AccAnniAmt
FROM A2ZACCOUNT WHERE AccType = 16 AND AccBalance > 0 AND AccStatus < 97;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOrgAmt,@accOpenDate,@accRenwlDate,@accRenwlAmt,@accPeriod,@AccIntRate,@accMatureDate,@accNoRenwl,@accAnniDate,@accNoAnni,@accAnniAmt;

WHILE @@FETCH_STATUS = 0 
	BEGIN

        SET @accRenwlDate = NULL;
        SET @accRenwlAmt = 0;
        SET @accNoRenwl = 0;    

        SET @accAnniDate = NULL;
        SET @accAnniAmt = 0;
        SET @accNoAnni = 0;  
        SET @dummyRDate = @accOpenDate;      

--        SET @accMatureDate = (DATEADD(month,@accPeriod,@dummyRDate));   

        SET @accMatureDate = (DATEADD(month,72,@accOpenDate)); 

        SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);  


        IF @accMatureDate > '2014-06-30' AND @accMatureDate < @trnDate
           BEGIN
              IF @memType = 1
                 BEGIN
                      SET @accPeriod = 66;
                      SET @AccIntRate = 13.40;
                 END
              ELSE
                 BEGIN
                      SET @accPeriod = 78;
                      SET @AccIntRate = 11.25;
                 END
         END
           

        IF @accOpenDate > '2014-06-30' 
           BEGIN
              IF @memType = 1
                 BEGIN
                      SET @accPeriod = 66;
                      SET @AccIntRate = 13.40;
                 END
              ELSE
                 BEGIN
                      SET @accPeriod = 78;
                      SET @AccIntRate = 11.25;
                 END
         END


    

                   
      UPDATE A2ZACCOUNT SET AccIntRate=@AccIntRate,AccPeriod=@accPeriod  
             WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo; 

    
      FETCH NEXT FROM accTable INTO
		    @cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOrgAmt,@accOpenDate,@accRenwlDate,@accRenwlAmt,@accPeriod,@AccIntRate,@accMatureDate,@accNoRenwl,@accAnniDate,@accNoAnni,@accAnniAmt;


	END


CLOSE accTable; 
DEALLOCATE accTable;

END






















GO
