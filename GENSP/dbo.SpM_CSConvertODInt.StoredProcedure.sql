USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[SpM_CSConvertODInt]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SpM_CSConvertODInt]
AS
/*

EXECUTE SpM_CSConvertODInt




*/

BEGIN

	SET NOCOUNT ON;

---=============  DECLARATION ===============
	DECLARE @trnDate SMALLDATETIME;
	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30);
   

    DECLARE @BegYear int;
    DECLARE @EndYear int;
    DECLARE @IYear int;

	DECLARE @nYear INT;	

	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @nCount INT;

    DECLARE @AccTypeCode  INT;
    DECLARE @AccTypeClass INT;
---============= END OF DECLARATION ===============


	    
    SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZCSPARAMETER);
    SET @EndYear = (SELECT FinancialEndYear FROM A2ZCSPARAMETER);

   	
	TRUNCATE TABLE A2ZODINTDUEHST;
--
--    DECLARE accTable CURSOR FOR
--SELECT AccTypeCode,AccTypeClass
--FROM A2ZACCTYPE WHERE AccTypeClass = 5;
--
--OPEN accTable;
--FETCH NEXT FROM accTable INTO
--@AccTypeCode,@AccTypeClass;
--
--WHILE @@FETCH_STATUS = 0 
--	BEGIN



	SET @fYear = @BegYear;
	SET @tYear = @EndYear;

	SET @nCount = @fYear
	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO A2ZODINTDUEHST (TrnDate,CuType,CuNo,MemNo,AccType,AccNo,AccODPaidInt,AccODDisbAmt,AccODDueInt,AccDrCr)' +
					' SELECT ' +
					'TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnCredit,TrnDebit,TrnDueIntAmt,TrnDrCr' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION';
--					' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''';
			

--            SET @strSQL = @strSQL + ' AND AccType = ' + CAST(@AccTypeCode AS VARCHAR(2));         
            SET @strSQL = @strSQL + ' WHERE TrnFlag = 0 AND TrnCSGL = 0 AND (PayType = 351 or PayType = 352)';        
			EXECUTE (@strSQL);


			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 


--   FETCH NEXT FROM accTable INTO
--		@AccTypeCode,@AccTypeClass;
--
--
--	END
--
--CLOSE accTable; 
--DEALLOCATE accTable;



END

GO
