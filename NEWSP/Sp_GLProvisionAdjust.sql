USE [A2ZCSMCUS]
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


CREATE PROCEDURE [dbo].[Sp_GLProvisionAdjust](@VoucherNo nvarchar(20),@VchNo nvarchar(20), @FromCashCode int, @userID int)  

AS
--EXECUTE Sp_TEST 1,1,1

BEGIN

DECLARE @cstotprov money;
DECLARE @gltotprov money;
DECLARE @diftotprov money;
DECLARE @glcrtrn money;
DECLARE @gldrtrn money;
DECLARE @trnDate smalldatetime;
DECLARE @yy VARCHAR(4);
DECLARE @mm VARCHAR(2);
DECLARE @dd VARCHAR(2);
DECLARE @fDate VARCHAR(10);
DECLARE @tDate VARCHAR(10);


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

SET @trnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);
SET @yy = year(@trnDate) ;
SET @mm = month(@trnDate);
SET @dd = day(@trnDate);
SET @fDate = @yy + '-' + @mm + '-' + @dd ; 
SET @tDate = @yy + '-' + @mm + '-' + @dd ; 


-------- START FDR PROVISION ADJUSTMENT ---------

EXECUTE A2ZGLMCUS..Sp_GLAccountStatement 20401001,@fDate,@tDate,0

SET @cstotprov = (SELECT SUM(A2ZCSMCUS..A2ZACCOUNT.AccProvBalance ) AS FDRCSPROV
						FROM A2ZCSMCUS..A2ZACCOUNT
						WHERE     (AccType = 15) AND (AccProvBalance <> 0) AND AccStatus < 98) ;

SET @glcrtrn = (SELECT SUM(A2ZGLMCUS..WFGLSTATEMENT.GLCreditAmt) AS GLCREDIT
			   FROM A2ZGLMCUS..WFGLSTATEMENT
			   WHERE     GLAccNo = 20401001);

SET @gldrtrn = (SELECT SUM(A2ZGLMCUS..WFGLSTATEMENT.GLDebitAmt) AS GLDEBIT
			   FROM A2ZGLMCUS..WFGLSTATEMENT
			   WHERE     GLAccNo = 20401001);

SET @gltotprov = @glcrtrn - @gldrtrn

SET @diftotprov =(@cstotprov-@gltotprov)


IF @diftotprov < 0
BEGIN
	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,0,'ProvAdj.GL','V',2,20401001,40105001,20401001,@diftotprov,ABS(@diftotprov),
			0,@FromCashCode,2,@trnDate,@userID);

	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,1,'ProvAdj.GL','V',2,20401001,40105001,40105001,ABS(@diftotprov),0,
			ABS(@diftotprov),@FromCashCode,2,@trnDate,@userID);
END

IF @diftotprov > 0
BEGIN
	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,0,'ProvAdj.GL','V',2,50103001,20401001,20401001,@diftotprov,0,
			ABS(@diftotprov),@FromCashCode,2,@trnDate,@userID);

	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,1,'ProvAdj.GL','V',2,50103001,20401001,50103001,ABS(@diftotprov),ABS(@diftotprov),
			0,@FromCashCode,2,@trnDate,@userID);
END

-------- END FDR PROVISION ADJUSTMENT ---------

-------- START 6YR PROVISION ADJUSTMENT ---------

EXECUTE A2ZGLMCUS..Sp_GLAccountStatement 20403001,@fDate,@tDate,0

SET @cstotprov = (SELECT SUM(A2ZCSMCUS..A2ZACCOUNT.AccProvBalance ) AS FDRCSPROV
						FROM A2ZCSMCUS..A2ZACCOUNT
						WHERE     (AccType = 16) AND (AccProvBalance <> 0) AND AccStatus < 98) ;

SET @glcrtrn = (SELECT SUM(A2ZGLMCUS..WFGLSTATEMENT.GLCreditAmt) AS GLCREDIT
			   FROM A2ZGLMCUS..WFGLSTATEMENT
			   WHERE     GLAccNo = 20403001);

SET @gldrtrn = (SELECT SUM(A2ZGLMCUS..WFGLSTATEMENT.GLDebitAmt) AS GLDEBIT
			   FROM A2ZGLMCUS..WFGLSTATEMENT
			   WHERE     GLAccNo = 20403001);

SET @gltotprov = @glcrtrn - @gldrtrn

SET @diftotprov =(@cstotprov-@gltotprov)


IF @diftotprov < 0
BEGIN
	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,0,'ProvAdj.GL','V',2,20403001,40105004,20403001,@diftotprov,ABS(@diftotprov),
			0,@FromCashCode,2,@trnDate,@userID);

	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,1,'ProvAdj.GL','V',2,20403001,40105004,40105004,ABS(@diftotprov),0,
			ABS(@diftotprov),@FromCashCode,2,@trnDate,@userID);
END

