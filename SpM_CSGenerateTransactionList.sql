USE [A2ZCSMCUS]
GO

/****** Object:  StoredProcedure [dbo].[SpM_CSGenerateTransactionList]    Script Date: 06/09/2018 11:48:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[SpM_CSGenerateTransactionList] (@fDate VARCHAR(10),@tDate VARCHAR(10),@nFlag INT,
@AccType INT,@AccClass SMALLINT,@Func99 INT,@CuType INT,@CuNo INT,@FcashCode INT,@TrnType SMALLINT,@VchNo NVARCHAR(20),
@teller INT,@TrnMode SMALLINT,@TrnNature INT,@TrnModule SMALLINT,@TranSw SMALLINT,@UserId INT)
AS
/*
EXECUTE SpM_CSGenerateTransactionList '2018-03-01','2018-03-01',0,99,0,1,0,0,0,0,'0',0,0,0,0,0,1

For Staff Transaction List
EXECUTE SpM_CSGenerateTransactionList '2016-04-01','2016-04-30',0,0,0,0,0,0,0,0,'0',0,0,0,4,0,0
*/

BEGIN

	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @nCount INT;
	DECLARE @trnDrCr SMALLINT;
	DECLARE @VType NVARCHAR(1);
	DECLARE @Func99Code INT;

	SELECT * INTO #WFTRANSACTIONLIST FROM WFTRANSACTIONLIST;
	TRUNCATE TABLE #WFTRANSACTIONLIST;

	ALTER TABLE #WFTRANSACTIONLIST ADD MemName NVARCHAR(100);
	ALTER TABLE #WFTRANSACTIONLIST ADD TrnTypeDes NVARCHAR(100);
	ALTER TABLE #WFTRANSACTIONLIST ADD AccTypeDescription NVARCHAR(100);
	ALTER TABLE #WFTRANSACTIONLIST ADD sAccNo NVARCHAR(16);

    SET @VType = 'C';    

    SET @fYear = LEFT(@fDate,4);
	SET @tYear = LEFT(@tDate,4);

	SET @nCount = @fYear

	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO #WFTRANSACTIONLIST (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,RepUserID)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,' + CAST(@UserId AS VARCHAR(4)) +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION' + 
                    ' WHERE TrnProcStat = 0 AND TrnFlag = 0 AND TrnCSGL = 0 AND TrnDate' +
					' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
			
            IF  @TrnModule = 1
                BEGIN
                     SET @strSQL = @strSQL + ' AND TrnModule != 4 AND AccTypeMode = 1';       
                END  
     
            IF  @TrnModule = 4
                BEGIN
                     SET @strSQL = @strSQL + ' AND AccTypeMode = 2';       
                END  

            IF  @TrnModule = 6 
                BEGIN           
                     SET @strSQL = @strSQL + ' AND TrnModule = ' + CAST(@TrnModule AS VARCHAR(2));
                END
				                
            IF @TranSw = 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND TrnSysUser = 0';
				END	

            IF @TranSw = 1
				BEGIN
                    SET @strSQL = @strSQL + ' AND TrnSysUser = 1';
				END

            IF @AccType <> 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND AccType = ' + CAST(@AccType AS VARCHAR(2));
				END	

    --        IF @Func99 <> 0
				--BEGIN
    --                SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@Func99 AS VARCHAR(3));
				--END	

            IF @CuType <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND CuType = ' + CAST(@CuType AS VARCHAR(2)) + ' AND CuNo = ' + CAST(@CuNo AS VARCHAR(6));
				END				
            
            IF @FcashCode <> 0 AND @TrnModule <> 4
				BEGIN
					SET @strSQL = @strSQL + ' AND FromCashCode = ' + CAST(@FcashCode AS VARCHAR(8));
				END				
             
            IF @TrnType <> 0
				BEGIN
                    IF @TrnType = 1
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1 AND TrnVchType <> ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @TrnType = 2
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3 AND TrnVchType <> ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @TrnType = 3
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1 AND TrnVchType = ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @TrnType = 4
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1';
                       END
                    IF @TrnType = 5
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3';
                       END
				END

            IF @VchNo <> '0'
				BEGIN
                    IF @VchNo = 'InterestPost' OR @VchNo = 'DevidentPost' 
                       BEGIN
                            SET @strSQL = @strSQL + ' AND VoucherNo = ' + '''' + CAST(@VchNo AS VARCHAR(20)) + '''';
                       END
                    ELSE
                       BEGIN
					        SET @strSQL = @strSQL + ' AND VchNo = ' + '''' + CAST(@VchNo AS VARCHAR(20)) + '''';
                       END
				END	

            IF @teller <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND UserID = ' + CAST(@teller AS VARCHAR(6));
				END	   
          
            IF @TrnMode <> 0
				BEGIN
                    SET @trnDrCr = @TrnMode - 1;
					SET @strSQL = @strSQL + ' AND TrnDrCr = ' + CAST(@trnDrCr AS VARCHAR(2));
				END	               

            
            IF @TrnNature <> 0
				BEGIN
                     IF @TrnNature = 1
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 2
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 3
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 5 AND @AccType = 0
                        BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = 105 AND PayType = 203';
                        END

                     IF @TrnNature = 5 AND @AccType <> 0
                        BEGIN
                            IF @accClass = 2
                               BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 105'; 
                               END
                            ELSE
                               BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 203'; 
                               END
                         END

                     IF @TrnNature = 7
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 9
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 11
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 12
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 19
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                    IF @TrnNature = 20
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END 

                    IF @TrnNature = 403 AND @AccType = 0
                        BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 353';
                        END

                    IF @TrnNature = 403 AND @AccType <> 0
                       BEGIN
                           IF @accClass = 6
                              BEGIN
                                   SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)); 
                              END
                           ELSE
                              BEGIN
                                   SET @strSQL = @strSQL + ' AND PayType = 353'; 
                              END
                       END

                    IF @TrnNature = 402 AND @AccType = 0
                       BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 352';
                       END
                      
                    IF @TrnNature = 402 AND @AccType <> 0
                       BEGIN
                            IF @accClass = 6
                               BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)); 
                               END
                            ELSE
                               BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 352'; 
                               END
                       END

                    IF @TrnNature = 107 AND @AccType = 0
                       BEGIN
                            SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 303' + ' AND PayType = 407';
                       END
                   
                    IF @TrnNature = 107 AND @AccType <> 0
                       BEGIN
                           IF @accClass = 2
                              BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)); 
                              END
                           ELSE IF @accClass = 4
                              BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 303'; 
                              END
                           ELSE
                              BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 407'; 
                              END
                       END

                    IF @TrnNature = 108 AND @AccType = 0
                       BEGIN
                            SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 304' + ' AND PayType = 408';
                       END                   
                      
                    IF @TrnNature = 108 AND @AccType <> 0
                       BEGIN
                            IF @accClass = 2
                               BEGIN
                                     SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)); 
                               END
                            ELSE IF @accClass = 4
                               BEGIN
                                     SET @strSQL = @strSQL + ' AND PayType = 304'; 
                               END
                            ELSE
                               BEGIN
                                     SET @strSQL = @strSQL + ' AND PayType = 408'; 
                               END
                       END  

                    IF @TrnNature = 113
                       BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 212' + ' AND PayType = 307' + ' AND PayType = 410';
                       END
                  
                    IF @TrnNature = 114
                       BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 213' + ' AND PayType = 308' + ' AND PayType = 411';
                       END

                END
            
			EXECUTE (@strSQL);            

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

		SET @strSQL = 'INSERT INTO #WFTRANSACTIONLIST (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,RepUserID)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,' + CAST(@UserId AS VARCHAR(4)) +
				' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE TrnProcStat = 0 AND TrnFlag = 0 AND TrnCSGL = 0 AND TrnDate' +
				' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
         
            IF  @TrnModule = 1
                BEGIN
                     SET @strSQL = @strSQL + ' AND TrnModule != 4 AND AccTypeMode = 1';       
                END  
     
            IF  @TrnModule = 4
                BEGIN
                     SET @strSQL = @strSQL + ' AND AccTypeMode = 2';       
                END  

            IF  @TrnModule = 6 
                BEGIN           
                     SET @strSQL = @strSQL + ' AND TrnModule = ' + CAST(@TrnModule AS VARCHAR(2));
                END
            
            IF @TranSw = 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND TrnSysUser = 0';
				END	

            IF @TranSw = 1
				BEGIN
                    SET @strSQL = @strSQL + ' AND TrnSysUser = 1';
				END

            IF @AccType <> 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND AccType = ' + CAST(@AccType AS VARCHAR(2));
				END	

    --        IF @Func99 <> 0
				--BEGIN
    --                SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@Func99 AS VARCHAR(3));
				--END	

            IF @CuType <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND CuType = ' + CAST(@CuType AS VARCHAR(2)) + ' AND CuNo = ' + CAST(@CuNo AS VARCHAR(6));
				END				
            
            IF @FcashCode <> 0 AND @TrnModule <> 4
				BEGIN
					SET @strSQL = @strSQL + ' AND FromCashCode = ' + CAST(@FcashCode AS VARCHAR(8));
				END				
             
            IF @TrnType <> 0
				BEGIN
                    IF @TrnType = 1
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1 AND TrnVchType <> ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @TrnType = 2
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3 AND TrnVchType <> ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @TrnType = 3
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1 AND TrnVchType = ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @TrnType = 4
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1';
                       END
                    IF @TrnType = 5
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3';
                       END
				END


            IF @VchNo <> '0'
				BEGIN
                    IF @VchNo = 'InterestPost' OR @VchNo = 'DevidentPost' 
                       BEGIN
                            SET @strSQL = @strSQL + ' AND VoucherNo = ' + '''' + CAST(@VchNo AS VARCHAR(20)) + '''';
                       END
                    ELSE
                       BEGIN
					        SET @strSQL = @strSQL + ' AND VchNo = ' + '''' + CAST(@VchNo AS VARCHAR(20)) + '''';
                       END
				END	
    
            IF @teller <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND UserID = ' + CAST(@teller AS VARCHAR(4));
				END	             

            IF @TrnMode <> 0
				BEGIN
                    SET @trnDrCr = @TrnMode - 1;
					SET @strSQL = @strSQL + ' AND TrnDrCr = ' + CAST(@trnDrCr AS VARCHAR(2));
				END	

             IF @TrnNature <> 0
				BEGIN
                     IF @TrnNature = 1
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 2
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 3
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 5 AND @AccType = 0
                        BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = 105 AND PayType = 203';
                        END

                     IF @TrnNature = 5 AND @AccType <> 0
                        BEGIN
                            IF @accClass = 2
                               BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 105'; 
                               END
                            ELSE
                               BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 203'; 
                               END
                         END

                     IF @TrnNature = 7
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 9
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 11
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 12
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                     IF @TrnNature = 19
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END

                    IF @TrnNature = 20
                        BEGIN
                             SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@TrnNature AS VARCHAR(2));
                        END 

                    IF @TrnNature = 403 AND @AccType = 0
                        BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 353';
                        END

                    IF @TrnNature = 403 AND @AccType <> 0
                       BEGIN
                           IF @accClass = 6
                              BEGIN
                                   SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)); 
                              END
                           ELSE
                              BEGIN
                                   SET @strSQL = @strSQL + ' AND PayType = 353'; 
                              END
                       END

                    IF @TrnNature = 402 AND @AccType = 0
                       BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 352';
                       END
                      
                    IF @TrnNature = 402 AND @AccType <> 0
                       BEGIN
                            IF @accClass = 6
                               BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)); 
                               END
                            ELSE
                               BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 352'; 
                               END
                       END

                    IF @TrnNature = 107 AND @AccType = 0
                       BEGIN
                            SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 303' + ' AND PayType = 407';
                       END
                   
                    IF @TrnNature = 107 AND @AccType <> 0
                       BEGIN
                           IF @accClass = 2
                              BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)); 
                              END
                           ELSE IF @accClass = 4
                              BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 303'; 
                              END
                           ELSE
                              BEGIN
                                    SET @strSQL = @strSQL + ' AND PayType = 407'; 
                              END
                       END

                    IF @TrnNature = 108 AND @AccType = 0
                       BEGIN
                            SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 304' + ' AND PayType = 408';
                       END                   
                      
                    IF @TrnNature = 108 AND @AccType <> 0
                       BEGIN
                            IF @accClass = 2
                               BEGIN
                                     SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)); 
                               END
                            ELSE IF @accClass = 4
                               BEGIN
                                     SET @strSQL = @strSQL + ' AND PayType = 304'; 
                               END
                            ELSE
                               BEGIN
                                     SET @strSQL = @strSQL + ' AND PayType = 408'; 
                               END
                       END  

                    IF @TrnNature = 113
                       BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 212' + ' AND PayType = 307' + ' AND PayType = 410';
                       END
                  
                    IF @TrnNature = 114
                       BEGIN
                             SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@TrnNature AS VARCHAR(3)) + ' AND PayType = 213' + ' AND PayType = 308' + ' AND PayType = 411';
                       END

                END
	
	EXECUTE (@strSQL);
       
	IF @TrnNature = 5 AND @accClass = 3
		BEGIN
			UPDATE #WFTRANSACTIONLIST SET #WFTRANSACTIONLIST.TrnDebit = ABS(#WFTRANSACTIONLIST.GLAmount);
		END
   
	DELETE FROM #WFTRANSACTIONLIST WHERE TrnDebit = 0 AND TrnCredit = 0;


	UPDATE #WFTRANSACTIONLIST SET MemName = (SELECT MemName FROM A2ZMEMBER 
	WHERE #WFTRANSACTIONLIST.CuType = A2ZMEMBER.CuType AND #WFTRANSACTIONLIST.CuNo = A2ZMEMBER.CuNo AND 
	#WFTRANSACTIONLIST.MemNo = A2ZMEMBER.MemNo);


    UPDATE #WFTRANSACTIONLIST SET TrnTypeDes = (SELECT TrnTypeDes FROM A2ZTRNTYPE  
    WHERE #WFTRANSACTIONLIST.TrnType = A2ZTRNTYPE.TrnType);
    
    UPDATE #WFTRANSACTIONLIST SET AccTypeDescription = (SELECT AccTypeDescription FROM A2ZACCTYPE
    WHERE  #WFTRANSACTIONLIST.AccType = A2ZACCTYPE.AccTypeCode);

	UPDATE #WFTRANSACTIONLIST SET sAccNo = AccNo;


	 IF @Func99 <> 0
	    BEGIN

			DELETE FROM #WFTRANSACTIONLIST WHERE PayType NOT IN(SELECT Func99Code FROM WF99FUNC WHERE UserId = @UserId)

		END
	
	

	  SELECT * FROM #WFTRANSACTIONLIST;
	
    	
END






GO

