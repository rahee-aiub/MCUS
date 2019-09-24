USE [A2ZHRMCUS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_HREmpGeneralInformation]    Script Date: 02/11/2019 1:29:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Sp_HREmpGeneralInformation](@CommonNo1 int,@CommonNo11 INT,@CommonNo2 INT,@CommonNo3 INT,@CommonNo4 INT,@CommonNo5 INT,@CommonNo6 INT,@CommonNo7 INT,@CommonNo8 INT,@CommonNo9 INT,@CommonNo10 INT)  

AS
BEGIN


/*

EXECUTE Sp_rptHREmpGeneralInformation 0,0,0,0

*/

DECLARE @strSQL NVARCHAR(MAX);

DECLARE @Area int;
DECLARE @Location int;
DECLARE @Project int;
DECLARE @Religion int;
DECLARE @Gender int;
DECLARE @Desig int;
DECLARE @ServType int;
DECLARE @Status int;
DECLARE @BaseGrade int;
DECLARE @Grade int;
DECLARE @Steps int;

set @Area = @CommonNo1;
set @Location = @CommonNo11;
set @Project = @CommonNo2;
set @Religion = @CommonNo3;
set @Gender = @CommonNo4;
set @Desig = @CommonNo5;
set @ServType = @CommonNo6;
set @Status = @CommonNo7;
set @BaseGrade = @CommonNo8;
set @Grade = @CommonNo9;
set @Steps = @CommonNo10;



TRUNCATE TABLE WFA2ZEMPGENINFO;

INSERT INTO WFA2ZEMPGENINFO (EmpCode,EmpName,EmpBaseGrade,EmpGrade,EmpGradeDesc,EmpDesignation,EmpDesigDesc,
EmpServiceType,EmpSTypeDesc,EmpProject,EmpProjectDesc,EmpArea,EmpAreaDesc,EmpLocation,EmpLocationDesc,EmpJoinDate,EmpPerDate,
EmpSection,EmpSectionDesc,EmpDepartment,EmpDepartmentDesc,EmpPayLabel,EmpRelagion,EmpRelagionDesc,EmpGender,Status) 
SELECT 
EmpCode,EmpName,EmpBaseGrade,EmpGrade,EmpGradeDesc,EmpDesignation,EmpDesigDesc,
EmpServiceType,EmpSTypeDesc,EmpProject,EmpProjectDesc,EmpArea,EmpAreaDesc,EmpLocation,EmpLocationDesc,EmpJoinDate,EmpPerDate,
EmpSection,EmpSectionDesc,EmpDepartment,EmpDepartmentDesc,EmpPayLabel,EmpRelagion,EmpRelagionDesc,EmpGender,Status
FROM A2ZEMPLOYEE; 


IF @Area != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpArea != @Area;
   END

IF @Location != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpLocation != @Location;
   END

IF @Project != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpProject != @Project;
   END

IF @Religion != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpRelagion != @Religion;
   END

IF @Gender != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpGender != @Gender;
   END

IF @Desig != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpDesignation != @Desig;
   END

IF @ServType != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpServiceType != @ServType;
   END

IF @Status != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE Status != @Status;
   END

IF @BaseGrade != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpBaseGrade != @BaseGrade;
   END

IF @Grade != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpGrade != @Grade;
   END

IF @Steps != 0
   BEGIN
     DELETE FROM WFA2ZEMPGENINFO  WHERE EmpPayLabel != @Steps;
   END

END

GO

