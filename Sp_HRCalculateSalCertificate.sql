
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_HRCalculateSalCertificate](@fDate SMALLDATETIME,@EmpNumber INT,@Area INT, @Location INT,@Project INT,@Religion INT,@Gender INT,@Status INT)   


AS
BEGIN

/*

EXECUTE Sp_HRCalculateSalCertificate '2016-03-31',1

*/

DECLARE @code int;
DECLARE @description nvarchar(50);
DECLARE @repcolumn int;

DECLARE @EmpCode int;
DECLARE @EmpNo int;

DECLARE @ENo INT;

DECLARE @EmpName nvarchar(50);
DECLARE @EmpDesigDesc nvarchar(50);
DECLARE @EmpGradeDesc nvarchar(50);
DECLARE @EmpAreaDesc nvarchar(50);

DECLARE @EmpGrade int;
DECLARE @EmpPayLabel int;

DECLARE @Code1Desc nvarchar(50);

DECLARE @TACodeNo1 int;
DECLARE @TACodeNo2 int;
DECLARE @TACodeNo3 int;
DECLARE @TACodeNo4 int;
DECLARE @TACodeNo5 int;
DECLARE @TACodeNo6 int;
DECLARE @TACodeNo7 int;
DECLARE @TACodeNo8 int;
DECLARE @TACodeNo9 int;
DECLARE @TACodeNo10 int;
DECLARE @TACodeNo11 int;
DECLARE @TACodeNo12 int;
DECLARE @TACodeNo13 int;
DECLARE @TACodeNo14 int;
DECLARE @TACodeNo15 int;
DECLARE @TACodeNo16 int;
DECLARE @TACodeNo17 int;
DECLARE @TACodeNo18 int;
DECLARE @TACodeNo19 int;
DECLARE @TACodeNo20 int;

DECLARE @TACodeDesc1 nvarchar(50);
DECLARE @TACodeDesc2 nvarchar(50);
DECLARE @TACodeDesc3 nvarchar(50);
DECLARE @TACodeDesc4 nvarchar(50);
DECLARE @TACodeDesc5 nvarchar(50);
DECLARE @TACodeDesc6 nvarchar(50);
DECLARE @TACodeDesc7 nvarchar(50);
DECLARE @TACodeDesc8 nvarchar(50);
DECLARE @TACodeDesc9 nvarchar(50);
DECLARE @TACodeDesc10 nvarchar(50);
DECLARE @TACodeDesc11 nvarchar(50);
DECLARE @TACodeDesc12 nvarchar(50);
DECLARE @TACodeDesc13 nvarchar(50);
DECLARE @TACodeDesc14 nvarchar(50);
DECLARE @TACodeDesc15 nvarchar(50);
DECLARE @TACodeDesc16 nvarchar(50);
DECLARE @TACodeDesc17 nvarchar(50);
DECLARE @TACodeDesc18 nvarchar(50);
DECLARE @TACodeDesc19 nvarchar(50);
DECLARE @TACodeDesc20 nvarchar(50);

DECLARE @TDCodeNo1 int;
DECLARE @TDCodeNo2 int;
DECLARE @TDCodeNo3 int;
DECLARE @TDCodeNo4 int;
DECLARE @TDCodeNo5 int;
DECLARE @TDCodeNo6 int;
DECLARE @TDCodeNo7 int;
DECLARE @TDCodeNo8 int;
DECLARE @TDCodeNo9 int;
DECLARE @TDCodeNo10 int;
DECLARE @TDCodeNo11 int;
DECLARE @TDCodeNo12 int;
DECLARE @TDCodeNo13 int;
DECLARE @TDCodeNo14 int;
DECLARE @TDCodeNo15 int;
DECLARE @TDCodeNo16 int;
DECLARE @TDCodeNo17 int;
DECLARE @TDCodeNo18 int;
DECLARE @TDCodeNo19 int;
DECLARE @TDCodeNo20 int;

