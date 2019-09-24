USE [A2ZHRMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_HRReverseSalaryTransaction]    Script Date: 07/09/2019 4:01:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_HRReverseSalaryTransaction]     


AS
BEGIN

/*

EXECUTE Sp_HRReverseSalaryTransaction

*/


DECLARE @code int;
DECLARE @description nvarchar(50);
DECLARE @repcolumn int;

DECLARE @EmpCode int;
DECLARE @EmpName nvarchar(50);
DECLARE @EmpDesigDesc nvarchar(50);
DECLARE @EmpGradeDesc nvarchar(50);

DECLARE @Area int;

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

DECLARE @DeductTotal money;
DECLARE @NetPayment money;

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

DECLARE @55Flag tinyint;
DECLARE @56Flag tinyint;
DECLARE @57Flag tinyint;
DECLARE @58Flag tinyint;
DECLARE @63PFlag tinyint;
DECLARE @63IFlag tinyint;
DECLARE @65PFlag tinyint;
DECLARE @65IFlag tinyint;


DECLARE @accType int;
DECLARE @accNo BIGINT;

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

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

SET @TrnDate = (SELECT ProcessDate FROM A2ZCSMCUS..A2ZCSPARAMETER);

SET @TrnDate = (SELECT ProcessDate FROM A2ZHRMCUS..A2ZHRPARAMETER);

SET @NewDate = (DATEADD(MONTH,-1,@TrnDate));

SET @NewDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @NewDate)), DATEADD(mm, 1, @NewDate)));

UPDATE A2ZHRMCUS..A2ZHRPARAMETER SET ProcessDate = @NewDate;


DECLARE postsalaryTable CURSOR FOR
SELECT EmpCode,EmpName,EmpDesigDesc,EmpGradeDesc,BasicAmount,ConsolidatedAmt,TACodeNo1,TAAmount1,TACodeNo2,TAAmount2,
TACodeNo3,TAAmount3,TACodeNo4,TAAmount4,TACodeNo5,TAAmount5,TACodeNo6,TAAmount6,TACodeNo7,TAAmount7,
TACodeNo8,TAAmount8,TACodeNo9,TAAmount9,TACodeNo10,TAAmount10,TACodeNo11,TAAmount11,TACodeNo12,TAAmount12,
TACodeNo13,TAAmount13,TACodeNo14,TAAmount14,TACodeNo15,TAAmount15,TACodeNo16,TAAmount16,TACodeNo17,TAAmount17,
TACodeNo18,TAAmount18,TACodeNo19,TAAmount19,TACodeNo20,TAAmount20,GrossTotal,
TDCodeNo1,TDAmount1,TDCodeNo2,TDAmount2,TDCodeNo3,TDAmount3,TDCodeNo4,TDAmount4,
TDCodeNo5,TDAmount5,TDCodeNo6,TDAmount6,TDCodeNo7,TDAmount7,TDCodeNo8,TDAmount8,
TDCodeNo9,TDAmount9,TDCodeNo10,TDAmount10,TDCodeNo11,TDAmount11,TDCodeNo12,TDAmount12,TDCodeNo13,TDAmount13,TDCodeNo14,TDAmount14,
TDCodeNo15,TDAmount15,TDCodeNo16,TDAmount16,TDCodeNo17,TDAmount17,TDCodeNo18,TDAmount18,
TDCodeNo19,TDAmount19,TDCodeNo20,TDAmount20,DeductTotal,NetPayment
FROM A2ZEMPFSALARY WHERE MONTH(SalDate) = MONTH(@NewDate) AND YEAR(SalDate) = YEAR(@NewDate);;

