
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_HRCalculateMonthlyLayoutBrackUp](@fDate SMALLDATETIME,@CodeNo INT, @Area INT, @Location INT, @Project INT,@Religion INT,@Gender INT,@Status INT,@zeroflag INT)   


AS
BEGIN

/*

EXECUTE Sp_HRCalculateMonthlyLayoutBrackUp '2016-02-29',99,0,0,0,0,0

*/


DECLARE @description nvarchar(50);


DECLARE @EmpCode int;
DECLARE @EmpNo int;
DECLARE @EmpName nvarchar(50);
DECLARE @EmpDesigDesc nvarchar(50);
DECLARE @EmpGradeDesc nvarchar(50);

DECLARE @EmpGrade int;
DECLARE @EmpPayLabel int;

DECLARE @RepColumn int;
DECLARE @RepColumnFlag int;
DECLARE @RepColumnCode int;

DECLARE @StepFlag int;

DECLARE @Code int;
DECLARE @CodeDesc nvarchar(50);
DECLARE @Amount money;

DECLARE @Code1 int;
DECLARE @Code1Desc nvarchar(50);
DECLARE @Amount1 money;

DECLARE @Code2 int;
DECLARE @Code2Desc nvarchar(50);
DECLARE @Amount2 money;

DECLARE @Code3 int;
DECLARE @Code3Desc nvarchar(50);
DECLARE @Amount3 money;

DECLARE @Code4 int;
DECLARE @Code4Desc nvarchar(50);
DECLARE @Amount4 money;

DECLARE @Code5 int;
DECLARE @Code5Desc nvarchar(50);
DECLARE @Amount5 money;

DECLARE @Code6 int;
DECLARE @Code6Desc nvarchar(50);
DECLARE @Amount6 money;


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


TRUNCATE TABLE WFSALARYLAYOUTBRACKUP;

TRUNCATE TABLE WFA2ZEMPLOYEE;


INSERT INTO WFA2ZEMPLOYEE (EmpNo,EmpArea,EmpLocation,EmpProject,EmpRelagion,EmpGender,Status) SELECT 
EmpCode,EmpArea,EmpLocation,EmpProject,EmpRelagion,EmpGender,Status FROM A2ZEMPLOYEE;

IF @Area != 0
   BEGIN
     DELETE FROM WFA2ZEMPLOYEE  WHERE EmpArea != @Area;
   END

IF @Location != 0
   BEGIN
     DELETE FROM WFA2ZEMPLOYEE  WHERE EmpLocation != @Location;
   END

IF @Project != 0
   BEGIN
     DELETE FROM WFA2ZEMPLOYEE  WHERE EmpProject != @Project;
   END

IF @Religion != 0
   BEGIN
     DELETE FROM WFA2ZEMPLOYEE  WHERE EmpRelagion != @Religion;
   END

IF @Gender != 0
   BEGIN
     DELETE FROM WFA2ZEMPLOYEE  WHERE EmpGender != @Gender;
   END

IF @Status != 0
   BEGIN
     DELETE FROM WFA2ZEMPLOYEE  WHERE Status != @Status;
   END


DECLARE EmpTable CURSOR FOR
  SELECT EmpNo FROM WFA2ZEMPLOYEE;

OPEN EmpTable;
FETCH NEXT FROM EmpTable INTO @EmpNo;


WHILE @@FETCH_STATUS = 0 
	BEGIN


