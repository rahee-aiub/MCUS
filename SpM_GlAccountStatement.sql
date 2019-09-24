
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SpM_GlAccountStatement](@AccountCode INT,@fDate VARCHAR(10), @tDate VARCHAR(10), @nFlag INT)
AS
--
-- SpM_GlAccountStatement 10101001,'2016-06-30','2016-06-30',20
-- SpM_GlAccountStatement 10101001,'2016-06-01','2016-10-31',1
-- SpM_GlAccountStatement 10101001,'2016-06-01','2016-10-31',2
--
--  @nFlag = 0 = Detail GL Account Statement
--  @nFlag = 1 = Voucher Wise Cosolidated GL Account Statement
--  @nFlag = 2 = Date Wise Consolidated GL Account Statement
--
BEGIN
	SET NOCOUNT ON;

	DECLARE @accType INT;
	DECLARE @opBalance MONEY;
	DECLARE @debitAmt MONEY;
	DECLARE @creditAmt MONEY;

    DECLARE @Id  INT;
    DECLARE @GLDebitAmt MONEY;
    DECLARE @GLCreditAmt MONEY;
    DECLARE @BALANCE MONEY;

	DECLARE @nDate SMALLDATETIME;
	DECLARE @opDate VARCHAR(10);
    DECLARE @endDate VARCHAR(10);
    DECLARE @xDate VARCHAR(10);
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @openTable VARCHAR(30)

    DECLARE @pYear INT;
	DECLARE @fYear INT;
	DECLARE @tYear INT;
    DECLARE @nYear INT;
	DECLARE @nCount INT;

    DECLARE @BegYear int;
    DECLARE @IYear int;

    DECLARE @ReadFlag INT;
    DECLARE @processDate SMALLDATETIME;

    DECLARE @tmm INT;
    DECLARE @tdd INT;

    DECLARE @xFlag INT;
    DECLARE @yFlag INT;

    

	SELECT * INTO #A2ZCGLMST FROM A2ZCGLMST WHERE GLAccNo = @AccountCode;
	SET @accType = (SELECT GLAccType FROM A2ZCGLMST WHERE GLAccNo = @AccountCode);

--================   Generate Opening Balance ===============
	UPDATE #A2ZCGLMST SET GLOpBal = 0;

    SELECT * INTO #WFA2ZTRANSACTION FROM WFA2ZTRANSACTION WHERE UserID = 0;
	TRUNCATE TABLE #WFA2ZTRANSACTION;

    SET @processDate = (SELECT ProcessDate FROM A2ZGLMCUS..A2ZGLPARAMETER);
    SET @pYear = YEAR(@processDate);
    SET @fYear = LEFT(@fDate,4);

    SET @xFlag = 2;

    IF @pYear <> @fYear
        BEGIN
           SET @xFlag = 1;
        END
     

------*********************************************************************************

     IF @xFlag = 1
       BEGIN

          SET @nDate = @fDate;

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZGLPARAMETER);

  

    IF @fDate = @processDate
       BEGIN
            SET @ReadFlag = 0;
       END
    ELSE
       BEGIN
            SET @ReadFlag = 1;
       END


    SET @IYear = YEAR(@fDate);


    IF @IYear > @BegYear
       BEGIN
            SET @opDate = CAST(@BegYear AS VARCHAR(4)) + '-07-01';
       END
    ELSE
       BEGIN

	        SET @opDate = CAST(YEAR(@fDate) AS VARCHAR(4)) + '-07-01';

	        SET @nYear = YEAR(@nDate);

	        IF MONTH(@nDate) < 7
		       BEGIN
			        SET @nYear = @nYear - 1;

			        SET @opDate = CAST(@nYear AS VARCHAR(4)) + '-07-01';
		       END

       END	


    
	        SET @openTable = 'A2ZCSMCUST' + CAST(YEAR(@opDate) AS VARCHAR(4)) + '..A2ZGLOPBALANCE';

	        SET @strSQL = 'UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = ' + 
		    'ISNULL((SELECT SUM(GLOpBal) FROM ' + @openTable +
		    ' WHERE ' + @openTable + '.GLAccNo = #A2ZCGLMST.GLAccNo),0)' +
		    ' FROM #A2ZCGLMST,' + @openTable + 
		    ' WHERE ' + @openTable + '.GLAccNo = #A2ZCGLMST.GLAccNo';
	        EXECUTE (@strSQL);
      

