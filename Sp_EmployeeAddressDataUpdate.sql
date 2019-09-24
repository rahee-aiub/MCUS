USE [A2ZHRMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_EmployeeAddressDataUpdate]    Script Date: 01/22/2019 11:25:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [dbo].[Sp_EmployeeAddressDataUpdate]
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

UPDATE dbo.A2ZEMPADDRESS SET
EmployeeID=@EmployeeID, 
EmpPresentAddress=@EmpPresentAddress, 
EmpPreDivision=@EmpPreDivision, 
EmpPreDistrict=@EmpPreDistrict, 
EmpPreUpzila=@EmpPreUpzila, 
EmpPreThana=@EmpPreThana, 
EmpPreTelNo=@EmpPreTelNo, 
EmpPreMobileNo=@EmpPreMobileNo, 
EmpPreEmail=@EmpPreEmail, 
EmpPermanentAddress=@EmpPermanentAddress, 
EmpPerDivision=@EmpPerDivision, 
EmpPerDistrict=@EmpPerDistrict, 
EmpPerUpzila=@EmpPerUpzila, 
EmpPerThana=@EmpPerThana, 
EmpPerTelNo=@EmpPerTelNo, 
EmpPerMobileNo=@EmpPerMobileNo, 
EmpPerEmail=@EmpPerEmail
WHERE EmployeeID=@EmployeeID;

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

