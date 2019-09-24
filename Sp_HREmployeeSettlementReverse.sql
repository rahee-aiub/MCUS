USE [A2ZHRMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_HREmployeeSettlementReverse]    Script Date: 06/26/2019 3:33:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_HREmployeeSettlementReverse] (@EmpCode INT, @VchNo NVARCHAR(20))  

AS
BEGIN

	BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON
--
-- Last Modified Date on June 25, 2019
--
-- Sp_HREmployeeSettlementReverse 403,'123',101,0
--

	DECLARE @FromCashCode INT;	
	DECLARE @TrnDate SMALLDATETIME;

	

	DELETE FROM A2ZCSMCUS..A2ZTRANSACTION  WHERE VchNo = @VchNo;


	UPDATE A2ZHRMCUS..A2ZEMPLOYEE SET Status = PrevStatus, StatusDate = PrevStatusDate WHERE EmpCode = @EmpCode;


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

