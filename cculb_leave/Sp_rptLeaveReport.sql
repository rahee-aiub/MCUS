USE A2ZHRMCUS
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


CREATE PROCEDURE [dbo].[Sp_rptLeaveReport](@userID INT)   

AS

BEGIN

/*

EXECUTE Sp_rptLeaveReport 4

*/
BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


SELECT EmpCode AS Userid, EmpCode,Note AS EmpName,EmpleaveCode,Porpus AS LDesi,LStartDate,LEndDate,LApplyDays,LInputeBy,LInputeDate,LCheckBy,LCkeckDate,LVerifyBy,LVerifyDate,
	   LApprovedDate,LApprovedBy,LGrantDays,LBalance,Porpus,Note INTO #TMPA2ZEMPLEAVE
FROM A2ZHRMCUS..A2ZEMPLEAVE
WHERE @Userid = 0

DELETE FROM #TMPA2ZEMPLEAVE WHERE UserID = @UserID

INSERT INTO #TMPA2ZEMPLEAVE (Userid,EmpCode,EmpleaveCode,LStartDate,LEndDate,LApplyDays,LInputeBy,LInputeDate,LCheckBy,LCkeckDate,
			LVerifyBy,LVerifyDate,LApprovedDate,LApprovedBy,LGrantDays,LBalance,Porpus,Note)
SELECT @userID,EmpCode,EmpleaveCode,LStartDate,LEndDate,LApplyDays,LInputeBy,LInputeDate,LCheckBy,LCkeckDate,
			LVerifyBy,LVerifyDate,LApprovedDate,LApprovedBy,LGrantDays,LBalance,Porpus,Note
FROM A2ZHRMCUS..A2ZEMPLEAVE
WHERE Status = 3

UPDATE #TMPA2ZEMPLEAVE SET EmpName = A2ZHRMCUS..A2ZEMPLOYEE.EmpName
FROM A2ZHRMCUS..A2ZEMPLOYEE,#TMPA2ZEMPLEAVE
WHERE A2ZHRMCUS..A2ZEMPLOYEE.EmpCode = #TMPA2ZEMPLEAVE.EmpCode

UPDATE #TMPA2ZEMPLEAVE SET LDesi = A2ZHRMCUS..A2ZEMPLEAVETYPE.EmpleaveName
FROM A2ZHRMCUS..A2ZEMPLEAVETYPE,#TMPA2ZEMPLEAVE
WHERE A2ZHRMCUS..A2ZEMPLEAVETYPE.EmpleaveCode = #TMPA2ZEMPLEAVE.EmpleaveCode

SELECT * FROM #TMPA2ZEMPLEAVE

DROP TABLE #TMPA2ZEMPLEAVE
-- =============================================================================================
---
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

