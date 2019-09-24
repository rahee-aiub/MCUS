
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSAccountTransferData]
(
@lblCuType smallint,
@lblCuNo int,
@txtMemNo int,
@txtAccType int,
@txtAccNo Bigint,
@lblTrnferCuType smallint,
@lblTrnferCuNo int,
@txtTrnMemNo int,
@lblTrnferAccNo Bigint,
@lblUserId int
)

/*
EXECUTE Sp_CSAccountTransferData 3,5,0,17,1730005000000002,3,3,0,1730003000000001,236



*/


AS
BEGIN

DECLARE @trnDate smalldatetime;
DECLARE @databaseName nvarchar(20);

DECLARE @strSQL NVARCHAR(MAX);
DECLARE @openTable VARCHAR(30)
DECLARE @ProcDate VARCHAR(10);


BEGIN TRY
	BEGIN TRANSACTION
	SET NOCOUNT ON

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);



SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))


INSERT INTO dbo.A2ZACCOUNT(AccType, AccNo,CuType,CuNo,MemNo,AccOpenDate,AccStatus,AccStatusDate,
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

SELECT AccType, @lblTrnferAccNo,@lblTrnferCuType,@lblTrnferCuNo,@txtTrnMemNo,AccOpenDate,AccStatus,AccStatusDate,
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
       UserId,@lblUserId,@trnDate,AccTodaysOpBalance
       FROM A2ZACCOUNT WHERE AccType = @txtAccType AND AccNo = @txtAccNo AND CuType = @lblCuType AND 
                             CuNo = @lblCuNo AND MemNo = @txtMemNo;

----------------------------------------------------------

UPDATE A2ZLOAN SET CuType = @lblTrnferCuType,CuNo = @lblTrnferCuNo,MemNo = @txtTrnMemNo
            WHERE CuType = @lblCuType AND CuNo = @lblCuNo AND MemNo = @txtMemNo;

--------------------------------------------------------

UPDATE A2ZPENSIONDEFAULTER SET CuType = @lblTrnferCuType,CuNo = @lblTrnferCuNo,MemNo = @txtTrnMemNo,AccNo = @lblTrnferAccNo
            WHERE AccType = @txtAccType AND AccNo = @txtAccNo AND CuType = @lblCuType AND 
                             CuNo = @lblCuNo AND MemNo = @txtMemNo;


UPDATE A2ZLOANDEFAULTER SET CuType = @lblTrnferCuType,CuNo = @lblTrnferCuNo,MemNo = @txtTrnMemNo,AccNo = @lblTrnferAccNo
            WHERE AccType = @txtAccType AND AccNo = @txtAccNo AND CuType = @lblCuType AND 
                             CuNo = @lblCuNo AND MemNo = @txtMemNo;


----------------------------------------------------------



UPDATE A2ZACCOUNT SET AccStatus = 98,AccStatusDate = @trnDate, AccTrfAccNo = @lblTrnferAccNo,ReInputBy=@lblUserId,ReInputByDate=@trnDate           
            WHERE AccType = @txtAccType AND AccNo = @txtAccNo AND CuType = @lblCuType AND 
                             CuNo = @lblCuNo AND MemNo = @txtMemNo;


UPDATE A2ZTRANSACTION SET CuType = @lblTrnferCuType,
                          CuNo = @lblTrnferCuNo,
                          MemNo = @txtTrnMemNo,
                          AccNo = @lblTrnferAccNo,
                          ReTrnId = @lblUserId,
                          ReTrnDate = @trnDate                       
            WHERE AccType = @txtAccType AND AccNo = @txtAccNo AND CuType = @lblCuType AND 
                             CuNo = @lblCuNo AND MemNo = @txtMemNo;  	




DECLARE databaseTable CURSOR FOR
SELECT DatabaseName FROM A2ZHKMCUS..A2ZDATABASE WHERE TransDatabase = 1;

OPEN databaseTable;
FETCH NEXT FROM databaseTable INTO
@databaseName;

WHILE @@FETCH_STATUS = 0 
	BEGIN
   
    SET @openTable = CAST(@databaseName AS VARCHAR(14)) + '..A2ZCSOPBALANCE';	

    SET @strSQL = 'UPDATE ' + @openTable + ' SET CuType = ' + CAST(@lblTrnferCuType AS VARCHAR(2)) +
       ', CuNo = ' + CAST(@lblTrnferCuNo AS VARCHAR(6)) +
       ', MemNo = ' + CAST(@txtTrnMemNo AS VARCHAR(6)) +
       ', AccNo = ' + CAST(@lblTrnferAccNo AS VARCHAR(16)) +     
       ' WHERE AccType = ' +  CAST(@txtAccType AS VARCHAR(2)) +
       ' AND AccNo =' + CAST(@txtAccNo AS VARCHAR (16)) +
       ' AND CuType =' + CAST(@lblCuType AS VARCHAR(2)) +
       ' AND CuNo =' + CAST(@lblCuNo AS VARCHAR(6)) +
       ' AND MemNo =' + CAST(@txtMemNo AS VARCHAR(6));  

   	
	EXECUTE (@strSQL);



    SET @openTable = CAST(@databaseName AS VARCHAR(14)) + '..A2ZTRANSACTION';	
    SET @strSQL = 'UPDATE ' + @openTable + ' SET CuType = ' + CAST(@lblTrnferCuType AS VARCHAR(2)) +
       ', CuNo = ' + CAST(@lblTrnferCuNo AS VARCHAR(6)) +
       ', MemNo = ' + CAST(@txtTrnMemNo AS VARCHAR(6)) +
       ', AccNo = ' + CAST(@lblTrnferAccNo AS VARCHAR(16)) +    

       ', ReTrnId = ' + CAST(@lblUserId AS VARCHAR(4)) +    
       ',ReTrnDate=' + '''' + CAST(@ProcDate AS VARCHAR(10)) + '''' + 
     
       ' WHERE AccType = ' +  CAST(@txtAccType AS VARCHAR(2)) +
       ' AND AccNo =' + CAST(@txtAccNo AS VARCHAR (16)) +
       ' AND CuType =' + CAST(@lblCuType AS VARCHAR(2)) +
       ' AND CuNo =' + CAST(@lblCuNo AS VARCHAR(6)) +
       ' AND MemNo =' + CAST(@txtMemNo AS VARCHAR(6));  

   
	EXECUTE (@strSQL);

    
	FETCH NEXT FROM databaseTable INTO
		@databaseName;


	END

CLOSE databaseTable; 
DEALLOCATE databaseTable;






--UPDATE A2ZCSMCUST2015..A2ZCSOPBALANCE SET CuType = @lblTrnferCuType,
--                          CuNo = @lblTrnferCuNo,
--                          MemNo = @txtTrnMemNo,
--                          AccNo = @lblTrnferAccNo         
--            WHERE AccType = @txtAccType AND AccNo = @txtAccNo AND CuType = @lblCuType AND 
--                               CuNo = @lblCuNo AND MemNo = @txtMemNo;    	



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

