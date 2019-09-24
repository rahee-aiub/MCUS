USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAcc17]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[Sp_CSAcc17]  

AS

BEGIN

/*

EXECUTE Sp_CSAcc17

*/

DECLARE @cuType smallint;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;

DECLARE @accPeriod int;

DECLARE @accNo Bigint;
DECLARE @accBenefitDate smalldatetime;
DECLARE @NewBenefitDate smalldatetime;
DECLARE @OpenDate smalldatetime;
DECLARE @newDate smalldatetime;
DECLARE @tDD int;

DECLARE @memType int;

DECLARE @atyMthBenefit money;
DECLARE @atyRecords money;

DECLARE @accFixedAmt money;
DECLARE @accFixedMthInt money;
DECLARE @NoMth money;
DECLARE @NewFixedMthInt money;


DECLARE accTable CURSOR FOR
SELECT CuType,CuNo,MemNo,AccType,AccNo,AccPeriod,AccFixedAmt,AccFixedMthInt
FROM A2ZACCOUNT WHERE AccType = 17 AND AccBalance > 0 AND AccStatus < 97;

OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accPeriod,@accFixedAmt,@accFixedMthInt;

WHILE @@FETCH_STATUS = 0 
	BEGIN

        SET @NewFixedMthInt = @accFixedMthInt;

        IF @accFixedMthInt = 0
           BEGIN

               SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    
               SET @atyMthBenefit = (SELECT AtyMaturedAmt FROM A2ZATYSLAB WHERE AtyAccType = 17 AND
						AtyFlag = @memType AND AtyPeriod = @accPeriod);

               SET @atyRecords = (SELECT AtyRecords FROM A2ZATYSLAB WHERE AtyAccType = 17 AND
						AtyFlag = @memType AND AtyPeriod = @accPeriod);

               SET @NoMth = (@accFixedAmt / @atyRecords);

               SET @NewFixedMthInt = (@atyMthBenefit * @NoMth);

            END

        SET @accBenefitDate = (SELECT AccBenefitDate FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType=@accType AND AccNo=@accNo);

        SET @OpenDate = (SELECT AccOpenDate FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType=@accType AND AccNo=@accNo);

        SET @tDD = DAY(@OpenDate);
        
        SET @newDate = @accBenefitDate; 

		IF @tDD < 29
           BEGIN
               SET @newDate = DATEADD(DAY,-DAY(@accBenefitDate) + DAY(@OpenDate),@accBenefitDate);
           END
        
        SET @newBenefitDate = (DATEADD(MONTH,1,@newDate))

--        UPDATE A2ZACCOUNT SET AccRenwlDate = (DATEADD(year,-AccPeriod,AccMatureDate)) WHERE CuType=@cuType AND CuNo=@cuNo AND AccType=@accType AND AccNo=@accNo;
--
----        UPDATE A2ZACCOUNT SET AccRenwlDate = NULL WHERE CuType=@cuType AND CuNo=@cuNo AND AccType=@accType AND AccNo=@accNo AND MONTH(AccOpenDate) = MONTH(AccRenwlDate) AND YEAR(AccOpenDate) = YEAR(AccRenwlDate);
--
--        UPDATE A2ZACCOUNT SET AccNoBenefit = ((DATEDIFF(MONTH,AccRenwlDate,AccBenefitDate))) WHERE CuType=@cuType AND CuNo=@cuNo AND AccType=@accType AND AccNo=@accNo AND AccRenwlDate is not NULL;
--
--        UPDATE A2ZACCOUNT SET AccNoBenefit = ((DATEDIFF(MONTH,AccOpenDate,AccBenefitDate))) WHERE CuType=@cuType AND CuNo=@cuNo AND AccType=@accType AND AccNo=@accNo AND AccRenwlDate is NULL;
--
        UPDATE A2ZACCOUNT SET AccPeriod = (AccPeriod * 12) WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo = @memNo AND AccType=@accType AND AccNo=@accNo;
--        
--        UPDATE A2ZACCOUNT SET AccTotIntWdrawn = (AccFixedMthInt * AccNoBenefit) WHERE CuType=@cuType AND CuNo=@cuNo AND AccType=@accType AND AccNo=@accNo;
   
        UPDATE A2ZACCOUNT SET AccBenefitDate = @newBenefitDate WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo = @memNo AND AccType=@accType AND AccNo=@accNo;

        UPDATE A2ZACCOUNT SET AccFixedMthInt = @NewFixedMthInt WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo = @memNo AND AccType=@accType AND AccNo=@accNo;
 
      FETCH NEXT FROM accTable INTO
	       @cuType,@cuNo,@memNo,@accType,@accNo,@accPeriod,@accFixedAmt,@accFixedMthInt;	    
       
	END


CLOSE accTable; 
DEALLOCATE accTable;

END






























GO
