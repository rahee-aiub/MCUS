USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_rptCSLedgerBal]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_rptCSLedgerBal] @AccType INT 
AS

/*
EXECUTE Sp_rptCSLedgerBal 12



*/

BEGIN

DECLARE @strSQL NVARCHAR(MAX);

TRUNCATE TABLE WFCSLEDGERBALANCE;


--SET @strSQL = 'INSERT INTO WFTRANSACTIONLIST (BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
--					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
--					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
--					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,UserIP,UserID,VerifyUserID,CreateDate)' +
--					' SELECT ' +
--					'BatchNo,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,FuncOptDesc,PayType,' +
--					'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,TrnVchType,TrnChqPrx,TrnChqNo,ShowInterest,TrnInterestAmt,TrnPenalAmt,' +
--					'TrnChargeAmT,TrnDueIntAmt,TrnODAmount,BranchNo,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,TrnGLFlag,GLAccNo,GLAmount,' +
--					'GLDebitAmt,GLCreditAmt,TrnFlag,TrnStatus,FromCashCode,TrnProcStat,TrnSysUser,TrnModule,UserIP,UserID,VerifyUserID,CreateDate' +
--					' FROM A2ZCSMCUST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION' + 
--                    ' WHERE TrnProcStat = 0 AND TrnFlag = 0 AND TrnCSGL = 0 AND ShowInterest = 0 AND TrnDate' +
--					' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
--			
--            
----            SET @strSQL = @strSQL + ' AND TrnModule = ' + CAST(@trnModule AS VARCHAR(2));
--				
--           
--            IF @accType <> 0
--				BEGIN
--                    SET @strSQL = @strSQL + ' AND AccType = ' + CAST(@accType AS VARCHAR(2));
--				END	
--
--            IF @cuType <> 0
--				BEGIN
--					SET @strSQL = @strSQL + ' AND CuType = ' + CAST(@cuType AS VARCHAR(2)) + ' AND CuNo = ' + CAST(@cuNo AS VARCHAR(6));
--				END				
--            
--            IF @FcashCode <> 0
--				BEGIN
--					SET @strSQL = @strSQL + ' AND FromCashCode = ' + CAST(@FcashCode AS VARCHAR(8));
--				END				
--             
--            IF @trnType <> 0
--				BEGIN
--					SET @strSQL = @strSQL + ' AND TrnType = ' + CAST(@trnType AS VARCHAR(2));
--				END	
--
--            IF @vchNo <> 0
--				BEGIN
--					SET @strSQL = @strSQL + ' AND VchNo = ' + CAST(@vchNo AS VARCHAR(20));
--				END	
--
--            IF @teller <> 0
--				BEGIN
--					SET @strSQL = @strSQL + ' AND UserID = ' + CAST(@teller AS VARCHAR(4));
--				END	   
--          
--            IF @trnMode <> 0
--				BEGIN
--                    SET @trnDrCr = @trnMode - 1;
--					SET @strSQL = @strSQL + ' AND TrnDrCr = ' + CAST(@trnDrCr AS VARCHAR(2));
--				END	               
--
--			EXECUTE (@strSQL);
--
--
--
--
--
--
--
--
--
--
--
--
--
--
-------------------------------------------

SET @strSQL = 'INSERT INTO WFCSLEDGERBALANCE (CuType,CuNo,MemNo,MemName,AccNo,OpenDate,Status,LastTrnDate,Balance)' +          
    
'SELECT dbo.A2ZACCOUNT.CuType,dbo.A2ZACCOUNT.CuNo,dbo.A2ZACCOUNT.MemNo,A2ZMEMBER.MemName,A2ZACCOUNT.AccNo,' +
           'A2ZACCOUNT.AccOpenDate,A2ZSTATUS.AccStatusDescription,A2ZACCOUNT.AccLastTrnDateU,' +
           'A2ZACCOUNT.AccBalance' + 
    

'FROM       dbo.A2ZEMPLOYEE LEFT OUTER JOIN' +
                      'dbo.A2ZMEMBER ON dbo.A2ZACCOUNT.MemNo = dbo.A2ZMEMBER.MemNo' +
                      'LEFT OUTER JOIN' +
                      'dbo.A2ZSTATUS ON dbo.A2ZACCOUNT.AccStatus = dbo.A2ZSTATUS.AccStatusCode';	
      

EXECUTE (@strSQL);
                

END









GO
