USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSQryTransactionControl]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Sp_CSQryTransactionControl]  


AS

BEGIN

--DECLARE @userID INT;


DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accPrincipal money;
DECLARE @accLastIntCr money;
DECLARE @accRenwlAmt money;
DECLARE @accIntRate smallmoney;
DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accAnniDate smalldatetime;
DECLARE @accPeriodMonths int;
DECLARE @accMatureDate smalldatetime;
DECLARE @accNoAnni int;
DECLARE @accNoRenwl int;
DECLARE @accAtyClass smallint;

DECLARE @trnDate smalldatetime;

DECLARE @RoundFlag tinyint;

DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;

DECLARE @calInterest money;
DECLARE @AdjChrg money;
DECLARE @NetInt money;







DECLARE @Id  INT;
DECLARE @AccType INT;
DECLARE @AccTypeClass INT;
DECLARE @FuncOpt INT;
DECLARE @PayType INT;
DECLARE @TrnType INT;
DECLARE @TrnMode INT;
DECLARE @TrnLogic INT;
DECLARE @RecMode INT;
DECLARE @ShowInt INT;

DECLARE @RecordCount INT;

DECLARE @txtTrnCode INT;
DECLARE @PayTypeDesc nvarchar(50);

DECLARE @CtrlPayType1  INT;
DECLARE @lblPayDesc1   nvarchar(50);
DECLARE @CtrlRecMode1  INT;
DECLARE @CtrlPayType2  INT;
DECLARE @lblPayDesc2   nvarchar(50);
DECLARE @CtrlRecMode2  INT;
DECLARE @CtrlPayType3  INT;
DECLARE @lblPayDesc3   nvarchar(50);
DECLARE @CtrlRecMode3  INT;
DECLARE @CtrlPayType4  INT;
DECLARE @lblPayDesc4   nvarchar(50);
DECLARE @CtrlRecMode4  INT;

SET @RecordCount = 0;


DECLARE TrnCtrlTable CURSOR FOR
SELECT Id,AccType,AccTypeClass,FuncOpt,PayType,TrnType,TrnMode,TrnLogic,RecMode,ShowInt
FROM A2ZTRNCTRL WHERE TrnCode = @txtTrnCode AND FuncOpt = @FuncOpt;

OPEN TrnCtrlTable;
FETCH NEXT FROM TrnCtrlTable INTO
@Id,@AccType,@AccTypeClass,@FuncOpt,@PayType,@TrnType,@TrnMode,@TrnLogic,@RecMode,@ShowInt;

WHILE @@FETCH_STATUS = 0 
	BEGIN
        
        SET @RecordCount = (@RecordCount + 1);

-------------------- Read Pay ---------------
        SET @PayTypeDesc = (SELECT PayTypeDes FROM A2ZPAYTYPE 
                         WHERE AtyClass = @AccTypeClass AND PayType = @PayType);

        IF @RecordCount = 1
           BEGIN
               SET @CtrlPayType1 = @PayType;
               SET @lblPayDesc1 = @PayTypeDesc;
               SET @CtrlRecMode1 = @RecMode;
           END
        IF @RecordCount = 2
           BEGIN
               SET @CtrlPayType2 = @PayType;
               SET @lblPayDesc2 = @PayTypeDesc;
               SET @CtrlRecMode2 = @RecMode;
           END
        IF @RecordCount = 3
           BEGIN
               SET @CtrlPayType3 = @PayType;
               SET @lblPayDesc3 = @PayTypeDesc;
               SET @CtrlRecMode3 = @RecMode;
           END
        IF @RecordCount = 4
           BEGIN
               SET @CtrlPayType4 = @PayType;
               SET @lblPayDesc4 = @PayTypeDesc;
               SET @CtrlRecMode4 = @RecMode;
           END
 

