
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_EmployeeAddressDataInsert]
(
@EmployeeID int,
@EmpPresentAddress varchar(200),
@EmpPreDivision int,
@EmpPreDistrict int,
@EmpPreUpzila int,
@EmpPreThana int,
@EmpPreTelNo varchar(50),
@EmpPreMobileNo varchar(50),
@EmpPreEmail varchar(50),
@EmpPermanentAddress varchar(200),
@EmpPerDivision int,
@EmpPerDistrict int,
@EmpPerUpzila int,
@EmpPerThana int,
@EmpPerTelNo varchar(50),
@EmpPerMobileNo varchar(50),
@EmpPerEmail varchar(50)
)

AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

INSERT INTO dbo.A2ZEMPADDRESS(EmployeeID, EmpPresentAddress, EmpPreDivision, EmpPreDistrict, EmpPreUpzila, EmpPreThana, EmpPreTelNo, EmpPreMobileNo, EmpPreEmail, EmpPermanentAddress, 
                      EmpPerDivision, EmpPerDistrict,EmpPerUpzila, EmpPerThana, EmpPerTelNo, EmpPerMobileNo, EmpPerEmail)

VALUES( @EmployeeID, @EmpPresentAddress, @EmpPreDivision, @EmpPreDistrict,@EmpPreUpzila, @EmpPreThana, @EmpPreTelNo, @EmpPreMobileNo, @EmpPreEmail, @EmpPermanentAddress, 
                      @EmpPerDivision, @EmpPerDistrict,@EmpPerUpzila, @EmpPerThana, @EmpPerTelNo, @EmpPerMobileNo, @EmpPerEmail)


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

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

