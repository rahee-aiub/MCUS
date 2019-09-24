USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_SYGenerateRecordControl]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE PROCEDURE [dbo].[Sp_SYGenerateRecordControl]  
AS

BEGIN

DECLARE @GLCashCode int;
DECLARE @RecType smallint;
DECLARE @LastRecNo int;



---------- Refresh Workfile ----------
TRUNCATE TABLE A2ZRECCTRLNO;
---------- End of Refresh Workfile ----------

DECLARE recTable CURSOR FOR
SELECT GLAccNo FROM A2ZGLMCUS.DBO.A2ZCGLMST WHERE GLRecType = 2 AND GLSubHead = 10101000;

OPEN recTable;
FETCH NEXT FROM recTable INTO @GLCashCode;

WHILE @@FETCH_STATUS = 0 
	BEGIN
		INSERT INTO A2ZRECCTRLNO (CtrlGLCashCode,CtrlRecType,CtrlRecLastNo) VALUES (@GLCashCode,1,0);
		INSERT INTO A2ZRECCTRLNO (CtrlGLCashCode,CtrlRecType,CtrlRecLastNo) VALUES (@GLCashCode,2,0);
        INSERT INTO A2ZRECCTRLNO (CtrlGLCashCode,CtrlRecType,CtrlRecLastNo) VALUES (@GLCashCode,14,0);
		INSERT INTO A2ZRECCTRLNO (CtrlGLCashCode,CtrlRecType,CtrlRecLastNo) VALUES (@GLCashCode,15,0);
        INSERT INTO A2ZRECCTRLNO (CtrlGLCashCode,CtrlRecType,CtrlRecLastNo) VALUES (@GLCashCode,16,0);
        INSERT INTO A2ZRECCTRLNO (CtrlGLCashCode,CtrlRecType,CtrlRecLastNo) VALUES (@GLCashCode,17,0);
        INSERT INTO A2ZRECCTRLNO (CtrlGLCashCode,CtrlRecType,CtrlRecLastNo) VALUES (@GLCashCode,51,0);
-------- End of Insert Record to Workfile ---------------    
	FETCH NEXT FROM recTable INTO @GLCashCode;
	END

END

CLOSE recTable; 
DEALLOCATE recTable;































GO