DECLARE @TDCodeDesc1 nvarchar(50);
DECLARE @TDCodeDesc2 nvarchar(50);
DECLARE @TDCodeDesc3 nvarchar(50);
DECLARE @TDCodeDesc4 nvarchar(50);
DECLARE @TDCodeDesc5 nvarchar(50);
DECLARE @TDCodeDesc6 nvarchar(50);
DECLARE @TDCodeDesc7 nvarchar(50);
DECLARE @TDCodeDesc8 nvarchar(50);
DECLARE @TDCodeDesc9 nvarchar(50);
DECLARE @TDCodeDesc10 nvarchar(50);
DECLARE @TDCodeDesc11 nvarchar(50);
DECLARE @TDCodeDesc12 nvarchar(50);
DECLARE @TDCodeDesc13 nvarchar(50);
DECLARE @TDCodeDesc14 nvarchar(50);
DECLARE @TDCodeDesc15 nvarchar(50);
DECLARE @TDCodeDesc16 nvarchar(50);
DECLARE @TDCodeDesc17 nvarchar(50);
DECLARE @TDCodeDesc18 nvarchar(50);
DECLARE @TDCodeDesc19 nvarchar(50);
DECLARE @TDCodeDesc20 nvarchar(50);


DECLARE @TAAmount1 money;
DECLARE @TAAmount2 money;
DECLARE @TAAmount3 money;
DECLARE @TAAmount4 money;
DECLARE @TAAmount5 money;
DECLARE @TAAmount6 money;
DECLARE @TAAmount7 money;
DECLARE @TAAmount8 money;
DECLARE @TAAmount9 money;
DECLARE @TAAmount10 money;
DECLARE @TAAmount11 money;
DECLARE @TAAmount12 money;
DECLARE @TAAmount13 money;
DECLARE @TAAmount14 money;
DECLARE @TAAmount15 money;
DECLARE @TAAmount16 money;
DECLARE @TAAmount17 money;
DECLARE @TAAmount18 money;
DECLARE @TAAmount19 money;
DECLARE @TAAmount20 money;

DECLARE @GrossTotal money;

DECLARE @TDAmount1 money;
DECLARE @TDAmount2 money;
DECLARE @TDAmount3 money;
DECLARE @TDAmount4 money;
DECLARE @TDAmount5 money;
DECLARE @TDAmount6 money;
DECLARE @TDAmount7 money;
DECLARE @TDAmount8 money;
DECLARE @TDAmount9 money;
DECLARE @TDAmount10 money;
DECLARE @TDAmount11 money;
DECLARE @TDAmount12 money;
DECLARE @TDAmount13 money;
DECLARE @TDAmount14 money;
DECLARE @TDAmount15 money;
DECLARE @TDAmount16 money;
DECLARE @TDAmount17 money;
DECLARE @TDAmount18 money;
DECLARE @TDAmount19 money;
DECLARE @TDAmount20 money;


DECLARE @DeductTotal money;
DECLARE @NetPayment money;

DECLARE @Amount1 money;
DECLARE @Allowance2 money;
DECLARE @Allowance3 money;
DECLARE @Allowance4 money;
DECLARE @Allowance5 money;

DECLARE @Deduction1 money;
DECLARE @Deduction2 money;
DECLARE @Deduction3 money;
DECLARE @Deduction4 money;
DECLARE @Deduction5 money;

DECLARE @Basic money;
DECLARE @Consulated money;

DECLARE @recline int;


TRUNCATE TABLE WFSCERTIFICATE;
TRUNCATE TABLE WFSALARYCERTIFICATE;


INSERT INTO WFSCERTIFICATE (EmpNo,EmpArea,EmpLocation,EmpProject,EmpRelagion,EmpGender,Status) SELECT 
EmpCode,EmpArea,EmpLocation,EmpProject,EmpRelagion,EmpGender,Status 
FROM A2ZEMPFSALARY 
WHERE MONTH(SalDate) = MONTH(@fDate) AND YEAR(SalDate) = YEAR(@fDate);

IF @EmpNumber != 0
   BEGIN
     DELETE FROM WFSCERTIFICATE  WHERE EmpNo != @EmpNumber;
   END


IF @Area != 0
   BEGIN
     DELETE FROM WFSCERTIFICATE  WHERE EmpArea != @Area;
   END

IF @Location != 0
   BEGIN
     DELETE FROM WFSCERTIFICATE  WHERE EmpLocation != @Location;
   END

IF @Project != 0
   BEGIN
     DELETE FROM WFSCERTIFICATE  WHERE EmpProject != @Project;
   END

