USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSUpdateMinimumBalance]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSUpdateMinimumBalance](@AccType INT,@calPeriod INT,@nFlag INT)
AS

--
-- EXECUTE Sp_CSUpdateMinimumBalance 12,2,0
--

BEGIN
	SET NOCOUNT ON;

	DECLARE @strSQL NVARCHAR(MAX);

	DECLARE @ID      INT;
	DECLARE @CuType  INT;
	DECLARE @CuNo    INT;
	DECLARE @MemNo   INT;

	DECLARE @TrnDate SMALLDATETIME;
	DECLARE @Balance MONEY;
	DECLARE @TotBalance MONEY;
	DECLARE @AmtOpening MONEY;

	DECLARE @tblMonth INT;
	DECLARE @MinmumBalance MONEY;

--	DECLARE InterestTable CURSOR FOR
--	SELECT ID,CuType,CuNo,MemNo,AmtOpening FROM WFCSINTEREST 
--	WHERE AccType = 12 AND CuType = 3 AND CuNo = 624 AND MemNo = 0;
	
	DECLARE InterestTable CURSOR FOR
	SELECT ID,CuType,CuNo,MemNo,AmtOpening FROM WFCSINTEREST 
	WHERE AccType = @AccType;
	
	OPEN InterestTable;
	FETCH NEXT FROM InterestTable INTO @ID,@CuType,@CuNo,@MemNo,@AmtOpening;

	WHILE @@FETCH_STATUS = 0 
		BEGIN
			DECLARE @BalanceTable TABLE (tblMonth INT,tblBalance MONEY);
			DELETE FROM WFCALTABLE;

			SET @TotBalance = @AmtOpening;

			DECLARE wfTable CURSOR FOR
			SELECT TrnDate,(SUM(TrnCredit) - SUM(TrnDebit)) AS Balance
			FROM WFA2ZTRANSACTION 
			WHERE AccType = @AccType AND CuType = @CuType AND CuNo = @CuNo AND MemNo = @MemNo
			GROUP BY TrnDate
			ORDER BY TrnDate;

			OPEN wfTable;
			FETCH NEXT FROM wfTable INTO @TrnDate,@Balance;

			WHILE @@FETCH_STATUS = 0 
				BEGIN
					SET @TotBalance = @TotBalance + @Balance;

					INSERT INTO @BalanceTable (tblMonth,tblBalance)
					VALUES (MONTH(@TrnDate),@TotBalance);
			
					FETCH NEXT FROM wfTable INTO @TrnDate,@Balance;		   
				END

			INSERT INTO WFCALTABLE(tblMonth,tblBalance) SELECT tblMonth,tblBalance FROM @BalanceTable;

--			SELECT * FROM @BalanceTable;
--			SELECT tblMonth,MIN(tblBalance) AS MinmumBalance FROM @BalanceTable
--			WHERE tblMonth IS NOT NULL
--			GROUP BY tblMonth;
			--==========  Update Minimun Balance To WFCSINTEREST ==========

--			DECLARE tmpTable CURSOR FOR
--			SELECT tblMonth,MIN(tblBalance) AS MinmumBalance FROM @BalanceTable
--			WHERE tblBalance IS NOT NULL
--			GROUP BY tblMonth;

			DECLARE tmpTable CURSOR FOR
			SELECT tblMonth,MIN(tblBalance) AS MinmumBalance FROM WFCALTABLE
			GROUP BY tblMonth;

			OPEN tmpTable;
			FETCH NEXT FROM tmpTable INTO @tblMonth,@MinmumBalance;

			WHILE @@FETCH_STATUS = 0 
				BEGIN
					--PRINT @tblMonth;

					SET @strSQL = 	
					'UPDATE WFCSINTEREST SET WFCSINTEREST.Amt' + 
					LEFT(DATENAME(MONTH,DATEADD(MONTH,-MONTH(GETDATE()) + @tblMonth,GETDATE())),3) +
					' = ' + CAST(@MinmumBalance AS VARCHAR(12)) + 
					' WHERE ID = ' + CAST(@ID AS VARCHAR(6)) + 
					' AND WFCSINTEREST.Amt' +
					LEFT(DATENAME(MONTH,DATEADD(MONTH,-MONTH(GETDATE()) + @tblMonth,GETDATE())),3) +
					' > ' + + CAST(@MinmumBalance AS VARCHAR(12));
					
					--PRINT @strSQL;
					
					EXECUTE(@strSQL);

					FETCH NEXT FROM tmpTable INTO @tblMonth,@MinmumBalance;
				END
		
			CLOSE tmpTable; 
			DEALLOCATE tmpTable;
			--==========  End of Update Minimun Balance To WFCSINTEREST ==========
			--SELECT * FROM @BalanceTable;

			DELETE FROM @BalanceTable;

			CLOSE wfTable; 
			DEALLOCATE wfTable;

			FETCH NEXT FROM InterestTable INTO @ID,@CuType,@CuNo,@MemNo,@AmtOpening;
		END

	CLOSE InterestTable; 
	DEALLOCATE InterestTable;

END




GO
