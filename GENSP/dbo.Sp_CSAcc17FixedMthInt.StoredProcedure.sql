USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAcc17FixedMthInt]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_CSAcc17FixedMthInt]  

AS

BEGIN

/*

EXECUTE Sp_CSAcc17FixedMthInt

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

        IF @accFixedMthInt = 0 or @accFixedMthInt is null 
           BEGIN

               SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    
               SET @atyMthBenefit = (SELECT AtyMaturedAmt FROM A2ZATYSLAB WHERE AtyAccType = 17 AND
						AtyFlag = @memType AND AtyPeriod = @accPeriod);

               SET @atyRecords = (SELECT AtyRecords FROM A2ZATYSLAB WHERE AtyAccType = 17 AND
						AtyFlag = @memType AND AtyPeriod = @accPeriod);

               SET @NoMth = (@accFixedAmt / @atyRecords);

               SET @NewFixedMthInt = (@atyMthBenefit * @NoMth);

               UPDATE A2ZACCOUNT SET AccFixedMthInt = @NewFixedMthInt WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo = @memNo AND AccType=@accType AND AccNo=@accNo;

            END      
 
      FETCH NEXT FROM accTable INTO
	       @cuType,@cuNo,@memNo,@accType,@accNo,@accPeriod,@accFixedAmt,@accFixedMthInt;	    
       
	END


CLOSE accTable; 
DEALLOCATE accTable;

END
































GO