IF @Religion != 0
   BEGIN
     DELETE FROM WFSCERTIFICATE  WHERE EmpRelagion != @Religion;
   END

IF @Gender != 0
   BEGIN
     DELETE FROM WFSCERTIFICATE  WHERE EmpGender != @Gender;
   END

IF @Status != 0
   BEGIN
     DELETE FROM WFSCERTIFICATE  WHERE Status != @Status;
   END


DECLARE EmpTable CURSOR FOR
  SELECT EmpNo FROM WFSCERTIFICATE;

OPEN EmpTable;
FETCH NEXT FROM EmpTable INTO @EmpNo;


WHILE @@FETCH_STATUS = 0 
	BEGIN


DECLARE salaryTable CURSOR FOR
SELECT EmpCode,EmpName,EmpDesigDesc,EmpGrade,EmpAreaDesc,EmpPayLabel,BasicAmount,ConsolidatedAmt,TACodeNo1,TAAmount1,TACodeNo2,TAAmount2,
       TACodeNo3,TAAmount3,TACodeNo4,TAAmount4,TACodeNo5,TAAmount5,
       TACodeNo6,TAAmount6,TACodeNo7,TAAmount7,TACodeNo8,TAAmount8,
       TACodeNo9,TAAmount9,TACodeNo10,TAAmount10,TACodeNo11,TAAmount11,
       TACodeNo12,TAAmount12,TACodeNo13,TAAmount13,TACodeNo14,TAAmount14,
       TACodeNo15,TAAmount15,TACodeNo16,TAAmount16,TACodeNo17,TAAmount17,
       TACodeNo18,TAAmount18,TACodeNo19,TAAmount19,TACodeNo20,TAAmount20,GrossTotal,
       TACodeDesc1,TACodeDesc2,TACodeDesc3,TACodeDesc4,TACodeDesc5,
       TACodeDesc6,TACodeDesc7,TACodeDesc8,TACodeDesc9,TACodeDesc10,TACodeDesc11,TACodeDesc12,
       TACodeDesc13,TACodeDesc14,TACodeDesc15,TACodeDesc16,TACodeDesc17,TACodeDesc18, 
       TACodeDesc19,TACodeDesc20,
       TDCodeNo1,TDAmount1,TDCodeNo2,TDAmount2,TDCodeNo3,TDAmount3,TDCodeNo4,TDAmount4,
       TDCodeNo5,TDAmount5,TDCodeNo6,TDAmount6,TDCodeNo7,TDAmount7,TDCodeNo8,TDAmount8,
       TDCodeNo9,TDAmount9,TDCodeNo10,TDAmount10,TDCodeNo11,TDAmount11,TDCodeNo12,TDAmount12,
       TDCodeNo13,TDAmount13,TDCodeNo14,TDAmount14,TDCodeNo15,TDAmount15,TDCodeNo16,TDAmount16,
       TDCodeNo17,TDAmount17,TDCodeNo18,TDAmount18,TDCodeNo19,TDAmount19,
       TDCodeNo20,TDAmount20,DeductTotal,NetPayment,
       TDCodeDesc1,TDCodeDesc2,TDCodeDesc3,TDCodeDesc4,TDCodeDesc5,
       TDCodeDesc6,TDCodeDesc7,TDCodeDesc8,TDCodeDesc9,TDCodeDesc10,TDCodeDesc11,TDCodeDesc12,
       TDCodeDesc13,TDCodeDesc14,TDCodeDesc15,TDCodeDesc16,TDCodeDesc17,TACodeDesc18,
       TDCodeDesc19,TDCodeDesc20
FROM A2ZEMPFSALARY WHERE EmpCode = @EmpNo AND MONTH(SalDate) = MONTH(@fDate) AND YEAR(SalDate) = YEAR(@fDate);

