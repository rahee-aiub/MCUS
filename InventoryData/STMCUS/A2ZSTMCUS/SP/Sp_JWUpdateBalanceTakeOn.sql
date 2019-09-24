USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_JWUpdateBalanceTakeOn]    Script Date: 6/23/2018 10:05:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Sp_JWUpdateBalanceTakeOn](@KeyNo int,@AmtBalance money,@G22Balance money,@G21Balance money,@G18Balance money,@G16Balance money) 
AS

BEGIN


/*
EXECUTE Sp_JWUpdateBalanceTakeOn 1,1,'B4/2017','B4/2017',10000001,2,1,0,0,0,0,0,0,0,0



*/


DECLARE @PayType int;
DECLARE @trnDesc varchar(50);
DECLARE @trnDate smalldatetime;
DECLARE @CreateDate datetime;

DECLARE @FOpt int;
DECLARE @FOptDesc varchar(50);
DECLARE @TaxRate smallmoney;

DECLARE @KeyNumber int;

DECLARE @WriteFlag int;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

-------- Insert Record to Workfile ---------------

 SET @WriteFlag = 1;


 SET @KeyNumber = (SELECT KeyNo FROM A2ZJWGOLD2018..A2ZJWOPBALANCE WHERE KeyNo= @KeyNo);
	


 IF @KeyNumber IS NULL 
    BEGIN
	     SET @WriteFlag = 0;
	END

 IF @WriteFlag = 0
    BEGIN
	     INSERT INTO A2ZJWGOLD2018..A2ZJWOPBALANCE (KeyNo,TrnAmount,TrnWeight22,TrnWeight21,TrnWeight18,TrnWeight16) 
		 VALUES (@KeyNo,@AmtBalance,@G22Balance,@G21Balance,@G18Balance,@G16Balance);       
	END

 IF @WriteFlag = 1
    BEGIN
	      UPDATE A2ZJWGOLD2018..A2ZJWOPBALANCE SET TrnAmount = @AmtBalance,TrnWeight22 = @G22Balance,TrnWeight21 = @G21Balance,TrnWeight18 = @G18Balance,TrnWeight16 = @G16Balance  WHERE KeyNo = @KeyNo;  
	END



COMMIT TRANSACTION
		SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH

END





























































































GO

