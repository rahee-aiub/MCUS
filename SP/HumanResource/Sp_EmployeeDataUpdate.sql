set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go








ALTER PROCEDURE [dbo].[Sp_EmployeeDataUpdate]
(
@EmpCode int,
@EmpName varchar(50),
@EmpGrade smallint,
@EmpDesignation smallint,
@EmpServiceType smallint,
@EmpArea smallint,
@EmpDepartment smallint,
@EmpSection smallint,
@EmpJoinDate smalldatetime,
@EmpPerDate smalldatetime,
@EmpLstPSIDate smalldatetime,
@EmpNxtPSIDate smalldatetime,
@EmpBank smallint,
@EmpAccNo varchar(50),
@EmpFName  varchar(50),
@EmpMName  varchar(50),
@EmpDOB smalldatetime,
@EmpSpouseName varchar(50),
@EmpNationality smallint,
@EmpRelagion smallint,
@EmpGender smallint,
@EmpMaritalStat smallint,
@EmpBloodGrp varchar(50),
@EmpHeight varchar(50),
@EmpNationalID varchar(50),
@EmpTIN  varchar(50),
@EmpPPNo varchar(50),
@EmpIssueDate smalldatetime,
@EmpPExpireDate smalldatetime, 
@EmpPlaceofIssue varchar(50),
@EmpLicenseNo varchar(50),
@EmpLExpiryDate smalldatetime,
@EmpPayLabel int
)

AS

BEGIN

UPDATE dbo.A2ZEMPLOYEE SET
EmpCode=@EmpCode,
EmpName=@EmpName, 
EmpGrade= @EmpGrade,
EmpDesignation= @EmpDesignation,
EmpServiceType=@EmpServiceType,
EmpArea =@EmpArea,
EmpDepartment=@EmpDepartment,
EmpSection=@EmpSection,
EmpJoinDate= @EmpJoinDate,
EmpPerDate=@EmpPerDate,
EmpLstPSIDate=@EmpLstPSIDate,
EmpNxtPSIDate=@EmpNxtPSIDate,
EmpBank=@EmpBank,
EmpAccNo=@EmpAccNo,
EmpFName =@EmpFName,
EmpMName =@EmpMName,
EmpDOB=@EmpDOB,
EmpSpouseName=EmpSpouseName,
EmpNationality=EmpNationality,
EmpRelagion= @EmpRelagion,
EmpGender=@EmpGender,
EmpMaritalStat=EmpMaritalStat,
EmpBloodGrp=@EmpBloodGrp,
EmpHeight=@EmpHeight,
EmpNationalID=@EmpNationalID,
EmpTIN=@EmpTIN,
EmpPPNo=@EmpPPNo,
EmpIssueDate=@EmpIssueDate ,
EmpPExpireDate=@EmpPExpireDate, 
EmpPlaceofIssue=@EmpPlaceofIssue,
EmpLicenseNo=@EmpLicenseNo,
EmpLExpiryDate=@EmpLExpiryDate,
EmpPayLabel=@EmpPayLabel

WHERE EmpCode=@EmpCode 

END