OPEN postsalaryTable;
FETCH NEXT FROM postsalaryTable INTO
@EmpCode,@EmpName,@EmpDesigDesc,@EmpGradeDesc,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
@TACodeNo3,@TAAmount3,@TACodeNo4,@TAAmount4,@TACodeNo5,@TAAmount5,
@TACodeNo6,@TAAmount6,@TACodeNo7,@TAAmount7,@TACodeNo8,@TAAmount8,
@TACodeNo9,@TAAmount9,@TACodeNo10,@TAAmount10,@TACodeNo11,@TAAmount11,@TACodeNo12,@TAAmount12,
@TACodeNo13,@TAAmount13,@TACodeNo14,@TAAmount14,@TACodeNo15,@TAAmount15,
@TACodeNo16,@TAAmount16,@TACodeNo17,@TAAmount17,@TACodeNo18,@TAAmount18,
@TACodeNo19,@TAAmount19,@TACodeNo20,@TAAmount20,@GrossTotal,
@TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
@TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
@TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,@TDCodeNo11,@TDAmount11,@TDCodeNo12,@TDAmount12,@TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,
@TDCodeNo15,@TDAmount15,@TDCodeNo16,@TDAmount16,@TDCodeNo17,@TDAmount17,@TDCodeNo18,@TDAmount18,
@TDCodeNo19,@TDAmount19,@TDCodeNo20,@TDAmount20,@DeductTotal,@NetPayment;

WHILE @@FETCH_STATUS = 0 
	BEGIN
 
        
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
            SET @TDAmount = @TDAmount1;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


   IF @TDCodeNo2 = 1 AND @TDAmount2 > 0
       BEGIN
            SET @TDAmount = @TDAmount2;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo3 = 1 AND @TDAmount3 > 0
       BEGIN
            SET @TDAmount = @TDAmount3;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo4 = 1 AND @TDAmount4 > 0
       BEGIN
            SET @TDAmount = @TDAmount4;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo5 = 1 AND @TDAmount5 > 0
       BEGIN
            SET @TDAmount = @TDAmount5;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo6 = 1 AND @TDAmount6 > 0
       BEGIN
            SET @TDAmount = @TDAmount6;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo7 = 1 AND @TDAmount7 > 0
       BEGIN
            SET @TDAmount = @TDAmount7;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


   IF @TDCodeNo8 = 1 AND @TDAmount8 > 0
       BEGIN
            SET @TDAmount = @TDAmount8;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo9 = 1 AND @TDAmount9 > 0
       BEGIN
            SET @TDAmount = @TDAmount9;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo10 = 1 AND @TDAmount10 > 0
       BEGIN
            SET @TDAmount = @TDAmount10;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END
    

   IF @TDCodeNo11 = 1 AND @TDAmount11 > 0
       BEGIN
            SET @TDAmount = @TDAmount11;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


   IF @TDCodeNo12 = 1 AND @TDAmount12 > 0
       BEGIN
            SET @TDAmount = @TDAmount12;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo13 = 1 AND @TDAmount13 > 0
       BEGIN
            SET @TDAmount = @TDAmount13;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo14 = 1 AND @TDAmount14 > 0
       BEGIN
            SET @TDAmount = @TDAmount14;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo15 = 1 AND @TDAmount15 > 0
       BEGIN
            SET @TDAmount = @TDAmount15;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo16 = 1 AND @TDAmount16 > 0
       BEGIN
            SET @TDAmount = @TDAmount16;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo17 = 1 AND @TDAmount17 > 0
       BEGIN
            SET @TDAmount = @TDAmount17;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


   IF @TDCodeNo18 = 1 AND @TDAmount18 > 0
       BEGIN
            SET @TDAmount = @TDAmount18;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo19 = 1 AND @TDAmount19 > 0
       BEGIN
            SET @TDAmount = @TDAmount19;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo20 = 1 AND @TDAmount20 > 0
       BEGIN
            SET @TDAmount = @TDAmount20;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOfficeBalance = (AccOfficeBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


-------------------  Provident Fund Own --------------------------------

    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
        CuType = 0 AND CuNo=0 AND AccType = 26 AND MemNo = @empcode AND AccStatus = 1);
          

    IF @TDCodeNo1 = 2 AND @TDAmount1 > 0
       BEGIN
            SET @TDAmount = @TDAmount1;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


   IF @TDCodeNo2 = 2 AND @TDAmount2 > 0
       BEGIN
            SET @TDAmount = @TDAmount2;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo3 = 2 AND @TDAmount3 > 0
       BEGIN
            SET @TDAmount = @TDAmount3;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo4 = 2 AND @TDAmount4 > 0
       BEGIN
            SET @TDAmount = @TDAmount4;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo5 = 2 AND @TDAmount5 > 0
       BEGIN
            SET @TDAmount = @TDAmount5;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo6 = 2 AND @TDAmount6 > 0
       BEGIN
            SET @TDAmount = @TDAmount6;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo7 = 2 AND @TDAmount7 > 0
       BEGIN
            SET @TDAmount = @TDAmount7;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


   IF @TDCodeNo8 = 2 AND @TDAmount8 > 0
       BEGIN
            SET @TDAmount = @TDAmount8;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo9 = 2 AND @TDAmount9 > 0
       BEGIN
            SET @TDAmount = @TDAmount9;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo10 = 2 AND @TDAmount10 > 0
       BEGIN
            SET @TDAmount = @TDAmount10;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END
    

   IF @TDCodeNo11 = 2 AND @TDAmount11 > 0
       BEGIN
            SET @TDAmount = @TDAmount11;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


   IF @TDCodeNo12 = 2 AND @TDAmount12 > 0
       BEGIN
            SET @TDAmount = @TDAmount12;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo13 = 2 AND @TDAmount13 > 0
       BEGIN
            SET @TDAmount = @TDAmount13;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo14 = 2 AND @TDAmount14 > 0
       BEGIN
            SET @TDAmount = @TDAmount14;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo15 = 2 AND @TDAmount15 > 0
       BEGIN
            SET @TDAmount = @TDAmount15;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo16 = 2 AND @TDAmount16 > 0
       BEGIN
            SET @TDAmount = @TDAmount16;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo17 = 2 AND @TDAmount17 > 0
       BEGIN
            SET @TDAmount = @TDAmount17;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END


   IF @TDCodeNo18 = 2 AND @TDAmount18 > 0
       BEGIN
            SET @TDAmount = @TDAmount18;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo19 = 2 AND @TDAmount19 > 0
       BEGIN
            SET @TDAmount = @TDAmount19;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

   IF @TDCodeNo20 = 2 AND @TDAmount20 > 0
       BEGIN
            SET @TDAmount = @TDAmount20;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 26 AND AccNo = @accNo;
		END

