USE [A2ZHRMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_EmpLeaveMaintenance]    Script Date: 07/11/2019 9:25:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Sp_EmpLeaveMaintenance]
(
@EmpCode int,
@EmpleaveCode int,
@LStartDate smalldatetime,
@LEndDate smalldatetime,
@EmpApplyDays int,
@EmpLBalance decimal,
@LPurpose varchar(50),
@LNote varchar(100),
@LProcStat smallint,
@InputBy int,
@InputByDate smalldatetime

)
AS 
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

INSERT INTO dbo.A2ZEMPLEAVE (EmpCode,EmpleaveCode,LStartDate,LEndDate,LApplyDays,LBalance,LPurpose,LNote,LProcStat,InputBy,InputByDate)
VALUES(@EmpCode,@EmpleaveCode,@LStartDate,@LEndDate,@EmpApplyDays,@EmpLBalance,@LPurpose,@LNote,@LProcStat,@InputBy,@InputByDate)

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

