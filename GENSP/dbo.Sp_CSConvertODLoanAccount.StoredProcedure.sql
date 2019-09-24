USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSConvertODLoanAccount]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CSConvertODLoanAccount]

AS
BEGIN

DECLARE @cuType int;
DECLARE @cuNo int;
DECLARE @memNo int;
DECLARE @accType int;
DECLARE @accNo Bigint;
DECLARE @NewaccNo Bigint;

DECLARE @CTYPE varchar(20);
DECLARE @CNO varchar(20);
DECLARE @Input varchar(10);
DECLARE @Result varchar(10);
DECLARE @AccountNo varchar(16);
DECLARE @PayTypevalue varchar(20);

DECLARE @CountAccNo varchar (16);
DECLARE @RestAccNo varchar(14);
DECLARE @XAcctype varchar(2);




DECLARE wfODTable CURSOR FOR
SELECT CuNo,MemNo,AccType FROM wfODLOAN;

OPEN wfODTable;
FETCH NEXT FROM wfODTable INTO @cuNo,@memNo,@accType;

WHILE @@FETCH_STATUS = 0 
	BEGIN
    
     set @XAcctype = @accType;

     SET @CountAccNo= (SELECT AccNo FROM A2ZACCOUNT WHERE CuNo = @cuNo AND MemNo=@memNo AND AccType=52);
    
     set @RestAccNo = RIGHT(@CountAccNo,14);

     set @AccountNo = @XAcctype + @RestAccNo;

     set @NewaccNo = @AccountNo;
     
     IF @accType <> 52	
        BEGIN	
             UPDATE A2ZACCOUNT SET AccType = @accType,AccNo=@NewaccNo
	                      WHERE CuNo = @cuNo AND MemNo=@memNo AND AccType=52;

             UPDATE A2ZCSMCUST2015..A2ZCSOPBALANCE SET AccType = @accType,AccNo=@NewaccNo
	                      WHERE CuNo = @cuNo AND MemNo=@memNo AND AccType=52;
        END

      

FETCH NEXT FROM wfODTable INTO @cuNo,@memNo,@accType;
END
CLOSE wfODTable; 
DEALLOCATE wfODTable;

END

GO
