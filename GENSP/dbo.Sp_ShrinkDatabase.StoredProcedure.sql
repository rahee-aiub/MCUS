USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_ShrinkDatabase]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_ShrinkDatabase]
AS

BEGIN
	DECLARE @strSQL VARCHAR(MAX);

	DECLARE @DatabaseName VARCHAR(MAX);
	DECLARE @mdfLocation VARCHAR(MAX);
	DECLARE @ldfLocation VARCHAR(MAX);

	DECLARE @mdfFileName VARCHAR(MAX);
	DECLARE @ldfFileName VARCHAR(MAX);
	DECLARE @filePath VARCHAR(MAX);

	DECLARE @nPosition INT;

	SELECT * INTO #A2ZDATABASE FROM A2ZHKMCUS..A2ZDATABASE

	DECLARE tblDataBase CURSOR FOR
	SELECT DatabaseName FROM #A2ZDATABASE;

	OPEN tblDataBase;
	FETCH NEXT FROM tblDataBase INTO @DatabaseName;

	WHILE @@FETCH_STATUS = 0 
		BEGIN

			SET @mdfFileName = (SELECT Name FROM sys.master_files
			WHERE DB_NAME(database_id) = @DatabaseName AND type_desc = 'ROWS');

			SET @ldfFileName = (SELECT Name FROM sys.master_files
			WHERE DB_NAME(database_id) = @DatabaseName AND type_desc = 'LOG');


			SET @strSQL = 'USE ' + @DatabaseName + 
			' ALTER DATABASE ' + @DatabaseName + 
			' SET RECOVERY SIMPLE WITH NO_WAIT; ' +
			' DBCC  SHRINKFILE(' + @mdfFileName + ', 1); ' +
			' ALTER DATABASE ' + @DatabaseName + 
			' SET RECOVERY FULL WITH NO_WAIT;';

			EXECUTE (@strSQL);

			SET @strSQL = 'USE ' + @DatabaseName + 
			' ALTER DATABASE ' + @DatabaseName + 
			' SET RECOVERY SIMPLE WITH NO_WAIT; ' +
			' DBCC  SHRINKFILE(' + @ldfFileName + ', 1); ' +
			' ALTER DATABASE ' + @DatabaseName + 
			' SET RECOVERY FULL WITH NO_WAIT;';

			EXECUTE (@strSQL);
			
--			PRINT @DatabaseName;
--			PRINT @mdfFileName;
--			PRINT @ldfFileName;
--			PRINT @strSQL;

			FETCH NEXT FROM tblDataBase INTO @DatabaseName;
		END

	CLOSE tblDataBase;
	DEALLOCATE tblDataBase;

END
GO
