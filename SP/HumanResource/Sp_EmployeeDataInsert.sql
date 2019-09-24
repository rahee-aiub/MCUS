set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go







ALTER PROCEDURE [dbo].[Sp_EmployeeDataInsert]
(
@EmployeeID int,
@EmployeeName varchar(50),
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

INSERT INTO dbo.A2ZEMPLOYEE(EmpCode, EmpName, EmpGrade, EmpDesignation, EmpServiceType, EmpArea, EmpDepartment, EmpSection, EmpJoinDate, EmpPerDate, EmpLstPSIDate, 
                      EmpNxtPSIDate, EmpBank, EmpAccNo, EmpFName, EmpMName, EmpDOB, EmpSpouseName, EmpNationality, EmpRelagion, EmpGender, EmpMaritalStat, 
                      EmpBloodGrp, EmpHeight, EmpNationalID, EmpTIN, EmpPPNo, EmpIssueDate, EmpPExpireDate, EmpPlaceofIssue, EmpLicenseNo, EmpLExpiryDate,EmpPayLabel
)

VALUES(   @EmployeeID, @EmployeeName, @EmpGrade, @EmpDesignation, @EmpServiceType, @EmpArea, @EmpDepartment, @EmpSection, @EmpJoinDate, @EmpPerDate, @EmpLstPSIDate, 
                      @EmpNxtPSIDate, @EmpBank, @EmpAccNo, @EmpFName, @EmpMName, @EmpDOB, @EmpSpouseName, @EmpNationality, @EmpRelagion, @EmpGender, @EmpMaritalStat, 
                      @EmpBloodGrp, @EmpHeight, @EmpNationalID, @EmpTIN, @EmpPPNo, @EmpIssueDate, @EmpPExpireDate, @EmpPlaceofIssue, @EmpLicenseNo, @EmpLExpiryDate,@EmpPayLabel)


END





