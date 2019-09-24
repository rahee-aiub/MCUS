USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSAbnormalTransaction]    Script Date: 09/04/2016 13:41:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_CSAbnormalTransaction](@userID INT)  

---ALTER PROCEDURE [dbo].[Sp_CSAbnormalTransaction]  
AS

BEGIN

--DECLARE @userID INT;

DECLARE @TrnDate smalldatetime;
DECLARE @VchNo nvarchar(20);
DECLARE @CuType int;
DECLARE @CuNo int;
DECLARE @MemNo int;
DECLARE @AccType int;
DECLARE @AccNo Bigint;
DECLARE @trnCode int;
DECLARE @accProvBalance money;

DECLARE @CType int;
DECLARE @CNo int;
DECLARE @MNo int;
DECLARE @AType int;
DECLARE @ANo Bigint;

TRUNCATE TABLE WFABNTRANSACTION;
---------- End of Refresh Workfile ----------

DECLARE trnTable CURSOR FOR
SELECT TrnDate,VchNo,CuType,CuNo,MemNo,AccType,AccNo
FROM A2ZTRANSACTION;

OPEN trnTable;
FETCH NEXT FROM trnTable INTO
@TrnDate,@VchNo,@CuType,@CuNo,@MemNo,@AccType,@AccNo;

WHILE @@FETCH_STATUS = 0 
	BEGIN

          DECLARE accTable CURSOR FOR
		  SELECT CuType,CuNo,MemNo,AccType,AccNo
		  FROM A2ZACCOUNT WHERE CuType !=@CuType AND CuNo!=@CuNo AND MemNo!=@MemNo AND AccType!=@AccType AND AccNo!=@AccNo;

		  OPEN accTable;
		  FETCH NEXT FROM accTable INTO
		  @CType,@CNo,@MNo,@AType,@ANo;

		  WHILE @@FETCH_STATUS = 0 
		  BEGIN

				INSERT INTO WFABNTRANSACTION
					(CuType,CuNo,MemNo,AccType,AccNo)
				VALUES (@cuType,@cuNo,@memNo,@accType,@accNo);


		  FETCH NEXT FROM accTable INTO
           @CType,@CNo,@MNo,@AType,@ANo;
		

	      END

		  CLOSE accTable; 
		  DEALLOCATE accTable;



	FETCH NEXT FROM trnTable INTO
        @TrnDate,@VchNo,@CuType,@CuNo,@MemNo,@AccType,@AccNo;
		

	END

CLOSE trnTable; 
DEALLOCATE trnTable;


END

----exec Sp_CSCalculateAnniversary6YR






































