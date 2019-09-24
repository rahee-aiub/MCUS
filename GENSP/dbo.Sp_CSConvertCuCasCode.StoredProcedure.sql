USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSConvertCuCasCode]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_CSConvertCuCasCode] 

AS

BEGIN


/*

EXECUTE Sp_CSConvertCuCasCode

*/

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
DECLARE @glCashCode INT;
-----------------------------end--------------------------------


UPDATE A2ZCUNION SET GLCashCode=0; 

DECLARE cuTable CURSOR FOR
SELECT CuNo,GLCashCode FROM WFCUCASHCODE;

OPEN cuTable;
FETCH NEXT FROM cuTable INTO @cuNo,@glCashCode;

WHILE @@FETCH_STATUS = 0 
	BEGIN
     
      UPDATE A2ZCUNION SET GLCashCode=@glCashCode WHERE CuNo = @cuNo; 
          

      FETCH NEXT FROM cuTable INTO @cuNo,@glCashCode;

	END


CLOSE cuTable; 
DEALLOCATE cuTable;

END


















































GO
