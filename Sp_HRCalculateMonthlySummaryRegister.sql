
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_HRCalculateMonthlySummaryRegister](@fDate SMALLDATETIME,@Area INT, @Location INT,@Project INT,@Religion INT,@Gender INT,@Status INT)    


AS
BEGIN

DECLARE @code int;
DECLARE @description nvarchar(50);


DECLARE @EmpCode int;
DECLARE @EmpNo int;

DECLARE @EmpArea int;
DECLARE @EmpAreaDesc nvarchar(50);
DECLARE @EmpName nvarchar(50);
DECLARE @EmpDesigDesc nvarchar(50);
DECLARE @EmpGradeDesc nvarchar(50);

DECLARE @repDate smalldatetime;
DECLARE @repColumn int;
DECLARE @repColumnName nvarchar(50);
DECLARE @repColumnFlag tinyint;
DECLARE @repColumnCode int;


DECLARE @WriteFlag int;
DECLARE @LastArea int;
DECLARE @AreaCode int;
DECLARE @AreaDesc nvarchar(50);

DECLARE @Records int;
DECLARE @NoOfEmp int;
DECLARE @TotNoOfEmp int;

DECLARE @Basic money;
DECLARE @TotBasic money;

DECLARE @Consulated money;
DECLARE @TotConsulated money;

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

DECLARE @Allowance1 money;
DECLARE @Allowance2 money;
DECLARE @Allowance3 money;
DECLARE @Allowance4 money;
DECLARE @Allowance5 money;
DECLARE @Allowance6 money;
DECLARE @Allowance7 money;

DECLARE @AllowDesc1 nvarchar(50);
DECLARE @AllowDesc2 nvarchar(50);
DECLARE @AllowDesc3 nvarchar(50);
DECLARE @AllowDesc4 nvarchar(50);
DECLARE @AllowDesc5 nvarchar(50);
DECLARE @AllowDesc6 nvarchar(50);
DECLARE @AllowDesc7 nvarchar(50);

DECLARE @TotAllowance1 money;
DECLARE @TotAllowance2 money;
DECLARE @TotAllowance3 money;
DECLARE @TotAllowance4 money;
DECLARE @TotAllowance5 money;
DECLARE @TotAllowance6 money;
DECLARE @TotAllowance7 money;

DECLARE @Deduction1 money;
DECLARE @Deduction2 money;
DECLARE @Deduction3 money;
DECLARE @Deduction4 money;
DECLARE @Deduction5 money;
DECLARE @Deduction6 money;
DECLARE @Deduction7 money;


DECLARE @DedDesc1 nvarchar(50);
DECLARE @DedDesc2 nvarchar(50);
DECLARE @DedDesc3 nvarchar(50);
DECLARE @DedDesc4 nvarchar(50);
DECLARE @DedDesc5 nvarchar(50);
DECLARE @DedDesc6 nvarchar(50);
DECLARE @DedDesc7 nvarchar(50);


DECLARE @TotDeduction1 money;
DECLARE @TotDeduction2 money;
DECLARE @TotDeduction3 money;
DECLARE @TotDeduction4 money;
DECLARE @TotDeduction5 money;
DECLARE @TotDeduction6 money;
DECLARE @TotDeduction7 money;

DECLARE @TotGrossTotal money;
DECLARE @TotDeductTotal money;
DECLARE @TotNetPayment money;

DECLARE @LastRepDate smalldatetime;

TRUNCATE TABLE WFSUMSALARY;

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


DECLARE salarysumTable CURSOR FOR
SELECT EmpCode,EmpName,EmpDesigDesc,EmpGradeDesc,EmpArea,EmpAreaDesc,BasicAmount,ConsolidatedAmt,TACodeNo1,TAAmount1,TACodeNo2,TAAmount2,
TACodeNo3,TAAmount3,TACodeNo4,TAAmount4,TACodeNo5,TAAmount5,TACodeNo6,TAAmount6,TACodeNo7,TAAmount7,
TACodeNo8,TAAmount8,TACodeNo9,TAAmount9,TACodeNo10,TAAmount10,TACodeNo11,TAAmount11,
TACodeNo12,TAAmount12,TACodeNo13,TAAmount13,TACodeNo14,TAAmount14,TACodeNo15,TAAmount15,
TACodeNo16,TAAmount16,TACodeNo17,TAAmount17,TACodeNo18,TAAmount18,TACodeNo19,TAAmount19,
TACodeNo20,TAAmount20,GrossTotal,
TDCodeNo1,TDAmount1,TDCodeNo2,TDAmount2,TDCodeNo3,TDAmount3,TDCodeNo4,TDAmount4,
TDCodeNo5,TDAmount5,TDCodeNo6,TDAmount6,TDCodeNo7,TDAmount7,TDCodeNo8,TDAmount8,
TDCodeNo9,TDAmount9,TDCodeNo10,TDAmount10,TDCodeNo11,TDAmount11,TDCodeNo12,TDAmount12,
TDCodeNo13,TDAmount13,TDCodeNo14,TDAmount14,TDCodeNo15,TDAmount15,TDCodeNo16,TDAmount16,
TDCodeNo17,TDAmount17,TDCodeNo18,TDAmount18,TDCodeNo19,TDAmount19,TDCodeNo20,TDAmount20,
DeductTotal,NetPayment
FROM A2ZEMPFSALARY WHERE EmpCode = @EmpNo AND MONTH(SalDate) = MONTH(@fDate) AND YEAR(SalDate) = YEAR(@fDate);