--=================  Generate Year Wise Transaction Data For Opeing Balance ===========
		
		SET @fYear = LEFT(@opDate,4);
		SET @tYear = LEFT(@fDate,4);
   
		SET @nCount = @fYear


		        WHILE (@nCount <> 0)
			         BEGIN
				          SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					      'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					      'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					      'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					      ' SELECT ' +
					      'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					      'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					      'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					      'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					      ' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE' +
					      ' TrnProcStat = 0 AND TrnDate' +
					      ' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' +
					      ' AND GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8));
			
				          EXECUTE (@strSQL);

				          SET @nCount = @nCount + 1;
				          IF @nCount > @tYear
					         BEGIN
						         SET @nCount = 0;
					         END
				     END 
	--=================  Generate Current Month Transaction Data For Opeing Balance ===========
				SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE' +
					' TrnProcStat = 0 AND' +
					' TrnDate BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @fDate + '''' +
					' AND GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8));

				EXECUTE (@strSQL);

				SET @strSQL = 'DELETE FROM #WFA2ZTRANSACTION WHERE TrnDate = ''' + @fDate + '''';
				EXECUTE (@strSQL);
			
				UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = #A2ZCGLMST.GLOpBal + 
				ISNULL((SELECT SUM(GLAmount) FROM #WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo),0)
				FROM #A2ZCGLMST,#WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo;
   
        END



-----**********************************************************************************

    IF @xFlag = 2
      BEGIN
	
	SET @nDate = @fDate;

    SET @BegYear = (SELECT FinancialBegYear FROM A2ZGLPARAMETER);

    SET @processDate = (SELECT ProcessDate FROM A2ZGLMCUS..A2ZGLPARAMETER);


    SET @tmm = MONTH(@processDate);
    SET @tdd = DAY(@processDate);
    

    IF @tmm < 10 AND @tdd < 10
       BEGIN
            SET @xDate = CAST(YEAR(@processDate) AS VARCHAR(4)) + '-' + '0' + CAST(MONTH(@processDate) AS VARCHAR(1)) + '-' + '0' +  CAST(DAY(@processDate) AS VARCHAR(1));
       END

    IF @tmm < 10 AND @tdd > 9
       BEGIN
            SET @xDate = CAST(YEAR(@processDate) AS VARCHAR(4)) + '-' + '0' + CAST(MONTH(@processDate) AS VARCHAR(1)) + '-' + CAST(DAY(@processDate) AS VARCHAR(2));
       END

    IF @tmm > 9 AND @tdd < 10
       BEGIN
            SET @xDate = CAST(YEAR(@processDate) AS VARCHAR(4)) + '-' + CAST(MONTH(@processDate) AS VARCHAR(2)) + '-' + '0' + CAST(DAY(@processDate) AS VARCHAR(1));
       END

    IF @tmm > 9 AND @tdd > 9
       BEGIN
            SET @xDate = CAST(YEAR(@processDate) AS VARCHAR(4)) + '-' + CAST(MONTH(@processDate) AS VARCHAR(2)) + '-' + CAST(DAY(@processDate) AS VARCHAR(2));
       END


    IF @fDate = @processDate
       BEGIN
            SET @ReadFlag = 0;
       END
    ELSE
       BEGIN
            SET @ReadFlag = 1;
       END


    SET @fYear = LEFT(@fDate,4);
	SET @nCount = LEFT(@xDate,4);
    SET @tYear = LEFT(@xDate,4);

    IF @fYear = @tYear
       BEGIN
            SET @opDate = @fDate;
            SET @endDate = @tDate;
       END
    ELSE
       BEGIN
            SET @opDate = CAST(@tYear AS VARCHAR(4)) + '-01-01';
            SET @endDate = @xDate;
       END

       

        IF @ReadFlag = 1
           BEGIN

		        WHILE (@nCount <> 0)
			         BEGIN
				          SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					      'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					      'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					      'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					      ' SELECT ' +
					      'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					      'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					      'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					      'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					      ' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE' +
					      ' TrnProcStat = 0 AND TrnDate' +
					      ' BETWEEN ''' + @opDate + '''' + ' AND ' + '''' + @xDate + '''' +
					      ' AND GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8));
			
				          EXECUTE (@strSQL);

                         
                          SET @nCount = @nCount - 1;

                          IF @nCount > @fYear
                             BEGIN
                                  SET @opDate = CAST(@nCount AS VARCHAR(4)) + '-01-01';
                             END

                          IF @nCount = @fYear
                             BEGIN
                                  SET @opDate = CAST(@nCount AS VARCHAR(4)) + '-' + CAST(MONTH(@fDate) AS VARCHAR(2)) + '-' + CAST(DAY(@fDate) AS VARCHAR(2));
                                  SET @endDate = @xDate;
                             END

                          IF @nCount < @fYear
     	                     BEGIN
			                      SET @nCount = 0;
			                 END

				     END 
	
			
				UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = #A2ZCGLMST.GLOpBal + 
				ISNULL((SELECT SUM(GLAmount) FROM #WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo),0)
				FROM #A2ZCGLMST,#WFA2ZTRANSACTION
				WHERE #WFA2ZTRANSACTION.GLAccNo = #A2ZCGLMST.GLAccNo;
         
                SET @opBalance = (SELECT GLOpBal FROM #A2ZCGLMST WHERE GLAccNo = @AccountCode);

                PRINT @opBalance;


                UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = #A2ZCGLMST.GLTodaysOpBalance - #A2ZCGLMST.GLOpBal           
                FROM #A2ZCGLMST; 
                
        END

        IF @ReadFlag = 0
           BEGIN
              UPDATE #A2ZCGLMST SET #A2ZCGLMST.GLOpBal = #A2ZCGLMST.GLTodaysOpBalance           
              FROM #A2ZCGLMST;    
           END  

      END

	--================= End of Generate Transaction Data For Opeing Balance ===========

	SET @opBalance = (SELECT GLOpBal FROM #A2ZCGLMST WHERE GLAccNo = @AccountCode);

--================   End of Generate Opening Balance ===============
		TRUNCATE TABLE #WFA2ZTRANSACTION;
--================ Generate Current Transaction ====================
		SET @fYear = LEFT(@fDate,4);
		SET @tYear = LEFT(@tDate,4);
   
		SET @nCount = @fYear
		WHILE (@nCount <> 0)
			BEGIN
			
				SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE' +
					' TrnProcStat = 0 AND' +
					' TrnDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''' +
					' AND GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8));

				EXECUTE (@strSQL);
				
				SET @nCount = @nCount + 1;
				IF @nCount > @tYear
					BEGIN
						SET @nCount = 0;
					END
			END 

		SET @strSQL = 'INSERT INTO #WFA2ZTRANSACTION (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
			'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
			'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
			'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate)' +
			' SELECT ' +
			'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
			'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
			'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
			'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,ValueDate,UserIP,UserID,VerifyUserID,CreateDate' +
			' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE TrnProcStat = 0 AND TrnDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''' +
			' AND GLAccNo = ' + CAST(@AccountCode AS VARCHAR(8));

		EXECUTE (@strSQL);
