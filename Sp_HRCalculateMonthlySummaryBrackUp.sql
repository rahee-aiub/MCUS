
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_HRCalculateMonthlySummaryBrackUp](@fDate SMALLDATETIME,@Mode INT,@CodeNo INT, @Area INT, @Location INT,@Project INT,@Religion INT,@Gender INT,@Status INT,@zeroflag INT)     

--ALTER PROCEDURE [dbo].[Sp_HRCalculateMonthlySummaryBrackUp]     


AS
BEGIN

--DECLARE @Mode INT;
--DECLARE @CodeNo INT;
--DECLARE @Area INT;
--DECLARE @Project INT;
--DECLARE @Religion INT;
--DECLARE @Gender INT;     





DECLARE @code int;
DECLARE @description nvarchar(50);
DECLARE @repcolumn int;

DECLARE @EmpCode int;
DECLARE @EmpNo int;
DECLARE @EmpArea int;
DECLARE @EmpAreaDesc nvarchar(50);
DECLARE @EmpName nvarchar(50);
DECLARE @EmpDesigDesc nvarchar(50);
DECLARE @EmpGradeDesc nvarchar(50);

DECLARE @WriteFlag int;
DECLARE @LastArea int;
DECLARE @AreaCode int;
DECLARE @AreaDesc nvarchar(50);

DECLARE @Code1Desc nvarchar(50);

DECLARE @NoOfEmp int;
DECLARE @TotNoOfEmp int;

DECLARE @Basic money;
DECLARE @TotBasic money;

DECLARE @Amount1 money;
DECLARE @TotAmount1 money;

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

DECLARE @Allowance1 money;
DECLARE @Allowance2 money;
DECLARE @Allowance3 money;
DECLARE @Allowance4 money;
DECLARE @Allowance5 money;

DECLARE @TotAllowance1 money;
DECLARE @TotAllowance2 money;
DECLARE @TotAllowance3 money;
DECLARE @TotAllowance4 money;
DECLARE @TotAllowance5 money;

DECLARE @Deduction1 money;
DECLARE @Deduction2 money;
DECLARE @Deduction3 money;
DECLARE @Deduction4 money;
DECLARE @Deduction5 money;

DECLARE @TotDeduction1 money;
DECLARE @TotDeduction2 money;
DECLARE @TotDeduction3 money;
DECLARE @TotDeduction4 money;
DECLARE @TotDeduction5 money;

DECLARE @TotGrossTotal money;
DECLARE @TotDeductTotal money;
DECLARE @TotNetPayment money;


--SET @Mode = 1;
--SET @CodeNo = 3;
--SET @Area = 0;
--SET @Project = 0;
--SET @Religion = 0;
--SET @Gender = 0;    


