
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_HRGenerateAllowance](@SalDate smalldatetime, @NoDays INT)  

---ALTER PROCEDURE [dbo].[Sp_HRGenerateAllowance]  
AS
BEGIN


/*

EXECUTE Sp_HRGenerateAllowance '2017-04-30',30

*/


DECLARE @EmpCode int;
DECLARE @EmpName nvarchar(50);
DECLARE @EmpBaseGrade tinyint;
DECLARE @EmpGrade int;

DECLARE @EmpDesignation tinyint;
DECLARE @EmpDesigDesc nvarchar(50);
DECLARE @EmpPayLabel int;

DECLARE @EmpServiceType tinyint;
DECLARE @EmpArea int;
DECLARE @EmpRelagion tinyint;



DECLARE @code int;
DECLARE @description nvarchar(50);

DECLARE @dependsOn int;

DECLARE @CodeStatus bit;
DECLARE @location bit;
DECLARE @servtype bit;
DECLARE @religion bit;
DECLARE @percentage bit;

DECLARE @perc money;
DECLARE @amount money;
DECLARE @Tamount money;
DECLARE @BasicAmt money;
DECLARE @ConsolidatedAmt money;
DECLARE @RestLabel int;

DECLARE @Consulated money;
DECLARE @Basic money;

DECLARE @TotalDays INT;

DECLARE @DeductTotal money;
DECLARE @NetPayment money;

DECLARE @DayAmount money;


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


DECLARE @StartBasic money;
DECLARE @Bar1 money;
DECLARE @Label1 int;
DECLARE @End1Basic money;
DECLARE @Bar2 money;
DECLARE @Label2 int;
DECLARE @End2Basic money;
DECLARE @Consolidated money;

DECLARE @EmpConsolidatedAmt money;

--BEGIN TRY
--	BEGIN TRANSACTION
--		SET NOCOUNT ON


DECLARE salaryTable CURSOR FOR
SELECT EmpCode,EmpName,EmpArea,EmpRelagion,EmpServiceType,EmpDesignation,EmpDesigDesc,EmpGrade,EmpBaseGrade,
EmpPayLabel,TotalDays,BasicAmount,ConsolidatedAmt,TACodeNo1,TAAmount1,TACodeNo2,TAAmount2,
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
FROM A2ZEMPFSALARY WHERE SalDate = @SalDate; 

OPEN salaryTable;
FETCH NEXT FROM salaryTable INTO
@EmpCode,@EmpName,@EmpArea,@EmpRelagion,@EmpServiceType,@EmpDesignation,@EmpDesigDesc,@EmpGrade,@EmpBaseGrade,
@EmpPayLabel,@TotalDays,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
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


---------------- Generate Basic & Concolidated Amount -----------------
         SET @BasicAmt = 0;
         SET @ConsolidatedAmt = 0;
         
         SET @StartBasic= (SELECT StartBasic FROM A2ZPAYSCALE WHERE Base = @EmpBaseGrade AND Payscale = @EmpGrade);
         SET @Bar1= (SELECT Bar1 FROM A2ZPAYSCALE WHERE Base = @EmpBaseGrade AND Payscale = @EmpGrade);
         SET @Label1= (SELECT label1 FROM A2ZPAYSCALE WHERE Base = @EmpBaseGrade AND Payscale = @EmpGrade); 
         SET @End1Basic= (SELECT End1Basic FROM A2ZPAYSCALE WHERE Base = @EmpBaseGrade AND Payscale = @EmpGrade); 
         SET @Bar2= (SELECT Bar2 FROM A2ZPAYSCALE WHERE Base = @EmpBaseGrade AND Payscale = @EmpGrade); 
         SET @Label2= (SELECT label2 FROM A2ZPAYSCALE WHERE Base = @EmpBaseGrade AND Payscale = @EmpGrade); 
         SET @End2Basic= (SELECT End2Basic FROM A2ZPAYSCALE WHERE Base = @EmpBaseGrade AND Payscale = @EmpGrade); 
         SET @Consolidated= (SELECT Consolidated FROM A2ZPAYSCALE WHERE Base = @EmpBaseGrade AND Payscale = @EmpGrade); 
         
         SET @EmpPayLabel = (@EmpPayLabel - 1);
         SET @Label1 = (@Label1 - 1);


         IF @EmpBaseGrade = 3
            BEGIN
               SET @ConsolidatedAmt = @Consulated
            END
         ELSE
         IF @EmpPayLabel > @Label1
            BEGIN
               SET @RestLabel = (@EmpPayLabel - @Label1)
               SET @BasicAmt = (@End1Basic + (@Bar2 * @RestLabel))
            END 
         ELSE  
            BEGIN
               SET @BasicAmt = (@StartBasic + (@Bar1 * @EmpPayLabel))
            END

          
