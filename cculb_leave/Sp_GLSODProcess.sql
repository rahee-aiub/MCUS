USE A2ZGLMCUS
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



ALTER PROCEDURE [dbo].[Sp_GLSODProcess] (@userID INT,@PDate smalldatetime)  
AS
BEGIN

DECLARE @trnDate smalldatetime;
DECLARE @HolDate smalldatetime;
DECLARE @nCount int;
DECLARE @dayName VARCHAR(9);

DECLARE @WeekDay1 VARCHAR(9);
DECLARE @WeekDay2 VARCHAR(9);

DECLARE @PrevDate smalldatetime;
DECLARE @currentDate smalldatetime;
DECLARE @lastDay int;

DECLARE @VoucherNo nvarchar(20);
DECLARE @VchNo nvarchar(20);  
DECLARE @FromCashCode int;

DECLARE @Tdate SMALLDATETIME;
DECLARE @CalDate VARCHAR(10);
DECLARE @periodFlag INT ;

/*
    periodFlag = 1 = End of Day
    periodFlag = 2 = End of Month
    periodFlag = 3 = Year End

EXECUTE Sp_GLSODProcess 1,'2016-10-05';

*/


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

		-------- For Customer Services Module Period End -------------------		
		
        
      

--		SET @PrevDate = @trnDate;
--        SET @HolDate = @trnDate;
--		SET @nCount = 0;	
--
--        SET @WeekDay1= (SELECT HolWeekDayName1 FROM  A2ZHKMCUS..A2ZWEEKHOLIDAY);
--	    SET @WeekDay2= (SELECT HolWeekDayName2 FROM  A2ZHKMCUS..A2ZWEEKHOLIDAY);
--               
--		WHILE @nCount = 0
--			BEGIN
--				SET @trnDate = DATEADD(day,1,@trnDate);
--                SET @dayName = DATEName(DW, @trnDate);
--				IF @dayName = @WeekDay1
--					CONTINUE;
--
--                IF @dayName = @WeekDay2
--					CONTINUE;				
--
--				SET @HolDate = (SELECT HolDate FROM A2ZHKMCUS..A2ZHOLIDAY WHERE HolDate = @trnDate);
--				IF @trnDate = @HolDate
--					CONTINUE
--                ELSE
--					BREAK;
--			END;
--		
--         
--        SET @currentDate = @trnDate;
--        SET @lastDay = DAY(DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));

       
       
