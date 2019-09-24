USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_HODataExport]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Sp_HODataExport]

--ALTER PROCEDURE [dbo].[Sp_BoothDataExport]
AS

BEGIN

DECLARE @CUFileName NVARCHAR(MAX);
DECLARE @MEMFileName NVARCHAR(MAX);
DECLARE @ACCFileName NVARCHAR(MAX);
DECLARE @OutDataPath varchar(100);
DECLARE @CUsql NVARCHAR(MAX);
DECLARE @Memsql NVARCHAR(MAX);
DECLARE @Accsql NVARCHAR(MAX);


SET @OutDataPath = (SELECT PrmOutDataPath FROM A2ZHKMCUS.DBO.A2ZERPSYSPRM);
SET @CUFileName = 'CU.DAT';
SET @MemFileName = 'Mem.DAT';
SET @AccFileName = 'Acc.DAT';



SET @CUsql='xp_cmdShell ' + '''' + 'bcp.exe A2ZCSMCUS.DBO.A2ZCUNION out ' + @OutDataPath + @CUFileName + ' -c -q -C1252 -T -t@' + ''''
EXECUTE (@CUsql);

SET @Memsql='xp_cmdShell ' + '''' + 'bcp.exe A2ZCSMCUS.DBO.A2ZMEMBER out ' + @OutDataPath + @MemFileName + ' -c -q -C1252 -T -t@' + ''''
EXECUTE (@Memsql);

SET @Accsql='xp_cmdShell ' + '''' + 'bcp.exe A2ZCSMCUS.DBO.A2ZACCOUNT out ' + @OutDataPath + @AccFileName + ' -c -q -C1252 -T -t@' + ''''
EXECUTE (@Accsql);




--EXECUTE xp_cmdShell 'bcp.exe A2ZCSMCUS.DBO.A2ZMEMBER OUT '@OutDataPath'MEM.DAT -c -q -C1252 -T -t@';
--
--EXECUTE xp_cmdShell 'bcp.exe A2ZCSMCUS.DBO.A2ZACCOUNT OUT '@OutDataPath'ACC.DAT -c -q -C1252 -T -t@';


END














GO
