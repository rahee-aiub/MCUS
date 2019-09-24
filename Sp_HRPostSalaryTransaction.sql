USE [A2ZHRMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_HRPostSalaryTransaction]    Script Date: 07/09/2019 3:56:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Sp_HRPostSalaryTransaction](@VoucherNo nvarchar(20),@VchNo nvarchar(20),@userID INT,@FromCashCode INT)     


AS
BEGIN

/*

EXECUTE Sp_HRPostSalaryTransaction 1,1,1,10101001

*/



DECLARE @code int;
DECLARE @description nvarchar(50);
DECLARE @repcolumn int;

DECLARE @EmpCode int;
DECLARE @EmpName nvarchar(50);
DECLARE @EmpDesigDesc nvarchar(50);
DECLARE @EmpGradeDesc nvarchar(50);

DECLARE @Area int;
DECLARE @EmpProject int;

DECLARE @accNo BIGINT;


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


DECLARE @TDAmount money;
DECLARE @LoanInterest money;

DECLARE @DeductTotal money;
DECLARE @NetPayment money;
DECLARE @Status int;



DECLARE @Allowance1 money;
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

DECLARE @26OfficeFlag tinyint;
DECLARE @26OwnFlag tinyint;

DECLARE @28Flag tinyint;
DECLARE @55Flag tinyint;
DECLARE @56Flag tinyint;
DECLARE @57Flag tinyint;
DECLARE @58Flag tinyint;
DECLARE @63PFlag tinyint;
DECLARE @63IFlag tinyint;
DECLARE @65PFlag tinyint;
DECLARE @65IFlag tinyint;


DECLARE @accType int;


DECLARE @TrnDate smalldatetime;

DECLARE @NewDate smalldatetime;

DECLARE @TrnCode int;
DECLARE @FuncOpt smallint;
DECLARE @FuncOptDes nvarchar(50);
DECLARE @PayType smallint;
DECLARE @TrnDesc nvarchar(50);
DECLARE @TrnType tinyint;
DECLARE @TrnDrCr tinyint;

DECLARE @ShowInterest tinyint;
DECLARE @TrnGLAccNoDr int;
DECLARE @TrnGLAccNoCr int;
DECLARE @GLCode int;

DECLARE @salctrlglcodedr int;
DECLARE @salctrlglcodecr int;


DECLARE @GLDrCr  tinyint;
DECLARE @GLDesc  nvarchar(50);
DECLARE @GLAmount money;

DECLARE @DebitAmt money;
DECLARE @CreditAmt money;


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


TRUNCATE TABLE WFPostSalary;


SET @TrnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

SET @NewDate = (SELECT ProcessDate FROM A2ZHRMCUS..A2ZHRPARAMETER);

DECLARE postsalaryTable CURSOR FOR
SELECT EmpCode,EmpName,EmpDesigDesc,EmpGradeDesc,EmpProject,BasicAmount,ConsolidatedAmt,
TACodeNo1,TAAmount1,TACodeNo2,TAAmount2,
TACodeNo3,TAAmount3,TACodeNo4,TAAmount4,TACodeNo5,TAAmount5,TACodeNo6,TAAmount6,TACodeNo7,TAAmount7,
TACodeNo8,TAAmount8,TACodeNo9,TAAmount9,TACodeNo10,TAAmount10,GrossTotal,
TDCodeNo1,TDAmount1,TDCodeNo2,TDAmount2,TDCodeNo3,TDAmount3,TDCodeNo4,TDAmount4,
TDCodeNo5,TDAmount5,TDCodeNo6,TDAmount6,TDCodeNo7,TDAmount7,TDCodeNo8,TDAmount8,
TDCodeNo9,TDAmount9,TDCodeNo10,TDAmount10,
TACodeNo11,TAAmount11,TACodeNo12,TAAmount12,
TACodeNo13,TAAmount13,TACodeNo14,TAAmount14,TACodeNo15,TAAmount15,TACodeNo16,TAAmount16,TACodeNo17,TAAmount17,
TACodeNo18,TAAmount18,TACodeNo19,TAAmount19,TACodeNo20,TAAmount20,
TDCodeNo11,TDAmount11,TDCodeNo12,TDAmount12,TDCodeNo13,TDAmount13,TDCodeNo14,TDAmount14,
TDCodeNo15,TDAmount15,TDCodeNo16,TDAmount16,TDCodeNo17,TDAmount17,TDCodeNo18,TDAmount18,
TDCodeNo19,TDAmount19,TDCodeNo20,TDAmount20,
DeductTotal,NetPayment,Status
FROM A2ZEMPFSALARY WHERE MONTH(SalDate) = MONTH(@NewDate) AND YEAR(SalDate) = YEAR(@NewDate);;

OPEN postsalaryTable;
FETCH NEXT FROM postsalaryTable INTO
@EmpCode,@EmpName,@EmpDesigDesc,@EmpGradeDesc,@EmpProject,@Basic,@Consulated,
@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
@TACodeNo3,@TAAmount3,@TACodeNo4,@TAAmount4,@TACodeNo5,@TAAmount5,
@TACodeNo6,@TAAmount6,@TACodeNo7,@TAAmount7,@TACodeNo8,@TAAmount8,
@TACodeNo9,@TAAmount9,@TACodeNo10,@TAAmount10,@GrossTotal,
@TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
@TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
@TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,
@TACodeNo11,@TAAmount11,@TACodeNo12,@TAAmount12,
@TACodeNo13,@TAAmount13,@TACodeNo14,@TAAmount14,@TACodeNo15,@TAAmount15,
@TACodeNo16,@TAAmount16,@TACodeNo17,@TAAmount17,@TACodeNo18,@TAAmount18,
@TACodeNo19,@TAAmount19,@TACodeNo20,@TAAmount20,
@TDCodeNo11,@TDAmount11,@TDCodeNo12,@TDAmount12,@TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,
@TDCodeNo15,@TDAmount15,@TDCodeNo16,@TDAmount16,@TDCodeNo17,@TDAmount17,@TDCodeNo18,@TDAmount18,
@TDCodeNo19,@TDAmount19,@TDCodeNo20,@TDAmount20,
@DeductTotal,@NetPayment,@Status;