DECLARE salaryTable CURSOR FOR
SELECT EmpCode,EmpName,EmpDesigDesc,EmpGrade,EmpPayLabel,BasicAmount,ConsolidatedAmt,TACodeNo1,TAAmount1,TACodeNo2,TAAmount2,
TACodeNo3,TAAmount3,TACodeNo4,TAAmount4,TACodeNo5,TAAmount5,TACodeNo6,TAAmount6,TACodeNo7,TAAmount7,
TACodeNo8,TAAmount8,TACodeNo9,TAAmount9,TACodeNo10,TAAmount10,TACodeNo11,TAAmount11,
TACodeNo12,TAAmount12,TACodeNo13,TAAmount13,TACodeNo14,TAAmount14,TACodeNo15,TAAmount15,
TACodeNo16,TAAmount16,TACodeNo17,TAAmount17,TACodeNo18,TAAmount18,TACodeNo19,TAAmount19,
TACodeNo20,TAAmount20,GrossTotal,TACodeDesc1,TACodeDesc2,TACodeDesc3,TACodeDesc4,TACodeDesc5,
TACodeDesc6,TACodeDesc7,TACodeDesc8,TACodeDesc9,TACodeDesc10,TACodeDesc11,TACodeDesc12,
TACodeDesc13,TACodeDesc14,TACodeDesc15,TACodeDesc16,TACodeDesc17,TACodeDesc18,
TACodeDesc19,TACodeDesc20,
TDCodeNo1,TDAmount1,TDCodeNo2,TDAmount2,TDCodeNo3,TDAmount3,TDCodeNo4,TDAmount4,
TDCodeNo5,TDAmount5,TDCodeNo6,TDAmount6,TDCodeNo7,TDAmount7,TDCodeNo8,TDAmount8,
TDCodeNo9,TDAmount9,TDCodeNo10,TDAmount10,TDCodeNo11,TDAmount11,TDCodeNo12,TDAmount12,
TDCodeNo13,TDAmount13,TDCodeNo14,TDAmount14,TDCodeNo15,TDAmount15,TDCodeNo16,TDAmount16,
TDCodeNo17,TDAmount17,TDCodeNo18,TDAmount18,TDCodeNo19,TDAmount19,TDCodeNo20,TDAmount20,
DeductTotal,NetPayment, TDCodeDesc1,TDCodeDesc2,TDCodeDesc3,TDCodeDesc4,TDCodeDesc5,
TDCodeDesc6,TDCodeDesc7,TDCodeDesc8,TDCodeDesc9,TDCodeDesc10,TDCodeDesc11,TDCodeDesc12,
TDCodeDesc13,TDCodeDesc14,TDCodeDesc15,TDCodeDesc16,TDCodeDesc17,TDCodeDesc18,
TDCodeDesc19,TDCodeDesc20
FROM A2ZEMPFSALARY WHERE EmpCode = @EmpNo AND MONTH(SalDate) = MONTH(@fDate) AND YEAR(SalDate) = YEAR(@fDate);

OPEN salaryTable;
FETCH NEXT FROM salaryTable INTO
@EmpCode,@EmpName,@EmpDesigDesc,@EmpGrade,@EmpPayLabel,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
@TACodeNo3,@TAAmount3,@TACodeNo4,@TAAmount4,@TACodeNo5,@TAAmount5,
@TACodeNo6,@TAAmount6,@TACodeNo7,@TAAmount7,@TACodeNo8,@TAAmount8,
@TACodeNo9,@TAAmount9,@TACodeNo10,@TAAmount10,@TACodeNo11,@TAAmount11,
@TACodeNo12,@TAAmount12,@TACodeNo13,@TAAmount13,@TACodeNo14,@TAAmount14,
@TACodeNo15,@TAAmount15,@TACodeNo16,@TAAmount16,@TACodeNo17,@TAAmount17,@TACodeNo18,@TAAmount18,
@TACodeNo19,@TAAmount19,@TACodeNo20,@TAAmount20,@GrossTotal,
@TACodeDesc1,@TACodeDesc2,@TACodeDesc3,@TACodeDesc4,@TACodeDesc5,
@TACodeDesc6,@TACodeDesc7,@TACodeDesc8,@TACodeDesc9,@TACodeDesc10,@TACodeDesc11,@TACodeDesc12,
@TACodeDesc13,@TACodeDesc14,@TACodeDesc15,@TACodeDesc16,@TACodeDesc17,@TACodeDesc18,
@TACodeDesc19,@TACodeDesc20,
@TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
@TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
@TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,@TDCodeNo11,@TDAmount11,
@TDCodeNo12,@TDAmount12,@TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,
@TDCodeNo15,@TDAmount15,@TDCodeNo16,@TDAmount16,@TDCodeNo17,@TDAmount17,
@TDCodeNo18,@TDAmount18,@TDCodeNo19,@TDAmount19,@TDCodeNo20,@TDAmount20,@DeductTotal,@NetPayment,
@TDCodeDesc1,@TDCodeDesc2,@TDCodeDesc3,@TDCodeDesc4,@TDCodeDesc5,
@TDCodeDesc6,@TDCodeDesc7,@TDCodeDesc8,@TDCodeDesc9,@TDCodeDesc10,@TDCodeDesc11,@TDCodeDesc12,
@TDCodeDesc13,@TDCodeDesc14,@TDCodeDesc15,@TDCodeDesc16,@TDCodeDesc17,@TDCodeDesc18,
@TDCodeDesc19,@TDCodeDesc20;