---------------------------------------------------------------------------

          SET @Tamount = (SELECT Amount FROM A2ZEMPTSALARY WHERE EmpCode = @EmpCode AND CodeHead=1 AND CodeNo=98 AND StatusT=1 AND SalDate=@SalDate);  
          IF @Tamount IS NOT NULL
              BEGIN
                  SET @BasicAmt = @Tamount;
              END

          SET @Tamount = (SELECT Amount FROM A2ZEMPTSALARY WHERE EmpCode = @EmpCode AND CodeHead=1 AND CodeNo=98 AND StatusR=1 AND SalDate=@SalDate);  
          IF @Tamount IS NOT NULL
              BEGIN
                  SET @BasicAmt = @Tamount;
              END

          
          SET @Tamount = (SELECT Amount FROM A2ZEMPTSALARY WHERE EmpCode = @EmpCode AND CodeHead=1 AND CodeNo=99 AND StatusT=1 AND SalDate=@SalDate);  
          IF @Tamount IS NOT NULL
              BEGIN
                  SET @ConsolidatedAmt = @Tamount;
              END

          SET @Tamount = (SELECT Amount FROM A2ZEMPTSALARY WHERE EmpCode = @EmpCode AND CodeHead=1 AND CodeNo=99 AND StatusR=1 AND SalDate=@SalDate);  
          IF @Tamount IS NOT NULL
              BEGIN
                  SET @ConsolidatedAmt = @Tamount;
              END

----------------------------------------------------------------------------
         IF @ConsolidatedAmt > 0
            BEGIN
                SET @DayAmount = (@ConsolidatedAmt / @NoDays);
                SET @ConsolidatedAmt = round((@DayAmount * @TotalDays),0);
            END
         
         IF @BasicAmt > 0
            BEGIN
                SET @DayAmount = (@BasicAmt / @NoDays);
                SET @BasicAmt = round((@DayAmount * @TotalDays),0);
            END         

         UPDATE A2ZEMPFSALARY SET BasicAmount=@BasicAmt,ConsolidatedAmt=@ConsolidatedAmt 
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
---------------- Generatye Allowances Amount -------------------
         
         SET @GrossTotal = 0;


         
         DECLARE allowTable CURSOR FOR
         SELECT Code,Description FROM A2ZEMPWISEALLOWCODE WHERE EmpCode=@EmpCode AND Status = 'True';
         OPEN allowTable;
         FETCH NEXT FROM allowTable INTO @code,@description;

         WHILE @@FETCH_STATUS = 0 
	     BEGIN
--             SET @TACodeNo1= (SELECT TACodeNo1 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo2= (SELECT TACodeNo2 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo3= (SELECT TACodeNo3 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo4= (SELECT TACodeNo4 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo5= (SELECT TACodeNo5 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo6= (SELECT TACodeNo6 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo7= (SELECT TACodeNo7 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo8= (SELECT TACodeNo8 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo9= (SELECT TACodeNo9 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo10= (SELECT TACodeNo10 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo11= (SELECT TACodeNo11 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo12= (SELECT TACodeNo12 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo13= (SELECT TACodeNo13 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo14= (SELECT TACodeNo14 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo15= (SELECT TACodeNo15 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo16= (SELECT TACodeNo16 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo17= (SELECT TACodeNo17 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo18= (SELECT TACodeNo18 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo19= (SELECT TACodeNo19 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--             SET @TACodeNo20= (SELECT TACodeNo20 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
   
             SET @dependsOn= (SELECT DependsOn FROM A2ZALLOWCTRL WHERE AllowCode = @code);  
             SET @location= (SELECT Location FROM A2ZALLOWCTRL WHERE AllowCode = @code);  
             SET @servtype= (SELECT ServType FROM A2ZALLOWCTRL WHERE AllowCode = @code);  
             SET @religion= (SELECT Religion FROM A2ZALLOWCTRL WHERE AllowCode = @code);  
             SET @percentage= (SELECT Percentage FROM A2ZALLOWCTRL WHERE AllowCode = @code);  

             SET @amount = 0;
                               
             IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                BEGIN
                     SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                         AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                            ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                     SET @amount = round(((@BasicAmt * @perc)/100),0);
                END

             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                        BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END


             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);   
                          SET @amount = round(((@BasicAmt * @perc)/100),0);     
                     END
                  
             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);        
                     END

             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);           
                     END

             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea);        
                     END


             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);        
                     END

             ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True'  AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);        
                     END

              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                               
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade);        
                     END

              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND 
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND 
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END


              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND ReligionCode=@EmpRelagion); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False'  AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND ReligionCode=@EmpRelagion);        
                     END

                  
              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND 
                              ServTypeCode=@EmpServiceType);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);            
                     END
                   
              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND 
                              ServTypeCode=@EmpServiceType);        
                     END
---------------------------------------------
              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);          
                     END
                  
              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END
               
              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea);   
                          SET @amount = round(((@BasicAmt * @perc)/100),0);      
                     END

              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);   
                          SET @amount = round(((@BasicAmt * @perc)/100),0);          
                     END
                 
              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                                
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation);
                          SET @amount = round(((@BasicAmt * @perc)/100),0);          
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                                
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND 
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);       
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                               
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND 
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                                 
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);           
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                                
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND ReligionCode=@EmpRelagion);        
                     END
                  
              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND 
                              ServTypeCode=@EmpServiceType);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);             
                     END
              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND DesignationCode=@EmpDesignation AND 
                              ServTypeCode=@EmpServiceType);        
                     END

