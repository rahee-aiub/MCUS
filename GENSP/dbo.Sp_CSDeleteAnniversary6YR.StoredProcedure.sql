USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSDeleteAnniversary6YR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Sp_CSDeleteAnniversary6YR](@AccType INT,@FuncOpt SMALLINT)  


AS

BEGIN

DECLARE @trnDate smalldatetime;

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccAnniDate = A2ZACCOUNT.AccPrevAnniDate,
                      A2ZACCOUNT.AccNoAnni = A2ZACCOUNT.AccPrevNoAnni,                   
                      A2ZACCOUNT.AccPrincipal  = A2ZACCOUNT.AccPrevRaCurr,   
                      A2ZACCOUNT.AccBalance  = A2ZACCOUNT.AccPrevRaLedger,
                      A2ZACCOUNT.AccProvBalance  = A2ZACCOUNT.AccPrevProvBalance      

FROM A2ZACCOUNT,WFCSANNIVERSARY6YR
WHERE A2ZACCOUNT.AccType = WFCSANNIVERSARY6YR.AccType AND A2ZACCOUNT.AccNo = WFCSANNIVERSARY6YR.AccNo AND 
A2ZACCOUNT.CuType = WFCSANNIVERSARY6YR.CuType AND A2ZACCOUNT.CuNo = WFCSANNIVERSARY6YR.CuNo AND 
A2ZACCOUNT.MemNo = WFCSANNIVERSARY6YR.MemNo;

DELETE FROM A2ZTRANSACTION  WHERE AccType = @AccType AND FuncOpt = @FuncOpt AND TrnDate = @trnDate;

TRUNCATE TABLE WFCSANNIVERSARY6YR;

END;






GO
