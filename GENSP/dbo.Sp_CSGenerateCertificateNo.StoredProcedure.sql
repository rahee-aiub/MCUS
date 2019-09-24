USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGenerateCertificateNo]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[Sp_CSGenerateCertificateNo](@accType INT,@WithFlag INT)  

AS

/*

EXECUTE Sp_CSGenerateCertificateNo 15,0


*/


BEGIN

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;

DECLARE @accNo Bigint;
DECLARE @accOldNumber nvarchar(50);

DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accOpenDate smalldatetime;
DECLARE @accPeriod int;

DECLARE @memName nvarchar(50);
DECLARE @accCertNo nvarchar(50);


---------- Refresh Workfile ----------
TRUNCATE TABLE WFCSCERTIFICATENO;
---------- End of Refresh Workfile ----------

IF @WithFlag = 0
   BEGIN
       DECLARE accTable CURSOR FOR
          SELECT CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccOrgAmt,AccPeriod,AccCertNo,AccOldNumber
          FROM A2ZACCOUNT WHERE AccType = @accType AND AccStatus < 97 AND (AccCertNo IS NULL OR AccCertNo = '');
   END
ELSE
   BEGIN
      DECLARE accTable CURSOR FOR
         SELECT CuType,CuNo,MemNo,AccType,AccNo,AccOpenDate,AccOrgAmt,AccPeriod,AccCertNo,AccOldNumber
         FROM A2ZACCOUNT WHERE AccType = @accType AND AccStatus < 97;
   END



OPEN accTable;
FETCH NEXT FROM accTable INTO
@cuType,@cuNo,@memNo,@accType,@accNo,@accOpenDate,@accOrgAmt,@accPeriod,@accCertNo,@accOldNumber;

WHILE @@FETCH_STATUS = 0 
	BEGIN

    	
    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);  

-------- Insert Record to Workfile ---------------

	INSERT INTO WFCSCERTIFICATENO
	(CuType,CuNo,MemNo,AccType,AccNo,MemName,AccOpenDate,AccOrgAmt,AccPeriod,AccCertNo,Status,AccOldNumber)
	
    VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@memName,@accOpenDate,@accOrgAmt,@accPeriod,@accCertNo,0,@accOldNumber);

-------- End of Insert Record to Workfile ---------------

    
	FETCH NEXT FROM accTable INTO
         @cuType,@cuNo,@memNo,@accType,@accNo,@accOpenDate,@accOrgAmt,@accPeriod,@accCertNo,@accOldNumber;		

	END

CLOSE accTable; 
DEALLOCATE accTable;

END











































GO