WHILE @@FETCH_STATUS = 0 
	BEGIN
    
    SET @Code1 = 0;
    SET @Code2 = 0;
    SET @Code3 = 0;
    SET @Code4 = 0;
    SET @Code5 = 0;
    SET @Code6 = 0;

    SET @Amount1 = 0;
    SET @Amount2 = 0;
    SET @Amount3 = 0;
    SET @Amount4 = 0;
    SET @Amount5 = 0;
    SET @Amount6 = 0;


IF @CodeNo = 99
   BEGIN
     SET @Amount1 = (ISNULL(@GrossTotal,0));
     SET @Code1Desc = 'Gross';
     SET @Code1 = 1;
     SET @Amount2 = (ISNULL(@DeductTotal,0));
     SET @Code2Desc = 'Total Ded.';
     SET @Code2 = 2;
     SET @Amount3 = (ISNULL(@NetPayment,0));
     SET @Code3Desc = 'Net Pay';
     SET @Code3 = 3;
   END
ELSE
   BEGIN

SET @StepFlag = 1;

DECLARE replayoutTable CURSOR FOR
SELECT RepColumn,RepColumnFlag,RepColumnCode

FROM A2ZREPORTLAYOUT WHERE RepColumn = @CodeNo;

OPEN replayoutTable;
FETCH NEXT FROM replayoutTable INTO
@RepColumn,@RepColumnFlag,@RepColumnCode;


