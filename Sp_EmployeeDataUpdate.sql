
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_EmployeeDataUpdate]
(
@EmpCode int,
@EmpName varchar(50),
@EmpBaseGrade smallint,
@EmpGrade smallint,
@EmpGradeDesc varchar(50),
@EmpDesignation smallint,
@EmpServiceType smallint,
@EmpSTypeDesc varchar(50),
@EmpArea int,
@EmpLocation int,
@EmpDepartment smallint,
@EmpSection smallint,
@EmpProject smallint,
@EmpJoinDate smalldatetime,
@EmpPerDate smalldatetime,
@EmpLastPostingDate smalldatetime,
@EmpLastPromotionDate smalldatetime,
@EmpLastIncrementDate smalldatetime,
@EmpDOB smalldatetime,
@EmpBank smallint,
@EmpAccNo varchar(50),
@EmpFName  varchar(50),
@EmpMName  varchar(50),
@EmpSpouseName varchar(50),
@EmpNationality smallint,
@EmpNationalityDesc varchar(50),
@EmpRelagion smallint,
@EmpRelagionDesc varchar(50),
@EmpGender smallint,
@EmpGenderDesc varchar(50),
@EmpMaritalStat smallint,
@EmpMaritalStatDesc varchar(50),
@EmpBloodGrp varchar(50),
@EmpHeight varchar(50),
@EmpNationalID varchar(50),
@EmpTIN  varchar(50),
@EmpPPNo varchar(50),
@EmpIssueDate smalldatetime,
@EmpPExpireDate smalldatetime, 
@EmpPlaceofIssue varchar(50),
@EmpLicenseNo varchar(50),
@EmpLExpiryDate smalldatetime,
@EmpPayLabel int,
@EmpConsolidatedAmt money,
@EmpConsolidatedDesc varchar(10)

)

AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

UPDATE dbo.A2ZEMPLOYEE SET
EmpCode=@EmpCode,
EmpName=@EmpName, 
EmpBaseGrade= @EmpBaseGrade,
EmpGrade= @EmpGrade,
EmpGradeDesc= @EmpGradeDesc,
EmpDesignation= @EmpDesignation,
EmpServiceType=@EmpServiceType,
EmpSTypeDesc=@EmpSTypeDesc,
EmpArea =@EmpArea,
EmpLocation =@EmpLocation,
EmpDepartment=@EmpDepartment,
EmpSection=@EmpSection,
EmpProject=@EmpProject,
EmpJoinDate= @EmpJoinDate,
EmpPerDate=@EmpPerDate,
EmpLastPostingDate=@EmpLastPostingDate,
EmpLastPromotionDate=@EmpLastPromotionDate,
EmpLastIncrementDate=@EmpLastIncrementDate,
EmpBank=@EmpBank,
EmpAccNo=@EmpAccNo,
EmpFName =@EmpFName,
EmpMName =@EmpMName,
EmpSpouseName=@EmpSpouseName,
EmpDOB=@EmpDOB,
EmpNationality=@EmpNationality,
EmpNationalityDesc=@EmpNationalityDesc,
EmpRelagion= @EmpRelagion,
EmpRelagionDesc= @EmpRelagionDesc,
EmpGender=@EmpGender,
EmpGenderDesc=@EmpGenderDesc,
EmpMaritalStat=@EmpMaritalStat,
EmpMaritalStatDesc=@EmpMaritalStatDesc,
EmpBloodGrp=@EmpBloodGrp,
EmpHeight=@EmpHeight,
EmpNationalID=@EmpNationalID,
EmpTIN=@EmpTIN,
EmpPPNo=@EmpPPNo,
EmpIssueDate=@EmpIssueDate ,
EmpPExpireDate=@EmpPExpireDate, 
EmpPlaceofIssue=@EmpPlaceofIssue,
EmpLicenseNo=@EmpLicenseNo,
EmpLExpiryDate=@EmpLExpiryDate,
EmpPayLabel=@EmpPayLabel,
EmpConsolidatedAmt=@EmpConsolidatedAmt,
EmpConsolidatedDesc=@EmpConsolidatedDesc

WHERE EmpCode=@EmpCode 

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

