USE [master]
GO
/****** Object:  StoredProcedure [dbo].[Sp_BackUp]    Script Date: 7/30/2017 3:34:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Sp_BackUp](@DataItems VARCHAR(max),@txtTo VARCHAR(max),@BackItems VARCHAR(max))
AS

BEGIN
	
	DECLARE @strSQL NVARCHAR(MAX);
 
    SET @strSQL ='BACKUP DATABASE ' + @DataItems + ' TO DISK = ' + '''' + @txtTo + @BackItems + '''';

 EXECUTE (@strSQL);	

END