--        IF @PDate = 0
--           BEGIN
--                SET @trnDate = (SELECT DUMMYProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER); 
--           END 
--        ELSE
--           BEGIN
--                SET @trnDate = @PDate; 
--           END 

        SET @trnDate = @PDate; 
        
		UPDATE A2ZCSMCUS..A2ZCSPARAMETER SET ProcessDate = @trnDate;

       	
		UPDATE A2ZCSMCUS..A2ZCSPARAMETER SET CurrentMonth = MONTH(ProcessDate),CurrentYear = YEAR(ProcessDate)
		-------- End of For Customer Services Module Period End -------------------		
              
		-------- For General Ledger Module Period End -------------------		

		UPDATE A2ZGLMCUS..A2ZGLPARAMETER SET ProcessDate = @trnDate;

      		
		UPDATE A2ZGLMCUS..A2ZGLPARAMETER SET CurrentMonth = MONTH(ProcessDate),CurrentYear = YEAR(ProcessDate);				
		-------- End of For General Ledger Module Period End -------------------
        
              
        SET @FromCashCode = (SELECT GLCashCode FROM A2ZCSMCUS..A2ZSYSIDS WHERE IdsNo=@userID);


        SET @VchNo = 'RenewalFDR';       
        EXECUTE A2ZCSMCUS..Sp_CSCalculateRenewalFDR @userID;
        EXECUTE A2ZCSMCUS..Sp_CSUpdateRenewalFDR @VoucherNo,@VchNo,@FromCashCode;
        
        SET @VchNo = 'Renewal6YR';    
        EXECUTE A2ZCSMCUS..Sp_CSCalculateRenewal6YR @userID;
        EXECUTE A2ZCSMCUS..Sp_CSUpdateRenewal6YR @VoucherNo,@VchNo,@FromCashCode;

		SET @VchNo = 'BenefitMSplus';    
        EXECUTE A2ZCSMCUS..Sp_CSCalculateMonthlyBenefit @userID;
        EXECUTE A2ZCSMCUS..Sp_CSUpdateMonthlyBenefit @VoucherNo,@VchNo,@FromCashCode;

        SET @VchNo = 'RenewalMSplus';    
        EXECUTE A2ZCSMCUS..Sp_CSCalculateRenewalMSplus @userID;
        EXECUTE A2ZCSMCUS..Sp_CSUpdateRenewalMSplus @VoucherNo,@VchNo,@FromCashCode;
         

        SET @VchNo = 'AnniversaryCPS';    
        EXECUTE A2ZCSMCUS..Sp_CSCalculateAnniversaryCPS @userID;
        EXECUTE A2ZCSMCUS..Sp_CSUpdateAnniversaryCPS @VoucherNo,@VchNo,@FromCashCode;

        SET @VchNo = 'AnniversaryFDR';    
        EXECUTE A2ZCSMCUS..Sp_CSCalculateAnniversaryFDR @userID;
        EXECUTE A2ZCSMCUS..Sp_CSUpdateAnniversaryFDR @VoucherNo,@VchNo,@FromCashCode;

        SET @VchNo = 'Anniversary6YR';    
        EXECUTE A2ZCSMCUS..Sp_CSCalculateAnniversary6YR @userID; 
        EXECUTE A2ZCSMCUS..Sp_CSUpdateAnniversary6YR @VoucherNo,@VchNo,@FromCashCode; 
        
        SET @VchNo = 'ProvisionCPS';    
        EXECUTE A2ZCSMCUS..Sp_CSCalculateProvisionCPS @userID;
        EXECUTE A2ZCSMCUS..Sp_CSUpdateProvisionCPS @VoucherNo,@VchNo,@FromCashCode;

        SET @VchNo = 'ProvisionFDR';    
        EXECUTE A2ZCSMCUS..Sp_CSCalculateProvisionFDR @userID;
        EXECUTE A2ZCSMCUS..Sp_CSUpdateProvisionFDR @VoucherNo,@VchNo,@FromCashCode;

        SET @VchNo = 'Provision6YR';    
        EXECUTE A2ZCSMCUS..Sp_CSCalculateProvision6YR @userID;  
        EXECUTE A2ZCSMCUS..Sp_CSUpdateProvision6YR @VoucherNo,@VchNo,@FromCashCode;  

	    EXECUTE A2ZCSMCUS..Sp_GLProvisionAdjust @VoucherNo,@VchNo,@FromCashCode,@userID;  	

		IF DAY(@trnDate) > 24 AND MONTH(@trnDate) BETWEEN 1 AND 5 OR DAY(@trnDate) > 24 AND MONTH(@trnDate) BETWEEN 7 AND 11
			BEGIN
			EXECUTE A2ZCSMCUS..Sp_CSCalculateSBProvision 12,0
			EXECUTE A2ZCSMCUS..Sp_CSPostProvisionSB @userID,@FromCashCode
		END
       
        EXECUTE A2ZCSMCUS..Sp_BoothTransactionControl;

        EXECUTE A2ZGLMCUS..Sp_GLUserCashCodeValidity;

        set @Tdate = (select top 1 TrnDate FROM A2ZCSMCUS..A2ZLOANDEFAULTER ORDER BY TrnDate desc)


        SET @CalDate = CAST(YEAR(@Tdate) AS VARCHAR(4)) + '-' +CAST(MONTH(@Tdate) AS VARCHAR(2)) + '-' + CAST(DAY(@Tdate) AS VARCHAR(2))    
       
        IF @Tdate < @PDate 
        BEGIN
           EXECUTE A2ZCSMCUS..Sp_CSGenerateLoanDefaulter @CalDate;
           EXECUTE A2ZCSMCUS..Sp_CSGeneratePensionDefaulter @CalDate;
        END

		IF YEAR(@Tdate) <> YEAR(@PDate) AND  MONTH(@Tdate) <> MONTH(@PDate)
			BEGIN
				SET @periodFlag = 3
			END
		IF YEAR(@Tdate) = YEAR(@PDate) AND  MONTH(@Tdate) <> MONTH(@PDate)
			BEGIN
				SET @periodFlag = 2
			END            
		EXECUTE A2ZHRMCUS..Sp_UpdateLeaveBalance @periodFlag;

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


