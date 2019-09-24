USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGenerateTodaysPensionDeposit]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE  [dbo].[Sp_CSGenerateTodaysPensionDeposit] (@AccNo Bigint)

AS
--EXECUTE Sp_CSGenerateTodaysPensionDeposit 2017

BEGIN


DECLARE @strSQL NVARCHAR(MAX);

DECLARE @PLCode INT;
DECLARE @PLIncome MONEY;
DECLARE @PLExpense MONEY;
DECLARE @processDate SMALLDATETIME;

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON


            UPDATE A2ZACCOUNT SET CalPaidDeposit = 0, CalPaidInterest = 0, CalPaidPenal = 0 WHERE AccNo=@AccNo; 

---------- CalPaidDeposit

			UPDATE A2ZACCOUNT SET A2ZACCOUNT.CalPaidDeposit = A2ZACCOUNT.CalPaidDeposit + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM A2ZTRANSACTION
			WHERE A2ZTRANSACTION.AccNo = @AccNo AND A2ZTRANSACTION.TrnCSGL = 0 AND A2ZTRANSACTION.ShowInterest = 0 AND 
			A2ZTRANSACTION.PayType = 301 AND A2ZTRANSACTION.TrnFlag = 0 AND A2ZTRANSACTION.TrnProcStat = 0 AND A2ZTRANSACTION.TrnDrCr = 1 AND A2ZTRANSACTION.TrnCredit != 0),0)
			FROM A2ZACCOUNT,A2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = @AccNo;

---------- CalPaidPenal

			UPDATE A2ZACCOUNT SET A2ZACCOUNT.CalPaidPenal = A2ZACCOUNT.CalPaidPenal + 
			ISNULL((SELECT SUM(GLCreditAmt) FROM A2ZTRANSACTION
			WHERE A2ZTRANSACTION.AccNo = @AccNo AND A2ZTRANSACTION.TrnCSGL = 0 AND A2ZTRANSACTION.ShowInterest = 1 AND 
			A2ZTRANSACTION.PayType = 302 AND A2ZTRANSACTION.TrnFlag = 0 AND A2ZTRANSACTION.TrnProcStat = 0 AND A2ZTRANSACTION.TrnDrCr = 1 AND A2ZTRANSACTION.TrnCredit != 0),0)
			FROM A2ZACCOUNT,A2ZTRANSACTION
			WHERE A2ZACCOUNT.AccNo = @AccNo;
 

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