-------------------  Income Tax --------------------------------

    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
        CuType = 0 AND CuNo=0 AND AccType = 28 AND MemNo = @empcode AND AccStatus = 1);
          

    IF @TDCodeNo1 = 8 AND @TDAmount1 > 0
       BEGIN
            SET @TDAmount = @TDAmount1;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END


   IF @TDCodeNo2 = 8 AND @TDAmount2 > 0
       BEGIN
            SET @TDAmount = @TDAmount2;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo3 = 8 AND @TDAmount3 > 0
       BEGIN
            SET @TDAmount = @TDAmount3;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo4 = 8 AND @TDAmount4 > 0
       BEGIN
            SET @TDAmount = @TDAmount4;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo5 = 8 AND @TDAmount5 > 0
       BEGIN
            SET @TDAmount = @TDAmount5;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo6 = 8 AND @TDAmount6 > 0
       BEGIN
            SET @TDAmount = @TDAmount6;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo7 = 8 AND @TDAmount7 > 0
       BEGIN
            SET @TDAmount = @TDAmount7;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END


   IF @TDCodeNo8 = 8 AND @TDAmount8 > 0
       BEGIN
            SET @TDAmount = @TDAmount8;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo9 = 8 AND @TDAmount9 > 0
       BEGIN
            SET @TDAmount = @TDAmount9;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo10 = 8 AND @TDAmount10 > 0
       BEGIN
            SET @TDAmount = @TDAmount10;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END
    

   IF @TDCodeNo11 = 8 AND @TDAmount11 > 0
       BEGIN
            SET @TDAmount = @TDAmount11;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END


   IF @TDCodeNo12 = 8 AND @TDAmount12 > 0
       BEGIN
            SET @TDAmount = @TDAmount12;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo13 = 8 AND @TDAmount13 > 0
       BEGIN
            SET @TDAmount = @TDAmount13;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo14 = 8 AND @TDAmount14 > 0
       BEGIN
            SET @TDAmount = @TDAmount14;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo15 = 8 AND @TDAmount15 > 0
       BEGIN
            SET @TDAmount = @TDAmount15;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo16 = 8 AND @TDAmount16 > 0
       BEGIN
            SET @TDAmount = @TDAmount16;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo17 = 8 AND @TDAmount17 > 0
       BEGIN
            SET @TDAmount = @TDAmount17;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END


   IF @TDCodeNo18 = 8 AND @TDAmount18 > 0
       BEGIN
            SET @TDAmount = @TDAmount18;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo19 = 8 AND @TDAmount19 > 0
       BEGIN
            SET @TDAmount = @TDAmount19;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

   IF @TDCodeNo20 = 8 AND @TDAmount20 > 0
       BEGIN
            SET @TDAmount = @TDAmount20;             
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET  AccLastTrnTypeU = AccPrevTrnTypeU,  AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU, 
					AccBalance = (AccBalance - @TDAmount), 
                    AccOwnBalance = (AccOwnBalance - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 28 AND AccNo = @accNo;
		END

---------------------Service Loan--------------------------------------------------
  
     SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
        CuType = 0 AND CuNo=0 AND AccType = 55 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);


    IF @TDCodeNo1 = 16 AND @TDAmount1 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount1;
            
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 16 AND @TDAmount2 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount2;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 16 AND @TDAmount3 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount3;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 16 AND @TDAmount4 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount4;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 16 AND @TDAmount5 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount5;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)             
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 16 AND @TDAmount6 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount6;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 16 AND @TDAmount7 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount7;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 16 AND @TDAmount8 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount8;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 16 AND @TDAmount9 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount9;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 16 AND @TDAmount10 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount10;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 16 AND @TDAmount11 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount11;
            
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 16 AND @TDAmount12 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount12;
			UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 16 AND @TDAmount13 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount13;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 16 AND @TDAmount14 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount14;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 16 AND @TDAmount15 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount15;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)             
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 16 AND @TDAmount16 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount16;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 16 AND @TDAmount17 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount17;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 16 AND @TDAmount18 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount18;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 16 AND @TDAmount19 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount19;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 16 AND @TDAmount20 > 0
       BEGIN
            SET @55Flag = 1;
            SET @TDAmount = @TDAmount20;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 55 AND AccNo = @accNo;
		END
    
