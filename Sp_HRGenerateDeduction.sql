
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_HRGenerateDeduction](@SalDate smalldatetime)  

---ALTER PROCEDURE [dbo].[Sp_HRGenerateDeduction]  
AS
BEGIN

/*

EXECUTE Sp_HRGenerateDeduction '2016-02-29'

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

DECLARE @Consulated money;
DECLARE @Basic money;




DECLARE @DeductTotal money;
DECLARE @NetPayment money;


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





--BEGIN TRY
--	BEGIN TRANSACTION
--		SET NOCOUNT ON

DECLARE salaryTable CURSOR FOR
SELECT EmpCode,EmpName,EmpArea,EmpRelagion,EmpServiceType,EmpDesignation,EmpDesigDesc,EmpGrade,EmpBaseGrade,EmpPayLabel,BasicAmount,ConsolidatedAmt,TACodeNo1,TAAmount1,TACodeNo2,TAAmount2,
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
@EmpCode,@EmpName,@EmpArea,@EmpRelagion,@EmpServiceType,@EmpDesignation,@EmpDesigDesc,@EmpGrade,@EmpBaseGrade,@EmpPayLabel,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
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



         SET @BasicAmt= (SELECT BasicAmount FROM A2ZEMPFSALARY WHERE EmpCode=@EmpCode AND SalDate = @SalDate);               
          
         SET @DeductTotal = 0;
         SET @amount = 0;
         
         

         DECLARE dedTable CURSOR FOR
         SELECT Code,Description FROM A2ZEMPWISEDEDCODE WHERE EmpCode = @EmpCode AND Code < 16 AND Status = 'True';
         OPEN dedTable;
         FETCH NEXT FROM dedTable INTO @code,@description;

         WHILE @@FETCH_STATUS = 0 
	         BEGIN
--                  SET @TDCodeNo1= (SELECT TDCodeNo1 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo2= (SELECT TDCodeNo2 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo3= (SELECT TDCodeNo3 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo4= (SELECT TDCodeNo4 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo5= (SELECT TDCodeNo5 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo6= (SELECT TDCodeNo6 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo7= (SELECT TDCodeNo7 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo8= (SELECT TDCodeNo8 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo9= (SELECT TDCodeNo9 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo10= (SELECT TDCodeNo10 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo11= (SELECT TDCodeNo11 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo12= (SELECT TDCodeNo12 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo13= (SELECT TDCodeNo13 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo14= (SELECT TDCodeNo14 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo15= (SELECT TDCodeNo15 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo16= (SELECT TDCodeNo16 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo17= (SELECT TDCodeNo17 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo18= (SELECT TDCodeNo18 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo19= (SELECT TDCodeNo19 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  
--                  SET @TDCodeNo20= (SELECT TDCodeNo20 FROM A2ZEMPFSALARY WHERE EmpCode = @empcode AND SalDate = @SalDate);  

    
                  SET @dependsOn= (SELECT DependsOn FROM A2ZDEDCTRL WHERE DedCode = @code);  
                  SET @location= (SELECT Location FROM A2ZDEDCTRL WHERE DedCode = @code);  
                  SET @servtype= (SELECT ServType FROM A2ZDEDCTRL WHERE DedCode = @code);  
                  SET @religion= (SELECT Religion FROM A2ZDEDCTRL WHERE DedCode = @code);  
                  SET @percentage= (SELECT Percentage FROM A2ZDEDCTRL WHERE DedCode = @code);  
                  
                  SET @amount = 0;

                  IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                BEGIN
                     SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                         DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                            ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                     SET @amount = round(((@BasicAmt * @perc)/100),0);
                END

             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                             
                        BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END


             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);   
                          SET @amount = round(((@BasicAmt * @perc)/100),0);     
                     END
                  
             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);        
                     END

             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);           
                     END

             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea);        
                     END


             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
             ELSE IF @dependsOn = 1 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);        
                     END

             ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                               
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);        
                     END

              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                                
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade);        
                     END

              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND 
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND 
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END


              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND ReligionCode=@EmpRelagion); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                               
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND ReligionCode=@EmpRelagion);        
                     END

                  
              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND 
                              ServTypeCode=@EmpServiceType);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);            
                     END
                   
              ELSE IF @dependsOn = 1 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False'  AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND BaseGrade=@EmpBaseGrade AND GradeCode=@EmpGrade AND 
                              ServTypeCode=@EmpServiceType);        
                     END
---------------------------------------------
              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);          
                     END
                  
              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END
               
              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea);   
                          SET @amount = round(((@BasicAmt * @perc)/100),0);      
                     END

              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);   
                          SET @amount = round(((@BasicAmt * @perc)/100),0);          
                     END
                 
              ELSE IF @dependsOn = 2 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                                
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation);
                          SET @amount = round(((@BasicAmt * @perc)/100),0);          
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                                
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND 
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);       
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                               
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND 
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                                 
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);           
                     END

              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                                 
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND ReligionCode=@EmpRelagion);        
                     END
                  
              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND 
                              ServTypeCode=@EmpServiceType);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);             
                     END
              ELSE IF @dependsOn = 2 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND DesignationCode=@EmpDesignation AND 
                              ServTypeCode=@EmpServiceType);        
                     END

-----------------------------------------------------------------
              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);   
                          SET @amount = round(((@BasicAmt * @perc)/100),0);     
                     END
                  
              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND LocationCode=@EmpArea AND
                              ServTypeCode=@EmpServiceType);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND LocationCode=@EmpArea); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);           
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND LocationCode=@EmpArea);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 3 AND @location = 'True' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                             
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND LocationCode=@EmpArea AND
                              ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                                
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);        
                     END

               ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'False' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                               
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                            
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND  
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'True' AND @religion = 'True' AND @percentage = 'False'  AND (@Basic > 0 OR @Consulated > 0)                           
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND  
                              ServTypeCode=@EmpServiceType AND ReligionCode=@EmpRelagion);        
                     END

              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND ReligionCode=@EmpRelagion); 
                          SET @amount = round(((@BasicAmt * @perc)/100),0);         
                     END
                  
              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'False' AND @religion = 'True' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                               
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND ReligionCode=@EmpRelagion);        
                     END
            
              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'True' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @perc= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND  
                              ServTypeCode=@EmpServiceType);  
                          SET @amount = round(((@BasicAmt * @perc)/100),0);            
                     END
                   
              ELSE IF @dependsOn = 3 AND @location = 'False' AND @servtype = 'True' AND @religion = 'False' AND @percentage = 'False' AND (@Basic > 0 OR @Consulated > 0)                              
                     BEGIN
                          SET @amount= (SELECT Amount FROM A2ZDEDCTRLDTL WHERE 
                              DedCode=@code AND  
                              ServTypeCode=@EmpServiceType);        
                     END

------------------------------------------------------------------------------

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
                  
                  SET @CodeStatus= (SELECT Status FROM A2ZDEDUCTION WHERE Code = @code);  

                  IF @EmpBaseGrade = 3 or @CodeStatus = 'False'
                     BEGIN
                         SET @amount = 0;
                     END  

-------------------------------------------------------------------------------------------------------
                  SET @Tamount= (SELECT Amount FROM A2ZEMPTSALARY WHERE EmpCode = @EmpCode AND CodeHead=2 AND CodeNo=@code AND StatusT=1 AND SalDate=@SalDate);  
                  IF @Tamount IS NOT NULL
                     BEGIN
                         SET @amount = @Tamount;
                     END

                  SET @Tamount= (SELECT Amount FROM A2ZEMPTSALARY WHERE EmpCode = @EmpCode AND CodeHead=2 AND CodeNo=@code AND StatusR=1 AND SalDate=@SalDate);  
                  IF @Tamount IS NOT NULL
                     BEGIN
                         SET @amount = @Tamount;
                     END

-------------------------------------------------------------------------------------------------------

                  IF @TDCodeNo1 = 0
                     BEGIN
                         SET @TDCodeNo1=@code; 
                         UPDATE A2ZEMPFSALARY SET TDCodeNo1=@code,TDCodeDesc1=@description,TDAmount1=ISNULL(@amount,0),TDStatus1='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo2 = 0
                     BEGIN
                         SET @TDCodeNo2=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo2=@code,TDCodeDesc2=@description,TDAmount2=ISNULL(@amount,0),TDStatus2='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo3 = 0
                     BEGIN
                         SET @TDCodeNo3=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo3=@code,TDCodeDesc3=@description,TDAmount3=ISNULL(@amount,0),TDStatus3='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo4 = 0
                     BEGIN
                         SET @TDCodeNo4=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo4=@code,TDCodeDesc4=@description,TDAmount4=ISNULL(@amount,0),TDStatus4='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo5 = 0
                     BEGIN
                         SET @TDCodeNo5=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo5=@code,TDCodeDesc5=@description,TDAmount5=ISNULL(@amount,0),TDStatus5='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo6 = 0
                     BEGIN
                         SET @TDCodeNo6=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo6=@code,TDCodeDesc6=@description,TDAmount6=ISNULL(@amount,0),TDStatus6='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo7 = 0
                     BEGIN
                         SET @TDCodeNo7=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo7=@code,TDCodeDesc7=@description,TDAmount7=ISNULL(@amount,0),TDStatus7='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo8 = 0
                     BEGIN
                         SET @TDCodeNo8=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo8=@code,TDCodeDesc8=@description,TDAmount8=ISNULL(@amount,0),TDStatus8='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo9 = 0
                     BEGIN
                         SET @TDCodeNo9=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo9=@code,TDCodeDesc9=@description,TDAmount9=ISNULL(@amount,0),TDStatus9='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo10 = 0
                     BEGIN
                         SET @TDCodeNo10=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo10=@code,TDCodeDesc10=@description,TDAmount10=ISNULL(@amount,0),TDStatus10='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo11 = 0
                     BEGIN
                         SET @TDCodeNo11=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo11=@code,TDCodeDesc11=@description,TDAmount11=ISNULL(@amount,0),TDStatus11='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo12 = 0
                     BEGIN
                         SET @TDCodeNo12=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo12=@code,TDCodeDesc12=@description,TDAmount12=ISNULL(@amount,0),TDStatus12='True'  
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo13 = 0
                     BEGIN
                         SET @TDCodeNo13=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo13=@code,TDCodeDesc13=@description,TDAmount13=ISNULL(@amount,0),TDStatus13='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo14 = 0
                     BEGIN
                         SET @TDCodeNo14=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo14=@code,TDCodeDesc14=@description,TDAmount14=ISNULL(@amount,0),TDStatus14='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo15 = 0
                     BEGIN
                         SET @TDCodeNo15=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo15=@code,TDCodeDesc15=@description,TDAmount15=ISNULL(@amount,0),TDStatus15='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo16 = 0
                     BEGIN
                         SET @TDCodeNo16=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo16=@code,TDCodeDesc16=@description,TDAmount16=ISNULL(@amount,0),TDStatus16='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo17 = 0
                     BEGIN
                         SET @TDCodeNo17=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo17=@code,TDCodeDesc17=@description,TDAmount17=ISNULL(@amount,0),TDStatus17='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo18 = 0
                     BEGIN
                         SET @TDCodeNo18=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo18=@code,TDCodeDesc18=@description,TDAmount18=ISNULL(@amount,0),TDStatus18='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo19 = 0
                     BEGIN
                         SET @TDCodeNo19=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo19=@code,TDCodeDesc19=@description,TDAmount19=ISNULL(@amount,0),TDStatus19='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
                  ELSE 
                  IF @TDCodeNo20 = 0
                     BEGIN
                         SET @TDCodeNo20=@code;
                         UPDATE A2ZEMPFSALARY SET TDCodeNo20=@code,TDCodeDesc20=@description,TDAmount20=ISNULL(@amount,0),TDStatus20='True'   
                                  WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
                     END
-------- End of Update ---------------
                  
                  SET @DeductTotal = (ISNULL(@DeductTotal,0) + ISNULL(@amount,0));

                  FETCH NEXT FROM dedTable INTO @code,@description;
                  END
                  CLOSE dedTable; 
                  DEALLOCATE dedTable;

     
    
    UPDATE A2ZEMPFSALARY SET DeductTotal=@DeductTotal WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 

    
    SET @GrossTotal= (SELECT GrossTotal FROM A2ZEMPFSALARY WHERE EmpCode=@EmpCode AND SalDate = @SalDate); 

    SET @NetPayment= (@GrossTotal - @DeductTotal);

    UPDATE A2ZEMPFSALARY SET NetPayment=@NetPayment WHERE EmpCode=@EmpCode AND SalDate = @SalDate; 
     

	FETCH NEXT FROM salaryTable INTO 
    @EmpCode,@EmpName,@EmpArea,@EmpRelagion,@EmpServiceType,@EmpDesignation,@EmpDesigDesc,@EmpGrade,@EmpBaseGrade,@EmpPayLabel,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
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

