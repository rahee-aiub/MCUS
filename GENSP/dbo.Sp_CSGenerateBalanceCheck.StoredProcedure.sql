USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGenerateBalanceCheck]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_CSGenerateBalanceCheck]@fDate VARCHAR(10)
AS
/*
									
EXECUTE Sp_CSGenerateBalanceCheck '2017-02-28'

*/

BEGIN

DECLARE @TrnCode INT;	
DECLARE @AccType INT;	
DECLARE @AccAtyClass INT;	
DECLARE @LederBalance MONEY;	
DECLARE @JournalBalance MONEY;	
DECLARE @DefferlBalance MONEY;


DECLARE @TrnDes nvarchar(50);

TRUNCATE TABLE A2ZBALANCECHECK;	

EXECUTE A2ZCSMCUS..Sp_CSAccountBalanceCheck @fDate;



EXECUTE A2ZGLMCUS..Sp_GlGenerateLedgerBalance @fDate,@fDate,0,0;


DECLARE tableBalanceCheck CURSOR FOR
	SELECT TrnCode,TrnDes,AccType FROM A2ZTRNCODE WHERE AccType !=99 AND AccType !=63; 
	
	OPEN tableBalanceCheck;
	FETCH NEXT FROM tableBalanceCheck INTO @TrnCode,@TrnDes,@AccType; 
		WHILE @@FETCH_STATUS = 0 
		BEGIN

    SET @AccAtyClass= (SELECT AccTypeClass FROM A2ZACCTYPE WHERE AccTypeCode = @AccType);

    SET @LederBalance = 0;


    SET @LederBalance= (SELECT ISNULL(SUM(AccOpBal),0) FROM A2ZACCOUNT WHERE AccType = @AccType);

    SET @JournalBalance = 0;
    

    SET @JournalBalance= (SELECT GLClBal FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo = @TrnCode);

    SET @DefferlBalance = 0
    SET @DefferlBalance = (ABS(@LederBalance) - ABS(@JournalBalance));

    INSERT INTO A2ZBALANCECHECK
    	(AccAtyClass,AccType,TrnCode,TrnDes,LedgerBalance,JournalBalance,DefferlBalance)	
         VALUES (@AccAtyClass,@AccType,@TrnCode,@TrnDes,ABS(@LederBalance),ABS(@JournalBalance),@DefferlBalance);

        FETCH NEXT FROM tableBalanceCheck INTO @TrnCode,@TrnDes,@AccType; 
		END;
--=============  End of UPDATE Principal and Interest Amount ==========
	CLOSE tableBalanceCheck;
	DEALLOCATE tableBalanceCheck;


END



GO