--------------------------Motor Cycle Loan-------------------------------------------

     SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
           CuType = 0 AND CuNo=0 AND AccType = 56 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);


    IF @TDCodeNo1 = 17 AND @TDAmount1 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount1;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 17 AND @TDAmount2 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount2;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 17 AND @TDAmount3 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount3;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 17 AND @TDAmount4 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount4;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 17 AND @TDAmount5 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount5;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 17 AND @TDAmount6 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount6;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 17 AND @TDAmount7 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount7;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 17 AND @TDAmount8 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount8;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 17 AND @TDAmount9 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount9;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 17 AND @TDAmount10 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount10;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 17 AND @TDAmount11 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount11;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 17 AND @TDAmount12 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount12;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 17 AND @TDAmount13 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount13;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 17 AND @TDAmount14 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount14;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 17 AND @TDAmount15 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount15;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 17 AND @TDAmount16 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount16;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 17 AND @TDAmount17 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount17;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 17 AND @TDAmount18 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount18;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 17 AND @TDAmount19 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount19;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 17 AND @TDAmount20 > 0
       BEGIN
            SET @56Flag = 1;
            SET @TDAmount = @TDAmount20;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 56 AND AccNo = @accNo;
		END  
--------------------------By Cycle Loan---------------------------------------------

     SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
           CuType = 0 AND CuNo=0 AND AccType = 57 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);


    IF @TDCodeNo1 = 18 AND @TDAmount1 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount1;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 18 AND @TDAmount2 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount2;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 18 AND @TDAmount3 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount3;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 18 AND @TDAmount4 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount4;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 18 AND @TDAmount5 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount5;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 18 AND @TDAmount6 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount6;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 18 AND @TDAmount7 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount7;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 18 AND @TDAmount8 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount8;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 18 AND @TDAmount9 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount9;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 18 AND @TDAmount10 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount10;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 18 AND @TDAmount11 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount11;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 18 AND @TDAmount12 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount12;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 18 AND @TDAmount13 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount13;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 18 AND @TDAmount14 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount14;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 18 AND @TDAmount15 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount15;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 18 AND @TDAmount16 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount16;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 18 AND @TDAmount17 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount17;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 18 AND @TDAmount18 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount18;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 18 AND @TDAmount19 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount19;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 18 AND @TDAmount20 > 0
       BEGIN
            SET @57Flag = 1;
            SET @TDAmount = @TDAmount20;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 57 AND AccNo = @accNo;
		END
   