--================ End of Generate Current Transaction ====================

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

	SELECT * INTO #WFGLSTATEMENT FROM WFGLSTATEMENT WHERE ID = 0;
	TRUNCATE TABLE #WFGLSTATEMENT;	

	INSERT INTO #WFGLSTATEMENT (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,
	UserID,CreateDate,ValueDate)
	SELECT
	BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,
	PayType,TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,
	TrnPenalAmt,TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,
	GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,UserIP,
	UserID,CreateDate,ValueDate
	FROM #WFA2ZTRANSACTION;

	UPDATE #WFGLSTATEMENT SET GLAccType = @accType;

	DROP TABLE #A2ZCGLMST;
	DROP TABLE #WFA2ZTRANSACTION;
	
	INSERT INTO #WFGLSTATEMENT (TrnDate,GLAccNo,GLAmount,GLDebitAmt,GLCreditAmt,TrnDesc,GLAccType)
	VALUES (@fDate,@AccountCode,@opBalance,@debitAmt,@creditAmt,'=== Opening Balance ===',@accType)

   DECLARE wfstatementTable CURSOR FOR
   SELECT Id,GLDebitAmt,GLCreditAmt
   FROM #WFGLSTATEMENT WHERE GLAmount <> 0 ORDER BY TrnDate,VchNo;

   OPEN wfstatementTable;
   FETCH NEXT FROM wfstatementTable INTO @Id,@GLDebitAmt,@GLCreditAmt;

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

        
			UPDATE #WFGLSTATEMENT SET #WFGLSTATEMENT.GLClosingBal = @BALANCE WHERE Id=@Id;
   
	    FETCH NEXT FROM wfstatementTable INTO @Id,@GLDebitAmt,@GLCreditAmt;

	END

	CLOSE wfstatementTable; 
	DEALLOCATE wfstatementTable;

--  @nFlag = 0 = Detail GL Account Statement
	IF @nFlag = 0
		BEGIN
			SELECT * FROM #WFGLSTATEMENT;
		END

--  @nFlag = 1 = Voucher Wise Cosolidated GL Account Statement	
	IF @nFlag = 1
		BEGIN
			SELECT TrnDate, GLAccNo, SUM(GLCreditAmt) AS GLCreditAmt, SUM(GLDebitAmt) AS GLDebitAmt, 
			TrnProcStat, VchNo, TrnType
			FROM #WFGLSTATEMENT			
			GROUP BY TrnDate, GLAccNo, TrnProcStat, VchNo, TrnType;
		END

--  @nFlag = 2 = Date Wise Consolidated GL Account Statement
	IF @nFlag = 2
		BEGIN
			SELECT TrnDate, GLAccNo, SUM(GLCreditAmt) AS GLCreditAmt, SUM(GLDebitAmt) AS GLDebitAmt, 
			TrnProcStat, TrnType
			FROM #WFGLSTATEMENT
			GROUP BY TrnDate, GLAccNo, TrnProcStat, TrnType;
		END

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

