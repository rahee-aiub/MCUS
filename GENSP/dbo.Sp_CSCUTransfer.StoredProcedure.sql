USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCUTransfer]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSCUTransfer](@fCuType INT,@fCuNo INT,@UserId INT,@nFlag INT)
AS
--
-- Sp_CSCUTransfer 2,2938,1,0
--
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON


	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @databaseName NVARCHAR(20);	
	DECLARE @openTable VARCHAR(30)
	DECLARE @ProcDate VARCHAR(10);

    DECLARE @CuTypeName VARCHAR(25);
    
	DECLARE @ProcessDate SMALLDATETIME;

	DECLARE @tCuType INT;
	DECLARE @tCuNo   INT;

	SET @tCuType = @fCuType + 1;
    SET @tCuNo = (SELECT CtrlRecLastNo FROM A2ZRECCTRL WHERE CtrlRecType = @tCuType);
    SET @tCuNo = @tCuNo + 1;
    UPDATE A2ZRECCTRL SET CtrlRecLastNo = @tCuNo WHERE CtrlRecType = @tCuType;


--	SET @tCuNo = (SELECT MAX(CuNo) FROM A2ZCUNION WHERE CuType = @tCuType) + 1;

	SET @ProcessDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

--==================   UPDATE CREDIT UNION TABLE =============
	IF @fCuType = 1
		BEGIN
            SET @CuTypeName = 'Associate';   
			UPDATE A2ZCUNION SET CuAffiCuType = @fCuType,CuAffiCuNo = @fCuNo,
			CuAssoCuType = @tCuType,CuAssoCuTypeName = @CuTypeName,CuAssoCuNo = @tCuNo 
			WHERE CuType = @fCuType AND CuNo = @fCuNo;
		END

	IF @fCuType = 2
		BEGIN
            SET @CuTypeName = 'Regular';
			UPDATE A2ZCUNION SET CuAssoCuType = @fCuType,CuAssoCuNo = @fCuNo,
			CuReguCuType = @tCuType,CuReguCuTypeName = @CuTypeName,CuReguCuNo = @tCuNo 
			WHERE CuType = @fCuType AND CuNo = @fCuNo;
		END

	INSERT INTO A2ZCUNION(CuType,CuTypeName,CuNo,CuName,CuOpDt,CuFlag,CuCertNo,CuAddL1,CuAddL2,
	CuAddL3,CuTel,CuMobile,CuFax,CuEmail,CuDivi,CuDist,CuUpzila,CuThana,CuProcFlag,CuProcDesc,
	CuStatus,CuStatusDate,CuAffiCuType,CuAffiCuTypeName,CuAffiCuNo,CuAssoCuType,CuAssoCuTypeName,
	CuAssoCuNo,CuReguCuType,CuReguCuTypeName,CuReguCuNo,CuOldCuNo,GLCashCode,InputBy,VerifyBy,ApprovBy, 
	InputByDate,VerifyByDate,ApprovByDate,UserId)	
	SELECT @tCuType,@CuTypeName,@tCuNo,CuName,CuOpDt,CuFlag,CuCertNo,CuAddL1,CuAddL2,
	CuAddL3,CuTel,CuMobile,CuFax,CuEmail,CuDivi,CuDist,CuUpzila,CuThana,CuProcFlag,CuProcDesc,
	CuStatus,CuStatusDate,CuAffiCuType,CuAffiCuTypeName,CuAffiCuNo,CuAssoCuType,CuAssoCuTypeName,
	CuAssoCuNo,CuReguCuType,CuReguCuTypeName,CuReguCuNo,CuOldCuNo,GLCashCode,InputBy,VerifyBy,ApprovBy, 
	InputByDate,VerifyByDate,ApprovByDate,@UserId
	FROM A2ZCUNION WHERE CuType = @fCuType AND CuNo = @fCuNo;

	UPDATE A2ZCUNION SET CuStatus = 9,CuStatusDate = @ProcessDate WHERE CuType = @fCuType AND CuNo = @fCuNo;

--==================   END OF UPDATE CREDIT UNION TABLE =============

--==================   UPDATE MEMBER TABLE =============

	UPDATE A2ZMEMBER SET CuType = @tCuType,CuNo = @tCuNo WHERE CuType = @fCuType AND CuNo = @fCuNo;

--==================   END OF UPDATE MEMBER TABLE =============

