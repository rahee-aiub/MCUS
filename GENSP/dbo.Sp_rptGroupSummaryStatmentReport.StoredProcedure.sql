USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptGroupSummaryStatmentReport]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_rptGroupSummaryStatmentReport](@CommonNo1 int,@CommonNo2 int,@CommonNo3 int,@CommonNo4 int)

AS
BEGIN

/*

EXECUTE Sp_rptGroupSummaryStatmentReport 3,5,0

*/


DECLARE @AccNo BIGINT;
DECLARE @trnDate smalldatetime;
DECLARE @fDate VARCHAR(10);


SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

IF DAY(@trnDate) > 9
   BEGIN
       SET @fDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' + CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-' + CAST(DAY(@trnDate) AS VARCHAR(2));
   END
ELSE
   BEGIN
       SET @fDate = CAST(YEAR(@trnDate) AS VARCHAR(4)) + '-' + CAST(MONTH(@trnDate) AS VARCHAR(2)) + '-0' + CAST(DAY(@trnDate) AS VARCHAR(1));
   END

PRINT @fDate;


DECLARE accTable CURSOR FOR SELECT AccNo
FROM A2ZACCOUNT WHERE CuType = @CommonNo1 AND CuNo = @CommonNo2 AND MemNo = @CommonNo3 AND AccType <> 99;

OPEN accTable;
FETCH NEXT FROM accTable INTO @AccNo;

WHILE @@FETCH_STATUS = 0 
	BEGIN


EXECUTE SpM_CSGenerateSingleAccountBalance @AccNo, @fDate, 0;


FETCH NEXT FROM accTable INTO @AccNo;


	END

CLOSE accTable; 
DEALLOCATE accTable;


IF @CommonNo4 = 0
   BEGIN

       SELECT     dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16)) as AccNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.MemNo, dbo.A2ZACCOUNT.AccBalance, 
                      dbo.A2ZACCOUNT.AccLastTrnDateU, dbo.A2ZACCOUNT.AccStatus, dbo.A2ZACCSTATUS.AccStatusDescription, dbo.A2ZACCTYPE.AccTypeDescription, 
                      dbo.A2ZACCOUNT.AccAtyClass,dbo.A2ZACCOUNT.AccLienAmt
       FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.A2ZACCOUNT.AccType = dbo.A2ZACCTYPE.AccTypeCode LEFT OUTER JOIN
                      dbo.A2ZACCSTATUS ON dbo.A2ZACCOUNT.AccStatus = dbo.A2ZACCSTATUS.AccStatusCode

       WHERE     (dbo.A2ZACCOUNT.CuType = @CommonNo1) AND (dbo.A2ZACCOUNT.CuNo = @CommonNo2) AND (dbo.A2ZACCOUNT.MemNo = @CommonNo3) AND 
                      (dbo.A2ZACCOUNT.AccType <> 99) ORDER BY dbo.A2ZACCOUNT.AccNo

   END

IF @CommonNo4 = 1
   BEGIN

       SELECT     dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16)) as AccNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.MemNo, dbo.A2ZACCOUNT.AccBalance, 
                      dbo.A2ZACCOUNT.AccLastTrnDateU, dbo.A2ZACCOUNT.AccStatus, dbo.A2ZACCSTATUS.AccStatusDescription, dbo.A2ZACCTYPE.AccTypeDescription, 
                      dbo.A2ZACCOUNT.AccAtyClass,dbo.A2ZACCOUNT.AccLienAmt
       FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.A2ZACCOUNT.AccType = dbo.A2ZACCTYPE.AccTypeCode LEFT OUTER JOIN
                      dbo.A2ZACCSTATUS ON dbo.A2ZACCOUNT.AccStatus = dbo.A2ZACCSTATUS.AccStatusCode

       WHERE     (dbo.A2ZACCOUNT.CuType = @CommonNo1) AND (dbo.A2ZACCOUNT.CuNo = @CommonNo2) AND (dbo.A2ZACCOUNT.MemNo = @CommonNo3) AND 
                      (dbo.A2ZACCOUNT.AccType <> 99 AND dbo.A2ZACCOUNT.AccStatus < 98) ORDER BY dbo.A2ZACCOUNT.AccNo

   END

IF @CommonNo4 = 2
   BEGIN

       SELECT     dbo.A2ZACCOUNT.AccType, CAST(dbo.A2ZACCOUNT.AccNo AS VARCHAR(16)) as AccNo, dbo.A2ZACCOUNT.CuType, dbo.A2ZACCOUNT.CuNo, dbo.A2ZACCOUNT.MemNo, dbo.A2ZACCOUNT.AccBalance, 
                      dbo.A2ZACCOUNT.AccLastTrnDateU, dbo.A2ZACCOUNT.AccStatus, dbo.A2ZACCSTATUS.AccStatusDescription, dbo.A2ZACCTYPE.AccTypeDescription, 
                      dbo.A2ZACCOUNT.AccAtyClass,dbo.A2ZACCOUNT.AccLienAmt
       FROM         dbo.A2ZACCOUNT LEFT OUTER JOIN
                      dbo.A2ZACCTYPE ON dbo.A2ZACCOUNT.AccType = dbo.A2ZACCTYPE.AccTypeCode LEFT OUTER JOIN
                      dbo.A2ZACCSTATUS ON dbo.A2ZACCOUNT.AccStatus = dbo.A2ZACCSTATUS.AccStatusCode

       WHERE     (dbo.A2ZACCOUNT.CuType = @CommonNo1) AND (dbo.A2ZACCOUNT.CuNo = @CommonNo2) AND (dbo.A2ZACCOUNT.MemNo = @CommonNo3) AND 
                      (dbo.A2ZACCOUNT.AccType <> 99 AND dbo.A2ZACCOUNT.AccStatus = 99) ORDER BY dbo.A2ZACCOUNT.AccNo

   END

END

GO
