USE [A2ZHRMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmployeeAddressDataInsert]    Script Date: 04/29/2015 15:46:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_EmployeeAddressDataInsert]
(
@EmployeeID int,
@EmpPresentAddress varchar(200),
@EmpPreDivision smallint,
@EmpPreDistrict smallint,
@EmpPreThana smallint,
@EmpPreTelNo varchar(50),
@EmpPreMobileNo varchar(50),
@EmpPreEmail varchar(50),
@EmpPermanentAddress varchar(200),
@EmpPerDivision smallint,
@EmpPerDistrict smallint,
@EmpPerThana smallint,
@EmpPerTelNo varchar(50),
@EmpPerMobileNo varchar(50),
@EmpPerEmail varchar(50)
)

AS

BEGIN

INSERT INTO dbo.A2ZEMPADDRESS(EmployeeID, EmpPresentAddress, EmpPreDivision, EmpPreDistrict, EmpPreThana, EmpPreTelNo, EmpPreMobileNo, EmpPreEmail, EmpPermanentAddress, 
                      EmpPerDivision, EmpPerDistrict, EmpPerThana, EmpPerTelNo, EmpPerMobileNo, EmpPerEmail)

VALUES( @EmployeeID, @EmpPresentAddress, @EmpPreDivision, @EmpPreDistrict, @EmpPreThana, @EmpPreTelNo, @EmpPreMobileNo, @EmpPreEmail, @EmpPermanentAddress, 
                      @EmpPerDivision, @EmpPerDistrict, @EmpPerThana, @EmpPerTelNo, @EmpPerMobileNo, @EmpPerEmail)


END

