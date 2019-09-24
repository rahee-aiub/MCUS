USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSGetGuarantorAccountInfo]    Script Date: 03/18/2018 2:48:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Sp_CSGetGuarantorAccountInfo](@accType int,@cuType INT,@cuNo INT,@memNo INT,@FuncOpt INT,@userID INT,@Module INT,@CtrlFlag INT)  


AS
/*

EXECUTE Sp_CSGetGuarantorAccountInfo 3,5,0,1,1,1


*/



BEGIN

DECLARE @Count int;

DECLARE @accTypeClass int;
DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @trnDesc nvarchar(50);
DECLARE @accOldNumber nvarchar(50);
DECLARE @accBalance money;


DECLARE @trnDate smalldatetime;
DECLARE @ProcDate VARCHAR(10);


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))



DELETE FROM WFCSGROUPACCOUNT WHERE UserId = @userID;

 BEGIN

     SET @trnCode = (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = @accType);
     SET @trnDesc = (SELECT Trndes FROM A2ZTRNCODE WHERE AccType = @accType);

     DECLARE accTable CURSOR FOR
     SELECT AccNo,AccBalance,AccOldNumber FROM A2ZACCOUNT WHERE CuType=@cuType AND 
                                         CuNo=@cuNo AND 
                                         MemNo=@memNo AND 
                                         AccType = @accType AND
                                         AccStatus < 98;
  END

         OPEN accTable;
         FETCH NEXT FROM accTable INTO @accNo,@accBalance,@accOldNumber;

         WHILE @@FETCH_STATUS = 0 
	     BEGIN
             
               --EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;

               SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);

               INSERT INTO WFCSGROUPACCOUNT (TrnCode,TrnCodeDesc,AccType,AccNo,AccBalance,AccOldNumber,UserId)
               VALUES (@trnCode,@trnDesc,@accType, @accNo,@accBalance,@accOldNumber,@userID);


                  FETCH NEXT FROM accTable INTO @accNo,@accBalance,@accOldNumber;
         END
         CLOSE accTable; 
         DEALLOCATE accTable;

END

GO