WHILE @@FETCH_STATUS = 0 
	BEGIN

     SET @TrnDesc = 'Salary & Benefits ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
     
     IF @Basic > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=98 AND SalCtrlPayType = @EmpProject);
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@Basic); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @Basic) WHERE GLCode = @GLCode;
                       END
                END
        END     

     IF @Consulated > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=99 AND SalCtrlPayType = @EmpProject);
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@Consulated); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @Consulated) WHERE GLCode = @GLCode;
                       END
                END
        END      
     

     IF @TACodeNo1 > 0 AND @TAAmount1 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo1 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount1); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount1) WHERE GLCode = @GLCode;
                       END
                END
        END      
    
     IF @TACodeNo2 > 0 AND @TAAmount2 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo2 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount2); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount2) WHERE GLCode = @GLCode;
                       END
                END
        END   
     
     IF @TACodeNo3 > 0 AND @TAAmount3 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo3 AND SalCtrlPayType = @EmpProject);
                         
             IF @TrnGLAccNoDr !=0
                BEGIN
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount3); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount3) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo4 > 0 AND @TAAmount4 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo4 AND SalCtrlPayType = @EmpProject);
                   
             IF @TrnGLAccNoDr !=0
                BEGIN       
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount4); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount4) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo5 > 0 AND @TAAmount5 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo5 AND SalCtrlPayType = @EmpProject);       
             
             IF @TrnGLAccNoDr !=0
                BEGIN
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount5); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount5) WHERE GLCode = @GLCode;
                       END
                END
        END      
         
     IF @TACodeNo6 > 0 AND @TAAmount6 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo6 AND SalCtrlPayType = @EmpProject);
               
             IF @TrnGLAccNoDr !=0        
                BEGIN
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount6); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount6) WHERE GLCode = @GLCode;
                       END
                END
        END     

     IF @TACodeNo7 > 0 AND @TAAmount7 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo7 AND SalCtrlPayType = @EmpProject);
                   
             IF @TrnGLAccNoDr !=0        
                BEGIN       
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount7); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount7) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo8 > 0 AND @TAAmount8 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo8 AND SalCtrlPayType = @EmpProject);
              
             IF @TrnGLAccNoDr !=0 
                BEGIN
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount8); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount8) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo9 > 0 AND @TAAmount9 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo9 AND SalCtrlPayType = @EmpProject);
                
             IF @TrnGLAccNoDr !=0 
                BEGIN
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount9); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount9) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo10 > 0 AND @TAAmount10 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo10 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN            
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount10); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount10) WHERE GLCode = @GLCode;
                       END
                END
        END        

     IF @TACodeNo11 > 0 AND @TAAmount11 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo11 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN                 
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount11); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount11) WHERE GLCode = @GLCode;
                       END
                END
        END      
    
     IF @TACodeNo12 > 0 AND @TAAmount12 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo12 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN             
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount12); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount12) WHERE GLCode = @GLCode;
                       END
                END
        END   
     
     IF @TACodeNo13 > 0 AND @TAAmount13 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo13 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN            
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount13); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount13) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo14 > 0 AND @TAAmount14 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo14 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN            
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount14); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount14) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo15 > 0 AND @TAAmount15 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo15 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN             
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount15); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount15) WHERE GLCode = @GLCode;
                       END
                END
        END      
         
     IF @TACodeNo16 > 0 AND @TAAmount16 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo16 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN            
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount16); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount16) WHERE GLCode = @GLCode;
                       END
                END
        END     

     IF @TACodeNo17 > 0 AND @TAAmount17 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo17 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount17); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount17) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo18 > 0 AND @TAAmount18 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo18 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN             
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount18); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount18) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo19 > 0 AND @TAAmount19 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo19 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0 
                BEGIN             
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount19); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount19) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TACodeNo20 > 0 AND @TAAmount20 > 0
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=1 AND SalCtrlCode=@TACodeNo20 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0                           
                BEGIN
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,0,@TrnDesc,@TAAmount20); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TAAmount20) WHERE GLCode = @GLCode;
                       END
                END
        END                