TRUNCATE TABLE WFSUMSALARYBRACKUP;

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
TACodeDesc1,TACodeDesc2,TACodeDesc3,TACodeDesc4,TACodeDesc5,
TACodeDesc6,TACodeDesc7,TACodeDesc8,TACodeDesc9,TACodeDesc10,TACodeDesc11,TACodeDesc12,
TACodeDesc13,TACodeDesc14,TACodeDesc15,TACodeDesc16,TACodeDesc17,TACodeDesc18,
TACodeDesc19,TACodeDesc20,
TDCodeNo1,TDAmount1,TDCodeNo2,TDAmount2,TDCodeNo3,TDAmount3,TDCodeNo4,TDAmount4,
TDCodeNo5,TDAmount5,TDCodeNo6,TDAmount6,TDCodeNo7,TDAmount7,TDCodeNo8,TDAmount8,
TDCodeNo9,TDAmount9,TDCodeNo10,TDAmount10,TDCodeNo11,TDAmount11,TDCodeNo12,TDAmount12,
TDCodeNo13,TDAmount13,TDCodeNo14,TDAmount14,TDCodeNo15,TDAmount15,TDCodeNo16,TDAmount16,
TDCodeNo17,TDAmount17,TDCodeNo18,TDAmount18,TDCodeNo19,TDAmount19,TDCodeNo20,TDAmount20,
DeductTotal,NetPayment,
TDCodeDesc1,TDCodeDesc2,TDCodeDesc3,TDCodeDesc4,TDCodeDesc5,
TDCodeDesc6,TDCodeDesc7,TDCodeDesc8,TDCodeDesc9,TDCodeDesc10,TDCodeDesc11,TDCodeDesc12,
TDCodeDesc13,TDCodeDesc14,TDCodeDesc15,TDCodeDesc16,TDCodeDesc17,TDCodeDesc18,
TDCodeDesc19,TDCodeDesc20
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
@TACodeDesc1,@TACodeDesc2,@TACodeDesc3,@TACodeDesc4,@TACodeDesc5,
@TACodeDesc6,@TACodeDesc7,@TACodeDesc8,@TACodeDesc9,@TACodeDesc10,@TACodeDesc11,@TACodeDesc12,
@TACodeDesc13,@TACodeDesc14,@TACodeDesc15,@TACodeDesc16,@TACodeDesc17,@TACodeDesc18,
@TACodeDesc19,@TACodeDesc20,
@TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
@TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
@TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,@TDCodeNo11,@TDAmount11,
@TDCodeNo12,@TDAmount12,@TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,@TDCodeNo15,@TDAmount15,
@TDCodeNo16,@TDAmount16,@TDCodeNo17,@TDAmount17,@TDCodeNo18,@TDAmount18,@TDCodeNo19,@TDAmount19,
@TDCodeNo20,@TDAmount20,@DeductTotal,@NetPayment,
@TDCodeDesc1,@TDCodeDesc2,@TDCodeDesc3,@TDCodeDesc4,@TDCodeDesc5,
@TDCodeDesc6,@TDCodeDesc7,@TDCodeDesc8,@TDCodeDesc9,@TDCodeDesc10,@TDCodeDesc11,@TDCodeDesc12,
@TDCodeDesc13,@TDCodeDesc14,@TDCodeDesc15,@TDCodeDesc16,@TDCodeDesc17,@TDCodeDesc18,
@TDCodeDesc19,@TDCodeDesc20;

