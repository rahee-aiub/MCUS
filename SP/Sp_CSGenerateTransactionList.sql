
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CSGenerateTransactionList]
@fDate VARCHAR(10)
,@tDate VARCHAR(10)
,@nFlag INT
,@accType INT
,@accClass SMALLINT
,@99Func INT
,@cuType INT
,@cuNo INT
,@FcashCode INT
,@trnType SMALLINT
,@vchNo NVARCHAR(20)
,@teller INT
,@trnMode SMALLINT
,@trnNature INT
,@trnModule SMALLINT
,@transSw SMALLINT
,@UserID INT


AS
/*

EXECUTE Sp_CSGenerateTransactionList '2016-05-01','2016-07-31',0,17,3,0,0,0,'0',0,0,0,0,5,1,0,1


*/


BEGIN

DECLARE @fYear INT;
DECLARE @tYear INT;
DECLARE @strSQL NVARCHAR(MAX);
DECLARE @nCount INT;
DECLARE @trnDrCr SMALLINT;
DECLARE @VType NVARCHAR(1);

    
    DELETE FROM WFTRANSACTIONLIST WHERE RepUserID = @UserID;
	

    SET @VType = 'C';    


    SET @fYear = LEFT(@fDate,4);
	SET @tYear = LEFT(@tDate,4);

	SET @nCount = @fYear

    PRINT @nCount;

	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO WFTRANSACTIONLIST (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,RepUserID)' +
					' SELECT ' +
					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,' + CAST(@UserID AS VARCHAR(4)) +
					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION' + 
                    ' WHERE TrnProcStat = 0 AND TrnFlag = 0 AND TrnCSGL = 0 AND TrnDate' +
					' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
			
            IF  @trnModule = 1
                BEGIN
                     SET @strSQL = @strSQL + ' AND TrnModule != 4';       
                END  
     
            IF  @trnModule <> 1 
                BEGIN           
                     SET @strSQL = @strSQL + ' AND TrnModule = ' + CAST(@trnModule AS VARCHAR(2));
                END
				                
            IF @transSw = 0 AND @trnNature = 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND ShowInterest = 0';
				END	

            IF @transSw = 1 AND @trnNature = 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND TrnSysUser = 1';
				END

            IF @accType <> 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND AccType = ' + CAST(@accType AS VARCHAR(2));
				END	

            IF @99Func <> 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@99Func AS VARCHAR(3));
				END	

            IF @cuType <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND CuType = ' + CAST(@cuType AS VARCHAR(2)) + ' AND CuNo = ' + CAST(@cuNo AS VARCHAR(6));
				END				
            
            IF @FcashCode <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND FromCashCode = ' + CAST(@FcashCode AS VARCHAR(8));
				END				
             
            IF @trnType <> 0
				BEGIN
                    IF @trnType = 1
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1 AND TrnVchType <> ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @trnType = 2
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3 AND TrnVchType <> ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @trnType = 3
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1 AND TrnVchType = ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @trnType = 4
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1';
                       END
                    IF @trnType = 5
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3';
                       END
				END

            IF @vchNo <> '0'
				BEGIN
					SET @strSQL = @strSQL + ' AND VchNo = ' + '''' + CAST(@vchNo AS VARCHAR(20)) + '''';
				END	

            IF @teller <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND UserID = ' + CAST(@teller AS VARCHAR(6));
				END	   
          
            IF @trnMode <> 0
				BEGIN
                    SET @trnDrCr = @trnMode - 1;
					SET @strSQL = @strSQL + ' AND TrnDrCr = ' + CAST(@trnDrCr AS VARCHAR(2));
				END	               

            
            IF @trnNature <> 0
				BEGIN
                    IF @trnNature > 100
                       BEGIN
                             IF @trnNature = 113
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 212' + ' OR PayType = 307' + ' OR PayType = 410';
                                END
                             IF @trnNature = 114
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 213' + ' OR PayType = 308' + ' OR PayType = 411';
                                END
                             IF @trnNature = 107 AND @accType = 0
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 303' + ' OR PayType = 407';
                                END
                             IF @trnNature = 107 AND @accType <> 0
                                BEGIN
                                      IF @accClass = 2
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)); 
                                         END
                                      IF @accClass = 4
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 303'; 
                                         END
                                      IF @accClass = 6
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 407'; 
                                         END
                                  END

                             IF @trnNature = 108 AND @accType = 0
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 304' + ' OR PayType = 408';
                                END                   
                             IF @trnNature = 108 AND @accType <> 0
                                BEGIN
                                      IF @accClass = 2
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)); 
                                         END
                                      IF @accClass = 4
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 304'; 
                                         END
                                      IF @accClass = 6
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 408'; 
                                         END
                                  END  

                            IF @trnNature = 403 AND @accType = 0
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 351';
                                END
                             IF @trnNature = 403 AND @accType <> 0
                                BEGIN
                                      IF @accClass = 6
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)); 
                                         END
                                      IF @accClass = 5
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 351'; 
                                         END
                                END

                            IF @trnNature = 402 AND @accType = 0
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 352';
                                END
                             IF @trnNature = 402 AND @accType <> 0
                                BEGIN
                                      IF @accClass = 6
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)); 
                                         END
                                      IF @accClass = 5
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 352'; 
                                         END
                                END
                     
                       END
                    ELSE
                        PRINT @trnNature;
                        PRINT @accType;
                        PRINT @accClass;
                        IF @trnNature = 5 AND @accType = 0
                           BEGIN
                               SET @strSQL = @strSQL + ' AND PayType = 105 OR PayType = 203';
                           END
                        IF @trnNature = 5 AND @accType <> 0
                           BEGIN
                               IF @accClass = 2
                                  BEGIN
                                       SET @strSQL = @strSQL + ' AND PayType = 105'; 
                                  END
                               IF @accClass = 3
                                  BEGIN
                                       SET @strSQL = @strSQL + ' AND PayType = 203'; 
                                  END
                           END
                         IF @trnNature <> 5
                            BEGIN
	        			    	 SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@trnNature AS VARCHAR(2));
                            END
				END	 

            
			EXECUTE (@strSQL);
            PRINT @strSQL;

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

		SET @strSQL = 'INSERT INTO WFTRANSACTIONLIST (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,RepUserID)' +
				' SELECT ' +
				'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
				'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
				'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
				'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,ValueDate,UserIP,UserID,VerifyUserID,CreateDate,' + CAST(@UserID AS VARCHAR(4)) +
				' FROM A2ZCSMCUS..A2ZTRANSACTION WHERE TrnProcStat = 0 AND TrnFlag = 0 AND TrnCSGL = 0 AND TrnDate' +
				' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
         
            IF  @trnModule = 1
                BEGIN
                     SET @strSQL = @strSQL + ' AND TrnModule != 4';       
                END  
            
            IF  @trnModule <> 1
                BEGIN
                     SET @strSQL = @strSQL + ' AND TrnModule = ' + CAST(@trnModule AS VARCHAR(2));       
                END  
            
            IF @transSw = 0 AND @trnNature = 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND ShowInterest = 0';
				END	

            IF @transSw = 1 AND @trnNature = 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND TrnSysUser = 1';
				END

            IF @accType <> 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND AccType = ' + CAST(@accType AS VARCHAR(2));
				END	

            IF @99Func <> 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@99Func AS VARCHAR(3));
				END	

            IF @cuType <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND CuType = ' + CAST(@cuType AS VARCHAR(2)) + ' AND CuNo = ' + CAST(@cuNo AS VARCHAR(6));
				END				
            
            IF @FcashCode <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND FromCashCode = ' + CAST(@FcashCode AS VARCHAR(8));
				END				
             
