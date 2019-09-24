USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoAccountNo]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[Sp_CSGetInfoAccountNo]

@AccountNo BIGINT


AS
SELECT 

AccType
,AccNo
,CuType
,CuNo
,MemNo
,AccOpenDate
,AccMonthlyDeposit
,AccFixedAmt
,AccFixedMthInt
,AccPeriod
,AccDebitFlag
,AccIntFlag
,AccMatureDate
,AccMatureAmt
,AccIntType
,AccAutoRenewFlag
,AccLoanSancAmt
,AccNoInstl
,AccLoanInstlAmt
,AccLoanLastInstlAmt
,AccIntRate
,AccContractIntFlag
,AccLoanGrace
,AccLoaneeAccType
,AccLoaneeMemNo
,AccSpecialNote
,AccCorrAccType
,AccCorrAccNo
,AccAutoTrfFlag
,AccOldNumber
,AccBalance
,AccProvBalance
,AccPrincipal
,AccOrgAmt
,AccLienAmt
,AccDisbAmt
,AccLastTrnDateU
,AccTotalDep
,AccAtyClass
,AccStatus
,AccStatusDate
,AccStatusNote
,AccDueIntAmt
,AccRenwlDate
,AccRenwlAmt
,AccCertNo
,AccBenefitDate
,AccOpBal
,AccNoAnni
,AccAnniDate

FROM A2ZACCOUNT  WHERE AccNo = @AccountNo

GO
