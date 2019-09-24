
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSCalculateEncashmentCPS](@cuType smallint,@cuNo int,@memNo int,@accType int,@accNo Bigint )  

--ALTER PROCEDURE [dbo].[Sp_CSCalculateEncashmentCPS]  

AS
--EXECUTE Sp_CSCalculateEncashmentCPS 3,9,1,14,1430009000010001

BEGIN

--DECLARE @cuType smallint;
--DECLARE @cuNo int;
--DECLARE @memNo int;
--DECLARE @accType smallint;
--DECLARE @accNo int;   



DECLARE @accProvBalance money;

DECLARE @ProcDate VARCHAR(10);

DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;
DECLARE @Pre3MDate smalldatetime;
DECLARE @Pre6MDate smalldatetime;
DECLARE @Pre12MDate smalldatetime;
DECLARE @Pre60MDate smalldatetime;

DECLARE @3MIntRate smallmoney;
DECLARE @6MIntRate smallmoney;
DECLARE @12MIntRate smallmoney;
DECLARE @24MIntRate smallmoney;

DECLARE @CalIntRate smallmoney;

DECLARE @calOrgInterest money;
DECLARE @calPeriod smallint;


DECLARE @AdjProvAmt money;
DECLARE @AdjProvAmtCr money;
DECLARE @AdjProvAmtDr money;

DECLARE @fdAmount money;
DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accRenwlAmt money;
DECLARE @accPeriod smallint;
DECLARE @paidMonths smallint;
DECLARE @noMonths smallint;
DECLARE @RestpaidMonths smallint;


DECLARE @accOpenDate smalldatetime;
DECLARE @accMatureDate smalldatetime;
DECLARE @accMatureAmt money;
DECLARE @accMonthlyDeposit money;
DECLARE @accTotalDep money;
DECLARE @RestTotalDep money;

DECLARE @aty60MaturedAmt money;

DECLARE @prmClosingFees money;

DECLARE @60caldeposit money;
DECLARE @60calInterest money;
DECLARE @AcalInterest money;
DECLARE @BcalInterest money;
DECLARE @CcalInterest money;
DECLARE @calInterest money;
DECLARE @Prod1Total money;
DECLARE @Prod2Total money;
DECLARE @ProdTotal money;

DECLARE @calClosingFees money;
DECLARE @calEncashment money;

DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

--SET @cuType = 3;
--SET @cuNo = 5
--SET @memNo = 0;
--SET @accType = 15;
--SET @accNo = 23800021;   


-----------------------------end--------------------------------

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2))

