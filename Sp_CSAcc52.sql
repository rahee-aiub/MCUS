USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_CSAcc52]    Script Date: 08/28/2018 12:06:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER PROCEDURE [dbo].[Sp_CSAcc52] 

AS

BEGIN

/*

EXECUTE Sp_CSAcc52

*/


DECLARE @cuType smallint;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType smallint;
DECLARE @accNo Bigint;


 UPDATE A2ZACCOUNT SET AccPeriod=12 WHERE AccType = 52 AND AccPeriod = 0; 


 UPDATE A2ZACCOUNT SET AccLoanExpiryDate=(DATEADD(MONTH,AccPeriod,AccOpenDate)) WHERE AccType = 52 AND AccRenwlDate is null; 

 UPDATE A2ZACCOUNT SET AccLoanExpiryDate=(DATEADD(MONTH,AccPeriod,AccRenwlDate)) WHERE AccType = 52 AND AccRenwlDate is not null; 


END




















































GO