-----------------------------------------------------------------
              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);   
                          SET @amount = round(((@BasicAmt * @perc)/100),0);     
                     END
                  
              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND LocationCode=@EmpArea); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);           
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND LocationCode=@EmpArea);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                               
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);        
                     END

               ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                               
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND  
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND  
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND ReligionCode=@EmpRelagion); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                               
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND ReligionCode=@EmpRelagion);        
                     END
            
              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND  
                              ServTypeCode=@EmpServiceType);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);            
                     END
                   
              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@BasicAmt > 0 OR @ConsolidatedAmt > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZALLOWCTRLDTL WHERE 
                              AllowCode=@code AND  
                              ServTypeCode=@EmpServiceType);        
                     END

-----------------------------------------------------------------

---               ELSE
--                  IF @dependsOn = 3
--                     BEGIN
--                         SET @percentage= (SELECT Amount FROM A2ZALLOWANCECONTROL WHERE AllowCode=@code AND DependsOn=@dependsOn AND DepCode=1); 
--                         SET @amount = ((@Basic * @percentage)/100);
--                     END
--                  ELSE
--                  IF @dependsOn = 4
--                     BEGIN
--                         SET @amount= (SELECT Amount FROM A2ZALLOWANCECONTROL WHERE AllowCode=@code AND DependsOn=@dependsOn AND DepCode=1); 
--                     END
--                  ELSE
--                  IF @dependsOn = 5
--                     BEGIN
--                         SET @amount= (SELECT Amount FROM A2ZALLOWANCECONTROL WHERE AllowCode=@code AND DependsOn=@dependsOn AND DepCode=1); 
--                     END

----- Update A2ZEMPTSALARY ------------ 

                  SET @CodeStatus= (SELECT Status FROM A2ZALLOWANCE WHERE Code = @code);  

                  IF @EmpBaseGrade = 3 or @CodeStatus = 'False'
                     BEGIN
                         SET @amount = 0;
                     END       

------------------------------------------------------------------------------------------------

                  SET @Tamount= (SELECT Amount FROM A2ZEMPTSALARY WHERE EmpCode = @EmpCode AND CodeHead=1 AND CodeNo=@code AND StatusT=1 AND SalDate=@SalDate);                   
                  IF @Tamount IS NOT NULL
                     BEGIN
                         SET @amount = @Tamount;
                     END

                  SET @Tamount= (SELECT Amount FROM A2ZEMPTSALARY WHERE EmpCode = @EmpCode AND CodeHead=1 AND CodeNo=@code AND StatusR=1 AND SalDate=@SalDate);                   
                  IF @Tamount IS NOT NULL
                     BEGIN
                         SET @amount = @Tamount;
                     END


