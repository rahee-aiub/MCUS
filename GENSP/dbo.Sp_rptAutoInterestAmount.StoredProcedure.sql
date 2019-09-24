USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptAutoInterestAmount]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_rptAutoInterestAmount] (@CommonNo1 int)
AS
BEGIN


SELECT     TOP (100) PERCENT CuNo, CuNumber, MemNo, AccType, MemName, AccIntRate, CuType, CAST(AccNo AS VARCHAR(16)) AS AccNo, ProcStat, TrnDate, AccOpenDate, 
                      AccStatus, AmtOpening, AmtJul, AmtAug, AmtSep, AmtOct, AmtNov, AmtDec, AmtJan, AmtFeb, AmtMar, AmtApr, AmtMay, AmtJun, AmtProduct, AmtInterest, IntRateJul, 
                      IntRateAug, IntRateSep, IntRateOct, IntRateNov, IntRateDec, IntRateJan, IntRateFeb, IntRateMar, IntRateApr, IntRateMay, IntRateJun
FROM         dbo.WFCSINTEREST
WHERE     (AccType = @CommonNo1)
ORDER BY CuType, CuNo

--UPDATE WFCSINTEREST SET WFCSINTEREST.ProcStat = 2 
--WHERE (AccType=@CommonNo1) AND WFCSINTEREST.ProcStat <> 3;

END





















GO
