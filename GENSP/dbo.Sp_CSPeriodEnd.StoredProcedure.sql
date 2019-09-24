USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSPeriodEnd]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_CSPeriodEnd] (@approvBy int, @periodFlag int)
AS
BEGIN

DECLARE @trnDate smalldatetime;
DECLARE @HolDate smalldatetime;
DECLARE @CalDate smalldatetime;

/*
    periodFlag = 1 = End of Day
    periodFlag = 2 = End of Month
    periodFlag = 3 = Year End
*/


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

--SET @CalDate = (DATEADD(day,1,@accMatureDate))


SET @HolDate= (SELECT HolDate FROM A2ZHOLIDAY WHERE HolDate = @trnDate);




--SET @newRenwlDate = (DATEADD(day,1,@accMatureDate))

IF ISNULL(@approvBy,0) = 0 
	BEGIN
		RAISERROR ('ApprovBy Null',10,1)
		RETURN;
	END

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON
		
		UPDATE A2ZCSPARAMETER SET ProcessDate = DATEADD(day,1,ProcessDate),
				ApprovBy = @approvBy, ApprovByDate = getdate()		
				
		IF @periodFlag = 2
			BEGIN
				UPDATE A2ZCSPARAMETER SET CurrentMonth = MONTH(ProcessDate),CurrentYear = YEAR(ProcessDate)				
			END		

--		IF @periodFlag = 3
--			BEGIN
--				UPDATE A2ZINVPARAMETER SET CurrentMonth = MONTH(ProcessDate),CurrentYear = YEAR(ProcessDate);
--
--				UPDATE A2ZINVPARAMETER SET FinancialBegYear = YEAR(ProcessDate), FinancialEndYear = YEAR(ProcessDate),
--						LastGRNNoRaw = 0, LastGRNNoPacking = 0, LastChallanNo = 0, LastChallanNoP = 0;
--				
--				SET @trnDate = (SELECT ProcessDate FROM A2ZINVPARAMETER);
--
--				INSERT INTO A2ZITEMSOPSTOCK (TrnDate,ItemType,ItemSourceCode,ItemCode,ItemName,UnitCode,ItemFlag,
--				ItemStatus,ItemRemarks,ItemPotency,SpecificationCode,StockBalance,StockIn,StockOut,StockAdjust,ItemPrice,
--				StockDamage,StockLoan,StockSupply,StockReturn,StockMinQty,StockMaxQty,StockStandardQty,StockMinDays,StockMaxDays,
--				UserId)
--				SELECT @trnDate,ItemType,ItemSourceCode,ItemCode,ItemName,UnitCode,ItemFlag,
--				ItemStatus,ItemRemarks,ItemPotency,SpecificationCode,StockBalance,StockIn,StockOut,StockAdjust,ItemPrice,
--				StockDamage,StockLoan,StockSupply,StockReturn,StockMinQty,StockMaxQty,StockStandardQty,StockMinDays,StockMaxDays,
--				@approvBy
--				FROM A2ZITEMS;
--
--				INSERT INTO A2ZPRODUCTOPSTOCK (TrnDate,ProdCode,ProdName,PackSizeCode,PackSizeCodePhys,ProdFlag,ProdType
--				,ProdStatus,ProdRemarks,SpecificationCode,UnitCode,ProdBaseQty,ProdBatchSizeRawUnit,ProdBatchSizeRawQty
--				,ProdBatchSizeFinishUnit,ProdBatchSizeFinishQty,ProdFillWeightUnit,ProdFillWeightQty,ProdSalesCode
--				,ProdDARNo,ProdDosage,ProdStrength,GenericCode,TradePrice,RevisionNo,ProdGroup,StockBalance,StockIn
--				,StockOut,StockAdjust,StockDamage,StockReturn,StockBalanceP,StockInP,StockOutP,StockAdjustP,StockDamageP
--				,StockReturnP,StockMinQty,StockMaxQty,StockStandardQty,StockMinDays,StockMaxDays,UserId)
--				SELECT @trnDate,ProdCode,ProdName,PackSizeCode,PackSizeCodePhys,ProdFlag,ProdType
--				,ProdStatus,ProdRemarks,SpecificationCode,UnitCode,ProdBaseQty,ProdBatchSizeRawUnit,ProdBatchSizeRawQty
--				,ProdBatchSizeFinishUnit,ProdBatchSizeFinishQty,ProdFillWeightUnit,ProdFillWeightQty,ProdSalesCode
--				,ProdDARNo,ProdDosage,ProdStrength,GenericCode,TradePrice,RevisionNo,ProdGroup,StockBalance,StockIn
--				,StockOut,StockAdjust,StockDamage,StockReturn,StockBalanceP,StockInP,StockOutP,StockAdjustP,StockDamageP
--				,StockReturnP,StockMinQty,StockMaxQty,StockStandardQty,StockMinDays,StockMaxDays,@approvBy
--				FROM A2ZPRODUCT
--
--				UPDATE A2ZPRODUCT SET LastBatchNo = 0;
--			END		

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
