USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GLPeriodEnd]    Script Date: 04/29/2015 15:42:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_GLPeriodEnd] (@approvBy int, @periodFlag int)
AS
BEGIN

DECLARE @trnDate smalldatetime;
DECLARE @HolDate smalldatetime;
DECLARE @nCount int;
DECLARE @dayName VARCHAR(9);


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
		SET @HolDate = @trnDate;
		SET @nCount = 0;		

		WHILE @nCount = 0
			BEGIN
				SET @trnDate = DATEADD(day,1,@trnDate);
                SET @dayName = DATEName(DW, @trnDate);
				IF @dayName = 'Friday'
					CONTINUE;
				
				SET @HolDate = (SELECT HolDate FROM A2ZHKMCUS..A2ZHOLIDAY WHERE HolDate = @trnDate);
				IF @trnDate = @HolDate
					CONTINUE
                ELSE
					BREAK;
			END;
		
		UPDATE A2ZCSMCUS..A2ZCSPARAMETER SET ProcessDate = @trnDate,ApprovBy = @approvBy, ApprovByDate = getdate();
	
		UPDATE A2ZCSMCUS..A2ZCSPARAMETER SET CurrentMonth = MONTH(ProcessDate),CurrentYear = YEAR(ProcessDate)
		-------- End of For Customer Services Module Period End -------------------		

		-------- For General Ledger Module Period End -------------------		

		UPDATE A2ZGLMCUS..A2ZGLPARAMETER SET ProcessDate = @trnDate,ApprovBy = @approvBy, ApprovByDate = getdate();
		
		UPDATE A2ZGLMCUS..A2ZGLPARAMETER SET CurrentMonth = MONTH(ProcessDate),CurrentYear = YEAR(ProcessDate);				
		-------- End of For General Ledger Module Period End -------------------

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

