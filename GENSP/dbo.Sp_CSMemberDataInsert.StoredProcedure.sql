USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSMemberDataInsert]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_CSMemberDataInsert]
(
@CuType smallint,
@CuNo   int,
@MemNo  int,
@MemName nvarchar(50),
@MemFName nvarchar(50),
@MemMName nvarchar(50),
@MemSpouseName  nvarchar(50),
@MemOccupation smallint,
@MemNationality smallint,
@MemGender smallint,
@MemReligion smallint,
@MemNature smallint,
@MemMaritalStatus smallint,
@MemOpenDate smalldatetime,
@MemDOB smalldatetime, 
@MemPOB nvarchar(50),
@MemPreAdd1 nvarchar(90),
@MemPreAdd2 nvarchar(90),
@MemPreAdd3 nvarchar(90),
@MemPreDivi int,
@MemPreDist int,
@MemPreUpzila int,
@MemPreThana int,
@MemPreTelephone nvarchar(30),
@MemPreMobile nvarchar(30),
@MemPreEmail  nvarchar(30),
@MemPerAdd1 nvarchar(90),
@MemPerAdd2 nvarchar(90),
@MemPerAdd3 nvarchar(90),
@MemPerDivi int,
@MemPerDist int,
@MemPerUpzila int,
@MemPerThana int,
@MemPerTelephone nvarchar(30),
@MemPerMobile nvarchar(30),
@MemPerEmail  nvarchar(30),
@MemEmpNm     nvarchar(50),
@MemEmpAdd    nvarchar(90),
@MemIntroMNo1 nvarchar(20),
@MemIntroNm1  nvarchar(50),
@MemIntroMNo2 nvarchar(20),
@MemIntroNm2  nvarchar(50),
@MemNationalId nvarchar(50),
@MemPPNo nvarchar(30),
@MemPPIssDt smalldatetime,
@MemPPExpDt smalldatetime,
@MemPPIssPlace  nvarchar(50),
@MemTin nvarchar(30),
@MemLastTaxDt smalldatetime,
@MemType smallint)

AS

BEGIN

INSERT INTO dbo.A2ZMEMBER(CuType, CuNo, MemNo, MemName, MemFName, MemMName, MemSpouseName, MemOccupation, MemNationality, MemGender, MemReligion, MemNature, 
                      MemMaritalStatus, MemOpenDate, MemDOB, MemPOB, MemPreAdd1, MemPreAdd2, MemPreAdd3, MemPreDivi, MemPreDist, MemPreUpzila,MemPreThana, MemPreTelephone, 
                      MemPreMobile, MemPreEmail, MemPerAdd1, MemPerAdd2, MemPerAdd3, MemPerDivi, MemPerDist, MemPerUpzila,MemPerThana, MemPerTelephone, MemPerMobile, 
                      MemPerEmail, MemEmpNm, MemEmpAdd, MemIntroMNo1, MemIntroNm1, MemIntroMNo2, MemIntroNm2, MemNationalId, MemPPNo, MemPPIssDt, MemPPExpDt, 
                      MemPPIssPlace, MemTin, MemLastTaxDt, MemType)

VALUES(@CuType,@CuNo, @MemNo, @MemName, @MemFName, @MemMName, @MemSpouseName, @MemOccupation, @MemNationality, @MemGender, @MemReligion, @MemNature, 
                      @MemMaritalStatus, @MemOpenDate, @MemDOB, @MemPOB, @MemPreAdd1, @MemPreAdd2, @MemPreAdd3, @MemPreDivi, @MemPreDist, @MemPreUpzila, @MemPreThana, @MemPreTelephone, 
                      @MemPreMobile, @MemPreEmail, @MemPerAdd1, @MemPerAdd2, @MemPerAdd3, @MemPerDivi, @MemPerDist, @MemPerUpzila, @MemPerThana, @MemPerTelephone, @MemPerMobile, 
                      @MemPerEmail, @MemEmpNm, @MemEmpAdd, @MemIntroMNo1, @MemIntroNm1, @MemIntroMNo2, @MemIntroNm2, @MemNationalId, @MemPPNo, @MemPPIssDt, @MemPPExpDt, 
                      @MemPPIssPlace, @MemTin, @MemLastTaxDt, @MemType)


END

GO
