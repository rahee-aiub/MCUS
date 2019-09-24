USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_NewGenerateLoanDefaulter]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_NewGenerateLoanDefaulter] (@AccType INT,@LoanGlCode INT,@IntGlCode INT)

--
-- Sp_NewGenerateLoanDefaulter 53,10502001,40102001
-- Sp_NewGenerateLoanDefaulter 51,10501001,40101001
-- Sp_NewGenerateLoanDefaulter 54,10501002,40101002
-- Sp_NewGenerateLoanDefaulter 61,10501004,40101004
--
--

AS
BEGIN

DECLARE @TrnDate SMALLDATETIME;
DECLARE @AccNo BIGINT;

SET @TrnDate = '2016-04-01'

WHILE @TrnDate < '2017-10-31'
	BEGIN

		INSERT INTO A2ZLOANDEFAULTER (TrnDate,CuType,CuNo,MemNo,AccType,AccNo,CalPrincAmt,InterestRate,AccOpenDate,AccLoanSancAmt,AccDisbAmt)
		SELECT @TrnDate,CuType,CuNo,MemNo,AccType,AccNo,AccLoanInstlAmt,AccIntRate,AccOpendate,AccLoanSancAmt,AccDisbAmt
		FROM A2ZACCOUNT
		WHERE AccType = @AccType AND AccStatus < 98;

		SET @TrnDate = DATEADD(MONTH,1,@TrnDate)
	END

UPDATE h
SET PaidPrincAmt = d.PrincipalPaid
FROM A2ZLOANDEFAULTER h JOIN
(SELECT YEAR(TrnDate) PaidYear,MONTH(TrnDate) PaidMonth,AccType,AccNo,SUM(GLCreditAmt) PrincipalPaid 
FROM A2ZCSMCUST2016..A2ZTRANSACTION
WHERE GLAccNo = @LoanGlCode AND AccType = @AccType
GROUP BY YEAR(TrnDate),MONTH(Trndate),AccNo,AccType) d
ON d.PaidYear = YEAR(h.TrnDate) AND
d.PaidMonth = MONTH(h.TrnDate) AND
d.AccType = h.AccType AND
d.AccNo = h.AccNo;


UPDATE h
SET PaidPrincAmt = d.PrincipalPaid
FROM A2ZLOANDEFAULTER h JOIN
(SELECT YEAR(TrnDate) PaidYear,MONTH(TrnDate) PaidMonth,AccType,AccNo,SUM(GLCreditAmt) PrincipalPaid 
FROM A2ZCSMCUST2017..A2ZTRANSACTION
WHERE GLAccNo = @LoanGlCode AND AccType = @AccType
GROUP BY YEAR(TrnDate),MONTH(Trndate),AccNo,AccType) d
ON d.PaidYear = YEAR(h.TrnDate) AND
d.PaidMonth = MONTH(h.TrnDate) AND
d.AccType = h.AccType AND
d.AccNo = h.AccNo;


UPDATE h
SET PaidIntAmt = d.InterestPaid
FROM A2ZLOANDEFAULTER h JOIN
(SELECT YEAR(TrnDate) PaidYear,MONTH(TrnDate) PaidMonth,AccNo,AccType,SUM(GLCreditAmt) InterestPaid 
FROM A2ZCSMCUST2016..A2ZTRANSACTION
WHERE GLAccNo = @IntGlCode AND AccType = @AccType
GROUP BY YEAR(TrnDate),MONTH(Trndate),AccNo,AccType) d
ON d.PaidYear = YEAR(h.TrnDate) AND
d.PaidMonth = MONTH(h.TrnDate) AND
d.AccType = h.AccType AND
d.AccNo = h.AccNo;


UPDATE h
SET PaidIntAmt = d.InterestPaid
FROM A2ZLOANDEFAULTER h JOIN
(SELECT YEAR(TrnDate) PaidYear,MONTH(TrnDate) PaidMonth,AccNo,AccType,SUM(GLCreditAmt) InterestPaid 
FROM A2ZCSMCUST2017..A2ZTRANSACTION
WHERE GLAccNo = @IntGlCode AND AccType = @AccType
GROUP BY YEAR(TrnDate),MONTH(Trndate),AccNo,AccType) d
ON d.PaidYear = YEAR(h.TrnDate) AND
d.PaidMonth = MONTH(h.TrnDate) AND
d.AccType = h.AccType AND
d.AccNo = h.AccNo;


