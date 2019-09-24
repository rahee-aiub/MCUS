USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSDeleteProvisionFDR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_CSDeleteProvisionFDR](@AccType INT,@FuncOpt SMALLINT)  


AS

BEGIN

DECLARE @trnDate smalldatetime;

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccProvBalance = A2ZACCOUNT.AccPrevProvBalance,
A2ZACCOUNT.AccProvCalDate = A2ZACCOUNT.AccPrevProvCalDate
FROM A2ZACCOUNT,WFCSPROVISIONFDR
WHERE A2ZACCOUNT.AccType = WFCSPROVISIONFDR.AccType AND A2ZACCOUNT.AccNo = WFCSPROVISIONFDR.AccNo AND 
A2ZACCOUNT.CuType = WFCSPROVISIONFDR.CuType AND A2ZACCOUNT.CuNo = WFCSPROVISIONFDR.CuNo AND 
A2ZACCOUNT.MemNo = WFCSPROVISIONFDR.MemNo;

DELETE FROM A2ZTRANSACTION  WHERE AccType = @AccType AND FuncOpt = @FuncOpt AND TrnDate = @trnDate;

TRUNCATE TABLE WFCSPROVISIONFDR;

END;



GO