------------------------------------------------------------------------------
     IF @TDCodeNo1 > 0 AND @TDAmount1 > 0 AND @TDCodeNo1 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo1 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount1); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount1) WHERE GLCode = @GLCode;
                       END
                END
        END      
      
     IF @TDCodeNo2 > 0 AND @TDAmount2 > 0 AND @TDCodeNo2 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo2 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount2); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount2) WHERE GLCode = @GLCode;
                       END
                END
        END      

     IF @TDCodeNo3 > 0 AND @TDAmount3 > 0 AND @TDCodeNo3 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo3 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount3); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount3) WHERE GLCode = @GLCode;
                       END
                END
        END      

     IF @TDCodeNo4 > 0 AND @TDAmount4 > 0 AND @TDCodeNo4 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo4 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount4); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount4) WHERE GLCode = @GLCode;
                       END
                END
        END      

     IF @TDCodeNo5 > 0 AND @TDAmount5 > 0 AND @TDCodeNo5 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo5 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount5); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount5) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo6 > 0 AND @TDAmount6 > 0 AND @TDCodeNo6 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo6 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount6); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount6) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo7 > 0 AND @TDAmount7 > 0 AND @TDCodeNo7 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo7 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount7); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount7) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo8 > 0 AND @TDAmount8 > 0 AND @TDCodeNo8 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo8 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount8); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount8) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo9 > 0 AND @TDAmount9 > 0 AND @TDCodeNo9 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo9 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount9); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount9) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo10 > 0 AND @TDAmount10 > 0 AND @TDCodeNo10 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo10 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount10); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount10) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo11 > 0 AND @TDAmount11 > 0 AND @TDCodeNo11 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo11 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount11); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount11) WHERE GLCode = @GLCode;
                       END
                END
        END      
      
     IF @TDCodeNo12 > 0 AND @TDAmount12 > 0 AND @TDCodeNo12 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo12 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount12); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount12) WHERE GLCode = @GLCode;
                       END
                END
        END      

     IF @TDCodeNo13 > 0 AND @TDAmount13 > 0 AND @TDCodeNo13 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo13 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount13); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount13) WHERE GLCode = @GLCode;
                       END
                END
        END      

     IF @TDCodeNo14 > 0 AND @TDAmount14 > 0 AND @TDCodeNo14 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo14 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount14); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount14) WHERE GLCode = @GLCode;
                       END
                END
        END      

     IF @TDCodeNo15 > 0 AND @TDAmount15 > 0 AND @TDCodeNo15 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo15 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount15); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount15) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo16 > 0 AND @TDAmount16 > 0 AND @TDCodeNo16 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo16 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount16); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount16) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo17 > 0 AND @TDAmount17 > 0 AND @TDCodeNo17 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo17 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount17); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount17) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo18 > 0 AND @TDAmount18 > 0 AND @TDCodeNo18 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo18 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount18); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount18) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo19 > 0 AND @TDAmount19 > 0 AND @TDCodeNo19 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo19 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount19); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount19) WHERE GLCode = @GLCode;
                       END
                END
        END   

     IF @TDCodeNo20 > 0 AND @TDAmount20 > 0 AND @TDCodeNo20 < 16
        BEGIN
             SET @TrnGLAccNoDr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                     SalCtrlHead=2 AND SalCtrlCode=@TDCodeNo20 AND SalCtrlPayType = @EmpProject);
             
             IF @TrnGLAccNoDr !=0
                BEGIN         
                    SET @GLCode= (SELECT GLCode FROM WFPostSalary where GLCode=@TrnGLAccNoDr) ;
             
                    IF @GLCode IS NULL
                       BEGIN
                           SET @TrnDesc = 'Deduction ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
                           INSERT INTO WFPostSalary(GLCode,GLDrCr,GLDesc,GLAmount) VALUES (@TrnGLAccNoDr,1,@TrnDesc,@TDAmount20); 
                       END  
                    ELSE
                       BEGIN
                           UPDATE WFPostSalary SET GLAmount = (GLAmount + @TDAmount20) WHERE GLCode = @GLCode;
                       END
                END
        END   
----------------------------------------------------------------------------

    SET @26OfficeFlag = 0;
    SET @26OwnFlag = 0;

    SET @55Flag = 0;
    SET @56Flag = 0;
    SET @57Flag = 0;
    SET @58Flag = 0;
    SET @63PFlag = 0;
    SET @63IFlag = 0;
	SET @65PFlag = 0;
	SET @65IFlag = 0;


-------------------  Provident Fund Office --------------------------------

    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
        CuType = 0 AND CuNo=0 AND AccType = 26 AND MemNo = @empcode AND AccStatus = 1);
          

    IF @TDCodeNo1 = 1 AND @TDAmount1 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount1;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 1 AND @TDAmount2 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount2;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo3 = 1 AND @TDAmount3 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount3;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


    IF @TDCodeNo4 = 1 AND @TDAmount4 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount4;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 1 AND @TDAmount5 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount5;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 1 AND @TDAmount6 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount6;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 1 AND @TDAmount7 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount7;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo8 = 1 AND @TDAmount8 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount8;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo9 = 1 AND @TDAmount9 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount9;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 1 AND @TDAmount10 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount10;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 1 AND @TDAmount11 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount11;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 1 AND @TDAmount12 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount12;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo13 = 1 AND @TDAmount13 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount13;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


    IF @TDCodeNo14 = 1 AND @TDAmount14 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount14;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 1 AND @TDAmount15 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount15;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 1 AND @TDAmount16 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount16;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 1 AND @TDAmount17 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount17;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo18 = 1 AND @TDAmount18 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount18;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo19 = 1 AND @TDAmount19 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount19;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 1 AND @TDAmount20 > 0
       BEGIN
            SET @26OfficeFlag = 1;
            SET @TDAmount = @TDAmount20;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @26OfficeFlag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 26);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 26 AND FuncOpt=75 AND PayType=10);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 26 AND FuncOpt=75 AND PayType=10);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 26 AND FuncOpt=75 AND PayType=10);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 26 AND FuncOpt=75 AND PayType=10);

           SET @TrnDesc = 'Staff PF Company';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,26,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@TDAmount,0,
                  0,0,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@TDAmount,
                  0,@TDAmount,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 

       END

