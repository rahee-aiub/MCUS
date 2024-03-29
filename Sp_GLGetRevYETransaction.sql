USE [A2ZGLMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GLGetRevYETransaction]    Script Date: 06/18/2017 13:05:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE  [dbo].[Sp_GLGetRevYETransaction](@VoucherNo nvarchar(20),@UserId int,@TrnDate VARCHAR(10))


AS
/*

EXECUTE Sp_GLGetRevYETransaction 'SAIF',1,'2017-06-29'


*/


BEGIN

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @accNo Bigint;
DECLARE @FuncOpt smallint;

DECLARE @fDate VARCHAR(10);
DECLARE @tDate VARCHAR(10);

DECLARE @AccStatus smallint;
DECLARE @strSQL NVARCHAR(MAX);

DECLARE @PrmYearEndDate SMALLDATETIME;
DECLARE @IYear int;


DELETE FROM WF_REVERSETRANSACTION WHERE DelUserId = @UserId OR VchNo = @VoucherNo;


SET @PrmYearEndDate = (SELECT PrmYearEndDate FROM A2ZHKMCUS..A2ZERPSYSPRM);

SET @IYear = YEAR(@PrmYearEndDate);

SET @fDate = @TrnDate;
SET @tDate = @TrnDate;


SET @strSQL = 'INSERT INTO WF_REVERSETRANSACTION (TrnId,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,PayType,' +
			  'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,ShowInterest,TrnInterestAmt,' +
			  'TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,' +
			  'GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnSysUser,ValueDate,DelUserId,UserID)' +
			  ' SELECT ' +
			  'Id,TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,FuncOpt,PayType,' +
			  'TrnType,TrnDrCr,TrnDebit,TrnCredit,TrnDesc,ShowInterest,TrnInterestAmt,' +
			  'TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,' +
			  'GLDebitAmt,GLCreditAmt,TrnFlag,FromCashCode,TrnSysUser,ValueDate,' + CAST(@UserId AS VARCHAR(4)) + ',UserID' +
			  ' FROM A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION WHERE TrnCSGL = 1 AND TrnDate' +
			  ' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';
		        
              SET @strSQL = @strSQL + ' AND VchNo = ' + '''' + @VoucherNo +  '''';  


EXECUTE (@strSQL);



END












