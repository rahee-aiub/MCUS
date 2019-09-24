USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSPostDevidentShare]    Script Date: 11/16/2016 19:34:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_CSPostDevidentShare](@userID INT,@FromCashCode INT) 
AS
--EXECUTE Sp_CSPostDevidentShare 1,10101001


BEGIN

DECLARE @CcuType int;
DECLARE @CcuNo int;

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;

DECLARE @accNo Bigint;
DECLARE @trnCode int;
DECLARE @accBalance money;
DECLARE @accOrgAmt money;
DECLARE @accPrincipal money;
DECLARE @accIntRate smallmoney;
DECLARE @accOpenDate smalldatetime;
DECLARE @accRenwlDate smalldatetime;
DECLARE @accAnniDate smalldatetime;
DECLARE @accRenwlAmt money;
DECLARE @accPeriodMonths int;
DECLARE @accMatureDate smalldatetime;
DECLARE @accNoAnni smallint;
DECLARE @accNoRenwl int;
DECLARE @accIntWdrawn money;
DECLARE @accProvBalance money;
DECLARE @accAtyClass smallint;


DECLARE @fDate smalldatetime;
DECLARE @tDate smalldatetime;
DECLARE @uptoMthProvision money;
DECLARE @uptoLastMthProvision money;
DECLARE @currMthProvision money;
DECLARE @fdAmount money;
DECLARE @noDays int;

DECLARE @RoundFlag tinyint;

DECLARE @memType int;
DECLARE @cuNumber nvarchar(10);
DECLARE @memName nvarchar(50);

DECLARE @FuncOpt smallint;
DECLARE @PayType smallint;

DECLARE @TrnType tinyint;
DECLARE @TrnDrCr tinyint;
DECLARE @ShowInterest tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;

DECLARE @currentDate smalldatetime;
DECLARE @firstDay int;
DECLARE @lastDay int;

DECLARE @firstDate smalldatetime;
DECLARE @lastDate smalldatetime;

DECLARE @OldCuNo   int;
DECLARE @OldMemNo  int;
DECLARE @AccType   int;
DECLARE @TrnDate   smalldatetime
DECLARE @VchNo     nvarchar(20);
DECLARE @TrnAmount money;
DECLARE @TrnDesc   nvarchar(50);





--TRUNCATE TABLE WFCSPROVISIONFDR;

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

--
--
SET @trnDate = (SELECT ProcessDate FROM A2ZCSPARAMETER);
--
--
--SET @currentDate  = @trnDate;
--SET @firstDay = DAY(DATEADD(dd, -DAY(@currentDate) + 1, @currentDate));
--SET @lastDay = DAY(DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));
--
--SET @firstDate = (DATEADD(dd, -DAY(@currentDate) + 1, @currentDate));
--SET @lastDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @currentDate)), DATEADD(mm, 1, @currentDate)));
------CurrentMonth = MONTH(@trnDate);

------------- READ A2ZTRNCTRL FILE -------------
SET @trnCode= (SELECT TrnCode FROM A2ZTRNCODE WHERE AccType = 11);


SET @FuncOpt= (SELECT FuncOpt FROM A2ZTRNCTRL WHERE AccType = 11 AND FuncOpt=19);
SET @PayType = (SELECT PayType FROM A2ZTRNCTRL WHERE AccType = 11 AND FuncOpt=19);
SET @TrnType= (SELECT TrnType FROM A2ZTRNCTRL WHERE AccType = 11 AND FuncOpt=19);
SET @TrnDesc= (SELECT TrnRecDesc FROM A2ZTRNCTRL WHERE AccType = 11 AND FuncOpt=19);
SET @TrnDrCr= (SELECT TrnMode FROM A2ZTRNCTRL WHERE AccType = 11 AND FuncOpt=19);
SET @ShowInterest= (SELECT ShowInt FROM A2ZTRNCTRL WHERE AccType = 11 AND FuncOpt=19);
SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZTRNCTRL WHERE AccType = 11 AND FuncOpt=19);
SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZTRNCTRL WHERE AccType = 11 AND FuncOpt=19);

---------  END OF READ A2ZTRNCTRL FILE -------------------------------

--------------------------------------------------------------------
DECLARE wfIntTable CURSOR FOR
SELECT OldCuNo,OldMemNo,AccType,VchNo,TrnAmount,TrnDesc
FROM WFDEVIDENTSHARE;


OPEN wfIntTable;
FETCH NEXT FROM wfIntTable INTO
@OldCuNo,@OldMemNo,@AccType,@VchNo,@TrnAmount,@TrnDesc;


WHILE @@FETCH_STATUS = 0 
	BEGIN


    SET @cuType = (SELECT CuType FROM A2ZMEMBER WHERE (MemOldCuNo = @OldCuNo AND MemOldMemNo = @OldMemNo) OR (MemOld1CuNo = @OldCuNo AND MemOld1MemNo = @OldMemNo) OR (MemOld2CuNo = @OldCuNo AND MemOld2MemNo = @OldMemNo));
    SET @cuNo = (SELECT CuNo FROM A2ZMEMBER WHERE (MemOldCuNo = @OldCuNo AND MemOldMemNo = @OldMemNo) OR (MemOld1CuNo = @OldCuNo AND MemOld1MemNo = @OldMemNo) OR (MemOld2CuNo = @OldCuNo AND MemOld2MemNo = @OldMemNo));

    SET @accNo = (SELECT AccNo FROM A2ZACCOUNT WHERE CuType=@cuType AND CuNo=@cuNo AND MemNo=@OldMemNo AND AccType=@AccType);
  
--------------- NOPRMAL TRANSACTION ------------
INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,TrnDebit,
TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

VALUES(@trnDate,@VchNo,@VchNo,@cuType,@cuNo,@OldMemNo,@AccType,@accNo,@trnCode,@FuncOpt,@PayType,@TrnType,1,0,
@TrnAmount,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@TrnAmount,0,@TrnAmount,0,@FromCashCode,1,1,@userID);

--------------- END OF NOPRMAL TRANSACTION ------------

--------------- CONTRA TRANSACTION ------------
INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,TrnCode,FuncOpt,PayType,TrnType,TrnDrCr,
TrnDebit,TrnCredit,TrnDesc,ShowInterest,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnModule,TrnSysUser,UserID)

VALUES (@trnDate,@VchNo,@VchNo,@cuType,@cuNo,@OldMemNo,@AccType,@accNo,@trnCode,@FuncOpt,@PayType,@TrnType,0,@TrnAmount,
0,@TrnDesc,@ShowInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoDr,@TrnAmount,@TrnAmount,0,1,@FromCashCode,1,1,@userID);

--------------- END OF CONTRA TRANSACTION ------------

UPDATE A2ZACCOUNT SET A2ZACCOUNT.AccBalance = (A2ZACCOUNT.AccBalance + @TrnAmount);



FETCH NEXT FROM wfIntTable INTO
    @OldCuNo,@OldMemNo,@AccType,@VchNo,@TrnAmount,@TrnDesc;



	END

CLOSE wfIntTable; 
DEALLOCATE wfIntTable;

COMMIT TRANSACTION
		SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH



END





