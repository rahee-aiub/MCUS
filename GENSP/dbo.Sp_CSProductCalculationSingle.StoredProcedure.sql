USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSProductCalculationSingle]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[Sp_CSProductCalculationSingle](@cuType INT,@cuNo INT,@memNo INT,@accType INT,@accNo BIGINT,
@fDate VARCHAR(10),@tDate VARCHAR(10),@intRate MONEY,@userID INT,@nFlag INT)

--
-- EXECUTE Sp_CSProductCalculationSingle 3,5,0,52,0,'2015-10-01','2015-10-31',12.25,0,0
--
--


AS
BEGIN

--==== Insert All Transaction to Work Table ==========
DELETE FROM WFCSPRODUCT WHERE UserId = @userId;

INSERT INTO WFCSPRODUCT (BatchNo,TrnDate,CuType,CuNo,MemNo,AccType,AccNo,TrnDebit,TrnCredit)
SELECT BatchNo,TrnDate,CuType,CuNo,MemNo,AccType,AccNo,SUM(TrnDebit) AS TrnDebit,SUM(TrnCredit) AS TrnCredit
FROM A2ZTRANSACTION
WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND 
AccNo = @accNo AND TrnFlag = 0 AND ShowInterest = 0 AND TrnDate BETWEEN @fDate AND @tDate
GROUP BY BatchNo,TrnDate,CuType,CuNo,MemNo,AccType,AccNo;

UPDATE WFCSPRODUCT SET TrnIntRate = @intRate WHERE UserId = @userId;
--==== Insert All Transaction to Work Table ==========


DECLARE @opBalance MONEY;
DECLARE @startDate SMALLDATETIME;
DECLARE @endDate SMALLDATETIME;
DECLARE @nCount INT;
DECLARE @dayCount INT;
DECLARE @productAmt MONEY;
DECLARE @interestAmt MONEY;

DECLARE @trnDate SMALLDATETIME;
DECLARE @debitAmount MONEY;
DECLARE @creditAmount MONEY;

SET @opBalance = (SELECT AccOpBal FROM A2ZACCOUNT
		WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND 
		AccNo = @accNo);

SET @nCount = 0;

--==  Calculated Day Balance =======
DECLARE wfTable CURSOR FOR
SELECT TrnDate,TrnDebit,TrnCredit FROM WFCSPRODUCT WHERE UserId = @userId ORDER BY TrnDate;

OPEN wfTable; 
FETCH NEXT FROM wfTable INTO @trnDate,@debitAmount,@creditAmount; 
WHILE @@FETCH_STATUS = 0 
BEGIN
	SET @opBalance = (@opBalance + @creditAmount) - @debitAmount;
	
	UPDATE WFCSPRODUCT SET TrnBalance = @opBalance
	WHERE TrnDate = @trnDate AND CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND 
	AccType = @accType AND AccNo = @accNo AND UserID = @userId;

FETCH NEXT FROM wfTable INTO @trnDate,@debitAmount,@creditAmount; 

END;	       

CLOSE wfTable; 
DEALLOCATE wfTable;
--==  End of Calculated Day Balance =======

--==  Calculated No of Day =======
SET @startDate = @fDate;

DECLARE wfTable CURSOR FOR
SELECT TrnDate FROM WFCSPRODUCT WHERE UserId = @userId ORDER BY TrnDate;

OPEN wfTable; 
FETCH FROM wfTable INTO @trnDate; 
WHILE @@FETCH_STATUS = 0 
BEGIN
	IF @nCount = 0
		BEGIN
			SET @startDate = @trnDate;
		END

	IF @nCount > 0
		BEGIN
			SET @dayCount = DATEDIFF(DAY,@startDate,@trnDate);		
			UPDATE WFCSPRODUCT SET TrnNofDay = @dayCount WHERE UserId = @userId AND TrnDate = @startDate;
			
			SET @startDate = @trnDate;
		END

	SET @nCount = 1;

FETCH NEXT FROM wfTable INTO @trnDate;

END;	       

--=== Update Last Record ========
SET @dayCount = DATEDIFF(DAY,@startDate,@tDate);
UPDATE WFCSPRODUCT SET TrnNofDay = @dayCount WHERE UserId = @userId AND TrnDate = @startDate;
--=== End of Update Last Record ========

--==  End of Calculated No of Day =======


--== Update Total Product and Interest Amount ===
UPDATE WFCSPRODUCT SET TrnProduct = TrnBalance * TrnNofDay, 
TrnInterestAmt = ROUND(((TrnBalance * TrnNofDay * TrnIntRate) / 36500),0)
WHERE UserId = @userId
--== End of Update Total Product and Interest Amount ===


CLOSE wfTable; 
DEALLOCATE wfTable;
--==  End of Calculated No of Day =======

--= SELECT SUM(TrnInterestAmt) FROM WFCSPRODUCT WHERE UserId = @userId

END;


GO
