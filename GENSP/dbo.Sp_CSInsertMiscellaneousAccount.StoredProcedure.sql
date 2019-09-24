USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSInsertMiscellaneousAccount]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_CSInsertMiscellaneousAccount](@cuType int,@cuNo int)

AS

BEGIN


DECLARE @PayType int;
DECLARE @CTYPE varchar(20);
DECLARE @CNO varchar(20);
DECLARE @Input varchar(10);
DECLARE @Result varchar(10);
DECLARE @AccountNo varchar(20);
DECLARE @PayTypevalue varchar(20);
DECLARE @CountAccNo varchar(20);
   

    DECLARE PayTypeTable CURSOR FOR
    SELECT PayType FROM A2ZPAYTYPE WHERE AtyClass=7;
    OPEN PayTypeTable;
    FETCH NEXT FROM PayTypeTable INTO @PayType;

    WHILE @@FETCH_STATUS = 0 
	BEGIN
     

     SET @PayTypevalue=@PayType;
   
     SET @CTYPE = @cuType;  
     SET @CNO=@cuNo;
     SET @Input=len(@CNO);
     
     IF(@Input='1')
		BEGIN
			SET @Result='000'; 
		END
     IF(@Input='2')
		BEGIN
			SET @Result='00'; 
		END
    IF(@Input='3')
       BEGIN
			SET @Result='0';
       END

     IF(@Input!='4')
       BEGIN
			SET @AccountNo='99'+@CTYPE+@Result+@CNO+'00000'+'0'+@PayTypevalue; 
       END

     IF(@Input='4')
       BEGIN
			SET @AccountNo='99'+@CTYPE+@CNO+'00000'+'0'+@PayTypevalue; 
       END
      
          
      SET @CountAccNo= (SELECT AccNo FROM A2ZACCOUNT WHERE  AccNo=@AccountNo);
      
		
      IF(@CountAccNo IS NULL)
        BEGIN
			INSERT INTO A2ZACCOUNT(AccType,CuType,CuNo,MemNo,AccNo,AccStatus,AccAtyClass)
			VALUES(99,@cuType,@cuNo,0,@AccountNo,1,7);       
        END
    
    FETCH NEXT FROM PayTypeTable INTO @PayType;
      
    END

	
	CLOSE PayTypeTable; 
	DEALLOCATE PayTypeTable;

END











GO
