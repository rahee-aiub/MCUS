USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_BoothDataExport]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Sp_BoothDataExport](@FromDate DateTime, @Todate DateTime, @CashCode varchar(50))

--ALTER PROCEDURE [dbo].[Sp_BoothDataExport]
AS

BEGIN

DECLARE @sql NVARCHAR(MAX);

DECLARE @MonthName varchar(10);
DECLARE @Year varchar(10);
DECLARE @Day varchar(10);
DECLARE @FileName varchar(200);
DECLARE @TrnDate DateTime;
DECLARE @OutDataPath varchar(100);

--DECLARE @FromDate DateTime;
--DECLARE @ToDate DateTime;
--DECLARE @CashCode varchar(50);
--
--set @FromDate = '2015-02-01';
--set @ToDate = '2015-02-01';
--set @CashCode = '10100101';



SET @MonthName = UPPER(LEFT(DATENAME(MM,@Todate),3));
SET @Year = YEAR(@Todate);

DECLARE TrnTable CURSOR FOR
SELECT TrnDate FROM A2ZTRANSACTION WHERE TrnDate BETWEEN @FromDate AND @Todate group by TrnDate;

OPEN TrnTable; 
FETCH NEXT FROM TrnTable INTO @TrnDate; 
WHILE @@FETCH_STATUS = 0 
BEGIN

--PRINT @TrnDate;

INSERT INTO A2ZCSMCUS.DBO.EA2ZTRANSACTION SELECT * FROM A2ZCSMCUS.DBO.A2ZTRANSACTION WHERE TRNDATE = @TrnDate;

SET @Day=DAY(@TrnDate);
IF(@Day<10)
  BEGIN
  SET @Day='0'+@Day;
  SET @FileName = 'B' + @CashCode + @MonthName + @Year + @Day + '.DAT';
  END
ELSE
  BEGIN
  SET @FileName = 'B' + @CashCode + @MonthName + @Year + @Day + '.DAT';
  END
--print @FileName;

SET @OutDataPath = (SELECT PrmOutDataPath FROM A2ZHKMCUS.DBO.A2ZERPSYSPRM);


--SET @sql = 'xp_cmdShell ' + '''' + 'bcp.exe A2ZCSMCUS.DBO.EA2ZTRANSACTION out E:/BOOTH/' + @FileName + ' -c -q -C1252 -T -t@' + '''';

SET @sql = 'xp_cmdShell ' + '''' + 'bcp.exe A2ZCSMCUS.DBO.EA2ZTRANSACTION out ' + @OutDataPath + @FileName + ' -c -q -C1252 -T -t@' + '''';

EXECUTE (@sql);

TRUNCATE TABLE A2ZCSMCUS.DBO.EA2ZTRANSACTION;


FETCH NEXT FROM TrnTable INTO @TrnDate; 

END

CLOSE TrnTable; 
DEALLOCATE TrnTable;

END













GO
