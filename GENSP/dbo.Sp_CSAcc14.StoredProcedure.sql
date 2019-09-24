USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAcc14]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO












CREATE PROCEDURE [dbo].[Sp_CSAcc14] 

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
DECLARE @accTotalDep money;
DECLARE @accRenwlAmt money;
DECLARE @accPeriod int;
DECLARE @accMonthlyDeposit money;

DECLARE @accLastTrnDateU smalldatetime;
DECLARE @accOpenDate smalldatetime;


DECLARE @accRenwlDate smalldatetime;
DECLARE @accNoRenwl smallint;
DECLARE @accAnniDate smalldatetime;
DECLARE @newRenwlDate smalldatetime;
DECLARE @newNoRenwl smallint;
DECLARE @newRenwlAmt money;

DECLARE @accMatureDate smalldatetime;
DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;
DECLARE @noMonths int;
DECLARE @countR int;

DECLARE @RESULT MONEY
DECLARE @DECVALUE MONEY;
-----------------------------end--------------------------------


DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccPeriod,AccBalance,AccMonthlyDeposit,AccTotalDep,AccLastTrnDateU,AccMatureDate
FROM A2ZACCOUNT WHERE AccType = 14 AND AccBalance > 0 AND AccStatus < 97;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accOpenDate,@accPeriod,@accBalance,@accMonthlyDeposit,@accTotalDep,@accLastTrnDateU,@accMatureDate;

WHILE @@FETCH_STATUS = 0 
	BEGIN
       
--    SET @fDate = @accOpenDate
--    SET @tDate = @accLastTrnDateU;
--
--    SET @noMonths = ((DATEDIFF(m, @fDate, @tDate)) + 1); 
--    IF  @noMonths > @accPeriod
--        BEGIN
--            SET @noMonths = @accPeriod;
--        END
--
--	SET @DECVALUE = 0;
--	IF @accMonthlyDeposit > 0 
--		BEGIN
--		    SET @RESULT = (@accTotalDep / @accMonthlyDeposit);
--			SET @DECVALUE = @RESULT - ROUND(@RESULT,0)
--		END
--
--    IF @DECVALUE <> 0 
--		BEGIN
--			SET @accTotalDep = (@noMonths * @accMonthlyDeposit); 
--		END    

    IF @accMatureDate IS NULL
       BEGIN
           SET @accMatureDate = (DATEADD(month,@accPeriod,@accOpenDate));   
       END


    UPDATE A2ZACCOUNT SET AccMatureDate=@accMatureDate
    WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo; 

              
      
    
      FETCH NEXT FROM accTable INTO
		    @cuType,@cuNo,@memNo,@accType,@accNo,@accOpenDate,@accPeriod,@accBalance,@accMonthlyDeposit,@accTotalDep,@accLastTrnDateU,@accMatureDate;


	END


CLOSE accTable; 
DEALLOCATE accTable;

END






















































GO
