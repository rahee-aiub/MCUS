USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSLoanAccountDefaulterDataUpdate]    Script Date: 06/29/2019 12:09:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_CSLoanAccountDefaulterDataUpdate]
(@TrnDate SMALLDATETIME,@CuType int,@CuNo int,@MemNo int,@AccType int, @AccNo Bigint,@CurrDueIntAmt money)

AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON


DECLARE @ID INT;

DECLARE @sqlQuery NVARCHAR(MAX);
DECLARE @ParmDef NVARCHAR(50);

DECLARE @ProcessDate SMALLDATETIME;
DECLARE @nYear INT;

DECLARE @valueInt INT;
DECLARE @valueInt1 INT;



UPDATE A2ZLOANDEFAULTER SET
CurrDueIntAmt = @CurrDueIntAmt 
WHERE MONTH(TrnDate) = MONTH(@TrnDate) AND YEAR(TrnDate) = YEAR(@TrnDate) AND
CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo AND AccType = @AccType AND AccNo = @AccNo;




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

