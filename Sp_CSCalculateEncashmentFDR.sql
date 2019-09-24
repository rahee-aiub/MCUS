
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSCalculateEncashmentFDR](@cuType smallint,@cuNo int,@memNo int,@accType int,@accNo Bigint )  

--ALTER PROCEDURE [dbo].[Sp_CSCalculateEncashmentFDR]  

AS
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
DECLARE @Pre24MDate smalldatetime;

DECLARE @3MIntRate smallmoney;
DECLARE @6MIntRate smallmoney;
DECLARE @12MIntRate smallmoney;
DECLARE @24MIntRate smallmoney;

DECLARE @CalIntRate smallmoney;
DECLARE @calFDInterest money;
DECLARE @calOrgInterest money;
DECLARE @calPeriod smallint;
DECLARE @calFDEncashment money;

DECLARE @AdjProvAmt money;
DECLARE @AdjProvAmtCr money;
DECLARE @AdjProvAmtDr money;

DECLARE @fdAmount money;
DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accRenwlAmt money;
DECLARE @accPeriod smallint;


DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
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

    EXECUTE SpM_CSGenerateSingleAccountBalance @accNo,@ProcDate,0;

    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccNo = @accNo);


    SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accOpenDate = (SELECT AccOpenDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accRenwlDate = (SELECT AccRenwlDate FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accPeriod = (SELECT AccPeriod FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
--    SET @accBalance = (SELECT AccBalance FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accOrgAmt = (SELECT AccOrgAmt FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
    SET @accRenwlAmt = (SELECT AccRenwlAmt FROM A2ZACCOUNT WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);

	SET @3MIntRate = (SELECT AtyIntRate FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = @memType AND AtyPeriod = 3);
  
    SET @6MIntRate = (SELECT AtyIntRate FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = @memType AND AtyPeriod = 6);

    SET @12MIntRate = (SELECT AtyIntRate FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = @memType AND AtyPeriod = 12);

    SET @24MIntRate = (SELECT AtyIntRate FROM A2ZATYSLAB WHERE AtyAccType = @accType AND
						AtyFlag = @memType AND AtyPeriod = 24);

    
    SET @accProvBalance = (SELECT AccProvBalance FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo);


    IF @accRenwlDate IS NULL
       BEGIN			
           SET @fdAmount = @accOrgAmt
           SET @fDate = @accOpenDate
       END
    ELSE	 
       BEGIN
           SET @fdAmount =  @accRenwlAmt
	       SET @fDate = @accRenwlDate
       END
	
    


    SET @Pre3MDate = (DATEADD(month,3,@fDate));
    SET @Pre6MDate = (DATEADD(month,6,@fDate));
    SET @Pre12MDate = (DATEADD(month,12,@fDate));
    SET @Pre24MDate = (DATEADD(month,24,@fDate));

    SET @calOrgInterest = 0;    


    IF @accPeriod = 3
       BEGIN
       SET @calFDInterest = 0
       SET @calFDEncashment = @accBalance 
       END

    IF @accPeriod = 6
       BEGIN
       IF @tDate >= @Pre3MDate
          BEGIN
              SET @CalIntRate = @3MIntRate;    
              SET @CalPeriod = 3;            
              SET @noDays = ((DATEDIFF(d, @fDate, @Pre3MDate)) + 1);
--              SET @calFDInterest = Round((((@fdAmount * @3MIntRate * @noDays) / 36500)), 0);
              SET @calFDInterest = Round((((@fdAmount * @3MIntRate * 3) / 1200)), 0);
              SET @calOrgInterest = @calFDInterest;
              SET @calFDEncashment = (@accBalance + @calFDInterest)
              SET @calFDEncashment = @accBalance
          END
       ELSE
          BEGIN
              SET @calFDInterest = 0
              SET @calFDEncashment = @accBalance 
          END 
    END

    IF @accPeriod = 12
       BEGIN
       IF @tDate >= @Pre6MDate
          BEGIN
              SET @CalIntRate = @6MIntRate;  
              SET @CalPeriod = 6;     
              SET @noDays = ((DATEDIFF(d, @fDate, @Pre6MDate)) + 1);
--              SET @calFDInterest = Round((((@fdAmount * @6MIntRate * @noDays) / 36500)), 0);
              SET @calFDInterest = Round((((@fdAmount * @6MIntRate * 6) / 1200)), 0);
              SET @calOrgInterest = @calFDInterest;
              SET @calFDEncashment = (@accBalance + @calFDInterest)
              SET @calFDEncashment = @accBalance 
          END
       ELSE
       IF @tDate >= @Pre3MDate
          BEGIN
              SET @CalIntRate = @3MIntRate;   
              SET @CalPeriod = 3;    
              SET @noDays = ((DATEDIFF(d, @fDate, @Pre3MDate)) + 1);
--              SET @calFDInterest = Round((((@fdAmount * @3MIntRate * @noDays) / 36500)), 0);
              SET @calFDInterest = Round((((@fdAmount * @3MIntRate * 3) / 1200)), 0);
              SET @calOrgInterest = @calFDInterest;
              SET @calFDEncashment = (@accBalance + @calFDInterest)
              SET @calFDEncashment = @accBalance
          END
       ELSE
          BEGIN
              SET @calFDInterest = 0
              SET @calFDEncashment = @accBalance 
          END
    END
    
    IF @accPeriod = 24 
       BEGIN
       IF @tDate >= @Pre12MDate
          BEGIN
              SET @CalIntRate = @12MIntRate;   
              SET @CalPeriod = 12;    
              SET @noDays = ((DATEDIFF(d, @fDate, @Pre12MDate)) + 1);
--              SET @calFDInterest = Round((((@fdAmount * @12MIntRate * @noDays) / 36500)), 0);  
              SET @calFDInterest = Round((((@fdAmount * @12MIntRate * 12) / 1200)), 0);
              SET @calOrgInterest = @calFDInterest;
              SET @calFDEncashment = (@fdAmount + @calFDInterest) 
              SET @calFDInterest = (@calFDEncashment - @accBalance)
              IF  @calFDInterest < 0
                  BEGIN 
                      SET @calFDEncashment = (@accBalance + @calFDInterest)
                  END
              ELSE
                  BEGIN
                      SET @calFDEncashment = (@accBalance)
                  END                              
           END      
       ELSE
       IF @tDate >= @Pre6MDate
          BEGIN
             SET @CalIntRate = @6MIntRate;   
             SET @CalPeriod = 6;    
             SET @noDays = ((DATEDIFF(d, @fDate, @Pre6MDate)) + 1);
--            SET @calFDInterest = Round((((@fdAmount * @6MIntRate * @noDays) / 36500)), 0);
             SET @calFDInterest = Round((((@fdAmount * @6MIntRate * 6) / 1200)), 0);
             SET @calOrgInterest = @calFDInterest;
             SET @calFDEncashment = (@accBalance + @calFDInterest)
             SET @calFDEncashment = @accBalance 
          END
       ELSE
       IF @tDate >= @Pre3MDate
          BEGIN
             SET @CalIntRate = @3MIntRate;   
             SET @CalPeriod = 3;    
             SET @noDays = ((DATEDIFF(d, @fDate, @Pre3MDate)) + 1);
--            SET @calFDInterest = Round((((@fdAmount * @3MIntRate * @noDays) / 36500)), 0);
             SET @calFDInterest = Round((((@fdAmount * @3MIntRate * 3) / 1200)), 0);
             SET @calOrgInterest = @calFDInterest;
             SET @calFDEncashment = (@accBalance + @calFDInterest)
             SET @calFDEncashment = @accBalance
          END     
       ELSE
          BEGIN
             SET @calFDInterest = 0
             SET @calFDEncashment = @accBalance 
          END
    END
    
	


    IF @accPeriod = 36 
       BEGIN
       IF @tDate >= @Pre24MDate
          BEGIN
              SET @CalIntRate = @24MIntRate;   
              SET @CalPeriod = 24;    
              SET @noDays = ((DATEDIFF(d, @fDate, @Pre24MDate)) + 1);
--              SET @calFDInterest = Round((((@fdAmount * @24MIntRate * @noDays) / 36500)), 0);
              SET @calFDInterest = Round((((@fdAmount * @24MIntRate * 24) / 1200)), 0);
              SET @calOrgInterest = @calFDInterest;
              SET @calFDEncashment = (@fdAmount + @calFDInterest) 
              SET @calFDInterest = (@calFDEncashment - @accBalance)  
              IF  @calFDInterest < 0
                  BEGIN 
                       SET @calFDEncashment = (@accBalance + @calFDInterest)
                  END
              ELSE
                  BEGIN
                      SET @calFDEncashment = (@accBalance)
                  END                    
          END    
       ELSE
          IF @tDate >= @Pre12MDate
             BEGIN
                 SET @CalIntRate = @12MIntRate;   
                 SET @CalPeriod = 12;    
                 SET @noDays = ((DATEDIFF(d, @fDate, @Pre12MDate)) + 1);
--                 SET @calFDInterest = Round((((@fdAmount * @12MIntRate * @noDays) / 36500)), 0);
                 SET @calFDInterest = Round((((@fdAmount * @12MIntRate * 12) / 1200)), 0);
                 SET @calOrgInterest = @calFDInterest;
                 SET @calFDEncashment = (@fdAmount + @calFDInterest) 
                 SET @calFDInterest = (@calFDEncashment - @accBalance)
                 IF  @calFDInterest < 0
                     BEGIN 
                         SET @calFDEncashment = (@accBalance + @calFDInterest)
                     END
                 ELSE
                     BEGIN
                         SET @calFDEncashment = (@accBalance)
                     END   
                   
               END
            
         ELSE
           IF @tDate >= @Pre6MDate
              BEGIN
                 SET @CalIntRate = @6MIntRate;   
                 SET @CalPeriod = 6;    
                 SET @noDays = ((DATEDIFF(d, @fDate, @Pre6MDate)) + 1);
--                 SET @calFDInterest = Round((((@fdAmount * @6MIntRate * @noDays) / 36500)), 0);
                 SET @calFDInterest = Round((((@fdAmount * @6MIntRate * 6) / 1200)), 0);
                 SET @calOrgInterest = @calFDInterest;
                 SET @calFDEncashment = (@accBalance + @calFDInterest)
                 SET @calFDEncashment = @accBalance 
              END
          ELSE
          IF @tDate >= @Pre3MDate
             BEGIN
                 SET @CalIntRate = @3MIntRate;   
                 SET @CalPeriod = 3;    
                 SET @noDays = ((DATEDIFF(d, @fDate, @Pre3MDate)) + 1);
--                 SET @calFDInterest = Round((((@fdAmount * @3MIntRate * @noDays) / 36500)), 0);
                 SET @calFDInterest = Round((((@fdAmount * @3MIntRate * 3) / 1200)), 0);
                 SET @calOrgInterest = @calFDInterest;
                 SET @calFDEncashment = (@accBalance + @calFDInterest)
                 SET @calFDEncashment = @accBalance
             END
          ELSE
          BEGIN
              SET @calFDInterest = 0
              SET @calFDEncashment = @accBalance 
          END
      
    END

--    IF  @calFDInterest > 0
--        BEGIN
           SET @AdjProvAmt = Round(((@calFDInterest - @accProvBalance)), 0);

           IF @AdjProvAmt = 0
              BEGIN	
                  SET @AdjProvAmtCr = 0;
                  SET @AdjProvAmtDr = 0;
              END
        
           IF @AdjProvAmt > 0
              BEGIN	
                  SET @AdjProvAmtCr = @AdjProvAmt;
                  SET @AdjProvAmtDr = 0;
              END
        
           IF @AdjProvAmt <0           
              BEGIN	
                  SET @AdjProvAmtCr = 0;
                  SET @AdjProvAmtDr = Abs(@AdjProvAmt);
              END

--        END

    
     


    UPDATE A2ZACCOUNT SET CalInterest=0,CalEncashment=0,CalProvAdjCr=0,CalProvAdjDr=0   
    WHERE AccType = @accType AND AccNo = @accNo AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo;

    UPDATE A2ZACCOUNT SET CalIntRate=@CalIntRate,CalFDAmount = @fdAmount,CalNofDays=@noDays,
    CalPeriod=@calPeriod,CalOrgInterest=@calOrgInterest,CalInterest=@calFDInterest,CalEncashment=@calFDEncashment,CalProvAdjCr=@AdjProvAmtCr,
    CalProvAdjDr=@AdjProvAmtDr,CalFDate=@fDate   
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

----exec Sp_CSProcessRenewalFDR
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