OPEN salaryTable;
FETCH NEXT FROM salaryTable INTO
@EmpCode,@EmpName,@EmpDesigDesc,@EmpGrade,@EmpAreaDesc,@EmpPayLabel,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
       @TACodeNo3,@TAAmount3,@TACodeNo4,@TAAmount4,@TACodeNo5,@TAAmount5,
       @TACodeNo6,@TAAmount6,@TACodeNo7,@TAAmount7,@TACodeNo8,@TAAmount8,
       @TACodeNo9,@TAAmount9,@TACodeNo10,@TAAmount10,@TACodeNo11,@TAAmount11,
       @TACodeNo12,@TAAmount12,@TACodeNo13,@TAAmount13,@TACodeNo14,@TAAmount14,
       @TACodeNo15,@TAAmount15,@TACodeNo16,@TAAmount16,@TACodeNo17,@TAAmount17,
       @TACodeNo18,@TAAmount18,@TACodeNo19,@TAAmount19,@TACodeNo20,@TAAmount20,@GrossTotal,
       @TACodeDesc1,@TACodeDesc2,@TACodeDesc3,@TACodeDesc4,@TACodeDesc5,
       @TACodeDesc6,@TACodeDesc7,@TACodeDesc8,@TACodeDesc9,@TACodeDesc10,@TACodeDesc11,@TACodeDesc12,
       @TACodeDesc13,@TACodeDesc14,@TACodeDesc15,@TACodeDesc16,@TACodeDesc17,@TACodeDesc18, 
       @TACodeDesc19,@TACodeDesc20,
       @TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
       @TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
       @TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,@TDCodeNo11,@TDAmount11,@TDCodeNo12,@TDAmount12,
       @TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,@TDCodeNo15,@TDAmount15,@TDCodeNo16,@TDAmount16,
       @TDCodeNo17,@TDAmount17,@TDCodeNo18,@TDAmount18,@TDCodeNo19,@TDAmount19,
       @TDCodeNo20,@TDAmount20,@DeductTotal,@NetPayment,
       @TDCodeDesc1,@TDCodeDesc2,@TDCodeDesc3,@TDCodeDesc4,@TDCodeDesc5,
       @TDCodeDesc6,@TDCodeDesc7,@TDCodeDesc8,@TDCodeDesc9,@TDCodeDesc10,@TDCodeDesc11,@TDCodeDesc12,
       @TDCodeDesc13,@TDCodeDesc14,@TDCodeDesc15,@TDCodeDesc16,@TDCodeDesc17,@TACodeDesc18,
       @TDCodeDesc19,@TDCodeDesc20;


WHILE @@FETCH_STATUS = 0 
	BEGIN
    
    SET @Amount1 = 0;
        
    SET @recline = 0;
    

    IF @Basic > 0
       BEGIN
           SET @Amount1 = (ISNULL(@Basic,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,0,'Basic',@Amount1);
       END
         
    IF @Consulated > 0
       BEGIN
           SET @Amount1 = (ISNULL(@Consulated,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,0,'Consolidated',@Amount1);
       END

    IF @TACodeNo1 > 0 AND @TAAmount1 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount1,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo1,@TACodeDesc1,@Amount1);
       END
          
     IF @TACodeNo2 > 0 AND @TAAmount2 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount2,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo2,@TACodeDesc2,@Amount1);
       END

     IF @TACodeNo3 > 0 AND @TAAmount3 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount3,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo3,@TACodeDesc3,@Amount1);
       END
     IF @TACodeNo4 > 0 AND @TAAmount4 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount4,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo4,@TACodeDesc4,@Amount1);
       END
          
     IF @TACodeNo5 > 0 AND @TAAmount5 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount5,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo5,@TACodeDesc5,@Amount1);
       END

     IF @TACodeNo6 > 0 AND @TAAmount6 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount6,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo6,@TACodeDesc6,@Amount1);
       END

     IF @TACodeNo7 > 0 AND @TAAmount7 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount7,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo7,@TACodeDesc7,@Amount1);
       END
          
     IF @TACodeNo8 > 0 AND @TAAmount8 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount8,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo8,@TACodeDesc8,@Amount1);
       END

     IF @TACodeNo9 > 0 AND @TAAmount9 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount9,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo9,@TACodeDesc9,@Amount1);
       END

     IF @TACodeNo10 > 0 AND @TAAmount10 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount10,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo10,@TACodeDesc10,@Amount1);
       END

    IF @TACodeNo11 > 0 AND @TAAmount11 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount11,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo11,@TACodeDesc11,@Amount1);
       END
          
     IF @TACodeNo12 > 0 AND @TAAmount12 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount12,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo12,@TACodeDesc12,@Amount1);
       END

     IF @TACodeNo13 > 0 AND @TAAmount13 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount13,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo13,@TACodeDesc13,@Amount1);
       END
     IF @TACodeNo14 > 0 AND @TAAmount14 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount14,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo14,@TACodeDesc14,@Amount1);
       END
          
     IF @TACodeNo15 > 0 AND @TAAmount15 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount15,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo15,@TACodeDesc15,@Amount1);
       END

     IF @TACodeNo16 > 0 AND @TAAmount16 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount16,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo16,@TACodeDesc16,@Amount1);
       END

     IF @TACodeNo17 > 0 AND @TAAmount17 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount17,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo17,@TACodeDesc17,@Amount1);
       END
          
     IF @TACodeNo18 > 0 AND @TAAmount18 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount18,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo18,@TACodeDesc18,@Amount1);
       END

     IF @TACodeNo19 > 0 AND @TAAmount19 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount9,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo19,@TACodeDesc19,@Amount1);
       END

     IF @TACodeNo20 > 0 AND @TAAmount20 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TAAmount20,0));
           SET @recline = (@recline + 1);
           INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,AllowCodeNo,AllowCodeDesc,AllowAmount)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TACodeNo20,@TACodeDesc20,@Amount1);
       END
