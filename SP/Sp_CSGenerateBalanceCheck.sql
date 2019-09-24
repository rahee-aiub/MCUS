
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSGenerateBalanceCheck]@fDate VARCHAR(10)
AS
/*
									
EXECUTE Sp_CSGenerateBalanceCheck '2015-10-01'

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

DECLARE tableBalanceCheck CURSOR FOR
	SELECT TrnCode,TrnDes,AccType FROM A2ZTRNCODE WHERE AccType !=99; 
	
	OPEN tableBalanceCheck;
	FETCH NEXT FROM tableBalanceCheck INTO @TrnCode,@TrnDes,@AccType; 
		WHILE @@FETCH_STATUS = 0 
		BEGIN

    SET @AccAtyClass= (SELECT AccTypeClass FROM A2ZACCTYPE WHERE AccTypeCode = @AccType);

    SET @LederBalance = 0;

    EXECUTE A2ZCSMCUS..Sp_CSAccountLedgerBalance @AccType,@fDate;

    SET @LederBalance= (SELECT ISNULL(SUM(AccOpBal),0) FROM A2ZACCOUNT WHERE AccType = @AccType);

    EXECUTE A2ZGLMCUS..Sp_GlGenerateAccountBalance @fDate,@fDate,0;

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

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