WHILE @@FETCH_STATUS = 0 
	BEGIN

 
    IF @RepColumnFlag = 1
       BEGIN
           IF @RepColumnCode = @TACodeNo1 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount1,0));
                  SET @CodeDesc = @TACodeDesc1;
                  SET @Code = @TACodeNo1;
              END
           IF @RepColumnCode = @TACodeNo2 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount2,0));
                  SET @CodeDesc = @TACodeDesc2;
                  SET @Code = @TACodeNo2;
              END
           IF @RepColumnCode = @TACodeNo3 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount3,0));
                  SET @CodeDesc = @TACodeDesc3;
                  SET @Code = @TACodeNo3;
              END
           IF @RepColumnCode = @TACodeNo4 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount4,0));
                  SET @CodeDesc = @TACodeDesc4;
                  SET @Code = @TACodeNo4;
              END
           IF @RepColumnCode = @TACodeNo5 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount5,0));
                  SET @CodeDesc = @TACodeDesc5;
                  SET @Code = @TACodeNo5;
              END
           IF @RepColumnCode = @TACodeNo6 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount6,0));
                  SET @CodeDesc = @TACodeDesc6;
                  SET @Code = @TACodeNo6;
              END
           IF @RepColumnCode = @TACodeNo7 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount7,0));
                  SET @CodeDesc = @TACodeDesc7;
                  SET @Code = @TACodeNo7;
              END
           IF @RepColumnCode = @TACodeNo8 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount8,0));
                  SET @CodeDesc = @TACodeDesc8;
                  SET @Code = @TACodeNo8;
              END
           IF @RepColumnCode = @TACodeNo9 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount9,0));
                  SET @CodeDesc = @TACodeDesc9;
                  SET @Code = @TACodeNo9;
              END
           IF @RepColumnCode = @TACodeNo10 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount10,0));
                  SET @CodeDesc = @TACodeDesc10;
                  SET @Code = @TACodeNo10;
              END    
           IF @RepColumnCode = @TACodeNo11 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount11,0));
                  SET @CodeDesc = @TACodeDesc11;
                  SET @Code = @TACodeNo11;
              END
           IF @RepColumnCode = @TACodeNo12 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount12,0));
                  SET @CodeDesc = @TACodeDesc12;
                  SET @Code = @TACodeNo12;
              END
           IF @RepColumnCode = @TACodeNo13 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount13,0));
                  SET @CodeDesc = @TACodeDesc13;
                  SET @Code = @TACodeNo13;
              END
           IF @RepColumnCode = @TACodeNo14 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount14,0));
                  SET @CodeDesc = @TACodeDesc14;
                  SET @Code = @TACodeNo14;
              END
           IF @RepColumnCode = @TACodeNo15 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount15,0));
                  SET @CodeDesc = @TACodeDesc15;
                  SET @Code = @TACodeNo15;
              END
           IF @RepColumnCode = @TACodeNo16 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount16,0));
                  SET @CodeDesc = @TACodeDesc16;
                  SET @Code = @TACodeNo16;
              END
           IF @RepColumnCode = @TACodeNo17 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount17,0));
                  SET @CodeDesc = @TACodeDesc17;
                  SET @Code = @TACodeNo17;
              END
           IF @RepColumnCode = @TACodeNo18 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount18,0));
                  SET @CodeDesc = @TACodeDesc18;
                  SET @Code = @TACodeNo18;
              END
           IF @RepColumnCode = @TACodeNo19 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount19,0));
                  SET @CodeDesc = @TACodeDesc19;
                  SET @Code = @TACodeNo19;
              END
           IF @RepColumnCode = @TACodeNo20 
              BEGIN   
                  SET @Amount = (ISNULL(@TAAmount20,0));
                  SET @CodeDesc = @TACodeDesc20;
                  SET @Code = @TACodeNo20;
              END    
       END

    IF @RepColumnFlag = 2
       BEGIN
           IF @RepColumnCode = @TDCodeNo1 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount1,0));
                 SET @CodeDesc = @TDCodeDesc1;
                 SET @Code = @TDCodeNo1;
              END
           IF @RepColumnCode = @TDCodeNo2 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount2,0));
                 SET @CodeDesc = @TDCodeDesc2;
                 SET @Code = @TDCodeNo2;
              END
           IF @RepColumnCode = @TDCodeNo3 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount3,0));
                 SET @CodeDesc = @TDCodeDesc3;
                 SET @Code = @TDCodeNo3;
              END
           IF @RepColumnCode = @TDCodeNo4 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount4,0));
                 SET @CodeDesc = @TDCodeDesc4;
                 SET @Code = @TDCodeNo4;
              END
           IF @RepColumnCode = @TDCodeNo5 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount5,0));
                 SET @CodeDesc = @TDCodeDesc5;
                 SET @Code = @TDCodeNo5;
              END
           IF @RepColumnCode = @TDCodeNo6 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount6,0));
                 SET @CodeDesc = @TDCodeDesc6;
                 SET @Code = @TDCodeNo6;
              END
           IF @RepColumnCode = @TDCodeNo7 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount7,0));
                 SET @CodeDesc = @TDCodeDesc7;
                 SET @Code = @TDCodeNo7;
              END
           IF @RepColumnCode = @TDCodeNo8 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount8,0));
                 SET @CodeDesc = @TDCodeDesc8;
                 SET @Code = @TDCodeNo8;
              END
           IF @RepColumnCode = @TDCodeNo9 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount9,0));
                 SET @CodeDesc = @TDCodeDesc9;
                 SET @Code = @TDCodeNo9;
              END
           IF @RepColumnCode = @TDCodeNo10 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount10,0));
                 SET @CodeDesc = @TDCodeDesc10;
                 SET @Code = @TDCodeNo10;
              END
           IF @RepColumnCode = @TDCodeNo11 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount11,0));
                 SET @CodeDesc = @TDCodeDesc11;
                 SET @Code = @TDCodeNo11;
              END
           IF @RepColumnCode = @TDCodeNo12 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount12,0));
                 SET @CodeDesc = @TDCodeDesc12;
                 SET @Code = @TDCodeNo12;
              END
           IF @RepColumnCode = @TDCodeNo13 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount13,0));
                 SET @CodeDesc = @TDCodeDesc13;
                 SET @Code = @TDCodeNo13;
              END
           IF @RepColumnCode = @TDCodeNo14 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount14,0));
                 SET @CodeDesc = @TDCodeDesc14;
                 SET @Code = @TDCodeNo14;
              END
           IF @RepColumnCode = @TDCodeNo15 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount15,0));
                 SET @CodeDesc = @TDCodeDesc15;
                 SET @Code = @TDCodeNo15;
              END
           IF @RepColumnCode = @TDCodeNo16 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount16,0));
                 SET @CodeDesc = @TDCodeDesc16;
                 SET @Code = @TDCodeNo16;
              END
           IF @RepColumnCode = @TDCodeNo17 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount17,0));
                 SET @CodeDesc = @TDCodeDesc17;
                 SET @Code = @TDCodeNo17;
              END
           IF @RepColumnCode = @TDCodeNo18 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount18,0));
                 SET @CodeDesc = @TDCodeDesc18;
                 SET @Code = @TDCodeNo18;
              END
           IF @RepColumnCode = @TDCodeNo19 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount19,0));
                 SET @CodeDesc = @TDCodeDesc19;
                 SET @Code = @TDCodeNo19;
              END
           IF @RepColumnCode = @TDCodeNo20 
              BEGIN   
                 SET @Amount = (ISNULL(@TDAmount20,0));
                 SET @CodeDesc = @TDCodeDesc20;
                 SET @Code = @TDCodeNo20;
              END
       END

    IF @StepFlag = 1
       BEGIN
         SET @Amount1   = @Amount;
         SET @Code1Desc = @CodeDesc;
         SET @Code1     = @Code;
       END
    
    IF @StepFlag = 2
       BEGIN
         SET @Amount2   = @Amount;
         SET @Code2Desc = @CodeDesc;
         SET @Code2     = @Code;
       END

    IF @StepFlag = 3
       BEGIN
         SET @Amount3   = @Amount;
         SET @Code3Desc = @CodeDesc;
         SET @Code3     = @Code;
       END

    IF @StepFlag = 4
       BEGIN
         SET @Amount4   = @Amount;
         SET @Code4Desc = @CodeDesc;
         SET @Code4     = @Code;
       END
   
    IF @StepFlag = 5
       BEGIN
         SET @Amount5   = @Amount;
         SET @Code5Desc = @CodeDesc;
         SET @Code5     = @Code;
       END
    
    IF @StepFlag = 6
       BEGIN
         SET @Amount6   = @Amount;
         SET @Code6Desc = @CodeDesc;
         SET @Code6     = @Code;
       END

   SET @StepFlag = (@StepFlag + 1);

   FETCH NEXT FROM replayoutTable INTO
       @RepColumn,@RepColumnFlag,@RepColumnCode;

	END
    CLOSE replayoutTable; 
    DEALLOCATE replayoutTable;
    END
   
	INSERT INTO WFSALARYLAYOUTBRACKUP
	(EmpNo,EmpName,EmpDesigDesc,EmpGrade,EmpPayLabel,Code1,Code1Desc,Amount1,Code2,Code2Desc,Amount2,Code3,Code3Desc,Amount3,Code4,Code4Desc,Amount4,Code5,Code5Desc,Amount5,Code6,Code6Desc,Amount6)  	
           VALUES (@EmpCode,@EmpName,@EmpDesigDesc,@EmpGrade,@EmpPayLabel,@Code1,@Code1Desc,@Amount1,@Code2,@Code2Desc,@Amount2,@Code3,@Code3Desc,@Amount3,@Code4,@Code4Desc,@Amount4,@Code5,@Code5Desc,@Amount5,@Code6,@Code6Desc,@Amount6);
    

	FETCH NEXT FROM salaryTable INTO
       @EmpCode,@EmpName,@EmpDesigDesc,@EmpGrade,@EmpPayLabel,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
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

FETCH NEXT FROM EmpTable INTO
       @EmpNo;

	END
    CLOSE EmpTable; 
    DEALLOCATE EmpTable;


IF @zeroflag = 1
   BEGIN
     DELETE FROM WFSALARYLAYOUTBRACKUP  WHERE Amount1 = 0 AND Amount2 = 0 AND Amount3 = 0 AND Amount4 = 0 AND Amount5 = 0 AND Amount6 = 0;
   END





END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