--            IF @trnType <> 0
--				BEGIN
--					SET @strSQL = @strSQL + ' AND TrnType = ' + CAST(@trnType AS VARCHAR(2));
--				END	


            IF @trnType <> 0
				BEGIN
                    IF @trnType = 1
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1 AND TrnVchType <> ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @trnType = 2
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3 AND TrnVchType <> ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @trnType = 3
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1 AND TrnVchType = ''' + CAST(@VType AS VARCHAR(1)) + '''';
                       END
                    IF @trnType = 4
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 1';
                       END
                    IF @trnType = 5
                       BEGIN
					        SET @strSQL = @strSQL + ' AND TrnType = 3';
                       END
				END


            IF @vchNo <> '0'
				BEGIN
					SET @strSQL = @strSQL + ' AND VchNo = ' + '''' + CAST(@vchNo AS VARCHAR(20)) + '''';
				END	
    
            IF @teller <> 0
				BEGIN
					SET @strSQL = @strSQL + ' AND UserID = ' + CAST(@teller AS VARCHAR(4));
				END	             

            IF @trnMode <> 0
				BEGIN
                    SET @trnDrCr = @trnMode - 1;
					SET @strSQL = @strSQL + ' AND TrnDrCr = ' + CAST(@trnDrCr AS VARCHAR(2));
				END	


            IF @trnNature <> 0
				BEGIN
                    IF @trnNature > 100
                       BEGIN
                             IF @trnNature = 113
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 212' + ' OR PayType = 307' + ' OR PayType = 410';
                                END
                             IF @trnNature = 114
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 213' + ' OR PayType = 308' + ' OR PayType = 411';
                                END
                             IF @trnNature = 107 AND @accType = 0
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 303' + ' OR PayType = 407';
                                END
                             IF @trnNature = 107 AND @accType <> 0
                                BEGIN
                                      IF @accClass = 2
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)); 
                                         END
                                      IF @accClass = 4
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 303'; 
                                         END
                                      IF @accClass = 6
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 407'; 
                                         END
                                  END

                             IF @trnNature = 108 AND @accType = 0
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 304' + ' OR PayType = 408';
                                END                   
                             IF @trnNature = 108 AND @accType <> 0
                                BEGIN
                                      IF @accClass = 2
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)); 
                                         END
                                      IF @accClass = 4
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 304'; 
                                         END
                                      IF @accClass = 6
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 408'; 
                                         END
                                  END   

                             IF @trnNature = 403 AND @accType = 0
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 351';
                                END
                             IF @trnNature = 403 AND @accType <> 0
                                BEGIN
                                      IF @accClass = 6
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)); 
                                         END
                                      IF @accClass = 5
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 351'; 
                                         END
                                END

                            IF @trnNature = 402 AND @accType = 0
                                BEGIN
                                      SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)) + ' OR PayType = 352';
                                END
                             IF @trnNature = 402 AND @accType <> 0
                                BEGIN
                                      IF @accClass = 6
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = ' + CAST(@trnNature AS VARCHAR(3)); 
                                         END
                                      IF @accClass = 5
                                         BEGIN
                                              SET @strSQL = @strSQL + ' AND PayType = 352'; 
                                         END
                                END          
                       END
                    ELSE
                        IF @trnNature = 5 AND @accType = 0
                           BEGIN
                               SET @strSQL = @strSQL + ' AND PayType = 105 OR PayType = 203';
                           END
                        IF @trnNature = 5 AND @accType <> 0
                           BEGIN
                               IF @accClass = 2
                                  BEGIN
                                       SET @strSQL = @strSQL + ' AND PayType = 105'; 
                                  END
                               IF @accClass = 3
                                  BEGIN
                                       SET @strSQL = @strSQL + ' AND PayType = 203'; 
                                  END
                           END
                         IF @trnNature <> 5
                            BEGIN
	        			    	 SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@trnNature AS VARCHAR(2));
                            END
				END	 

	
	EXECUTE (@strSQL);
   
   IF @trnNature = 5 AND @accClass = 3
      BEGIN
           UPDATE WFTRANSACTIONLIST SET WFTRANSACTIONLIST.TrnDebit = WFTRANSACTIONLIST.GLAmount;
      END


   
   DELETE FROM WFTRANSACTIONLIST WHERE TrnDebit = 0 AND TrnCredit = 0;
      
    
	
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