-------------------  Provident Fund Own --------------------------------

    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
        CuType = 0 AND CuNo=0 AND AccType = 26 AND MemNo = @empcode AND AccStatus = 1);
          

    IF @TDCodeNo1 = 2 AND @TDAmount1 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount1;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 2 AND @TDAmount2 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount2;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo3 = 2 AND @TDAmount3 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount3;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo4 = 2 AND @TDAmount4 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount4;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 2 AND @TDAmount5 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount5;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 2 AND @TDAmount6 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount6;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 2 AND @TDAmount7 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount7;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo8 = 2 AND @TDAmount8 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount8;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo9 = 2 AND @TDAmount9 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount9;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 2 AND @TDAmount10 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount10;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 2 AND @TDAmount11 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount11;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 2 AND @TDAmount12 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount12;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo13 = 2 AND @TDAmount13 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount13;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo14 = 2 AND @TDAmount14 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount14;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 2 AND @TDAmount15 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount15;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 2 AND @TDAmount16 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount16;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 2 AND @TDAmount17 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount17;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo18 = 2 AND @TDAmount18 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount18;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo19 = 2 AND @TDAmount19 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount19;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 2 AND @TDAmount20 > 0
       BEGIN
            SET @26OwnFlag = 1;
            SET @TDAmount = @TDAmount20;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END
    

    IF @26OwnFlag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 26);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 26 AND FuncOpt=75 AND PayType=11);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 26 AND FuncOpt=75 AND PayType=11);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 26 AND FuncOpt=75 AND PayType=11);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 26 AND FuncOpt=75 AND PayType=11);

           SET @TrnDesc = 'Staff PF Own';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,26,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@TDAmount,0,
                  0,0,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@TDAmount,
                  0,@TDAmount,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 

       END

-------------------  Income Tax --------------------------------

    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
        CuType = 0 AND CuNo=0 AND AccType = 28 AND MemNo = @empcode AND AccStatus = 1);
          

    IF @TDCodeNo1 = 8 AND @TDAmount1 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount1;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 8 AND @TDAmount2 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount2;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo3 = 8 AND @TDAmount3 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount3;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo4 = 8 AND @TDAmount4 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount4;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 8 AND @TDAmount5 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount5;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 8 AND @TDAmount6 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount6;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 8 AND @TDAmount7 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount7;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo8 = 8 AND @TDAmount8 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount8;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo9 = 8 AND @TDAmount9 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount9;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 8 AND @TDAmount10 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount10;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 8 AND @TDAmount11 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount11;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 8 AND @TDAmount12 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount12;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo13 = 8 AND @TDAmount13 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount13;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo14 = 8 AND @TDAmount14 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount14;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 8 AND @TDAmount15 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount15;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 8 AND @TDAmount16 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount16;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 8 AND @TDAmount17 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount17;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo18 = 8 AND @TDAmount18 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount18;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo19 = 8 AND @TDAmount19 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount19;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 8 AND @TDAmount20 > 0
       BEGIN
            SET @28Flag = 1;
            SET @TDAmount = @TDAmount20;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccOwnBalance = (AccOwnBalance + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END
    

    IF @28Flag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 28);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 28 AND FuncOpt=75);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 28 AND FuncOpt=75);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 28 AND FuncOpt=75);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 28 AND FuncOpt=75);

           SET @TrnDesc = 'Income Tax';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,28,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@TDAmount,0,
                  0,0,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@TDAmount,
                  0,@TDAmount,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 

       END

---------------------Service Loan-------------------------------------------------------
    
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
        CuType = 0 AND CuNo=0 AND AccType = 55 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);
          

    IF @TDCodeNo1 = 16 AND @TDAmount1 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount1;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 16 AND @TDAmount2 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount2;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 16 AND @TDAmount3 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount3;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 16 AND @TDAmount4 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount4;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 16 AND @TDAmount5 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount5;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 16 AND @TDAmount6 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount6;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 16 AND @TDAmount7 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount7;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 16 AND @TDAmount8 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount8;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 16 AND @TDAmount9 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount9;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 16 AND @TDAmount10 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount10;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 16 AND @TDAmount11 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount11;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END
    
    IF @TDCodeNo12 = 16 AND @TDAmount12 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount12;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo13 = 16 AND @TDAmount13 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount13;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END
    IF @TDCodeNo14 = 16 AND @TDAmount14 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount14;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END    
    
    IF @TDCodeNo15 = 16 AND @TDAmount15 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount15;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END
     
    IF @TDCodeNo16 = 16 AND @TDAmount16 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount16;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END    

    IF @TDCodeNo17 = 16 AND @TDAmount17 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount17;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END
    
    IF @TDCodeNo18 = 16 AND @TDAmount18 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount18;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo19 = 16 AND @TDAmount19 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount19;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 16 AND @TDAmount20 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount20;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @55Flag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 55);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 55 AND FuncOpt=75);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 55 AND FuncOpt=75);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 55 AND FuncOpt=75);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 55 AND FuncOpt=75);

           SET @TrnDesc = 'Loan Amount Received';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,55,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@TDAmount,0,
                  0,0,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,(0-@TDAmount),
                  0,@TDAmount,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 

       END

