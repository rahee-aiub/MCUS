USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSInsertMiscellaneousAccount1]    Script Date: 02/13/2017 09:47:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_CSInsertMiscellaneousAccount1](@cuType int,@cuNo int,@memNo int)

AS

BEGIN


DECLARE @PayType int;
DECLARE @CTYPE varchar(20);
DECLARE @CNO varchar(20);
DECLARE @MNO varchar(20);
DECLARE @Input1 varchar(10);
DECLARE @Result1 varchar(10);
DECLARE @Input2 varchar(10);
DECLARE @Result2 varchar(10);
DECLARE @AccountNo varchar(20);
DECLARE @PayTypevalue varchar(20);
DECLARE @CountAccNo varchar(20);
   

    DECLARE PayTypeTable CURSOR FOR
    SELECT PayType FROM A2ZPAYTYPE WHERE AtyClass=7 and (PayType=507 OR PayType=510);
    OPEN PayTypeTable;
    FETCH NEXT FROM PayTypeTable INTO @PayType;

    WHILE @@FETCH_STATUS = 0 
	BEGIN
     

     SET @PayTypevalue=@PayType;

     SET @CTYPE = @cuType;  
     SET @CNO=@cuNo;
     SET @MNO=@memNo;
     SET @Input1=len(@CNO);
     SET @Input2=len(@MNO);
     
     IF(@Input1='1')
		BEGIN
			SET @Result1='000'; 
		END
     IF(@Input1='2')
		BEGIN
			SET @Result1='00'; 
		END
    IF(@Input1='3')
       BEGIN
			SET @Result1='0';
       END

---------------------------------------------
    IF(@Input2='1')
		BEGIN
			SET @Result2='0000'; 
		END
     IF(@Input2='2')
		BEGIN
			SET @Result2='000'; 
		END
    IF(@Input2='3')
       BEGIN
			SET @Result2='00';
       END

     IF(@Input2='4')
       BEGIN
			SET @Result2='0';
       END

---------------------------------------------

     IF(@Input1!='4' AND @Input2!='5' )
       BEGIN
			SET @AccountNo='99'+@CTYPE+@Result1+@CNO+@Result2+@MNO+'0'+@PayTypevalue; 
       END

     IF(@Input1!='4' AND @Input2='5' )
       BEGIN
			SET @AccountNo='99'+@CTYPE+@Result1+@CNO+@MNO+'0'+@PayTypevalue; 
       END

     IF(@Input1='4' AND @Input2!='5' )
       BEGIN
			SET @AccountNo='99'+@CTYPE+@CNO+@Result2+@MNO+'0'+@PayTypevalue; 
       END

     IF(@Input1='4' AND @Input2='5' )
       BEGIN
			SET @AccountNo='99'+@CTYPE+@CNO+@MNO+'0'+@PayTypevalue; 
       END
      
          
      SET @CountAccNo= (SELECT AccNo FROM A2ZACCOUNT WHERE  AccNo=@AccountNo);
      
		
      IF(@CountAccNo IS NULL)
        BEGIN
			INSERT INTO A2ZACCOUNT(AccType,CuType,CuNo,MemNo,AccNo,AccStatus,AccAtyClass)
			VALUES(99,@cuType,@cuNo,@memNo,@AccountNo,1,7);       
        END
    
    FETCH NEXT FROM PayTypeTable INTO @PayType;
      
    END

	
	CLOSE PayTypeTable; 
	DEALLOCATE PayTypeTable;

END