IF @diftotprov > 0
BEGIN
	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,0,'ProvAdj.GL','V',2,50104001,20403001,20403001,@diftotprov,0,
			ABS(@diftotprov),@FromCashCode,2,@trnDate,@userID);

	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,1,'ProvAdj.GL','V',2,50104001,20403001,50104001,ABS(@diftotprov),ABS(@diftotprov),
			0,@FromCashCode,2,@trnDate,@userID);
END

-------- END 6YR PROVISION ADJUSTMENT ---------

-------- START MS+ PROVISION ADJUSTMENT ---------

EXECUTE A2ZGLMCUS..Sp_GLAccountStatement 20405001,@fDate,@tDate,0

SET @cstotprov = (SELECT SUM(A2ZCSMCUS..A2ZACCOUNT.AccProvBalance ) AS FDRCSPROV
						FROM A2ZCSMCUS..A2ZACCOUNT
						WHERE     (AccType = 17) AND (AccProvBalance <> 0) AND AccStatus < 98) ;

SET @glcrtrn = (SELECT SUM(A2ZGLMCUS..WFGLSTATEMENT.GLCreditAmt) AS GLCREDIT
			   FROM A2ZGLMCUS..WFGLSTATEMENT
			   WHERE     GLAccNo = 20405001);

SET @gldrtrn = (SELECT SUM(A2ZGLMCUS..WFGLSTATEMENT.GLDebitAmt) AS GLDEBIT
			   FROM A2ZGLMCUS..WFGLSTATEMENT
			   WHERE     GLAccNo = 20405001);

SET @gltotprov = @glcrtrn - @gldrtrn

SET @diftotprov =(@cstotprov-@gltotprov)


IF @diftotprov < 0
BEGIN
	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,0,'ProvAdj.GL','V',2,20405001,40105006,20405001,@diftotprov,ABS(@diftotprov),
			0,@FromCashCode,2,@trnDate,@userID);

	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,1,'ProvAdj.GL','V',2,20405001,40105006,40105006,ABS(@diftotprov),0,
			ABS(@diftotprov),@FromCashCode,2,@trnDate,@userID);
END

IF @diftotprov > 0
BEGIN
	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,0,'ProvAdj.GL','V',2,50107001,20405001,20405001,@diftotprov,0,
			ABS(@diftotprov),@FromCashCode,2,@trnDate,@userID);

	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,1,'ProvAdj.GL','V',2,50107001,20405001,50107001,ABS(@diftotprov),ABS(@diftotprov),
			0,@FromCashCode,2,@trnDate,@userID);
END

-------- END MS+ PROVISION ADJUSTMENT ---------

-------- START CPS PROVISION ADJUSTMENT ---------

EXECUTE A2ZGLMCUS..Sp_GLAccountStatement 20402001,@fDate,@tDate,0

SET @cstotprov = (SELECT SUM(A2ZCSMCUS..A2ZACCOUNT.AccProvBalance ) AS FDRCSPROV
						FROM A2ZCSMCUS..A2ZACCOUNT
						WHERE     (AccType = 14) AND (AccProvBalance <> 0) AND AccStatus < 98) ;

SET @glcrtrn = (SELECT SUM(A2ZGLMCUS..WFGLSTATEMENT.GLCreditAmt) AS GLCREDIT
			   FROM A2ZGLMCUS..WFGLSTATEMENT
			   WHERE     GLAccNo = 20402001);

SET @gldrtrn = (SELECT SUM(A2ZGLMCUS..WFGLSTATEMENT.GLDebitAmt) AS GLDEBIT
			   FROM A2ZGLMCUS..WFGLSTATEMENT
			   WHERE     GLAccNo = 20402001);

SET @gltotprov = @glcrtrn - @gldrtrn

SET @diftotprov =(@cstotprov-@gltotprov)


IF @diftotprov < 0
BEGIN
	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,0,'ProvAdj.GL','V',2,20402001,40105005,20402001,@diftotprov,ABS(@diftotprov),
			0,@FromCashCode,2,@trnDate,@userID);

	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,1,'ProvAdj.GL','V',2,20402001,40105005,40105005,ABS(@diftotprov),0,
			ABS(@diftotprov),@FromCashCode,2,@trnDate,@userID);
END

IF @diftotprov > 0
BEGIN
	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,0,'ProvAdj.GL','V',2,50109001,20402001,20402001,@diftotprov,0,
			ABS(@diftotprov),@FromCashCode,2,@trnDate,@userID);

	INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,TrnType,TrnDrCr,TrnDesc,TrnVchType,TrnCSGL,TrnGLAccNoDr,
			TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,FromCashCode,TrnModule,ValueDate,userID)
			VALUES (@trnDate,'ProvAdj.',3,1,'ProvAdj.GL','V',2,50109001,20402001,50109001,ABS(@diftotprov),ABS(@diftotprov),
			0,@FromCashCode,2,@trnDate,@userID);
END

-------- END CPS PROVISION ADJUSTMENT ---------

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

