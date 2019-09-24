USE A2ZHRMCUS
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


CREATE PROCEDURE [dbo].[Sp_HRLeaveCheck](@userID INT)   

AS

BEGIN

/*

EXECUTE Sp_HRLeaveCheck 484

*/

DECLARE @EmpCode INT;
DECLARE @Desig INT 
DECLARE @EmpArea INT 
DECLARE @EmpLocation INT 
DECLARE @FromCashCode INT
--
BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON
--
SET @Desig = (SELECT EmpDesignation
FROM A2ZHRMCUS..A2ZEMPLOYEE
WHERE EmpCode = @UserID)

PRINT @UserID
DELETE FROM WFLEAVEAPPROVE WHERE UserID = @UserID

INSERT INTO WFLEAVEAPPROVE (UserID,EmpIdsNo,EmpCashCode,CashCodeDesc,RegFlg)
SELECT @UserID,IdsNo,FromCashCode,FromCashCodeDesc,0
FROM A2ZCSMCUS..A2ZUSERCASHCODE
WHERE IdsNo = @UserID  

INSERT INTO WFLEAVEAPPROVE (UserID,EmpIdsNo,EmpCashCode,CashCodeDesc)
SELECT @UserID,IdsNo,FromCashCode,FromCashCodeDesc
FROM A2ZCSMCUS..A2ZUSERCASHCODE,A2ZHRMCUS..WFLEAVEAPPROVE
WHERE A2ZCSMCUS..A2ZUSERCASHCODE.FromCashCode = A2ZHRMCUS..WFLEAVEAPPROVE.EmpCashCode AND UserID = @UserID

DELETE FROM A2ZHRMCUS..WFLEAVEAPPROVE WHERE RegFlg = 0 AND UserID = @UserID

UPDATE A2ZHRMCUS..WFLEAVEAPPROVE SET EmpDesignation = (SELECT EmpDesignation
FROM A2ZHRMCUS..A2ZEMPLOYEE
WHERE EmpCode = EmpIdsNo)

-- =============================================================================================
IF @Desig IN (7,10,11)
	BEGIN
		DELETE FROM A2ZHRMCUS..WFLEAVEAPPROVE WHERE EmpDesignation <> 14 AND UserID = @UserID
					
		INSERT INTO A2ZHRMCUS..WFLEAVEAPPROVE (UserID,EmpIdsNo,RegFlg)
		SELECT UserID, EmpIdsNo,0 FROM A2ZHRMCUS..WFLEAVEAPPROVE WHERE UserID = @UserID GROUP BY EmpIdsNo, UserID
	
		DELETE FROM A2ZHRMCUS..WFLEAVEAPPROVE WHERE RegFlg IS NULL AND UserID = @UserID

		DELETE FROM A2ZHRMCUS..WFLEAVEAPPROVE WHERE EmpIdsNo = @UserID
		
		SELECT  EmpCode,EmpleaveCode,LStartDate,LEndDate,LApplyDays,LApplyDate,LBalance,LInputeBy,
			    LInputeDate,Status,Porpus,Note
		FROM A2ZHRMCUS..A2ZEMPLEAVE,A2ZHRMCUS..WFLEAVEAPPROVE
		WHERE A2ZHRMCUS..A2ZEMPLEAVE.Status = 0 AND A2ZHRMCUS..A2ZEMPLEAVE.EmpCode = A2ZHRMCUS..WFLEAVEAPPROVE.EmpIdsNo 
		AND A2ZHRMCUS..WFLEAVEAPPROVE.UserID = @UserID
						
	END

IF @Desig = 14
	BEGIN
		DELETE FROM A2ZHRMCUS..WFLEAVEAPPROVE WHERE EmpDesignation IN (1,3,4,5,6,7,10,11,14) AND UserID = @UserID

		INSERT INTO A2ZHRMCUS..WFLEAVEAPPROVE (UserID,EmpIdsNo,RegFlg)
		SELECT UserID, EmpIdsNo,0 FROM A2ZHRMCUS..WFLEAVEAPPROVE WHERE UserID = @UserID GROUP BY EmpIdsNo, UserID
				
		DELETE FROM A2ZHRMCUS..WFLEAVEAPPROVE WHERE RegFlg IS NULL AND UserID = @UserID

		DELETE FROM A2ZHRMCUS..WFLEAVEAPPROVE WHERE EmpIdsNo = @UserID

		SELECT  EmpCode,EmpleaveCode,LStartDate,LEndDate,LApplyDays,LApplyDate,LBalance,LInputeBy,
			    LInputeDate,Status,Porpus,Note
		FROM A2ZHRMCUS..A2ZEMPLEAVE,A2ZHRMCUS..WFLEAVEAPPROVE
		WHERE A2ZHRMCUS..A2ZEMPLEAVE.Status = 0 AND A2ZHRMCUS..A2ZEMPLEAVE.EmpCode = A2ZHRMCUS..WFLEAVEAPPROVE.EmpIdsNo 
		AND A2ZHRMCUS..WFLEAVEAPPROVE.UserID = @UserID
	END
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

