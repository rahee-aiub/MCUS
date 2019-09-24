USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateDividendAmount]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_CSCalculateDividendAmount] (@accType INT,@fDate VARCHAR(10),@NoOfMonth INT,@IntRate smallmoney,@nFlag INT)
AS
BEGIN

/*

EXECUTE Sp_CSCalculateDividendAmount 11,'2016-07-01',12,4,0

*/

IF ISNULL(@accType,0) = 0
	BEGIN
		RAISERROR ('Account Type is Null',10,1)
		RETURN;
	END
BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @opDate VARCHAR(10);
	DECLARE @nCount INT;	
    DECLARE @trnDate smalldatetime;

	SET @nCount = 1;
	SET @opDate = @fDate;

    SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

	TRUNCATE TABLE WFCSSHAREINT;

	INSERT INTO WFCSSHAREINT (TrnDate,AccType,AccNo,CuType,CuNo,CuNumber,MemNo,AccOpenDate,AccStatus,AccIntRate)
	SELECT @trnDate,AccType,AccNo,CuType,CuNo,LTRIM(STR(CuType) + '-' + LTRIM(STR(CuNo))),MemNo,AccOpenDate,AccStatus,@IntRate
	FROM A2ZACCOUNT WHERE AccType = @accType;

	WHILE @nCount <= @NoOfMonth
		BEGIN	
         
            print @opDate;

			EXECUTE Sp_CSGenerateOpeningBalanceAType @accType,@opDate,0;
			
			DELETE FROM WFA2ZTRANSACTION WHERE TrnFlag = 1;

			SET @strSQL = 	
			'UPDATE WFCSSHAREINT SET WFCSSHAREINT.Amt' + LEFT(DATENAME(MONTH,@opDate),3) + '=' +
			'ISNULL((SELECT AccOpBal FROM A2ZACCOUNT
			WHERE WFCSSHAREINT.CuType = A2ZACCOUNT.CuType AND 
			WFCSSHAREINT.CuNo = A2ZACCOUNT.CuNo AND 
			WFCSSHAREINT.MemNo = A2ZACCOUNT.MemNo AND 
			WFCSSHAREINT.AccType = A2ZACCOUNT.AccType AND 
			WFCSSHAREINT.AccNo = A2ZACCOUNT.AccNo),0)
			FROM A2ZACCOUNT,WFCSSHAREINT
			WHERE WFCSSHAREINT.CuType = A2ZACCOUNT.CuType AND 
			WFCSSHAREINT.CuNo = A2ZACCOUNT.CuNo AND 
			WFCSSHAREINT.MemNo = A2ZACCOUNT.MemNo AND 
			WFCSSHAREINT.AccType = A2ZACCOUNT.AccType AND 
			WFCSSHAREINT.AccNo = A2ZACCOUNT.AccNo';

			EXECUTE(@strSQL);			

			SET @opDate = CONVERT(VARCHAR(10), DATEADD(MONTH,1,@opDate), 120);
	
			SET @nCount = @nCount + 1;		
		END

		UPDATE WFCSSHAREINT SET AmtProduct = (AmtJul + AmtAug + AmtSep + AmtOct + AmtNov + AmtDec +
											AmtJan + AmtFeb + AmtMar + AmtApr + AmtMay + AmtJun),
		AmtOpening = AmtJul;

		UPDATE WFCSSHAREINT SET AmtInterest = ROUND(((AmtProduct * @IntRate) / 1200),0);
       
        UPDATE WFCSSHAREINT SET WFCSSHAREINT.ProcStat = 2 WHERE AccType = @accType;

		---===========  ZERO AmtInterest IF Yearly Debosit < 2400 ==========

		DECLARE @CuType INT;
		DECLARE @CuNo INT;
		DECLARE @TotCredit MONEY;

		DECLARE wfTrnTable CURSOR FOR
		SELECT CuType,CuNo,SUM(TrnCredit) AS TotCredit FROM WFA2ZTRANSACTION WHERE AccType = @accType
		GROUP BY CuType,CuNo HAVING SUM(TrnCredit) < 2400;

		OPEN wfTrnTable;
		FETCH NEXT FROM wfTrnTable INTO @CuType,@CuNo,@TotCredit;

		WHILE @@FETCH_STATUS = 0 
			BEGIN
				UPDATE WFCSSHAREINT SET AmtInterest = 0 
				WHERE AccType = @accType AND CuType = @CuType AND CuNo = @CuNo;

				FETCH NEXT FROM wfTrnTable INTO @CuType,@CuNo,@TotCredit;
			END

		CLOSE wfTrnTable; 
		DEALLOCATE wfTrnTable;
		
		---===========  End of ZERO AmtInterest IF Yearly Debosit < 2400 ==========

        DELETE FROM WFCSSHAREINT  WHERE AmtInterest = 0 AND AccType = @accType;


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
