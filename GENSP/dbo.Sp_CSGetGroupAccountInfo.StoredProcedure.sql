USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetGroupAccountInfo]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSGetGroupAccountInfo](@cuType INT,@cuNo INT,@memNo INT,@FuncOpt INT,@userID INT,@Module INT,@CtrlFlag INT)  


AS
/*

EXECUTE Sp_CSGetGroupAccountInfo 3,5,0,1,1,1


*/



BEGIN

DECLARE @Count int;
DECLARE @accType int;
DECLARE @accTypeClass int;
DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @trnDesc nvarchar(50);
DECLARE @accOldNumber nvarchar(50);
DECLARE @accBalance money;
DECLARE @accLoanInstlAmt money;
DECLARE @accOrgAmt money;

DECLARE @accStatus int;
DECLARE @accTrfAccNo Bigint;


DECLARE @trnDate smalldatetime;
DECLARE @ProcDate VARCHAR(10);


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))



DELETE FROM WFCSGROUPACCOUNT WHERE UserId = @userID;


IF @Module = 4 AND @FuncOpt > 0
   BEGIN
       DECLARE ctrlTable CURSOR FOR
             SELECT TrnCode,TrnDesc,AccType,AccTypeClass FROM A2ZTRNCTRL 
                      WHERE FuncOpt = @FuncOpt AND AccTypeMode !=1 AND AccType !=99;
   END
ELSE
IF @Module = 4 AND @FuncOpt = 0
   BEGIN
       DECLARE ctrlTable CURSOR FOR
             SELECT TrnCode,TrnDesc,AccType,AccTypeClass FROM A2ZTRNCTRL 
                      WHERE AccTypeMode !=1 AND AccType !=99;
   END
ELSE
IF @FuncOpt > 0
   BEGIN
      DECLARE ctrlTable CURSOR FOR
             SELECT TrnCode,TrnDesc,AccType,AccTypeClass FROM A2ZTRNCTRL 
                    WHERE FuncOpt = @FuncOpt AND AccTypeMode !=2 AND AccType !=99;
   END
ELSE
IF @FuncOpt = 0
   BEGIN
      DECLARE ctrlTable CURSOR FOR
             SELECT TrnCode,TrnDesc,AccType,AccTypeClass FROM A2ZTRNCTRL 
                    WHERE AccTypeMode !=2 AND AccType !=99;
   END

OPEN ctrlTable;
FETCH NEXT FROM ctrlTable INTO @trnCode,@trnDesc,@accType,@accTypeClass;

WHILE @@FETCH_STATUS = 0 
	BEGIN
         
         IF (@FuncOpt = 1 AND (@accTypeClass = 2 OR @accTypeClass = 3))
            BEGIN
                 DECLARE accTable CURSOR FOR
                 SELECT AccNo,AccBalance,AccLoanInstlAmt,AccOrgAmt,AccOldNumber,AccStatus,AccTrfAccNo FROM A2ZACCOUNT 
                 WHERE CuType=@cuType AND 
                                         CuNo=@cuNo AND 
                                         MemNo=@memNo AND 
                                         AccType=@accType AND 
                                         AccStatus != 99 AND AccBalance=0;
--                                         AccStatus = 1 AND AccBalance=0;
            END
         
         ELSE
         IF (@FuncOpt = 0 AND @CtrlFlag = 1)
             BEGIN         
                DECLARE accTable CURSOR FOR
                SELECT AccNo,AccBalance,AccLoanInstlAmt,AccOrgAmt,AccOldNumber,AccStatus,AccTrfAccNo FROM A2ZACCOUNT 
                WHERE CuType=@cuType AND 
                                 CuNo=@cuNo AND 
                                 MemNo=@memNo AND 
                                 AccType=@accType; 
--                                 AccStatus !=99;
--                                 AccStatus !=98;
            END
             
         ELSE
            BEGIN         
                DECLARE accTable CURSOR FOR
                SELECT AccNo,AccBalance,AccLoanInstlAmt,AccOrgAmt,AccOldNumber,AccStatus,AccTrfAccNo FROM A2ZACCOUNT 
                WHERE CuType=@cuType AND 
                                 CuNo=@cuNo AND 
                                 MemNo=@memNo AND 
                                 AccType=@accType AND 
                                 AccStatus !=99;
