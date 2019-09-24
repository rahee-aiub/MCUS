USE [A2ZHRMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmployeeAddressDataUpdate]    Script Date: 04/29/2015 15:46:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sp_EmployeeAddressDataUpdate]
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

UPDATE dbo.A2ZEMPADDRESS SET
EmployeeID=@EmployeeID, 
EmpPresentAddress=@EmpPresentAddress, 
EmpPreDivision=@EmpPreDivision, 
EmpPreDistrict=@EmpPreDistrict, 
EmpPreThana=@EmpPreThana, 
EmpPreTelNo=@EmpPreTelNo, 
EmpPreMobileNo=@EmpPreMobileNo, 
EmpPreEmail=@EmpPreEmail, 
EmpPermanentAddress=@EmpPermanentAddress, 
EmpPerDivision=@EmpPerDivision, 
EmpPerDistrict=@EmpPerDistrict, 
EmpPerThana=@EmpPerThana, 
EmpPerTelNo=@EmpPerTelNo, 
EmpPerMobileNo=@EmpPerMobileNo, 
EmpPerEmail=@EmpPerEmail

END

