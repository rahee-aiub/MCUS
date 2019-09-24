USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSMemDataConvert]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_CSMemDataConvert] 

AS

BEGIN



DECLARE @cuType smallint;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType smallint;
DECLARE @accNo Bigint;
DECLARE @cuThana INT;

DECLARE @OldThana INT;
DECLARE @NewDivision INT;
DECLARE @NewDistrict INT;
DECLARE @NewUpzila INT;
DECLARE @NewThana INT;

DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @fdAmount money;
DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accRenwlAmt money;
DECLARE @accPeriod smallint;

DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accNoRenwl smallint;
DECLARE @accAnniDate smalldatetime;
DECLARE @newRenwlDate smalldatetime;
DECLARE @newNoRenwl smallint;
DECLARE @newRenwlAmt money;

--DECLARE @MemNo int;
DECLARE @MemPreThana int;
DECLARE @MemPerThana int;

DECLARE @AccIntRate smallmoney;
DECLARE @AccNoInstl int;
DECLARE @AccLoanInstlAmt money;
DECLARE @AccDisbAmt money;

DECLARE @accMatureDate smalldatetime;
DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;
DECLARE @noMonths int;
DECLARE @countPre int;
DECLARE @countPer int;
-----------------------------end--------------------------------


DECLARE memTable CURSOR FOR
SELECT CuType,CuNo,MemNo,MemPreThana,MemPerThana FROM A2ZMEMBER;

OPEN memTable;
FETCH NEXT FROM memTable INTO
@cuType,@cuNo,@MemNo,@MemPreThana,@MemPerThana;

WHILE @@FETCH_STATUS = 0 
	BEGIN
 
         SET @countPre = 0;
         SET @countPer = 0;

         DECLARE wfTable CURSOR FOR
         SELECT OldThana,NewDivision,NewDistrict,NewUpzila,NewThana FROM wfThana;
         OPEN wfTable;
         FETCH NEXT FROM wfTable INTO @OldThana,@NewDivision,@NewDistrict,@NewUpzila,@NewThana;

         WHILE @@FETCH_STATUS = 0 
	     BEGIN        

           IF @MemPreThana=@OldThana
              BEGIN                   
                  SET @countPre = 1;
                  UPDATE A2ZMEMBER SET MemPreDivi=@NewDivision,MemPreDist=@NewDistrict,MemPreUpzila=@NewUpzila,MemPreThana=@NewThana
                  WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo=@MemNo; 
              END
           IF @MemPerThana=@OldThana
               BEGIN
               SET @countPer= 1;
                  UPDATE A2ZMEMBER SET MemPerDivi=@NewDivision,MemPerDist=@NewDistrict,MemPerUpzila=@NewUpzila,MemPerThana=@NewThana 
                  WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo=@MemNo;
               END
           
                  FETCH NEXT FROM wfTable INTO @OldThana,@NewDivision,@NewDistrict,@NewUpzila,@NewThana;
                  END
                  CLOSE wfTable; 
                  DEALLOCATE wfTable;
                    
         IF @countPre = 0
            BEGIN
                 UPDATE A2ZMEMBER SET MemPreDivi=0,MemPreDist=0,MemPreUpzila=0,MemPreThana=0 
                  WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo=@MemNo; 
            END
         
        IF @countPer = 0
            BEGIN
                 UPDATE A2ZMEMBER SET MemPerDivi=0,MemPerDist=0,MemPerUpzila=0,MemPerThana=0 
                  WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo=@MemNo; 
            END

      FETCH NEXT FROM memTable INTO
          @cuType,@cuNo,@MemNo,@MemPreThana,@MemPerThana;

	END


CLOSE memTable; 
DEALLOCATE memTable;

END















































GO
