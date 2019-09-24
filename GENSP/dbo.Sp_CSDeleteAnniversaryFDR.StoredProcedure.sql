USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSDeleteAnniversaryFDR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_CSDeleteAnniversaryFDR](@AccType INT,@FuncOpt SMALLINT)  


AS

BEGIN

DECLARE @trnDate smalldatetime;

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccAnniDate = A2ZACCOUNT.AccPrevAnniDate,
                      A2ZACCOUNT.AccNoAnni = A2ZACCOUNT.AccPrevNoAnni,                   
                      A2ZACCOUNT.AccPrincipal  = A2ZACCOUNT.AccPrevRaCurr,   
                      A2ZACCOUNT.AccBalance  = A2ZACCOUNT.AccPrevRaLedger,
                      A2ZACCOUNT.AccProvBalance  = A2ZACCOUNT.AccPrevProvBalance     

FROM A2ZACCOUNT,WFCSANNIVERSARYFDR
WHERE A2ZACCOUNT.AccType = WFCSANNIVERSARYFDR.AccType AND A2ZACCOUNT.AccNo = WFCSANNIVERSARYFDR.AccNo AND 
A2ZACCOUNT.CuType = WFCSANNIVERSARYFDR.CuType AND A2ZACCOUNT.CuNo = WFCSANNIVERSARYFDR.CuNo AND 
A2ZACCOUNT.MemNo = WFCSANNIVERSARYFDR.MemNo;

DELETE FROM A2ZTRANSACTION  WHERE AccType = @AccType AND FuncOpt = @FuncOpt AND TrnDate = @trnDate;

TRUNCATE TABLE WFCSANNIVERSARYFDR;

END;





GO
