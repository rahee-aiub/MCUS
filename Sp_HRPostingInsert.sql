
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_HRPostingInsert]
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
@EmpPostingDate smalldatetime,
@EmpLastPostingDate smalldatetime
)

AS
BEGIN

DECLARE @trnDate smalldatetime;

DECLARE @Ids int;
DECLARE @CashCodeDesc nvarchar(50);

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

INSERT INTO dbo.A2ZHRPOSTING(EmpCode,EmpPostingDate,EmpLastPostingDate,EmpOldArea, EmpOldAreaDesc,EmpOldLocation, EmpOldLocationDesc, EmpOldSection, 
EmpOldSectionDesc, EmpOldDepartment, EmpOldDepartmentDesc, EmpOldProject, EmpOldProjectDesc, 
EmpOldDesignation, EmpOldDesigDesc, EmpOldCashCode, EmpNewArea, EmpNewAreaDesc,EmpNewLocation, EmpNewLocationDesc, EmpNewSection, EmpNewSectionDesc, 
EmpNewDepartment, EmpNewDepartmentDesc, EmpNewProject, EmpNewProjectDesc, EmpNewDesignation, EmpNewDesigDesc,EmpNewCashCode
)
VALUES(   @EmpCode,@EmpPostingDate,@EmpLastPostingDate, @EmpOldArea, @EmpOldAreaDesc,@EmpOldLocation, @EmpOldLocationDesc, @EmpOldSection, @EmpOldSectionDesc, 
@EmpOldDepartment, @EmpOldDepartmentDesc, @EmpOldProject, @EmpOldProjectDesc, @EmpOldDesignation, 
@EmpOldDesigDesc, @EmpOldCashCode, @EmpNewArea, @EmpNewAreaDesc,@EmpNewLocation, @EmpNewLocationDesc, @EmpNewSection, @EmpNewSectionDesc, @EmpNewDepartment, 
@EmpNewDepartmentDesc, @EmpNewProject, @EmpNewProjectDesc, @EmpNewDesignation, @EmpNewDesigDesc,@EmpNewCashCode)


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
EmpLastPostingDate=@EmpPostingDate

WHERE EmpCode=@EmpCode 

----------------------------------------------

SET @trnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

IF @EmpPostingDate > @trnDate
   BEGIN
      UPDATE A2ZHKMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode,
                                      IdsStatHold=1,
                                      IdsEffectDate=@EmpPostingDate 
      WHERE EmpCode=@EmpCode 
   END
ELSE
   BEGIN
-----------------------------------------------
        UPDATE A2ZBTMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

        UPDATE A2ZCSMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

        UPDATE A2ZGLMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

        UPDATE A2ZHKMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

        UPDATE A2ZHRMCUS..A2ZSYSIDS SET GLCashCode=@EmpNewCashCode WHERE EmpCode=@EmpCode 

        SET @Ids = (SELECT IdsNo FROM A2ZCSMCUS..A2ZSYSIDS WHERE EmpCode=@EmpCode); 
        SET @CashCodeDesc = (SELECT GLAccDesc FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo=@EmpNewCashCode); 

        UPDATE A2ZCSMCUS..A2ZUSERCASHCODE SET FromCashCode=@EmpNewCashCode, FromCashCodeDesc=@CashCodeDesc WHERE IdsNo=@Ids 
------------------------------------------------
   END

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