--==================   INSERT NEW ACCOUNT =============
	INSERT INTO dbo.A2ZACCOUNT(AccType,AccNo,
	CuType,CuNo,MemNo,AccOpenDate,AccStatus,AccStatusDate,
	AccStatusNote,AccMonthlyDeposit,AccPeriod,AccDebitFlag,AccIntFlag,AccMatureDate,AccMatureAmt,
	AccIntType,AccAutoRenewFlag,AccIntRate,AccContractIntFlag,AccSpecialNote,AccFixedAmt,AccFixedMthInt,
	AccBenefitDate,AccBalance,AccPrincipal,AccInterest,AccTotalDep,AccTotIntPaid,AccTotPenaltyPaid,
	AccLienAmt,AccLastTrnTypeU,AccLastTrnDateU,AccLastTrnAmtU,AccLastTrnTypeS,AccLastTrnDateS,AccLastTrnAmtS,
	AccOrgAmt,AccFirstRenwlDate,AccRenwlDate,AccPrevRenwlDate,AccRenwlAmt,AccPrevRenwlAmt,AccAnniDate,
	AccPrevAnniDate,AccAnniAmt,AccPrevAnniAmt,AccIntWdrawn,AccTotIntWdrawn,AccNoAnni,AccNoRenwl,
	AccNoBenefit,AccLastIntCr,AccRaNetInt,AccAdjChgd,AccPrinChgd,AccLoanAppNo,AccLoanSancAmt,
	AccLoanSancDate,AccLoanExpiryDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccLoanFirstInstlDt,
	AccLoanGrace,AccDisbAmt,AccDisbDate,AccODIntDate,AccProvBalance,AccProvCalDate,AccDuePrincAmt,
	AccDueIntAmt,AccDuePenalAmt,AccLastDuePrincAmt,AccLastDueIntAmt,AccAtyClass,OldCuNo,AccCertNo,
	UserId,InputBy,InputByDate,AccTodaysOpBalance) 
	SELECT AccType,
	(LEFT(AccNo,2) + CAST(@tCuType AS NVARCHAR(2)) + RIGHT('0000' + CAST(@tCuNo AS NVARCHAR(4)),4) + RIGHT(AccNo,9)),
	@tCuType,@tCuNo,MemNo,AccOpenDate,AccStatus,AccStatusDate,
	AccStatusNote,AccMonthlyDeposit,AccPeriod,AccDebitFlag,AccIntFlag,AccMatureDate,AccMatureAmt,
	AccIntType,AccAutoRenewFlag,AccIntRate,AccContractIntFlag,AccSpecialNote,AccFixedAmt,AccFixedMthInt,
	AccBenefitDate,AccBalance,AccPrincipal,AccInterest,AccTotalDep,AccTotIntPaid,AccTotPenaltyPaid,
	AccLienAmt,AccLastTrnTypeU,AccLastTrnDateU,AccLastTrnAmtU,AccLastTrnTypeS,AccLastTrnDateS,AccLastTrnAmtS,
	AccOrgAmt,AccFirstRenwlDate,AccRenwlDate,AccPrevRenwlDate,AccRenwlAmt,AccPrevRenwlAmt,AccAnniDate,
	AccPrevAnniDate,AccAnniAmt,AccPrevAnniAmt,AccIntWdrawn,AccTotIntWdrawn,AccNoAnni,AccNoRenwl,
	AccNoBenefit,AccLastIntCr,AccRaNetInt,AccAdjChgd,AccPrinChgd,AccLoanAppNo,AccLoanSancAmt,
	AccLoanSancDate,AccLoanExpiryDate,AccNoInstl,AccLoanInstlAmt,AccLoanLastInstlAmt,AccLoanFirstInstlDt,
	AccLoanGrace,AccDisbAmt,AccDisbDate,AccODIntDate,AccProvBalance,AccProvCalDate,AccDuePrincAmt,
	AccDueIntAmt,AccDuePenalAmt,AccLastDuePrincAmt,AccLastDueIntAmt,AccAtyClass,OldCuNo,AccCertNo,
	UserId,@UserId,@ProcessDate,AccTodaysOpBalance
	FROM A2ZACCOUNT 
	WHERE CuType = @fCuType AND CuNo = @fCuNo;
--==================   END OF INSERT NEW ACCOUNT =============

