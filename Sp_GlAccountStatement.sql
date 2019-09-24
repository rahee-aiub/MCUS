
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GlAccountStatement](@AccountCode INT, @fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
/*

EXECUTE Sp_GLAccountStatement 10101001,'2016-09-01','2016-10-30',0



*/

BEGIN
	DECLARE @accType INT;
	DECLARE @opBalance MONEY;
	DECLARE @debitAmt MONEY;
	DECLARE @creditAmt MONEY;

    DECLARE @Id  INT;
    DECLARE @GLDebitAmt MONEY;
    DECLARE @GLCreditAmt MONEY;
    DECLARE @BALANCE MONEY;

	TRUNCATE TABLE WFGLSTATEMENT;	

	EXECUTE Sp_GLGenerateOpeningBalanceSingle @AccountCode, @fDate,1;

--===============  Find Out Debit Credit Amount ============
	SET @accType = (SELECT GLAccType FROM A2ZCGLMST WHERE GLAccNo = @AccountCode);
	SET @opBalance = (SELECT GLOpBal FROM A2ZCGLMST WHERE GLAccNo = @AccountCode);

	SET @debitAmt = 0;
	SET @creditAmt = 0;
    SET @BALANCE = 0;

	IF @accType = 1 OR @accType = 5
		BEGIN
			IF @opBalance > 0
				BEGIN
					SET @debitAmt = ABS(@opBalance);
				END
			ELSE
				BEGIN
					SET @creditAmt = ABS(@opBalance);
				END
		END
	
	IF @accType = 2 OR @accType = 4
		BEGIN
			IF @opBalance > 0
				BEGIN
					SET @creditAmt = ABS(@opBalance);
				END
			ELSE
				BEGIN
					SET @debitAmt = ABS(@opBalance);
				END
		END
--===============  Find Out Debit Credit Amount ============

	EXECUTE Sp_GLGenerateStatementDataSingle @AccountCode, @fDate,@tDate,0;

	

	INSERT INTO WFGLSTATEMENT (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,
	UserID,CreateDate,GLAccType)
	SELECT
	BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,
	UserID,CreateDate,@accType
	FROM WFA2ZTRANSACTION;

	INSERT INTO WFGLSTATEMENT (TrnDate,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnDesc,GLAccType)
	VALUES (@fDate,@AccountCode,@opBalance,@debitAmt,@creditAmt,'=== Opening Balance ===',@accType)



   DECLARE wfstatementTable CURSOR FOR
   SELECT Id,GLDebitAmt,GLCreditAmt
   FROM WFGLSTATEMENT WHERE GLAmount <> 0 ORDER BY TrnDate,VchNo;

   OPEN wfstatementTable;
   FETCH NEXT FROM wfstatementTable INTO
   @Id,@GLDebitAmt,@GLCreditAmt;

   WHILE @@FETCH_STATUS = 0 
  	BEGIN
        
        IF @accType = 1 or @accType = 5
           BEGIN
                SET @BALANCE = (@BALANCE + @GLDebitAmt - @GLCreditAmt); 
           END

        IF @accType = 2 or @accType = 4
           BEGIN
                SET @BALANCE = (@BALANCE + @GLCreditAmt - @GLDebitAmt); 
           END

        
        UPDATE WFGLSTATEMENT SET WFGLSTATEMENT.GLClosingBal = @BALANCE WHERE Id=@Id;
   
	    FETCH NEXT FROM wfstatementTable INTO
		   @Id,@GLDebitAmt,@GLCreditAmt;


	END

CLOSE wfstatementTable; 
DEALLOCATE wfstatementTable;





END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