WHILE @@FETCH_STATUS = 0 
	BEGIN
    
    SET @Amount1 = 0;
        
    IF @Mode = 1
       BEGIN
           IF @CodeNo = 97
              BEGIN   
                  SET @Amount1 = (ISNULL(@GrossTotal,0));
                  SET @Code1Desc = 'Gross';
              END           

           IF @CodeNo = @TACodeNo1 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount1,0));
                  SET @Code1Desc = @TACodeDesc1;
              END
           IF @CodeNo = @TACodeNo2 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount2,0));
                  SET @Code1Desc = @TACodeDesc2;
              END
           IF @CodeNo = @TACodeNo3 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount3,0));
                  SET @Code1Desc = @TACodeDesc3;
              END
           IF @CodeNo = @TACodeNo4 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount4,0));
                  SET @Code1Desc = @TACodeDesc4;
              END
           IF @CodeNo = @TACodeNo5 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount5,0));
                  SET @Code1Desc = @TACodeDesc5;
              END
           IF @CodeNo = @TACodeNo6 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount6,0));
                  SET @Code1Desc = @TACodeDesc6;
              END
           IF @CodeNo = @TACodeNo7 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount7,0));
                  SET @Code1Desc = @TACodeDesc7;
              END
           IF @CodeNo = @TACodeNo8 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount8,0));
                  SET @Code1Desc = @TACodeDesc8;
              END
           IF @CodeNo = @TACodeNo9 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount9,0));
                  SET @Code1Desc = @TACodeDesc9;
              END
           IF @CodeNo = @TACodeNo10 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount10,0));
                  SET @Code1Desc = @TACodeDesc10;
              END    
           IF @CodeNo = @TACodeNo11 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount11,0));
                  SET @Code1Desc = @TACodeDesc11;
              END
           IF @CodeNo = @TACodeNo12 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount12,0));
                  SET @Code1Desc = @TACodeDesc12;
              END
           IF @CodeNo = @TACodeNo13 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount13,0));
                  SET @Code1Desc = @TACodeDesc13;
              END
           IF @CodeNo = @TACodeNo14 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount14,0));
                  SET @Code1Desc = @TACodeDesc14;
              END
           IF @CodeNo = @TACodeNo15 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount15,0));
                  SET @Code1Desc = @TACodeDesc15;
              END
           IF @CodeNo = @TACodeNo16 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount16,0));
                  SET @Code1Desc = @TACodeDesc16;
              END
           IF @CodeNo = @TACodeNo17 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount17,0));
                  SET @Code1Desc = @TACodeDesc17;
              END
           IF @CodeNo = @TACodeNo18 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount18,0));
                  SET @Code1Desc = @TACodeDesc18;
              END
           IF @CodeNo = @TACodeNo19 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount19,0));
                  SET @Code1Desc = @TACodeDesc19;
              END
           IF @CodeNo = @TACodeNo20 
              BEGIN   
                  SET @Amount1 = (ISNULL(@TAAmount20,0));
                  SET @Code1Desc = @TACodeDesc20;
              END    
       END
    IF @Mode = 2
       BEGIN
           IF @CodeNo = 98
              BEGIN   
                  SET @Amount1 = (ISNULL(@DeductTotal,0));
                  SET @Code1Desc = 'Total Ded.';
              END
           IF @CodeNo = 99
              BEGIN   
                  SET @Amount1 = (ISNULL(@NetPayment,0));
                  SET @Code1Desc = 'Net Pay';
              END

           IF @CodeNo = @TDCodeNo1 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount1,0));
                 SET @Code1Desc = @TDCodeDesc1;
              END
           IF @CodeNo = @TDCodeNo2 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount2,0));
                 SET @Code1Desc = @TDCodeDesc2;
              END
           IF @CodeNo = @TDCodeNo3 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount3,0));
                 SET @Code1Desc = @TDCodeDesc3;
              END
           IF @CodeNo = @TDCodeNo4 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount4,0));
                 SET @Code1Desc = @TDCodeDesc4;
              END
           IF @CodeNo = @TDCodeNo5 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount5,0));
                 SET @Code1Desc = @TDCodeDesc5;
              END
           IF @CodeNo = @TDCodeNo6 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount6,0));
                 SET @Code1Desc = @TDCodeDesc6;
              END
           IF @CodeNo = @TDCodeNo7 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount7,0));
                 SET @Code1Desc = @TDCodeDesc7;
              END
           IF @CodeNo = @TDCodeNo8 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount8,0));
                 SET @Code1Desc = @TDCodeDesc8;
              END
           IF @CodeNo = @TDCodeNo9 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount9,0));
                 SET @Code1Desc = @TDCodeDesc9;
              END
           IF @CodeNo = @TDCodeNo10 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount10,0));
                 SET @Code1Desc = @TDCodeDesc10;
              END
           IF @CodeNo = @TDCodeNo11 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount11,0));
                 SET @Code1Desc = @TDCodeDesc11;
              END
           IF @CodeNo = @TDCodeNo12 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount12,0));
                 SET @Code1Desc = @TDCodeDesc12;
              END
           IF @CodeNo = @TDCodeNo13 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount13,0));
                 SET @Code1Desc = @TDCodeDesc13;
              END
           IF @CodeNo = @TDCodeNo14 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount14,0));
                 SET @Code1Desc = @TDCodeDesc14;
              END
           IF @CodeNo = @TDCodeNo15 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount15,0));
                 SET @Code1Desc = @TDCodeDesc15;
              END
           IF @CodeNo = @TDCodeNo16 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount16,0));
                 SET @Code1Desc = @TDCodeDesc16;
              END
           IF @CodeNo = @TDCodeNo17 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount17,0));
                 SET @Code1Desc = @TDCodeDesc17;
              END
           IF @CodeNo = @TDCodeNo18 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount18,0));
                 SET @Code1Desc = @TDCodeDesc18;
              END
           IF @CodeNo = @TDCodeNo19 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount19,0));
                 SET @Code1Desc = @TDCodeDesc19;
              END
           IF @CodeNo = @TDCodeNo20 
              BEGIN   
                 SET @Amount1 = (ISNULL(@TDAmount20,0));
                 SET @Code1Desc = @TDCodeDesc20;
              END
       END
 
   
   	
   SET @TotNoOfEmp = (SELECT NoOfEmp FROM WFSUMSALARYBRACKUP WHERE AreaCode=@EmpArea);
   SET @TotAmount1 = (SELECT Amount1 FROM WFSUMSALARYBRACKUP WHERE AreaCode=@EmpArea);
   
   
   IF @TotNoOfEmp IS NULL 
       BEGIN
          SET @TotNoOfEmp = (ISNULL(@TotNoOfEmp,0) + 1);          
          SET @TotAmount1 = (ISNULL(@TotAmount1,0) + ISNULL(@Amount1,0));
          INSERT INTO WFSUMSALARYBRACKUP
	         (AreaCode,AreaDesc,NoOfEmp,Code1,Code1Desc,Amount1)
          VALUES (@EmpArea,@EmpAreaDesc,@TotNoOfEmp,@CodeNo,@Code1Desc,@TotAmount1);
       END
    ELSE
       BEGIN
            SET @TotNoOfEmp = (ISNULL(@TotNoOfEmp,0) + 1);
            SET @TotAmount1 = (ISNULL(@TotAmount1,0) + ISNULL(@Amount1,0));
            UPDATE WFSUMSALARYBRACKUP SET NoOfEmp=@TotNoOfEmp,Amount1=@TotAmount1
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
       @TACodeDesc1,@TACodeDesc2,@TACodeDesc3,@TACodeDesc4,@TACodeDesc5,
       @TACodeDesc6,@TACodeDesc7,@TACodeDesc8,@TACodeDesc9,@TACodeDesc10,@TACodeDesc11,@TACodeDesc12,
       @TACodeDesc13,@TACodeDesc14,@TACodeDesc15,@TACodeDesc16,@TACodeDesc17,@TACodeDesc18,
       @TACodeDesc19,@TACodeDesc20,
       @TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
       @TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
       @TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,@TDCodeNo11,@TDAmount11,
       @TDCodeNo12,@TDAmount12,@TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,
       @TDCodeNo15,@TDAmount15,@TDCodeNo16,@TDAmount16,@TDCodeNo17,@TDAmount17,
       @TDCodeNo18,@TDAmount18,@TDCodeNo19,@TDAmount19,@TDCodeNo20,@TDAmount20,
       @DeductTotal,@NetPayment,
       @TDCodeDesc1,@TDCodeDesc2,@TDCodeDesc3,@TDCodeDesc4,@TDCodeDesc5,
       @TDCodeDesc6,@TDCodeDesc7,@TDCodeDesc8,@TDCodeDesc9,@TDCodeDesc10,@TDCodeDesc11,@TDCodeDesc12,
       @TDCodeDesc13,@TDCodeDesc14,@TDCodeDesc15,@TDCodeDesc16,@TDCodeDesc17,@TDCodeDesc18,
       @TDCodeDesc19,@TDCodeDesc20;

	END

CLOSE salarysumTable; 
DEALLOCATE salarysumTable;

FETCH NEXT FROM EmpTable INTO
       @EmpNo;

	END
    CLOSE EmpTable; 
    DEALLOCATE EmpTable;


IF @zeroflag = 1
   BEGIN
     DELETE FROM WFSUMSALARYBRACKUP  WHERE Amount1 = 0;
   END


END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

