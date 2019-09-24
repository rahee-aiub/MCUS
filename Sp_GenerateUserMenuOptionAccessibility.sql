USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_GenerateUserMenuOptionAccessibility]    Script Date: 07/16/2018 2:40:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_GenerateUserMenuOptionAccessibility](@Module int,@UserId INT) 
AS
BEGIN

----exec Sp_GenerateUserMenuOptionAccessibility 1,0

DECLARE @firstDate smalldatetime;
DECLARE @lastDate smalldatetime;




---------- Refresh Workfile ----------
TRUNCATE TABLE WFA2ZERPMENU;
---------- End of Refresh Workfile ----------

    IF @Module = 1
	   BEGIN
	        INSERT INTO WFA2ZERPMENU (ModuleNo,UserId,MenuName)
            SELECT ModuleNo,UserId,MenuName
	        FROM A2ZCSMCUS..A2ZERPMENU
	        WHERE MenuUrl is not null;
	   END
    
	IF @Module = 2
	   BEGIN
	        INSERT INTO WFA2ZERPMENU (ModuleNo,UserId,MenuName)
            SELECT ModuleNo,UserId,MenuName
	        FROM A2ZGLMCUS..A2ZERPMENU
	        WHERE MenuUrl is not null;
	   END

    IF @Module = 6
	   BEGIN
	        INSERT INTO WFA2ZERPMENU (ModuleNo,UserId,MenuName)
            SELECT ModuleNo,UserId,MenuName
	        FROM A2ZBTMCUS..A2ZERPMENU
	        WHERE MenuUrl is not null;
	   END


	UPDATE WFA2ZERPMENU SET ModuleName = (SELECT ModuleName FROM A2ZHKMCUS..A2ZERPMODULE 
	WHERE WFA2ZERPMENU.ModuleNo = A2ZHKMCUS..A2ZERPMODULE.ModuleNo);

	UPDATE WFA2ZERPMENU SET UserName = (SELECT IdsName FROM A2ZHKMCUS..A2ZSYSIDS 
	WHERE WFA2ZERPMENU.UserId = A2ZHKMCUS..A2ZSYSIDS.IdsNo);


	DELETE FROM WFA2ZERPMENU  WHERE UserName IS NULL;


	IF @UserId <> 0
	   BEGIN
	        DELETE FROM WFA2ZERPMENU  WHERE UserId <> @UserId;
	   END

--COMMIT TRANSACTION
--		SET NOCOUNT OFF
--END TRY
--
--BEGIN CATCH
--		ROLLBACK TRANSACTION
--
--		DECLARE @ErrorSeverity INT
--		DECLARE @ErrorState INT
--		DECLARE @ErrorMessage NVARCHAR(4000);	  
--		SELECT 
--			@ErrorMessage = ERROR_MESSAGE(),
--			@ErrorSeverity = ERROR_SEVERITY(),
--			@ErrorState = ERROR_STATE();	  
--		RAISERROR 
--		(
--			@ErrorMessage, -- Message text.
--			@ErrorSeverity, -- Severity.
--			@ErrorState -- State.
--		);	
--END CATCH

END

----exec Sp_CSCalculateProvision6YR


GO

