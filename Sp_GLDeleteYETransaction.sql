
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE  [dbo].[Sp_GLDeleteYETransaction](@VoucherNo nvarchar(20),@UserId int,@TrnDate VARCHAR(10))


AS
/*

EXECUTE Sp_GLDeleteYETransaction '140378',1,'2017-01-31'


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



SET @PrmYearEndDate = (SELECT PrmYearEndDate FROM A2ZHKMCUS..A2ZERPSYSPRM);

SET @IYear = YEAR(@PrmYearEndDate);

SET @fDate = @TrnDate;
SET @tDate = @TrnDate;


UPDATE A2ZGLMCUS..A2ZCGLMST SET GLOpBal = 0; 

          

SET @strSQL = 'UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLOpBal = ' +
              'A2ZGLMCUS..A2ZCGLMST.GLOpBal + ISNULL((SELECT SUM(GLAmount)' +
              ' FROM A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION' +	
              ' WHERE TrnDate =' + '''' + @fDate +  '''' + ' AND VchNo = ' + '''' + @VoucherNo +  '''' +
              ' AND A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo),0)' +
 
              ' FROM A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION,A2ZGLMCUS..A2ZCGLMST' +	
              ' WHERE TrnDate =' + '''' + @fDate +  '''' + ' AND VchNo = ' + '''' + @VoucherNo +  '''' +
              ' AND A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo';  		   		 
              

EXECUTE (@strSQL);


UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance - A2ZGLMCUS..A2ZCGLMST.GLOpBal           
FROM A2ZGLMCUS..A2ZCGLMST WHERE GLOpBal <> 0;         



SET @strSQL = 'DELETE FROM A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION' +			  
			  ' WHERE TrnDate =' + '''' + @fDate +  '''' + ' AND VchNo = ' + '''' + @VoucherNo +  '''';  


EXECUTE (@strSQL);



END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

