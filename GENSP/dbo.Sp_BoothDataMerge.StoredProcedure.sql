USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_BoothDataMerge]    Script Date: 1/4/2018 1:40:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_BoothDataMerge](@FileName varchar(200),@ProcessDate VARCHAR(10))

--ALTER PROCEDURE [dbo].[Sp_BoothDataExport]
AS

BEGIN

DECLARE @sql NVARCHAR(MAX);
DECLARE @InDataPath varchar(100);


SET @InDataPath = (SELECT PrmOutDataPath FROM A2ZHKMCUS.DBO.A2ZERPSYSPRM);

SET @sql = 'xp_cmdShell ' + '''' + 'bcp.exe A2ZCSMCUS.DBO.IA2ZTRANSACTION in ' + @InDataPath + @FileName + ' -c -q -C1252 -T -t@' + '''';

EXECUTE (@sql);

EXECUTE Sp_CSMergeTransaction @ProcessDate;


END

























GO
