USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSDeleteRenewalFDR]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_CSDeleteRenewalFDR](@AccType INT,@FuncOpt SMALLINT)  


AS

BEGIN

DECLARE @trnDate smalldatetime;

SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);

UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccRenwlDate = A2ZACCOUNT.AccPrevRenwlDate,
                      A2ZACCOUNT.AccRenwlAmt = A2ZACCOUNT.AccPrevRenwlAmt,
                      A2ZACCOUNT.AccNoRenwl = A2ZACCOUNT.AccPrevNoRenwl,
                      A2ZACCOUNT.AccNoAnni = A2ZACCOUNT.AccPrevNoAnni,
                      A2ZACCOUNT.AccIntRate = A2ZACCOUNT.AccPrevIntRate,
                      A2ZACCOUNT.AccPeriod = A2ZACCOUNT.AccPrevPeriod,
                      A2ZACCOUNT.AccMatureDate = A2ZACCOUNT.AccPrevMatureDate,
                      A2ZACCOUNT.AccPrincipal  = A2ZACCOUNT.AccPrevRaCurr,   
                      A2ZACCOUNT.AccBalance  = A2ZACCOUNT.AccPrevRaLedger,   
                      A2ZACCOUNT.AccProvBalance  = A2ZACCOUNT.AccPrevProvBalance   

FROM A2ZACCOUNT,WFCSRENEWFDR
WHERE A2ZACCOUNT.AccType = WFCSRENEWFDR.AccType AND A2ZACCOUNT.AccNo = WFCSRENEWFDR.AccNo AND 
A2ZACCOUNT.CuType = WFCSRENEWFDR.CuType AND A2ZACCOUNT.CuNo = WFCSRENEWFDR.CuNo AND 
A2ZACCOUNT.MemNo = WFCSRENEWFDR.MemNo;

DELETE FROM A2ZTRANSACTION  WHERE AccType = @AccType AND FuncOpt = @FuncOpt AND TrnDate = @trnDate;

TRUNCATE TABLE WFCSRENEWFDR;

END;





GO
