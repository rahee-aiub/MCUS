
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSDeleteManualRenewalFDR](@accNo Bigint,@FuncOpt SMALLINT)  


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

FROM A2ZACCOUNT,WFCSMANUALRENEWFDR
WHERE A2ZACCOUNT.AccType = WFCSMANUALRENEWFDR.AccType AND A2ZACCOUNT.AccNo = @accNo AND 
A2ZACCOUNT.CuType = WFCSMANUALRENEWFDR.CuType AND A2ZACCOUNT.CuNo = WFCSMANUALRENEWFDR.CuNo AND 
A2ZACCOUNT.MemNo = WFCSMANUALRENEWFDR.MemNo;

DELETE FROM A2ZTRANSACTION  WHERE AccNo = @accNo AND VchNo = 'ManualRenewal' AND TrnDate = @trnDate;;

DELETE FROM WFCSMANUALRENEWFDR WHERE AccNo = @accNo;

--TRUNCATE TABLE WFCSMANUALRENEWFDR;

END;
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

