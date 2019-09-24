
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE  [dbo].[Sp_GLChangeYESingleTransaction](@Amount1 money,@Amount2 money,@id int,@nflag int)


AS
/*

EXECUTE Sp_GLChangeYESingleTransaction 500,500,1930743,0


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

UPDATE A2ZGLMCUS..A2ZCGLMST SET GLOpBal = 0; 

SET @strSQL = 'UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLOpBal = ' +
              'A2ZGLMCUS..A2ZCGLMST.GLOpBal + ISNULL((SELECT SUM(GLAmount)' +
              ' FROM A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION' +	
              ' WHERE A2ZTRANSACTION.Id between ' + CAST(@id AS VARCHAR(MAX)) + ' AND ' + CAST(@id AS VARCHAR(MAX)) +
              ' AND A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo),0)' + 	
              ' FROM A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION,A2ZGLMCUS..A2ZCGLMST' +	
              ' WHERE A2ZTRANSACTION.Id between ' + CAST(@id AS VARCHAR(MAX)) + ' AND ' + CAST(@id AS VARCHAR(MAX)) +
              ' AND A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo';  		   		 
	  

EXECUTE (@strSQL);


UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance - A2ZGLMCUS..A2ZCGLMST.GLOpBal           
FROM A2ZGLMCUS..A2ZCGLMST WHERE GLOpBal <> 0;      


--string strQuery2 = "UPDATE A2ZTRANSACTION SET  GLDebitAmt = '" + Gamount + "',GLAmount = '" + Camount + "' WHERE Id = '" + idincrement + "' ";

IF @nflag = 0
   BEGIN
        SET @strSQL = 'UPDATE A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION SET GLDebitAmt = ' +	
               CAST(@Amount1 AS VARCHAR(18)) + ',GLAmount =' + CAST(@Amount2 AS VARCHAR(18)) +
              ' WHERE Id=' + CAST(@id AS VARCHAR(MAX)); 
	END 

IF @nflag = 1
   BEGIN
        SET @strSQL = 'UPDATE A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION SET GLCreditAmt = ' +	
               CAST(@Amount1 AS VARCHAR(18)) + ',GLAmount =' + CAST(@Amount2 AS VARCHAR(18)) +
              ' WHERE Id=' + CAST(@id AS VARCHAR(MAX)); 
	END 


--PRINT @strSQL;

EXECUTE (@strSQL);


UPDATE A2ZGLMCUS..A2ZCGLMST SET GLOpBal = 0; 

SET @strSQL = 'UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLOpBal = ' +
              'A2ZGLMCUS..A2ZCGLMST.GLOpBal + ISNULL((SELECT SUM(GLAmount)' +
              ' FROM A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION' +	
              ' WHERE A2ZTRANSACTION.Id between ' + CAST(@id AS VARCHAR(MAX)) + ' AND ' + CAST(@id AS VARCHAR(MAX)) +
              ' AND A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo),0)' + 	
              ' FROM A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION,A2ZGLMCUS..A2ZCGLMST' +	
              ' WHERE A2ZTRANSACTION.Id between ' + CAST(@id AS VARCHAR(MAX)) + ' AND ' + CAST(@id AS VARCHAR(MAX)) +
              ' AND A2ZCSMCUST' + CAST(@IYear AS VARCHAR(4)) + '..A2ZTRANSACTION.GLAccNo = A2ZGLMCUS..A2ZCGLMST.GLAccNo';  		   		 
	  

EXECUTE (@strSQL);


UPDATE A2ZGLMCUS..A2ZCGLMST SET A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance = A2ZGLMCUS..A2ZCGLMST.GLTodaysOpBalance + A2ZGLMCUS..A2ZCGLMST.GLOpBal           
FROM A2ZGLMCUS..A2ZCGLMST WHERE GLOpBal <> 0;      



END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