UPDATE h
SET DisbursementAmt = d.Disbursement
FROM A2ZLOANDEFAULTER h JOIN
(SELECT YEAR(TrnDate) PaidYear,MONTH(TrnDate) PaidMonth,AccNo,AccType,SUM(GLDebitAmt) Disbursement 
FROM A2ZCSMCUST2016..A2ZTRANSACTION
WHERE GLAccNo = @LoanGlCode AND GLDebitAmt > 0 AND AccType = @AccType
GROUP BY YEAR(TrnDate),MONTH(Trndate),AccNo,AccType) d
ON d.PaidYear = YEAR(h.TrnDate) AND
d.PaidMonth = MONTH(h.TrnDate) AND
d.AccType = h.AccType AND
d.AccNo = h.AccNo;


UPDATE h
SET DisbursementAmt = d.Disbursement
FROM A2ZLOANDEFAULTER h JOIN
(SELECT YEAR(TrnDate) PaidYear,MONTH(TrnDate) PaidMonth,AccNo,AccType,SUM(GLDebitAmt) Disbursement 
FROM A2ZCSMCUST2017..A2ZTRANSACTION
WHERE GLAccNo = @LoanGlCode AND GLDebitAmt > 0 AND AccType = @AccType
GROUP BY YEAR(TrnDate),MONTH(Trndate),AccNo,AccType) d
ON d.PaidYear = YEAR(h.TrnDate) AND
d.PaidMonth = MONTH(h.TrnDate) AND
d.AccType = h.AccType AND
d.AccNo = h.AccNo;


UPDATE h
SET OpeningBalance = d.TrnAmount
FROM A2ZLOANDEFAULTER h JOIN
(SELECT AccNo,TrnAmount,AccType
FROM A2ZCSMCUST2015..A2ZCSOPBALANCE) d
ON d.AccNo = h.AccNo AND 
d.AccType = h.AccType AND 
h.TrnDate = '2016-04-01' AND h.AccType = @AccType

--DELETE FROM A2ZLOANDEFAULTER
--WHERE AccType = @AccType AND OpeningBalance = 0 AND DisbursementAmt = 0 AND PaidPrincAmt = 0 AND PaidIntAmt = 0
--AND TrnDate < AccOpenDate;

--DELETE FROM A2ZLOANDEFAULTER
--WHERE AccType = @AccType AND OpeningBalance = 0 AND DisbursementAmt = 0 AND PaidPrincAmt = 0 AND PaidIntAmt = 0
--AND CalIntAmt = 0
--AND TrnDate > AccOpenDate;

DECLARE AccTable CURSOR FOR
SELECT AccNo FROM A2ZACCOUNT WHERE AccType = @AccType

OPEN AccTable;
FETCH NEXT FROM AccTable INTO @AccNo;
WHILE @@FETCH_STATUS = 0 
	BEGIN
		EXECUTE Sp_NewGenerateLoanDefaulter2 @AccNo;
		
		FETCH NEXT FROM AccTable INTO @AccNo;
	END

CLOSE AccTable; 
DEALLOCATE AccTable;

UPDATE A2ZLOANDEFAULTER SET TrnDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, TrnDate)), DATEADD(mm, 1, TrnDate)))
WHERE AccType = @AccType;

UPDATE A2ZLOANDEFAULTER SET CalPrincAmt = ABS(OpeningBalance),PayablePrincAmt = ABS(OpeningBalance),CurrDuePrincAmt = ABS(OpeningBalance)
WHERE AccType = @AccType AND ABS(OpeningBalance) <= CalPrincAmt AND TRNDATE > '2016-04-30' AND AccDisbAmt >= AccLoanSancAmt

UPDATE A2ZLOANDEFAULTER SET CalPrincAmt = 0,CalIntAmt = 0,UptoDuePrincAmt = 0,PayablePrincAmt = 0,CurrDuePrincAmt = 0
WHERE AccType = @AccType AND OpeningBalance >= 0 AND AccDisbAmt >= AccLoanSancAmt

UPDATE A2ZLOANDEFAULTER SET CalIntAmt = 0
WHERE AccType = @AccType AND OpeningBalance >= 0 

DELETE FROM A2ZLOANDEFAULTER
WHERE AccType = @AccType AND AccType = @AccType AND CalPrincAmt = 0 AND CalIntAmt = 0 AND UptoDuePrincAmt = 0 AND UptoDueIntAmt = 0 AND 
	  PayablePrincAmt = 0 AND PayableIntAmt = 0 AND PayablePenalAmt = 0 AND PaidPrincAmt = 0 AND PaidIntAmt = 0 AND PaidPenalAmt = 0 AND 
	  CurrDuePrincAmt = 0 AND CurrDueIntAmt = 0 AND NoDueInstalment = 0 AND OpeningBalance = 0 AND DisbursementAmt = 0;


UPDATE A2ZLOANDEFAULTER SET NoDueInstalment = CEILING(CurrDuePrincAmt / CalPrincAmt)
WHERE AccType = @AccType AND CalPrincAmt > 0 AND CurrDuePrincAmt > 0

END







GO