------------------------------------------------------------------------------------------------

                  IF @TACodeNo1 = 0
                     BEGIN
                         SET @TACodeNo1=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo1=@code,TACodeDesc1=@description,TAAmount1=ISNULL(@amount,0),TAStatus1='True' 
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo2 = 0
                     BEGIN
                         SET @TACodeNo2=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo2=@code,TACodeDesc2=@description,TAAmount2=ISNULL(@amount,0),TAStatus2='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo3 = 0
                     BEGIN
                         SET @TACodeNo3=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo3=@code,TACodeDesc3=@description,TAAmount3=ISNULL(@amount,0),TAStatus3='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo4 = 0
                     BEGIN
                         SET @TACodeNo4=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo4=@code,TACodeDesc4=@description,TAAmount4=ISNULL(@amount,0),TAStatus4='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo5 = 0
                     BEGIN
                         SET @TACodeNo5=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo5=@code,TACodeDesc5=@description,TAAmount5=ISNULL(@amount,0),TAStatus5='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo6 = 0
                     BEGIN
                         SET @TACodeNo6=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo6=@code,TACodeDesc6=@description,TAAmount6=ISNULL(@amount,0),TAStatus6='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate;  
                     END
                  ELSE 
                  IF @TACodeNo7 = 0
                     BEGIN
                         SET @TACodeNo7=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo7=@code,TACodeDesc7=@description,TAAmount7=ISNULL(@amount,0),TAStatus7='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo8 = 0
                     BEGIN
                         SET @TACodeNo8=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo8=@code,TACodeDesc8=@description,TAAmount8=ISNULL(@amount,0),TAStatus8='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo9 = 0
                     BEGIN
                         SET @TACodeNo9=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo9=@code,TACodeDesc9=@description,TAAmount9=ISNULL(@amount,0),TAStatus9='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo10 = 0
                     BEGIN
                         SET @TACodeNo10=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo10=@code,TACodeDesc10=@description,TAAmount10=ISNULL(@amount,0),TAStatus10='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo11 = 0
                     BEGIN
                         SET @TACodeNo11=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo11=@code,TACodeDesc11=@description,TAAmount11=ISNULL(@amount,0),TAStatus11='True' 
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo12 = 0
                     BEGIN
                         SET @TACodeNo12=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo12=@code,TACodeDesc12=@description,TAAmount12=ISNULL(@amount,0),TAStatus12='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo13 = 0
                     BEGIN
                         SET @TACodeNo13=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo13=@code,TACodeDesc13=@description,TAAmount13=ISNULL(@amount,0),TAStatus13='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo14 = 0
                     BEGIN
                         SET @TACodeNo14=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo14=@code,TACodeDesc14=@description,TAAmount14=ISNULL(@amount,0),TAStatus14='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo15 = 0
                     BEGIN
                         SET @TACodeNo15=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo15=@code,TACodeDesc15=@description,TAAmount15=ISNULL(@amount,0),TAStatus15='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo16 = 0
                     BEGIN
                         SET @TACodeNo16=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo16=@code,TACodeDesc16=@description,TAAmount16=ISNULL(@amount,0),TAStatus16='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo17 = 0
                     BEGIN
                         SET @TACodeNo17=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo17=@code,TACodeDesc17=@description,TAAmount17=ISNULL(@amount,0),TAStatus17='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo18 = 0
                     BEGIN
                         SET @TACodeNo18=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo18=@code,TACodeDesc18=@description,TAAmount18=ISNULL(@amount,0),TAStatus18='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo19 = 0
                     BEGIN
                         SET @TACodeNo19=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo19=@code,TACodeDesc19=@description,TAAmount19=ISNULL(@amount,0),TAStatus19='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TACodeNo20 = 0
                     BEGIN
                         SET @TACodeNo20=@code;  
                         UPDATE A2ZEMPFSALARY SET TACodeNo20=@code,TACodeDesc20=@description,TAAmount20=ISNULL(@amount,0),TAStatus20='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
-------- End of Update ---------------
                  
                  SET @GrossTotal = (ISNULL(@GrossTotal,0) + ISNULL(@amount,0));

                  FETCH NEXT FROM allowTable INTO @code,@description;
                  END
                  CLOSE allowTable; 
                  DEALLOCATE allowTable;
      


    SET @GrossTotal = (@GrossTotal + ISNULL(@BasicAmt,0) + ISNULL(@ConsolidatedAmt,0));    

    UPDATE A2ZEMPFSALARY SET GrossTotal=@GrossTotal WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
   

	FETCH NEXT FROM salaryTable INTO 
    @EmpCode,@EmpName,@EmpArea,@EmpRelagion,@EmpServiceType,@EmpDesignation,@EmpDesigDesc,@EmpGrade,
@EmpBaseGrade,@EmpPayLabel,@TotalDays,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
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

	

	END

CLOSE salaryTable; 
DEALLOCATE salaryTable;

--COMMIT TRANSACTION
--		SET NOCOUNT OFF
--END TRY
--
--BEGIN CATCH
--		ROLLBACK TRANSACTION
--
--		DECLARE @ErrorSeverity INT
--		DECLARE @ErrorState INT
--		DECLARE @ErrorMessage NVARCHAR(4000);	  
--		SELECT 
--			@ErrorMessage = ERROR_MESSAGE(),
--			@ErrorSeverity = ERROR_SEVERITY(),
--			@ErrorState = ERROR_STATE();	  
--		RAISERROR 
--		(
--			@ErrorMessage, -- Message text.
--			@ErrorSeverity, -- Severity.
--			@ErrorState -- State.
--		);	
--END CATCH

END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