--                                 AccStatus < 98;
            END

         OPEN accTable;
         FETCH NEXT FROM accTable INTO @accNo,@accBalance,@accLoanInstlAmt,@accOrgAmt,@accOldNumber,@accStatus,@accTrfAccNo;

         WHILE @@FETCH_STATUS = 0 
	     BEGIN
                      
--               EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;
--
--               SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);


               IF @accTypeClass = 6
                  BEGIN             
                      INSERT INTO WFCSGROUPACCOUNT (TrnCode,TrnCodeDesc,AccType,AccNo,AccBalance,AccOrgInstlAmt,AccOldNumber,AccStatus,AccTrfAccNo,UserId)
                      VALUES (@trnCode,@trnDesc,@accType, @accNo,@accBalance,@accLoanInstlAmt,@accOldNumber,@accStatus,@accTrfAccNo,@userID);
                  END

               IF @accTypeClass = 2 OR @accTypeClass = 3 
                  BEGIN             
                      INSERT INTO WFCSGROUPACCOUNT (TrnCode,TrnCodeDesc,AccType,AccNo,AccBalance,AccOrgInstlAmt,AccOldNumber,AccStatus,AccTrfAccNo,UserId)
                      VALUES (@trnCode,@trnDesc,@accType, @accNo,@accBalance,@accOrgAmt,@accOldNumber,@accStatus,@accTrfAccNo,@userID);
                  END

               IF @accTypeClass != 2 AND @accTypeClass != 3 AND @accTypeClass != 6 
                  BEGIN             
                      INSERT INTO WFCSGROUPACCOUNT (TrnCode,TrnCodeDesc,AccType,AccNo,AccBalance,AccOrgInstlAmt,AccOldNumber,AccStatus,AccTrfAccNo,UserId)
                      VALUES (@trnCode,@trnDesc,@accType, @accNo,@accBalance,0,@accOldNumber,@accStatus,@accTrfAccNo,@userID);
                  END
    
              FETCH NEXT FROM accTable INTO @accNo,@accBalance,@accLoanInstlAmt,@accOrgAmt,@accOldNumber,@accStatus,@accTrfAccNo;
                  END
                  CLOSE accTable; 
                  DEALLOCATE accTable;
         

	FETCH NEXT FROM ctrlTable INTO @trnCode,@trnDesc,@accType,@accTypeClass;
		
	END

CLOSE ctrlTable; 
DEALLOCATE ctrlTable;

IF (@FuncOpt = 0 OR @FuncOpt = 1 OR @FuncOpt = 3 OR @FuncOpt = 11 OR @FuncOpt = 12)
    BEGIN         
         DECLARE acc1Table CURSOR FOR
         SELECT AccNo,AccBalance,AccOldNumber FROM A2ZACCOUNT WHERE CuType=@cuType AND 
                CuNo=@cuNo AND 
                MemNo=@memNo AND 
                AccType=99 AND 
                AccStatus < 98;
   

         OPEN acc1Table;
         FETCH NEXT FROM acc1Table INTO @accNo,@accBalance,@accOldNumber;

         WHILE @@FETCH_STATUS = 0 
	     BEGIN
               
--               EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;
--
--               SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);

               SET @Count = RIGHT(@accNo,3);
                             
               SET @trnDesc = (SELECT PayTypeDes FROM A2ZPAYTYPE WHERE PayType = @Count);

               
               
               INSERT INTO WFCSGROUPACCOUNT (TrnCode,TrnCodeDesc,AccType,AccNo,AccBalance,AccOldNumber,UserId)
               VALUES (@trnCode,@trnDesc,99, @accNo,@accBalance,@accOldNumber,@userID);
                  

                  FETCH NEXT FROM acc1Table INTO @accNo,@accBalance,@accOldNumber;
                  END
                  CLOSE acc1Table; 
                  DEALLOCATE acc1Table;

             IF @FuncOpt = 3
                 BEGIN
                     DELETE FROM WFCSGROUPACCOUNT WHERE UserId = @userID AND  RIGHT(AccNo,3) IN (507,510) -- 507 = Loan Processing Fees, 510 = CPS Closing Fee
                 END


END




END

GO
