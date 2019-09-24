USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCreditUnionDataInsert]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSCreditUnionDataInsert]
(

	@CuType	smallint,
	@CuTypeName	nvarchar(25),
	@CuNo	int,
	@CuName	nvarchar(50),
	@CuOpDt	smalldatetime,
	@CuFlag	smallint,
	@CuCertNo	int,
	@CuAddL1	nvarchar(50),
	@CuAddL2	nvarchar(50),
	@CuAddL3	nvarchar(50),
	@CuTel	nvarchar(50),
	@CuMobile	nvarchar(50),
	@CuFax	nvarchar(50),
	@CuEmail	nvarchar(50),
	@CuDivi	int,
	@CuDist	int,
	@CuUpzila	int,
	@CuThana	int,
	@CuProcFlag	smallint,
	@CuProcDesc	nchar(15),
	@CuStatus	smallint,
	@CuStatusDate	smalldatetime,
	@CuAffiCuType	smallint,
	@CuAffiCuTypeName	nvarchar(25),
	@CuAffiCuNo	int,
	@CuAssoCuType	smallint,
	@CuAssoCuTypeName	nvarchar(50),
	@CuAssoCuNo	int,
    @CuReguCuType	smallint,
	@CuReguCuTypeName	nvarchar(50),
	@CuReguCuNo	int,
	@GLCashCode	int,
	@InputBy	smallint,
	@VerifyBy	smallint,
	@ApprovBy	smallint,
	@InputByDate	datetime,
	@VerifyByDate	datetime,
	@ApprovByDate	datetime,
	@ValueDate	smalldatetime,
	@UserId	smallint,
	@CreateDate	datetime

)

AS
BEGIN



BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON



INSERT INTO dbo.A2ZCUNION( CuType, CuTypeName, CuNo, CuName, CuOpDt, CuFlag, CuCertNo, CuAddL1, CuAddL2, CuAddL3, CuTel, CuMobile, CuFax, CuEmail, CuDivi, CuDist, 
                      CuUpzila, CuThana, CuProcFlag, CuProcDesc, CuStatus, CuStatusDate, CuAffiCuType, CuAffiCuTypeName, CuAffiCuNo, CuAssoCuType, CuAssoCuTypeName, 
                      CuAssoCuNo,CuReguCuType, CuReguCuTypeName, 
                      CuReguCuNo, GLCashCode, InputBy, VerifyBy, ApprovBy, InputByDate, VerifyByDate, ApprovByDate, ValueDate, UserId, CreateDate)


VALUES( @CuType, @CuTypeName, @CuNo,@CuName, @CuOpDt, @CuFlag, @CuCertNo, @CuAddL1, @CuAddL2, @CuAddL3, @CuTel, @CuMobile, @CuFax, @CuEmail, @CuDivi, @CuDist, 
                      @CuUpzila, @CuThana, @CuProcFlag, @CuProcDesc, @CuStatus, @CuStatusDate, @CuAffiCuType, @CuAffiCuTypeName, @CuAffiCuNo, @CuAssoCuType, @CuAssoCuTypeName, 
                      @CuAssoCuNo,@CuReguCuType, @CuReguCuTypeName, 
                      @CuReguCuNo, @GLCashCode, @InputBy, @VerifyBy, @ApprovBy, @InputByDate, @VerifyByDate, @ApprovByDate, @ValueDate, @UserId, @CreateDate)



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
