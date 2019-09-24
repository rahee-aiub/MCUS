USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSRebuildFDRMatureDate]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_CSRebuildFDRMatureDate] (@accNo bigint)

AS
BEGIN



DECLARE @cuType smallint;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType smallint;


DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @fdAmount money;
DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accPrincipal money;
DECLARE @accRenwlAmt money;
DECLARE @accPeriod smallint;

DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accNoRenwl smallint;

DECLARE @newRenwlDate smalldatetime;
DECLARE @newNoRenwl smallint;
DECLARE @newRenwlAmt money;


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

DECLARE @LaccMatureDate smalldatetime;
DECLARE @LaccRenwlDate smalldatetime;
DECLARE @LaccNoRenwl smallint;

DECLARE @LaccAnniDate smalldatetime;
DECLARE @LaccNoAnni smallint;
DECLARE @LaccAnniAmt money;
DECLARE @LaccRenwlAmt money;

-----------------------------end--------------------------------

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);


DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccBalance,AccOrgAmt,AccPrincipal,AccOpenDate,AccRenwlDate,AccRenwlAmt,AccPeriod,AccMatureDate,AccNoRenwl,AccAnniDate,AccNoAnni,AccAnniAmt
FROM A2ZACCOUNT WHERE AccNo = @accNo AND AccBalance > 0 AND AccStatus < 97;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOrgAmt,@accPrincipal,@accOpenDate,@accRenwlDate,@accRenwlAmt,@accPeriod,@accMatureDate,@accNoRenwl,@accAnniDate,@accNoAnni,@accAnniAmt;

WHILE @@FETCH_STATUS = 0 
	BEGIN

        SET @accAnniDate = NULL;
        SET @accAnniAmt = 0;
        SET @accNoAnni = 0; 

        IF @accRenwlDate IS NULL
           BEGIN
                SET @dummyRDate = @accOpenDate;  
                SET @accRenwlDate = NULL;
                SET @accRenwlAmt = 0;
                SET @accNoRenwl = 0;       
           END
        ELSE
           BEGIN
                SET @dummyRDate = @accRenwlDate;     
           END
         

        SET @accMatureDate = (DATEADD(month,@accPeriod,@dummyRDate));   
        SET @LaccMatureDate = (DATEADD(month,@accPeriod,@dummyRDate));   


        WHILE @trnDate > @accMatureDate
              BEGIN
                SET @LaccRenwlDate = @accRenwlDate;
                SET @accRenwlDate = @accMatureDate;
                SET @dummyRDate = @accRenwlDate;
                SET @LaccNoRenwl = @accNoRenwl;
                SET @accNoRenwl = (@accNoRenwl + 1);
                SET @LaccMatureDate = @accMatureDate;
	           	SET @accMatureDate = DATEADD(MONTH,@accPeriod,@accRenwlDate)
	          END
        
        SET @noMonths = ((DATEDIFF(m, @dummyRDate, @trnDate)) + 0);  
        IF @noMonths >= 12
           BEGIN
                SET @countR = (@noMonths / 12);
                SET @countM = (@countR * 12);
                SET @LaccAnniDate = @accAnniDate;
                SET @accAnniDate = (DATEADD(month,@countM,@dummyRDate))
                SET @LaccNoAnni = @accNoAnni;  
                SET @accNoAnni = @countR;  
           END
        ELSE
           BEGIN
                SET @accAnniDate = NULL;
                SET @accAnniAmt = 0;
                SET @accNoAnni = 0;  
           END  


        IF @accPrincipal > @accBalance
           BEGIN
               SET @accPrincipal = @accBalance;  
           END  

       IF @LaccNoRenwl > 0
          BEGIN
               SET  @LaccRenwlAmt = @accBalance;
          END
       ELSE
          BEGIN
               SET @LaccRenwlAmt = 0;
          END

         
                   
      UPDATE A2ZACCOUNT SET AccMatureDate=@LaccMatureDate,AccRenwlDate=@LaccRenwlDate,AccNoRenwl=@LaccNoRenwl,AccRenwlAmt =@LaccRenwlAmt,AccAnniDate=@LaccAnniDate,AccNoAnni=@LaccNoAnni,AccPrincipal=@accPrincipal    
             WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo; 

    
      FETCH NEXT FROM accTable INTO
            @cuType,@cuNo,@memNo,@accType,@accNo,@accBalance,@accOrgAmt,@accPrincipal,@accOpenDate,@accRenwlDate,@accRenwlAmt,@accPeriod,@accMatureDate,@accNoRenwl,@accAnniDate,@accNoAnni,@accAnniAmt;
		   


	END


CLOSE accTable; 
DEALLOCATE accTable;

END




GO