--    
--
--
--    
----------------------------------------------------------------------------------------------------
--
--    SET @cuNumber = LTRIM(STR(@cuType) + '-' + LTRIM(STR(@cuNo)));
--    SET @TrnDesc= (SELECT PayTypeDes FROM A2ZPAYTYPE WHERE AtyClass = @accAtyClass AND PayType=@PayType);
-------- Find Member Type ----------------------
--
--	SET @memType = (SELECT MemType FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
--
--    SET @memName = (SELECT MemName FROM A2ZMEMBER WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo);
--
--    SET @RoundFlag = (SELECT PrmRoundFlag FROM A2ZCSPARAM WHERE AccType = @accType);
--
-------- End of Find Member Type ----------------------
--
--    SET @accProvBalance = (SELECT AccProvBalance FROM A2ZACCOUNT WHERE CuType = @cuType AND CuNo = @cuNo AND MemNo = @memNo AND AccType = @accType AND AccNo = @accNo);
--
--------  Interest Calculation -----------------
--    SET @tDate = @trnDate;
--
--    IF @accAnniDate IS NULL
--      IF @accRenwlDate IS NULL
--         BEGIN			
--              SET @fdAmount = @accOrgAmt
--              SET @fDate = @accOpenDate
--         END
--       ELSE	 
--          BEGIN
--              SET @fdAmount =  @accRenwlAmt
--		      SET @fDate = @accRenwlDate
--          END
--	ELSE
--    IF @accRenwlDate IS NULL
--		BEGIN
--            SET @fdAmount =  @accOrgAmt
--			SET @fDate = @accAnniDate
--        END
--    ELSE
--        BEGIN
--            SET @fdAmount =  @accRenwlAmt
--			SET @fDate = @accAnniDate
--		END
--    
--
--    SET @newAnniDate = (DATEADD(year,1,@fDate))
--   
--
-- --   SELECT @aDate,@tDate;
--
--    IF @tDate >= @newAnniDate
--       BEGIN
--          SET @tDate = @newAnniDate;
--          SET @noDays = ((DATEDIFF(d, @fDate, @tDate)) + 0);
--
--------  Interest Calculation -----------------
--    IF @RoundFlag = 1
--       BEGIN    
--       SET @calInterest = Round((((@fdAmount * @accIntRate * @noDays) / 36500)), 0);
--       END
--    
--    IF @RoundFlag = 2
--       BEGIN
--       SET @calInterest = floor(((@fdAmount * @accIntRate * @noDays) / 36500));
--       END
--
--    IF @RoundFlag = 3
--       BEGIN
--       SET @calInterest = ((@fdAmount * @accIntRate * @noDays) / 36500);
--       END   
--    
--   SELECT @accIntRate;
--   SELECT @noDays;
--   SELECT @calInterest;
--
--
--------  End of Interest Calculation -----------
--
-----   NEW NO OF ANNIVERSARY -----------------
--
--        SET @newNoAnni = Round(( @accNoAnni + 1), 0);
--
--        SET @newLastIntCr = Round(((@accLastIntCr + @calInterest)), 0);
--
--        SET @AdjChrg = Round(((@AccPrincipal - @AccBalance)), 0);
--        SET @NetInt = Round(((@calInterest - @AdjChrg)), 0);
--        SET @newAnniAmt = Round(((@AccPrincipal + @NetInt)), 0);
--
--        SET @AdjProvAmt = Round(((@calInterest - @accProvBalance)), 0);
--        
--        IF @AdjProvAmt = 0
--           BEGIN	
--              SET @AdjProvAmtCr = 0;
--              SET @AdjProvAmtDr = 0;
--           END
--        
--        IF @AdjProvAmt > 0
--           BEGIN	
--              SET @AdjProvAmtCr = @AdjProvAmt;
--              SET @AdjProvAmtDr = 0;
--           END
--        
--        IF @AdjProvAmt <0           
--           BEGIN	
--              SET @AdjProvAmtCr = 0;
--              SET @AdjProvAmtDr = Abs(@AdjProvAmt);
--           END
--
--        
-----   END OF NEW NO OF ANNIVERSARY -----------------
--
-------- Insert Record to Workfile ---------------
--
--	INSERT INTO WFCSANNIVERSARY6YR
--	(CuType,CuNo,MemNo,AccType,AccNo,TrnCode,TrnDate,AccBalance,AccOrgAmt,AccPrincipal,AccIntRate,AccOpenDate,
--	AccRenwlDate,AccAnniDate,AccRenwlAmt,AccPeriodMonths,AccMatureDate,AccNoAnni,AccNoRenwl,CalAdjProvCr,CalAdjProvDr,CalInterest,NewNoAnni,NewAnniDate,NewLastIntCr,NewAnniAmt,FDAmount,NoDays,CuNumber,MemName,
--    FuncOpt,PayType,TrnDesc,TrnType,TrnDrCr,ShowInterest,TrnGLAccNoDr,TrnGLAccNoCr,UserId,ProcStat)
--	
--    VALUES (@cuType,@cuNo,@memNo,@accType,@accNo,@trnCode,@trnDate,@accBalance,@accOrgAmt,@accPrincipal,@accIntRate,@accOpenDate,
--	@accRenwlDate,@accAnniDate,@accRenwlAmt,@accPeriodMonths,@accMatureDate,@accNoAnni,@accNoRenwl,@AdjProvAmtCr,@AdjProvAmtDr,@calInterest,@newNoAnni,@newAnniDate,@newLastIntCr,@newAnniAmt,@fdAmount,@noDays,@cuNumber,@memName,
--    @FuncOpt,@PayType,@TrnDesc,@TrnType,@TrnDrCr,@ShowInterest,@TrnGLAccNoDr,@TrnGLAccNoCr,@UserID,1);
--
-------- End of Insert Record to Workfile ---------------

--    END

	FETCH NEXT FROM TrnCtrlTable INTO
        @Id,@AccType,@AccTypeClass,@FuncOpt,@PayType,@TrnType,@TrnMode,@TrnLogic,@RecMode,@ShowInt;

	END

CLOSE TrnCtrlTable; 
DEALLOCATE TrnCtrlTable;

END

































GO
