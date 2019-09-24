USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateInterestAmountYearly]    Script Date: 06/28/2018 10:54:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_CSCalculateInterestAmountYearly] (@accType INT,@nFlag INT,
@IntRate smallmoney,@fBegYear int)
AS
BEGIN

/*

EXECUTE Sp_CSCalculateInterestAmountYearly 13,0,7,2016,'2016-06-29'

*/

IF ISNULL(@accType,0) = 0
	BEGIN
		RAISERROR ('Account Type is Null',10,1)
		RETURN;
	END
BEGIN TRY
--	BEGIN TRANSACTION
	SET NOCOUNT ON

	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @opDate VARCHAR(10);
    DECLARE @fDate VARCHAR(10);
	DECLARE @nCount INT;	
	
    DECLARE @NoOfMonth INT;
    DECLARE @calPeriod INT;
	DECLARE @trnDate smalldatetime;


	SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

	SET @nCount = 1;
	
    SET @fDate = CAST(@fBegYear AS VARCHAR(4)) + '-' + '07' + '-' + '01'
    SET @NoOfMonth = 13;

    SET @opDate = @fDate;

--	TRUNCATE TABLE WFCSINTEREST;

    DELETE FROM WFCSINTEREST  WHERE AccType = @accType;

	INSERT INTO WFCSINTEREST (TrnDate,AccType,AccNo,CuType,CuNo,CuNumber,MemNo,AccOpenDate,AccStatus,AccIntRate, CalPeriod)
	SELECT @trnDate,AccType,AccNo,CuType,CuNo,LTRIM(STR(CuType) + '-' + LTRIM(STR(CuNo))),MemNo,AccOpenDate,AccStatus,AccIntRate,@calPeriod
	FROM A2ZACCOUNT WHERE AccType = @accType AND AccStatus < 97 AND AccStatus <> 3;
    
    UPDATE WFCSINTEREST SET WFCSINTEREST.MemName = A2ZMEMBER.MemName
    FROM WFCSINTEREST,A2ZMEMBER
    WHERE WFCSINTEREST.CuType = A2ZMEMBER.CuType AND WFCSINTEREST.CuNo = A2ZMEMBER.CuNo AND 
    WFCSINTEREST.MemNo = A2ZMEMBER.MemNo;

	WHILE @nCount < @NoOfMonth
		BEGIN	

			EXECUTE Sp_CSGenerateOpeningBalanceAType @accType,@opDate,0;	

			SET @strSQL = 	
			'UPDATE WFCSINTEREST SET WFCSINTEREST.Amt' + LEFT(DATENAME(MONTH,@opDate),3) + '=' +
			'ISNULL((SELECT AccOpBal FROM A2ZACCOUNT
			WHERE WFCSINTEREST.CuType = A2ZACCOUNT.CuType AND 
			WFCSINTEREST.CuNo = A2ZACCOUNT.CuNo AND 
			WFCSINTEREST.MemNo = A2ZACCOUNT.MemNo AND 
			WFCSINTEREST.AccType = A2ZACCOUNT.AccType AND 
			WFCSINTEREST.AccNo = A2ZACCOUNT.AccNo),0)
			FROM A2ZACCOUNT,WFCSINTEREST
			WHERE WFCSINTEREST.CuType = A2ZACCOUNT.CuType AND 
			WFCSINTEREST.CuNo = A2ZACCOUNT.CuNo AND 
			WFCSINTEREST.MemNo = A2ZACCOUNT.MemNo AND 
			WFCSINTEREST.AccType = A2ZACCOUNT.AccType AND 
			WFCSINTEREST.AccNo = A2ZACCOUNT.AccNo';

			EXECUTE(@strSQL);

            SET @strSQL = 	
            'UPDATE WFCSINTEREST SET WFCSINTEREST.Amt' + LEFT(DATENAME(MONTH,@opDate),3) + 
            '= 0 WHERE WFCSINTEREST.AccType =' + CAST(@accType AS VARCHAR(2)) + ' AND WFCSINTEREST.Amt' + 
            LEFT(DATENAME(MONTH,@opDate),3) + ' < 0';
            EXECUTE(@strSQL);
					
			
            SET @strSQL = 	
            'UPDATE WFCSINTEREST SET WFCSINTEREST.IntRate' + LEFT(DATENAME(MONTH,@opDate),3) + '=' + CAST(@IntRate AS VARCHAR(10)) + 'WHERE WFCSINTEREST.AccType =' + CAST(@accType AS VARCHAR(2));
            EXECUTE(@strSQL);	
			                   
			SET @opDate = CONVERT(VARCHAR(10), DATEADD(MONTH,1,@opDate), 120);
	
			SET @nCount = @nCount + 1;		
		END

		---=============  For Minimum Balance Calculation ==============
		DELETE FROM WFA2ZTRANSACTION WHERE TrnFlag = 1;

		UPDATE WFCSINTEREST SET AmtOpening = AmtJul WHERE AccType = @accType;	       	    

		EXECUTE Sp_CSUpdateMinimumBalance @accType,1,0;

		---=============  End of For Minimum Balance Calculation ==============

   	    UPDATE WFCSINTEREST SET AmtProduct = (AmtJul + AmtAug + AmtSep + AmtOct + AmtNov + AmtDec +
									AmtJan + AmtFeb + AmtMar + AmtApr + AmtMay + AmtJun),
        AmtOpening = AmtJul WHERE AccType = @accType;

		UPDATE WFCSINTEREST SET IntAmtJan = ((AmtJan * IntRateJan) / 1200),
						IntAmtFeb = ((AmtFeb * IntRateFeb) / 1200),
						IntAmtMar = ((AmtMar * IntRateMar) / 1200),
						IntAmtApr = ((AmtApr * IntRateApr) / 1200),
						IntAmtMay = ((AmtMay * IntRateMay) / 1200),
						IntAmtJun = ((AmtJun * IntRateJun) / 1200),
						IntAmtJul = ((AmtJul * IntRateJul) / 1200),
                        IntAmtAug = ((AmtAug * IntRateAug) / 1200),
                        IntAmtSep = ((AmtSep * IntRateSep) / 1200),
                        IntAmtOct = ((AmtOct * IntRateOct) / 1200),
                        IntAmtNov = ((AmtNov * IntRateNov) / 1200),
                        IntAmtDec = ((AmtDec * IntRateDec) / 1200)
                        WHERE AccType = @accType;

            UPDATE WFCSINTEREST SET AmtInterest = ROUND((IntAmtJan + IntAmtFeb + 
                                IntAmtMar + IntAmtApr + IntAmtMay + IntAmtJun + 
								IntAmtJul + IntAmtAug + IntAmtSep + IntAmtOct + 
								IntAmtNov + IntAmtDec),0)
								WHERE AccType = @accType;           


      UPDATE WFCSINTEREST SET WFCSINTEREST.ProcStat = 2 WHERE AccType = @accType;

      DELETE FROM WFCSINTEREST  WHERE AmtInterest = 0 AND AccType = @accType;

--	COMMIT TRANSACTION
	SET NOCOUNT OFF
END TRY

BEGIN CATCH
		--ROLLBACK TRANSACTION

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

