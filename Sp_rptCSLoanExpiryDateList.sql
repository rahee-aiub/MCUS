USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptCSLoanExpiryDateList]    Script Date: 04/18/2018 11:00:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
EXECUTE Sp_rptCSLoanExpiryDateList 52,0,0,0,'2018-03-01' 
*/

ALTER PROCEDURE [dbo].[Sp_rptCSLoanExpiryDateList] (@CommonNo1 int,@CommonNo2 int,@CommonNo3 int,@CommonNo4 int, @fDate VARCHAR(10))
AS
BEGIN

DECLARE @trnDate smalldatetime;
DECLARE @ProcDate VARCHAR(10);

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

SET @ProcDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@fDate) AS VARCHAR(2)) + '-' + CAST(DAY(@fDate) AS VARCHAR(2))


EXECUTE Sp_CSAccountLedgerBalance @CommonNo1,@ProcDate;


IF @CommonNo2 = 0 AND @CommonNo4 = 0
   BEGIN

        SELECT     dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16)) as AccNo, dbo.A2ZACCOUNT.MemNo, dbo.A2ZMEMBER.MemName, 
                      dbo.A2ZACCOUNT.AccOpBal, dbo.A2ZACCOUNT.AccOpenDate,dbo.A2ZACCOUNT.AccRenwlDate,dbo.A2ZACCOUNT.AccLoanExpiryDate,dbo.A2ZACCOUNT.AccLoanSancAmt
        FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType
        WHERE     (dbo.A2ZACCOUNT.AccType =@CommonNo1 AND dbo.A2ZACCOUNT.AccStatus < 97 AND dbo.A2ZACCOUNT.AccOpBal <> 0 AND AccLoanExpiryDate <= @ProcDate)
   END
ELSE
IF @CommonNo2 <> 0 AND @CommonNo4 = 0
   BEGIN
        SELECT     dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16)) as AccNo, dbo.A2ZACCOUNT.MemNo, dbo.A2ZMEMBER.MemName, 
                      dbo.A2ZACCOUNT.AccOpBal, dbo.A2ZACCOUNT.AccOpenDate,dbo.A2ZACCOUNT.AccRenwlDate,dbo.A2ZACCOUNT.AccLoanExpiryDate,dbo.A2ZACCOUNT.AccLoanSancAmt
        FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType
        WHERE     (dbo.A2ZACCOUNT.AccType =@CommonNo1 AND dbo.A2ZACCOUNT.CuType =@CommonNo2 AND dbo.A2ZACCOUNT.CuNo =@CommonNo3 AND dbo.A2ZACCOUNT.AccOpBal <> 0 AND dbo.A2ZACCOUNT.AccStatus < 97 AND AccLoanExpiryDate <= @ProcDate)
   END
ELSE
IF @CommonNo2 = 0 AND @CommonNo4 <> 0
   BEGIN

        SELECT     dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16)) as AccNo, dbo.A2ZACCOUNT.MemNo, dbo.A2ZMEMBER.MemName,dbo.A2ZCUNION.CuDist, 
                      dbo.A2ZACCOUNT.AccOpBal, dbo.A2ZACCOUNT.AccOpenDate,dbo.A2ZACCOUNT.AccRenwlDate,dbo.A2ZACCOUNT.AccLoanExpiryDate,dbo.A2ZACCOUNT.AccLoanSancAmt
        FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType
					  LEFT OUTER JOIN
                      dbo.A2ZCUNION ON dbo.A2ZACCOUNT.CuNo = dbo.A2ZCUNION.CuNo AND 
                      dbo.A2ZACCOUNT.CuType = dbo.A2ZCUNION.CuType
        WHERE     (dbo.A2ZACCOUNT.AccType =@CommonNo1 AND dbo.A2ZACCOUNT.AccStatus < 97 AND dbo.A2ZACCOUNT.AccOpBal <> 0 AND AccLoanExpiryDate <= @ProcDate AND A2ZCUNION.CuDist = @CommonNo4)
   END
ELSE
IF @CommonNo2 <> 0 AND @CommonNo4 <> 0
   BEGIN
        SELECT     dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16)) as AccNo, dbo.A2ZACCOUNT.MemNo, dbo.A2ZMEMBER.MemName, 
                      dbo.A2ZACCOUNT.AccOpBal, dbo.A2ZACCOUNT.AccOpenDate,dbo.A2ZACCOUNT.AccRenwlDate,dbo.A2ZACCOUNT.AccLoanExpiryDate,dbo.A2ZACCOUNT.AccLoanSancAmt
        FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo AND dbo.A2ZACCOUNT.CuNo = dbo.A2ZMEMBER.CuNo AND 
                      dbo.A2ZACCOUNT.CuType = dbo.A2ZMEMBER.CuType
					  LEFT OUTER JOIN
                      dbo.A2ZCUNION ON dbo.A2ZACCOUNT.CuNo = dbo.A2ZCUNION.CuNo AND 
                      dbo.A2ZACCOUNT.CuType = dbo.A2ZCUNION.CuType
        WHERE     (dbo.A2ZACCOUNT.AccType =@CommonNo1 AND dbo.A2ZACCOUNT.CuType =@CommonNo2 AND dbo.A2ZACCOUNT.CuNo =@CommonNo3 AND dbo.A2ZACCOUNT.AccOpBal <> 0 AND dbo.A2ZACCOUNT.AccStatus < 97 AND AccLoanExpiryDate <= @ProcDate AND A2ZCUNION.CuDist = @CommonNo4)
   END



END

GO