OPEN salarysumTable;
FETCH NEXT FROM salarysumTable INTO
@EmpCode,@EmpName,@EmpDesigDesc,@EmpGradeDesc,@EmpArea,@EmpAreaDesc,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
@TACodeNo3,@TAAmount3,@TACodeNo4,@TAAmount4,@TACodeNo5,@TAAmount5,
@TACodeNo6,@TAAmount6,@TACodeNo7,@TAAmount7,@TACodeNo8,@TAAmount8,
@TACodeNo9,@TAAmount9,@TACodeNo10,@TAAmount10,@TACodeNo11,@TAAmount11,@TACodeNo12,@TAAmount12,
@TACodeNo13,@TAAmount13,@TACodeNo14,@TAAmount14,@TACodeNo15,@TAAmount15,@TACodeNo16,@TAAmount16,
@TACodeNo17,@TAAmount17,@TACodeNo18,@TAAmount18,@TACodeNo19,@TAAmount19,@TACodeNo20,@TAAmount20,
@GrossTotal,
@TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
@TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
@TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,@TDCodeNo11,@TDAmount11,
@TDCodeNo12,@TDAmount12,@TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,@TDCodeNo15,@TDAmount15,
@TDCodeNo16,@TDAmount16,@TDCodeNo17,@TDAmount17,@TDCodeNo18,@TDAmount18,@TDCodeNo19,@TDAmount19,
@TDCodeNo20,@TDAmount20,@DeductTotal,@NetPayment;