-----------------------Motor Cycle Loan----------------------------------------------------

    
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
           CuType = 0 AND CuNo=0 AND AccType = 56 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);
     

    IF @TDCodeNo1 = 17 AND @TDAmount1 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount1;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 17 AND @TDAmount2 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount2;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 17 AND @TDAmount3 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount3;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 17 AND @TDAmount4 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount4;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 17 AND @TDAmount5 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount5;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 17 AND @TDAmount6 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount6;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 17 AND @TDAmount7 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount7;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 17 AND @TDAmount8 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount8;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 17 AND @TDAmount9 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount9;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 17 AND @TDAmount10 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount10;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

     IF @TDCodeNo11 = 17 AND @TDAmount11 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount11;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 17 AND @TDAmount12 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount12;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 17 AND @TDAmount13 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount13;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 17 AND @TDAmount14 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount14;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 17 AND @TDAmount15 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount15;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 17 AND @TDAmount16 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount16;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 17 AND @TDAmount17 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount17;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 17 AND @TDAmount18 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount18;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 17 AND @TDAmount19 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount19;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 17 AND @TDAmount20 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount20;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END


       IF @56Flag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 56);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 56 AND FuncOpt=75);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 56 AND FuncOpt=75);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 56 AND FuncOpt=75);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 56 AND FuncOpt=75);

           SET @TrnDesc = 'Loan Amount Received';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,56,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@TDAmount,0,
                  0,0,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,(0-@TDAmount),
                  0,@TDAmount,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 

       END


-------------------By Cycle Loan---------------------------------------------------------
    
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
                CuType = 0 AND CuNo=0 AND AccType = 57 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);
    

    IF @TDCodeNo1 = 18 AND @TDAmount1 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount1;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 18 AND @TDAmount2 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount2;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 18 AND @TDAmount3 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount3;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 18 AND @TDAmount4 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount4;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 18 AND @TDAmount5 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount5;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 18 AND @TDAmount6 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount6;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 18 AND @TDAmount7 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount7;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 18 AND @TDAmount8 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount8;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 18 AND @TDAmount9 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount9;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 18 AND @TDAmount10 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount10;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END


    IF @TDCodeNo11 = 18 AND @TDAmount11 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount11;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 18 AND @TDAmount12 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount12;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 18 AND @TDAmount13 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount13;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 18 AND @TDAmount14 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount14;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 18 AND @TDAmount15 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount15;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 18 AND @TDAmount16 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount16;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 18 AND @TDAmount17 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount17;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 18 AND @TDAmount18 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount18;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 18 AND @TDAmount19 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount19;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 18 AND @TDAmount20 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount20;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END


       IF @57Flag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 57);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 57 AND FuncOpt=75);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 57 AND FuncOpt=75);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 57 AND FuncOpt=75);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 57 AND FuncOpt=75);

           SET @TrnDesc = 'Loan Amount Received';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,57,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@TDAmount,0,
                  0,0,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,(0-@TDAmount),
                  0,@TDAmount,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 

       END

----------------------Computer Loan-------------------------------------------------------

    
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
                CuType = 0 AND CuNo=0 AND AccType = 58 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);
     

    IF @TDCodeNo1 = 19 AND @TDAmount1 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount1;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 19 AND @TDAmount2 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount2;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 19 AND @TDAmount3 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount3;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 19 AND @TDAmount4 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount4;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 19 AND @TDAmount5 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount5;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 19 AND @TDAmount6 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount6;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 19 AND @TDAmount7 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount7;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 19 AND @TDAmount8 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount8;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 19 AND @TDAmount9 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount9;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 19 AND @TDAmount10 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount10;
 			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END


    IF @TDCodeNo11 = 19 AND @TDAmount11 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount11;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 19 AND @TDAmount12 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount12;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 19 AND @TDAmount13 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount13;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 19 AND @TDAmount14 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount14;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 19 AND @TDAmount15 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount15;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 19 AND @TDAmount16 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount16;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 19 AND @TDAmount17 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount17;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 19 AND @TDAmount18 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount18;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 19 AND @TDAmount19 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount19;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 19 AND @TDAmount20 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount20;
 			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END


       IF @58Flag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 58);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 58 AND FuncOpt=75);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 58 AND FuncOpt=75);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 58 AND FuncOpt=75);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 58 AND FuncOpt=75);

           SET @TrnDesc = 'Loan Amount Received';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,58,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@TDAmount,0,
                  0,0,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,(0-@TDAmount),
                  0,@TDAmount,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 

       END

