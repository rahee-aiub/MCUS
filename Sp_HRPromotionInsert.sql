
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_HRPromotionInsert]
(
@EmpCode int,
@EmpOldArea int,
@EmpOldAreaDesc varchar(50),
@EmpOldLocation int,
@EmpOldLocationDesc varchar(50),
@EmpOldSection smallint,
@EmpOldSectionDesc varchar(50),
@EmpOldDepartment smallint,
@EmpOldDepartmentDesc varchar(50),
@EmpOldProject smallint,
@EmpOldProjectDesc varchar(50),
@EmpOldDesignation smallint,
@EmpOldDesigDesc varchar(50),
@EmpOldCashCode int,
@EmpOldServiceType smallint,
@EmpOldSTypeDesc varchar(50),
@EmpOldBaseGrade smallint,
@EmpOldBaseGradeDesc varchar(50),
@EmpOldGrade int,
@EmpOldGradeDesc varchar(50),
@EmpOldPayScaleDesc varchar(50),
@EmpOldPayLabel int,
@EmpOldBasic money,

@EmpNewArea int,
@EmpNewAreaDesc varchar(50),
@EmpNewLocation int,
@EmpNewLocationDesc varchar(50),
@EmpNewSection smallint,
@EmpNewSectionDesc varchar(50),
@EmpNewDepartment smallint,
@EmpNewDepartmentDesc varchar(50),
@EmpNewProject smallint,
@EmpNewProjectDesc varchar(50),
@EmpNewDesignation smallint,
@EmpNewDesigDesc varchar(50),
@EmpNewCashCode int,
@EmpNewServiceType smallint,
@EmpNewSTypeDesc varchar(50),
@EmpNewBaseGrade smallint,
@EmpNewBaseGradeDesc varchar(50),
@EmpNewGrade int,
@EmpNewGradeDesc varchar(50),
@EmpNewPayScaleDesc varchar(50),
@EmpNewPayLabel int,
@EmpNewBasic money,
@EmpPromotionDate smalldatetime,
@EmpLastPromotionDate smalldatetime

)

AS
BEGIN


DECLARE @Ids int;
DECLARE @CashCodeDesc nvarchar(50);


BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

INSERT INTO dbo.A2ZHRPROMOTION(EmpCode, EmpPromotionDate,EmpLastPromotionDate, EmpOldArea, EmpOldAreaDesc,EmpOldLocation, EmpOldLocationDesc, EmpOldSection, 
EmpOldSectionDesc, EmpOldDepartment, EmpOldDepartmentDesc, EmpOldProject, EmpOldProjectDesc, 
EmpOldDesignation, EmpOldDesigDesc,EmpOldCashCode, EmpOldServiceType, EmpOldSTypeDesc, EmpOldBaseGrade, EmpOldBaseGradeDesc, EmpOldGrade, EmpOldGradeDesc, 
EmpOldPayScaleDesc, EmpOldPayLabel, EmpOldBasic, EmpNewArea, 
EmpNewAreaDesc,EmpNewLocation,EmpNewLocationDesc, EmpNewSection, EmpNewSectionDesc, EmpNewDepartment, EmpNewDepartmentDesc, 
EmpNewProject, EmpNewProjectDesc, EmpNewDesignation, EmpNewDesigDesc,EmpNewCashCode,EmpNewServiceType, EmpNewSTypeDesc, 
EmpNewBaseGrade, EmpNewBaseGradeDesc,EmpNewGrade, EmpNewGradeDesc, EmpNewPayScaleDesc, EmpNewPayLabel,EmpNewBasic
)
VALUES(@EmpCode, @EmpPromotionDate,@EmpLastPromotionDate, @EmpOldArea, @EmpOldAreaDesc,@EmpOldLocation, @EmpOldLocationDesc, @EmpOldSection, @EmpOldSectionDesc, 
@EmpOldDepartment, @EmpOldDepartmentDesc, @EmpOldProject, @EmpOldProjectDesc, @EmpOldDesignation, 
@EmpOldDesigDesc,@EmpOldCashCode, @EmpOldServiceType, @EmpOldSTypeDesc, @EmpOldBaseGrade, @EmpOldBaseGradeDesc,@EmpOldGrade, @EmpOldGradeDesc,  
@EmpOldPayScaleDesc, @EmpOldPayLabel, @EmpOldBasic, @EmpNewArea, 
@EmpNewAreaDesc,@EmpNewLocation,@EmpNewLocationDesc, @EmpNewSection, @EmpNewSectionDesc, @EmpNewDepartment, 
@EmpNewDepartmentDesc, @EmpNewProject, @EmpNewProjectDesc, @EmpNewDesignation, @EmpNewDesigDesc,@EmpNewCashCode,
@EmpNewServiceType, @EmpNewSTypeDesc, @EmpNewBaseGrade, @EmpNewBaseGradeDesc,@EmpNewGrade, @EmpNewGradeDesc,
@EmpNewPayScaleDesc, @EmpNewPayLabel, @EmpNewBasic)


UPDATE dbo.A2ZEMPLOYEE SET
EmpCode=@EmpCode,
EmpDesignation= @EmpNewDesignation,
EmpDesigDesc= @EmpNewDesigDesc,
EmpCashCode= @EmpNewCashCode,
EmpArea =@EmpNewArea,
EmpAreaDesc =@EmpNewAreaDesc,
EmpLocation =@EmpNewLocation,
EmpLocationDesc =@EmpNewLocationDesc,
EmpDepartment=@EmpNewDepartment,
EmpDepartmentDesc=@EmpNewDepartmentDesc,
EmpSection=@EmpNewSection,
EmpSectionDesc=@EmpNewSectionDesc,
EmpProject=@EmpNewProject,
EmpProjectDesc=@EmpNewProjectDesc,
EmpServiceType=@EmpNewServiceType,
EmpSTypeDesc=@EmpNewSTypeDesc,
EmpBaseGrade=@EmpNewBaseGrade,
EmpBaseGradeDesc=@EmpNewBaseGradeDesc,
EmpGrade=@EmpNewGrade,
EmpGradeDesc=@EmpNewGradeDesc,
EmpLastPromotionDate=@EmpPromotionDate,
--EmpGrade=@EmpNewPayScale,
EmpPayLabel=@EmpNewPayLabel

WHERE EmpCode=@EmpCode 


-----------------------------------------------------
UPDATE A2ZBTMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

UPDATE A2ZCSMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

UPDATE A2ZGLMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

UPDATE A2ZHKMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

UPDATE A2ZHRMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

SET @Ids = (SELECT IdsNo FROM A2ZCSMCUS..A2ZSYSIDS WHERE EmpCode=@EmpCode); 
SET @CashCodeDesc = (SELECT GLAccDesc FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo=@EmpNewCashCode); 

UPDATE A2ZCSMCUS..A2ZUSERCASHCODE SET FromCashCode=@EmpNewCashCode, FromCashCodeDesc=@CashCodeDesc WHERE IdsNo=@Ids 

-----------------------------------------------------

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