------------------------Computer Loan------------------------------------------------

    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
           CuType = 0 AND CuNo=0 AND AccType = 58 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);

    IF @TDCodeNo1 = 19 AND @TDAmount1 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount1;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 19 AND @TDAmount2 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount2;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 19 AND @TDAmount3 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount3;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 19 AND @TDAmount4 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount4;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 19 AND @TDAmount5 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount5;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 19 AND @TDAmount6 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount6;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 19 AND @TDAmount7 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount7;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 19 AND @TDAmount8 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount8;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 19 AND @TDAmount9 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount9;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 19 AND @TDAmount10 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount10;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 19 AND @TDAmount11 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount11;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 19 AND @TDAmount12 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount12;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 19 AND @TDAmount13 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount13;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 19 AND @TDAmount14 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount14;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 19 AND @TDAmount15 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount15;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 19 AND @TDAmount16 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount16;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 19 AND @TDAmount17 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount17;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 19 AND @TDAmount18 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount18;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 19 AND @TDAmount19 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount19;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 19 AND @TDAmount20 > 0
       BEGIN
            SET @58Flag = 1;
            SET @TDAmount = @TDAmount20;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 58 AND AccNo = @accNo;
		END
---------------------------PF Loan Principal--------------------------------------------
    
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
           CuType = 0 AND CuNo=0 AND AccType = 63 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);

    IF @TDCodeNo1 = 20 AND @TDAmount1 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount1;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 20 AND @TDAmount2 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount2;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 20 AND @TDAmount3 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount3;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 20 AND @TDAmount4 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount4;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 20 AND @TDAmount5 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount5;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 20 AND @TDAmount6 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount6;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 20 AND @TDAmount7 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount7;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 20 AND @TDAmount8 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount8;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 20 AND @TDAmount9 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount9;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 20 AND @TDAmount10 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount10;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 20 AND @TDAmount11 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount11;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 20 AND @TDAmount12 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount12;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 20 AND @TDAmount13 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount13;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 20 AND @TDAmount14 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount14;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 20 AND @TDAmount15 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount15;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 20 AND @TDAmount16 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount16;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 20 AND @TDAmount17 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount17;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 20 AND @TDAmount18 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount18;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 20 AND @TDAmount19 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount19;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 20 AND @TDAmount20 > 0
       BEGIN
            SET @63PFlag = 1;
            SET @TDAmount = @TDAmount20;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 63 AND AccNo = @accNo;

	END


---------------------------EDU Loan Principal--------------------------------------------
    
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
           CuType = 0 AND CuNo=0 AND AccType = 65 AND MemNo = @empcode AND AccStatus = 1 AND AccBalance < 0);

    IF @TDCodeNo1 = 22 AND @TDAmount1 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount1;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo2 = 22 AND @TDAmount2 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount2;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END    

    IF @TDCodeNo3 = 22 AND @TDAmount3 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount3;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END   

    IF @TDCodeNo4 = 22 AND @TDAmount4 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount4;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo5 = 22 AND @TDAmount5 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount5;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo6 = 22 AND @TDAmount6 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount6;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo7 = 22 AND @TDAmount7 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount7;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END    

    IF @TDCodeNo8 = 22 AND @TDAmount8 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount8;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END   

    IF @TDCodeNo9 = 22 AND @TDAmount9 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount9;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo10 = 22 AND @TDAmount10 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount10;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo11 = 22 AND @TDAmount11 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount11;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo12 = 22 AND @TDAmount12 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount12;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END    

    IF @TDCodeNo13 = 22 AND @TDAmount13 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount13;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END   

    IF @TDCodeNo14 = 22 AND @TDAmount14 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount14;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo15 = 22 AND @TDAmount15 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount15;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo16 = 22 AND @TDAmount16 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount16;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo17 = 22 AND @TDAmount17 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount17;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END    

    IF @TDCodeNo18 = 22 AND @TDAmount18 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount18;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END   

    IF @TDCodeNo19 = 22 AND @TDAmount19 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount19;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount),
                    AccTotalDep = (AccTotalDep - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;
		END

    IF @TDCodeNo20 = 22 AND @TDAmount20 > 0
       BEGIN
            SET @65PFlag = 1;
            SET @TDAmount = @TDAmount20;
            UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @TDAmount),
                    AccPrincipal = (AccPrincipal - @TDAmount)
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 65 AND AccNo = @accNo;

	END