-------------------PF Loan Interest-----------------------------------------------
	SET @LoanInterest = 0
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
                CuType = 0 AND CuNo=0 AND AccType = 63 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);
     

    IF @TDCodeNo1 = 21 AND @TDAmount1 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount1;
		END

    IF @TDCodeNo2 = 21 AND @TDAmount2 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount2;
		END
    
    IF @TDCodeNo3 = 21 AND @TDAmount3 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount3;
		END

    IF @TDCodeNo4 = 21 AND @TDAmount4 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount4;
		END
    
    IF @TDCodeNo5 = 21 AND @TDAmount5 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount5;
		END

    IF @TDCodeNo6 = 21 AND @TDAmount6 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount6;
		END

    IF @TDCodeNo7 = 21 AND @TDAmount7 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount7;
		END

    IF @TDCodeNo8 = 21 AND @TDAmount8 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount8;
		END

    IF @TDCodeNo9 = 21 AND @TDAmount9 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount9;
		END

    IF @TDCodeNo10 = 21 AND @TDAmount10 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount10;
		END

    IF @TDCodeNo11 = 21 AND @TDAmount11 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount11;
		END

    IF @TDCodeNo12 = 21 AND @TDAmount12 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount12;
		END
    
    IF @TDCodeNo13 = 21 AND @TDAmount13 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount13;
		END

    IF @TDCodeNo14 = 21 AND @TDAmount14 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount14;
		END
    
    IF @TDCodeNo15 = 21 AND @TDAmount15 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount15;
		END

    IF @TDCodeNo16 = 21 AND @TDAmount16 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount16;
		END

    IF @TDCodeNo17 = 21 AND @TDAmount17 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount17;
		END

    IF @TDCodeNo18 = 21 AND @TDAmount18 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount18;
		END

    IF @TDCodeNo19 = 21 AND @TDAmount19 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount19;
		END

    IF @TDCodeNo20 = 21 AND @TDAmount20 > 0
       BEGIN
            SET @63IFlag = 1;
            SET @LoanInterest = @TDAmount20;
		END

    IF @63IFlag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 63);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 63 AND FuncOpt=75 AND PayType = 402);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 63 AND FuncOpt=75 AND PayType = 402);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 63 AND FuncOpt=75 AND PayType = 402);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 63 AND FuncOpt=75 AND PayType = 402);           

           
           SET @TrnDesc = 'Loan Interest Received';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,63,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@LoanInterest,0,
                  1,@LoanInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@LoanInterest,
                  0,@LoanInterest,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2);
       END



 -------------------PF Loan Principal------------------------------------------------


   
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
                CuType = 0 AND CuNo=0 AND AccType = 63 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);
     

    IF @TDCodeNo1 = 20 AND @TDAmount1 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount1;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 20 AND @TDAmount2 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount2;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 20 AND @TDAmount3 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount3;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 20 AND @TDAmount4 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount4;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 20 AND @TDAmount5 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount5;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 20 AND @TDAmount6 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount6;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 20 AND @TDAmount7 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount7;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 20 AND @TDAmount8 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount8;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 20 AND @TDAmount9 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount9;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 20 AND @TDAmount10 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount10;
 			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END


    IF @TDCodeNo11 = 20 AND @TDAmount11 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount11;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 20 AND @TDAmount12 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount12;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 20 AND @TDAmount13 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount13;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 20 AND @TDAmount14 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount14;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 20 AND @TDAmount15 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount15;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 20 AND @TDAmount16 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount16;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 20 AND @TDAmount17 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount17;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 20 AND @TDAmount18 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount18;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 20 AND @TDAmount19 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount19;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 20 AND @TDAmount20 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount20;
 			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END


       IF @63PFlag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 63);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 63 AND FuncOpt=75 AND PayType = 403);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 63 AND FuncOpt=75 AND PayType = 403);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 63 AND FuncOpt=75 AND PayType = 403);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 63 AND FuncOpt=75 AND PayType = 403);

           SET @TrnDesc = 'Loan Amount Received';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,63,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@TDAmount,0,
                  0,@LoanInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@TDAmount,
                  0,@TDAmount,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2);
       END

----MRC START -----
-------------------EDU Loan Interest-----------------------------------------------

    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
                CuType = 0 AND CuNo=0 AND AccType = 65 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);
     

    

    IF @TDCodeNo1 = 23 AND @TDAmount1 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount1;
		END

    IF @TDCodeNo2 = 23 AND @TDAmount2 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount2;
		END
    
    IF @TDCodeNo3 = 23 AND @TDAmount3 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount3;
		END

    IF @TDCodeNo4 = 23 AND @TDAmount4 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount4;
		END
    
    IF @TDCodeNo5 = 23 AND @TDAmount5 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount5;
		END

    IF @TDCodeNo6 = 23 AND @TDAmount6 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount6;
		END

    IF @TDCodeNo7 = 23 AND @TDAmount7 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount7;
		END

    IF @TDCodeNo8 = 23 AND @TDAmount8 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount8;
		END

    IF @TDCodeNo9 = 23 AND @TDAmount9 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount9;
		END

    IF @TDCodeNo10 = 23 AND @TDAmount10 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount10;
		END

    IF @TDCodeNo11 = 23 AND @TDAmount11 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount11;
		END

    IF @TDCodeNo12 = 23 AND @TDAmount12 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount12;
		END
    
    IF @TDCodeNo13 = 23 AND @TDAmount13 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount13;
		END

    IF @TDCodeNo14 = 23 AND @TDAmount14 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount14;
		END
    
    IF @TDCodeNo15 = 23 AND @TDAmount15 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount15;
		END

    IF @TDCodeNo16 = 23 AND @TDAmount16 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount16;
		END

    IF @TDCodeNo17 = 23 AND @TDAmount17 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount17;
		END

    IF @TDCodeNo18 = 23 AND @TDAmount18 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount18;
		END

    IF @TDCodeNo19 = 23 AND @TDAmount19 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount19;
		END

    IF @TDCodeNo20 = 23 AND @TDAmount20 > 0
       BEGIN
            SET @65IFlag = 1;
            SET @LoanInterest = @TDAmount20;
		END

    IF @65IFlag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 65);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 65 AND FuncOpt=75 AND PayType = 402);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 65 AND FuncOpt=75 AND PayType = 402);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 65 AND FuncOpt=75 AND PayType = 402);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 65 AND FuncOpt=75 AND PayType = 402);           

           
           SET @TrnDesc = 'Loan Interest Received';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,65,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@LoanInterest,0,
                  1,@LoanInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@LoanInterest,
                  0,@LoanInterest,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2);
       END



 -------------------EDU Loan Principal------------------------------------------------

   
   
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
                CuType = 0 AND CuNo=0 AND AccType = 65 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);
     

    

    IF @TDCodeNo1 = 22 AND @TDAmount1 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount1;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 22 AND @TDAmount2 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount2;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 22 AND @TDAmount3 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount3;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 22 AND @TDAmount4 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount4;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 22 AND @TDAmount5 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount5;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 22 AND @TDAmount6 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount6;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 22 AND @TDAmount7 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount7;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 22 AND @TDAmount8 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount8;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 22 AND @TDAmount9 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount9;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 22 AND @TDAmount10 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount10;
 			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END


    IF @TDCodeNo11 = 22 AND @TDAmount11 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount11;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 22 AND @TDAmount12 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount12;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 22 AND @TDAmount13 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount13;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 22 AND @TDAmount14 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount14;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 22 AND @TDAmount15 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount15;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 22 AND @TDAmount16 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount16;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 22 AND @TDAmount17 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount17;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 22 AND @TDAmount18 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount18;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 22 AND @TDAmount19 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount19;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 22 AND @TDAmount20 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount20;
 			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @TDAmount,
                    AccBalance = (AccBalance + @TDAmount), 
                    AccPrincipal = (AccPrincipal + @TDAmount),
                    AccTotalDep = (AccTotalDep + @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END


       IF @65PFlag = 1
       BEGIN
           SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 65);
           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 65 AND FuncOpt=75 AND PayType = 403);
           SET @PayType = (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 65 AND FuncOpt=75 AND PayType = 403);
           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 65 AND FuncOpt=75 AND PayType = 403);
           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 65 AND FuncOpt=75 AND PayType = 403);


           SET @TrnDesc = 'Loan Amount Received';
           SET @FuncOptDes = 'Payroll Deposit';
