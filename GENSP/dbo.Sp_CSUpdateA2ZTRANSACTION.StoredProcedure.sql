USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSUpdateA2ZTRANSACTION]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_CSUpdateA2ZTRANSACTION]  
AS

BEGIN

--EXECUTE Sp_CSUpdateA2ZTRANSACTION

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON



UPDATE A2ZTRANSACTION SET A2ZTRANSACTION.AccTypeMode = 1 
WHERE AccType=11 OR 
      AccType=12 OR 
      AccType=13 OR 
      AccType=14 OR
      AccType=15 OR
      AccType=16 OR
      AccType=17 OR
      AccType=18 OR 
      AccType=21 OR 
      AccType=23 OR
      AccType=24 OR
      AccType=51 OR
      AccType=52 OR
      AccType=53 OR
      AccType=54 OR
      AccType=59 OR
      AccType=60 OR
      AccType=61 OR
      AccType=64 OR
      AccType=99;


UPDATE A2ZCSMCUST2015..A2ZTRANSACTION SET A2ZTRANSACTION.AccTypeMode = 1 
WHERE AccType=11 OR 
      AccType=12 OR 
      AccType=13 OR 
      AccType=14 OR
      AccType=15 OR
      AccType=16 OR
      AccType=17 OR
      AccType=18 OR 
      AccType=21 OR 
      AccType=23 OR
      AccType=24 OR
      AccType=51 OR
      AccType=52 OR
      AccType=53 OR
      AccType=54 OR
      AccType=59 OR
      AccType=60 OR
      AccType=61 OR
      AccType=64 OR
      AccType=99;

UPDATE A2ZCSMCUST2016..A2ZTRANSACTION SET A2ZTRANSACTION.AccTypeMode = 1 
WHERE AccType=11 OR 
      AccType=12 OR 
      AccType=13 OR 
      AccType=14 OR
      AccType=15 OR
      AccType=16 OR
      AccType=17 OR
      AccType=18 OR 
      AccType=21 OR 
      AccType=23 OR
      AccType=24 OR
      AccType=51 OR
      AccType=52 OR
      AccType=53 OR
      AccType=54 OR
      AccType=59 OR
      AccType=60 OR
      AccType=61 OR
      AccType=64 OR
      AccType=99;


UPDATE A2ZTRANSACTION SET A2ZTRANSACTION.AccTypeMode = 2 
WHERE AccType=19 OR 
      AccType=25 OR 
      AccType=26 OR 
      AccType=27 OR
      AccType=55 OR
      AccType=56 OR
      AccType=57 OR
      AccType=58 OR 
      AccType=62 OR 
      AccType=63 OR
      AccType=65;
      

UPDATE A2ZCSMCUST2015..A2ZTRANSACTION SET A2ZTRANSACTION.AccTypeMode = 2 
WHERE AccType=19 OR 
      AccType=25 OR 
      AccType=26 OR 
      AccType=27 OR
      AccType=55 OR
      AccType=56 OR
      AccType=57 OR
      AccType=58 OR 
      AccType=62 OR 
      AccType=63 OR
      AccType=65;

UPDATE A2ZCSMCUST2016..A2ZTRANSACTION SET A2ZTRANSACTION.AccTypeMode = 2 
WHERE AccType=19 OR 
      AccType=25 OR 
      AccType=26 OR 
      AccType=27 OR
      AccType=55 OR
      AccType=56 OR
      AccType=57 OR
      AccType=58 OR 
      AccType=62 OR 
      AccType=63 OR
      AccType=65;

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
