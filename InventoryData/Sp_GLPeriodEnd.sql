USE [A2ZGLMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_GLPeriodEnd]    Script Date: 06/23/2018 11:32:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_GLPeriodEnd] (@approvBy int, @periodFlag int)
AS
BEGIN

DECLARE @trnDate smalldatetime;
DECLARE @WtrnDate smalldatetime;

DECLARE @HolDate smalldatetime;
DECLARE @nCount int;
DECLARE @dayName VARCHAR(9);

DECLARE @WeekDay1 VARCHAR(9);
DECLARE @WeekDay2 VARCHAR(9);

DECLARE @PrevDate smalldatetime;
DECLARE @currentDate smalldatetime;
DECLARE @lastDay int;

DECLARE @CalDate VARCHAR(10);

DECLARE @tMM int;
DECLARE @tYY int;

/*
    periodFlag = 1 = End of Day
    periodFlag = 2 = End of Month
    periodFlag = 3 = Year End

EXECUTE Sp_GLPeriodEnd 1,1;

*/


IF ISNULL(@approvBy,0) = 0 
	BEGIN
		RAISERROR ('ApprovBy Null',10,1)
		RETURN;
	END

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

		-------- For Customer Services Module Period End -------------------		
		SET @trnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);
        SET @tYY = YEAR(@trnDate);
        SET @tMM = MONTH(@trnDate);

               

        SET @WtrnDate = @trnDate;
		SET @PrevDate = @trnDate;
        SET @HolDate = @trnDate;
		SET @nCount = 0;	

        SET @WeekDay1= (SELECT HolWeekDayName1 FROM  A2ZHKMCUS..A2ZWEEKHOLIDAY);
	    SET @WeekDay2= (SELECT HolWeekDayName2 FROM  A2ZHKMCUS..A2ZWEEKHOLIDAY);
               
		WHILE @nCount = 0
			BEGIN
				SET @WtrnDate = DATEADD(day,1,@WtrnDate);
                SET @dayName = DATEName(DW, @WtrnDate);
				IF @dayName = @WeekDay1
                   BEGIN
                        SET @trnDate = @WtrnDate
					    CONTINUE;
                   END

                IF @dayName = @WeekDay2
                   BEGIN
                        SET @trnDate = @WtrnDate
					    CONTINUE;	
                   END			

				SET @HolDate = (SELECT HolDate FROM A2ZHKMCUS..A2ZHOLIDAY WHERE HolDate = @WtrnDate);
				IF @WtrnDate = @HolDate
                   BEGIN
                        SET @trnDate = @WtrnDate
                        PRINT @trnDate;
				    	CONTINUE;
                   END
                ELSE
                    BEGIN
					     BREAK;
                    END
			END;
		
        
        
        SET @currentDate = @trnDate;
       

        SET @lastDay = DAY(DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));

        
        IF DAY(@trnDate) = @lastDay OR MONTH(@trnDate) != @tMM 
           BEGIN
              SET @periodFlag = 2;
           END
        
         
        IF @periodFlag = 2 
        BEGIN
            UPDATE A2ZHRMCUS..A2ZEMPTSALARY SET StatusT=0;  
        END
              
     
     EXECUTE A2ZCSMCUS..Sp_CSMoveDailyTransaction @tYY;

	 EXECUTE A2ZSTMCUS..Sp_STMoveDailyTransaction @tYY;

--     SET @CalDate = CAST(YEAR(@PrevDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@PrevDate) AS VARCHAR(2)) + '-' + CAST(DAY(@PrevDate) AS VARCHAR(2))    

--     IF @periodFlag = 2 
--        BEGIN
--           EXECUTE A2ZCSMCUS..Sp_CSGenerateLoanDefaulter @CalDate;
--           EXECUTE A2ZCSMCUS..Sp_CSGeneratePensionDefaulter @CalDate;
--        END

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