---------------------------------------------------------------------------------


-----------------------------------------------------------------------------
    SET @accNo= (SELECT AccNo FROM A2ZCSMCUS..A2ZACCOUNT WHERE 
                CuType = 0 AND CuNo=0 AND AccType = 25 AND MemNo = @empcode AND AccStatus = 1);

           UPDATE A2ZCSMCUS..A2ZACCOUNT SET AccLastTrnTypeU = AccPrevTrnTypeU, 
                    AccLastTrnDateU = AccPrevTrnDateU,
					AccLastTrnAmtU = AccPrevTrnAmtU,
                    AccBalance = (AccBalance - @NetPayment) 
                    
			WHERE CuType = 0 AND CuNo = 0 AND MemNo = @EmpCode AND AccType = 25 AND AccNo = @accNo;
---------------------------------------------------------------------------------------
	FETCH NEXT FROM postsalaryTable INTO
       @EmpCode,@EmpName,@EmpDesigDesc,@EmpGradeDesc,@Basic,@Consulated,@TACodeNo1,@TAAmount1,@TACodeNo2,@TAAmount2,
       @TACodeNo3,@TAAmount3,@TACodeNo4,@TAAmount4,@TACodeNo5,@TAAmount5,
       @TACodeNo6,@TAAmount6,@TACodeNo7,@TAAmount7,@TACodeNo8,@TAAmount8,
       @TACodeNo9,@TAAmount9,@TACodeNo10,@TAAmount10,@TACodeNo11,@TAAmount11,@TACodeNo12,@TAAmount12,
       @TACodeNo13,@TAAmount13,@TACodeNo14,@TAAmount14,@TACodeNo15,@TAAmount15,
       @TACodeNo16,@TAAmount16,@TACodeNo17,@TAAmount17,@TACodeNo18,@TAAmount18,
       @TACodeNo19,@TAAmount19,@TACodeNo20,@TAAmount20,@GrossTotal,
       @TDCodeNo1,@TDAmount1,@TDCodeNo2,@TDAmount2,@TDCodeNo3,@TDAmount3,@TDCodeNo4,@TDAmount4,
       @TDCodeNo5,@TDAmount5,@TDCodeNo6,@TDAmount6,@TDCodeNo7,@TDAmount7,@TDCodeNo8,@TDAmount8,
       @TDCodeNo9,@TDAmount9,@TDCodeNo10,@TDAmount10,@TDCodeNo11,@TDAmount11,@TDCodeNo12,@TDAmount12,@TDCodeNo13,@TDAmount13,@TDCodeNo14,@TDAmount14,
       @TDCodeNo15,@TDAmount15,@TDCodeNo16,@TDAmount16,@TDCodeNo17,@TDAmount17,@TDCodeNo18,@TDAmount18,
       @TDCodeNo19,@TDAmount19,@TDCodeNo20,@TDAmount20,@DeductTotal,@NetPayment;



	END


--SET @TrnDate = (SELECT ProcessDate FROM A2ZHRMCUS..A2ZHRPARAMETER);
--
--SET @NewDate = (DATEADD(MONTH,-1,@TrnDate));
--
--SET @NewDate = (DATEADD(dd, -DAY(DATEADD(mm, 1, @NewDate)), DATEADD(mm, 1, @NewDate)));
--
--UPDATE A2ZHRMCUS..A2ZHRPARAMETER SET ProcessDate = @NewDate;


DELETE FROM A2ZCSMCUS..A2ZTRANSACTION  WHERE SalDate = @NewDate AND FuncOpt = 75;

DELETE FROM A2ZHRMCUS..A2ZEMPFSALARY  WHERE SalDate >= @NewDate;

DELETE FROM A2ZHRMCUS..A2ZEMPTSALARY  WHERE SalDate > @NewDate;



CLOSE postsalaryTable; 
DEALLOCATE postsalaryTable;

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