------------ NORMAL TRANSACTION -------------
           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,65,@accNo,
                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@TDAmount,0,
                  0,@LoanInterest,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,(0-@TDAmount),
                  0,@TDAmount,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2);
       END


---MRC END ------

----- NET SALARY POSTING ------------------------

           IF @Status = 99
              BEGIN            
                  SET @TrnGLAccNoCr= (SELECT SalCtrlGLCode FROM A2ZSALARYCTRL WHERE 
                                 SalCtrlHead=99 AND SalCtrlCode=0 AND SalCtrlPayType = @EmpProject);

                  SET @TrnDesc = 'Net Salary Stop ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4)) + '-' + 'Per No. ' + CAST(@EmpCode AS VARCHAR(6));			

                  INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,
                  AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)
                  VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,0,
                  0,0,75,0,0,3,1,@TrnDesc ,0,0,
                  0,0,0,0,@TrnGLAccNoCr,@TrnGLAccNoCr,@NetPayment,
                  0,@NetPayment,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 
              END
           ELSE
              BEGIN
					SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 25 AND FuncOpt=75);
					SET @TrnDesc = 'Net Salary Amount ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));

                  INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,
                  AccNo,TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)
                  VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,0,
                  0,0,75,0,0,3,1,@TrnDesc ,0,0,
                  0,0,0,0,@TrnGLAccNoCr,@TrnGLAccNoCr,@NetPayment,
                  0,@NetPayment,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 
              END
--                  SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
--                      CuType = 0 AND CuNo=0 AND AccType = 25 AND MemNo = @empcode AND AccStatus = 1);
--
--                  UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccPrevTrnTypeU = AccLastTrnTypeU, AccPrevTrnDateU = AccLastTrnDateU,
--					AccPrevTrnAmtU = AccLastTrnAmtU, AccLastTrnTypeU = 3,
--					AccLastTrnDateU = @trnDate, AccLastTrnAmtU = @NetPayment,
--                    AccBalance = (AccBalance + @NetPayment) 
--                    
--			      WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 25 AND AccNo = @accNo;
--
--                  SET @TrnCode= (SELECT TrnCode FROM A2ZCSMCUS..A2ZTRNCODE WHERE AccType = 25);
--                  SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 25 AND FuncOpt=75);   
--                  SET @PayType= (SELECT PayType FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 25 AND FuncOpt=75);    
--                  SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 25 AND FuncOpt=75);
--                  SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 25 AND FuncOpt=75);
--
--                  SET @TrnDesc = 'Net Salary Amount ' + DATENAME(MONTH,@NewDate) + '-' + CAST(YEAR(@NewDate) AS VARCHAR(4));			
--           
-------------- NORMAL TRANSACTION -------------
--                  INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
--                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
--                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
--                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)
--
--                  VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,25,@accNo,
--                  @TrnCode,@FuncOpt,@FuncOptDes,@PayType,3,1,@TrnDesc,@NetPayment,0,
--                  0,0,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@NetPayment,
--                  0,@NetPayment,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2);
--    
--            END

