USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GLUserCashCodeValidity]    Script Date: 04/10/2017 23:17:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_GLUserCashCodeValidity] 
AS
--EXECUTE Sp_GLUserCashCodeValidity

BEGIN


DECLARE @Ids int;
DECLARE @idsNo int;
DECLARE @empCode int;
DECLARE @glCashCode int;
DECLARE @idsEffectDate smalldatetime;

DECLARE @trnDate smalldatetime;

DECLARE @CashCodeDesc nvarchar(100);



BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON



SET @trnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);


DECLARE UserTable CURSOR FOR
SELECT IdsNo,EmpCode,GLCashCode,IdsEffectDate
FROM A2ZHKMCUS..A2ZSYSIDS WHERE IdsStatHold=1 AND @trnDate >= IdsEffectDate;;

OPEN UserTable;
FETCH NEXT FROM UserTable INTO
@idsNo,@empCode,@glCashCode,@idsEffectDate;

WHILE @@FETCH_STATUS = 0 
	BEGIN

        UPDATE A2ZBTMCUS..A2ZSYSIDS SET GLCashCode=@glCashCode WHERE EmpCode=@empCode 

        UPDATE A2ZCSMCUS..A2ZSYSIDS SET GLCashCode=@glCashCode WHERE EmpCode=@empCode 

        UPDATE A2ZGLMCUS..A2ZSYSIDS SET GLCashCode=@glCashCode WHERE EmpCode=@empCode 

        UPDATE A2ZHKMCUS..A2ZSYSIDS SET IdsStatHold=0 WHERE EmpCode=@empCode 

        UPDATE A2ZHRMCUS..A2ZSYSIDS SET GLCashCode=@glCashCode WHERE EmpCode=@empCode 

        SET @Ids = (SELECT IdsNo FROM A2ZCSMCUS..A2ZSYSIDS WHERE EmpCode=@empCode); 
        SET @CashCodeDesc = (SELECT GLAccDesc FROM A2ZGLMCUS..A2ZCGLMST WHERE GLAccNo=@glCashCode); 

        UPDATE A2ZCSMCUS..A2ZUSERCASHCODE SET FromCashCode=@glCashCode, FromCashCodeDesc=@CashCodeDesc WHERE IdsNo=@Ids 

  
	FETCH NEXT FROM UserTable INTO
        @idsNo,@empCode,@glCashCode,@idsEffectDate;
        
	END

CLOSE UserTable; 
DEALLOCATE UserTable;


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


