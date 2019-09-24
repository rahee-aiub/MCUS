USE [MASTER]
GO
/****** Object:  StoredProcedure [dbo].[Sp_Restore]    Script Date: 7/30/2017 3:18:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_Restore](@DataItems VARCHAR(max),@txtFrom VARCHAR(max),@BackItems VARCHAR(max))
AS

BEGIN
	
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @strSQL2 NVARCHAR(MAX);
	DECLARE @strSQL3 NVARCHAR(MAX);
	

	SET @strSQL='ALTER DATABASE '  + @DataItems + ' SET SINGLE_USER WITH ROLLBACK IMMEDIATE ';
	EXECUTE (@strSQL);


	SET @strSQL2 ='RESTORE DATABASE ' + @DataItems + '  FROM DISK = ' + '''' + @txtFrom + @BackItems +  ''' WITH REPLACE'; 
	EXECUTE (@strSQL2);

	SET @strSQL= 'ALTER DATABASE ' + @DataItems + ' SET MULTI_USER ';
	EXECUTE (@strSQL3);
	

	PRINT @strSQL



END