--------GROSS SALARY POSTING------------------------------------------------------
--  
--           SET @PayType = @EmpProject;
--           SET @FuncOpt= (SELECT FuncOpt FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 99 AND FuncOpt=75 AND PayType=@PayType);       
--           SET @TrnGLAccNoDr= (SELECT GLAccNoDR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 99 AND FuncOpt=75 AND PayType=@PayType);
--           SET @TrnGLAccNoCr= (SELECT GLAccNoCR FROM A2ZCSMCUS..A2ZTRNCTRL WHERE AccType = 99 AND FuncOpt=75 AND PayType=@PayType);
--
--
--           SET @TrnDesc = 'Salary & Benefits';
--           
-------------- NORMAL TRANSACTION -------------
--           INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
--                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
--                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
--                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID)
--
--           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,0,0,
--                  0,@FuncOpt,0,@PayType,3,0,@TrnDesc,0,@GrossTotal,
--                  0,0,1,@TrnGLAccNoDr,@TrnGLAccNoCr,@TrnGLAccNoCr,@GrossTotal,
--                  @GrossTotal,0,0,0,@FromCashCode,4,0,@userID); 

--------------------------------------------------------------------------------  

	FETCH NEXT FROM postsalaryTable INTO
       @EmpCode,@EmpName,@EmpDesigDesc,@EmpGradeDesc,@EmpProject,@Basic,@Consulated,
       @TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
       @TACodeNo3,@TAAmount3,@TACodeNo4,@TAAmount4,@TACodeNo5,@TAAmount5,
       @TACodeNo6,@TAAmount6,@TACodeNo7,@TAAmount7,@TACodeNo8,@TAAmount8,
       @TACodeNo9,@TAAmount9,@TACodeNo10,@TAAmount10,@GrossTotal,
       @TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
       @TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
       @TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,
       
       @TACodeNo11,@TAAmount11,@TACodeNo12,@TAAmount12,
       @TACodeNo13,@TAAmount13,@TACodeNo14,@TAAmount14,@TACodeNo15,@TAAmount15,
       @TACodeNo16,@TAAmount16,@TACodeNo17,@TAAmount17,@TACodeNo18,@TAAmount18,
       @TACodeNo19,@TAAmount19,@TACodeNo20,@TAAmount20,
       @TDCodeNo11,@TDAmount11,@TDCodeNo12,@TDAmount12,@TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,
       @TDCodeNo15,@TDAmount15,@TDCodeNo16,@TDAmount16,@TDCodeNo17,@TDAmount17,@TDCodeNo18,@TDAmount18,
       @TDCodeNo19,@TDAmount19,@TDCodeNo20,@TDAmount20,
       @DeductTotal,@NetPayment,@Status;

	END

CLOSE postsalaryTable; 
DEALLOCATE postsalaryTable;


DECLARE wfpostsalaryTable CURSOR FOR
SELECT GLCode,GLDrCr,GLDesc,GLAmount
FROM WFPostSalary;

OPEN wfpostsalaryTable;
FETCH NEXT FROM wfpostsalaryTable INTO
@GLCode,@GLDrCr,@GLDesc,@GLAmount;

WHILE @@FETCH_STATUS = 0 
	BEGIN

    IF @GLDrCr = 0
       BEGIN
          SET @TrnGLAccNoDr = @GLCode;
          SET @TrnGLAccNoCr = 0;
          SET @DebitAmt = @GLAmount;
          SET @CreditAmt = 0;
       END
    ELSE
       BEGIN
          SET @TrnGLAccNoDr = 0;
          SET @TrnGLAccNoCr = @GLCode; 
          SET @DebitAmt = 0;
          SET @CreditAmt = @GLAmount;
       END 

    INSERT INTO A2ZCSMCUS..A2ZTRANSACTION(TrnDate,VchNo,VoucherNo,CuType,CuNo,MemNo,AccType,AccNo,
                  TrnCode,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnCredit,TrnDebit,
                  ShowInterest,TrnInterestAmt,TrnCSGL,TrnGLAccNoDr,TrnGLAccNoCr,GLAccNo,GLAmount,
                  GLDebitAmt,GLCreditAmt,TrnFlag,TrnProcStat,FromCashCode,TrnModule,ValueDate,UserID,SalDate,AccTypeMode)

           VALUES (@TrnDate,@VchNo,@VoucherNo,0,0,@EmpCode,0,0,
                  0,75,0,0,3,@GLDrCr,@GLDesc,0,@GLAmount,
                  0,0,0,@TrnGLAccNoDr,@TrnGLAccNoCr,@GLCode,@GLAmount,
                  @DebitAmt,@CreditAmt,0,0,@FromCashCode,4,@TrnDate,@userID,@NewDate,2); 

FETCH NEXT FROM wfpostsalaryTable INTO
       @GLCode,@GLDrCr,@GLDesc,@GLAmount;

    END


SET @TrnDate = (SELECT ProcessDate FROM A2ZHRMCUS..A2ZHRPARAMETER);

SET @NewDate = (DATEADD(MONTH,1,@TrnDate));

SET @NewDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @NewDate)), DATEADD(mm, 1, @NewDate)));


UPDATE A2ZHRMCUS..A2ZHRPARAMETER SET ProcessDate = @NewDate;

---------------------------------------------------------------------

INSERT INTO A2ZHRMCUS..A2ZEMPTSALARY(SalDate,EmpCode,CodeHead,CodeNo,Amount,StatusT,StatusR,UserID)
SELECT @NewDate,EmpCode,CodeHead,CodeNo,Amount,StatusT,StatusR,UserID
FROM A2ZEMPTSALARY WHERE SalDate = @TrnDate; 

UPDATE A2ZHRMCUS..A2ZEMPTSALARY SET Amount = 0, StatusT = 0 WHERE SalDate = @NewDate AND StatusT = 1


CLOSE wfpostsalaryTable; 
DEALLOCATE wfpostsalaryTable;




COMMIT TRANSACTION
		SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH

END



GO