--------------------------------------------------------
    SET @recline = 0;
     

    IF @TDCodeNo1 > 0 AND @TDAmount1 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount1,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo1,@TDCodeDesc1,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo1,DedCodeDesc=@TDCodeDesc1,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END
          
   IF @TDCodeNo2 > 0 AND @TDAmount2 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount2,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo2,@TDCodeDesc2,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo2,DedCodeDesc=@TDCodeDesc2,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

   IF @TDCodeNo3 > 0 AND @TDAmount3 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount3,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo3,@TDCodeDesc3,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo3,DedCodeDesc=@TDCodeDesc3,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo4 > 0 AND @TDAmount4 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount4,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo4,@TDCodeDesc4,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo4,DedCodeDesc=@TDCodeDesc4,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo5 > 0 AND @TDAmount5 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount5,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo5,@TDCodeDesc5,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo5,DedCodeDesc=@TDCodeDesc5,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo6 > 0 AND @TDAmount6 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount6,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo6,@TDCodeDesc6,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo6,DedCodeDesc=@TDCodeDesc6,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END


    IF @TDCodeNo7 > 0 AND @TDAmount7 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount7,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo7,@TDCodeDesc7,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo7,DedCodeDesc=@TDCodeDesc7,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo8 > 0 AND @TDAmount8 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount8,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo8,@TDCodeDesc8,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo8,DedCodeDesc=@TDCodeDesc8,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo9 > 0 AND @TDAmount9 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount9,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo9,@TDCodeDesc9,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo9,DedCodeDesc=@TDCodeDesc9,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo10 > 0 AND @TDAmount10 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount10,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo10,@TDCodeDesc10,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo10,DedCodeDesc=@TDCodeDesc10,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END
    
    IF @TDCodeNo11 > 0 AND @TDAmount11 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount11,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo11,@TDCodeDesc11,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo11,DedCodeDesc=@TDCodeDesc11,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END
          
   IF @TDCodeNo12 > 0 AND @TDAmount12 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount12,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo12,@TDCodeDesc12,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo12,DedCodeDesc=@TDCodeDesc12,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

   IF @TDCodeNo13 > 0 AND @TDAmount13 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount13,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo13,@TDCodeDesc13,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo13,DedCodeDesc=@TDCodeDesc13,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo14 > 0 AND @TDAmount14 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount14,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo14,@TDCodeDesc14,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo14,DedCodeDesc=@TDCodeDesc14,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo15 > 0 AND @TDAmount15 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount15,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo15,@TDCodeDesc15,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo15,DedCodeDesc=@TDCodeDesc15,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo16 > 0 AND @TDAmount16 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount16,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo16,@TDCodeDesc16,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo16,DedCodeDesc=@TDCodeDesc16,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END


    IF @TDCodeNo17 > 0 AND @TDAmount17 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount17,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo17,@TDCodeDesc17,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo17,DedCodeDesc=@TDCodeDesc17,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo18 > 0 AND @TDAmount18 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount18,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo18,@TDCodeDesc18,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo18,DedCodeDesc=@TDCodeDesc18,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo19 > 0 AND @TDAmount19 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount19,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo19,@TDCodeDesc19,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo19,DedCodeDesc=@TDCodeDesc19,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

    IF @TDCodeNo20 > 0 AND @TDAmount20 > 0
       BEGIN   
           SET @Amount1 = (ISNULL(@TDAmount20,0));
           SET @recline = (@recline + 1);

           SET @ENo= (SELECT EmpNo FROM WFSALARYCERTIFICATE WHERE EmpNo=@EmpCode AND RecLine = @recline ); 
           IF @ENo IS NULL
              BEGIN
                   INSERT INTO WFSALARYCERTIFICATE (EmpNo,EmpName,EmpDesigDesc,EmpAreaDesc,GrossTotal,DeductTotal,NetPayment,RecLine,DedCodeNo,DedCodeDesc,DedAmount)  	
                   VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpAreaDesc,@GrossTotal,@DeductTotal,@NetPayment,@recline,@TDCodeNo20,@TDCodeDesc20,@Amount1);
              END    
           ELSE
              BEGIN
                   UPDATE WFSALARYCERTIFICATE SET DedCodeNo=@TDCodeNo20,DedCodeDesc=@TDCodeDesc20,DedAmount=@Amount1 
                   WHERE EmpNo=@EmpCode AND RecLine = @recline;

              END                
       END

      

	FETCH NEXT FROM salaryTable INTO
       @EmpCode,@EmpName,@EmpDesigDesc,@EmpGrade,@EmpAreaDesc,@EmpPayLabel,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
       @TACodeNo3,@TAAmount3,@TACodeNo4,@TAAmount4,@TACodeNo5,@TAAmount5,
       @TACodeNo6,@TAAmount6,@TACodeNo7,@TAAmount7,@TACodeNo8,@TAAmount8,
       @TACodeNo9,@TAAmount9,@TACodeNo10,@TAAmount10,@TACodeNo11,@TAAmount11,
       @TACodeNo12,@TAAmount12,@TACodeNo13,@TAAmount13,@TACodeNo14,@TAAmount14,
       @TACodeNo15,@TAAmount15,@TACodeNo16,@TAAmount16,@TACodeNo17,@TAAmount17,
       @TACodeNo18,@TAAmount18,@TACodeNo19,@TAAmount19,@TACodeNo20,@TAAmount20,@GrossTotal,
       @TACodeDesc1,@TACodeDesc2,@TACodeDesc3,@TACodeDesc4,@TACodeDesc5,
       @TACodeDesc6,@TACodeDesc7,@TACodeDesc8,@TACodeDesc9,@TACodeDesc10,@TACodeDesc11,@TACodeDesc12,
       @TACodeDesc13,@TACodeDesc14,@TACodeDesc15,@TACodeDesc16,@TACodeDesc17,@TACodeDesc18, 
       @TACodeDesc19,@TACodeDesc20,
       @TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
       @TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
       @TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,@TDCodeNo11,@TDAmount11,@TDCodeNo12,@TDAmount12,
       @TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,@TDCodeNo15,@TDAmount15,@TDCodeNo16,@TDAmount16,
       @TDCodeNo17,@TDAmount17,@TDCodeNo18,@TDAmount18,@TDCodeNo19,@TDAmount19,
       @TDCodeNo20,@TDAmount20,@DeductTotal,@NetPayment,
       @TDCodeDesc1,@TDCodeDesc2,@TDCodeDesc3,@TDCodeDesc4,@TDCodeDesc5,
       @TDCodeDesc6,@TDCodeDesc7,@TDCodeDesc8,@TDCodeDesc9,@TDCodeDesc10,@TDCodeDesc11,@TDCodeDesc12,
       @TDCodeDesc13,@TDCodeDesc14,@TDCodeDesc15,@TDCodeDesc16,@TDCodeDesc17,@TACodeDesc18,
       @TDCodeDesc19,@TDCodeDesc20;

	END

    CLOSE salaryTable; 
    DEALLOCATE salaryTable;


FETCH NEXT FROM EmpTable INTO @EmpNo;

       
	END

    CLOSE EmpTable; 
    DEALLOCATE EmpTable;





END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