--==================   UPDATE OTHER TABLE =============
	UPDATE A2ZLOAN SET CuType = @tCuType,CuNo = @tCuNo WHERE CuType = @fCuType AND CuNo = @fCuNo;

	UPDATE A2ZPENSIONDEFAULTER SET CuType = @tCuType,CuNo = @tCuNo,
	AccNo = (LEFT(AccNo,2) + CAST(@tCuType AS NVARCHAR(2)) + RIGHT('0000' + CAST(@tCuNo AS NVARCHAR(4)),4) + RIGHT(AccNo,9))
	WHERE CuType = @fCuType AND CuNo = @fCuNo;
    
	UPDATE A2ZLOANDEFAULTER SET CuType = @tCuType,CuNo = @tCuNo,
	AccNo = (LEFT(AccNo,2) + CAST(@tCuType AS NVARCHAR(2)) + RIGHT('0000' + CAST(@tCuNo AS NVARCHAR(4)),4) + RIGHT(AccNo,9))
	WHERE CuType = @fCuType AND CuNo = @fCuNo;
--==================   END OF UPDATE OTHER TABLE =============

--=================  PREVIOUS ACCOUNT NUMBER ============
	UPDATE A2ZACCOUNT SET AccStatus = 98,AccStatusDate = @ProcessDate,
	AccTrfAccNo = (LEFT(AccNo,2) + CAST(@tCuType AS NVARCHAR(2)) + RIGHT('0000' + CAST(@tCuNo AS NVARCHAR(4)),4) + RIGHT(AccNo,9)),
	ReInputBy=@UserId,ReInputByDate=@ProcessDate
	WHERE CuType = @fCuType AND CuNo = @fCuNo;
--=================  END OF PREVIOUS ACCOUNT NUMBER ============

--================== CURRENT TRANSACTION TABLE =================
	UPDATE A2ZTRANSACTION SET CuType = @tCuType,CuNo = @tCuNo,
    AccNo = (LEFT(AccNo,2) + CAST(@tCuType AS NVARCHAR(2)) + RIGHT('0000' + CAST(@tCuNo AS NVARCHAR(4)),4) + RIGHT(AccNo,9)),
	ReTrnId = @UserId,ReTrnDate = @ProcessDate
	WHERE CuType = @fCuType AND CuNo = @fCuNo;
--================== END OF CURRENT TRANSACTION TABLE =================

--================== UPDATE YEAR WISE OPENING AND TRANSACTION DATA ===============
	DECLARE databaseTable CURSOR FOR
	SELECT DatabaseName FROM A2ZHKMCUS..A2ZDATABASE WHERE TransDatabase = 1;

	OPEN databaseTable;
	FETCH NEXT FROM databaseTable INTO @databaseName;

	WHILE @@FETCH_STATUS = 0 
		BEGIN
	   
			SET @openTable = CAST(@databaseName AS VARCHAR(14)) + '..A2ZCSOPBALANCE';	

			SET @strSQL = 'UPDATE ' + @openTable + ' SET CuType = ' + CAST(@tCuType AS VARCHAR(2)) +
				',CuNo = ' + CAST(@tCuNo AS VARCHAR(6)) +
				',AccNo = (LEFT(AccNo,2) + CAST(' + CAST(@tCuType AS NVARCHAR(2)) + ' AS NVARCHAR(2)) + RIGHT(' + '''' + '0000' + 
				'''' + '+ CAST(' + CAST(@tCuNo AS NVARCHAR(4)) + ' AS NVARCHAR(4)),4) + RIGHT(AccNo,9))' +
				' WHERE CuType = ' + CAST(@fCuType AS VARCHAR(2)) +
				' AND CuNo = ' + CAST(@fCuNo AS VARCHAR(6));
	   	
			EXECUTE (@strSQL);

			SET @openTable = CAST(@databaseName AS VARCHAR(14)) + '..A2ZTRANSACTION';


			SET @strSQL = 'UPDATE ' + @openTable + ' SET CuType = ' + CAST(@tCuType AS VARCHAR(2)) +
				',CuNo = ' + CAST(@tCuNo AS VARCHAR(6)) +
				',AccNo = (LEFT(AccNo,2) + CAST(' + CAST(@tCuType AS NVARCHAR(2)) + ' AS NVARCHAR(2)) + RIGHT(' + '''' + '0000' + 
				'''' + '+ CAST(' + CAST(@tCuNo AS NVARCHAR(4)) + ' AS NVARCHAR(4)),4) + RIGHT(AccNo,9))' +
				' WHERE CuType = ' + CAST(@fCuType AS VARCHAR(2)) +
				' AND CuNo = ' + CAST(@fCuNo AS VARCHAR(6));

			EXECUTE (@strSQL);

			FETCH NEXT FROM databaseTable INTO	@databaseName;
		END

	CLOSE databaseTable; 
	DEALLOCATE databaseTable;
--================== END OF UPDATE YEAR WISE OPENING AND TRANSACTION DATA ===============

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
