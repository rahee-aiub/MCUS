USE [A2ZCSMCUS]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CSGetInfoMember]    Script Date: 1/4/2018 1:40:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE  [dbo].[Sp_CSGetInfoMember]
@CuType INT
,@CUNo INT
,@MemNo INT



AS

BEGIN


SELECT 
CuType
,CuNo
,MemNo
,MemName
,MemFName
,MemMName
,MemSpouseName
,MemOccupation
,MemNationality
,MemGender
,MemReligion
,MemNature
,MemMaritalStatus
,MemOpenDate
,MemDOB
,MemPOB
,MemPreAdd1
,MemPreAdd2
,MemPreAdd3
,MemPreDivi
,MemPreDist
,MemPreUpzila
,MemPreThana
,MemPreTelephone
,MemPreMobile
,MemPreEmail
,MemPerAdd1
,MemPerAdd2
,MemPerAdd3
,MemPerDivi
,MemPerDist
,MemPerUpzila
,MemPerThana
,MemPerTelephone
,MemPerMobile
,MemPerEmail
,MemEmpNm
,MemEmpAdd
,MemIntroMNo1
,MemIntroNm1
,MemIntroMNo2
,MemIntroNm2
,MemNationalId
,MemPPNo
,MemPPIssDt
,MemPPExpDt
,MemPPIssPlace
,MemTin
,MemLastTaxDt
,MemType
,MemOldMemNo
FROM A2ZMEMBER WHERE  CuType = @CuType AND CuNo = @CUNo AND MemNo = @MemNo

END












GO
