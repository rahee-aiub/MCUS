USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateMemberHelp]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_UpdateMemberHelp] 

AS

BEGIN



DECLARE @cuType smallint;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType smallint;
DECLARE @accNo Bigint;

DECLARE @trnDate smalldatetime;
DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @fdAmount money;
DECLARE @accBalance money;
DECLARE @accTotalDep money;
DECLARE @accRenwlAmt money;
DECLARE @accPeriod int;
DECLARE @accMonthlyDeposit money;

DECLARE @accLastTrnDateU smalldatetime;
DECLARE @accOpenDate smalldatetime;


DECLARE @accRenwlDate smalldatetime;
DECLARE @accNoRenwl smallint;
DECLARE @accAnniDate smalldatetime;
DECLARE @newRenwlDate smalldatetime;
DECLARE @newNoRenwl smallint;
DECLARE @newRenwlAmt money;

DECLARE @accMatureDate smalldatetime;
DECLARE @memType int;
DECLARE @RoundFlag tinyint;
DECLARE @noDays int;
DECLARE @noMonths int;
DECLARE @countR int;

DECLARE @RESULT MONEY
DECLARE @DECVALUE MONEY;
-----------------------------end--------------------------------


UPDATE A2ZMEMBERHELP SET A2ZMEMBERHELP.GLCashCode = A2ZCUNION.GLCashCode
FROM A2ZMEMBERHELP,A2ZCUNION
WHERE A2ZMEMBERHELP.CuType = A2ZCUNION.CuType AND A2ZMEMBERHELP.CuNo = A2ZCUNION.CuNo;       



END






















































GO
