USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCalculateInterestAmount]    Script Date: 06/19/2017 22:46:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_CSCalculateInterestAmount] (@accType INT,@nFlag INT,
@IntRate1 smallmoney,@IntRate2 smallmoney,@IntRate3 smallmoney,@IntRate4 smallmoney,
@IntRate5 smallmoney,@IntRate6 smallmoney)
AS
BEGIN

/*

EXECUTE Sp_CSCalculateInterestAmount 12,0,7,7,7,7,7,7

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
    DECLARE @IntRate smallmoney;	

    DECLARE @trnDate smalldatetime;

    DECLARE @fBegYear INT;	
    DECLARE @fEndYear INT;	
    DECLARE @CMth INT;	
    DECLARE @NoOfMonth INT;
    DECLARE @calPeriod INT;

	SET @nCount = 1;
	

    SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

    SET @fBegYear = (SELECT FinancialBegYear FROM A2ZCSPARAMETER);
    SET @fEndYear = (SELECT FinancialEndYear FROM A2ZCSPARAMETER);
    SET @CMth = (SELECT CurrentMonth FROM A2ZCSPARAMETER);

    PRINT @CMth;

    IF @CMth = 12
       BEGIN
            SET @fDate = CAST(@fBegYear AS VARCHAR(4)) + '-' + '07' + '-' + '01'  
            SET @NoOfMonth = 7;  
            SET @calPeriod = 1;
       END

    IF @CMth = 6
       BEGIN
            SET @fDate = CAST(@fEndYear AS VARCHAR(4)) + '-' + '01' + '-' + '01'  
            SET @NoOfMonth = 7;  
            SET @calPeriod = 2;
       END

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

    print @opDate;
    PRINT @nCount;
    PRINT @NoOfMonth;

	WHILE @nCount < @NoOfMonth
		BEGIN	

           print @opDate;

			EXECUTE Sp_CSGenerateOpeningBalanceAType @accType,@opDate,0;
			
            IF @nCount = 1
               BEGIN
                   SET @IntRate = @IntRate1;
               END

            IF @nCount = 2
               BEGIN
                   SET @IntRate = @IntRate2;
               END

            IF @nCount = 3
               BEGIN
                   SET @IntRate = @IntRate3;
               END

            IF @nCount = 4
               BEGIN
                   SET @IntRate = @IntRate4;
               END

            IF @nCount = 5
               BEGIN
                   SET @IntRate = @IntRate5;
               END

            IF @nCount = 6
               BEGIN
                   SET @IntRate = @IntRate6;
               END

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

--		IF @accType = 12
--			BEGIN
				DELETE FROM WFA2ZTRANSACTION WHERE TrnFlag = 1;
				IF @calPeriod = 1
					BEGIN
						DELETE FROM WFA2ZTRANSACTION WHERE MONTH(TrnDate) BETWEEN 1 AND 6;

						UPDATE WFCSINTEREST SET AmtOpening = AmtJul WHERE AccType = @accType;
					END

				IF @calPeriod = 2
					BEGIN
						DELETE FROM WFA2ZTRANSACTION WHERE MONTH(TrnDate) BETWEEN 7 AND 12;

						UPDATE WFCSINTEREST SET AmtOpening = AmtJan WHERE AccType = @accType;
					END
	       	    
				EXECUTE Sp_CSUpdateMinimumBalance @accType,@calPeriod,0;
--			END
		---=============  End of For Minimum Balance Calculation ==============

        IF @CMth = 12
           BEGIN
	       	    UPDATE WFCSINTEREST SET AmtProduct = (AmtJul + AmtAug + AmtSep + AmtOct + AmtNov + AmtDec +
											AmtJan + AmtFeb + AmtMar + AmtApr + AmtMay + AmtJun),
		        AmtOpening = AmtJul WHERE AccType = @accType;

               UPDATE WFCSINTEREST SET IntAmtJul = ((AmtJul * IntRateJul) / 1200),
                                IntAmtAug = ((AmtAug * IntRateAug) / 1200),
                                IntAmtSep = ((AmtSep * IntRateSep) / 1200),
                                IntAmtOct = ((AmtOct * IntRateOct) / 1200),
                                IntAmtNov = ((AmtNov * IntRateNov) / 1200),
                                IntAmtDec = ((AmtDec * IntRateDec) / 1200)
                                            WHERE AccType = @accType;
		
              UPDATE WFCSINTEREST SET AmtInterest = ROUND((IntAmtJul + IntAmtAug + IntAmtSep + 
              IntAmtOct + IntAmtNov + IntAmtDec),0) WHERE AccType = @accType;
           END


      IF @CMth = 6
           BEGIN
	       	    UPDATE WFCSINTEREST SET AmtProduct = (AmtJan + AmtFeb + AmtMar + AmtApr + AmtMay + AmtJun),
		        AmtOpening = AmtJan WHERE AccType = @accType;

                UPDATE WFCSINTEREST SET IntAmtJan = ((AmtJan * IntRateJan) / 1200),
                                IntAmtFeb = ((AmtFeb * IntRateFeb) / 1200),
                                IntAmtMar = ((AmtMar * IntRateMar) / 1200),
                                IntAmtApr = ((AmtApr * IntRateApr) / 1200),
                                IntAmtMay = ((AmtMay * IntRateMay) / 1200),
                                IntAmtJun = ((AmtJun * IntRateJun) / 1200)
                                            WHERE AccType = @accType;
		
                UPDATE WFCSINTEREST SET AmtInterest = ROUND((IntAmtJan + IntAmtFeb + 
                                      IntAmtMar + IntAmtApr + IntAmtMay + IntAmtJun),0)
                                      WHERE AccType = @accType;
           END


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