SET @tDate = @trnDate;

    EXECUTE Sp_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;

    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);



    SET @prmClosingFees = (SELECT PrmAccClosingFees FROM A2ZCSPARAM WHERE AccType=14);   

    SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accOpenDate = (SELECT AccOpenDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accMatureDate = (SELECT AccMatureDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accMonthlyDeposit = (SELECT AccMonthlyDeposit FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accMatureAmt = (SELECT AccMatureAmt FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accTotalDep = (SELECT AccTotalDep FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

    
    SET @fDate = @accOpenDate

    SET @Pre12MDate = (DATEADD(month,12,@fDate));
    SET @Pre60MDate = (DATEADD(month,60,@fDate));


    SET @aty60MaturedAmt = (SELECT AtyMaturedAmt FROM A2ZATYSLAB WHERE AtyAccType=14 AND AtyFlag=@memType AND AtyRecords=@accMonthlyDeposit AND AtyPeriod=60);   

    SET @accPeriod = (SELECT AccPeriod FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
--    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
 -------------------------------------------------------------------------   
    SET @paidMonths = (@accTotalDep / @accMonthlyDeposit); 

    SET @noMonths = ((DATEDIFF(m, @fDate, @tDate)) + 0);

    IF @paidMonths > @noMonths
       BEGIN
           SET @paidMonths = @noMonths;
       END


    IF @paidMonths = @accPeriod AND @tDate >= @accMatureDate 
       BEGIN       
          SET @calClosingFees  = @prmClosingFees;
          SET @calInterest = (@accMatureAmt - @accBalance);
          SET @calEncashment = ((@accBalance + @calInterest) - @calClosingFees);
       END
    ELSE
       BEGIN
          IF @accPeriod = 60
             BEGIN
                IF @tDate >= @Pre12MDate AND @paidMonths >= 12
                   BEGIN
                       SET @calClosingFees  = @prmClosingFees;
                       SET @ProdTotal = ((@paidMonths * (@paidMonths + 1) / 2) * @accMonthlyDeposit);                     
                       SET @AcalInterest = Round((((@ProdTotal * 8) / 1200)),0);
                       SET @calInterest = ((@accTotalDep + @AcalInterest) - @accBalance); 
                       SET @calEncashment = ((@accBalance + @calInterest) - @calClosingFees); 
                   END
                ELSE
                   BEGIN
                      SET @calClosingFees  = @prmClosingFees;
                      SET @calInterest = (@accTotalDep - @accBalance); 
                      SET @calEncashment = ((@accBalance + @calInterest) - @calClosingFees); 
                   END
             END
          
            

          IF @accPeriod = 120
             BEGIN
                IF @tDate >= @Pre12MDate AND @paidMonths >= 12
                   BEGIN
                        IF @tDate > @Pre60MDate AND @paidMonths >= 60
                           BEGIN
                               set @AcalInterest = 0;
                               SET @calClosingFees  = @prmClosingFees;
                               SET @60caldeposit = (@accMonthlyDeposit * 60);
                               SET @60calInterest = (@aty60MaturedAmt - @60caldeposit);  
                               SET @RestpaidMonths = (@paidMonths - 60);
                               IF @RestpaidMonths >= 12
                                  BEGIN
                                      SET @Prod1Total = ((@paidMonths * (@paidMonths + 1) / 2) * @accMonthlyDeposit); 
                                      SET @Prod2Total = ((60 * (60 + 1) / 2) * @accMonthlyDeposit);
                                      SET @ProdTotal = (@Prod1Total - @Prod2Total); 
                                      SET @AcalInterest = Round((((@ProdTotal * 8) / 1200)),0);                                                             
                                  END
                               
                               SET @calInterest = ((@accTotalDep + @60calInterest + @AcalInterest) - @accBalance);
                               SET @calEncashment = ((@accBalance + @calInterest) - @calClosingFees);  
                           END
                        ELSE
                            BEGIN
                               SET @calClosingFees  = @prmClosingFees;
                               SET @ProdTotal = ((@paidMonths * (@paidMonths + 1) / 2) * @accMonthlyDeposit);                     
                               SET @AcalInterest = Round((((@ProdTotal * 8) / 1200)),0);           
                               SET @calInterest = ((@accTotalDep + @AcalInterest) - @accBalance); 
                               SET @calEncashment = ((@accBalance + @calInterest) - @calClosingFees);              
                            END
                 
                   END
                 ELSE
                   BEGIN
                      SET @calClosingFees  = @prmClosingFees;
                      SET @calInterest = (@accTotalDep - @accBalance); 
                      SET @calEncashment = ((@accBalance + @calInterest) - @calClosingFees); 
                   END
              
             END
       END



--
--    IF  @calFDInterest > 0
--        BEGIN
--           SET @AdjProvAmt = Round(((@calFDInterest - @accProvBalance)), 0);
--
--           IF @AdjProvAmt = 0
--              BEGIN	
--                  SET @AdjProvAmtCr = 0;
--                  SET @AdjProvAmtDr = 0;
--              END
--        
--           IF @AdjProvAmt > 0
--              BEGIN	
--                  SET @AdjProvAmtCr = @AdjProvAmt;
--                  SET @AdjProvAmtDr = 0;
--              END
--        
--           IF @AdjProvAmt <0           
--              BEGIN	
--                  SET @AdjProvAmtCr = 0;
--                  SET @AdjProvAmtDr = Abs(@AdjProvAmt);
--              END
--
--        END

    
     
--
--
    UPDATE A2ZACCOUNT SET CalClosingFees=0,CalInterest=0,CalEncashment=0,CalProvAdjCr=0,CalProvAdjDr=0   
    WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo;

    UPDATE A2ZACCOUNT SET CalClosingFees=@calClosingFees,CalInterest=@calInterest,CalEncashment=@calEncashment,CalProvAdjCr=@AdjProvAmtCr,
    CalProvAdjDr=@AdjProvAmtDr,CalPeriod=@paidMonths   
    WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo;
    
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

