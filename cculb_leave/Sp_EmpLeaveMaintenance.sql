USE A2ZHRMCUS
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



ALTER PROCEDURE [dbo].[Sp_EmpLeaveMaintenance]
(
@EmpCode int,
@EmpleaveCode int,
@LStartDate smalldatetime,
@LEndDate smalldatetime,
@EmpApplyDays int,
@EmpLBalance decimal
)
AS 
BEGIN
DECLARE @Desig INT 
BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

SET @Desig = (SELECT EmpDesignation
FROM A2ZHRMCUS..A2ZEMPLOYEE
WHERE EmpCode = @EmpCode)


IF @Desig = 14
	BEGIN
		INSERT INTO dbo.A2ZEMPLEAVE (EmpCode,EmpleaveCode,LStartDate,LEndDate,LApplyDays,LBalance,Status)
		VALUES(@EmpCode,@EmpleaveCode,@LStartDate,@LEndDate,@EmpApplyDays,@EmpLBalance,1)
	END

 ELSE IF @Desig IN (1,4,5,6,7,10,11)
	BEGIN
		INSERT INTO dbo.A2ZEMPLEAVE (EmpCode,EmpleaveCode,LStartDate,LEndDate,LApplyDays,LBalance,Status)
		VALUES(@EmpCode,@EmpleaveCode,@LStartDate,@LEndDate,@EmpApplyDays,@EmpLBalance,2)
	END
 ELSE
		INSERT INTO dbo.A2ZEMPLEAVE (EmpCode,EmpleaveCode,LStartDate,LEndDate,LApplyDays,LBalance,Status)
		VALUES(@EmpCode,@EmpleaveCode,@LStartDate,@LEndDate,@EmpApplyDays,@EmpLBalance,0)

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








