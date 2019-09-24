USE [A2ZHRMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_HRBankAdvise]    Script Date: 02/13/2019 9:52:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [dbo].[Sp_HRBankAdvise](@fDate SMALLDATETIME,@BankCode INT,@Area INT, @Location INT)     



AS

BEGIN


DECLARE @EmpCode int;
DECLARE @EmpName nvarchar(50);
DECLARE @NetPayment money;
DECLARE @EmpBank int;
DECLARE @EmpAccNo nvarchar(50);

DECLARE @EmpArea int;
DECLARE @EmpAreaDesc nvarchar(50);

DECLARE @EmpLocation int;
DECLARE @EmpLocationDesc nvarchar(50);


TRUNCATE TABLE WFBANKADVISE;


IF @BankCode = 99
   BEGIN
        DECLARE EmpTable CURSOR FOR
        SELECT EmpCode,EmpName,EmpBank,EmpAccNo,EmpArea,EmpAreaDesc,EmpLocation,EmpLocationDesc
        FROM A2ZEMPLOYEE WHERE EmpBank IS NULL;
   END
ELSE
   BEGIN
        DECLARE EmpTable CURSOR FOR
        SELECT EmpCode,EmpName,EmpBank,EmpAccNo,EmpArea,EmpAreaDesc,EmpLocation,EmpLocationDesc
        FROM A2ZEMPLOYEE WHERE EmpBank = @BankCode;
   END

OPEN EmpTable;
FETCH NEXT FROM EmpTable INTO
@EmpCode,@EmpName,@EmpBank,@EmpAccNo,@EmpArea,@EmpAreaDesc,@EmpLocation,@EmpLocationDesc;

WHILE @@FETCH_STATUS = 0 
	BEGIN
    
    SET @NetPayment = 0;

    SET @NetPayment= (SELECT NetPayment FROM A2ZEMPFSALARY WHERE EmpCode = @EmpCode AND MONTH(SalDate) = MONTH(@fDate) AND YEAR(SalDate) = YEAR(@fDate));

    IF @NetPayment > 0
       BEGIN
           INSERT INTO WFBANKADVISE(EmpBank,EmpNo,EmpName,EmpAccNo,NetPayment,EmpArea,EmpAreaDesc,EmpLocation,EmpLocationDesc)

           VALUES (@BankCode,@EmpCode,@EmpName,@EmpAccNo,@NetPayment,@EmpArea,@EmpAreaDesc,@EmpLocation,@EmpLocationDesc); 

        END

	FETCH NEXT FROM EmpTable INTO
        @EmpCode,@EmpName,@EmpBank,@EmpAccNo,@EmpArea,@EmpAreaDesc,@EmpLocation,@EmpLocationDesc;       

	END

CLOSE EmpTable; 
DEALLOCATE EmpTable;


IF @Area != 0
   BEGIN
     DELETE FROM WFBANKADVISE  WHERE EmpArea != @Area;
   END

IF @Location != 0
   BEGIN
     DELETE FROM WFBANKADVISE  WHERE EmpLocation != @Location;
   END




END































































GO

