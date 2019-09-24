USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSDeleteDividendAmount]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_CSDeleteDividendAmount](@AccType INT,@FuncOpt SMALLINT)  


AS

BEGIN

DECLARE @trnDate smalldatetime;

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);


DELETE FROM A2ZTRANSACTION  WHERE AccType = @AccType AND FuncOpt = @FuncOpt AND TrnDate = @trnDate;

DELETE FROM WFCSSHAREINT  WHERE AccType = @AccType;

--TRUNCATE TABLE WFCSINTEREST;

END;






GO
