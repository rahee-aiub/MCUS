USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCUTransferDataUpdate]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_CSCUTransferDataUpdate]
(
		
	
	@CuType	smallint,
	@CuNo	int,
    @CuAffiCuType	smallint,
	@CuAffiCuTypeName	nvarchar(25),
	@CuAffiCuNo	int,
	@CuAssoCuType	smallint,
	@CuAssoCuTypeName	nvarchar(50),
	@CuAssoCuNo	int,
    @CuReguCuType	smallint,
	@CuReguCuTypeName	nvarchar(50),
	@CuReguCuNo	int,
    @CuStatus	smallint,
	@CuStatusDate	smalldatetime,
	@CuOldCuNo	int
	
)

AS

BEGIN


BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

	UPDATE dbo.A2ZCUNION SET
	CuType=@CuType,
	CuNo=@CuNo,
	CuAffiCuType=@CuAffiCuType,
	CuAffiCuTypeName=@CuAffiCuTypeName,
	CuAffiCuNo=@CuAffiCuNo,
	CuAssoCuType=@CuAssoCuType,
	CuAssoCuTypeName=@CuAssoCuTypeName,
	CuAssoCuNo=@CuAssoCuNo,
    CuReguCuType=@CuReguCuType,
	CuReguCuTypeName=@CuReguCuTypeName,
	CuReguCuNo=@CuReguCuNo,
    CuStatus=@CuStatus,
	CuStatusDate=@CuStatusDate,
	CuOldCuNo=@CuOldCuNo
	
	
   WHERE  CuType = @CuType and CuNo =@CuNo


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
