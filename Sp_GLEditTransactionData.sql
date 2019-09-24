USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GLEditTransactionData]    Script Date: 05/18/2017 15:46:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROCEDURE [dbo].[Sp_GLEditTransactionData](@fDate VARCHAR(10),@userid int) 

/*
EXECUTE Sp_GLEditTransactionData '2017-01-31'



*/


AS
BEGIN


DECLARE @TrnGLAccNoDr    int;
DECLARE @TrnGLAccNoCr    int;
DECLARE @GLAccNo         int;
DECLARE @GLAmount        money;
DECLARE @GLDebitAmt      money;
DECLARE @GLCreditAmt     money;
DECLARE @EditId          int;

DECLARE @trnDate smalldatetime;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @openTable VARCHAR(30)
DECLARE @ProcDate VARCHAR(10);

DECLARE @fYear INT;


BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON


SET @trnDate = (SELECT ProcessDate FROM A2ZGLMCUS..A2ZGLPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

SET @fYear = LEFT(@fDate,4);

SET @openTable = 'A2ZCSMCUST' + CAST(@fYear AS VARCHAR(4)) + '..A2ZTRANSACTION';


DECLARE wftrnTable CURSOR FOR
SELECT TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,EditId 
FROM WFEDITA2ZTRANSACTION;

OPEN wftrnTable;
FETCH NEXT FROM wftrnTable INTO
@TrnGLAccNoDr,@TrnGLAccNoCr,@GLAccNo,@GLAmount,@GLDebitAmt,@GLCreditAmt,@EditId;

WHILE @@FETCH_STATUS = 0 
	BEGIN
   
   
    SET @strSQL = 'UPDATE ' + @openTable + ' SET TrnGLAccNoDr = ' + CAST(@TrnGLAccNoDr AS VARCHAR(8)) +
       ', TrnGLAccNoCr = ' + CAST(@TrnGLAccNoCr AS VARCHAR(8)) +
       ', GLAccNo = ' + CAST(@GLAccNo AS VARCHAR(8)) +
       ', GLAmount = ' + CAST(@GLAmount AS VARCHAR(max)) +    
       ', GLDebitAmt = ' + CAST(@GLDebitAmt AS VARCHAR(max)) +   
       ', GLCreditAmt = ' + CAST(@GLCreditAmt AS VARCHAR(max)) +   
       ',ReTrnId=' + CAST(@userid AS VARCHAR(4)) +',ReTrnDate=' + '''' + CAST(@ProcDate AS VARCHAR(10)) + '''' +  
       ' WHERE Id = ' +  CAST(@EditId AS VARCHAR(max));

   
	EXECUTE (@strSQL);

    
	FETCH NEXT FROM wftrnTable INTO
		@TrnGLAccNoDr,@TrnGLAccNoCr,@GLAccNo,@GLAmount,@GLDebitAmt,@GLCreditAmt,@EditId;


	END

CLOSE wftrnTable; 
DEALLOCATE wftrnTable;






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










