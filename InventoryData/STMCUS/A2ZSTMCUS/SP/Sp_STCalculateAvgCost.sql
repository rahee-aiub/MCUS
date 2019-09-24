USE [A2ZSTMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_STCalculateAvgCost]    Script Date: 6/23/2018 10:07:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE  [dbo].[Sp_STCalculateAvgCost](@StkItemCode INT, @UnitQty INT, @UnitPrice MONEY, @AvgCostDate smalldatetime)

AS
BEGIN

/*

EXECUTE Sp_STCalculateAvgCost 1010001,10,20,'2018-03-01'

*/

DECLARE @StoredQty INT;
DECLARE @OldAvgPrice MONEY;
DECLARE @NewAvgPrice MONEY;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

				SET @StoredQty = (SELECT STKUnitQty FROM A2ZSTMST WHERE STKItemCode = @StkItemCode);
				SET @OldAvgPrice = (SELECT STKUnitAvgCost FROM A2ZSTMST WHERE STKItemCode = @StkItemCode);

				
				SET @NewAvgPrice = (((@StoredQty * @OldAvgPrice) + (@UnitQty * @UnitPrice)) / (@StoredQty + @UnitQty));
	
				UPDATE A2ZSTMST SET STKUnitAvgCost =  @NewAvgPrice, STKUnitAvgCostDate = @AvgCostDate WHERE STKItemCode = @StkItemCode




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


END;





GO

