USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAcc53IntRate]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE PROCEDURE [dbo].[Sp_CSAcc53IntRate] 

AS

BEGIN



DECLARE @cuType smallint;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType smallint;
DECLARE @accNo Bigint;
DECLARE @IntRate smallmoney;

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
SELECT CuNo,IntRate
FROM WFTEACHERSLOANINTRATE;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuNo,@IntRate;

WHILE @@FETCH_STATUS = 0 
	BEGIN
                     
    UPDATE A2ZACCOUNT SET AccIntRate=@IntRate 
             WHERE AccType = 53 AND CuNo = @cuNo; 

    FETCH NEXT FROM accTable INTO
		    @cuNo,@IntRate;


	END


CLOSE accTable; 
DEALLOCATE accTable;

END




















































GO
