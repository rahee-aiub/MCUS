
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSGenerateGroupAccount](@cuType INT,@cuNo INT,@memNo INT,@FuncOpt INT,@userID INT,@Module INT)  


AS
/*

EXECUTE Sp_CSGenerateGroupAccount 3,5,0,1,1,01


*/



BEGIN

DECLARE @accType int;
DECLARE @accTypeClass int;
DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @trnDesc nvarchar(50);
DECLARE @accBalance money;

DECLARE @trnDate smalldatetime;
DECLARE @ProcDate VARCHAR(10);


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))




DELETE FROM WFCSGROUPACCOUNT WHERE UserId = @userID;


IF @Module = 4
   BEGIN
       DECLARE ctrlTable CURSOR FOR
             SELECT TrnCode,TrnDesc,AccType,AccTypeClass FROM A2ZTRNCTRL 
                      WHERE FuncOpt = @FuncOpt AND AccTypeMode !=1;
   END
ELSE
   BEGIN
      DECLARE ctrlTable CURSOR FOR
             SELECT TrnCode,TrnDesc,AccType,AccTypeClass FROM A2ZTRNCTRL 
                    WHERE FuncOpt = @FuncOpt AND AccTypeMode !=2;
   END

OPEN ctrlTable;
FETCH NEXT FROM ctrlTable INTO @trnCode,@trnDesc,@accType,@accTypeClass;

WHILE @@FETCH_STATUS = 0 
	BEGIN
         
         IF (@FuncOpt = 1 AND (@accTypeClass = 2 OR @accTypeClass = 3))
            BEGIN
                 DECLARE accTable CURSOR FOR
                 SELECT AccNo,AccBalance FROM A2ZACCOUNT WHERE CuType=@cuType AND 
                                         CuNo=@cuNo AND 
                                         MemNo=@memNo AND 
                                         AccType=@accType AND 
                                         AccStatus = 1 AND AccBalance=0;
            END
         
         ELSE
            BEGIN         
                DECLARE accTable CURSOR FOR
                SELECT AccNo,AccBalance FROM A2ZACCOUNT WHERE CuType=@cuType AND 
                                 CuNo=@cuNo AND 
                                 MemNo=@memNo AND 
                                 AccType=@accType AND 
                                 AccStatus < 98;
            END

         OPEN accTable;
         FETCH NEXT FROM accTable INTO @accNo,@accBalance;

         WHILE @@FETCH_STATUS = 0 
	     BEGIN
                   
               EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;
               SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);

               INSERT INTO WFCSGROUPACCOUNT (TrnCode,TrnCodeDesc,AccType,AccNo,AccBalance,UserId)
               VALUES (@trnCode,@trnDesc,@accType,@accNo,@accBalance,@userID);


                  FETCH NEXT FROM accTable INTO @accNo,@accBalance;
                  END
                  CLOSE accTable; 
                  DEALLOCATE accTable;
         

	FETCH NEXT FROM ctrlTable INTO @trnCode,@trnDesc,@accType,@accTypeClass;
		

	END

CLOSE ctrlTable; 
DEALLOCATE ctrlTable;


END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