WHILE @@FETCH_STATUS = 0 
	BEGIN
    
    
    SET @Allowance1 = 0;
    SET @Allowance2 = 0;
    SET @Allowance3 = 0;
    SET @Allowance4 = 0;
    SET @Allowance5 = 0;
    SET @Allowance6 = 0;
    SET @Allowance7 = 0;

    SET @Deduction1 = 0;
    SET @Deduction2 = 0;
    SET @Deduction3 = 0;
    SET @Deduction4 = 0;
    SET @Deduction5 = 0;
    SET @Deduction6 = 0;
    SET @Deduction7 = 0;
   


    set @LastRepDate = (SELECT TOP 1 RepDate FROM A2ZREPORTLAYOUT ORDER BY Id desc); 

    
    DECLARE replayoutTable CURSOR FOR
    SELECT RepDate,RepColumn,RepColumnName,RepColumnFlag,RepColumnCode FROM A2ZREPORTLAYOUT 
                    WHERE (MONTH(RepDate) = MONTH(@fDate) AND YEAR(RepDate) = YEAR(@fDate)) or 
                       (MONTH(RepDate) = MONTH(@LastRepDate) AND YEAR(RepDate) = YEAR(@LastRepDate));
    OPEN replayoutTable;
    FETCH NEXT FROM replayoutTable INTO @repDate,@repColumn,@repColumnName,@repColumnFlag,@repColumnCode;

    WHILE @@FETCH_STATUS = 0 
	    BEGIN

    IF @repColumn = 1
       BEGIN
           SET @AllowDesc1 = @repColumnName;
           IF @repColumnCode = @TACodeNo1 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount1,0));
              END
           IF @repColumnCode = @TACodeNo2 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount2,0));
              END
           IF @repColumnCode = @TACodeNo3 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount3,0));
              END
           IF @repColumnCode = @TACodeNo4 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount4,0));
              END
           IF @repColumnCode = @TACodeNo5 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount5,0));
              END
           IF @repColumnCode = @TACodeNo6 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount6,0));
              END
           IF @repColumnCode = @TACodeNo7 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount7,0));
              END
           IF @repColumnCode = @TACodeNo8 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount8,0));
              END
           IF @repColumnCode = @TACodeNo9 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount9,0));
              END
           IF @repColumnCode = @TACodeNo10 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount10,0));
              END
           IF @repColumnCode = @TACodeNo11 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount11,0));
              END
           IF @repColumnCode = @TACodeNo12 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount12,0));
              END
           IF @repColumnCode = @TACodeNo13 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount13,0));
              END
           IF @repColumnCode = @TACodeNo14 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount14,0));
              END
           IF @repColumnCode = @TACodeNo15 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount15,0));
              END
           IF @repColumnCode = @TACodeNo16 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount16,0));
              END
           IF @repColumnCode = @TACodeNo17 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount17,0));
              END
           IF @repColumnCode = @TACodeNo18 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount18,0));
              END
           IF @repColumnCode = @TACodeNo19 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount19,0));
              END
           IF @repColumnCode = @TACodeNo20 
              BEGIN   
                 SET @Allowance1 = (@Allowance1 + ISNULL(@TAAmount20,0));
              END
       END              
        
       IF @repColumn = 2
       BEGIN
           SET @AllowDesc2 = @repColumnName;
           IF @repColumnCode = @TACodeNo1 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount1,0));
              END
           IF @repColumnCode = @TACodeNo2 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount2,0));
              END
           IF @repColumnCode = @TACodeNo3 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount3,0));
              END
           IF @repColumnCode = @TACodeNo4 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount4,0));
              END
           IF @repColumnCode = @TACodeNo5 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount5,0));
              END
           IF @repColumnCode = @TACodeNo6 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount6,0));
              END
           IF @repColumnCode = @TACodeNo7 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount7,0));
              END
           IF @repColumnCode = @TACodeNo8 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount8,0));
              END
           IF @repColumnCode = @TACodeNo9 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount9,0));
              END
           IF @repColumnCode = @TACodeNo10 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount10,0));
              END
           IF @repColumnCode = @TACodeNo11 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount11,0));
              END
           IF @repColumnCode = @TACodeNo12 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount12,0));
              END
           IF @repColumnCode = @TACodeNo13 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount13,0));
              END
           IF @repColumnCode = @TACodeNo14 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount14,0));
              END
           IF @repColumnCode = @TACodeNo15 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount15,0));
              END
           IF @repColumnCode = @TACodeNo16 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount16,0));
              END
           IF @repColumnCode = @TACodeNo17 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount17,0));
              END
           IF @repColumnCode = @TACodeNo18 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount18,0));
              END
           IF @repColumnCode = @TACodeNo19 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount19,0));
              END
           IF @repColumnCode = @TACodeNo20 
              BEGIN   
                 SET @Allowance2 = (@Allowance2 + ISNULL(@TAAmount20,0));
              END
       END                     
       
       
       IF @repColumn = 3
       BEGIN
           SET @AllowDesc3 = @repColumnName;
           IF @repColumnCode = @TACodeNo1 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount1,0));
              END
           IF @repColumnCode = @TACodeNo2 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount2,0));
              END
           IF @repColumnCode = @TACodeNo3 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount3,0));
              END
           IF @repColumnCode = @TACodeNo4 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount4,0));
              END
           IF @repColumnCode = @TACodeNo5 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount5,0));
              END
           IF @repColumnCode = @TACodeNo6 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount6,0));
              END
           IF @repColumnCode = @TACodeNo7 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount7,0));
              END
           IF @repColumnCode = @TACodeNo8 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount8,0));
              END
           IF @repColumnCode = @TACodeNo9 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount9,0));
              END
           IF @repColumnCode = @TACodeNo10 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount10,0));
              END
           IF @repColumnCode = @TACodeNo11 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount11,0));
              END
           IF @repColumnCode = @TACodeNo12 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount12,0));
              END
           IF @repColumnCode = @TACodeNo13 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount13,0));
              END
           IF @repColumnCode = @TACodeNo14 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount14,0));
              END
           IF @repColumnCode = @TACodeNo15 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount15,0));
              END
           IF @repColumnCode = @TACodeNo16 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount16,0));
              END
           IF @repColumnCode = @TACodeNo17 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount17,0));
              END
           IF @repColumnCode = @TACodeNo18 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount18,0));
              END
           IF @repColumnCode = @TACodeNo19 
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount19,0));
              END
           IF @repColumnCode = @TACodeNo20               
              BEGIN   
                 SET @Allowance3 = (@Allowance3 + ISNULL(@TAAmount20,0));
              END

       END 
           
       
       IF @repColumn = 4
       BEGIN
           SET @AllowDesc4 = @repColumnName;
           IF @repColumnCode = @TACodeNo1 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount1,0));
              END
           IF @repColumnCode = @TACodeNo2 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount2,0));
              END
           IF @repColumnCode = @TACodeNo3 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount3,0));
              END
           IF @repColumnCode = @TACodeNo4 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount4,0));
              END
           IF @repColumnCode = @TACodeNo5 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount5,0));
              END
           IF @repColumnCode = @TACodeNo6 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount6,0));
              END
           IF @repColumnCode = @TACodeNo7 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount7,0));
              END
           IF @repColumnCode = @TACodeNo8 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount8,0));
              END
           IF @repColumnCode = @TACodeNo9 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount9,0));
              END
           IF @repColumnCode = @TACodeNo10 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount10,0));
              END
           IF @repColumnCode = @TACodeNo11 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount11,0));
              END
           IF @repColumnCode = @TACodeNo12 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount12,0));
              END
           IF @repColumnCode = @TACodeNo13 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount13,0));
              END
           IF @repColumnCode = @TACodeNo14 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount14,0));
              END
           IF @repColumnCode = @TACodeNo15 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount15,0));
              END
           IF @repColumnCode = @TACodeNo16 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount16,0));
              END
           IF @repColumnCode = @TACodeNo17 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount17,0));
              END
           IF @repColumnCode = @TACodeNo18 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount18,0));
              END
           IF @repColumnCode = @TACodeNo19 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount19,0));
              END
           IF @repColumnCode = @TACodeNo20 
              BEGIN   
                 SET @Allowance4 = (@Allowance4 + ISNULL(@TAAmount20,0));
              END
       END              
       
       IF @repColumn = 5
       BEGIN
           SET @AllowDesc5 = @repColumnName;
           IF @repColumnCode = @TACodeNo1 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount1,0));
              END
           IF @repColumnCode = @TACodeNo2 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount2,0));
              END
           IF @repColumnCode = @TACodeNo3 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount3,0));
              END
           IF @repColumnCode = @TACodeNo4 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount4,0));
              END
           IF @repColumnCode = @TACodeNo5 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount5,0));
              END
           IF @repColumnCode = @TACodeNo6 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount6,0));
              END
           IF @repColumnCode = @TACodeNo7 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount7,0));
              END
           IF @repColumnCode = @TACodeNo8 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount8,0));
              END
           IF @repColumnCode = @TACodeNo9 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount9,0));
              END
           IF @repColumnCode = @TACodeNo10 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount10,0));
              END
           IF @repColumnCode = @TACodeNo11 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount11,0));
              END
           IF @repColumnCode = @TACodeNo12 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount12,0));
              END
           IF @repColumnCode = @TACodeNo13 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount13,0));
              END
           IF @repColumnCode = @TACodeNo14 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount14,0));
              END
           IF @repColumnCode = @TACodeNo15 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount15,0));
              END
           IF @repColumnCode = @TACodeNo16 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount16,0));
              END
           IF @repColumnCode = @TACodeNo17 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount17,0));
              END
           IF @repColumnCode = @TACodeNo18 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount18,0));
              END
           IF @repColumnCode = @TACodeNo19 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount19,0));
              END
           IF @repColumnCode = @TACodeNo20 
              BEGIN   
                 SET @Allowance5 = (@Allowance5 + ISNULL(@TAAmount20,0));
              END
       END
       IF @repColumn = 6
       BEGIN
           SET @AllowDesc6 = @repColumnName;
           IF @repColumnCode = @TACodeNo1 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount1,0));
              END
           IF @repColumnCode = @TACodeNo2 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount2,0));
              END
           IF @repColumnCode = @TACodeNo3 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount3,0));
              END
           IF @repColumnCode = @TACodeNo4 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount4,0));
              END
           IF @repColumnCode = @TACodeNo5 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount5,0));
              END
           IF @repColumnCode = @TACodeNo6 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount6,0));
              END
           IF @repColumnCode = @TACodeNo7 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount7,0));
              END
           IF @repColumnCode = @TACodeNo8 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount8,0));
              END
           IF @repColumnCode = @TACodeNo9 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount9,0));
              END
           IF @code = @TACodeNo10 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount10,0));
              END
           IF @repColumnCode = @TACodeNo11 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount11,0));
              END
           IF @repColumnCode = @TACodeNo12 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount12,0));
              END
           IF @repColumnCode = @TACodeNo13 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount13,0));
              END
           IF @repColumnCode = @TACodeNo14 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount14,0));
              END
           IF @repColumnCode = @TACodeNo15 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount15,0));
              END
           IF @repColumnCode = @TACodeNo16 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount16,0));
              END
           IF @repColumnCode = @TACodeNo17 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount17,0));
              END
           IF @repColumnCode = @TACodeNo18 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount18,0));
              END
           IF @repColumnCode = @TACodeNo19 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount19,0));
              END
           IF @repColumnCode = @TACodeNo20 
              BEGIN   
                 SET @Allowance6 = (@Allowance6 + ISNULL(@TAAmount20,0));
              END
       END  

       IF @repColumn = 7
       BEGIN
           SET @AllowDesc7 = @repColumnName;
           IF @repColumnCode = @TACodeNo1 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount1,0));
              END
           IF @repColumnCode = @TACodeNo2 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount2,0));
              END
           IF @repColumnCode = @TACodeNo3 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount3,0));
              END
           IF @repColumnCode = @TACodeNo4 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount4,0));
              END
           IF @repColumnCode = @TACodeNo5 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount5,0));
              END
           IF @repColumnCode = @TACodeNo6 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount6,0));
              END
           IF @repColumnCode = @TACodeNo7 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount7,0));
              END
           IF @repColumnCode = @TACodeNo8 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount8,0));
              END
           IF @repColumnCode = @TACodeNo9 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount9,0));
              END
           IF @code = @TACodeNo10 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount10,0));
              END
           IF @repColumnCode = @TACodeNo11 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount11,0));
              END
           IF @repColumnCode = @TACodeNo12 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount12,0));
              END
           IF @repColumnCode = @TACodeNo13 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount13,0));
              END
           IF @repColumnCode = @TACodeNo14 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount14,0));
              END
           IF @repColumnCode = @TACodeNo15 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount15,0));
              END
           IF @repColumnCode = @TACodeNo16 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount16,0));
              END
           IF @repColumnCode = @TACodeNo17 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount17,0));
              END
           IF @repColumnCode = @TACodeNo18 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount18,0));
              END
           IF @repColumnCode = @TACodeNo19 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount19,0));
              END
           IF @repColumnCode = @TACodeNo20 
              BEGIN   
                 SET @Allowance7 = (@Allowance7 + ISNULL(@TAAmount20,0));
              END
       END         

       IF @repColumn = 8
       BEGIN
           SET @DedDesc1 = @repColumnName;
           IF @repColumnCode = @TDCodeNo1 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount1,0));
              END
           IF @repColumnCode = @TDCodeNo2 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount2,0));
              END
           IF @repColumnCode = @TDCodeNo3 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount3,0));
              END
           IF @repColumnCode = @TDCodeNo4 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount4,0));
              END
           IF @repColumnCode = @TDCodeNo5 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount5,0));
              END
           IF @repColumnCode = @TDCodeNo6 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount6,0));
              END
           IF @repColumnCode = @TDCodeNo7 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount7,0));
              END
           IF @repColumnCode = @TDCodeNo8 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount8,0));
              END
           IF @repColumnCode = @TDCodeNo9 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount9,0));
              END
           IF @repColumnCode = @TDCodeNo10 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount10,0));
              END
           IF @repColumnCode = @TDCodeNo11 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount11,0));
              END
           IF @repColumnCode = @TDCodeNo12 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount12,0));
              END
           IF @repColumnCode = @TDCodeNo13 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount13,0));
              END
           IF @repColumnCode = @TDCodeNo14 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount14,0));
              END
           IF @repColumnCode = @TDCodeNo15 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount15,0));
              END
           IF @repColumnCode = @TDCodeNo16 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount16,0));
              END
           IF @repColumnCode = @TDCodeNo17 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount17,0));
              END
           IF @repColumnCode = @TDCodeNo18 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount18,0));
              END
           IF @repColumnCode = @TDCodeNo19 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount19,0));
              END
           IF @repColumnCode = @TDCodeNo20 
              BEGIN   
                 SET @Deduction1 = (@Deduction1 + ISNULL(@TDAmount20,0));
              END
       END      
       
       IF @repColumn = 9
       BEGIN
           SET @DedDesc2 = @repColumnName;
           IF @repColumnCode = @TDCodeNo1 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount1,0));
              END
           IF @repColumnCode = @TDCodeNo2 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount2,0));
              END
           IF @repColumnCode = @TDCodeNo3 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount3,0));
              END
           IF @repColumnCode = @TDCodeNo4 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount4,0));
              END
           IF @repColumnCode = @TDCodeNo5 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount5,0));
              END
           IF @repColumnCode = @TDCodeNo6 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount6,0));
              END
           IF @repColumnCode = @TDCodeNo7 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount7,0));
              END
           IF @repColumnCode = @TDCodeNo8 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount8,0));
              END
           IF @repColumnCode = @TDCodeNo9 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount9,0));
              END
           IF @repColumnCode = @TDCodeNo10 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount10,0));
              END
           IF @repColumnCode = @TDCodeNo11 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount11,0));
              END
           IF @repColumnCode = @TDCodeNo12 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount12,0));
              END
           IF @repColumnCode = @TDCodeNo13 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount13,0));
              END
           IF @repColumnCode = @TDCodeNo14 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount14,0));
              END
           IF @repColumnCode = @TDCodeNo15 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount15,0));
              END
           IF @repColumnCode = @TDCodeNo16 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount16,0));
              END
           IF @repColumnCode = @TDCodeNo17 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount17,0));
              END
           IF @repColumnCode = @TDCodeNo18 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount18,0));
              END
           IF @repColumnCode = @TDCodeNo19 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount19,0));
              END
           IF @repColumnCode = @TDCodeNo20 
              BEGIN   
                 SET @Deduction2 = (@Deduction2 + ISNULL(@TDAmount20,0));
              END
       END 
       
       IF @repColumn = 10
       BEGIN
           SET @DedDesc3 = @repColumnName;
           IF @repColumnCode = @TDCodeNo1 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount1,0));
              END
           IF @repColumnCode = @TDCodeNo2 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount2,0));
              END
           IF @repColumnCode = @TDCodeNo3 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount3,0));
              END
           IF @repColumnCode = @TDCodeNo4 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount4,0));
              END
           IF @repColumnCode = @TDCodeNo5 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount5,0));
              END
           IF @repColumnCode = @TDCodeNo6 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount6,0));
              END
           IF @repColumnCode = @TDCodeNo7 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount7,0));
              END
           IF @repColumnCode = @TDCodeNo8 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount8,0));
              END
           IF @repColumnCode = @TDCodeNo9 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount9,0));
              END
           IF @repColumnCode = @TDCodeNo10 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount10,0));
              END
           IF @repColumnCode = @TDCodeNo11 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount11,0));
              END
           IF @repColumnCode = @TDCodeNo12 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount12,0));
              END
           IF @repColumnCode = @TDCodeNo13 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount13,0));
              END
           IF @repColumnCode = @TDCodeNo14 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount14,0));
              END
           IF @repColumnCode = @TDCodeNo15 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount15,0));
              END
           IF @repColumnCode = @TDCodeNo16 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount16,0));
              END
           IF @repColumnCode = @TDCodeNo17 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount17,0));
              END
           IF @repColumnCode = @TDCodeNo18 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount18,0));
              END
           IF @repColumnCode = @TDCodeNo19 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount19,0));
              END
           IF @repColumnCode = @TDCodeNo20 
              BEGIN   
                 SET @Deduction3 = (@Deduction3 + ISNULL(@TDAmount20,0));
              END
       END 

       IF @repColumn = 11
       BEGIN
           SET @DedDesc4 = @repColumnName;
           IF @repColumnCode = @TDCodeNo1 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount1,0));
              END
           IF @repColumnCode = @TDCodeNo2 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount2,0));
              END
           IF @repColumnCode = @TDCodeNo3 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount3,0));
              END
           IF @repColumnCode = @TDCodeNo4 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount4,0));
              END
           IF @repColumnCode = @TDCodeNo5 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount5,0));
              END
           IF @repColumnCode = @TDCodeNo6 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount6,0));
              END
           IF @repColumnCode = @TDCodeNo7 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount7,0));
              END
           IF @repColumnCode = @TDCodeNo8 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount8,0));
              END
           IF @repColumnCode = @TDCodeNo9 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount9,0));
              END
           IF @repColumnCode = @TDCodeNo10 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount10,0));
              END
           IF @repColumnCode = @TDCodeNo11 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount11,0));
              END
           IF @repColumnCode = @TDCodeNo12 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount12,0));
              END
           IF @repColumnCode = @TDCodeNo13 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount13,0));
              END
           IF @repColumnCode = @TDCodeNo14 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount14,0));
              END
           IF @repColumnCode = @TDCodeNo15 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount15,0));
              END
           IF @repColumnCode = @TDCodeNo16 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount16,0));
              END
           IF @repColumnCode = @TDCodeNo17 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount17,0));
              END
           IF @repColumnCode = @TDCodeNo18 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount18,0));
              END
           IF @repColumnCode = @TDCodeNo19 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount19,0));
              END
           IF @repColumnCode = @TDCodeNo20 
              BEGIN   
                 SET @Deduction4 = (@Deduction4 + ISNULL(@TDAmount20,0));
              END
       END              
       
       IF @repColumn = 12
       BEGIN
           SET @DedDesc5 = @repColumnName;
           IF @repColumnCode = @TDCodeNo1 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount1,0));
              END
           IF @repColumnCode = @TDCodeNo2 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount2,0));
              END
           IF @repColumnCode = @TDCodeNo3 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount3,0));
              END
           IF @repColumnCode = @TDCodeNo4 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount4,0));
              END
           IF @repColumnCode = @TDCodeNo5 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount5,0));
              END
           IF @repColumnCode = @TDCodeNo6 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount6,0));
              END
           IF @repColumnCode = @TDCodeNo7 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount7,0));
              END
           IF @repColumnCode = @TDCodeNo8 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount8,0));
              END
           IF @repColumnCode = @TDCodeNo9 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount9,0));
              END
           IF @repColumnCode = @TDCodeNo10 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount10,0));
              END
           IF @repColumnCode = @TDCodeNo11 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount11,0));
              END
           IF @repColumnCode = @TDCodeNo12 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount12,0));
              END
           IF @repColumnCode = @TDCodeNo13 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount13,0));
              END
           IF @repColumnCode = @TDCodeNo14 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount14,0));
              END
           IF @repColumnCode = @TDCodeNo15 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount15,0));
              END
           IF @repColumnCode = @TDCodeNo16 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount16,0));
              END
           IF @repColumnCode = @TDCodeNo17 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount17,0));
              END
           IF @repColumnCode = @TDCodeNo18 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount18,0));
              END
           IF @repColumnCode = @TDCodeNo19 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount19,0));
              END
           IF @repColumnCode = @TDCodeNo20 
              BEGIN   
                 SET @Deduction5 = (@Deduction5 + ISNULL(@TDAmount20,0));
              END
       END                  
       
       IF @repColumn = 13
       BEGIN
           SET @DedDesc6 = @repColumnName;
           IF @repColumnCode = @TDCodeNo1 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount1,0));
              END
           IF @repColumnCode = @TDCodeNo2 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount2,0));
              END
           IF @repColumnCode = @TDCodeNo3 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount3,0));
              END
           IF @repColumnCode = @TDCodeNo4 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount4,0));
              END
           IF @repColumnCode = @TDCodeNo5 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount5,0));
              END
           IF @repColumnCode = @TDCodeNo6 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount6,0));
              END
           IF @repColumnCode = @TDCodeNo7 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount7,0));
              END
           IF @repColumnCode = @TDCodeNo8 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount8,0));
              END
           IF @repColumnCode = @TDCodeNo9 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount9,0));
              END
           IF @repColumnCode = @TDCodeNo10 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount10,0));
              END
           IF @repColumnCode = @TDCodeNo11 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount11,0));
              END
           IF @repColumnCode = @TDCodeNo12 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount12,0));
              END
           IF @repColumnCode = @TDCodeNo13 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount13,0));
              END
           IF @repColumnCode = @TDCodeNo14 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount14,0));
              END
           IF @repColumnCode = @TDCodeNo15 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount15,0));
              END
           IF @repColumnCode = @TDCodeNo16 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount16,0));
              END
           IF @repColumnCode = @TDCodeNo17 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount17,0));
              END
           IF @repColumnCode = @TDCodeNo18 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount18,0));
              END
           IF @repColumnCode = @TDCodeNo19 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount19,0));
              END
           IF @repColumnCode = @TDCodeNo20 
              BEGIN   
                 SET @Deduction6 = (@Deduction6 + ISNULL(@TDAmount20,0));
              END
       END                  

      IF @repColumn = 14
       BEGIN
           SET @DedDesc7 = @repColumnName;
           IF @repColumnCode = @TDCodeNo1 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount1,0));
              END
           IF @repColumnCode = @TDCodeNo2 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount2,0));
              END
           IF @repColumnCode = @TDCodeNo3 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount3,0));
              END
           IF @repColumnCode = @TDCodeNo4 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount4,0));
              END
           IF @repColumnCode = @TDCodeNo5 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount5,0));
              END
           IF @repColumnCode = @TDCodeNo6 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount6,0));
              END
           IF @repColumnCode = @TDCodeNo7 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount7,0));
              END
           IF @repColumnCode = @TDCodeNo8 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount8,0));
              END
           IF @repColumnCode = @TDCodeNo9 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount9,0));
              END
           IF @repColumnCode = @TDCodeNo10 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount10,0));
              END
           IF @repColumnCode = @TDCodeNo11 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount11,0));
              END
           IF @repColumnCode = @TDCodeNo12 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount12,0));
              END
           IF @repColumnCode = @TDCodeNo13 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount13,0));
              END
           IF @repColumnCode = @TDCodeNo14 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount14,0));
              END
           IF @repColumnCode = @TDCodeNo15 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount15,0));
              END
           IF @repColumnCode = @TDCodeNo16 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount16,0));
              END
           IF @repColumnCode = @TDCodeNo17 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount17,0));
              END
           IF @repColumnCode = @TDCodeNo18 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount18,0));
              END
           IF @repColumnCode = @TDCodeNo19 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount19,0));
              END
           IF @repColumnCode = @TDCodeNo20 
              BEGIN   
                 SET @Deduction7 = (@Deduction7 + ISNULL(@TDAmount20,0));
              END
       END                  

       

     FETCH NEXT FROM replayoutTable INTO @repDate,@repColumn,@repColumnName,@repColumnFlag,@repColumnCode;
                  END
                  CLOSE replayoutTable; 
                  DEALLOCATE replayoutTable;

    

    SET @TotNoOfEmp = (SELECT NoOfEmp FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @Records = (SELECT NoOfEmp FROM WFSUMSALARY WHERE AreaCode=@EmpArea);

    SET @TotBasic = (SELECT Basic FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotConsulated = (SELECT Consulated FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotAllowance1 = (SELECT Allowance1 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotAllowance2 = (SELECT Allowance2 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotAllowance3 = (SELECT Allowance3 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotAllowance4 = (SELECT Allowance4 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotAllowance5 = (SELECT Allowance5 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotAllowance6 = (SELECT Allowance6 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);  
    SET @TotAllowance7 = (SELECT Allowance7 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);     

    SET @TotDeduction1 = (SELECT Deduction1 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotDeduction2 = (SELECT Deduction2 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotDeduction3 = (SELECT Deduction3 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotDeduction4 = (SELECT Deduction4 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotDeduction5 = (SELECT Deduction5 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotDeduction6 = (SELECT Deduction6 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    SET @TotDeduction7 = (SELECT Deduction7 FROM WFSUMSALARY WHERE AreaCode=@EmpArea);
    
    SET @TotGrossTotal = (SELECT Gross FROM WFSUMSALARY WHERE AreaCode=@EmpArea);   
    SET @TotDeductTotal = (SELECT TotalDeduction FROM WFSUMSALARY WHERE AreaCode=@EmpArea);  
    SET @TotNetPayment = (SELECT NetPay FROM WFSUMSALARY WHERE AreaCode=@EmpArea);   


    SET @TotNoOfEmp = (ISNULL(@TotNoOfEmp,0) + 1);
    SET @TotBasic = (ISNULL(@TotBasic,0) + ISNULL(@Basic,0));
    SET @TotConsulated = (ISNULL(@TotConsulated,0) + ISNULL(@Consulated,0));
    SET @TotAllowance1 = (ISNULL(@TotAllowance1,0) + ISNULL(@Allowance1,0));
    SET @TotAllowance2 = (ISNULL(@TotAllowance2,0) + ISNULL(@Allowance2,0));
    SET @TotAllowance3 = (ISNULL(@TotAllowance3,0) + ISNULL(@Allowance3,0));
    SET @TotAllowance4 = (ISNULL(@TotAllowance4,0) + ISNULL(@Allowance4,0));
    SET @TotAllowance5 = (ISNULL(@TotAllowance5,0) + ISNULL(@Allowance5,0));
    SET @TotAllowance6 = (ISNULL(@TotAllowance6,0) + ISNULL(@Allowance6,0));
    SET @TotAllowance7 = (ISNULL(@TotAllowance7,0) + ISNULL(@Allowance7,0));

    SET @TotDeduction1 = (ISNULL(@TotDeduction1,0) + ISNULL(@Deduction1,0));
    SET @TotDeduction2 = (ISNULL(@TotDeduction2,0) + ISNULL(@Deduction2,0));
    SET @TotDeduction3 = (ISNULL(@TotDeduction3,0) + ISNULL(@Deduction3,0));
    SET @TotDeduction4 = (ISNULL(@TotDeduction4,0) + ISNULL(@Deduction4,0));
    SET @TotDeduction5 = (ISNULL(@TotDeduction5,0) + ISNULL(@Deduction5,0));
    SET @TotDeduction6 = (ISNULL(@TotDeduction6,0) + ISNULL(@Deduction6,0));
    SET @TotDeduction7 = (ISNULL(@TotDeduction7,0) + ISNULL(@Deduction7,0));
    

    SET @TotGrossTotal = (ISNULL(@TotGrossTotal,0) + ISNULL(@GrossTotal,0));
    SET @TotDeductTotal = (ISNULL(@TotDeductTotal,0) + ISNULL(@DeductTotal,0));
    SET @TotNetPayment = (ISNULL(@TotNetPayment,0) + ISNULL(@NetPayment,0));



    IF @Records IS NULL
       BEGIN
          INSERT INTO WFSUMSALARY
	         (AreaCode,AreaDesc,NoOfEmp,Basic,Consulated,AllowDesc1,Allowance1,AllowDesc2,Allowance2,AllowDesc3,Allowance3,AllowDesc4,Allowance4,
              AllowDesc5,Allowance5,AllowDesc6,Allowance6,AllowDesc7,Allowance7,Gross,DedDesc1,Deduction1,DedDesc2,Deduction2,DedDesc3,Deduction3,DedDesc4,Deduction4,DedDesc5,Deduction5,
              DedDesc6,Deduction6,DedDesc7,Deduction7,TotalDeduction,NetPay)
	
          VALUES (@EmpArea,@EmpAreaDesc,@TotNoOfEmp,@TotBasic,@TotConsulated,@AllowDesc1,@TotAllowance1,@AllowDesc2,@TotAllowance2,
               @AllowDesc3,@TotAllowance3,@AllowDesc4,@TotAllowance4,@AllowDesc5,@TotAllowance5,@AllowDesc6,@TotAllowance6,@AllowDesc7,@TotAllowance7,@TotGrossTotal,@DedDesc1,@TotDeduction1,
               @DedDesc2,@TotDeduction2,@DedDesc3,@TotDeduction3,@DedDesc4,@TotDeduction4,@DedDesc5,@TotDeduction5,@DedDesc6,@TotDeduction6,@DedDesc7,@TotDeduction7,
               @TotDeductTotal,@TotNetPayment);
       END
    ELSE
       BEGIN
            UPDATE WFSUMSALARY SET NoOfEmp=@TotNoOfEmp,Basic=@TotBasic,Consulated=@TotConsulated,Allowance1=@TotAllowance1,
                   Allowance2=@TotAllowance2,Allowance3=@TotAllowance3,Allowance4=@TotAllowance4,
                   Allowance5=@TotAllowance5,Allowance6=@TotAllowance6,Allowance7=@TotAllowance7,Gross=@TotGrossTotal,
                   Deduction1=@TotDeduction1,Deduction2=@TotDeduction2,Deduction3=@TotDeduction3,
                   Deduction4=@TotDeduction4,Deduction5=@TotDeduction5,Deduction6=@TotDeduction6,
                   Deduction7=@TotDeduction7,TotalDeduction=@TotDeductTotal,
                   NetPay=@TotNetPayment
                   WHERE AreaCode=@EmpArea; 	
       END


   FETCH NEXT FROM salarysumTable INTO
       @EmpCode,@EmpName,@EmpDesigDesc,@EmpGradeDesc,@EmpArea,@EmpAreaDesc,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
       @TACodeNo3,@TAAmount3,@TACodeNo4,@TAAmount4,@TACodeNo5,@TAAmount5,
       @TACodeNo6,@TAAmount6,@TACodeNo7,@TAAmount7,@TACodeNo8,@TAAmount8,
       @TACodeNo9,@TAAmount9,@TACodeNo10,@TAAmount10,@TACodeNo11,@TAAmount11,
       @TACodeNo12,@TAAmount12,@TACodeNo13,@TAAmount13,@TACodeNo14,@TAAmount14,
       @TACodeNo15,@TAAmount15,@TACodeNo16,@TAAmount16,@TACodeNo17,@TAAmount17,
       @TACodeNo18,@TAAmount18,@TACodeNo19,@TAAmount19,@TACodeNo20,@TAAmount20,@GrossTotal,
       @TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
       @TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
       @TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,@TDCodeNo11,@TDAmount11,
       @TDCodeNo12,@TDAmount12,@TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,
       @TDCodeNo15,@TDAmount15,@TDCodeNo16,@TDAmount16,@TDCodeNo17,@TDAmount17,
       @TDCodeNo18,@TDAmount18,@TDCodeNo19,@TDAmount19,@TDCodeNo20,@TDAmount20,
       @DeductTotal,@NetPayment;

	END

    CLOSE salarysumTable; 
    DEALLOCATE salarysumTable;

FETCH NEXT FROM EmpTable INTO
       @EmpNo;

	END
    CLOSE EmpTable; 
    DEALLOCATE EmpTable;

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

