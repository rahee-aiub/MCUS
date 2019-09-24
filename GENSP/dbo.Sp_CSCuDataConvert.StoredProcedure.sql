USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSCuDataConvert]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_CSCuDataConvert] 

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


DECLARE @AccIntRate smallmoney;
DECLARE @AccNoInstl int;
DECLARE @AccLoanInstlAmt money;
DECLARE @AccDisbAmt money;

DECLARE @accMatureDate smalldatetime;
DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;
DECLARE @noMonths int;
DECLARE @countR int;
-----------------------------end--------------------------------


DECLARE cuTable CURSOR FOR
SELECT CuType,CuNo,CuThana FROM A2ZCUNION;

OPEN cuTable;
FETCH NEXT FROM cuTable INTO
@cuType,@cuNo,@cuThana;

WHILE @@FETCH_STATUS = 0 
	BEGIN
 
         SET @countR = 0;

         DECLARE wfTable CURSOR FOR
         SELECT OldThana,NewDivision,NewDistrict,NewUpzila,NewThana FROM wfThana;
         OPEN wfTable;
         FETCH NEXT FROM wfTable INTO @OldThana,@NewDivision,@NewDistrict,@NewUpzila,@NewThana;

         WHILE @@FETCH_STATUS = 0 
	     BEGIN        

           IF @cuThana=@OldThana
              BEGIN                   
                  SET @countR = 1;
                  UPDATE A2ZCUNION SET CuDivi=@NewDivision,CuDist=@NewDistrict,CuUpzila=@NewUpzila,CuThana=@NewThana 
                  WHERE CuType = @cuType AND CuNo = @cuNo; 
              END
          
              FETCH NEXT FROM wfTable INTO @OldThana,@NewDivision,@NewDistrict,@NewUpzila,@NewThana;
              END
              CLOSE wfTable; 
              DEALLOCATE wfTable;
              
      
         IF @countR = 0
            BEGIN
                UPDATE A2ZCUNION SET CuDivi=0,CuDist=0,CuUpzila=0,CuThana=0 
                  WHERE CuType = @cuType AND CuNo = @cuNo; 
            END


      FETCH NEXT FROM cuTable INTO
		    @cuType,@cuNo,@cuThana;;

	END


CLOSE cuTable; 
DEALLOCATE cuTable;

END
















































GO
